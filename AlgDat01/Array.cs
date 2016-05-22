using System;
using System.Linq;

namespace AlgDat01
{
    public abstract class Array : Dictionary
    {
        protected int[] array;

        protected int length = 0;

        public Array()
        {
            array = new int[1];
        }

        protected void increaseLength()
        {
            length++;

            if (length > array.Length)
            {
                int[] newArray = new int[array.Length * 2];

                for (int i = 0; i < array.Length; i++)
                {
                    newArray[i] = array[i];
                }

                array = newArray;
            }
        }

        protected void decreaseLength()
        {
            length--;

            if (length < array.Length / 2)
            {
                int[] newArray = new int[array.Length / 2];

                for (int i = 0; i < length; i++)
                {
                    newArray[i] = array[i];
                }

                array = newArray;
            }
        }

        protected bool searchUnsorted(int element)
        {
            return searchIndexUnsorted(element) >= 0;
        }

        protected bool searchSorted(int element)
        {
            return length != 0 && array[searchIndexSorted(element)] == element;
        }

        protected int searchIndexUnsorted(int element)
        {
            for (int i = 0; i < length; i++)
            {
                if (array[i] == element)
                {
                    return i;
                }
            }

            return -1;
        }

        protected int searchIndexSorted(int element)
        {
            if (length == 0)
            {
                return 0;
            }

            int left = 0;
            int center = 0;
            int right = length - 1;

            do
            {
                center = left + (right - left) / 2;

                if (array[center] < element)
                {
                    left = center + 1;
                }
                else
                {
                    right = center - 1;
                }
            }
            while (array[center] != element && left <= right);

            return center;
        }

        protected void insertUnsorted(int element)
        {
            increaseLength();

            array[length - 1] = element;
        }

        protected void insertSorted(int element, int index)
        {
            increaseLength();

            for (int i = length - 1; i > index; i--)
            {   
                array[i] = array[i - 1];
            }

            if (length > 1 && element > array[index])
            {
                array[(index + 1)] = element;
            }
            else
            {
                array[index] = element;
            }
        }

        protected bool deleteUnsorted(int element)
        {
            int index = searchIndexUnsorted(element);

            if (index >= 0)
            {
                array[index] = array[length - 1];

                decreaseLength();

                return true;
            }
            else
            {
                return false;
            }
        }

        protected bool deleteSorted(int element)
        {
            if (length == 0)
            {
                return false;
            }

            int place = searchIndexSorted(element);

            if (array[place] == element)
            {
                for (int i = place; i < length - 1; i++)
                {
                    array[i] = array[i + 1];
                }

                decreaseLength();

                return true;
            }
            else
            {
                return false;
            }
        }

        public abstract bool Search(int element);

        public abstract bool Insert(int element);

        public abstract bool Delete(int element);

        public void Print()
        {
            Console.WriteLine("[{0}]", String.Join(", ", array.Take(length)));
        }
    }

    public class MultiSetUnsortedArray : Array, MultiSet
    {
        public override bool Search(int element)
        {
            return searchUnsorted(element);
        }

        public override bool Insert(int element)
        {
            insertUnsorted(element);

            return true;
        }

        public override bool Delete(int element)
        {
            return deleteUnsorted(element);
        }
    }

    public class SetUnsortedArray : Array, Set
    {
        public override bool Search(int element)
        {
            return searchUnsorted(element);
        }

        public override bool Insert(int element)
        {
            if (Search(element))
            {
                return false;
            }
            else
            {
                insertUnsorted(element);

                return true;
            }
        }
            
        public override bool Delete(int element)
        {
            return deleteUnsorted(element);
        }
    }
     
    public class MultiSetArray : Array, SortedMultiSet
    {
        public override bool Search(int element)
        {
            return searchSorted(element);
        }

        public override bool Insert(int element)
        {
            int index = searchIndexSorted(element);

            insertSorted(element, index);

            return true;
        }

        public override bool Delete(int element)
        {
            return deleteSorted(element);
        }
    }

    public class SetSortedArray : Array, SortedSet
    {
        public override bool Search(int element)
        {
            return searchSorted(element);
        }

        public override bool Insert(int element)
        {
            int index = searchIndexSorted(element);

            if (length != 0 && array[index] == element)
            {
                return false;
            }
            else
            {
                insertSorted(element, index);

                return true;
            }
        }
            
        public override bool Delete(int element)
        {
            return deleteSorted(element);
        }
    }
}