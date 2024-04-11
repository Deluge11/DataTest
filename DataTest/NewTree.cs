using Microsoft.Win32.SafeHandles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Data.Common;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DataTest
{
    public class NewTree
    {
        public myNode RootPtr = new myNode("RootPtr");
        public myNode Root { get; set; } = null!;
        public void Add(string value)
        {
            if (string.IsNullOrEmpty(value))
                return;

            if (Root == null)
            {
                Root = new myNode(value);
                RootPtr.Left = Root;
            }
            else
            {
                AddNode(Root, new myNode(value));
                Balance();
            }
        }
        private void AddNode(myNode node, myNode addNode)
        {
            if (addNode.getSize(addNode.Value) > node.getSize(node.Value))
            {
                if (node.Right == null)
                {
                    node.Right = addNode;
                }
                else
                {
                    AddNode(node.Right, addNode);
                }
            }
            else if (addNode.getSize(addNode.Value) <= node.getSize(node.Value))
            {
                if (node.Left == null)
                {
                    node.Left = addNode;
                }
                else
                {
                    AddNode(node.Left, addNode);
                }
            }
        }

        public void Print(bool printsize = false)
        {
            if (Root == null)
            {
                Console.WriteLine("Tree is Empty");
                return;
            }
            Print(Root, printsize);
            Console.WriteLine("=====================================");
        }
        private void Print(myNode node, bool printsize, string space = " ")
        {
            if (printsize) // Print by ValueSize
            {
                Console.WriteLine(space + node.getSize(node.Value));
            }
            else // Print by Value
            {
                Console.WriteLine(space + node.Value);
            }
            if (node.Left != null) Print(node.Left, printsize, space + "  ");
            if (node.Right != null) Print(node.Right, printsize, space + "  ");
        }

        public void Balance()
        {
            Balance(RootPtr);
        }
        private void Balance(myNode Nod)
        {
            if (Nod.Left != null)
            {
                //Check The Balance Of Depth Nodes First
                Balance(Nod.Left);
                if (Nod.Left.getFactor() == 2 || Nod.Left.getFactor() == -2)
                {
                    Nod.Left = Rotate(Nod.Left);
                }
            }
            if (Nod.Right != null)
            {
                Balance(Nod.Right);
                if (Nod.Right.getFactor() == 2 || Nod.Right.getFactor() == -2)
                {
                    Nod.Right = Rotate(Nod.Right);
                }
            }
        }
        private myNode Rotate(myNode Nod)
        {
            myNode NewRoot = Nod;
            myNode OldLeft;
            myNode OldRight;
            string Rotation = HighestTwoSubTree(Nod); // Rotation Type 

            if (Rotation.Equals("LL")) // Left Left Rotation 
            {
                if (Nod.getSize(Nod.Value) == Nod.getSize(Nod.Left.Value))
                    return Nod;

                NewRoot = Nod.Left;
                OldRight = NewRoot.Right;
                Nod.Left = null;
                NewRoot.Right = Nod;
                if (Nod == Root) { Root = NewRoot; }
                if (OldRight != null) { AddNode(NewRoot, OldRight); }
            }
            else if (Rotation.Equals("LR")) // Left Right Rotation 
            {
                if (Nod.getSize(Nod.Value) == Nod.getSize(Nod.Left.Right.Value))
                    return Nod;

                NewRoot = Nod.Left.Right;
                OldLeft = NewRoot.Left;
                OldRight = NewRoot.Right;
                Nod.Left.Right = null;
                NewRoot.Left = Nod.Left;
                Nod.Left = null;
                NewRoot.Right = Nod;
                if (Nod == Root) { Root = NewRoot; }
                if (OldLeft != null) { AddNode(NewRoot, OldLeft); }
                if (OldRight != null) { AddNode(NewRoot, OldRight); }
            }
            else if (Rotation.Equals("RR")) // Right Right Rotation 
            {
                NewRoot = Nod.Right;
                OldLeft = NewRoot.Left;
                Nod.Right = null;
                NewRoot.Left = Nod;
                if (Nod == Root) { Root = NewRoot; }
                if (OldLeft != null) { AddNode(NewRoot, OldLeft); }
            }
            else if (Rotation.Equals("RL")) // Right Left Rotation 
            {
                if (Nod.getSize(Nod.Right.Value) == Nod.getSize(Nod.Right.Left.Value))
                    return Nod;

                NewRoot = Nod.Right.Left;
                OldRight = NewRoot.Right;
                OldLeft = NewRoot.Left;
                Nod.Right.Left = null;
                NewRoot.Right = Nod.Right;
                Nod.Right = null;
                NewRoot.Left = Nod;
                if (Nod == Root) { Root = NewRoot; }
                if (OldLeft != null) { AddNode(NewRoot, OldLeft); }
                if (OldRight != null) { AddNode(NewRoot, OldRight); }
            }
            return NewRoot;
        }
        private string HighestTwoSubTree(myNode node)
        {
            string result; // Rotation Type 

            // Check the two nodes that make the root unbalanced
            if (node.getHeight(node.Left) > node.getHeight(node.Right))
            {
                if (node.getHeight(node.Left.Left) > node.getHeight(node.Left.Right))
                {
                    result = "LL";
                }
                else
                {
                    result = "LR";
                }
            }
            else
            {
                if (node.getHeight(node.Right.Right) > node.getHeight(node.Right.Left))
                {
                    result = "RR";
                }
                else
                {
                    result = "RL";
                }
            }
            return result;
        }

        public myNode Search(string value, bool searchparent = false)
        {
            myNode SearchedNode = null;
            Search(Root, value, ref SearchedNode, false, false);

            if (string.IsNullOrEmpty(value) || Root == null || SearchedNode == null) // Invalid value || Empty Tree || Node not found
                return null;

            if (!searchparent) // Default
            {
                if (SearchedNode.Value == value)
                {
                    return SearchedNode;
                }
            }
            else if (searchparent) // Search for Parent Mode
            {
                SearchedNode = null;
                Search(RootPtr, value, ref SearchedNode, true, false);
                return SearchedNode;
            }
            return null;
        }
        private void Search(myNode node, string value, ref myNode Searched, bool searchparent = false, bool getHighest = false)
        {
            if (node == null || Searched != null) //Result
                return;

            if (getHighest) // Bring the Highest node on the Left SubTree
            {
                if (node.Left == null) return;

                for (Searched = node.Left; Searched.Right != null; Searched = Searched.Right) //Default Case ==> result == Last node
                {
                    if (searchparent)
                    {
                        if (Searched.Right.Right == null) // Parent of The Last Node ==> result = Last node parent
                            return;
                    }
                }
            }
            else if (!getHighest)
            {
                if (searchparent) // Search for perant
                {
                    if (node.Left != null)
                    {
                        if (node.Left.Value.Equals(value))
                        {
                            Searched = node;
                            return;
                        }
                    }
                    if (node.Right != null)
                    {
                        if (node.Right.Value.Equals(value))
                        {
                            Searched = node;
                            return;
                        }
                    }
                    if (node == RootPtr) //Special Case `The Root always to the left of RootPointer`
                    {
                        Search(node.Left, value, ref Searched, searchparent, false);
                    }
                }
                if (!searchparent) // Default Mode
                {
                    if (node.Value.Equals(value) && Searched == null)
                    {
                        Searched = node;
                        return;
                    }
                }
                if (node.getSize(value) > node.getSize(node.Value))
                {
                    Search(node.Right, value, ref Searched, searchparent, false);
                }
                else if (node.getSize(value) <= node.getSize(node.Value))
                {
                    Search(node.Left, value, ref Searched, searchparent, false);
                }
            }
        }

        public myNode Delete(string value)
        {
            myNode node = Search(value); // Check if the node exists
            if (node == null)
            {
                return null;
            }
            myNode parent = null;
            Search(RootPtr, value, ref parent, true, false);

            char side = parent.Left == node ? 'L' : 'R'; // Node side

            Delete(side, node.childrensNum(), node, parent);
            Balance();
            return node;
        }
        private void Delete(char side, int cNum, myNode root, myNode parent)
        {
            if (cNum == 0) // Delete the leaf
            {
                if (side == 'L')
                {
                    parent.Left = null;
                }
                else
                {
                    parent.Right = null;
                }
            }
            else if (cNum == 1) // Replace the node with his leaf
            {
                if (side == 'L')
                {
                    if (root.Right == null)
                    {
                        parent.Left = root.Left;
                    }
                    else
                    {
                        parent.Left = root.Right;
                    }
                }
                else
                {
                    if (root.Right == null)
                    {
                        parent.Right = root.Left;
                    }
                    else
                    {
                        parent.Right = root.Right;
                    }
                }
            }
            else if (cNum == 2)
            {
                myNode HighestNodeFromLeft = null;

                Search(root, root.Value, ref HighestNodeFromLeft, false, true);

                root.Value = HighestNodeFromLeft.Value;


                if (root.Left.Right == null) // If the Left Subtree dont have Right brunch ==> The root will be replaced by the left branch
                {
                    myNode OldRight = root.Right.deepCopy();

                    if (side == 'L')
                    {
                        parent.Left = root.Left;
                        parent.Left.Right = OldRight;
                    }
                    else
                    {
                        parent.Right = root.Left;
                        parent.Right.Right = OldRight;
                    }
                }

                else // If the Left Subtree has two branches ==> The root will be replaced by the Highest Node in Left Subtre
                {
                    myNode HighestNodeParent = null;

                    Search(root, root.Value, ref HighestNodeParent, true, true);

                    if (HighestNodeFromLeft.childrensNum() > 0) // The last node on the Right has a Left branch
                    {
                        HighestNodeParent.Right = HighestNodeFromLeft.Left;
                    }
                    else //The last node has no children
                    {
                        HighestNodeParent.Right = null;
                    }
                }
            }
            if (parent == RootPtr) //Special Case
            {
                Root = parent.Left;
            }
        }

        public void Update(string value, string newValue)
        {
            try
            {
                myNode node = Delete(value).deepCopy(); //Catch the Deleted node => Change it value => Re-add it to the tree
                node.Left = null;
                node.Right = null;

                node.Value = newValue;
                AddNode(RootPtr, node);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Not Found!");
            }
        }
    }

    public class myNode
    {
        public myNode(string value) { Value = value; }
        public string Value { get; set; }
        public myNode Left { get; set; } = null!;
        public myNode Right { get; set; } = null!;

        public int getSize(string value)
        {
            int size = 0;
            for (int i = 0; i < value.Length; i++)
            {
                size += (int)value[i];
            }
            return size;
        }
        public int getHeight(myNode node)
        {
            if (node == null) return -1;
            return 1 + Math.Max(getHeight(node.Left), getHeight(node.Right));
        }
        public int getFactor()
        {
            if (this == null) return 0;
            return getHeight(Left) - getHeight(Right);
        }
        public short childrensNum()
        {
            short num = 0;
            if (Left != null)
                num++;
            if (Right != null)
                num++;
            return num;
        }
        public myNode deepCopy()
        {
            myNode clone = new myNode("null");
            clone.Value = this.Value;
            clone.Left = this.Left;
            clone.Right = this.Right;
            return clone;
        }
    }
}
