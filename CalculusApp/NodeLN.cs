using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculusApp
{
    class NodeLN : Node
    {
        private Node node1;
        public override Node Node1 { get { return node1; } }
        public NodeLN()
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
            throw new Exception("LN doesn't need second parameter");
        }
        public override double GetValueForX(double X)
        {
            double input = node1.GetValueForX(X);
            if (input > 0) { return Math.Log(input); }
            else { throw new Exception("ln: input was not greater than 0"); }
        }
        public override string GetHumanReadableString()
        {
            if (node1 != null )
            {
                return "ln(" + node1.GetHumanReadableString() + ")";
            }
            return "";
        }
        public override string GetLocalString()
        {
            return "l( )";
        }
        public override Node Clone()
        {
            Node newNode = new NodeLN();
            Node newNode1 = node1.Clone();
            newNode.AddNode1(newNode1);
            return newNode;
        }
        public override Node GetDerivative() //O = original, D = derivative
        {
            Node nodeDiv = new NodeDivision();
            Node node1O = node1.Clone();
            Node node1D = node1.GetDerivative();

            nodeDiv.AddNode1(node1D);
            nodeDiv.AddNode2(node1O);

            return nodeDiv;
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

            //check if node is ln
            if (node1 is NodeE)
            {
                return node1.Node1;
            }
            if (node1 is NodeNumber)
            {
                if (!node1.ContainsP() && !node1.ContainsX() && ((NodeNumber)node1).Number == 1)
                {
                    return new NodeNumber(false, false, 0);
                }
            }
            return this;
        }
        public override bool SameAs(Node n)
        {
            if (n is NodeLN)
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
