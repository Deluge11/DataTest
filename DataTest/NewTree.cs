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

        public void PrintTree()
        {
            if (Root == null)
            {
                Console.WriteLine("Tree is Empty");
                return;
            }
            Print(Root);
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
            myNode tmp;
            myNode OldL;
            myNode OldR;
            string Rotation = HighestTwoSubTree(Nod);

            if (Rotation.Equals("LL"))
            {
                NewRoot = Nod.Left;
                tmp = NewRoot.Right;
                NewRoot.Right = Nod;
                NewRoot.Right.Left = tmp;
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
                if (OldL != null) { AddNode(Root, OldL); }
                if (OldR != null) { AddNode(Root, OldR); }
            }
            else if (Rotation.Equals("RR"))
            {
                NewRoot = Nod.Right;
                tmp = NewRoot.Left;
                NewRoot.Left = Nod;
                NewRoot.Left.Right = tmp;
            }
            else if (Rotation.Equals("RL"))
            {
                NewRoot = Nod.Right.Left;
                OldL = NewRoot.Left;
                OldR = NewRoot.Right;
                Nod.Right.Left = null;
                NewRoot.Right = Nod.Right;
                Nod.Right = null;
                NewRoot.Left = Nod;
                if (OldL != null) { AddNode(Root, OldL); }
                if (OldR != null) { AddNode(Root, OldR); }
            }

            if (Nod == Root)
            {
                Root = NewRoot;
                return Root;
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
