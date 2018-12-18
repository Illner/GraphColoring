using System;
using System.Collections.Generic;
using System.Text;

namespace GraphColoring.MyDataStructure
{
    class FibonacciHeap
    {
        // Variable
        #region
        /// <summary>
        /// rootLinkedList - linked list contained roots
        /// countNodes - count of nodes
        /// minNode - pointer to node with minimal value
        /// nodeMappingList - used for mapping nodes, need for Decrease function
        /// </summary>
        public MyLinkedList rootLinkedList;
        private int countNodes;
        private int maxCountNodes;
        private MyNode minNode;
        private MyNode[] nodeMappingArray;
        private int sizeRankArray;
        #endregion

        // Constructor
        #region
        public FibonacciHeap(int maxCountNodes)
        {
            this.maxCountNodes = maxCountNodes;

            // Set default values
            countNodes = 0;
            minNode = null;
            rootLinkedList = new MyLinkedList();
            nodeMappingArray = new MyNode[maxCountNodes + 1];
            sizeRankArray = (int)Math.Ceiling(2 * Math.Log(maxCountNodes, 2));
        }
        #endregion

        // Method
        #region
        /// <summary>
        /// Return (and delete) smallest item in Fibonacci heap
        /// </summary>
        /// <returns>smallest item</returns>
        public MyNode ExtractMin()
        {
            // Variable
            MyNode returnMyNode;

            if (minNode == null)
                throw new MyException.MyDataStructureException.FibonacciHeapAttempToExtractMinInEmptyHeapException();

            countNodes--;
            returnMyNode = minNode;

            while (minNode.GetChildrenLinkedList().GetRepresentant() != null)
            {
                MyNode child = minNode.GetChildrenLinkedList().GetRepresentant();
                
                rootLinkedList.AddElement(child);
                child.SetParent(null);
                child.SetFeature(false);
            }

            // Destroy minElement
            rootLinkedList.DeleteElement(minNode);
            nodeMappingArray[minNode.GetIdentifier()] = null;
            minNode = null;

            Consolidation();

            return returnMyNode;
        }

        /// <summary>
        /// Return tru if element with the identifier exists, otherwise return false
        /// </summary>
        /// <param name="identifier">The identifier</param>
        /// <returns>true, if the element exists</returns>
        public bool ElementExists(int identifier)
        {
            // Variable
            MyNode node = nodeMappingArray[identifier];

            if (node == null)
                return false;

            return true;
        }

        /// <summary>
        /// Return value of the element with the identifier
        /// </summary>
        /// <param name="identifier">The identifier</param>
        /// <returns>Value of the element</returns>
        public int GetValue(int identifier)
        {
            // Variable
            MyNode node = nodeMappingArray[identifier];

            if (node == null)
                throw new MyException.MyDataStructureException.FibonacciHeapUnknownElementException("Identifier: " + identifier);

            return node.GetValue();
        }

        /// <summary>
        /// Decrease the value of item
        /// </summary>
        /// <param name="identifier">the node we want to decrease</param>
        /// <param name="newValue">new value of item</param>
        public void Decrease(int identifier, int newValue)
        {
            // Variable
            bool result;
            MyNode parent;
            MyNode node = nodeMappingArray[identifier];

            if (node == null)
                throw new MyException.MyDataStructureException.FibonacciHeapUnknownElementException("Identifier: " + identifier + ", new value: " + newValue);

            result = node.DecrementValue(newValue);

            // Attempt to icrement the value
            if (!result)
                return;
            
            parent = node.GetParent();


            if (parent != null && node.GetValue() < parent.GetValue())
            {
                Cut(node, parent);
                
                MyNode grandParent = parent.GetParent();

                while (grandParent != null)
                {
                    if (!parent.GetFeature())
                    {
                        parent.SetFeature(true);
                        break;
                    }
                    else
                    {
                        Cut(parent, grandParent);
                    }

                    parent = grandParent;
                    grandParent = parent.GetParent();
                }
            }

            // Check min
            if (minNode == null || newValue < minNode.GetValue())
                minNode = node;
        }

        private void Cut(MyNode child, MyNode parent)
        {
            bool result = parent.GetChildrenLinkedList().DeleteElement(child);
            
            parent.DecrementRank();
            child.SetParent(null);
            child.SetFeature(false);
            rootLinkedList.AddElement(child);
        }

