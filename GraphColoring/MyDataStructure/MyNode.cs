namespace GraphColoring.MyDataStructure
{
    class MyNode
    {
        // Variable
        #region
        /// <summary>
        /// identifier - identifier of the node
        /// value - value of the node
        /// parentNode - pointer to parent (can be null => root)
        /// childrenLinkedList - list of children represented by linked list
        /// rank - count of children
        /// feature - true if we deleted its child, otherwise false
        /// </summary>
        private int identifier;
        private int value;
        private MyNode parentNode;
        private MyNode leftNode, rightNode;
        private MyLinkedList childrenLinkedList;
        private int rank;
        private bool feature;
        #endregion

        // Constructor
        #region
        public MyNode(int identifier, int value)
        {
            this.identifier = identifier;
            this.value = value;

            // Set default values
            rank = 0;
            feature = false;
            parentNode = null;
            leftNode = this;
            rightNode = this;
            childrenLinkedList = new MyLinkedList();
        }

        public MyNode(int identifier, int value, MyNode parentNode)
        {
            this.identifier = identifier;
            this.value = value;
            this.parentNode = parentNode;

            // Set default values
            rank = 0;
            feature = false;
            leftNode = this;
            rightNode = this;
            childrenLinkedList = new MyLinkedList();
        }

        override
        public string ToString()
        {
            string text;

            text = "Identifier: " + identifier + ", value: " + value + ", rank: " + rank + ", feature: " + feature + ", parentNode: ";

            if (HasParent())
                text += parentNode.GetIdentifier();
            else
                text += "None";
            text += ", leftNode: " + leftNode.GetIdentifier() + ", rightNode: " + rightNode.GetIdentifier();

            return text;
        }
        #endregion

        // Method
        #region
        /// <summary>
        /// Return true if this node has a parent, otherwise false
        /// </summary>
        /// <returns></returns>
        public bool HasParent()
        {
            if (parentNode == null)
                return false;

            return true;
        }

        /// <summary>
        /// Decrement value
        /// </summary>
        /// <param name="newValue">new value</param>
        /// <returns>true if the value has been decremented, otherwise false</returns>
        public bool DecrementValue(int newValue)
        {
            if (newValue >= value)
                return false;

            SetValue(newValue);
            return true;
        }

        /// <summary>
        /// Decrement the rank by 1
        /// </summary>
        public void DecrementRank()
        {
            if (rank == 0)
                throw new MyException.MyDataStructureException.MyNodeNegativeRankException(ToString());

            rank--;
        }

        /// <summary>
        /// Increment rank by 1
        /// </summary>
        public void IncrementRank()
        {
            rank++;
        }

        public void ResetNode()
        {
            SetLeftNode(this);
            SetRightNode(this);
        }
        #endregion

        // Property
        #region
        /// <summary>
        /// Return the identifier of this node
        /// </summary>
        /// <returns>identifier</returns>
        public int GetIdentifier()
        {
            return identifier;
        }

        /// <summary>
        /// Return the value of this node
        /// </summary>
        /// <returns>valuevalue</returns>
        public int GetValue()
        {
            return value;
        }

        /// <summary>
        /// Set a new value of this node
        /// </summary>
        /// <param name="value">value</param>
        public void SetValue(int value)
        {
            this.value = value;
        }

        /// <summary>
        /// Return the parent of this node
        /// </summary>
        /// <returns>parent</returns>
        public MyNode GetParent()
        {
            return parentNode;
        }

        public MyNode GetLeftNode()
        {
            return leftNode;
        }

        public void SetLeftNode(MyNode leftNode)
        {
            this.leftNode = leftNode;
        }

        public MyNode GetRightNode()
        {
            return rightNode;
        }

        public void SetRightNode(MyNode rightNode)
        {
            this.rightNode = rightNode;
        }

        /// <summary>
        /// Set a new parent of this node
        /// </summary>
        /// <param name="parentNode">new parent</param>
        public void SetParent(MyNode parentNode)
        {
            this.parentNode = parentNode;
        }

        /// <summary>
        /// Return the feature of this node
        /// </summary>
        /// <returns>feature</returns>
        public bool GetFeature()
        {
            return feature;
        }

        /// <summary>
        /// Set a new feature's value
        /// </summary>
        /// <param name="feature"></param>
        public void SetFeature(bool feature)
        {
            if (parentNode != null)
                this.feature = feature;
            else
                this.feature = false;
        }

        /// <summary>
        /// Return the rank of this node
        /// </summary>
        /// <returns>rank</returns>
        public int GetRank()
        {
            return rank;
        }

        /// <summary>
        /// Set a new rank's value
        /// </summary>
        /// <param name="rank">rank</param>
        public void SetRank(int rank)
        {
            this.rank = rank;
        }

        /// <summary>
        /// Return linked list with children
        /// </summary>
        /// <returns>list of children</returns>
        public MyLinkedList GetChildrenLinkedList()
        {
            return childrenLinkedList;
        }
        #endregion
    }
}
