
using System;

namespace DataTest
{
    public class NewTree
    {
        public TreeNode RootPtr = new TreeNode("RootPtr");
        public TreeNode Root { get; private set; } = null!;
        public void Add(string value)
        {
            if (string.IsNullOrEmpty(value))
                return;

            if (Root == null)
            {
                Root = new TreeNode(value);
                RootPtr.Left = Root;
            }
            else
            {
                AddNode(Root, new TreeNode(value));
                Balance();
            }
        }
        private void AddNode(TreeNode node, TreeNode newNode)
        {
            if (newNode.getSize(newNode.Value) > node.getSize(node.Value))
            {
                if (node.Right == null)
                {
                    node.Right = newNode;
                }
                else
                {
                    AddNode(node.Right, newNode);
                }
            }
            else if (newNode.getSize(newNode.Value) <= node.getSize(node.Value))
            {
                if (node.Left == null)
                {
                    node.Left = newNode;
                }
                else
                {
                    AddNode(node.Left, newNode);
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
        private void Print(TreeNode node, bool printsize, string space = " ")
        {
            if (printsize) // Print by ValueSize
            {
                Console.WriteLine(space + node.getSize(node.Value));
            }
            else // Print by Value
            {
                Console.WriteLine(space + node.Value);
            }
            if (node.Left != null)
            {
                Print(node.Left, printsize, space + "  ");
            }
            if (node.Right != null)
            {
                Print(node.Right, printsize, space + "  ");
            }
        }

        public void Balance()
        {
            Balance(RootPtr);
        }
        private void Balance(TreeNode node)
        {
            if (node.Left != null)
            {
                //Check The Balance Of Depth Nodes First
                Balance(node.Left);
                if (node.Left.getFactor() == 2 || node.Left.getFactor() == -2)
                {
                    node.Left = Rotate(node.Left);
                }
            }
            if (node.Right != null)
            {
                Balance(node.Right);
                if (node.Right.getFactor() == 2 || node.Right.getFactor() == -2)
                {
                    node.Right = Rotate(node.Right);
                }
            }
        }
        private TreeNode Rotate(TreeNode node)
        {
            TreeNode NewRoot = node;
            TreeNode OldLeft = null;
            TreeNode OldRight = null;
            string Rotation = HighestTwoSubTree(node); // Rotation type 

            //Maintaining the binary tree princeple

            if (Rotation == "LL")
            {
                if (node.getSize(node.Value) == node.getSize(node.Left.Value))
                {
                    return node;
                }
            }
            else if (Rotation == "LR")
            {
                if (node.getSize(node.Value) == node.getSize(node.Left.Right.Value))
                {
                    return node;
                }
            }
            else if (Rotation == "RL")
            {
                if (node.getSize(node.Right.Value) == node.getSize(node.Right.Left.Value))
                {
                    return node;
                }
            }

            if (Rotation == "LL") // Left Left Rotation 
            {
                NewRoot = node.Left;
                OldRight = NewRoot.Right;
                node.Left = null;
                NewRoot.Right = node;
            }
            else if (Rotation == "LR") // Left Right Rotation 
            {
                NewRoot = node.Left.Right;
                OldLeft = NewRoot.Left;
                OldRight = NewRoot.Right;
                node.Left.Right = null;
                NewRoot.Left = node.Left;
                node.Left = null;
                NewRoot.Right = node;
            }
            else if (Rotation == "RR") // Right Right Rotation 
            {
                if (Nod.getSize(Nod.Value) == Nod.getSize(Nod.Right.Value))
                    return Nod;

                NewRoot = Nod.Right;
                OldLeft = NewRoot.Left;
                node.Right = null;
                NewRoot.Left = node;
            }
            else if (Rotation == "RL") // Right Left Rotation 
            {
                if (Nod.getSize(Nod.Value) == Nod.getSize(Nod.Right.Left.Value))
                    return Nod;

                NewRoot = Nod.Right.Left;
                OldRight = NewRoot.Right;
                OldLeft = NewRoot.Left;
                node.Right.Left = null;
                NewRoot.Right = node.Right;
                node.Right = null;
                NewRoot.Left = node;
            }

            if (OldLeft != null)
            {
                AddNode(NewRoot, OldLeft);
            }
            if (OldRight != null)
            {
                AddNode(NewRoot, OldRight);
            }
            if (node == Root)
            {
                Root = NewRoot;
            }
            return NewRoot;
        }
        private string HighestTwoSubTree(TreeNode node)
        {
            string result; // Rotation Type 

            // Check the two nodes(highest) that make the root unbalanced
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

        public TreeNode Search(string value, bool searchparent = false)
        {
            TreeNode SearchedNode = null;
            Search(Root, value, ref SearchedNode, false, false);

            if (string.IsNullOrEmpty(value) || Root == null || SearchedNode == null) // Invalid value || Empty Tree || Node not found
                return null;

            if (!searchparent) //Default
            {
                return SearchedNode;
            }
            else if (searchparent) // Search for Parent Mode
            {
                SearchedNode = null;
                Search(RootPtr, value, ref SearchedNode, true, false);
                return SearchedNode;
            }
            return null;
        }
        private void Search(TreeNode node, string value, ref TreeNode Searched, bool searchparent = false, bool getHighest = false)
        {
            if (node == null || Searched != null) //Result
                return;

            if (getHighest) // Bring the Highest node on the Left SubTree
            {
                if (node.Left == null) return;

                for (Searched = node.Left; Searched.Right != null; Searched = Searched.Right)
                {
                    //Default Case ==> result = Last node

                    if (searchparent)
                    {
                        if (Searched.Right.Right == null) // Searchparent mode ==> result = Last node parent
                            return;
                    }
                }
            }
            else
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
                else
                {
                    Search(node.Left, value, ref Searched, searchparent, false);
                }
            }
        }

        public TreeNode Delete(string value)
        {
            TreeNode node = Search(value); // Check if the node exists
            if (node == null)
            {
                return null;
            }
            TreeNode parent = null;
            Search(RootPtr, value, ref parent, true, false);

            char side = parent.Left == node ? 'L' : 'R'; // Node side

            Delete(side, node.childrensNum(), node, parent);
            Balance();
            return node;
        }
        private void Delete(char side, int childrensNumber, TreeNode node, TreeNode parent)
        {
            if (childrensNumber == 0) // Delete the leaf
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
            else if (childrensNumber == 1) // Replace the node with his leaf
            {
                if (side == 'L')
                {
                    if (node.Right == null)
                    {
                        parent.Left = node.Left;
                    }
                    else
                    {
                        parent.Left = node.Right;
                    }
                }
                else
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
            }
            else if (childrensNumber == 2)
            {
                TreeNode LargestNodeFromLeft = null;

                Search(node, node.Value, ref LargestNodeFromLeft, false, true);

                node.Value = LargestNodeFromLeft.Value;


                if (node.Left.Right == null) // If the Left Subtree have no Right brunch ==> The root will be replaced by the left branch
                {
                    TreeNode OldRight = node.Right.deepCopy();

                    if (side == 'L')
                    {
                        parent.Left = node.Left;
                        parent.Left.Right = OldRight;
                    }
                    else
                    {
                        parent.Right = node.Left;
                        parent.Right.Right = OldRight;
                    }
                }

                else // If the Left Subtree has two branches ==> The root will be replaced by the Highest Node in Left Subtre
                {
                    TreeNode LargestNodeParent = null;

                    Search(node, node.Value, ref LargestNodeParent, true, true);

                    if (LargestNodeFromLeft.childrensNum() > 0) // The last node on the Right has a Left branch
                    {
                        LargestNodeParent.Right = LargestNodeFromLeft.Left;
                    }
                    else //The last node has no children
                    {
                        LargestNodeParent.Right = null;
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
                TreeNode node = Delete(value).deepCopy(); //Catch the Deleted node => Change it value => Re-add it to the tree
                node.Left = null;
                node.Right = null;

                node.Value = newValue;
                AddNode(RootPtr, node);
                Balance();
            }
            catch (Exception)
            {
                Console.WriteLine("Not Found!");
            }
        }

        public int TreeHeight()
        {
            return RootPtr.getHeight(Root);
        }

    }

    public class TreeNode
    {
        public string Value { get; set; }
        public TreeNode(string value) { Value = value; }
        public TreeNode Left { get; set; } = null!;
        public TreeNode Right { get; set; } = null!;

        public int getSize(string value)
        {
            int size = 0;
            for (int i = 0; i < value.Length; i++)
            {
                size += (short)value[i];
            }
            return size;
        }
        public int getHeight(TreeNode node)
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
        public TreeNode deepCopy()
        {
            TreeNode clone = new TreeNode("null");
            clone.Value = this.Value;
            clone.Left = this.Left;
            clone.Right = this.Right;
            return clone;
        }
    }
}
