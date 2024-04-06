using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
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
            if (addNode.ValueSize > node.ValueSize)
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
            else if (addNode.ValueSize <= node.ValueSize)
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
            Console.WriteLine(space + node.ValueSize);
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

        public void Search(string value)
        {
            Search(Root, value);
        }
        public void Search(myNode node, string value)
        {
            int vSize = Root.GetSize(value);

            if (value == node.Value)
            {
                Console.WriteLine("found");
                return;
            }

            if (vSize > node.ValueSize)
            {
                if (node.Right != null)
                {
                    Search(node.Right, value);
                }
            }
            else if (vSize <= node.ValueSize)
            {
                if (node.Left != null)
                {
                    Search(node.Left, value);
                }
                if (node.Right != null)
                {
                    if (vSize == node.Right.ValueSize)
                    {
                        Search(node.Right, value);
                    }
                }
            }
        }

        public void Delete(string value)
        {

        }
    }

    public class myNode
    {
        public int ValueSize { get; private set; }
        public string Value { get; set; }
        public myNode Left { get; set; } = null!;
        public myNode Right { get; set; } = null!;
        public myNode(string value)
        {
            this.SetValue(value);
        }
        public int GetSize(string value)
        {
            int valueSize;

            if (int.TryParse(value, out valueSize))
            {
                valueSize = int.Parse(value);
            }
            else
            {
                int i = 0;
                while (!value[i].Equals('@'))
                {
                    valueSize += (int)value[i];
                    if (value.Length == ++i)
                        break;
                }
            }
            return valueSize;
        }
        public void SetValue(string value)
        {
            this.Value = value;
            this.ValueSize = GetSize(value);
        }
        public int getHeight(myNode node)
        {
            if (node == null) return -1;
            return 1 + Math.Max(getHeight(node.Left), getHeight(node.Right));
        }
        public int getFactor()
        {
            if (this == null) return -1;
            return getHeight(this.Left) - getHeight(this.Right);
        }
    }
}
