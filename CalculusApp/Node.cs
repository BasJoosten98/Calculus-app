using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculusApp
{
    abstract class Node
    {
        //fields
        private static int nextID = 1;
        private int id;
        protected Node parent;
        //properties
        public Node Parent { get { return parent; } }
        public virtual Node Node1 { get { return null; } }
        public virtual Node Node2 { get { return null; } }
        public int ID { get { return id; } }

        //methods
        public Node()
        {
            id = nextID;
            nextID++;
        }
        public abstract double GetValueForX(double X);
        public abstract string GetHumanReadableString();
        public virtual void AddNode1(Node n) { throw new Exception("addNode1 not implemented"); }
        public virtual void AddNode2(Node n) { throw new Exception("addNode2 not implemented"); }
        public void AddParentNode(Node n) { parent = n; }
        public abstract string GetLocalString();
        public abstract Node GetDerivative();
        public abstract Node Clone();
        public abstract bool ContainsX();
        public abstract bool ContainsP();
        public abstract Node MakeNodeClean(Node prevNodeNumber, out Node replacementNode);
        public abstract bool SameAs(Node n);
    }
}




        //old--------------------------------------------------------------

        //fields
//        string sumPart;
//        Node leftNode;
//        Node rightNode;
//        double numberSaved;
//        bool numberHasBeenSaved;
//        bool numberSavedHasX;

//        //properties
//        public string SumPart { get { return sumPart; } }
//        public Node LeftNode { get { return leftNode; } }
//        public Node RightNode { get { return rightNode; } }

//        //methods
//        public void addSumPart(string s)
//        {
//            this.sumPart = s;
//        }
//        public void addLeftNode(Node n)
//        {
//            this.leftNode = n;
//        }
//        public void addRightNode(Node n)
//        {
//            this.rightNode = n;
//        } 
//        private double getSavedNumber(float x)
//        {
//            if (numberHasBeenSaved)
//            {
//                if (numberSavedHasX)
//                {
//                    return numberSaved * x;
//                }
//                return numberSaved;
//            }
//            return 0;
//        }
//        public double GetValueForX(float x)
//        {
//            if (numberHasBeenSaved)
//            {
//                return getSavedNumber(x);
//            }
//            double leftNodeValue;
//            double rightNodeValue;

//            if (rightNode != null) //both leftnode and rightnode are knwon, so this node is a ACTION
//            {
//                leftNodeValue = leftNode.GetValueForX(x);
//                rightNodeValue = rightNode.GetValueForX(x);
//                switch (sumPart)
//                {
//                    case "+":
//                        return leftNodeValue + rightNodeValue;
//                    case "-":
//                        return leftNodeValue - rightNodeValue;
//                    case "*":
//                        return leftNodeValue * rightNodeValue;
//                    case "/":
//                        if (rightNodeValue == 0f)
//                        {
//                            throw new DivideByZeroException();
//                        }
//                        if (leftNodeValue != 0f) { 
//                            if (1 / Math.Abs(rightNodeValue) >= (1 / Math.Abs(leftNodeValue)) * 10000)
//                            {
//                                throw new DivideByZeroException();
//                            }
//                        }
//                        return leftNodeValue / rightNodeValue;
//                    case "^":
//                        return Math.Pow(leftNodeValue, rightNodeValue);
//                }
//                throw new Exception("Math error for: " + sumPart);
//            }
//            else if (leftNode != null) //only leftnode known, so this is a UNIQUE ACTION (sin, e, cos etc.)
//            {
//                leftNodeValue = leftNode.GetValueForX(x);
//                switch (sumPart)
//                {
//                    case "s":
//                        return Math.Sin(leftNodeValue);
//                    case "c":
//                        return Math.Cos(leftNodeValue);
//                    case "e":
//                        return Math.Pow(Math.E, leftNodeValue);
//                    case "l":
//                        if(leftNodeValue > 0) { return Math.Log10(leftNodeValue) / Math.Log10(Math.E); }
//                        else { throw new Exception("ln: input was not greater than 0"); }
//                    case "!":
//                        int nr;
//                        if(int.TryParse(leftNodeValue.ToString(),out nr))
//                        {
//                            return getFactorial(nr);
//                        }
//                        else
//                        {
//                            throw new Exception("Factorial: input was not a positive integer");
//                        }
//                }
//                throw new Exception("Math error for: " + sumPart);
//            }
//            else //both leftnode and rightnode are unknown, so this is a NUMBER
//            {                 
//                char[] NumberParts = sumPart.ToArray();
//                double Number = 0;
//                bool NumberIsSet = false;
//                bool piHasBeenUsed = false;
//                bool negativeNumber = false;
//                bool decimalNumber = false;
//                int decimalCounter = 0; //how many decimals in this number

