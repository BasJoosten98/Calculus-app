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

        public override Node Node1 { get { return node1; } }
        public NodeFactorial()
        {

        }
        public override void AddNode1(Node n)
        {
            if (n.ContainsX()) { throw new Exception("Factorial does not allow x values"); }
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
            //x value can be ignored
            string node1result = node1.GetValueForX(0).ToString();
            int number;
            if(!int.TryParse(node1result, out number))
            {
                throw new Exception("Math error: Factorial of negative integers or decimals are not allowed");
            }
            if(number < 0)
            {
                throw new Exception("Math error: Factorial of negative integers or decimals are not allowed");
            }
            return getFactorial(number);
        }
        private int getFactorial(int number)
        {
            if (number == 1 || number == 0)
            {
                return 1;
            }
            else if (number > 1)
            {
                return number * getFactorial(number - 1);
            }
            else
            {
                throw new Exception("Math error: Factorial of negative integers or decimals are not allowed");
            }
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
            if(node1 is NodeNumber)
            {
                string node1result = node1.GetValueForX(0).ToString();
                int number;
                if (!int.TryParse(node1result, out number))
                {
                    throw new Exception("Factorial of negative integers or decimals are not allowed");
                }
                if (number < 0)
                {
                    throw new Exception("Factorial of negative integers or decimals are not allowed");
                }
                number = getFactorial(number);
                if(number <= 9999)
                {
                    return new NodeNumber(false, false, number);
                }
            }
            return this;
        }
        public override bool SameAs(Node n)
        {
            if (n is NodeFactorial)
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
