using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculusApp
{
    class NodeNumber :Node
    {
        private double number;
        private bool containsX;
        private bool containsP;

        public double Number { get { return number; } }
        public NodeNumber(bool hasX, bool hasP, double nr)
        {
            number = nr;
            containsX = hasX;
            containsP = hasP;
        }
        public override void AddNode1(Node n)
        {
            throw new Exception("number doesn't need first parameter");
        }
        public override void AddNode2(Node n)
        {
            throw new Exception("number doesn't need second parameter");
        }
        public override double GetValueForX(double X)
        {
            double holder = number;
            if (containsP)
            {
                holder *= Math.PI;
            }
            if (containsX)
            {
                holder *= X;
            }
            return holder;
        }
        public override string GetHumanReadableString()
        {
            
            string holder = number.ToString();
            if(containsP || containsX)
            {
                if (number == 1) { holder = ""; }
                else if (number == -1) { holder = "-"; }
                if (containsP)
                {
                    holder += "π";
                }
                if (containsX)
                {
                    holder += "x";
                }
            }            
            return holder;
        }
        public override string GetLocalString()
        {
            return GetHumanReadableString();
        }
        public void SwapPositivity()
        {
            number = -number;
        }
        public override Node Clone()
        {
            Node newNode = new NodeNumber(containsX, containsP, number);      
            return newNode;
        }
        public override Node GetDerivative() //O = original, D = derivative
        {
            if (containsX)
            {
                return new NodeNumber(false, containsP, number);
            }
            else
            {
                return new NodeNumber(false, false, 0);
            }
        }
        public override bool ContainsX()
        {
            return containsX;
        }
        public override bool ContainsP()
        {
            return containsP;
        }
        public override Node MakeNodeClean(Node prevNodeNumber, out Node replacementNode)
        {
            replacementNode = null;
            return this;
        }
        public override bool SameAs(Node n)
        {
            if (n is NodeNumber)
            {
                if(n.ContainsX() == this.ContainsX() && n.ContainsP() == this.ContainsP() && ((NodeNumber)n).Number == this.number)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
