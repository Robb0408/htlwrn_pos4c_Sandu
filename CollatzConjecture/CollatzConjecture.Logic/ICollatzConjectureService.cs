namespace CollatzConjecture.Logic;

public interface ICollatzConjectureService
{
    // This method should return true if the sequence reaches 1, otherwise false 
    // (which, according to the conjecture, should never happen if the input is a positive integer).
    bool IsSequenceValid(int number); 

    // This method should return true if the sequence reaches 1, otherwise false 
    // IMPORTANT: the given number can be bigger then the MaxValue of every DataType in C#
    // The calculation of the number is done with "paper-like" algorithms
    // Do not convert the string parameter to numeric variable with Convert or Parse
    bool IsSequenceValid(string number);

    // This method returns every step of the sequence 
    List<int> GetSequence(int number);

    // This method returns every step of the sequence as string
    List<string> GetSequence(string number);
}