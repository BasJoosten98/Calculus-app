using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculusApp
{
    class NodePlus : Node
    {
        private Node node1;
        private Node node2;
        public override Node Node1 { get { return node1; } }
        public override Node Node2 { get { return node2; } }
        public NodePlus()
        {

        }
        public override void AddNode1(Node n)
        {
            if (node1 != null) { node1.AddParentNode(null); }
            node1 = n;
            if (n != null)
            {
                node1.AddParentNode(this);
            }
        }
        public override void AddNode2(Node n)
        {
            if (node2 != null) { node2.AddParentNode(null); }
            node2 = n;
            if (n != null)
            {
                node2.AddParentNode(this);
            }
        }
        public override double GetValueForX(double X)
        {
            return node1.GetValueForX(X) + node2.GetValueForX(X);
        }
        public override string GetHumanReadableString()
        {
            if (node1 != null && node2 != null)
            {
                return node1.GetHumanReadableString() + " + " + node2.GetHumanReadableString();
            }
            return "";
        }
        public override string GetLocalString()
        {
            return "+( , )";
        }
        public override Node Clone()
        {
            Node newNode = new NodePlus();
            Node newNode1 = node1.Clone();
            Node newNode2 = node2.Clone();
            newNode.AddNode1(newNode1);
            newNode1.AddParentNode(newNode);
            newNode.AddNode2(newNode2);
            newNode2.AddParentNode(newNode);
            return newNode;
        }
        public override Node GetDerivative() //O = original, D = derivative
        {
            Node nodePlus = new NodePlus();
            Node node1D = node1.GetDerivative();
            Node node2D = Node2.GetDerivative();

            nodePlus.AddNode1(node1D);
            nodePlus.AddNode2(node2D);

            return nodePlus;
        }
        public override bool ContainsX()
        {
            bool holder = false;
            if (node1.ContainsX()) { holder = true; }
            if (Node2.ContainsX()) { holder = true; }
            return holder;
        }
        public override bool ContainsP()
        {
            bool holder = false;
            if (node1.ContainsP()) { holder = true; }
            if (Node2.ContainsP()) { holder = true; }
            return holder;
        }
        public override Node MakeNodeClean(Node prevNodeNumber, out Node replacementNode)
        {
            Node cleanNode1;
            Node cleanNode2;
            Node receivedReplacementNode;
            if (node1 is NodeNumber && node2 is NodeNumber) //both are numbers, can't go deeper in the tree!
            {
                Node combinedNode = combineNodes(node1, node2);
                if (combinedNode != null)
                {
                    replacementNode = null;
                    return combinedNode;
                }
                else
                {
                    if (prevNodeNumber != null)
                    {
                        combinedNode = combineNodes(node1, prevNodeNumber);
                        if (combinedNode != null)
                        {
                            replacementNode = combinedNode;
                            return node2;
                        }
                        combinedNode = combineNodes(node2, prevNodeNumber);
                        if (combinedNode != null)
                        {
                            replacementNode = combinedNode;
                            return node1;
                        }
                    }
                    //everything can't be combined!
                    replacementNode = null;
                    return this;
                }
            }
            if (node1 is NodeNumber) //node1 is a number, but node2 is not
            {
                if(Node2 is NodeMinus)
                {
                    Node newNode2 = Node2.MakeNodeClean(null, out replacementNode); //change node2 to nodeplus
                    this.AddNode2(newNode2);
                }
                if (node2 is NodePlus) { cleanNode2 = node2.MakeNodeClean(node1, out receivedReplacementNode); }
                else { cleanNode2 = node2.MakeNodeClean(null, out receivedReplacementNode); }
                this.AddNode2(cleanNode2);
                if (receivedReplacementNode != null) //node1 has been combined in receivedReplacementNode
                {
                    this.AddNode1(receivedReplacementNode);
                    Node combinedNode1 = combineNodes(node1, node2);
                    if (combinedNode1 != null)
                    {
                        replacementNode = null;
                        return combinedNode1;
                    }
                    else
                    {
                        if (prevNodeNumber != null) //try to combine node1 with preNodeNumber
                        {
                            Node combinedNode2 = combineNodes(node1, prevNodeNumber);
                            if (combinedNode2 != null)
                            {
                                replacementNode = combinedNode2;
                                return node2;
                            }
                        }
                        replacementNode = null;
                        return this;
                    }

                }
                else //node1 has not been combined in receivedReplacementNode
                {
                    Node combinedNode1 = combineNodes(node1, node2);
                    if (combinedNode1 != null)
                    {
                        replacementNode = null;
                        return combinedNode1;
                    }
                    else
                    {
                        if (prevNodeNumber != null)//try to combine node1 with preNodeNumber
                        {
                            Node combinedNode2 = combineNodes(node1, prevNodeNumber);
                            if (combinedNode2 != null)
                            {
                                replacementNode = combinedNode2;
                                return node2;
                            }
                        }
                        replacementNode = null;
                        return this;
                    }
                }
            }
            else if (node2 is NodeNumber) //node2 is a number, but node1 is not
            {
                if (Node1 is NodeMinus)
                {
                    Node newNode1 = Node1.MakeNodeClean(null, out replacementNode); //change node2 to nodeplus
                    this.AddNode1(newNode1);
                }
                if (node1 is NodePlus) { cleanNode1 = node1.MakeNodeClean(node2, out receivedReplacementNode); }
                else { cleanNode1 = node1.MakeNodeClean(null, out receivedReplacementNode); }
                this.AddNode1(cleanNode1);
                if (receivedReplacementNode != null) //node1 has been combined in receivedReplacementNode
                {
                    this.AddNode2(receivedReplacementNode);
                    Node combinedNode1 = combineNodes(node1, node2);
                    if (combinedNode1 != null)
                    {
                        replacementNode = null;
                        return combinedNode1;
                    }
                    else
                    {
                        if (prevNodeNumber != null) //try to combine node1 with preNodeNumber
                        {
                            Node combinedNode = combineNodes(node2, prevNodeNumber);
                            if (combinedNode != null)
                            {
                                replacementNode = combinedNode;
                                return node1;
                            }
                        }
                    }
                    replacementNode = null;
                    return this;
                }
                else //node1 has not been combined in receivedReplacementNode
                {
                    Node combinedNode1 = combineNodes(node1, node2);
                    if (combinedNode1 != null)
                    {
                        replacementNode = null;
                        return combinedNode1;
                    }
                    else
                    {
                        if (prevNodeNumber != null)//try to combine node1 with preNodeNumber
                        {
                            Node combinedNode = combineNodes(node2, prevNodeNumber);
                            if (combinedNode != null)
                            {
                                replacementNode = combinedNode;
                                return node1;
                            }
                        }
                    }
                    replacementNode = null;
                    return this;
                }
            }
            //both are no numbers
            Node replacement1; //will be null
            Node replacement2; //will be null
            cleanNode1 = node1.MakeNodeClean(null, out replacement1);
            cleanNode2 = Node2.MakeNodeClean(null, out replacement2);
            this.AddNode1(cleanNode1);
            this.AddNode2(cleanNode2);
            Node combinedNodeFinal = combineNodes(cleanNode1, cleanNode2);
            if (combinedNodeFinal != null)
            {
                replacementNode = null;
                return combinedNodeFinal;
            }
            replacementNode = null;
            return this;
        }
        private Node combineNodes(Node cleanNode1, Node cleanNode2)
        {
            if (cleanNode1 is NodeNumber && !(cleanNode2 is NodeNumber))
            {
                if (((NodeNumber)cleanNode1).Number == 0)
                {
                    return cleanNode2;
                }
            }
            if (cleanNode2 is NodeNumber && !(cleanNode1 is NodeNumber))
            {
                if (((NodeNumber)cleanNode2).Number == 0)
                {
                    return cleanNode1;
                }
            }
            if (cleanNode1 is NodeNumber && cleanNode2 is NodeNumber)
            {
                if (((NodeNumber)cleanNode1).Number == 0)
                {
                    Node newNumberNode;
                    newNumberNode = new NodeNumber(cleanNode2.ContainsX(), cleanNode2.ContainsP(), ((NodeNumber)cleanNode2).Number);
                    return newNumberNode;
                }
                else if (((NodeNumber)cleanNode2).Number == 0)
                {
                    Node newNumberNode;
                    newNumberNode = new NodeNumber(cleanNode1.ContainsX(), cleanNode1.ContainsP(), ((NodeNumber)cleanNode1).Number);
                    return newNumberNode;
                }
                else if (cleanNode1.ContainsX() == cleanNode2.ContainsX())
                {
                    if (cleanNode1.ContainsP() == cleanNode2.ContainsP())
                    {
                        Node newNumberNode;
                        newNumberNode = new NodeNumber(cleanNode1.ContainsX(), cleanNode1.ContainsP(), ((NodeNumber)cleanNode1).Number + ((NodeNumber)cleanNode2).Number);
                        return newNumberNode;
                    }
                }
            }

            //none of the nodes are numbers, so they need to be cleaned!
            Node trash;

            if(cleanNode1 is NodeLN && cleanNode2 is NodeLN)
            {
                Node nodeln = new NodeLN();
                Node nodeTim = new NodeTimes();
                nodeTim.AddNode1(cleanNode1.Node1);
                nodeTim.AddNode2(cleanNode2.Node1);
                nodeln.AddNode1(nodeTim);
                nodeln.MakeNodeClean(null, out trash);
                return nodeln;
            }

            return null;
        }
        public void SwapPositivity()
        {
            if (node1 is NodeNumber)
            {
                ((NodeNumber)node1).SwapPositivity();
            }
            else if (node1 is NodeMinus)
            {
                ((NodeMinus)node1).SwapPositivity();
            }
            else if(node1 is NodePlus)
            {
                ((NodePlus)node1).SwapPositivity();
            }
            else
            {
                Node nodeTimes = new NodeTimes();
                Node nodeNumber = new NodeNumber(false, false, -1);
                nodeTimes.AddNode1(nodeNumber);
                nodeTimes.AddNode2(node1);
                this.AddNode1(nodeTimes);
            }
            if (node2 is NodeNumber)
            {
                ((NodeNumber)node2).SwapPositivity();
            }
            else if (node2 is NodeMinus)
            {
                ((NodeMinus)node2).SwapPositivity();
            }
            else if (node2 is NodePlus)
            {
                ((NodePlus)node2).SwapPositivity();
            }
            else
            {
                Node nodeTimes = new NodeTimes();
                Node nodeNumber = new NodeNumber(false, false, -1);
                nodeTimes.AddNode1(nodeNumber);
                nodeTimes.AddNode2(node2);
                this.AddNode2(nodeTimes);
            }
        }
        public override bool SameAs(Node n)
        {
            if (n is NodePlus)
            {
                bool temp = false;
                if (n.Node1.SameAs(this.node1))
                {
                    if (n.Node2.SameAs(this.node2))
                    {
                        temp = true;
                    }
                }
                else if (n.Node2.SameAs(this.node1))
                {
                    if (n.Node1.SameAs(this.node2))
                    {
                        temp = true;
                    }
                }
                return temp;
            }
            return false;
        }
    }
}
