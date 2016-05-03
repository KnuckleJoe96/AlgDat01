using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgDat01 {
    abstract class Array : Dictionary {
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

        public abstract bool Search(int elem);
        public abstract bool Insert(int elem);
        public abstract bool Delete(int elem);
        public abstract void Print();
    }
    //===============================================================================================================

    class SetUnsortedArray : Array, Set {
        public override bool Delete(int elem) {
            throw new NotImplementedException();
        }

        public override bool Insert(int elem) {
            throw new NotImplementedException();
        }

        public override void Print() {
            throw new NotImplementedException();
        }

        public override bool Search(int elem) {
            throw new NotImplementedException();
        }
    }

    //===============================================================================================================

    class SetSortedArray : Array, SortedSet {
        public override bool Delete(int elem) {
            throw new NotImplementedException();
        }

        public override bool Insert(int elem) {
            throw new NotImplementedException();
        }

        public override void Print() {
            throw new NotImplementedException();
        }

        public override bool Search(int elem) {
            throw new NotImplementedException();
        }
    }

    //===============================================================================================================

    class MultiSetUnsortedArray : Array, MultiSet {
        public override bool Delete(int elem) {
            throw new NotImplementedException();
        }

        public override bool Insert(int elem) {
            throw new NotImplementedException();
        }

        public override void Print() {
            throw new NotImplementedException();
        }

        public override bool Search(int elem) {
            throw new NotImplementedException();
        }
    }

    //===============================================================================================================

    class MultiSetArray : Array, SortedMultiSet {
        public override bool Delete(int elem) {
            throw new NotImplementedException();
        }

        public override bool Insert(int elem) {
            throw new NotImplementedException();
        }

        public override void Print() {
            throw new NotImplementedException();
        }

        public override bool Search(int elem) {
            throw new NotImplementedException();
        }
    }
}
