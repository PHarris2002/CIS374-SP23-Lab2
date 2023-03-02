using System;
using System.Runtime.InteropServices;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            MaxHeap<int> heap1 = new MaxHeap<int>();

            heap1.Add(4);
            heap1.Add(3);
            heap1.Add(2);
            heap1.Add(1);
            heap1.Add(0);
            Console.WriteLine(heap1.Count);

            Console.WriteLine("Before Remove:");
            Console.WriteLine(heap1.Peek());

            Console.WriteLine("After Remove 4:");
            heap1.Remove(4);
            Console.WriteLine(heap1.Peek());

            Console.WriteLine("After Remove 3:");
            heap1.Remove(3);
            Console.WriteLine(heap1.Peek());

            Console.WriteLine("After Remove 2:");
            heap1.Remove(2);
            Console.WriteLine(heap1.Peek());

        }

    }
}

