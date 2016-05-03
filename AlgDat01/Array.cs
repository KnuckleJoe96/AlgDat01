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

    //===============================================================================================================

    class SetUnsortedArray : Set {
      public override bool ExecuteDelete(int elem) {
        throw new NotImplementedException();
      }

      public override bool ExecuteInsert(int elem) {
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

    class SetSortedArray : SortedSet {
      public override bool ExecuteDelete(int elem) {
        throw new NotImplementedException();
      }

      public override bool ExecuteInsert(int elem) {
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

    class MultiSetUnsortedArray : MultiSet {
      public override bool ExecuteDelete(int elem) {
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

    class MultiSetArray : MultiSet {
      public override bool ExecuteDelete(int elem) {
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
}
