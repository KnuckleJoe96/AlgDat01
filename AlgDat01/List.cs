using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgDat01 {
    public class LinkedList {
        public LElem start;
        public LElem end;

        public LinkedList(int Value) {
            start = new LElem(Value);
            end = start;
        }

        public class LElem {
            public int value;
            public LElem next;
            public LElem prev;

            public LElem(int Value) {
                value = Value;
            }
        }
    }

    public abstract class List : Dictionary {
        public LElem start;
        public LElem end;

        public List(int Value) {
            start = new LElem(Value);
            end = start;
        }

        public List() { }

        public class LElem {
            public int value;
            public LElem next;
            public LElem prev;

            public LElem(int Value) {
                value = Value;
            }
        }

        public bool Search(int elem) {
            if (ReturnSearch(elem) != null) {
                Console.WriteLine("Item found!");
                return true;
            }

            Console.WriteLine("Item not found!");
            return false;
        }
        public abstract bool Insert(int elem);
        public bool Delete(int elem) {
            if (start != null) {
                LElem item = ReturnSearch(elem);

                //1. Case: leere Liste
                if (item == null) {
                    Console.WriteLine("Delete failed.");
                    return false;
                }

                //2. Case: Item in der Mitte
                if (item != start && item != end) {
                    item.prev.next = item.next;
                    item.next.prev = item.prev;
                    item.prev = null;
                    item.next = null;

                    Console.WriteLine("Delete successful.");
                    return true;
                }

                //3. Case: Nur 1 Element
                if (item == start && item == end) {
                    start = null;
                    end = null;

                    Console.WriteLine("Delete successful.");
                    return true;
                }

                //4. Case: Item am Anfang
                if (item == start) {
                    start = start.next;
                    start.prev.next = null;
                    start.prev = null;

                    Console.WriteLine("Delete successful.");
                    return true;
                }

                //5. Case: Item am Ende
                if (item == end) {
                    end = end.prev;
                    end.next.prev = null;
                    end.next = null;

                    Console.WriteLine("Delete successful.");
                    return true;
                }

            }

            Console.WriteLine("Delete failed.");
            return false;
        }
        public void Print() {
            Console.WriteLine("\nPrint: ");

            if (start != null) {
                LElem temp = start;

                while (temp != null) {
                    Console.Write(temp.value + " ");

                    temp = temp.next;
                }
            }
            else {
                Console.WriteLine("Not successful!");
            }
            Console.WriteLine("\n");
        }

        public LElem ReturnSearch(int elem) {
            if (start != null) {
                LElem temp = start;

                while (temp != null) {
                    if (temp.value == elem)
                        return temp;

                    else
                        temp = temp.next;
                }
            }
            return null;
        }

        public LElem PositionFinder(int elem) {
            LElem temp = start;

            while (temp != null && temp.value < elem) {
                temp = temp.next;
            }

            return temp;
        }

        public bool InsertHelper(LElem temp, LElem newElem) {

            if (temp != start && temp != null) {
                temp.prev.next = newElem;
                newElem.next = temp;
                newElem.prev = temp.prev;
                temp.prev = newElem;

                return true;
            }

            if (temp == null) {
                end.next = newElem;
                newElem.prev = end;
                end = end.next;

                return true;
            }

            if (temp == start) {
                start.prev = newElem;
                newElem.next = start;
                start = start.prev;

                return true;
            }

            return false;
        }
    }

    public class SetSortedLinkedList : SetUnsortedLinkedList, SortedSet {
        public override bool Insert(int elem) {
            LElem temp = PositionFinder(elem);
            LElem newElem = new LElem(elem);

            if (start != null) {
                if (temp != null) {
                    if (temp.value != newElem.value) {
                        Console.WriteLine("Insert successful.");
                        return InsertHelper(temp, newElem);
                    }
                }
                else {
                    Console.WriteLine("Insert successful.");
                    return InsertHelper(temp, newElem);
                }
            }
            else {
                start = new LElem(elem);
                end = start;
                Console.WriteLine("Insert successful (new List).");
                return true;
            }

            Console.WriteLine("Insert failed.");
            return false;
        }
    }

    //===============================================================================================================

    public class SetUnsortedLinkedList : List, Set {
        public override bool Insert(int elem) {
            if (start != null) {

                if (ReturnSearch(elem) == null) {
                    LElem newElem = new LElem(elem);

                    end.next = newElem;
                    newElem.prev = end;
                    end = newElem;

                    Console.WriteLine("Insert successful.");
                    return true;
                }                
            }
            else {
                start = new LElem(elem);
                end = start;
                Console.WriteLine("Insert successful (new List).");
                return true;
            }

            Console.WriteLine("Insert failed.");
            return false;//-----------------
        }
    }

    //===============================================================================================================

    public class MultiSetSortedLinkedList : MultiSetUnsortedLinkedList, SortedMultiSet {
        public override bool Insert(int elem) {
            if (start != null) {
                LElem temp = PositionFinder(elem);
                LElem newElem = new LElem(elem);

                Console.WriteLine("Insert successful.");
                return InsertHelper(temp, newElem);
            }
            else {
                start = new LElem(elem);
                end = start;
                Console.WriteLine("Insert successful (new List).");
                return true;
            }
        }
    }

    //===============================================================================================================

    public class MultiSetUnsortedLinkedList : List, MultiSet {
        public override bool Insert(int elem) {

            if (start != null) {
                LElem newElem = new LElem(elem);

                end.next = newElem;
                newElem.prev = end;
                end = newElem;

                Console.WriteLine("Insert successful.");
                return true;
            }
            else {
                start = new LElem(elem);
                end = start;

                Console.WriteLine("Insert successful (new List).");
                return true;
            }            
        }
    }
}
