//using System;
//using System.Collections.Generic;

//namespace DataTest
//{

//    public class BinaryTree
//    {
//        public int TreeLevel { get; private set; } = -1;
//        public int NodesNum { get; private set; }
//        public Node Root { get; private set; } = null!;

//        public List<Node[]> Levels = new List<Node[]>();
//        public void Add(string value)
//        {
//            if (value.Equals(""))
//                return;

//            if (TreeLevel == -1)
//            {
//                TreeLevel++;
//                NodesNum++;
//                Root = new Node(value, TreeLevel, 0);
//                Levels.Add(new Node[] { Root });
//            }
//            else
//            {
//                AddNode(value);
//            }
//        }
//        private void AddNode(string value)
//        {
//            int lastArray = Levels.Count - 1;
//            int lastIndexValue = Levels[lastArray].Length - 1;

//            if (Levels[lastArray][lastIndexValue] == null) // Check if Tree Full or Not
//            {
//                for (int i = 0; i <= lastIndexValue + 1; i++)
//                {
//                    if (Levels[lastArray][i] == null)
//                    {
//                        Levels[lastArray][i] = new Node(value, TreeLevel, i);
//                        if (i % 2 == 0)
//                        {
//                            Levels[TreeLevel - 1][i / 2].Left = Levels[lastArray][i];
//                        }
//                        else
//                        {
//                            Levels[TreeLevel - 1][i / 2].Right = Levels[lastArray][i];
//                        }
//                        Sort(value, i);
//                        NodesNum++;
//                        return;
//                    }
//                }
//            }
//            else
//            {
//                TreeLevel++;

//                int newLevelLength = (Int16)Math.Pow(2, TreeLevel);
//                Node[] newLevel = new Node[newLevelLength];
//                Levels.Add(newLevel);

//                AddNode(value);
//                return;
//            }
//        }
//        private void Sort(string value, int nodeIndex)
//        {
//            int valueSize = Root.GetSize(value);
//            int checkLevel = TreeLevel;
//            bool flipped = false;

//            for (int i = nodeIndex; i >= 0; i--)
//            {
//                if (flipped)
//                {
//                    i++;
//                    flipped = false;
//                }

//                if (i > 0)
//                {
//                    Node brotherNode = Levels[checkLevel][i - 1];

//                    if (valueSize < brotherNode.ValueSize) // <> ASC DISC
//                    {
//                        Levels[checkLevel][i].SetValue(brotherNode.Value);
//                        Levels[checkLevel][i - 1].SetValue(value);
//                        continue;
//                    }
//                    break;
//                }
//                else if (i == 0)
//                {
//                    int upperLastIndex = Levels[checkLevel - 1].Length - 1;
//                    Node upperNode = Levels[checkLevel - 1][upperLastIndex];

//                    if (valueSize < upperNode.ValueSize) // <> ASC DISC
//                    {
//                        Levels[checkLevel][0].SetValue(upperNode.Value);
//                        upperNode.SetValue(value);

//                        i = Levels[--checkLevel].Length - 1;
//                        flipped = true;
//                        continue;
//                    }
//                    break;
//                }
//            }
//        }
//        public void PrintTree()
//        {
//            if (TreeLevel == -1)
//            {
//                Console.WriteLine("Tree is Empty");
//                return;
//            }
//            Print(Root);
//        }
//        private void Print(Node node, string space = " ")
//        {
//            Console.WriteLine(space + node.Value);
//            if (node.Left != null) Print(node.Left, space + "  ");
//            if (node.Right != null) Print(node.Right, space + "  ");
//        }
//        public void PrintLevels()
//        {
//            if (TreeLevel == -1)
//            {
//                Console.WriteLine("Tree is Empty");
//                return;
//            }
//            int level = 1;
//            foreach (var lvl in Levels)
//            {
//                Console.WriteLine("Level:" + level++);
//                foreach (var node in lvl)
//                {
//                    if (node == null)
//                        return;
//                    Console.WriteLine("  " + node.Value);
//                }
//                Console.WriteLine("==============");
//            }
//        }
//        public int[] Search(string value)
//        {
//            if (TreeLevel == -1)
//            {
//                Console.WriteLine("Tree is Empty");
//                return null;
//            }
//            if (value.Equals(""))
//            {
//                Console.WriteLine("Invalid Value!");
//                return null;
//            }

//            int searchSize = Root.GetSize(value);
//            int iLevel = 0;
//            int lastNodeIndex;
//            int[] result = null;
//            foreach (var lvl in Levels)
//            {
//                if (iLevel == TreeLevel)
//                {
//                    // Last Level May Within Null Values
//                    int maxNodes = (int)Math.Pow(2, TreeLevel + 1) - 1;
//                    int nullNodes = maxNodes - NodesNum;
//                    lastNodeIndex = lvl.Length - 1 - nullNodes;
//                }
//                else
//                {
//                    lastNodeIndex = lvl.Length - 1;
//                }

