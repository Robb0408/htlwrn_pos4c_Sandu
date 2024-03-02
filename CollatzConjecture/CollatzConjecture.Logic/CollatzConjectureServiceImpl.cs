using System.Text;
using System.Text.RegularExpressions;

namespace CollatzConjecture.Logic;

public class CollatzConjectureServiceImpl : ICollatzConjectureService
{
    public bool IsSequenceValid(int number)
    {
        var result = GetSequence(number); 
        return result.Count > 0 && result[^1] == 1;
    }

    public bool IsSequenceValid(string number)
    {
        var result = GetSequence(number); 
        return result.Count > 0 && result[^1] == "1";
    }

    public List<int> GetSequence(int number)
    {
        var current = number;
        var sequence = new List<int>();
        do
        {
            sequence.Add(current);
            if (int.IsEvenInteger(current))
            {
                current /= 2;
            }
            else
            {
                current = current * 3 + 1;
            }
        } while (current != 1);
        sequence.Add(current);
        return sequence;
    }

    public List<string> GetSequence(string number)
    {
        if (Regex.IsMatch(number, @".*\D+.*"))
        {
            Console.Error.WriteLine("Invalid input");
            return [];
        }
        var n = number;
        var sequence = new List<string>();
        do
        {
            sequence.Add(n);
            Console.WriteLine(n);
            if (int.TryParse(n[^1].ToString(), out var lastDigit) && lastDigit % 2 == 0)
            {
                n = DivideByTwo(n);
            }
            else
            {
                n = MultiplyByThree(n);
                n = AddOne(n);
            }

        } while (n != "1");
        sequence.Add(n);
        return sequence;
    }

    public string AddOne(string number)
    {
        var index = 1;
        var sb = new StringBuilder();
        var carry = 0;
        do
        {
            if (number[^index] == '9')
            {
                sb.Append(0);
                carry = 1;
            }
            else
            {
                if (carry == 0)
                {
                    sb.Append(int.Parse(number[^index].ToString()) + 1);
                }
                else
                {
                    sb.Append(int.Parse(number[^index].ToString()) + carry);
                }

                carry = 0;
            }

            index++;
        } while (index <= number.Length && carry == 1);

        if (carry == 1)
        {
            sb.Append(1);
        }

        var calculated = new string(sb.ToString().Reverse().ToArray());
        sb.Clear();
        if (number.Length != 1)
        {
            sb.Append(number[..^(index - 1)]);
        }

        sb.Append(calculated);
        return sb.ToString();
    }

    public string MultiplyByThree(string number)
    {
        var index = 1;
        var sb = new StringBuilder();
        var carry = 0;
        do
        {
            var result = int.Parse(number[^index].ToString()) * 3 + carry;
            carry = result / 10;
            sb.Append(result % 10);
            index++;
        } while (index <= number.Length);

        if (carry > 0)
        {
            sb.Append(carry);
        }

        var calculated = new string(sb.ToString().Reverse().ToArray());
        sb.Clear();
        if (number.Length != 1)
        {
            sb.Append(number[..^(index - 1)]);
        }

        sb.Append(calculated);
        return sb.ToString();
    }

    public string DivideByTwo(string number)
    {
        // Add zero at the end to make sure to avoid out of range exception,
        // the zero is just a placeholder and will be not used for calculation
        var n = number + "0";
        var sb = new StringBuilder();
        var index = 0;
        var currentNumber = int.Parse(n[index].ToString());
        do
        {
            index++;
            sb.Append(currentNumber / 2);
            currentNumber = currentNumber % 2 * 10 + int.Parse(n[index].ToString());
        } while (index < number.Length);
        // Remove zeros before first non-zero digit
        return sb.ToString().TrimStart('0');
    }
}