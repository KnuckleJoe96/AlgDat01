using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Dictionaries;

namespace DictionaryTester
{
    //
    //SAMUEL, hier bitte Skype-Name eintragen: samuel.mierowski
    //
    class Tester
    {
        static int testCount = 0;
        static int failedTestCount = 0;

        public static void Main(string[] args)
        {
            BasicTest(new SetSortedArray());
            BasicTest(new SetUnsortedArray());
            BasicTest(new MultiSetArray());
            BasicTest(new MultiSetUnsortedArray());

            BasicTest(new SetSortedLinkedList());
            BasicTest(new SetUnsortedLinkedList());
            BasicTest(new MultiSetSortedLinkedList());
            BasicTest(new MultiSetUnsortedLinkedList());

            BasicTest(new BinTree());
            BasicTest(new AVLTree());

            BasicTest(new HashTabSepChain());
            BasicTest(new HashTabQuadProb());

            ParallelTest(new MultiSet[]{new MultiSetUnsortedArray(), new MultiSetUnsortedLinkedList()});

            ParallelTest(new Set[]{new HashTabSepChain(), new HashTabQuadProb(), new SetUnsortedArray(), new SetUnsortedLinkedList()});

            ParallelTest(new SortedMultiSet[]{new MultiSetArray(), new MultiSetSortedLinkedList()});

            ParallelTest(new SortedSet[]{new SetSortedArray(), new SetSortedLinkedList(), new BinTree(), new AVLTree()});

            Console.WriteLine();
            Console.WriteLine("----------------------------");
            Console.WriteLine("{0} from {1} tests failed", failedTestCount, testCount);
            Console.WriteLine("----------------------------");
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

                Console.WriteLine("{0}.{1}({2}) returned {3} should be {4}",
                                  op.Target.GetType().FullName,
                                  op.GetMethodInfo().Name,
                                  element,
                                  result,
                                  expectedResult);
            }
        }

        // tests if all dicts return the same result for random operations
        public static void ParallelTest<T>(T[] dicts) where T : Dictionary
        {
            if (dicts.Length == 0)
            {
                return;
            }

            Console.WriteLine("----- Parallel Testing {0} -----", typeof(T).Name);

            testCount++;

            DictionaryOperation[][] operations = new DictionaryOperation[dicts.Length][];

            for (int i = 0; i < dicts.Length; i++)
            {
                operations[i] = new DictionaryOperation[]{dicts[i].Search, dicts[i].Insert, dicts[i].Delete};
            }

            bool[] results = new bool[dicts.Length];

            var rng = new Random();

            for (int i = 0; i < 30; i++)
            {
                int element = rng.Next(1000);

                int opIndex = rng.Next(operations[0].Length);

                for (int j = 0; j < dicts.Length; j++)
                {
                    var operation = operations[j][opIndex];
                    Console.WriteLine(operation.Method + " - "+ element);
                    results[j] = operation(element);
                }

                if (!results.Aggregate((a, b) => a == b))
                {
                    Console.WriteLine("ParallelTest failed");

                    foreach (var dict in dicts)
                    {
                        Console.WriteLine(dict.GetType().Name);
                        dict.Print();
                        Console.WriteLine();
                    }
                        
                    Console.WriteLine("Results: {0}", String.Join(", ", results));

                    failedTestCount++;

                    break;
                }
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

            //TestOperation(dict.Insert, 5, true);
            //TestOperation(dict.Insert, 8, true);
            //TestOperation(dict.Insert, 2, true);
            //TestOperation(dict.Insert, 7, true);
            //TestOperation(dict.Insert, 3, true);
            //TestOperation(dict.Insert, 13, true);
            //TestOperation(dict.Insert, 4, true);
            //TestOperation(dict.Insert, 14, true);
            //TestOperation(dict.Insert, 999, true);
            //TestOperation(dict.Insert, -3, true);
            //TestOperation(dict.Insert, 9, true);
            //TestOperation(dict.Insert, 0, true);
            //TestOperation(dict.Insert, 5, false);
            //TestOperation(dict.Insert, 0, false);
            TestOperation(dict.Insert, 493, true);
            TestOperation(dict.Insert, 42, true);
            TestOperation(dict.Insert, 711, true);
            TestOperation(dict.Insert, 320, true);
            TestOperation(dict.Insert, 620, true);
            TestOperation(dict.Insert, 775, true);
            TestOperation(dict.Insert, 625, true);
            TestOperation(dict.Insert, 260, true);


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