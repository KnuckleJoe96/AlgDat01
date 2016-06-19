using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class Node {
    public Node father;
    public Node left;
    public Node right;

    public int value;

    public Node(int Value) {
        value = Value;
    }
}

namespace Dictionaries {
    public class BinTree : SortedSet {
        public BinTreeNode root;

        public class BinTreeNode : Node {    
            public BinTreeNode(int Value) : base(Value) {
            }

        }

        public BinTree(int Value) {
            root.value = Value;
        }

        public BinTree() { }

        public virtual bool Delete(int Value) {
            if (root != null) {
                BinTreeNode fatherNode;
                BinTreeNode foundElement = ReturnSearch(Value, out fatherNode);
                bool isChildLeft = false;

                if (foundElement != null) {
                    if (fatherNode != null)
                        isChildLeft = ((fatherNode.left != null) && (fatherNode.left == foundElement)); // kann man sich das null sparen?

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

            return false;
        }

        // Löschen mit 0 KindElementen
        void deleteWith0Children(BinTreeNode foundElement, BinTreeNode fatherNode, bool isChildLeft) {
            if (fatherNode != null) {
                if (isChildLeft) {
                    fatherNode.left.father = null;
                    fatherNode.left = null;
                }
                else {
                    fatherNode.right.father = null;
                    fatherNode.right = null;
                }
            }
            else {
                root = null;
            }
        }

        // Löschen mit 1 KindElement
        void deleteWith1Child(BinTreeNode foundElement, BinTreeNode fatherNode, bool isChildLeft) {
            if (fatherNode != null) {
                if (isChildLeft) {
                    if (foundElement.left != null) { // haben wir selbst noch ein linkes Kind?
                        foundElement.left.father = fatherNode; // bisherigen "Großvater" jetzt zum Vater machen
                        fatherNode.left = foundElement.left; // bei Großvater jetzt den Enkel als Kind machen
                    }
                    else {
                        foundElement.right.father = fatherNode;
                        fatherNode.left = foundElement.right;
                    }
                }
                else {
                    if (foundElement.left != null) {
                        foundElement.left.father = fatherNode;
                        fatherNode.right = foundElement.left;
                    }
                    else {
                        foundElement.right.father = fatherNode;
                        fatherNode.right = foundElement.right;
                    }
                }
            }
            else {
                if (isChildLeft) { // Wenn wir doch die Wurzel sind, warum sollten wir dann das linke Kind sein???
                    root = (BinTreeNode)root.left;
                    root.left.father = null;
                    foundElement.left = null;
                }
                else {
                    root = (BinTreeNode)root.right;
                    root.right.father = null;
                    foundElement.right = null;
                }
            }
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
                int symPredecessor = findSymmetricalPredecessor(foundElement).value; // Die zeilen könnte man auch vor das if/else Konstrukt tun

                Delete(symPredecessor); // ebenso

                root.value = symPredecessor;
            }
        }

        // Einfügen von neuen KindElementen mit Ausgabe ob es Erfolgreich war
        public virtual bool Insert(int Value) {
            if (ReturnInsert(Value) != null) {
                return true;
            }

            return false;
        }

        public BinTreeNode ReturnInsert(int Value) {
            BinTreeNode fatherNode;
            BinTreeNode foundElement = ReturnSearch(Value, out fatherNode);

            if (foundElement == null) { // wert ist noch nicht im baum

                BinTreeNode newNode = new BinTreeNode(Value);

                if (root != null) {
                  
                    if (Value < fatherNode.value) { // wenn kleiner dann links anfügen
                        fatherNode.left = newNode;
                        fatherNode.left.father = fatherNode;
                    }

                    else {
                        fatherNode.right = newNode;
                        fatherNode.right.father = fatherNode;
                    }

                    return newNode;
                }
                else {
                    root = newNode;

                    return newNode;
                }
            }
            else {

                return null;
            }
        }

        // Test zum Suchen ob ein Element vorhanden ist
        public bool Search(int Value) {
            BinTreeNode unimportant; 
            if (ReturnSearch(Value, out unimportant) != null) {
                return true;
            }

            return false;
        }

        // Such-Funktion, welche das gesuchte Element, sowie den Vaterknoten zurückliefert
        public BinTreeNode ReturnSearch(int Value, out BinTreeNode fatherNode) {
            BinTreeNode temp = root;
            fatherNode = temp;
            bool found = false; // found wird nie true :o

            if (temp != null) { // Wenn Wurzel nicht leer ist

                if (root.value == Value) { // gesuchter wert ist die wurzel
                    fatherNode = null;
                    return root;
                }

                while (found == false) {
                    if (Value == temp.value)
                        return temp;

                    if (Value < temp.value) {
                        fatherNode = temp;

                        if (temp.left != null)

                            temp = (BinTreeNode)temp.left;

                        else
                            return null;
                    }
                    else {
                        fatherNode = temp;

                        if (temp.right != null)
                            temp = (BinTreeNode)temp.right;

                        else
                            return null;
                    }
                }
            }
            return null; // Es gibt gar keinen Baum
        }

        // Aufruf der Printfunktion und Rückmeldung falls Baum leeer ist
        public void Print() {
            if (root != null)
                InOrderReversed(root);
            else
                Console.WriteLine("empty");

            Console.WriteLine();
        }

        // Rekursive Funktion zur Ausgabe des Binärbaumes in gedrehter Form
        void InOrderReversed(BinTreeNode temp, int depth = 0) {

            depth++;

			// Wenn es rechten Teilbaum gibt -> gehe in rechten Teilbaum
            if (temp.right != null) {
                InOrderReversed((BinTreeNode)temp.right, depth);
            }

            for (int i = 0; i < depth; i++) { Console.Write("- "); } // für jeden Tiefe ein strichle
            Console.WriteLine(temp.value + " ");

            if (temp.left != null) {
                InOrderReversed((BinTreeNode)temp.left, depth);
            }
        }

        BinTreeNode findSymmetricalPredecessor(BinTreeNode e) { // Die größte Zahl im linken Teilbaum
            BinTreeNode temp = (BinTreeNode)e.left;

            while (temp.right != null) {
                temp = (BinTreeNode)temp.right;
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

        public void rotateRight(int value) {
            BinTreeNode father;
            BinTreeNode node = ReturnSearch(value, out father);
            BinTreeNode fatherFather = (BinTreeNode)father.father;

            //Vaterknoten darf nicht null sein & node muss linkes Kind für RechtsRot sein. 
            if (father != null) {
                if (node == father.left) {

                    if (fatherFather != null) { //Unterscheidung: Vaterknoten = root?                  
                        if (fatherFather.left == father) fatherFather.left = node;
                        else fatherFather.right = node; // ich werde rechts an Großvater gehängt

                        node.father = fatherFather; // bisheriger Großvater wird mein Vater
                    }
                    else { //Father == root
                        root = node; // wenn mein vater keinen vater hat werde ich die neue Wurzel
                    }

                    //Rechtes Kind vorhanden?
                    if (node.right == null) {
                        father.left = null;
                    }
                    else {
                        father.left = node.right; // mein bisheriger rechter Teilbaum wird links an Vater gehängt
                    }

                    father.father = node; // ich werde jetzt vater von meinem bisherigen vater
                    node.right = father; // und mein vater wird mein rechter nachfolger
                }
            }
        }

        public void rotateLeft(int value) {
            BinTreeNode father;
            BinTreeNode node = ReturnSearch(value, out father);
            BinTreeNode fatherFather = (BinTreeNode)father.father;

            //Vaterknoten darf nicht null sein & node muss rechtes Kind für LinksRot sein. 
            if (father != null) {
                if (node == father.right) {

                    if (fatherFather != null) { //Unterscheidung: Vaterknoten = root?                  
                        if (fatherFather.left == father) fatherFather.left = node;
                        else fatherFather.right = node;

                        node.father = fatherFather;
                    }
                    else { //Father == root
                        root = node;
                    }

                    //Linkes Kind vorhanden?
                    if (node.left == null) {
                        father.right = null;
                    }
                    else {
                        father.right = node.left;
                    }

                    father.father = node;
                    node.left = father;

                }
            }
        }
    }    
}