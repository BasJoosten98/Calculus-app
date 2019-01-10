using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculusApp
{
    class NodeMinus : Node
    {
        private Node node1;
        private Node node2;
        public override Node Node1 { get { return node1; } }
        public override Node Node2 { get { return node2; } }
        public NodeMinus()
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
            return node1.GetValueForX(X) - node2.GetValueForX(X);
        }
        public override string GetHumanReadableString()
        {
            if (node1 != null && node2 != null)
            {
                return node1.GetHumanReadableString() + " -(" + node2.GetHumanReadableString()+")";
            }
            return "";
        }
        public override string GetLocalString()
        {
            return "-( , )";
        }
        public override Node Clone()
        {
            Node newNode = new NodeMinus();
            Node newNode1 = node1.Clone();
            Node newNode2 = node2.Clone();
            newNode.AddNode1(newNode1);
            newNode.AddNode2(newNode2);
            return newNode;
        }
        public override Node GetDerivative() //O = original, D = derivative
        {
            Node nodeMin = new NodeMinus();
            Node node1D = node1.GetDerivative();
            Node node2D = Node2.GetDerivative();

            nodeMin.AddNode1(node1D);
            nodeMin.AddNode2(node2D);

            return nodeMin;
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
            else if (node1 is NodePlus)
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
        public override Node MakeNodeClean(Node prevNodeNumber, out Node replacementNode)
        {
            Node garbage;
            Node nodePlus = new NodePlus();
            nodePlus.AddNode1(node1);
            if(node2 is NodeNumber)
            {
                ((NodeNumber)node2).SwapPositivity();
                nodePlus.AddNode2(node2);
            }
            else if(node2 is NodeMinus)
            {
                Node newNode2 = node2.MakeNodeClean(null, out garbage); //node2 is now nodeplus
                ((NodePlus)newNode2).SwapPositivity();
                nodePlus.AddNode2(newNode2);
            }
            else
            {
                Node nodeTimes = new NodeTimes();
                Node nodeNumber = new NodeNumber(false, false, -1);
                nodeTimes.AddNode1(nodeNumber);
                nodeTimes.AddNode2(node2);
                nodePlus.AddNode2(nodeTimes);
            }
            replacementNode = null;
            nodePlus.MakeNodeClean(null, out garbage);
            return nodePlus;
        }
        public override bool SameAs(Node n)
        {
            if (n is NodeMinus)
            {
                bool temp = true;
                if (!n.Node1.SameAs(this.node1))
                {
                    temp = false;
                }
                if (!n.Node2.SameAs(this.node2))
                {
                    temp = false;
                }
                return temp;
            }
            return false;
        }
        //old combineNodes for minus
        //private Node combineNodes(Node cleanNode1, Node cleanNode2)
        //{
        //    if (cleanNode1 is NodeNumber && !(cleanNode2 is NodeNumber))
        //    {
        //        if (((NodeNumber)cleanNode1).Number == 0)
        //        {
        //            Node numberNode = new NodeNumber(false, false, -1);
        //            Node timesNode = new NodeTimes();
        //            timesNode.AddNode1(numberNode);
        //            timesNode.AddNode2(cleanNode2);
        //            return timesNode;
        //        }
        //    }
        //    if (cleanNode2 is NodeNumber && !(cleanNode1 is NodeNumber))
        //    {
        //        if (((NodeNumber)cleanNode2).Number == 0)
        //        {
        //            return cleanNode1;
        //        }
        //    }
        //    if (cleanNode1 is NodeNumber && cleanNode2 is NodeNumber)
        //    {
        //        if (((NodeNumber)cleanNode1).Number == 0)
        //        {
        //            ((NodeNumber)cleanNode2).SwapPositivity();
        //            return cleanNode2;
        //        }
        //        else if (((NodeNumber)cleanNode2).Number == 0)
        //        {
        //            return cleanNode1;
        //        }
        //        else if (cleanNode1.ContainsX() == cleanNode2.ContainsX())
        //        {
        //            if (cleanNode1.ContainsP() == cleanNode2.ContainsP())
        //            {
        //                Node newNumberNode;
        //                newNumberNode = new NodeNumber(cleanNode1.ContainsX(), cleanNode1.ContainsP(), ((NodeNumber)cleanNode1).Number - ((NodeNumber)cleanNode2).Number);
        //                return newNumberNode;
        //            }
        //        }
        //    }
        //    return null;
        //}
    }
}
