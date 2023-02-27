using System;
using System.Reflection;

namespace Lab2
{
	public class MaxHeap<T> where T: IComparable<T>
    {
        private T[] array;
        private const int initialSize = 8;

        public int Count { get; private set; }

        public int Capacity => array.Length;

        public bool IsEmpty => Count == 0;


        public MaxHeap(T[] initialArray = null)
        {
            array = new T[initialSize];

            if (initialArray == null)
            {
                return;
            }

            foreach (var item in initialArray)
            {
                Add(item);
            }

        }

        /// <summary>
        /// Returns the max item but does NOT remove it.
        /// Time complexity: O(1).
        /// </summary>
        public T Peek()
        {
            if (IsEmpty)
            {
                throw new Exception("Empty Heap");
            }

            return array[0];
        }

        /// <summary>
        /// Adds given item to the heap.
        /// Time complexity: O(log(n)).
        /// </summary>
        public void Add(T item)
        {
            int nextEmptyIndex = Count;

            array[nextEmptyIndex] = item;

            TrickleUp(nextEmptyIndex);

            Count++;

            // resize if full
            if (Count == Capacity)
            {
                DoubleArrayCapacity();
            }

        }

        public T Extract()
        {
            return ExtractMax();
        }

        /// <summary>
        /// Removes and returns the min item in the max-heap.
        /// Time complexity: O( 1 ).
        /// </summary>
        public T ExtractMin()
        {
            T minItem = array[0];

            foreach (var item in array)
            {
                if (item.CompareTo(minItem) == -1)
                {
                    minItem = item;
                }
            }

            return minItem;
        }

        /// <summary>
        /// Removes and returns the min item in the max-heap.
        /// Time ctexity: O( log(n) ).
        /// </summary>
        public T ExtractMax()
        {
            if (IsEmpty)
            {
                throw new Exception("Empty Heap");
            }

            T max = array[0];

            // swap root (first) and last element
            Swap(0, Count - 1);

            // "remove" last
            Count--;

            // trickle down from root (first)
            TrickleDown(0);

            return max;
        }

        /// <summary>
        /// Returns true if the heap contains the given value; otherwise false.
        /// Time complexity: O( N ).
        /// </summary>
        public bool Contains(T value)
        {
            // linear search

            foreach (var item in array)
            {
                if (item.CompareTo(value) == 0)
                {
                    return true;
                }
            }

            return false;

        }

        // Time Complexity: O( log(n) )
        private void TrickleUp(int index)
        {
            while (index > 0)
            {
                int parentIndex = Parent(index);
                if (array[index].CompareTo(array[parentIndex]) == -1)
                {
                    return;
                }

                else
                {
                    Swap(index, parentIndex);
                    index = parentIndex;
                }
            }

        }
        
        // Time Complexity: O( log(n) )
        private void TrickleDown(int index)
        {
            int leftChildIndex = LeftChild(index);
            int rightChildIndex = RightChild(index);

            // If both children are greater than the current node's index, then return.
            if (array[index].CompareTo(array[leftChildIndex]) == 1 && array[index].CompareTo(array[rightChildIndex]) == 1)
            {
                return;
            }

            // If left child is greater than current node's index, swap the two nodes
            else if (array[index].CompareTo(array[leftChildIndex]) == -1)
            {
                Swap(index, leftChildIndex);
                TrickleDown(index);
            }

            else

            {
                Swap(index, rightChildIndex);
                TrickleDown(index);
            }
        }

        /// <summary>
        /// Gives the position of a node's parent, the node's position in the heap.
        /// </summary>
        private static int Parent(int position)
        {
            int parentIndex = (position - 1) / 2;
            return parentIndex;
        }

        /// <summary>
        /// Returns the position of a node's left child, given the node's position.
        /// </summary>
        private static int LeftChild(int position)
        {
            int leftChildIndex = 2 * position + 1;
            return leftChildIndex;
        }

        /// <summary>
        /// Returns the position of a node's right child, given the node's position.
        /// </summary>
        private static int RightChild(int position)
        {
            int rightChildIndex = 2 * position + 2;
            return rightChildIndex;
        }

        private void Swap(int index1, int index2)
        {
            var temp = array[index1];

            array[index1] = array[index2];
            array[index2] = temp;
        }

        private void DoubleArrayCapacity()
        {
            Array.Resize(ref array, array.Length * 2);
        }


    }
}

