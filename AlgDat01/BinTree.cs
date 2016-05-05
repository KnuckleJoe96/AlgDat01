using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgDat01 {
  public class BinTreeNode {
    public int value;
    public BinTreeNode left;
    public BinTreeNode right;

    public BinTreeNode(int Value) {
      value = Value;
    }
  }

  enum balanceFactor { MinusMinus = -2, Minus = -1, Null = 0, Plus = 1, PlusPlus = 2 }

  public class AVLTreeNode : BinTreeNode {
    balanceFactor balance;
    int depth;

    public AVLTreeNode(int Value) : base(Value) { }

    public void calculateBalance() {
      balance = (balanceFactor)(((AVLTreeNode)left).depth - ((AVLTreeNode)right).depth);
    }
  }

  //
  //------------------------------------------------------------------------------------------------
  //

  public class BinTree : SortedSet {
    BinTreeNode root;

    public BinTree(int Value) {
      root.value = Value;
    }

    public BinTree() { }

    public bool Delete(int Value) {
      if (root != null) {
        BinTreeNode fatherNode;
        BinTreeNode foundElement = ReturnSearch(Value, out fatherNode);
        bool isChildLeft = false;

        if (foundElement != null) {
          if (fatherNode != null)
            isChildLeft = ((fatherNode.left != null) && (fatherNode.left == foundElement));

          int amountChildren = checkForChildren(foundElement);

          //Gefundenes Element hat keine KindElemente
          if (amountChildren == 0) {
            deleteWith0Children(foundElement, fatherNode, isChildLeft);

            return true;
          }

          //Gefundenes Element hat 1 KindElement
          if (amountChildren == 1) {
            deleteWith1Child(foundElement, fatherNode, isChildLeft);

            return true;
          }

          //Gefundenes Element hat 2 KinderElement
          if (amountChildren == 2) {
            deleteWith2Children(foundElement, fatherNode, isChildLeft);

            return true;
          }
        }
      }
      Console.WriteLine("Delete failed!");

      return false;
    }

    // Löschen mit 0 KindElementen
    void deleteWith0Children(BinTreeNode foundElement, BinTreeNode fatherNode, bool isChildLeft) {
      if (fatherNode != null) {
        if (isChildLeft)
          fatherNode.left = null;
        else
          fatherNode.right = null;
      }
      else {
        root = null;
      }
      Console.WriteLine("Delete successful!");
    }

    // Löschen mit 1 KindElement
    void deleteWith1Child(BinTreeNode foundElement, BinTreeNode fatherNode, bool isChildLeft) {
      if (fatherNode != null) {
        if (isChildLeft) {
          if (foundElement.left != null)
            fatherNode.left = foundElement.left;
          else
            fatherNode.left = foundElement.right;
        }
        else {
          if (foundElement.left != null)
            fatherNode.right = foundElement.left;
          else
            fatherNode.right = foundElement.right;
        }
      }
      else {
        if (isChildLeft) {
          root = root.left;
          foundElement.left = null;
        }
        else {
          root = root.right;
          foundElement.right = null;
        }
      }
      Console.WriteLine("Delete successful!");
    }

    // Löschen mit 2 KindElementen
    void deleteWith2Children(BinTreeNode foundElement, BinTreeNode fatherNode, bool isChildLeft) {
      if (fatherNode != null) {
        int symPredecessor = findSymmetricalPredecessor(foundElement).value;

        Delete(symPredecessor);

        if (isChildLeft)
          fatherNode.left.value = symPredecessor;


        else
          fatherNode.right.value = symPredecessor;
      }
      else {
        int symPredecessor = findSymmetricalPredecessor(foundElement).value;

        Delete(symPredecessor);

        root.value = symPredecessor;
      }
      Console.WriteLine("Delete successful!");
    }

    // Einfügen von neuen KindElementen mit Ausgabe ob es Erfolgreich war
    public bool Insert(int Value) {
      BinTreeNode fatherNode;
      BinTreeNode foundElement = ReturnSearch(Value, out fatherNode);

      if (foundElement == null) {

        if (root != null) {

          if (Value < fatherNode.value)
            fatherNode.left = new BinTreeNode(Value);

          else
            fatherNode.right = new BinTreeNode(Value);

          Console.WriteLine("Insert successful!");
          return true;
        }
        else {
          root = new BinTreeNode(Value);

          Console.WriteLine("Insert successful!");
          return true;
        }
      }
      else {
        Console.WriteLine("Insert failed!");

        return false;
      }
    }


    // Test zum Suchen ob ein Element vorhanden ist
    public bool Search(int Value) {
      BinTreeNode notImportant; //Not good looking
      if (ReturnSearch(Value, out notImportant) != null) {
        Console.WriteLine("Item found.");
        return true;
      }

      Console.WriteLine("Item not found.");
      return false;
    }

    // Such-Funktion, welche das gesuchte Element, sowie den Vaterknoten zurückliefert
    public BinTreeNode ReturnSearch(int Value, out BinTreeNode fatherNode) {
      BinTreeNode temp = root;
      fatherNode = temp;
      bool found = false;

      if (temp != null) {

        if (root.value == Value) {
          fatherNode = null;
          return root;
        }

        while (found == false) {
          if (Value == temp.value)
            return temp;

          if (Value < temp.value) {
            fatherNode = temp;

            if (temp.left != null)

              temp = temp.left;

            else
              return null;
          }
          else {
            fatherNode = temp;

            if (temp.right != null)
              temp = temp.right;

            else
              return null;
          }
        }
      }
      return null;
    }


    // Aufruf der Printfunktion und Rückmeldung falls Baum leeer ist
    public void Print() {
      Console.WriteLine("\nPrint: ");
      int depth = 0;
      if (root != null)
        InOrderReversed(root, ref depth);
      else
        Console.WriteLine("Print failed.");
      Console.WriteLine();
    }


    // Rekursive Funktion zur Ausgabe des Binärbaumes in gedrehter Form
    void InOrderReversed(BinTreeNode temp, ref int depth) {
      if (temp.right != null) {
        depth++;
        InOrderReversed(temp.right, ref depth);
      }

      for (int i = 0; i < depth; i++) { Console.Write("- "); }
      Console.WriteLine(temp.value + " ");

      if (temp.left != null) {
        depth++;
        InOrderReversed(temp.left, ref depth);
      }

      depth--;
    }

    BinTreeNode findSymmetricalPredecessor(BinTreeNode e) {
      BinTreeNode temp = e.left;

      while (temp.right != null) {
        temp = temp.right;
      }
      return temp;
    }

    int checkForChildren(BinTreeNode e) {
      if (e.left != null && e.right != null)
        return 2;

      if (e.left != null || e.right != null)
        return 1;

      return 0;
    }
  }
}