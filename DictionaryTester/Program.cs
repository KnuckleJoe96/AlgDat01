using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using AlgDat01;

namespace DictionaryTester
{
    //
    //SAMUEL, hier bitte Skype-Name eintragen: 
    //
    class Tester
    {
        static int testCount = 0;
        static int failedTestCount = 0;

        public static void Main(string[] args)
        {
            /*BasicTest(new SetSortedArray());
            FuzzyTest(new SetSortedArray());
            BasicTest(new SetUnsortedArray());
            FuzzyTest(new SetUnsortedArray());
            BasicTest(new MultiSetArray());
            FuzzyTest(new MultiSetArray());
            BasicTest(new MultiSetUnsortedArray());
            FuzzyTest(new MultiSetUnsortedArray());*/

            //BasicTest(new SetSortedLinkedList());
            //FuzzyTest(new SetSortedLinkedList());
            //BasicTest(new SetUnsortedLinkedList());
            //FuzzyTest(new SetUnsortedLinkedList());
            //BasicTest(new MultiSetSortedLinkedList());
            //FuzzyTest(new MultiSetSortedLinkedList());
            //BasicTest(new MultiSetUnsortedLinkedList());
            //FuzzyTest(new MultiSetUnsortedLinkedList());

            BasicTest(new BinTree());
            //FuzzyTest(new BinTree());
            /*BasicTest(new AVLTree());
            FuzzyTest(new AVLTree());

            BasicTest(new HashTabSepChain());
            FuzzyTest(new HashTabSepChain());
            BasicTest(new HashTabQuadProb());
            FuzzyTest(new HashTabQuadProb());*/

            Console.WriteLine("----------------");
            Console.WriteLine("{0} from {1} tests failed", failedTestCount, testCount);
            Console.WriteLine("----------------");
        }

        public static void PrintTestHeader(Dictionary dict)
        {
            Console.WriteLine("----- Testing {0} -----", dict.GetType().Name);
        }

        public delegate bool DictionaryOperation(int element);

        public static void TestOperation(DictionaryOperation op, int element, bool expectedResult)
        {
            bool result = op(element);

            testCount++;

            if (result != expectedResult)
            {
                failedTestCount++;

                Console.WriteLine("{0}.Insert({1}) returned {2} should be {3}",
                                  op.Target.GetType().FullName,
                                  element,
                                  result,
                                  expectedResult);
            }
        }

        public static void FuzzyTest(Dictionary dict)
        {
            DictionaryOperation[] operations = {dict.Search, dict.Insert, dict.Delete,};

            var rng = new Random();

            for (int i = 0; i < 100000; i++)
            {
                var operation = operations[rng.Next(operations.Length)];
                int element = rng.Next(1000);

                operation(element);
            }
        }

        public static void BasicTest(MultiSet dict)
        {
            PrintTestHeader(dict);

            dict.Print();

            TestOperation(dict.Insert, 5, true);
            TestOperation(dict.Insert, 8, true);
            TestOperation(dict.Insert, 2, true);
            TestOperation(dict.Insert, 9, true);
            TestOperation(dict.Insert, 0, true);
            TestOperation(dict.Insert, 5, true);

            dict.Print();

            TestOperation(dict.Search, 5, true);
            TestOperation(dict.Search, -1, false);
            TestOperation(dict.Search, 10, false);

            dict.Print();

            TestOperation(dict.Delete, 2, true);
            TestOperation(dict.Delete, 5, true);
            TestOperation(dict.Delete, 0, true);
            TestOperation(dict.Delete, 6, false);
            TestOperation(dict.Delete, 8, true);
            TestOperation(dict.Delete, 9, true);
            TestOperation(dict.Delete, 5, true);
            TestOperation(dict.Delete, 5, false);

            dict.Print();

            Console.WriteLine();
        }

        public static void BasicTest(Set dict)
        {
            PrintTestHeader(dict);

            dict.Print();

            TestOperation(dict.Insert, 5, true);
            TestOperation(dict.Insert, 8, true);
            TestOperation(dict.Insert, 2, true);
            TestOperation(dict.Insert, 9, true);
            TestOperation(dict.Insert, 0, true);
            TestOperation(dict.Insert, 5, false);

            dict.Print();

            TestOperation(dict.Search, 5, true);
            TestOperation(dict.Search, -1, false);
            TestOperation(dict.Search, 10, false);

            dict.Print();

            TestOperation(dict.Delete, 2, true);
            TestOperation(dict.Delete, 5, true);
            TestOperation(dict.Delete, 0, true);
            TestOperation(dict.Delete, 6, false);
            TestOperation(dict.Delete, 8, true);
            TestOperation(dict.Delete, 9, true);
            TestOperation(dict.Delete, 5, false);

            dict.Print();

            Console.WriteLine();
        }

        public static void BasicTest(SortedMultiSet dict)
        {
            PrintTestHeader(dict);

            dict.Print();

            TestOperation(dict.Insert, 5, true);
            TestOperation(dict.Insert, 8, true);
            TestOperation(dict.Insert, 2, true);
            TestOperation(dict.Insert, 9, true);
            TestOperation(dict.Insert, 0, true);
            TestOperation(dict.Insert, 5, true);

            dict.Print();

            TestOperation(dict.Search, 5, true);
            TestOperation(dict.Search, -4, false);
            TestOperation(dict.Search, 10, false);
            TestOperation(dict.Search, 4, false);

            dict.Print();

            // TODO check if dict is sorted

            TestOperation(dict.Delete, 2, true);
            TestOperation(dict.Delete, 5, true);
            TestOperation(dict.Delete, 0, true);
            TestOperation(dict.Delete, 6, false);
            TestOperation(dict.Delete, 8, true);
            TestOperation(dict.Delete, 9, true);
            TestOperation(dict.Delete, 5, true);
            TestOperation(dict.Delete, 5, false);

            dict.Print();

            Console.WriteLine();
        }

        public static void BasicTest(SortedSet dict)
        {
            PrintTestHeader(dict);

            dict.Print();

            TestOperation(dict.Insert, 5, true);
            TestOperation(dict.Insert, 8, true);
            TestOperation(dict.Insert, 2, true);
            TestOperation(dict.Insert, 7, true);
            TestOperation(dict.Insert, 3, true);
            TestOperation(dict.Insert, 13, true);
            TestOperation(dict.Insert, 4, true);
            TestOperation(dict.Insert, 14, true);
            TestOperation(dict.Insert, 999, true);
            TestOperation(dict.Insert, -3, true);
            TestOperation(dict.Insert, 9, true);
            TestOperation(dict.Insert, 0, true);
            TestOperation(dict.Insert, 5, false);
            TestOperation(dict.Insert, 0, false);
            

            dict.Print();

            TestOperation(dict.Search, 8, true);
            TestOperation(dict.Search, -4, false);
            TestOperation(dict.Search, 10, false);
            TestOperation(dict.Search, 4, true);

            dict.Print();

            // TODO check if dict is sorted

            TestOperation(dict.Delete, 2, true);
            TestOperation(dict.Delete, 5, true);
            TestOperation(dict.Delete, 0, true);
            TestOperation(dict.Delete, 6, false);
            TestOperation(dict.Delete, 8, true);
            TestOperation(dict.Delete, 9, true);
            TestOperation(dict.Delete, 5, false);

            dict.Print();

            Console.WriteLine();
        }
    }
}