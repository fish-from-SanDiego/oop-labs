using Lab5.Application.Abstractions.ResultTypes;

namespace Lab5.Application.Abstractions.Entities;

public interface IFetchedCollection<out T> : IReadOnlyCollection<T>
{
    public GetResult GetResult { get; }
    public IReadOnlyCollection<T> Collection { get; }
}