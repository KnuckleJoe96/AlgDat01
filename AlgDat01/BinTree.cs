using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgDat01 {
  class BinTree : SortedSet {
    Elem root;
    int counter = 1;

    public override bool ExecuteInsert(int Value) {
      counter++;
      Elem newElem = new Elem() { value = Value };
      bool found = false;
      Elem temp = root;
      while (found == false) {
        if (Value < temp.value) {
          if (temp.left != null)
            temp = temp.left;
          else {
            temp.left = newElem;
            found = true;
          }
        }
        else {
          if (temp.right != null)
            temp = temp.right;
          else {
            temp.right = newElem;
            found = true;
          }
        }
      }
      return true;
    }

    public override bool Search(int Value) {
      int tempcount = 0;
      Elem temp = root;
      bool found = false;
      while (found == false || tempcount < counter) {
        if (Value == temp.value)
          return true;
        if (Value < temp.value) {
          if (temp.left != null) {
            tempcount++;
            temp = temp.left;
          }
          else
            return false;
        }
        else {
          if (temp.right != null) {
            tempcount++;
            temp = temp.right;
          }
          else
            return false;
        }
      }
      return false;
    }

    public override bool Delete(int Value) {
      if (Search(Value) == true) {
        int tempcount = 0;
        Elem father = root;
        Elem temp = root;
        bool found = false;
        while (found == false || tempcount < counter) {
          if (Value == temp.value)
            return true;
          if (Value < temp.value) {
            if (temp.left != null) {
              tempcount++;
              father = temp;
              temp = temp.left;
            }
            else
              return false;
          }
          else {
            if (temp.right != null) {
              tempcount++;
              father = temp;
              temp = temp.right;
            }
            else
              return false;
          }
        }
        father = temp.left;
        return false;
      }
    }

    class Elem {
      public int value;
      public Elem right;
      public Elem left;
    }
  }
}
