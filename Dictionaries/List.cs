using System;
using System.Collections;
using System.Collections.Generic;

namespace Dictionaries {
    public class LinkedList : IEnumerable<int> {
        public LElem start;
        public LElem end;

        public LinkedList(int Value) {
            start = new LElem(Value);
            end = start;
        }

        public LinkedList() {}

        public IEnumerator<int> GetEnumerator()
        {
            var elem = start;

            while (elem != null)
            {
                yield return elem.value;

                elem = elem.next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class LElem {
        public int value;
        public LElem next;
        public LElem prev;

        public LElem(int Value) {
            value = Value;
        }
    }

    public abstract class DictList : Dictionary, IEnumerable<int> {
        public LinkedList myList;

        public DictList(int Value) {
            myList = new LinkedList(Value);
        }

        public DictList() {
            myList = new LinkedList();
        }

        public IEnumerator<int> GetEnumerator()
        {
            return myList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Search(int elem) {
            if (ReturnSearch(elem) != null) {
                return true;
            }
                
            return false;
        }
        public abstract bool Insert(int elem);
        public bool Delete(int elem) {
            if (myList.start != null) {
                LElem item = ReturnSearch(elem);

                //1. Case: leere Liste
                if (item == null) {
                    return false;
                }

                //2. Case: Item in der Mitte
                if (item != myList.start && item != myList.end) {
                    item.prev.next = item.next;
                    item.next.prev = item.prev;
                    item.prev = null;
                    item.next = null;

                    return true;
                }

                //3. Case: Nur 1 Element
                if (item == myList.start && item == myList.end) {
                    myList.start = null;
                    myList.end = null;

                    return true;
                }

                //4. Case: Item am Anfang
                if (item == myList.start) {
                    myList.start = myList.start.next;
                    myList.start.prev.next = null;
                    myList.start.prev = null;

                    return true;
                }

                //5. Case: Item am Ende
                if (item == myList.end) {
                    myList.end = myList.end.prev;
                    myList.end.next.prev = null;
                    myList.end.next = null;

                    return true;
                }

            }

            return false;
        }
        public void Print() {
            if (myList.start != null) {
                Console.WriteLine("[{0}]", String.Join(" -> ", this));
            }
            else {
                Console.WriteLine("empty");
            }
        }

        public LElem ReturnSearch(int elem) {
            if (myList.start != null) {
                LElem temp = myList.start;

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
            LElem temp = myList.start;

            while (temp != null && temp.value < elem) {
                temp = temp.next;
            }

            return temp;
        }

        public bool InsertHelper(LElem temp, LElem newElem) {

            if (temp != myList.start && temp != null) {
                temp.prev.next = newElem;
                newElem.next = temp;
                newElem.prev = temp.prev;
                temp.prev = newElem;

                return true;
            }

            if (temp == null) {
                myList.end.next = newElem;
                newElem.prev = myList.end;
                myList.end = myList.end.next;

                return true;
            }

            if (temp == myList.start) {
                myList.start.prev = newElem;
                newElem.next = myList.start;
                myList.start = myList.start.prev;

                return true;
            }

            return false;
        }
    }

    public class SetSortedLinkedList : SetUnsortedLinkedList, SortedSet {
        public override bool Insert(int elem) {
            LElem temp = PositionFinder(elem);
            LElem newElem = new LElem(elem);

            if (myList.start != null) {
                if (temp != null) {
                    if (temp.value != newElem.value) {
                        return InsertHelper(temp, newElem);
                    }
                }
                else {
                    return InsertHelper(temp, newElem);
                }
            }
            else {
                myList.start = new LElem(elem);
                myList.end = myList.start;
                return true;
            }

            return false;
        }
    }

    //===============================================================================================================

    public class SetUnsortedLinkedList : DictList, Set {
        public override bool Insert(int elem) {
            if (myList.start != null) {

                if (ReturnSearch(elem) == null) {
                    LElem newElem = new LElem(elem);

                    myList.end.next = newElem;
                    newElem.prev = myList.end;
                    myList.end = newElem;

                    return true;
                }                
            }
            else {
                myList.start = new LElem(elem);
                myList.end = myList.start;
                return true;
            }

            return false;//-----------------
        }
    }

    //===============================================================================================================

    public class MultiSetSortedLinkedList : MultiSetUnsortedLinkedList, SortedMultiSet {
        public override bool Insert(int elem) {
            if (myList.start != null) {
                LElem temp = PositionFinder(elem);
                LElem newElem = new LElem(elem);

                return InsertHelper(temp, newElem);
            }
            else {
                myList.start = new LElem(elem);
                myList.end = myList.start;
                return true;
            }
        }
    }

    //===============================================================================================================

    public class MultiSetUnsortedLinkedList : DictList, MultiSet {
        public override bool Insert(int elem) {

            if (myList.start != null) {
                LElem newElem = new LElem(elem);

                myList.end.next = newElem;
                newElem.prev = myList.end;
                myList.end = newElem;

                return true;
            }
            else {
                myList.start = new LElem(elem);
                myList.end = myList.start;

                return true;
            }            
        }
    }
}
