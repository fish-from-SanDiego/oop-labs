using System.Collections;
using Lab5.Application.Abstractions.Entities;
using Lab5.Application.Abstractions.ResultTypes;

namespace Lab5.Infrastructure.DataAccess.Entities;

public class FetchedCollection<T> : IFetchedCollection<T>
{
    public FetchedCollection(IReadOnlyCollection<T> collection, GetResult getResult)
    {
        Collection = collection;
        GetResult = getResult;
    }

    public int Count => Collection.Count;
    public GetResult GetResult { get; }
    public IReadOnlyCollection<T> Collection { get; }

    public IEnumerator<T> GetEnumerator()
    {
        return Collection.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}