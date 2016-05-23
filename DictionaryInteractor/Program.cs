using System;
using Dictionaries;

namespace DictionaryInteractor
{
    class Interactor
    {
        static readonly string[] abstracts = {"Set", "SortedSet", "MultiSet", "SortedMultiSet"};
        static readonly string[][] concretes = {
            new[]{"SetUnsortedArray", "SetUnsortedLinkedList", "HashTabSepChain", "HashTabQuadProb"}, 
            new[]{"SetSortedArray", "SetSortedLinkedList", "BinTree", "AVLTree"},
            new[]{"MultiSetUnsortedArray", "MultiSetUnsortedLinkedList"},
            new[]{"MultiSetArray", "MultiSetSortedLinkedList"}
        };

        static readonly string backCode = "back";
        static readonly string prompt = ">>> ";

        delegate bool SelectionHandler(string selection);

        static void getOption(string[] options, string optionPrompt, SelectionHandler handle)
        {
            while (true)
            {
                Console.WriteLine("{0} [{1}]", optionPrompt, String.Join(", ", options));

                Console.Write(prompt);

                string selection = Console.ReadLine();

                Console.WriteLine();

                if (selection == backCode)
                {
                    break;
                }

                if (!handle(selection))
                {
                    Console.WriteLine("option \"{0}\" not found\n", selection);
                }
            }
        }

        static void interact(Dictionary dict)
        {
            while (true)
            {
                Console.WriteLine("search, insert or delete element or print {0}", dict.GetType().Name);

                Console.Write(prompt);

                string selection = Console.ReadLine();

                Console.WriteLine();

                if (selection == null)
                {
                    continue;
                }

                string[] words = selection.Split(new[]{' '}, StringSplitOptions.RemoveEmptyEntries);

                if (words.Length == 0)
                {
                    continue;
                }

                if (words[0] == backCode)
                {
                    return;
                }
                else if (words[0] == "print")
                {
                    dict.Print();
                }
                else if (words.Length == 2)
                {
                    int number;

                    if (!Int32.TryParse(words[1], out number))
                    {
                        Console.WriteLine("\"{0}\" is not a number\n", words[1]);
                        continue;
                    }

                    if (words[0] == "search")
                    {
                        if (dict.Search(number))
                        {
                            Console.WriteLine("found {0}", number);
                        }
                        else
                        {
                            Console.WriteLine("not found {0}", number);
                        }
                    }
                    else if (words[0] == "insert")
                    {
                        if (dict.Insert(number))
                        {
                            Console.WriteLine("inserted {0}", number);
                        }
                        else
                        {
                            Console.WriteLine("did not insert {0}", number);
                        }
                    }
                    else if (words[0] == "delete")
                    {
                        if (dict.Delete(number))
                        {
                            Console.WriteLine("deleted {0}", number);
                        }
                        else
                        {
                            Console.WriteLine("did not delete {0}", number);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("invalid action");
                }

                Console.WriteLine();
            }
        }

        static bool ignoreCaseEquals(string a, string b)
        {
            if (a == null || b == null)
            {
                return false;
            }

            return a.ToLower() == b.ToLower();
        }

        public static void Main()
        {
            getOption(abstracts, "select abstract Dictionary", abstractSelection =>
            {
                for (int i = 0; i < abstracts.Length; i++)
                {
                    if (ignoreCaseEquals(abstractSelection, abstracts[i]))
                    {
                        getOption(concretes[i], "select concrete " + abstracts[i], concreteSelection => 
                        {
                            foreach (string concrete in concretes[i])
                            {
                                if (ignoreCaseEquals(concreteSelection, concrete))
                                {
                                    Type dictType = typeof(Dictionary).Assembly.GetType("Dictionaries." + concrete);

                                    Dictionary dict = Activator.CreateInstance(dictType) as Dictionary;

                                    interact(dict);

                                    return true;
                                }
                            }

                            return false;
                        });

                        return true;
                    }
                }

                return false;
            });
        }
    }
}