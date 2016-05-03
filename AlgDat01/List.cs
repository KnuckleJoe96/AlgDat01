using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgDat01 {
  public class List {
    LElem start;

    public List(int Value) {
      start = new LElem(Value);
    }

    class SetSortedLinkedList : SortedSet {
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

    class SetUnsortedLinkedList : Set {
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

    class MultiSetSortedLinkedList : SortedMultiset {
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

    class MultiSetUnsortedLinkedList : MultiSet {
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

    class LElem {
      public int value;
      public LElem next;
      public LElem prev;

      public LElem(int Value) {
        value = Value;
      }
    }
  }
}