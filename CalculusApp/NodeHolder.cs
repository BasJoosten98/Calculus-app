using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace CalculusApp
{
    class NodeHolder
    {
        //fields
        Node startNode;
        char[] enteredSum;
        char[] action1Parameter = new char[] { 's', 'c', 'e', 'l', '!' };
        char[] action2Parameter = new char[] { '+', '-', '*', '/', '^' };
        List<Node> allNodes = new List<Node>();

        
        //properties
        public string EnteredSum { get { return enteredSum.ToString(); } }

        //methods
        public NodeHolder(string Sum)
        {
            startNode = null;
            setNodes(Sum);
            CleanUpTree();
            CleanUpTree();
        }
        public NodeHolder(Node Startnode)
        {
            startNode = Startnode;          
            CleanUpTree();
            CleanUpTree();
        }
        public NodeHolder(List<double> polyPoints)
        {
            startNode = null;
            startNode = GetPolyNode(polyPoints);
            CleanUpTree();
            CleanUpTree();
        }
        private void setNodes(string Sum)
        {
            enteredSum = Sum.ToArray<char>();
            startNode = setNodesLoop();
            if (startNode == null) { throw new Exception("Nothing has been filled in"); }
            enteredSum = Sum.ToArray<char>();
        }
        private Node setNodesLoop() 
        {
            Node headNode = null;
            string tempNumber = null; //number in parameter (input)
            int parameter = 0; //parameter section
            bool negativeNumber = false;
            bool decimalNumber = false;
            bool tempNumberContainsX = false;
            bool tempNumberContainsP = false;

            while (enteredSum.Length != 0)
            {
                switch (enteredSum[0])
                {
                    case '(': //start of 1st parameter
                        if(headNode == null) { throw new Exception("No action found for ("); }
                        parameter = 1;
                        remove1stChar();
                        break;

                    case ')': //end of final parameter, CREATE FINAL NODE AND RETURN HEADNODE
                        if(headNode == null) { throw new Exception("No action found for )"); }
                        if(parameter == 1) //1st parameter input here---------------------
                        {
                            if (!action2Parameter.Contains(headNode.GetLocalString().ToArray<char>()[0])) {
                                if (headNode.Node1 == null)
                                {
                                    if (tempNumber != null) //input was a number
                                    {
                                        Node node1 = new NodeNumber(tempNumberContainsX, tempNumberContainsP, stringToDouble(tempNumber));
                                        headNode.AddNode1(node1);
                                        tempNumberContainsX = false;
                                        tempNumberContainsP = false;
                                        tempNumber = null;
                                        remove1stChar();
                                        return headNode;
                                    }
                                    else
                                    {
                                        throw new Exception("parameter 1 is missing for " + headNode.GetLocalString());
                                    }
                                }
                                else //action already has input
                                {
                                    remove1stChar();
                                    return headNode;
                                }
                            }
                            else
                            {
                                throw new Exception(headNode.GetLocalString() + " requires 2 parameters");
                            }
                        }
                        //2nd parameter input here---------------------
                        if (!action1Parameter.Contains(headNode.GetLocalString().ToArray<char>()[0]))
                        {
                            if (tempNumber != null && headNode.Node2 == null) //2nd parameter was a number
                            {
                                Node node2 = new NodeNumber(tempNumberContainsX, tempNumberContainsP, stringToDouble(tempNumber));
                                headNode.AddNode2(node2);
                                tempNumberContainsX = false;
                                tempNumberContainsP = false;
                                tempNumber = null;
                                remove1stChar();
                                return headNode;
                            }
                            else if (headNode.Node2 != null) //2nd parameter was a function
                            {
                                remove1stChar();
                                return headNode;
                            }
                            else
                            {
                                throw new Exception("parameter 2 is missing for " + headNode.GetLocalString());
                            }
                        }
                        else
                        {
                            throw new Exception(headNode.GetLocalString() + " requires 1 parameter, not 2");
                        }

                    case ',': //end of 1st parameter and start of 2nd parameter
                        if (headNode == null) { throw new Exception("No action found for ,"); }
                        if(parameter == 0) { throw new Exception("Parameter 1 for " + headNode.GetLocalString() + " has to be between ( and ,"); }
                        if(parameter == 2) { throw new Exception(headNode.GetLocalString() + " can not have more than 2 parameters"); }
                        if (tempNumber != null && headNode.Node1 == null) //1st parameter was a number
                        {
                            Node node1 = new NodeNumber(tempNumberContainsX, tempNumberContainsP, stringToDouble(tempNumber));
                            headNode.AddNode1(node1);
                            tempNumberContainsX = false;
                            tempNumberContainsP = false;
                            tempNumber = null;
                            remove1stChar();
                        }
                        else if(headNode.Node1 != null) //1st parameter was a action
                        {
                            remove1stChar();
                        }
                        else
                        {
                            throw new Exception("parameter 1 is missing for " + headNode.GetLocalString());
                        }
                        parameter = 2;
                        break;
                    case '.': //point for decimal numbers
                        if (decimalNumber) { throw new Exception("A number can not have more than 1 dot"); }
                        decimalNumber = true;
                        remove1stChar();
                        break;
                    case 'x':
                        if (tempNumberContainsX) { throw new Exception("A number can only have one x"); }
                        if (tempNumber != null)
                        {
                            tempNumberContainsX = true;
                        }
                        else
                        {
                            if (negativeNumber) { tempNumber = "-1"; tempNumberContainsX = true; }
                            else { tempNumber = "1"; tempNumberContainsX = true; }
                            negativeNumber = false;
                        }
                        remove1stChar();
                        break;

                    case 'p': //pi
                        if (tempNumberContainsP) { throw new Exception("A number can only have one p"); }
                        if (tempNumber != null)
                        {
                            tempNumberContainsP = true;
                        }
                        else
                        {
                            if (negativeNumber) { tempNumber = "-1"; tempNumberContainsP = true; }
                            else { tempNumber = "1"; tempNumberContainsP = true; }
                            negativeNumber = false;
                        }
                        remove1stChar();
                        break;

                    default: //it's must be a number OR an action                
                        int number;
                        if (int.TryParse(enteredSum[0].ToString(), out number)) //NUMBER
                        {
                            if (tempNumberContainsP || tempNumberContainsX)
                            {
                                throw new Exception("No numbers can be placed after PI or X");
                            }
                            else //You can add the number
                            {
                                if (tempNumber != null) //tempnumber already has a value
                                {
                                    if (decimalNumber)
                                    {
                                        if (tempNumber.Contains(".")) { throw new Exception("A number can not have more than 1 dot"); }
                                        tempNumber += "." + number.ToString();
                                        decimalNumber = false;
                                    }
                                    else
                                    {
                                        tempNumber += number.ToString();
                                    }
                                }
                                else //tempnumber is not set yet
                                {
                                    if (decimalNumber)
                                    {
                                        if (negativeNumber) { tempNumber = "-0." + number.ToString(); }
                                        else { tempNumber = "0." + number.ToString(); }
                                        decimalNumber = false;
                                    }
                                    else
                                    {
                                        if (negativeNumber) { tempNumber = "-" + number.ToString(); }
                                        else { tempNumber = number.ToString(); }
                                    }
                                    negativeNumber = false;
                                }
                                remove1stChar();
                            }
                        }
                        else if (action1Parameter.Contains(enteredSum[0]) || action2Parameter.Contains(enteredSum[0])) //ACTION 
                        {
                            if (enteredSum[0] == '-' && (int.TryParse(enteredSum[1].ToString(), out number) || enteredSum[1] == 'p' || enteredSum[1] == 'x')) //negative number check
                            {
                                    negativeNumber = true;
                                    remove1stChar();
                            }
                            else if (parameter == 0)
                            {
                                headNode = getNodeForChar(enteredSum[0]);
                                remove1stChar();
                            }
                            else if (parameter == 1)
                            {
                                Node node1 = setNodesLoop();
                                headNode.AddNode1(node1);
                            }
                            else if (parameter == 2)
                            {
                                Node node2 = setNodesLoop();
                                headNode.AddNode2(node2);
                            }
                        }
                        else //SOMETHING UNKNOWN/USELESS
                        {
                            remove1stChar();
                        }
                        break;
                }
            }
            if(parameter == 0 && tempNumber != null && headNode == null) //function was only a number
            {            
                headNode = new NodeNumber(tempNumberContainsX, tempNumberContainsP, stringToDouble(tempNumber));
                return headNode;
            }
            if(headNode != null) //node hasn't been closed 
            {
                throw new Exception("Node is incomplete for " + headNode.GetLocalString());
            }
            return null;
        }
        private double stringToDouble(string temp)
        {
            try
            {
                return Convert.ToDouble(temp);
            }
            catch
            {
                throw new Exception("Could not read all numbers correctly");
            }
        } 
        private Node getNodeForChar(char c)
        {
            switch (c)
            {
                case '+':
                    return new NodePlus();
                case '-':
                    return new NodeMinus();
                case '/':
                    return new NodeDivision();
                case '*':
                    return new NodeTimes();
                case 's':
                    return new NodeSin();
                case 'c':
                    return new NodeCos();
                case 'l':
                    return new NodeLN();
                case '!':
                    return new NodeFactorial();
                case 'e':
                    return new NodeE();
                case '^':
                    return new NodePower();
            }
            throw new Exception("unknown character for node detected");
        }
        private void remove1stChar()
        {
            char[] newSum = new char[enteredSum.Length - 1];
            for(int i = 0; i < newSum.Length; i++)
            {
                newSum[i] = enteredSum[i + 1];
            }
            enteredSum = newSum;
        }
        public double GetFunctionOutputForX(double X)
        {
            return startNode.GetValueForX(X);
        }
        public string GetHumanReadableString()
        {
            return startNode.GetHumanReadableString();
        }
        public string CreateNodeStructurePicture()
        {
            //create list of nodes (for number values)
            allNodes.Clear();
            createListAllNodes(startNode);

            FileStream fs;
            StreamWriter sw;
            string fileName = "abc.dot";
            try
            {
                fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                sw = new StreamWriter(fs);
                sw.WriteLine("graph calculus {");
                sw.WriteLine("node [ fontname = \"Arial\" ]");

                for(int i = 0; i < allNodes.Count; i++)
                {
                    sw.WriteLine("node" + (allNodes[i].ID) + " [ label = \"" + allNodes[i].GetLocalString() + "\" ]");
                    //if (allNodes[i].Parent != null)
                    //{
                    //    sw.WriteLine("node" + (allNodes[i].Parent.ID) + " -- node" + (allNodes[i].ID));
                    //}
                    if(allNodes[i].Node1 != null)
                    {
                        sw.WriteLine("node" + (allNodes[i].ID) + " -- node" + (allNodes[i].Node1.ID));
                    }
                    if (allNodes[i].Node2 != null)
                    {
                        sw.WriteLine("node" + (allNodes[i].ID) + " -- node" + (allNodes[i].Node2.ID));
                    }

                }
                sw.WriteLine("}");
                sw.Close();
                
            }
            catch (IOException)
            {
                throw new Exception("Structure picture failed");
            }
            return CreateDotPNG(fileName);
        }
        private string CreateDotPNG(string filename)
        {
            Process dot = new Process();
            dot.StartInfo.FileName = "dot.exe";
            dot.StartInfo.Arguments = "-Tpng -oabc.png abc.dot";
            dot.Start();
            dot.WaitForExit();
            return "abc.png";

        }
        private void createListAllNodes(Node curNode)
        {
            if (curNode.Parent != null)
            {
                if (curNode.ID == curNode.Parent.Node1.ID || curNode.ID == curNode.Parent.Node2.ID)
                {
                    if (!allNodes.Contains(curNode))
                    {
                        
                    }
                    
                }
                allNodes.Add(curNode);
            }
            else
            {
                if (!allNodes.Contains(curNode))
                {
                    
                }
                allNodes.Add(curNode);
            }

            if (curNode.Node1 != null)
            {
                createListAllNodes(curNode.Node1);
            }
            if(curNode.Node2 != null)
            {
                createListAllNodes(curNode.Node2);
            }
            
        }
        public override string ToString()
        {
            return startNode.GetHumanReadableString();
        }
        private void CleanUpTree()
        {
            Node receivedReplacementNode;
            Node cleanStartNode = startNode.MakeNodeClean(null, out receivedReplacementNode);
            cleanStartNode.AddParentNode(null);
            startNode = cleanStartNode;
        }
        public double RienmannIntegralValue(int deltaX, double fromX, double toX, bool absolute)
        {
            if (toX <= fromX)
            {
                throw new Exception("To X must be larger than From X");
            }
            if(deltaX <= 0)
            {
                throw new Exception("Delta X must be larger than 0");
            }
            double answer = 0;
            double answerTemp;
            double totalDistance = (toX - fromX);
            double deltaDistance = totalDistance / deltaX; //niet nauwkeurig!!
            //double halfDeltaDistance = deltaDistance / 2;
                for (int i = 0; i < deltaX; i++)
                {
                    answerTemp = GetFunctionOutputForX(fromX + (totalDistance + totalDistance*i*2)/(deltaX*2)) * deltaDistance;
                    if (absolute) { answerTemp = Math.Abs(answerTemp); }
                    answer += answerTemp;
                }
            

            return answer;
        } 
        public NodeHolder GetDerivative()
        {
            Node StartNode = startNode.GetDerivative();
            NodeHolder nh = new NodeHolder(StartNode);
            return nh;
        }
        public void DrawMaclaurinSerieFast(GraphDrawer drawer, int order)
        {
            if(order < 1) { throw new Exception("order must be larger than 0"); }
            double[] MaclaurinValues = makeMaclaurinList(0, order, null, 0.02);
            drawMaclaurinSerieFastRecursive(1, order, MaclaurinValues, drawer, null);
        }
        private Node drawMaclaurinSerieFastRecursive(int counter, int order, double[] maclaurinValues, GraphDrawer drawer, Node prevCompletedNode)
        {
            NodeHolder nh;
            Node nodeTim = new NodeTimes();
            Node nodeDiv = new NodeDivision();
            Node nodeFac = new NodeFactorial();
            Node nodePow = new NodePower();
            double zeroValue = maclaurinValues[counter - 1];

            nodeTim.AddNode1(nodeDiv);
            nodeTim.AddNode2(nodePow);
            nodeDiv.AddNode1(new NodeNumber(false, false, zeroValue));
            nodeDiv.AddNode2(nodeFac);
            nodeFac.AddNode1(new NodeNumber(false, false, counter - 1));
            nodePow.AddNode1(new NodeNumber(true, false, 1)); //X
            nodePow.AddNode2(new NodeNumber(false, false, counter - 1));

            if (counter == order)
            {
                Node nodePlus = new NodePlus();
                if (prevCompletedNode != null)
                {
                    nodePlus.AddNode1(nodeTim);
                    nodePlus.AddNode2(prevCompletedNode);
                    prevCompletedNode = nodePlus.Clone();
                    nh = new NodeHolder(prevCompletedNode);
                }
                else
                {
                    prevCompletedNode = nodeTim;
                    nh = new NodeHolder(prevCompletedNode);
                }
                if (counter == 1) { drawer.drawFunction(nh, true); }
                else { drawer.drawFunction(nh, false); }
                return nodeTim;
            }
            else if (counter < order && counter > 0)
            {
                Node nodePlus = new NodePlus();
                if (prevCompletedNode != null)
                {
                    nodePlus.AddNode1(nodeTim);
                    nodePlus.AddNode2(prevCompletedNode);
                    prevCompletedNode = nodePlus.Clone();
                    nh = new NodeHolder(prevCompletedNode);
                }
                else
                {
                    prevCompletedNode = nodeTim;
                    nh = new NodeHolder(prevCompletedNode);
                }
                if(counter == 1) { drawer.drawFunction(nh, true); }
                else { drawer.drawFunction(nh, false); }
                nodePlus.AddNode1(nodeTim);
                nodePlus.AddNode2(drawMaclaurinSerieFastRecursive(counter + 1, order, maclaurinValues, drawer, prevCompletedNode));                
                return nodePlus;
            }
            else
            {
                throw new Exception("Error while making Maclaurin serie: counter problem: " + counter);
            }
        }
        private double[] makeMaclaurinList(int counter, int order, double[] prevResults, double h)
        {
            double[] newPrevResults;
            double[] futureResults;
            if (prevResults == null)
            {
                double tempSum;
                double tempSumSave = 0; //saves the first one with division by h (is the one used for drawing!)
                newPrevResults = new double[order];
                for(int i = 0; i < order; i++)
                {
                    tempSum = startNode.GetValueForX(0 + i * h);
                    newPrevResults[i] = tempSum;
                    if (i == 0) { tempSumSave = tempSum / Math.Pow(h, counter); }
                }
                futureResults = makeMaclaurinList(counter + 1, order, newPrevResults, h);
                futureResults[counter] = tempSumSave; //saving the one used for drawing
                return futureResults;
            }
            else if(order - counter >= 1)
            {
                double tempSum;
                double tempSumSave = 0; //saves the first one with division by h (is the one used for drawing!)
                newPrevResults = new double[order - counter];
                for(int i = 0; i < (order - counter); i++)
                {
                    tempSum = (prevResults[i+1] - prevResults[i]);
                    newPrevResults[i] = tempSum;
                    if(i == 0) { tempSumSave = tempSum / Math.Pow(h, counter); }
                }
                futureResults = makeMaclaurinList(counter + 1, order, newPrevResults, h);
                futureResults[counter] = tempSumSave; //saving the one used for drawing
                return futureResults;
            }
            else if(order == counter && order > 0)
            {
                return new double[order];
            }
            else
            {
                throw new Exception("counter/order problem: counter: " + counter + ", order: " + order);
            }

        } 
        public NodeHolder GetMaclaurinSerieAccurate(int order)
        {
            if(order <= 0)
            {
                throw new Exception("Order must be larger than 0");
            }
            Node MaclaurinNode = getMaclaurinSerieRecursiveAccurate(1, order, startNode);
            NodeHolder MaclaurinNodeHolder = new NodeHolder(MaclaurinNode);
            return MaclaurinNodeHolder;
        }
        private Node getMaclaurinSerieRecursiveAccurate(int counter, int order, Node curNode)
        {
            Node nodeTim = new NodeTimes();
            Node nodeDiv = new NodeDivision();
            Node nodeFac = new NodeFactorial();
            Node nodePow = new NodePower();
            double zeroValue = curNode.GetValueForX(0);

            nodeTim.AddNode1(nodeDiv);
            nodeTim.AddNode2(nodePow);
            nodeDiv.AddNode1(new NodeNumber(false, false, zeroValue));
            nodeDiv.AddNode2(nodeFac);
            nodeFac.AddNode1(new NodeNumber(false, false, counter - 1));
            nodePow.AddNode1(new NodeNumber(true, false, 1));
            nodePow.AddNode2(new NodeNumber(false, false, counter - 1));

            if (counter == order)
            {
                return nodeTim;
            }
            else if(counter < order && counter > 0)
            {
                Node nodePlus = new NodePlus();
                nodePlus.AddNode1(nodeTim);
                curNode = curNode.GetDerivative();
                Node receivedReplacementNode;
                curNode = curNode.MakeNodeClean(null, out receivedReplacementNode);
                nodePlus.AddNode2(getMaclaurinSerieRecursiveAccurate(counter + 1, order, curNode));
                return nodePlus;
            }
            else
            {
                throw new Exception("Error while making Maclaurin serie: counter problem: " + counter);
            }
        }
        private Node GetPolyNode(List<double> polyPoints)
        {
            //create polyfunction nodes
            List<NodePolyFunction> polyFunctions = new List<NodePolyFunction>();
            int totalPoints = polyPoints.Count / 2;
            int nOrder = totalPoints - 1;

            for(int i = 0; i < polyPoints.Count; i += 2) //each point
            {
                double ans = polyPoints[i + 1];
                List<double> xValues = new List<double>();

                for(int j = nOrder; j >= 0; j-- ) //each x value
                {
                    double x = polyPoints[i];
                    if(x == 0) { x = 0.000000001; }
                    xValues.Add(Math.Pow(x, j));
                }

                NodePolyFunction newNode = new NodePolyFunction(xValues, ans);
                polyFunctions.Add(newNode);

            }
            
            //transform polyfunctions into ideal functions (1a + 0 + 0 = ?, 0 + 1b + 0 = ? etc.)
            for(int i = 0; i < polyFunctions.Count; i++) //make current x value equal to 1 of this function
            {
                double divNumber;
                divNumber = polyFunctions[i].xValues[i];

                polyFunctions[i].divideBy(divNumber);

                for(int j = 0; j < polyFunctions.Count; j++)//make current x value equal to 0 of other functions
                {
                    if(j != i)
                    {
                        double timesNumber;
                        timesNumber = polyFunctions[j].xValues[i];

                        polyFunctions[j].minusBy(timesNumber, polyFunctions[i]);
                    }
                }
            }

            //make the return node out of the ideal (poly) functions
            Node headNode = new NodePlus();
            Node tempNode = headNode;
            for(int i = 0; i < polyFunctions.Count(); i++)
            {
                Node nodePow = new NodePower();
                Node nodeTim = new NodeTimes();
                Node nodeNum = new NodeNumber(false, false, polyFunctions[i].answer);
                Node nodeNumLow = new NodeNumber(true, false, 1);
                Node nodeNumUp = new NodeNumber(false, false, nOrder - i);

                nodeTim.AddNode1(nodeNum);
                nodeTim.AddNode2(nodePow);

                nodePow.AddNode1(nodeNumLow);
                nodePow.AddNode2(nodeNumUp);

                if (i <= polyFunctions.Count - 3) //normal
                {
                    tempNode.AddNode1(nodeTim);
                    Node nodePlus = new NodePlus();
                    tempNode.AddNode2(nodePlus);
                    tempNode = nodePlus;
                }
                else if(i <= polyFunctions.Count - 2) //one before last one
                {
                    tempNode.AddNode1(nodeTim);
                }
                else if(i <= polyFunctions.Count - 1) //last one
                {
                    tempNode.AddNode2(nodeTim);
                }
            }
            return headNode;
        }
    }
}
