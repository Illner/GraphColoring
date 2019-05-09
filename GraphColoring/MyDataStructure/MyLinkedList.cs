namespace GraphColoring.MyDataStructure
{
    class MyLinkedList
    {
        #region Variable
        private MyNode representant;
        private int countElements;
        #endregion
        
        #region Constructor
        public MyLinkedList()
        {
            representant = null;
            countElements = 0;
        }

        public MyLinkedList(MyNode node)
        {
            representant = node;
            node.ResetNode();
            countElements = 1;
        }
        #endregion
        
        #region Method
        /// <summary>
        /// Add element to linkedList
        /// Duplicit values are allowed!
        /// </summary>
        /// <param name="node">The node we want to add</param>
        public void AddElement(MyNode node)
        {
            if (node.GetParent() != null)
                node.GetParent().GetChildrenLinkedList().DeleteElement(node);

            if (representant == null)
            {
                representant = node;
                node.ResetNode();
            }
            else
            {
                // Swap pointers
                node.SetRightNode(representant);
                node.SetLeftNode(representant.GetLeftNode());
                representant.SetLeftNode(node);
                node.GetLeftNode().SetRightNode(node);
            }

            countElements++;
        }

        /// <summary>
        /// Delete one element in linkedList
        /// </summary>
        /// <param name="node">The node we want to remove</param>
        /// <returns>true if element has been removed, otherwise false</returns>
        public bool DeleteElement(MyNode node)
        {
            // Empty linkedList
            if (representant == null)
                return false;

            // We have to delete our representant
            if (representant == node)
            {
                // Only one element in linkedList
                if (countElements == 1)
                {
                    representant = null;
                    node.ResetNode();
                    countElements--;
                    return true;
                }

                // We have to choose new representant
                MyNode tempRepresentant = representant.GetRightNode();
                representant.GetLeftNode().SetRightNode(representant.GetRightNode());
                representant.GetRightNode().SetLeftNode(representant.GetLeftNode());
                representant = tempRepresentant;
                node.ResetNode();

                countElements--;
                return true;
            }

            // Not representant
            node.GetLeftNode().SetRightNode(node.GetRightNode());
            node.GetRightNode().SetLeftNode(node.GetLeftNode());
            node.ResetNode();

            countElements--;

            return true;
        }

        /// <summary>
        /// Concatenate two linkedList
        /// </summary>
        /// <param name="secondLinkedList">The seconnd list we want to concatenate</param>
        /// <returns>concatenated list</returns>
        private MyLinkedList ConcatenateLinkedLists(MyLinkedList secondLinkedList)
        {
            // Variable
            MyNode secondRepresentant = secondLinkedList.GetRepresentant();

            // The first (this) linked list is empty => retutrn the second one
            if (representant == null)
                return secondLinkedList;

            // The second linked list is empty => return the first one
            if (secondRepresentant == null)
                return this;

            // Both are nonempty
            MyNode temp1 = representant.GetLeftNode();
            MyNode temp2 = secondRepresentant.GetLeftNode();
            representant.GetLeftNode().SetRightNode(secondRepresentant);
            representant.SetLeftNode(secondRepresentant.GetLeftNode());
            secondRepresentant.SetLeftNode(temp1);
            temp2.SetRightNode(representant);

            countElements += secondLinkedList.GetCountElements();

            return this;
        }


        public System.Collections.Generic.IEnumerable<MyNode> MyEnumerator()
        {
            MyNode currentNode = representant;

            for (int x = 0; x < countElements; x++)
            {
                currentNode = currentNode.GetRightNode();
                yield return currentNode;
            }
        }


        override
        public string ToString()
        {
            string text = "Linked list - " + countElements + "\n";
            MyNode currentNode = representant;

            if (currentNode == null)
            {
                text += "Empty list";
                return text += "\n";
            }

            for (int i = 0; i < countElements; i++)
            {
                text += currentNode + "\n";
                currentNode = currentNode.GetRightNode();
            }

            return text += "\n";
        }
        #endregion
        
        #region Property
        /// <summary>
        /// Return representant of the linked list
        /// </summary>
        /// <returns>representant - node</returns>
        public MyNode GetRepresentant()
        {
            return representant;
        }

        /// <summary>
        /// Return count of elements
        /// </summary>
        /// <returns>count of elements</returns>
        public int GetCountElements()
        {
            return countElements;
        }
        #endregion
    }
}
