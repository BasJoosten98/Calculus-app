using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculusApp
{
    class NodePower : Node
    {
        private Node node1;
        private Node node2;
        public override Node Node1 { get { return node1; } }
        public override Node Node2 { get { return node2; } }
        public NodePower()
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
            return Math.Pow(node1.GetValueForX(X), node2.GetValueForX(X));
        }
        public override string GetHumanReadableString()
        {
            if (node1 != null && node2 != null)
            {
                return "(" + node1.GetHumanReadableString() + ")^(" + node2.GetHumanReadableString() + ")";
            }
            return "";
        }
        public override string GetLocalString()
        {
            return "^( , )";
        }
        public override Node Clone()
        {
            Node newNode = new NodePower();
            Node newNode1 = node1.Clone();
            Node newNode2 = node2.Clone();
            newNode.AddNode1(newNode1);
            newNode.AddNode2(newNode2);
            return newNode;
        }
        public override Node GetDerivative() //O = original, D = derivative
        {
            Node node1O1 = node1.Clone();
            Node node1D = node1.GetDerivative();
            Node node2O1 = node2.Clone();
            Node nodeTimes1 = new NodeTimes();
            Node nodeTimes2 = new NodeTimes();
            if (node2.ContainsX())
            {
                Node thisNode = this.Clone();
                Node node1O2 = node1.Clone();
                Node node2D = node2.GetDerivative();
                Node nodePlus = new NodePlus();
                Node nodeLN = new NodeLN();
                Node nodeDiv = new NodeDivision();
                Node nodeTimes3 = new NodeTimes();

                nodeTimes3.AddNode1(thisNode);
                nodeTimes3.AddNode2(nodePlus);

                nodePlus.AddNode1(nodeTimes1);
                nodePlus.AddNode2(nodeTimes2);

                nodeTimes1.AddNode1(node1D);
                nodeTimes1.AddNode2(nodeDiv);

                nodeDiv.AddNode1(node2O1);
                nodeDiv.AddNode2(node1O1);

                nodeTimes2.AddNode1(node2D);
                nodeTimes2.AddNode2(nodeLN);

                nodeLN.AddNode1(node1O2);

                return nodeTimes3;
            }
            else
            {
                Node node2O2 = node2.Clone();
                Node nodeNumberOne = new NodeNumber(false, false, 1);
                Node nodeMin = new NodeMinus();
                Node nodePow = new NodePower();

                nodeTimes2.AddNode1(nodePow);
                nodeTimes2.AddNode2(nodeTimes1);

                nodeTimes1.AddNode1(node2O1);
                nodeTimes1.AddNode2(node1D);

                nodePow.AddNode1(node1O1);
                nodePow.AddNode2(nodeMin);

                nodeMin.AddNode1(node2O2);
                nodeMin.AddNode2(nodeNumberOne);

                return nodeTimes2;
            }

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
        private Node combineNodes(Node cleanNode1, Node cleanNode2, bool bothAreUpperNodes)
        {
            if (!bothAreUpperNodes)
            {
                if (!(cleanNode1 is NodeNumber) && cleanNode2 is NodeNumber)
                {
                    if (((NodeNumber)cleanNode2).Number == 0)
                    {
                        Node newNumberNode;
                        newNumberNode = new NodeNumber(false, false, 1);
                        return newNumberNode;
                    }
                }
                if (cleanNode1 is NodeNumber && cleanNode2 is NodeNumber)
                {
                    if (((NodeNumber)cleanNode2).Number == 0 && ((NodeNumber)cleanNode1).Number != 0)
                    {
                        Node newNumberNode;
                        newNumberNode = new NodeNumber(false, false, 1);
                        return newNumberNode;
                    }
                    else if (!cleanNode1.ContainsX() && !cleanNode2.ContainsX())
                    {
                        if (!cleanNode1.ContainsP() && !cleanNode2.ContainsP())
                        {
                            Node newNumberNode;
                            newNumberNode = new NodeNumber(false, false, Math.Pow(((NodeNumber)cleanNode1).Number, ((NodeNumber)cleanNode2).Number));
                            return newNumberNode;
                        }
                    }
                    else if (((NodeNumber)cleanNode2).Number == 1)
                    {
                        return cleanNode1;
                    }
                }
            }
            else //both are upper nodes, try to multiply time if possible (this is a copy of nodeTimes.combineNodes() method)
            {
                if (cleanNode1 is NodeNumber && cleanNode2 is NodeNumber)
                {

                    if (cleanNode1.ContainsX() != cleanNode2.ContainsX()) //one has X
                    {
                        if (cleanNode1.ContainsP() != cleanNode2.ContainsP()) //one has P
                        {
                            Node newNumberNode;
                            newNumberNode = new NodeNumber(true, true, ((NodeNumber)cleanNode1).Number * ((NodeNumber)cleanNode2).Number);
                            return newNumberNode;
                        }
                        else if (!cleanNode1.ContainsP()) //both have not P
                        {
                            Node newNumberNode;
                            newNumberNode = new NodeNumber(true, false, ((NodeNumber)cleanNode1).Number * ((NodeNumber)cleanNode2).Number);
                            return newNumberNode;
                        }
                    }
                    else if (!cleanNode1.ContainsX()) //both have not X
                    {
                        if (cleanNode1.ContainsP() != cleanNode2.ContainsP()) //one has P
                        {
                            Node newNumberNode;
                            newNumberNode = new NodeNumber(false, true, ((NodeNumber)cleanNode1).Number * ((NodeNumber)cleanNode2).Number);
                            return newNumberNode;
                        }
                        else if (!cleanNode1.ContainsP()) //both have not P
                        {
                            Node newNumberNode;
                            newNumberNode = new NodeNumber(false, false, ((NodeNumber)cleanNode1).Number * ((NodeNumber)cleanNode2).Number);
                            return newNumberNode;
                        }
                    }
                }
            }
            return null;
        }
        public override Node MakeNodeClean(Node prevNodeNumber, out Node replacementNode)
        {

            //Node receivedReplacementNode;
            //Node cleanNode1 = node1.MakeNodeClean(null, out receivedReplacementNode);
            //Node cleanNode2 = node2.MakeNodeClean(null, out receivedReplacementNode);
            //this.AddNode1(cleanNode1);
            //this.AddNode2(cleanNode2);
            //replacementNode = null;
            //return this;

            Node cleanNode1;
            Node cleanNode2;
            Node receivedReplacementNode;


            if (node1 is NodeNumber && node2 is NodeNumber) //both are numbers, can't go deeper in the tree!
            {
                Node combinedNode = combineNodes(node1, node2, false);
                if (combinedNode != null)
                {
                    replacementNode = null;
                    return combinedNode;
                }
                else
                {
                    if (prevNodeNumber != null)
                    {
                        //combinedNode = combineNodes(node1, prevNodeNumber);
                        //if (combinedNode != null)
                        //{
                        //    replacementNode = combinedNode;
                        //    return node2;
                        //}
                        combinedNode = combineNodes(node2, prevNodeNumber, true);
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
            } //not possible this part!--------------------------------------------------------------------------------------
            //if (node1 is NodeNumber) //node1 is a number, but node2 is not
            //{
            //    if (node2 is NodePlus) { cleanNode2 = node2.MakeNodeClean(node1, out receivedReplacementNode); }
            //    else { cleanNode2 = node2.MakeNodeClean(null, out receivedReplacementNode); }
            //    this.AddNode2(cleanNode2);
            //    if (receivedReplacementNode != null) //node1 has been combined in receivedReplacementNode
            //    {
            //        this.AddNode1(receivedReplacementNode);
            //        Node combinedNode1 = combineNodes(node1, node2);
            //        if (combinedNode1 != null)
            //        {
            //            replacementNode = null;
            //            return combinedNode1;
            //        }
            //        else
            //        {
            //            if (prevNodeNumber != null) //try to combine node1 with preNodeNumber
            //            {
            //                Node combinedNode2 = combineNodes(node1, prevNodeNumber);
            //                if (combinedNode2 != null)
            //                {
            //                    replacementNode = combinedNode2;
            //                    return node2;
            //                }
            //            }
            //            replacementNode = null;
            //            return this;
            //        }

            //    }
            //    else //node1 has not been combined in receivedReplacementNode
            //    {
            //        Node combinedNode1 = combineNodes(node1, node2);
            //        if (combinedNode1 != null)
            //        {
            //            replacementNode = null;
            //            return combinedNode1;
            //        }
            //        else
            //        {
            //            if (prevNodeNumber != null)//try to combine node1 with preNodeNumber
            //            {
            //                Node combinedNode2 = combineNodes(node1, prevNodeNumber);
            //                if (combinedNode2 != null)
            //                {
            //                    replacementNode = combinedNode2;
            //                    return node2;
            //                }
            //            }
            //            replacementNode = null;
            //            return this;
            //        }
            //    }
            //} not possible this part!--------------------------------------------------------------------------------------
            else if (node2 is NodeNumber) //node2 is a number, but node1 is not
            {
                if (node1 is NodePower) { cleanNode1 = node1.MakeNodeClean(node2, out receivedReplacementNode); }
                else { cleanNode1 = node1.MakeNodeClean(null, out receivedReplacementNode); }
                this.AddNode1(cleanNode1);
                if (receivedReplacementNode != null) //node2 has been combined in receivedReplacementNode
                {
                    this.AddNode2(receivedReplacementNode);
                    Node combinedNode1 = combineNodes(node1, node2, false);
                    if (combinedNode1 != null)
                    {
                        replacementNode = null;
                        return combinedNode1;
                    }
                    else
                    {
                        if (prevNodeNumber != null) //try to combine node1 with preNodeNumber
                        {
                            Node combinedNode = combineNodes(node2, prevNodeNumber, true);
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
                else //node2 has not been combined in receivedReplacementNode
                {
                    Node combinedNode1 = combineNodes(node1, node2, false);
                    if (combinedNode1 != null)
                    {
                        replacementNode = null;
                        return combinedNode1;
                    }
                    else
                    {
                        if (prevNodeNumber != null)//try to combine node1 with preNodeNumber
                        {
                            Node combinedNode = combineNodes(node2, prevNodeNumber, true);
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
            Node combinedNodeFinal = combineNodes(cleanNode1, cleanNode2, false);
            if (combinedNodeFinal != null)
            {
                replacementNode = null;
                return combinedNodeFinal;
            }
            replacementNode = null;
            return this;
        }
    }
}
