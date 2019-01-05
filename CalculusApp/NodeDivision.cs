using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculusApp
{
    class NodeDivision :Node
    {
        private Node node1;
        private Node node2;
        public override Node Node1 { get { return node1; } }
        public override Node Node2 { get { return node2; } }
        public NodeDivision()
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
            if(node2.GetValueForX(X) != 0f)
            {
                double answer = 0;
                double node1Answer = node1.GetValueForX(X);
                double node2Answer = node2.GetValueForX(X);
                answer = node1Answer / node2Answer;
                return answer;
            }
            throw new Exception("Division by 0 is not possible (x = " + X + ")");
        }
        public override string GetHumanReadableString()
        {
            if (node1 != null && node2 != null)
            {
                return "(" + node1.GetHumanReadableString() + ") / (" + node2.GetHumanReadableString() + ")";
            }
            return "";
        }
        public override string GetLocalString()
        {
            return "/( , )";
        }
        public override Node Clone()
        {
            Node newNode = new NodeDivision();
            Node newNode1 = node1.Clone();
            Node newNode2 = node2.Clone();
            newNode.AddNode1(newNode1);
            newNode.AddNode2(newNode2);
            return newNode;
        }
        public override Node GetDerivative() //O = original, D = derivative
        {
            Node nodeDiv = new NodeDivision();
            Node nodeTimes1 = new NodeTimes();
            Node nodeTimes2 = new NodeTimes();
            Node nodeMin = new NodeMinus();
            Node nodePow = new NodePower();
            Node nodeNumber2 = new NodeNumber(false, false, 2);
            Node node1D = node1.GetDerivative();
            Node node2D = Node2.GetDerivative();
            Node node1O = node1.Clone();
            Node node2O = node2.Clone();
            Node node2O2 = node2.Clone();

            //divison (head node)
            nodeDiv.AddNode1(nodeMin);
            nodeDiv.AddNode2(nodePow);

            //creating upper part of division
            nodeMin.AddNode1(nodeTimes1);
            nodeMin.AddNode2(nodeTimes2);

            nodeTimes1.AddNode1(node1D);
            nodeTimes1.AddNode2(node2O);

            nodeTimes2.AddNode1(node1O);
            nodeTimes2.AddNode2(node2D);

            //creating lower part of division
            nodePow.AddNode1(node2O2);
            nodePow.AddNode2(nodeNumber2);

            return nodeDiv;
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
                if (node2 is NodeDivision) { cleanNode2 = node2.MakeNodeClean(node1, out receivedReplacementNode); }
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
                if (node1 is NodeDivision) { cleanNode1 = node1.MakeNodeClean(node2, out receivedReplacementNode); }
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
            if(combinedNodeFinal != null)
            {
                replacementNode = null;
                return combinedNodeFinal;
            }
            replacementNode = null;
            return this;
        }
        private Node combineNodes(Node cleanNode1, Node cleanNode2)
        {
            if (cleanNode1 is NodeNumber && cleanNode2 is NodeNumber)
            {
                int negOrPos = 0;
                if (((NodeNumber)cleanNode1).Number == ((NodeNumber)cleanNode2).Number)
                {
                    negOrPos = 1;                    
                }
                else if (((NodeNumber)cleanNode1).Number == -1 * ((NodeNumber)cleanNode2).Number)
                {
                    negOrPos = -1;
                }
                if(negOrPos != 0)
                {
                    if (cleanNode1.ContainsX() == cleanNode2.ContainsX() && cleanNode1.ContainsP() == cleanNode2.ContainsP())
                    {
                        Node newNumberNode;
                        newNumberNode = new NodeNumber(false, false, negOrPos);
                        return newNumberNode;
                    }
                    else if (cleanNode1.ContainsX() == cleanNode2.ContainsX() && !cleanNode2.ContainsP())
                    {
                        Node newNumberNode;
                        newNumberNode = new NodeNumber(false, true, negOrPos);
                        return newNumberNode;
                    }
                    else if (cleanNode1.ContainsP() == cleanNode2.ContainsP() && !cleanNode2.ContainsX())
                    {
                        Node newNumberNode;
                        newNumberNode = new NodeNumber(true, false, negOrPos);
                        return newNumberNode;
                    }
                }
            }
            if (cleanNode1 is NodeNumber)
            {
                if (((NodeNumber)cleanNode1).Number == 0)
                {
                    Node newNumberNode;
                    newNumberNode = new NodeNumber(false, false, 0);
                    return newNumberNode;
                }
                if (cleanNode2 is NodeFactorial) {
                    if (((NodeNumber)cleanNode1).Number == ((NodeFactorial)cleanNode2).GetValueForX(0))
                    {
                        return new NodeNumber(cleanNode1.ContainsX(), cleanNode1.ContainsP(), 1);
                    }
                }
            }
            if(cleanNode2 is NodeNumber)
            {
                if (cleanNode1 is NodeFactorial)
                {
                    if (((NodeNumber)cleanNode2).Number == ((NodeFactorial)cleanNode1).GetValueForX(0))
                    {
                        Node nodeDiv = new NodeDivision();
                        nodeDiv.AddNode1(new NodeNumber(false, false, 1));
                        nodeDiv.AddNode2(new NodeNumber(cleanNode2.ContainsX(), cleanNode2.ContainsP(), 1));
                        return nodeDiv;
                    }
                }
                if(((NodeNumber)cleanNode2).Number == 1)
                {
                    return cleanNode1;
                }
            }          
            return null;
        }
    }
}