        /// <summary>
        /// Add new heap to Fibonacci heap
        /// </summary>
        /// <param name="identifier">New's item identifier</param>
        /// <param name="value">New's item value</param>
        public void Insert(int identifier, int value)
        {
            MyNode node = new MyNode(identifier, value);

            // Check min
            if (minNode == null || value < minNode.GetValue())
                minNode = node;

            // Add to mapping list
            if (nodeMappingArray[identifier] != null)
                throw new MyException.MyDataStructureException.FibonacciHeapInsertIdentifierAlreadyKnownException("Identifier: " + identifier + ", value: " + value);

            nodeMappingArray[identifier] = node;

            countNodes++;
            rootLinkedList.AddElement(node);
        }

        private void Consolidation()
        {
            MyNode[] rankArray = new MyNode[sizeRankArray];
            MyNode secondRoot;

            int countOfNodes = rootLinkedList.GetCountElements();
            MyNode nextNode;
            MyNode rootNode = rootLinkedList.GetRepresentant();

            for (int i = 0; i < countOfNodes; i++)
            {
                nextNode = rootNode.GetRightNode();

                if (rankArray[rootNode.GetRank()] == null)
                {
                    rankArray[rootNode.GetRank()] = rootNode;
                }
                // We already have root in array[i]. We need to megre.
                else
                {
                    // Variable
                    MyNode newRoot = rootNode;
                    int previousRate;

                    while (rankArray[newRoot.GetRank()] != null)
                    {
                        previousRate = newRoot.GetRank();
                        secondRoot = rankArray[previousRate];
                        newRoot = MergeTwoHeaps(secondRoot, newRoot);

                        rankArray[previousRate] = null;
                    }

                    rankArray[newRoot.GetRank()] = newRoot;
                }

                rootNode = nextNode;
            }

            rootLinkedList = new MyLinkedList();
            foreach (MyNode rootArray in rankArray)
            {
                if (rootArray != null)
                {
                    // Check min
                    if (minNode == null || rootArray.GetValue() < minNode.GetValue())
                        minNode = rootArray;

                    rootLinkedList.AddElement(rootArray);
                }
            }
        }

        /// <summary>
        /// Merge two heaps
        /// </summary>
        /// <param name="root1">The root of the first heap</param>
        /// <param name="root2">The root of the second heap</param>
        /// <returns>The root of the merged heap</returns>
        public MyNode MergeTwoHeaps(MyNode root1, MyNode root2)
        {
            // The first one has smaller root's value
            if (root1.GetValue() <= root2.GetValue())
            {
                root1.GetChildrenLinkedList().AddElement(root2);
                root2.SetParent(root1);
                root2.SetFeature(false);
                root1.SetParent(null);
                root1.IncrementRank();
                root1.ResetNode();

                return root1;
            }
            // The second one has smaller root's value
            else
            {
                root2.GetChildrenLinkedList().AddElement(root1);
                root1.SetParent(root2);
                root1.SetFeature(false);
                root2.SetParent(null);
                root2.IncrementRank();
                root2.ResetNode();

                return root2;
            }
        }

        override
        public string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            Queue<MyNode> nodeQueue = new Queue<MyNode>();

            stringBuilder.AppendLine("digraph {");

            foreach (MyNode node in rootLinkedList.MyEnumerator())
            {
                nodeQueue.Enqueue(node);
            }
            
            while (nodeQueue.Count != 0)
            {
                MyNode currentNode = nodeQueue.Dequeue();

                MyLinkedList childrenList = currentNode.GetChildrenLinkedList();
                if (childrenList.GetCountElements() == 0)
                {
                    stringBuilder.AppendLine("\"" + currentNode.GetIdentifier() + "(" + currentNode.GetValue() + ")\"" + " -> \"" + currentNode.GetIdentifier() + "(" + currentNode.GetValue() + ")" + "\" ;");
                    continue;
                }

                foreach (MyNode child in childrenList.MyEnumerator())
                {
                    stringBuilder.AppendLine("\"" + currentNode.GetIdentifier() + "(" + currentNode.GetValue() + ")\"" + " -> \"" + child.GetIdentifier() + "(" + child.GetValue() + ")" + "\" ;");
                    nodeQueue.Enqueue(child);
                }
            }
            System.Console.WriteLine();
            stringBuilder.AppendLine("}");

            return stringBuilder.ToString();
        }
        #endregion

        // Property
        #region
        /// <summary>
        /// Return count of nodes in Fibonacci heap
        /// </summary>
        /// <returns>count of nodes</returns>
        public int GetCountNodes()
        {
            return countNodes;
        }

        public MyNode GetMinNode()
        {
            return minNode;
        }

        public int GetMaxCountNodes()
        {
            return maxCountNodes;
        }
        #endregion
    }
}
