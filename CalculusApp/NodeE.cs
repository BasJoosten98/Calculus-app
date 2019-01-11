using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculusApp
{
    class NodeE : Node
    {
        private Node node1;

        public override Node Node1 { get { return node1; } }
        public NodeE()
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
            throw new Exception("E doesn't need second parameter");
        }
        public override double GetValueForX(double X)
        {
            return Math.Pow(Math.E, node1.GetValueForX(X));
        }
        public override string GetHumanReadableString()
        {
            if (node1 != null)
            {
                return "e^(" + node1.GetHumanReadableString() + ")";
            }
            return "";
        }
        public override string GetLocalString()
        {
            return "e( )";
        }
        public override Node Clone()
        {
            Node newNode = new NodeE();
            Node newNode1 = node1.Clone();
            newNode.AddNode1(newNode1);
            return newNode;
        }
        public override Node GetDerivative() //O = original, D = derivative
        {
            Node node1O = node1.Clone();
            Node node1D = node1.GetDerivative();
            Node nodeE = new NodeE();
            Node nodeTimes = new NodeTimes();

            nodeTimes.AddNode1(nodeE);
            nodeTimes.AddNode2(node1D);

            nodeE.AddNode1(node1O);

            return nodeTimes;
        }
        public override bool ContainsX()
        {
            bool holder = false;
            if (node1.ContainsX()) { holder = true; }
            return holder;
        }
        public override bool ContainsP()
        {
            bool holder = false;
            if (node1.ContainsP()) { holder = true; }
            return holder;
        }
        public override Node MakeNodeClean(Node prevNodeNumber, out Node replacementNode)
        {
            //clean the node
            Node receivedReplacementNode;
            Node cleanNode1 = node1.MakeNodeClean(null, out receivedReplacementNode);
            this.AddNode1(cleanNode1);
            replacementNode = null;

            //check if node is ln
            if(node1 is NodeLN)
            {
                return node1.Node1;
            }
            if(node1 is NodeNumber)
            {
                if(!node1.ContainsP() && !node1.ContainsX() && ((NodeNumber)node1).Number == 0)
                {
                    return new NodeNumber(false, false, 1);
                }
            }
            return this;
        }
        public override bool SameAs(Node n)
        {
            if (n is NodeE)
            {
                if (n.Node1.SameAs(this.node1))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
