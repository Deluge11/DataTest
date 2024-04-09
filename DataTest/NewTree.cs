﻿using Microsoft.Win32.SafeHandles;
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
            if (string.IsNullOrEmpty(value))
                return;

            if (Root == null)
            {
                Root = new myNode(value);
                RootPtr.Left = Root;
                RootPtr.Right = Root;
            }
            else
            {
                AddNode(Root, new myNode(value));
                Balance();
            }
        }
        private void AddNode(myNode node, myNode addNode)
        {
            if (string.Compare(addNode.Value, node.Value) > 0)
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
            else if (string.Compare(addNode.Value, node.Value) <= 0)
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

        public void Print()
        {
            if (Root == null)
            {
                Console.WriteLine("Tree is Empty");
                return;
            }
            Print(Root);
            Console.WriteLine("=====================================");
        }
        private void Print(myNode node, string space = " ")
        {
            Console.WriteLine(space + node.Value);
            if (node.Left != null) Print(node.Left, space + "  ");
            if (node.Right != null) Print(node.Right, space + "  ");
        }

        public void Balance()
        {
            Balance(RootPtr);
        }
        private void Balance(myNode Nod)
        {
            if (Nod.Left != null)
            {
                // Balance Left Subtree Befour The Root
                Balance(Nod.Left); 
                if (Nod.Left.getFactor() == 2 || Nod.Left.getFactor() == -2)
                {
                    Nod.Left = Rotate(Nod.Left);
                }
            }
            if (Nod.Right != null)
            {
                // Balance Right Subtree Befour The Root
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
            string Rotation = HighestTwoSubTree(Nod); // Get Highest Path

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
            myNode SearchedNode = null;
            Search(Root, value, ref SearchedNode, false, false);

            if (string.IsNullOrEmpty(value) || Root == null || SearchedNode == null)
                return null;

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
                    Search(RootPtr, value, ref SearchedNode, true, false);
                    return SearchedNode;
                }
            }
            return null;
        }
        private void Search(myNode node, string value, ref myNode Searched, bool searchparent = false, bool getLowest = false)
        {
            if (node == null || Searched != null) // Found
                return;

            if (getLowest) // Get Lowest Node In Right SubTree Mode
            {
                if (node.Right == null) return;

                Searched = node.Right;

                for (; Searched.Left != null; Searched = Searched.Left)
                {
                    if (searchparent)
                    {
                        if (Searched.Left.Left == null)
                            return;
                    }
                }
            }
            else if (!getLowest) // Default
            {
                if (searchparent) // Search for perant
                {
                    if (node.Left != null)
                        if (node.Left.Value == value)
                            Searched = node;
                    if (node.Right != null)
                        if (node.Right.Value == value)
                            Searched = node;
                }
                if (!searchparent) // Default
                {
                    if (node.Value == value && Searched == null)
                    {
                        Searched = node;
                    }
                }

                if (string.Compare(value, node.Value) >= 0)
                {
                    Search(node.Right, value, ref Searched, searchparent, false);
                }
                if (string.Compare(value, node.Value) <= 0)
                {
                    Search(node.Left, value, ref Searched, searchparent, false);
                }
            }
        }

        public myNode Delete(string value)
        {
            myNode node;
            myNode parent;
            char side;
            int NumOfChildrens;

            node = Search(value);
            if (node == null) return null;

            parent = Search(value, true);
            side = parent.Left == node ? 'L' : 'R';

            NumOfChildrens = node.childrensNum();
            Delete(side, NumOfChildrens, node, parent);
            Balance();
            return node;
        }
        private void Delete(char side, int cNum, myNode node, myNode parent)
        {
            myNode LowestNodeFromRight = null;
            myNode LowestNodeParent = null;

            if (side == 'L')  // Left to Parent
            {
                if (cNum == 0)
                {
                    parent.Left = null;
                }
                else if (cNum == 1)
                {
                    if (node.Left != null)
                    {
                        parent.Left = node.Left;
                    }
                    else
                    {
                        parent.Left = node.Right;
                    }
                }
                else if (cNum == 2) // Replace Deleted Node with LowestNode in Right Subtree
                {
                    Search(node, node.Value, ref LowestNodeFromRight, false, true);
                    Search(node, node.Value, ref LowestNodeParent, true, true);

                    node.Value = LowestNodeFromRight.Value;

                    if (node.Right.Left == null)
                    {
                        myNode OldL = node.Left.deepCopy();
                        parent.Left = node.Right;
                        parent.Left.Left = OldL;
                    }
                    else
                    {
                        if (LowestNodeFromRight.childrensNum() > 0) //Had Right Leaf
                        {
                            LowestNodeParent.Left = LowestNodeFromRight.Right;
                        }
                        else
                        {
                            LowestNodeParent.Left = null;
                        }
                    }
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
                    Search(node, node.Value, ref LowestNodeFromRight, false, true);
                    Search(node, node.Value, ref LowestNodeParent, true, true);

                    node.Value = LowestNodeFromRight.Value;

                    if (node.Right.Left == null)
                    {
                        myNode OldL = node.Left.deepCopy();
                        parent.Right = node.Right;
                        parent.Right.Left = OldL;
                    }
                    else
                    {
                        if (LowestNodeFromRight.childrensNum() > 0)
                        {
                            LowestNodeParent.Left = LowestNodeFromRight.Right;
                        }
                        else
                        {
                            LowestNodeParent.Left = null;
                        }
                    }
                }
            }
            if (parent == RootPtr)
            {
                Root = parent.Left;
            }
        }

        public void Update(string value, string newValue)
        {
            try
            {
                myNode node = Delete(value).deepCopy();
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
