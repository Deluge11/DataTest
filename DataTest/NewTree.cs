using Microsoft.Win32.SafeHandles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Data.Common;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
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
            if (value.Equals(""))
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
            if (printsize)
            {
                Console.WriteLine(space + node.getSize(node.Value));
            }
            else
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
            myNode OldL;
            myNode OldR;
            string Rotation = HighestTwoSubTree(Nod);

            if (Rotation.Equals("LL"))
            {
                NewRoot = Nod.Left;
                OldR = NewRoot.Right;
                Nod.Left = null;
                NewRoot.Right = Nod;
                if (Nod == Root) { Root = NewRoot; }
                if (OldR != null) { AddNode(Root, OldR); }
            }
            else if (Rotation.Equals("LR"))
            {
                NewRoot = Nod.Left.Right;
                OldL = NewRoot.Left;
                OldR = NewRoot.Right;
                Nod.Left.Right = null;
                NewRoot.Left = Nod.Left;
                Nod.Left = null;
                NewRoot.Right = Nod;
                if (Nod == Root) { Root = NewRoot; }
                if (OldL != null) { AddNode(Root, OldL); }
                if (OldR != null) { AddNode(Root, OldR); }
            }
            else if (Rotation.Equals("RR"))
            {
                NewRoot = Nod.Right;
                OldL = NewRoot.Left;
                Nod.Right = null;
                NewRoot.Left = Nod;
                if (Nod == Root) { Root = NewRoot; }
                if (OldL != null) { AddNode(Root, OldL); }
            }
            else if (Rotation.Equals("RL"))
            {
                NewRoot = Nod.Right.Left;
                OldR = NewRoot.Right;
                OldL = NewRoot.Left;
                Nod.Right.Left = null;
                NewRoot.Right = Nod.Right;
                Nod.Right = null;
                NewRoot.Left = Nod;
                if (Nod == Root) { Root = NewRoot; }
                if (OldL != null) { AddNode(Root, OldL); }
                if (OldR != null) { AddNode(Root, OldR); }
            }
            return NewRoot;
        }
        private string HighestTwoSubTree(myNode node)
        {
            string result;

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
            if (string.IsNullOrEmpty(value) || Root == null)
                return null;

            myNode SearchedNode = null;
            Search(Root, value, ref SearchedNode, false);

            if (!searchparent)
            {
                if (SearchedNode.Value == value)
                {
                    return SearchedNode;
                }
            }
            else if (searchparent)
            {
                if (SearchedNode != null)
                {
                    SearchedNode = null;
                    Search(RootPtr, value, ref SearchedNode, true);
                    return SearchedNode;
                }
            }
            return null;
        }
        private void Search(myNode node, string value, ref myNode Searched, bool searchparent = false)
        {
            if (node == null)
                return;

            int vSize = Root.getSize(value);

            if (searchparent)
            {
                if (node.Left != null)
                    if (node.Left.Value == value)
                        Searched = node;
                if (node.Right != null)
                    if (node.Right.Value == value)
                        Searched = node;
            }
            if (!searchparent)
            {
                if (node.Value == value && Searched == null)
                {
                    Searched = node;
                }
            }
            if (vSize <= node.getSize(node.Value))
            {
                Search(node.Left, value, ref Searched, searchparent);
            }
            if (vSize >= node.getSize(node.Value))
            {
                Search(node.Right, value, ref Searched, searchparent);
            }
        }

        public myNode Delete(string value)
        {
            myNode node = Search(value);
            if (node == null) return null;

            int NumOfChildrens = node.childrensNum();

            myNode parent = Search(value, true);
            char side = parent.Left == node ? 'L' : 'R';

            Delete(side, NumOfChildrens,node,parent);
            return node;
        }
        public void Delete(char side, int cNum,myNode node ,myNode parent)
        {
            myNode LowestNodeFromRight = null;
            if (side == 'L')
            {
                if (cNum == 0)
                {
                    parent.Left = null;
                }
                else if (cNum == 1)
                {
                    if (node.Left == null)
                    {
                        parent.Left = node.Right;
                    }
                    else
                    {
                        parent.Left = node.Left;
                    }
                }
                else if (cNum == 2)
                {
                    LowestNodeFromRight = node.getLowestNode();
                    node.Value = Delete(LowestNodeFromRight.Value).Value;
                }
            }
            else
            {
                if (cNum == 0)
                {
                    parent.Right = null;
                }
                else if (cNum == 1)
                {
                    if (node.Right == null)
                    {
                        parent.Right = node.Left;
                    }
                    else
                    {
                        parent.Right = node.Right;
                    }
                }
                else if (cNum == 2)
                {
                    LowestNodeFromRight = node.getLowestNode();
                    node.Value = Delete(LowestNodeFromRight.Value).Value;
                }
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
            int valueSize = 0;

            int i = 0;
            while (!value[i].Equals('@'))
            {
                valueSize += (int)value[i];
                if (value.Length == ++i)
                    break;
            }
            return valueSize;
        }
        public int getHeight(myNode node)
        {
            if (node == null) return -1;
            return 1 + Math.Max(getHeight(node.Left), getHeight(node.Right));
        }
        public int getFactor()
        {
            if (this == null) return -1;
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
        public myNode reNodeWithOutChild()
        {
            Left = null;
            Right = null;
            return this;
        }
        public myNode deepCopy()
        {
            myNode clone = new myNode("null");
            clone.Value = this.Value;
            clone.Left = this.Left;
            clone.Right = this.Right;
            return clone;
        }
        public myNode getLowestNode()
        {
            myNode Subroot = this.Right;

            while (Subroot.Left != null)
            {
                Subroot = Subroot.Left;
            }
            return Subroot;
        }
    }

}
