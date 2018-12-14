using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CalculusApp
{
    class GraphDrawer
    {
        System.Windows.Forms.DataVisualization.Charting.Chart myChart;
        NodeHolder lastDrawnFunction;
        Series pointSerie;
        bool functionHasBeenDrawn;
        bool newtonHasBeenDrawn;
        bool rienmannHasBeenDrawn;
        int deltaX;
        double fromX;
        double toX;
        List<System.Drawing.Color> myColor = new List<System.Drawing.Color> { System.Drawing.Color.Blue, System.Drawing.Color.Gray, System.Drawing.Color.Red, System.Drawing.Color.Black, System.Drawing.Color.Orange, System.Drawing.Color.Green, System.Drawing.Color.Purple, System.Drawing.Color.Pink, System.Drawing.Color.DarkBlue, System.Drawing.Color.DarkGreen, System.Drawing.Color.Gold, System.Drawing.Color.GreenYellow, System.Drawing.Color.DodgerBlue, System.Drawing.Color.DarkOrange, System.Drawing.Color.Bisque };
        System.Drawing.Color newtonColor = System.Drawing.Color.Red;
        System.Drawing.Color functionColor = System.Drawing.Color.Blue;
        System.Drawing.Color RienmannColor = System.Drawing.Color.Green;

        public GraphDrawer(System.Windows.Forms.DataVisualization.Charting.Chart chart)
        {
            myChart = chart;
            SetAxis(10, 10);
        }
        public void SetAxis(int X, int Y) //NEGATIVE NUMBERS WILL BE IGNORED
        {
            bool axisChanged = false;
            if (X > 0)
            {
                myChart.ChartAreas[0].AxisX.Maximum = X;
                myChart.ChartAreas[0].AxisX.Minimum = -X;
                axisChanged = true;
            }
            if (Y > 0)
            {
                myChart.ChartAreas[0].AxisY.Maximum = Y;
                myChart.ChartAreas[0].AxisY.Minimum = -Y;
                axisChanged = true;
            }

            bool newton = newtonHasBeenDrawn;
            bool rienmann = rienmannHasBeenDrawn;
            if (axisChanged)
            {
                if (functionHasBeenDrawn)
                {
                    drawFunction(lastDrawnFunction, true);
                }
                if (newton)
                {
                    drawNewton(lastDrawnFunction);
                }
                if (rienmann)
                {
                    drawRienmann(lastDrawnFunction, deltaX, fromX, toX);
                }
            }
        }
        public void drawNewton(NodeHolder function)
        {
            //clearing previous function and setting up variables------------------------
            lastDrawnFunction = function;
            bool newSerieHasBeenUsed = false;
            bool yBetweenMaxAndMin = false;
            bool previousAnswerWasPossible = false;
            double xDistance = myChart.ChartAreas[0].AxisX.Maximum / 1000;
            if (true) //FAST RENDER = false OR ACCURATE RENDER = true
            {
                xDistance = 0.01f;
            }
            for (int i = 0; i < myChart.Series.Count; i++)
            {
                if (myChart.Series[i].Color == newtonColor)
                {
                    myChart.Series[i].Points.Clear();
                }
            }
            newtonHasBeenDrawn = false;
            int serieCounter = nextSerie(newtonColor, -1);

            //drawing the new function-------------------------------------------
            for (double i = myChart.ChartAreas[0].AxisX.Minimum; i <= myChart.ChartAreas[0].AxisX.Maximum; i += xDistance)
            {
                try
                {
                    double answer = 0;
                    answer = (function.GetFunctionOutputForX(i + 0.01f) - function.GetFunctionOutputForX(i)) / 0.01f;
                    if (answer >= myChart.ChartAreas[0].AxisY.Minimum && answer <= myChart.ChartAreas[0].AxisY.Maximum)
                    {
                        myChart.Series[serieCounter].Points.AddXY(i, answer);  //DRAW CURRENT          
                        if (previousAnswerWasPossible && !yBetweenMaxAndMin) //current point is between min/max, but previous was not, DRAW PREVIOUS
                        {
                            answer = (function.GetFunctionOutputForX(i - xDistance + 0.01f) - function.GetFunctionOutputForX(i - xDistance)) / 0.01f;
                            myChart.Series[serieCounter].Points.AddXY(i - xDistance, answer);
                        }
                        newSerieHasBeenUsed = true;
                        yBetweenMaxAndMin = true;
                    }
                    else if (yBetweenMaxAndMin) //previous answer was between max/min, DRAW ONE MORE AND START NEW SERIE
                    {
                        myChart.Series[serieCounter].Points.AddXY(i, answer);
                        serieCounter = nextSerie(newtonColor, serieCounter);
                        newSerieHasBeenUsed = false;
                        yBetweenMaxAndMin = false;
                    }
                    else
                    {
                        if (newSerieHasBeenUsed)
                        {
                            serieCounter = nextSerie(newtonColor, serieCounter);
                            newSerieHasBeenUsed = false;
                            yBetweenMaxAndMin = false;
                        }
                    }
                    previousAnswerWasPossible = true;
                }
                catch
                {
                    if (newSerieHasBeenUsed)
                    {
                        serieCounter = nextSerie(newtonColor, serieCounter);
                        newSerieHasBeenUsed = false;
                    }
                    previousAnswerWasPossible = false;
                }
            }
            newtonHasBeenDrawn = true;
            myChart.Invalidate();
        }
        private System.Drawing.Color newColor(System.Drawing.Color curColor)
        {
            if (myColor.Contains(curColor))
            {
                int index = myColor.IndexOf(curColor);
                if(index == myColor.Count - 1)
                {
                    index = 0;
                }
                else
                {
                    index++;
                }
                return myColor[index];
            }
            return myColor[0];
        }
        public void drawFunction(NodeHolder function, bool erase)
        {
            //clearing previous function and setting up variables------------------------
            lastDrawnFunction = function;
            int serieCounter;
            bool newSerieHasBeenUsed = false;
            bool yBetweenMaxAndMin = false;
            bool previousAnswerWasPossible = false;
            double xDistance = myChart.ChartAreas[0].AxisX.Maximum / 1000;
            if (true) //FAST RENDER = false OR ACCURATE RENDER = true
            {
                xDistance = 0.01f;
            }
            if (erase)
            {
                for (int i = 0; i < myChart.Series.Count; i++)
                {
                    if (myChart.Series[i].Color != System.Drawing.Color.Transparent && myChart.Series[i].ChartType == SeriesChartType.Line)
                    {
                        myChart.Series[i].Points.Clear();
                    }
                }
                functionHasBeenDrawn = false;
                newtonHasBeenDrawn = false;
                rienmannHasBeenDrawn = false;
                functionColor = System.Drawing.Color.Blue;
                
            }
            else
            {
                functionColor = newColor(functionColor);
            }
            serieCounter = nextSerie(functionColor, -1);

            //drawing the new function-------------------------------------------
            for (double i = myChart.ChartAreas[0].AxisX.Minimum; i <= myChart.ChartAreas[0].AxisX.Maximum; i += xDistance)
            {
                try
                {
                    double answer = 0;
                    answer = function.GetFunctionOutputForX(i);
                    if (answer >= myChart.ChartAreas[0].AxisY.Minimum && answer <= myChart.ChartAreas[0].AxisY.Maximum)
                    {
                        myChart.Series[serieCounter].Points.AddXY(i, answer);  //DRAW CURRENT          
                        if (previousAnswerWasPossible && !yBetweenMaxAndMin) //current point is between min/max, but previous was not, DRAW PREVIOUS
                        {
                            answer = function.GetFunctionOutputForX(i - xDistance);
                            myChart.Series[serieCounter].Points.AddXY(i - xDistance, answer);
                        }
                        newSerieHasBeenUsed = true;
                        yBetweenMaxAndMin = true;
                    }
                    else if (yBetweenMaxAndMin) //previous answer was between max/min, DRAW ONE MORE AND START NEW SERIE
                    {
                        myChart.Series[serieCounter].Points.AddXY(i, answer);
                        serieCounter = nextSerie(functionColor, serieCounter);
                        newSerieHasBeenUsed = false;
                        yBetweenMaxAndMin = false;
                    }
                    else
                    {
                        if (newSerieHasBeenUsed)
                        {
                            serieCounter = nextSerie(functionColor, serieCounter);
                            newSerieHasBeenUsed = false;
                            yBetweenMaxAndMin = false;
                        }
                    }
                    previousAnswerWasPossible = true;
                }
                catch
                {
                    if (newSerieHasBeenUsed)
                    {
                        serieCounter = nextSerie(functionColor, serieCounter);
                        newSerieHasBeenUsed = false;
                    }
                    previousAnswerWasPossible = false;
                }
            }
            functionHasBeenDrawn = true;
            myChart.Invalidate();
        } 
        public void drawRienmann(NodeHolder function, int deltaX, double fromX, double toX)
        {
            //clearing previous function and setting up variables------------------------
            lastDrawnFunction = function;
            this.fromX = fromX;
            this.toX = toX;
            this.deltaX = deltaX;
            bool newSerieHasBeenUsed = false;
            for (int i = 0; i < myChart.Series.Count; i++)
            {
                if (myChart.Series[i].Color == RienmannColor)
                {
                    myChart.Series[i].Points.Clear();
                }
            }
            rienmannHasBeenDrawn = false;
            int serieCounter = nextSerie(RienmannColor, -1);

            //drawing the new function-------------------------------------------
            double answerTemp;
            double totalDistance = (toX - fromX);
            double deltaDistance = totalDistance / deltaX; //niet nauwkeurig!!
            //double halfDeltaDistance = deltaDistance / 2;
            double XStartPosition;
            
            for (int i = 0; i < deltaX; i++)
            {
                //try
                //{
                    XStartPosition = fromX + i * deltaDistance;
                    answerTemp = function.GetFunctionOutputForX(fromX + (totalDistance + totalDistance * i * 2) / (deltaX * 2));

                    if(answerTemp > myChart.ChartAreas[0].AxisY.Maximum )
                    {
                        answerTemp = myChart.ChartAreas[0].AxisY.Maximum + Math.Abs( myChart.ChartAreas[0].AxisY.Maximum) * 0.1;
                    }
                    if(answerTemp < myChart.ChartAreas[0].AxisY.Minimum)
                    {
                        answerTemp = myChart.ChartAreas[0].AxisY.Minimum - Math.Abs( myChart.ChartAreas[0].AxisY.Minimum) * 0.1;
                    }

                    myChart.Series[serieCounter].Points.AddXY(XStartPosition, 0);
                    myChart.Series[serieCounter].Points.AddXY(XStartPosition, answerTemp);
                    myChart.Series[serieCounter].Points.AddXY(XStartPosition + deltaDistance, answerTemp);
                    myChart.Series[serieCounter].Points.AddXY(XStartPosition + deltaDistance, 0);
                    myChart.Series[serieCounter].Points.AddXY(XStartPosition, 0);
                //}
                //catch
                //{
                //    if (newSerieHasBeenUsed)
                //    {
                //        serieCounter = nextSerie(functionColor, serieCounter);
                //        newSerieHasBeenUsed = false;
                //    }
                //}
            }
            rienmannHasBeenDrawn = true;
        }
        private System.Windows.Forms.DataVisualization.Charting.Series createNewSerie(System.Drawing.Color color)
        {
            System.Windows.Forms.DataVisualization.Charting.Series newSerie = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "serie" + myChart.Series.Count,
                Color = color, //System.Drawing.Color.Blue
                IsVisibleInLegend = false,
                IsXValueIndexed = false,
                ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
            };
            newSerie.BorderWidth = 2;
            myChart.Series.Add(newSerie);
            return newSerie;
        }
        private int nextSerie(System.Drawing.Color color, int currentSerie)
        {
            currentSerie++;
            if(currentSerie >= myChart.Series.Count)
            {
                createNewSerie(color);
                return currentSerie;
            }
            if(myChart.Series[currentSerie].Color == color && myChart.Series[currentSerie].ChartType == SeriesChartType.Line)
            {
                return currentSerie;
            }
            else
            {
                return nextSerie(color, currentSerie);
            }
        } 
        public void DrawEmptyChart()
        {
            //for(int i = 0; i < myChart.Series.Count; i++) //erase all points
            //{
            //    myChart.Series[i].Points.Clear();
            //}
            int serieCounter = nextSerie(System.Drawing.Color.Transparent, -1); //get first/new trasnparent serie
            myChart.Series[serieCounter].Points.AddXY(-1, 0);
            myChart.Series[serieCounter].Points.AddXY(1, 0);
        } 
        public void DrawPolyPoint(double x, double y)
        {
            if(pointSerie == null)
            {
                Series newSerie = new Series
                {
                    Name = "serie" + myChart.Series.Count,
                    Color = newtonColor, //System.Drawing.Color.Blue
                    IsVisibleInLegend = false,
                    IsXValueIndexed = false,
                    ChartType = SeriesChartType.Point
                };
                newSerie.MarkerSize = 10;
                myChart.Series.Add(newSerie);
                pointSerie = newSerie;
            }
            pointSerie.Points.AddXY(x, y);

        }
        public void RemoveAllPolyPoints()
        {
            if(pointSerie != null)
            {
                pointSerie.Points.Clear();
            }
        }
    }
}
