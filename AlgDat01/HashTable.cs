using System;

namespace AlgDat01
{
    public class HashTabSepChain : Set
    {
        int size = 127; // prime number

        MultiSetUnsortedLinkedList[] table;

        public HashTabSepChain()
        {
            table = new MultiSetUnsortedLinkedList[size];

            for (int i = 0; i < table.Length; i++)
            {
                table[i] = new MultiSetUnsortedLinkedList();
            }
        }

        int hash(int key)
        {
            if (key < 0)
            {
                key = -key;
            }

            return key % size;
        }

        public bool Search(int element)
        {
            return table[hash(element)].Search(element);
        }

        public bool Insert(int element)
        {
            return table[hash(element)].Insert(element);
        }

        public bool Delete(int element)
        {
            return table[hash(element)].Delete(element);
        }

        public void Print()
        {
            Console.WriteLine("{0} : ", this.GetType().Name);

            foreach (var list in table)
            {
                Console.Write("    ");
                list.Print();
                Console.WriteLine("    ------------------------");
            }
        }
    }

    public class HashTabQuadProb : Set
    {
        public bool Search(int element)
        {
            throw new NotImplementedException();
        }

        public bool Insert(int element)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int element)
        {
            throw new NotImplementedException();
        }

        public void Print()
        {
            throw new NotImplementedException();
        }
    }
}