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
            Elem newElem = new Elem() { value = Value };
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

        public override bool Delete(int Value) {
            if (Search(Value) == true) {
                Elem father = root;
                Elem temp = root;
                if(temp.value == Value) {
                    
                        
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

        Elem findSymmetricalSuccessor(Elem e) {
            Elem temp = e.left;
            bool finished = false;

            while (!finished) {
                if (temp.right != null) {
                    temp = temp.right;
                }
                else {
                    finished = true;
                }
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
        }
    }
}
