using System.Collections;

namespace lab_7;

public sealed class GenericLinkedList<T> : IEnumerable<T>
{
    private LinkedListNode<T>? _first;
    private LinkedListNode<T>? _last;

    public int Count { get; private set; }

    public void AddFirst(T value)
    {
        var node = new LinkedListNode<T>(value) { Next = _first };

        if (_first is null)
        {
            _last = node;
        }
        else
        {
            _first.Previous = node;
        }

        _first = node;
        Count++;
    }

    public void AddLast(T value)
    {
        var node = new LinkedListNode<T>(value) { Previous = _last };

        if (_last is null)
        {
            _first = node;
        }
        else
        {
            _last.Next = node;
        }

        _last = node;
        Count++;
    }

    public void Clear()
    {
        _first = null;
        _last = null;
        Count = 0;
    }

    public bool Contains(T value) => Find(value) is not null;

    public LinkedListNode<T>? Find(T value)
    {
        var comparer = EqualityComparer<T>.Default;

        for (var node = _first; node is not null; node = node.Next)
        {
            if (comparer.Equals(node.Value, value))
            {
                return node;
            }
        }

        return null;
    }

    public LinkedListNode<T>? FindLast(T value)
    {
        var comparer = EqualityComparer<T>.Default;

        for (var node = _last; node is not null; node = node.Previous)
        {
            if (comparer.Equals(node.Value, value))
            {
                return node;
            }
        }

        return null;
    }

    public bool Remove(T value)
    {
        var node = Find(value);
        if (node is null)
        {
            return false;
        }

        RemoveNode(node);
        return true;
    }

    public void RemoveFirst()
    {
        if (_first is null)
        {
            throw new InvalidOperationException("Список порожній.");
        }

        RemoveNode(_first);
    }

    public void RemoveLast()
    {
        if (_last is null)
        {
            throw new InvalidOperationException("Список порожній.");
        }

        RemoveNode(_last);
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (var node = _first; node is not null; node = node.Next)
        {
            yield return node.Value;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private void RemoveNode(LinkedListNode<T> node)
    {
        if (node.Previous is null)
        {
            _first = node.Next;
        }
        else
        {
            node.Previous.Next = node.Next;
        }

        if (node.Next is null)
        {
            _last = node.Previous;
        }
        else
        {
            node.Next.Previous = node.Previous;
        }

        node.Next = null;
        node.Previous = null;
        Count--;
    }
}

public sealed class LinkedListNode<T>(T value)
{
    public T Value { get; } = value;
    public LinkedListNode<T>? Next { get; internal set; }
    public LinkedListNode<T>? Previous { get; internal set; }
}
