using System.Collections.Generic;
using System;

public class PriorityQueue<T>
{
    private List<T> data;
    private Func<T, T, int> comparer;

    public int Count => data.Count;

    public bool IsEmpty() => data.Count == 0;

    public PriorityQueue()
    {
        data = new List<T>();
        comparer = Comparer<T>.Default.Compare;
    }

    public PriorityQueue(Func<T, T, int> customComparer)
    {
        data = new List<T>();
        comparer = customComparer;
    }

    public void Enqueue(T item)
    {
        data.Add(item);
        int currentIndex = data.Count - 1;

        while (currentIndex > 0)
        {
            int parentIndex = (currentIndex - 1) / 2;
            if (comparer(data[currentIndex], data[parentIndex]) >= 0)
                break;

            SwapElements(currentIndex, parentIndex);
            currentIndex = parentIndex;
        }
    }

    public T Dequeue()
    {
        if (data.Count == 0)
            throw new InvalidOperationException("Priority queue is empty.");

        int lastIndex = data.Count - 1;
        T frontItem = data[0];
        data[0] = data[lastIndex];
        data.RemoveAt(lastIndex);

        lastIndex--;
        int parentIndex = 0;

        while (true)
        {
            int leftChildIndex = parentIndex * 2 + 1;

            if (leftChildIndex > lastIndex)
                break;

            int rightChildIndex = leftChildIndex + 1;
            if (rightChildIndex <= lastIndex && comparer(data[rightChildIndex], data[leftChildIndex]) < 0)
                leftChildIndex = rightChildIndex;

            if (comparer(data[parentIndex], data[leftChildIndex]) <= 0)
                break;

            SwapElements(parentIndex, leftChildIndex);
            parentIndex = leftChildIndex;
        }

        return frontItem;
    }

    public T Peek()
    {
        if (data.Count == 0)
            throw new InvalidOperationException("Priority queue is empty.");
        return data[0];
    }

    private void SwapElements(int index1, int index2)
    {
        T temp = data[index1];
        data[index1] = data[index2];
        data[index2] = temp;
    }
}