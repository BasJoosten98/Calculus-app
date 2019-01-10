using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculusApp
{
    class NodeSin :Node
    {
        private Node node1;
        public override Node Node1 { get { return node1; } }
        public NodeSin()
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
            throw new Exception("sin doesn't need second parameter");
        }
        public override double GetValueForX(double X)
        {
            return Math.Sin(node1.GetValueForX(X));
        }
        public override string GetHumanReadableString()
        {
            if (node1 != null)
            {
                return "sin(" + node1.GetHumanReadableString() + ")";
            }
            return "";
        }
        public override string GetLocalString()
        {
            return "s( )";
        }
        public override Node Clone()
        {
            Node newNode = new NodeSin();
            Node newNode1 = node1.Clone();
            newNode.AddNode1(newNode1);
            return newNode;
        }
        public override Node GetDerivative() //O = original, D = derivative
        {
            Node node1O = node1.Clone();
            Node node1D = node1.GetDerivative();
            Node cosnode = new NodeCos();
            Node timesnode = new NodeTimes();

            timesnode.AddNode1(cosnode);
            timesnode.AddNode2(node1D);

            cosnode.AddNode1(node1O);

            return timesnode;
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
            Node receivedReplacementNode;
            Node cleanNode1 = node1.MakeNodeClean(null, out receivedReplacementNode);
            this.AddNode1(cleanNode1);
            replacementNode = null;
            return this;
        }
        public override bool SameAs(Node n)
        {
            if (n is NodeSin)
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
