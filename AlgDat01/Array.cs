using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgDat01 {
    class Array {
        int[] array;

        public Array(int size, int startValue) {
            array = new int[size];
            array[0] = startValue;
        }

        public Array(int size) {
            array = new int[size];
        }

        public Array() {
            array = new int[100];
        }
    }
    //===============================================================================================================

    class SetUnsortedArray : Set {
        public bool Delete(int elem) {
            throw new NotImplementedException();
        }

        public bool Insert(int elem) {
            throw new NotImplementedException();
        }

        public void Print() {
            throw new NotImplementedException();
        }

        public bool Search(int elem) {
            throw new NotImplementedException();
        }
    }

    //===============================================================================================================

    class SetSortedArray : SortedSet {
        public bool Delete(int elem) {
            throw new NotImplementedException();
        }

        public bool Insert(int elem) {
            throw new NotImplementedException();
        }

        public void Print() {
            throw new NotImplementedException();
        }

        public bool Search(int elem) {
            throw new NotImplementedException();
        }
    }

    //===============================================================================================================

    class MultiSetUnsortedArray : MultiSet {
        public bool Delete(int elem) {
            throw new NotImplementedException();
        }

        public bool Insert(int elem) {
            throw new NotImplementedException();
        }

        public void Print() {
            throw new NotImplementedException();
        }

        public bool Search(int elem) {
            throw new NotImplementedException();
        }
    }

    //===============================================================================================================

    class MultiSetArray : MultiSet {
        public bool Delete(int elem) {
            throw new NotImplementedException();
        }

        public bool Insert(int elem) {
            throw new NotImplementedException();
        }

        public void Print() {
            throw new NotImplementedException();
        }

        public bool Search(int elem) {
            throw new NotImplementedException();
        }
    }
}
