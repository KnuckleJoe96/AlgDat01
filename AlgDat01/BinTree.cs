using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgDat01 {
  class BinTree : SortedSet {
    Elem root;
    int counter = 0;

    public BinTree(int Root) {
      root = new Elem(Root);
    }

    public BinTree() {}

    public override bool ExecuteInsert(int Value) {
      if (root != null) {
        Elem newElem = new Elem(Value);
        Elem temp = root;
        counter++;
        bool found = false;

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
      else {
        root = new Elem(Value);
        counter++;
        return true;
      }
    }

    public override bool Search(int Value) {
      Elem temp = root;
      int tempcount = 0;
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

    public override bool ExecuteDelete(int Value) {
      if (root != null) {
        if (Search(Value) == true) {
          Elem father = root;
          Elem temp = root;
          if (temp.value == Value) {

            if (father.left == temp) {
              if (checkForSuccessors(temp) == 0) {
                father.left = null;
              }
              if (checkForSuccessors(temp) == 1) {
                if (temp.left != null)
                  father.left = temp.left;
                else
                  father.left = temp.right;
              }
              if (checkForSuccessors(temp) == 2) {
                father.left = findSymmetricalSuccessor(temp);
                father.left.right = temp.right;
                father.left.left = temp.left;
                Delete(father.left.value);
              }
            }
            if (father.right == temp) {
              if (checkForSuccessors(temp) == 0) {
                father.right = null;
              }
              if (checkForSuccessors(temp) == 1) {
                if (temp.right != null)
                  father.right = temp.left;
                else
                  father.right = temp.right;
              }
              if (checkForSuccessors(temp) == 2) {
                father.right = findSymmetricalSuccessor(temp);
                father.right.right = temp.right;
                father.right.left = temp.left;
                Delete(father.right.value);
              }
            }

          }
          bool found = false;

          while (found == false) {
            if (Value == temp.value)
              return true;
            if (Value < temp.value) {
              if (temp.left != null) {
                father = temp;
                temp = temp.left;
              }
              else
                return false;
            }
            else {
              if (temp.right != null) {
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
        else {
          return false;
        }
      }
      else {
        return false;
      }
    }

    public override void Print() {
      if(root != null)
        InOrder(root);
    }

    void InOrder(Elem temp) {
      if (temp.left != null)
        InOrder(temp.left);
      Console.WriteLine(temp.value);
      if (temp.right != null)
        InOrder(temp.right);
    }

    Elem findSymmetricalSuccessor(Elem e) {
      Elem temp = e.left;

      while (temp.right != null) {
        temp = temp.right;
      }
      return temp;
    }

    int checkForSuccessors(Elem e) {
      if (e.left != null && e.right != null)
        return 2;

      if (e.left != null || e.right != null)
        return 1;

      return 0;
    }

    class Elem {
      public int value;
      public Elem right;
      public Elem left;

      public Elem(int Value) {
        value = Value;
      }
    }
  }
}