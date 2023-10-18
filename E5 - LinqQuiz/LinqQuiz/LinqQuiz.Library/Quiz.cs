using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Numerics;

namespace LinqQuiz.Library;

public static class Quiz
{
    /// <summary>
    /// Returns all even numbers between 1 and the specified upper limit.
    /// </summary>
    /// <param name="exclusiveUpperLimit">Upper limit (exclusive)</param>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Thrown if <paramref name="exclusiveUpperLimit"/> is lower than 1.
    /// </exception>
    public static int[] GetEvenNumbers(int exclusiveUpperLimit)
    {
        return Enumerable.Range(1, (exclusiveUpperLimit >= 1) ? exclusiveUpperLimit - 1 : throw new ArgumentOutOfRangeException($"{exclusiveUpperLimit} is lower than 1"))
            .Where(x => int.IsEvenInteger(x))
            .ToArray();
    }

    /// <summary>
    /// Returns the squares of the numbers between 1 and the specified upper limit 
    /// that can be divided by 7 without a remainder (see also remarks).
    /// </summary>
    /// <param name="exclusiveUpperLimit">Upper limit (exclusive)</param>
    /// <exception cref="OverflowException">
    ///     Thrown if the calculating the square results in an overflow for type <see cref="System.Int32"/>.
    /// </exception>
    /// <remarks>
    /// The result is an empty array if <paramref name="exclusiveUpperLimit"/> is lower than 1.
    /// The result is in descending order.
    /// </remarks>
    public static int[] GetSquares(int exclusiveUpperLimit)
    {
        return Enumerable.Range(1, (exclusiveUpperLimit >= 1) ? exclusiveUpperLimit - 1 : 1)
            .Where(x => x % 7 == 0)
            .Select(x => Convert.ToInt32(Math.Pow(x, 2)))
            .OrderByDescending(x => x)
            .ToArray();
    }
    /// <summary>
    /// Returns a statistic about families.
    /// </summary>
    /// <param name="families">Families to analyze</param>
    /// <returns>
    /// Returns one statistic entry per family in <paramref name="families"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    ///     Thrown if <paramref name="families"/> is <c>null</c>.
    /// </exception>
    /// <remarks>
    /// <see cref="FamilySummary.AverageAge"/> is set to 0 if <see cref="IFamily.Persons"/>
    /// in <paramref name="families"/> is empty.
    /// </remarks>
    public static FamilySummary[] GetFamilyStatistic(IReadOnlyCollection<IFamily> families)
    {
        return families.Select(family => new FamilySummary
        {
            FamilyID = family.ID,
            AverageAge = family.Persons.Any() ? family.Persons.Average(person => person.Age) : 0,
            NumberOfFamilyMembers = family.Persons.Count
        }).ToArray();
    }

    /// <summary>
    /// Returns a statistic about the number of occurrences of letters in a text.
    /// </summary>
    /// <param name="text">Text to analyze</param>
    /// <returns>
    /// Collection containing the number of occurrences of each letter (see also remarks).
    /// </returns>
    /// <remarks>
    /// Casing is ignored (e.g. 'a' is treated as 'A'). Only letters between A and Z are counted;
    /// special characters, numbers, whitespaces, etc. are ignored. The result only contains
    /// letters that are contained in <paramref name="text"/> (i.e. there must not be a collection element
    /// with number of occurrences equal to zero.
    /// </remarks>
    public static (char letter, int numberOfOccurrences)[] GetLetterStatistic(string text)
    {
        return text.Where(char.IsLetter)
            .Select(char.ToUpper)
            .GroupBy(c => c)
            .Select(group => (group.Key, group.Count()))
            .ToArray();
    }
}