//                // Check if ValueSize on each Level
//                if (lvl[0].ValueSize <= searchSize && searchSize <= lvl[lastNodeIndex].ValueSize)
//                {
//                    result = BinarySearch(value, searchSize, lvl, lastNodeIndex);

//                    if (result != null)
//                        return result;
//                }
//                iLevel++;
//            }

//            Console.WriteLine("Not Found!");
//            return result;
//        }
//        private int[] BinarySearch(string value, int size, Node[] nodesArr, int lastNodeIndex)
//        {
//            int start = 0;
//            int end = lastNodeIndex;
//            int mid = (end + start) / 2;
//            int ptr;

//            while (end >= start)
//            {
//                if (size > nodesArr[mid].ValueSize)
//                {
//                    start = mid + 1;
//                    mid = (end + start) / 2;
//                }
//                if (size < nodesArr[mid].ValueSize)
//                {
//                    end = mid - 1;
//                    mid = (end + start) / 2;
//                }

//                if (size == nodesArr[mid].ValueSize)
//                {
//                    if (nodesArr[mid].Value == value)
//                    {
//                        Console.WriteLine("Found in Level " + (nodesArr[mid].NodeLevel + 1) + " on Index number: " + nodesArr[mid].IndexinLevel);
//                        return new int[] { nodesArr[mid].NodeLevel, nodesArr[mid].IndexinLevel };
//                    }

//                    //May There Some Different Values With Same Size  <1211 1121 1112>

//                    ptr = mid;
//                    while (ptr < lastNodeIndex)
//                    {
//                        if (size == nodesArr[++ptr].ValueSize)
//                            if (nodesArr[ptr].Value == value)
//                            {
//                                Console.WriteLine("Found in Level " + (nodesArr[ptr].NodeLevel + 1) + " on Index number: " + nodesArr[ptr].IndexinLevel);
//                                return new int[] { nodesArr[ptr].NodeLevel, nodesArr[ptr].IndexinLevel };
//                            }
//                    }

//                    ptr = mid;
//                    while (ptr > 0)
//                    {
//                        if (size == nodesArr[--ptr].ValueSize)
//                            if (nodesArr[ptr].Value == value)
//                            {
//                                Console.WriteLine("Found in Level " + (nodesArr[ptr].NodeLevel + 1) + " on Index number: " + nodesArr[ptr].IndexinLevel);
//                                return new int[] { nodesArr[ptr].NodeLevel, nodesArr[ptr].IndexinLevel };
//                            }
//                    }
//                    return null;
//                }
//            }
//            return null;
//        }
//        public void Delete(string value)
//        {
//            int[] indexs = Search(value);
//            if (indexs != null)
//            {
//                int nLevel = indexs[0];
//                int nIndex = indexs[1];

//            }
//        }
//    }

//    public class Node
//    {
//        //public int LeftIndex { get; set; }
//        //public int RightIndex { get; set; }

//        public int ValueSize { get; private set; }
//        public string Value { get; private set; }
//        public int IndexinTree { get; private set; }
//        public int IndexinLevel { get; private set; }
//        public int ParentIndex { get; private set; }
//        public Node Left { get; set; } = null!;
//        public Node Right { get; set; } = null!;
//        public int NodeLevel { get; private set; }
//        public int GetSize(string value)
//        {
//            int valueSize;

//            if (int.TryParse(value, out valueSize))
//            {
//                valueSize = int.Parse(value);
//            }
//            else
//            {
//                int i = 0;
//                while (!value[i].Equals('@'))
//                {
//                    valueSize += (int)value[i];
//                    if (value.Length == ++i)
//                        break;
//                }
//            }
//            return valueSize;
//        }
//        public void SetValue(string value)
//        {
//            this.Value = value;
//            this.ValueSize = GetSize(value);
//        }
//        public Node(string value, int Level, int index)
//        {

//            //LeftIndex = 2 * IndexOfNode + 1;
//            //RightIndex = 2 * IndexOfNode + 2;

//            this.SetValue(value);
//            this.NodeLevel = Level;
//            this.IndexinLevel = index;

//            this.IndexinTree = IndexinLevel + (int)Math.Pow(2, NodeLevel) - 1;
//            this.ParentIndex = IndexinTree % 2 == 0 ? IndexinTree / 2 - 1 : IndexinTree / 2;



//        }
//    }
//}
