using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculusApp
{
    class NodeFactorial : Node
    {
        private Node node1;

        public NodeFactorial()
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
            throw new Exception("Factorial doesn't need second parameter");
        }
        public override double GetValueForX(double X)
        {
            int number;
            if(int.TryParse(X.ToString(), out number))
            {
                if (number == 1 || number == 0)
                {
                    return 1;
                }
                else if (number > 1)
                {
                    return number * GetValueForX(number - 1);
                }
            }
            throw new Exception("Math error: Factorial of negative integers or decimals are not allowed");
        }
        public override string GetHumanReadableString()
        {
            if (node1 != null)
            {
                return "(" + node1.GetHumanReadableString() + ")!";
            }
            return "";
        }
        public override string GetLocalString()
        {
            return "!( )";
        }
        public override Node Clone()
        {
            Node newNode = new NodeFactorial();
            Node newNode1 = node1.Clone();
            newNode.AddNode1(newNode1);
            return newNode;
        }
        public override Node GetDerivative() //O = original, D = derivative
        {
            NodeNumber nodeNumber = new NodeNumber(false, false, 0);
            return nodeNumber;
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
    }
}
