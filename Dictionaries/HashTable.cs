using System;
using System.Collections.Generic;
using System.Linq;

namespace Dictionaries
{
    public abstract class HashTableBase
    {
        protected int tableSize;
        int sizeIndex = 0;
        protected int elementCount = 0;
        int lowerLimit = 1;
        int upperLimit = 6;

        public HashTableBase()
        {
            tableSize = sizes[sizeIndex];
        }
            
        // list of primes where every prime can be written as p = 4k + 3 and the numbers are roughly powers of 2 
        protected static readonly int[] sizes = {
            7, 11, 19, 43, 67, 131, 263, 523, 1031, 2063, 4099, 8219, 16411, 32771,65539, 131111, 262147, 524347, 1048583,
            2097211, 4194319, 8388619, 16777259, 33554467, 67108879, 134217779, 268435459, 536870923, 1073741827
        };

        protected void adaptSize()
        {
            // prevent adapting the size while rehashing
            if (elementCount <= 0)
            {
                return;
            }

            bool sizeHasChanged = false;

            if (elementCount >= upperLimit)
            {
                sizeIndex++;
                sizeHasChanged = true;
            }
            else if (elementCount <= lowerLimit && sizeIndex > 0)
            {
                sizeIndex--;
                sizeHasChanged = true;
            }

            if (sizeHasChanged)
            {
                tableSize = sizes[sizeIndex];

                // elementCount should be between 1/4 and 3/4
                lowerLimit = tableSize / 4;
                upperLimit = tableSize - lowerLimit;

                int oldElementCount = elementCount;
                elementCount = -elementCount;

                rehash();

                elementCount = oldElementCount;
            }
        }

        protected int hash(int key)
        {
            // hash division
            return Utils.mathMod(key, tableSize);
        }

        // create new hashtable with new size from the old hashtable
        protected abstract void rehash();
    }

    public class HashTabSepChain : HashTableBase, Set
    {
        SetUnsortedLinkedList[] table;

        public HashTabSepChain()
        {
            table = Utils.InitializeArray<SetUnsortedLinkedList>(tableSize);
        }

        protected override void rehash()
        {
            var oldTable = table;

            table = Utils.InitializeArray<SetUnsortedLinkedList>(tableSize);

            foreach (var list in oldTable)
            {
                foreach (int element in list)
                {
                    Insert(element);
                }
            }
        }
            
        public bool Search(int element)
        {
            return table[hash(element)].Search(element);
        }

        public bool Insert(int element)
        {
            if (table[hash(element)].Insert(element))
            {
                elementCount++;

                adaptSize();

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int element)
        {
            if (table[hash(element)].Delete(element))
            {
                elementCount--;

                adaptSize();

                return true;
            }
            else
            {
                return false;
            }
        }
            
        public void Print()
        {
            Console.WriteLine("[ {0} ]", String.Join(" | ", table.Select(list => {var a = String.Join(" -> ", list); return a == "" ? "-" : a;})));
        }
    }

    public class HashTabQuadProb : HashTableBase, Set
    {
        class Cell
        {
            public int element = 0;

            //  0 = free
            // +x = used on x sequences
            // -x = deleted but used on x sequences
            public int refCount = 0;

            public override string ToString()
            {
                return refCount > 0 ? element.ToString() : refCount == 0 ? "-" : "x";
            }
        }
            
        Cell[] table;

        public HashTabQuadProb()
        {
            table = Utils.InitializeArray<Cell>(tableSize);
        }

        // quadratic probing
        IEnumerator<int> getProbingSequence(int start)
        {
            yield return start;

            int quadBase = 1;

            while (true)
            {
                int quad = quadBase * quadBase;

                yield return Utils.mathMod(start + quad, tableSize);
                yield return Utils.mathMod(start - quad, tableSize);

                quadBase++;
            }
        }
            
        protected override void rehash()
        {
            var oldTable = table;

            table = Utils.InitializeArray<Cell>(tableSize);

            foreach (Cell cell in oldTable)
            {
                if (cell.refCount > 0)
                {
                    Insert(cell.element);
                }
            }
        }

        public bool Search(int element)
        {
            int elementHash = hash(element);

            var seq = getProbingSequence(elementHash);

            for (int i = 0; i < tableSize; i++)
            {
                seq.MoveNext();

                var cell = table[seq.Current];

                if (cell.refCount > 0 && cell.element == element)
                {
                    return true;
                }
                else if (cell.refCount == 0)
                {
                    return false;
                }
            }

            return false;
        }

        public bool Insert(int element)
        {
            adaptSize();

            int elementHash = hash(element);

            var seq = getProbingSequence(elementHash);

            for (int i = 0; i < tableSize; i++)
            {
                seq.MoveNext();

                var cell = table[seq.Current];

                if (cell.refCount > 0 && cell.element == element)
                {
                    // rollback refCount increases
                    decreaseRefCounts(elementHash, i - 1);

                    return false;
                }
                else if (cell.refCount > 0)
                {
                    cell.refCount += 1;
                }
                else if (cell.refCount == 0)
                {
                    cell.element = element;
                    cell.refCount = 1;

                    elementCount++;

                    return true;
                }
                else if (cell.refCount < 0)
                {
                    // check if element appears later on the probingSequence
                    for (int j = i; j < tableSize; j++)
                    {
                        seq.MoveNext();

                        var laterCell = table[seq.Current];

                        if (laterCell.refCount > 0 && laterCell.element == element)
                        {
                            // rollback refCount increases
                            decreaseRefCounts(elementHash, i - 1);

                            return false;
                        }
                        else if (laterCell.refCount == 0)
                        {
                            break;
                        }
                    }
                        
                    cell.element = element;
                    cell.refCount = -(cell.refCount - 1);

                    elementCount++;

                    return true;
                }
            }

            System.Diagnostics.Debug.Fail("this code shoud never be reached");
            return false;
        }

        // decreases the refCount of all cells on the probingSequence
        void decreaseRefCounts(int elementHash, int probingSequenceIndex)
        {
            var seq = getProbingSequence(elementHash);

            for (int c = 0; c <= probingSequenceIndex; c++)
            {
                seq.MoveNext();

                Cell cell = table[seq.Current];

                cell.refCount += cell.refCount < 0 ? 1 : -1;
            }
        }

        public bool Delete(int element)
        {
            int elementHash = hash(element);

            var seq = getProbingSequence(elementHash);

            for (int i = 0; i < tableSize; i++)
            {
                seq.MoveNext();

                var cell = table[seq.Current];

                if (cell.refCount > 0 && cell.element == element)
                {
                    cell.refCount *= -1;

                    elementCount--;

                    decreaseRefCounts(elementHash, i);

                    adaptSize();

                    return true;
                }
                else if (cell.refCount == 0)
                {
                    return false;
                }
            }

            return false;
        }

        public void Print()
        {
            Console.WriteLine("[ {0} ]", String.Join<Cell>(" | ", table));
        }
    }
}