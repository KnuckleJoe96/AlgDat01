﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionaries {
    public enum balanceFactor { MinusMinus = -2, Minus = -1, Null = 0, Plus = 1, PlusPlus = 2 }

    

    public class AVLTree : BinTree {

        public class AVLTreeNode : BinTreeNode {
            public new AVLTreeNode left;
            public new AVLTreeNode right;
            public new AVLTreeNode father;
            public balanceFactor balance;
            public int depthLeft;
            public int depthRight;

            public AVLTreeNode(int Value) : base(Value) {
                depthLeft = 0;
                depthRight = 0;
                balance = 0;
            }


        }

        public AVLTree() { }

        public AVLTree(int Value) {
            root.value = Value;
        }

        public void calculateBalance(AVLTreeNode node) {
            if (node.left != null && node.right != null) {
                node.balance = (balanceFactor)(node.depthLeft - node.depthRight);
                if (node.balance == balanceFactor.MinusMinus || node.balance == balanceFactor.PlusPlus) compensate(node);
            }
            else if (node.left != null) {
                node.balance = (balanceFactor)(node.depthLeft);
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
            AVLTreeNode insertedN = (AVLTreeNode)ReturnInsert(value);

            if (insertedN != null) {
                if (insertedN == root) {
                    //1. Fall: Vaterknoten war kein Blatt --> kein Ausgleich nötig, Balance(Vater) ist jetzt 0
                    if (insertedN.father.right != null && insertedN.father.left != null)
                        calculateBalance(insertedN.father);

                    //2. Fall: Vater war Blatt, links eingefügt --> Tiefe && Balance aktualisieren
                    else {
                        if (insertedN.father.left != null)
                            insertedN.father.depthLeft++;
                        else
                            insertedN.father.depthRight++;

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

            if (node.right != null)
                node.depthRight = recalculateDepth(node.right) + 1;

            if (node.left == null && node.right == null) {
                node.depthLeft = 0;
                node.depthRight = 0;

                return 0;
            }

            calculateBalance(node);
            return node.depthLeft > node.depthRight ? node.depthLeft : node.depthRight;
        }

        public void compensate(AVLTreeNode node) {
            //6 Fälle: ++ & Vater+ | ++ & Vater- | -- & Vater+ | -- & Vater-
            if (node.balance == balanceFactor.MinusMinus) {
                if (node.left.balance == balanceFactor.Plus) {
                    rotateLeft(node.left.right.value);
                    rotateRight(node.left.value);
                }
                else if (node.left.balance == balanceFactor.Minus) {
                    rotateRight(node.left.value);
                }
            }
            else {
                if (node.right.balance == balanceFactor.Plus) {
                    rotateRight(node.right.left.value);
                    rotateLeft(node.right.value);
                }
                else if (node.right.balance == balanceFactor.Minus) {
                    rotateLeft(node.right.value);
                }
            }
        }
    }
}