//                for (int i = 0; i < NumberParts.Length; i++)
//                {
//                    switch (NumberParts[i])
//                    {
//                        case '-': //negative number
//                            if (i == 0) { negativeNumber = true; }
//                            else { } //IGNORE MINUS
//                            break;
//                        case '.': //decimal number
//                            if (decimalNumber) { } //IGNORE POINT
//                            else { decimalNumber = true; }
//                            break;
//                        case 'x': //consider this the last part
//                            if (NumberIsSet)
//                            {
//                                if(decimalNumber && decimalCounter > 0)
//                                {
//                                    Number = Number / Math.Pow(10, decimalCounter);
//                                }
//                                numberSaved = Number;
//                                numberHasBeenSaved = true;
//                                numberSavedHasX = true;
//                                return Number * x;
//                            }
//                            else  //only x in this node as sumpart
//                            {
//                                numberSaved = 1;
//                                numberHasBeenSaved = true;
//                                numberSavedHasX = true;
//                                return x;
//                            }

//                        case 'p':
//                            if (NumberIsSet)
//                            {
//                                Number = Math.PI * Number;
//                                piHasBeenUsed = true;
//                            }
//                            else 
//                            {
//                                Number = Math.PI;
//                                piHasBeenUsed = true;
//                            }
//                            NumberIsSet = true;
//                            break;
//                        default:
//                            if (!piHasBeenUsed)
//                            {
//                                int tempNumber;
//                                if (int.TryParse(NumberParts[i].ToString(), out tempNumber))
//                                {
//                                    if (!NumberIsSet)
//                                    {
//                                        if (negativeNumber) { Number = -tempNumber; }
//                                        else { Number = tempNumber; }
//                                        negativeNumber = false;
//                                    }
//                                    else
//                                    {
//                                        Number *= 10;
//                                        Number += tempNumber;
//                                    }
//                                    NumberIsSet = true;
//                                    if (decimalNumber)
//                                    {
//                                        decimalCounter++;
//                                    }
//                                }
//                            }
//                            break;
//                    }
//                }
//                //number with no X in it
//                if (decimalNumber && decimalCounter > 0) 
//                {
//                    numberSaved = Number / Math.Pow(10, decimalCounter);
//                    numberHasBeenSaved = true;
//                    return numberSaved;
//                }
//                numberSaved = Number;
//                numberHasBeenSaved = true;
//                return Number;
//            }
//        } 
//        private int getFactorial(int number)
//        {
//            if(number == 1 || number == 0)
//            {
//                return 1;
//            }
//            else if(number > 1)
//            {
//                return number * getFactorial(number - 1);
//            }
//            return -1;
//        }
//        public string HumanReadAbleSum()
//        {
//            string holder;
//            if(rightNode != null)
//            {
//                string leftHolder = LeftNode.HumanReadAbleSum();
//                string rightHolder = rightNode.HumanReadAbleSum();
//                switch (SumPart)
//                {
//                    case "+":
//                        return leftHolder + " + " + rightHolder;
//                    case "-":
//                        return leftHolder + " - " + rightHolder;
//                    case "*":
//                        return "(" + leftHolder + " * " + rightHolder + ")";
//                    case "/":
//                        return "(" + leftHolder + " / " + rightHolder + ")";
//                    case "^":
//                        return  leftHolder + "^(" + rightHolder + ")";
//                }
//                return "";
//            }
//            else if( leftNode != null)
//            {
//                string leftHolder = LeftNode.HumanReadAbleSum();
//                switch (sumPart)
//                {
//                    case "s":
//                        return "sin(" + leftHolder + ")";
//                    case "c":
//                        return "cos(" + leftHolder + ")";
//                    case "e":
//                        return "e^(" + leftHolder + ")";
//                    case "l":
//                        return "ln(" + leftHolder + ")";
//                    case "!":
//                        return "(" + leftHolder + ")!";
//                }
//                return "";
//            }
//            else
//            {
//                return sumPart;
//            }
//        } 
//    }

