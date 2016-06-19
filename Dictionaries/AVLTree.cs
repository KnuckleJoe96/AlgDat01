using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionaries {
    public enum balanceFactor { MinusMinus = -2, Minus = -1, Null = 0, Plus = 1, PlusPlus = 2 }

    

    public class AVLTree : BinTree {
        public new AVLTreeNode root;
            
        public class AVLTreeNode /*: Node*/ {
            public AVLTreeNode father;
            public AVLTreeNode left;
            public AVLTreeNode right;
            public balanceFactor balance;
            public int depthLeft;
            public int depthRight;

            public int value;

            public AVLTreeNode(int Value) /*: base(Value)*/ {
                value = Value;
                depthLeft = 0;
                depthRight = 0;
                balance = 0;
            }
        }

        public AVLTree() {}

        public AVLTree(int Value) {
            root = new AVLTreeNode(Value);
        }

        public void calculateBalance(AVLTreeNode node) {
            if (node.left != null && node.right != null) {
                node.balance = (balanceFactor)(node.depthRight - node.depthLeft);
                if (node.balance == balanceFactor.MinusMinus || node.balance == balanceFactor.PlusPlus) compensate(node);
            }
            else if (node.left != null) {
                node.balance = (balanceFactor)(-node.depthLeft);
                if (node.balance == balanceFactor.MinusMinus) compensate(node);
            }
            else if (node.right != null) {
                node.balance = (balanceFactor)(node.depthRight);
                if (node.balance == balanceFactor.PlusPlus) compensate(node);
            }
            else
                node.balance = 0;
        }

        public override bool Insert(int value) {
            AVLTreeNode insertedN = ReturnInsert(value);

            if (insertedN != null) {
                if (insertedN.value != ((AVLTreeNode)root).value) {
                    //1. Fall: Vaterknoten war kein Blatt --> kein Ausgleich nötig, Balance(Vater) ist jetzt 0
                    if (insertedN.father.right != null && insertedN.father.left != null)
                        calculateBalance((AVLTreeNode)insertedN.father);

                    //2. Fall: Vater war Blatt, links eingefügt --> Tiefe && Balance aktualisieren
                    else {
                        if (insertedN.father.left != null)
                            ((AVLTreeNode)insertedN.father).depthLeft++;
                        else
                            ((AVLTreeNode)insertedN.father).depthRight++;

                        recalculateDepth((AVLTreeNode)root);
                    }
                    return true;
                }
                return true;
            }
            return false;
        }

        public int recalculateDepth(AVLTreeNode node) {

            if (node.left != null)
                node.depthLeft = recalculateDepth(node.left) + 1;
            else node.depthLeft = 0;

            if (node.right != null)
                node.depthRight = recalculateDepth(node.right) + 1;
            else node.depthRight = 0;

            if (node.left == null && node.right == null) {
                node.depthLeft = 0;
                node.depthRight = 0;

                return 0;
            }

            calculateBalance(node);
            return node.depthLeft > node.depthRight ? node.depthLeft : node.depthRight;
        }

        public void compensate(AVLTreeNode node) {
            //4 Fälle: ++ & Kind+ -> L-Rot | ++ & Kind- -> R-L-Rot | -- & Kind+ -> L-R-Rot | -- & Kind- R-Rot
            if (node.balance == balanceFactor.MinusMinus) {
                if ((node.left).balance == balanceFactor.Plus) { 
                    rotateLeft(node.left.right.value);
                    rotateRight(node.left.value);
                }
                else if ((node.left).balance == balanceFactor.Minus) {
                    rotateRight(node.left.value);
                }
            }
            else {
                if ((node.right).balance == balanceFactor.Plus) {
                    rotateLeft(node.right.value);                    
                }
                else if ((node.right).balance == balanceFactor.Minus) {
                    rotateRight(node.right.left.value);
                    rotateLeft(node.right.value);
                }
            }

            recalculateDepth(root);
        }

        public AVLTreeNode ReturnSearch(int Value, out AVLTreeNode fatherNode) {
            AVLTreeNode temp = root;
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

                            temp = (AVLTreeNode)temp.left;

                        else
                            return null;
                    }
                    else {
                        fatherNode = temp;

                        if (temp.right != null)
                            temp = (AVLTreeNode)temp.right;

                        else
                            return null;
                    }
                }
            }
            return null;
        }

        public new AVLTreeNode ReturnInsert(int Value) {
            AVLTreeNode fatherNode;
            AVLTreeNode foundElement = ReturnSearch(Value, out fatherNode);

            if (foundElement == null) {

                AVLTreeNode newNode = new AVLTreeNode(Value);

                if (root != null) {

                    if (Value < fatherNode.value) {
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


        public void rotateRight(int value) {
            AVLTreeNode father;
            AVLTreeNode node = ReturnSearch(value, out father);
            AVLTreeNode fatherFather = father.father;

            //Vaterknoten darf nicht null sein & node muss linkes Kind für RechtsRot sein. 
            if (father != null) {
                if (node == father.left) {

                    if (fatherFather != null) { //Unterscheidung: Vaterknoten = root?                  
                        if (fatherFather.left == father) fatherFather.left = node;
                        else fatherFather.right = node;

                        node.father = fatherFather;
                    }
                    else { //Father == root
                        root = node;
                    }

                    //Rechtes Kind vorhanden?
                    if (node.right == null) {
                        father.left = null;
                    }
                    else {
                        father.left = node.right;
                    }

                    father.father = node;
                    node.right = father;
                }
            }
        }

        public void rotateLeft(int value) {
            AVLTreeNode father;
            AVLTreeNode node = ReturnSearch(value, out father);
            AVLTreeNode fatherFather = father.father;

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

        // Aufruf der Printfunktion und Rückmeldung falls Baum leeer ist
        public new void Print() {
            if (root != null)
                InOrderReversed(root);
            else
                Console.WriteLine("empty");

            Console.WriteLine();
        }

        // Rekursive Funktion zur Ausgabe des Binärbaumes in gedrehter Form
        void InOrderReversed(AVLTreeNode temp, int depth = 0) {

            depth++;

            if (temp.right != null) {
                InOrderReversed(temp.right, depth);
            }

            for (int i = 0; i < depth; i++) { Console.Write("- "); }
            Console.WriteLine(temp.value + " ");

            if (temp.left != null) {
                InOrderReversed(temp.left, depth);
            }
        }
    }
}
