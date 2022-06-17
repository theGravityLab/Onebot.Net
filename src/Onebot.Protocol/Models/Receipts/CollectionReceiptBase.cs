using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Onebot.Protocol.Models.Receipts;

public abstract record CollectionReceiptBase<T> : ReceiptBase, ICollection<T>
{
    private readonly Collection<T> inner = new();

    public IEnumerator<T> GetEnumerator() => inner.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => inner.GetEnumerator();

    public void Add(T item) => inner.Add(item);

    public void Clear() => inner.Clear();

    public bool Contains(T item) => inner.Contains(item);

    public void CopyTo(T[] array, int arrayIndex) => inner.CopyTo(array, arrayIndex);

    public bool Remove(T item) => inner.Remove(item);

    public int Count => inner.Count;
    public bool IsReadOnly => false;
}