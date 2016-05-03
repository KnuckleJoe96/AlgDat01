using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgDat01 {
    public abstract class List : Dictionary {
        public LElem start;
        public LElem end;

        public List(int Value) {
            start = new LElem(Value);
            end = start;
        }

        public List() { }

        public class LElem { //eigentilch private??
            public int value;
            public LElem next;
            public LElem prev;

            public LElem(int Value) {
                value = Value;
            }
        }

        public bool Search(int elem) {
            if (ReturnSearch(elem) != null)
                return true;
            return false;
        }
        public abstract bool Insert(int elem);
        public bool Delete(int elem) {
            if (start != null) {
                LElem item = ReturnSearch(elem);

                //1. Case: leere Liste
                if (item == null) return false;

                //2. Case: Item in der Mitte
                if (item != start && item != end) {
                    item.prev.next = item.next;
                    item.next.prev = item.prev;
                    item.prev = null;
                    item.next = null;

                    return true;
                }

                //3. Case: Nur 1 Element
                if (item == start && item == end) {
                    start = null;
                    end = null;

                    return true;
                }

                //4. Case: Item am Anfang
                if (item == start) {
                    start = start.next;
                    start.prev.next = null;
                    start.prev = null;

                    return true;
                }

                //5. Case: Item am Ende
                if (item == end) {
                    end = end.prev;
                    end.next.prev = null;
                    end.next = null;

                    return true;
                }

            }
            return false;
        }
        public void Print() {
            Console.WriteLine("Print: ");

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

            while (temp.value <= elem && temp != null) {
                temp = temp.next;
            }

            return temp;
        }

        public bool InsertHelper(LElem temp, LElem newElem) {
            if (temp != null) {
                if (temp != start && temp != null) {
                    temp.prev.next = newElem;
                    newElem.next = temp;
                    newElem.prev = temp.prev;
                    temp.prev = newElem;
                }

                if (temp == null) {
                    end.next = newElem;
                    newElem.prev = end;
                    end = end.next;
                }

                if (temp == start) {
                    start.prev = newElem;
                    newElem.next = start;
                    start = start.prev;
                }

                return true;
            }
            return false;
        }
    }

    public class SetSortedLinkedList : List, SortedSet {
        public override bool Insert(int elem) {
            if (start != null) {
                LElem temp = PositionFinder(elem);
                LElem newElem = new LElem(elem);

                if(temp.value != newElem.value) {
                    return InsertHelper(temp, newElem);
                }               
            }

            return false;
        }
    }

    //===============================================================================================================

    public class SetUnsortedLinkedList : List, Set {
        public override bool Insert(int elem) {
            if (!Search(elem)) {
                if (start != null) {
                    LElem newElem = new LElem(elem);

                    end.next = newElem;
                    newElem.prev = end;
                    end = newElem;
                }

                return true;
            }

            return false;
        }
    }

    //===============================================================================================================

    public class MultiSetSortedLinkedList : List, SortedMultiSet {
        public override bool Insert(int elem) {
            if (start != null) {
                LElem temp = PositionFinder(elem);
                LElem newElem = new LElem(elem);

                return InsertHelper(temp, newElem);
            }

            return false;
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
            }

            return true;
        }
    }
}
