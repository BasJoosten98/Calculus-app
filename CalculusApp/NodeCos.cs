using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculusApp
{
    class NodeCos :Node
    {
        private Node node1;

        public override Node Node1 { get { return node1; } }
        public NodeCos()
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
            throw new Exception("cos doesn't need second parameter");
        }
        public override double GetValueForX(double X)
        {
            return Math.Cos(node1.GetValueForX(X));
        }
        public override string GetHumanReadableString()
        {
            if (node1 != null )
            {
                return "cos(" + node1.GetHumanReadableString() + ")";
            }
            return "";
        }
        public override string GetLocalString()
        {
            return "c( )";
        }
        public override Node Clone()
        {
            Node newNode = new NodeCos();
            Node newNode1 = node1.Clone();
            newNode.AddNode1(newNode1);
            return newNode;
        }
        public override Node GetDerivative() //O = original, D = derivative
        {
            Node negativenode = new NodeNumber(false, false, -1);
            Node node1O = node1.Clone();
            Node node1D = node1.GetDerivative();
            Node sinnode = new NodeSin();
            Node timesnode = new NodeTimes();
            Node timesnode2 = new NodeTimes();

            timesnode2.AddNode1(negativenode);
            timesnode2.AddNode2(timesnode);

            timesnode.AddNode1(sinnode);
            timesnode.AddNode2(node1D);

            sinnode.AddNode1(node1O);
                        
            return timesnode2;
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
            if(n is NodeCos)
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
