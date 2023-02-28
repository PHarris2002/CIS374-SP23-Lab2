using System;
using System.ComponentModel;
using System.Linq;

namespace Lab2
{
    public class MinHeap<T> where T : IComparable<T>
    {
        private T[] array;
        private const int initialSize = 8;

        public int Count { get; private set; }

        public int Capacity => array.Length;

        public bool IsEmpty => Count == 0;


        public MinHeap(T[] initialArray = null)
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
        /// Returns the min item but does NOT remove it.
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
            return ExtractMin();
        }

        /// <summary>
        /// Removes and returns the max item in the min-heap.
        /// Time complexity: O( N ).
        /// </summary>
        public T ExtractMax()
        {
            if (IsEmpty)
            {
                throw new Exception("Empty Heap");
            }

            T maxItem = array[0];

            foreach (var item in array)
            {
                if (item.CompareTo(maxItem) == 1)
                {
                    maxItem = item;
                }
            }

            return maxItem;
        }

        /// <summary>
        /// Removes and returns the min item in the min-heap.
        /// Time ctexity: O( log(n) ).
        /// </summary>
        public T ExtractMin()
        {
            if (IsEmpty)
            {
                throw new Exception("Empty Heap");
            }

            T min = array[0];

            // swap root (first) and last element
            Swap(0, Count - 1);

            // "remove" last
            Count--;

            // trickle down from root (first)
            TrickleDown(0);

            return min;
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
                if (array[index].CompareTo(array[parentIndex]) == 1)
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
            //Assign variables for later usage
            int minChildIndex = index;
            T tmp = array[0];

            // Since trickling down requires looking at both the node's children, we call these two methods.
            int leftChildIndex = LeftChild(index);
            int rightChildIndex = RightChild(index);

            // Checks if right child's index is outside of the array
            if (rightChildIndex >= Count)
            {
                // Checks if the left child's index is also out of the array
                if (leftChildIndex >= Count)
                {
                    return;
                }

                // If there is a left child, then make that child the min child index
                else
                {
                    minChildIndex = leftChildIndex;
                }
            }

            // If both nodes are present, compare the left child with the right child
            else
            {
                // If the left child is greater than the right child, make the left child the min child index
                if (array[leftChildIndex].CompareTo(array[rightChildIndex]) == -1)
                {
                    minChildIndex = leftChildIndex;
                }

                // Otherwise, the right child is the min child index
                else
                {
                    minChildIndex = rightChildIndex;
                }
            }
            // Compare the current index with the min child index
            if (array[index].CompareTo(array[minChildIndex]) == 1)
            {
                // Call swap if current node is less than the child
                Swap(index, minChildIndex);

                // Use recursion
                TrickleDown(minChildIndex);

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


