using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgDat01 {
    interface Dictionary {
        bool Search(int elem); //true = gefunden
        bool Insert(int elem); //true = hinzugefügt
        bool Delete(int elem); //true = gelöscht
        void Print(); //Ausgabe der Elemente
    }

    abstract class Set : Dictionary {
        public abstract bool Search(int elem);
        public bool Insert(int elem) {
            if (!Search(elem)) {
                return executeInsert(elem);
            }
            else {
                return false;
            }
        }
        public bool Delete(int elem) {
            if (Search(elem)) {
                return executeDelete(elem);
            }
            else {
                return false;
            }
        }
        public abstract void Print();

        public abstract bool executeInsert(int elem);
        public abstract bool executeDelete(int elem);
    }

    abstract class SortedSet : Set {

    }

    abstract class MultiSet : Dictionary {
        public abstract bool Search(int elem);
        public bool Insert(int elem) {
            if (!Search(elem)) {
                return executeInsert(elem);
            }
            else {
                return false;
            }
        }
        public bool Delete(int elem) {
            if (Search(elem)) {
                return executeDelete(elem);
            }
            else {
                return false;
            }
        }
        public abstract void Print();

        public abstract bool executeInsert(int elem);
        public abstract bool executeDelete(int elem);
    }

    abstract class SortedMultiset : MultiSet {

    }
}
