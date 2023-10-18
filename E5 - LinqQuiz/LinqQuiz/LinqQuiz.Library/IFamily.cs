namespace LinqQuiz.Library;

public interface IFamily
{
    int ID { get; }
    IReadOnlyCollection<IPerson> Persons { get; }
}