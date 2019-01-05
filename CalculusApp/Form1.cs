using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CalculusApp
{
    public partial class Form1 : Form
    {
        List<NodeHolder> myFunctions;
        GraphDrawer myDrawer;
        NodeHolder lastSelectedFunction;
        List<double> points;
        bool appIsDoneLoading;
        

        public Form1()
        {
            InitializeComponent();
            myFunctions = new List<NodeHolder>();
            myDrawer = new GraphDrawer(ChartFunction);
            myDrawer.DrawEmptyChart();
            points = new List<double>();
            appIsDoneLoading = true;
        }

        private void btnParseSum_Click(object sender, EventArgs e)
        {
            string sum = tbSum.Text;
            parseSum(sum);           
        }
        private void parseSum(string sum)
        {
            try
            {
                NodeHolder nh = new NodeHolder(sum);
                selectSum(nh);
                myFunctions.Add(nh);
                lbFunctions.Items.Add(nh);
                lbFunctions.SelectedIndex = lbFunctions.Items.IndexOf(nh);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Parsing failed: " + ex.Message);
            }
        } 
        private void selectSum(NodeHolder nh)
        {
            myDrawer.drawFunction(nh, true);
            lblHumanReadableString.Text = nh.GetHumanReadableString();
            pbNodeStructure.ImageLocation = nh.CreateNodeStructurePicture();
            lastSelectedFunction = nh;
        }
        private void btnCalculateForX_Click(object sender, EventArgs e)
        {
            if (lastSelectedFunction != null)
            {
                try
                {
                    double xValue = Convert.ToDouble(tbXValue.Text);
                    double answer = lastSelectedFunction.GetFunctionOutputForX(xValue);
                    lblCalculateForXAnswer.Text = answer.ToString();
                }
                catch
                {
                    lblCalculateForXAnswer.Text = "Not Possible!";
                }
            }
        }    
        private void btnSetAxis_Click(object sender, EventArgs e)
        {
            double xAxisFrom = 0;
            double yAxisFrom = 0;
            double xAxisTo = 0;
            double yAxisTo = 0;
            double number;
            bool axisSuccesful = true;

            //x axis check
            if(double.TryParse(tbXAxisFromValue.Text, out number))
            {
                xAxisFrom = number;
            }
            else { axisSuccesful = false; }
            if (double.TryParse(tbXAxisToValue.Text, out number))
            {
                xAxisTo = number;
            }
            else { axisSuccesful = false; }
            if (!axisSuccesful)
            {
                MessageBox.Show("X axis is in a wrong format or has not been fully filled in");
                return;
            }

            //y axis check
            if (double.TryParse(tbYAxisFromValue.Text, out number))
            {
                yAxisFrom = number;
            }
            else { axisSuccesful = false; }
            if (double.TryParse(tbYAxisToValue.Text, out number))
            {
                yAxisTo = number;
            }
            else { axisSuccesful = false; }
            if (!axisSuccesful)
            {
                MessageBox.Show("Y axis is in a wrong format or has not been fully filled in");
                return;
            }

            //changing the axis
            try
            {
                myDrawer.SetAxis(xAxisFrom, xAxisTo, yAxisFrom, yAxisTo);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Set axis failed: " + ex.Message);
            }
        }
        private void btnDerivativeByFunction_Click(object sender, EventArgs e)
        {
            if(lastSelectedFunction != null)
            {
                NodeHolder nh = lastSelectedFunction.GetDerivative();
                selectSum(nh);
                myFunctions.Add(nh);
                lbFunctions.Items.Add(nh);
                lbFunctions.SelectedIndex = lbFunctions.Items.IndexOf(nh);
            }
        }
        private void lbFunctions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbFunctions.SelectedIndex >= 0)
            {
                NodeHolder selected = (NodeHolder)lbFunctions.SelectedItem;
                selectSum(selected);
            }
            else
            {
                MessageBox.Show("No function has been selected");
            }
        }
        private void btnDerivativeByNewton_Click(object sender, EventArgs e)
        {
            if(lastSelectedFunction != null)
            {
                myDrawer.drawNewton(lastSelectedFunction);
            }
        }
        private void btnCalculateRienmann_Click(object sender, EventArgs e)
        {
            bool absolute = cbRienmannAbsolute.Checked;
            int deltaX;
            double toX = 0;
            double fromX = 0;

            if(lastSelectedFunction == null)
            {
                MessageBox.Show("Please select a function first");
                return;
            }
            if(!int.TryParse(tbDeltaX.Text, out deltaX))
            {
                MessageBox.Show("Delta X is in a wrong format");
                return;
            }
            if (tbToX.Text != "" || tbFromX.Text != "") 
            {
                try
                {
                    toX = Convert.ToDouble(tbToX.Text);
                    fromX = Convert.ToDouble(tbFromX.Text);
                }
                catch
                {
                    MessageBox.Show("To X OR From X is in a wrong format. Please fix this before continueing");
                    return;
                }                
            }
            else
            {
                if(points.Count != 4)
                {
                    if(points.Count == 0)
                    {
                        MessageBox.Show("Please draw 2 points or fill in To X and From X");
                    }
                    else
                    {
                        MessageBox.Show("Only 2 points should be drawn for this calculation (no more & no less). Or you can fill in To X and From X.");
                    }
                    return;
                }
                if(points[0] > points[2])
                {
                    toX = points[0];
                    fromX = points[2];
                }
                else if (points[0] < points[2])
                {
                    toX = points[2];
                    fromX = points[0];
                }
                else
                {
                    MessageBox.Show("Both points are located at the same x position, this is not allowed for this calculation");
                    return;
                }
            }

            try
            {
                string holder = lastSelectedFunction.RienmannIntegralValue(deltaX, fromX, toX, absolute).ToString();
                myDrawer.drawRienmann(lastSelectedFunction, deltaX, fromX, toX);
                lblRienmannAnswer.Text = holder;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Rienmann calculation failed: " + ex.Message);
            }
        }

        private void btnCreateMaclaurinSerieAccurate_Click(object sender, EventArgs e)
        {
            int order;
            if(!int.TryParse(tbOrder.Text, out order))
            {
                MessageBox.Show("Order needs to be an integer");
                return;
            }
            try
            {
                if (lastSelectedFunction != null)
                {
                    NodeHolder nh = lastSelectedFunction.GetMaclaurinSerieAccurate(order);
                    selectSum(nh);
                    myFunctions.Add(nh);
                    lbFunctions.Items.Add(nh);
                    lbFunctions.SelectedIndex = lbFunctions.Items.IndexOf(nh);
                }
                else { throw new Exception("No function has been selected"); }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error while making Maclaurin serie: " + ex.Message);
            }
        }

        private void btnDrawMaclaurinSeriesFast_Click(object sender, EventArgs e)
        {
            int order;
            if (!int.TryParse(tbOrder.Text, out order))
            {
                MessageBox.Show("Order needs to be an integer");
                return;
            }
            try
            {
                if (lastSelectedFunction != null)
                {
                    lastSelectedFunction.DrawMaclaurinSerieFast(myDrawer, order);
                }
                else { throw new Exception("No function has been selected"); }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while drawing Maclaurin serie: " + ex.Message);
            }
        }

        private void btnAddPointPoly_Click(object sender, EventArgs e)
        {
            double xValuePoly = 0;
            double yValuePoly = 0;
            string xValue = tbXValuePoly.Text;
            string yValue = tbYValuePoly.Text;

            if(!double.TryParse(xValue, out xValuePoly))
            {
                MessageBox.Show("X is in a wrong format");
                return;
            }
            if (!double.TryParse(yValue, out yValuePoly))
            {
                MessageBox.Show("Y is in a wrong format");
                return;
            }
            points.Add(xValuePoly);
            points.Add(yValuePoly);
            myDrawer.DrawPolyPoint(xValuePoly, yValuePoly);
            lblNumberOfPoints.Text = "Total Points: " + (points.Count / 2).ToString();
        }

        private void btnResetPointsPoly_Click(object sender, EventArgs e)
        {
            myDrawer.RemoveAllPolyPoints();
            points.Clear();
            lblNumberOfPoints.Text = "Total Points: " + (points.Count / 2).ToString();
        }

        private void btnCalculatePoly_Click(object sender, EventArgs e)
        {
            if(points.Count/2 >= 2)
            {
                try
                {
                    NodeHolder nh = new NodeHolder(points);
                    selectSum(nh);
                    myFunctions.Add(nh);
                    lbFunctions.Items.Add(nh);
                    lbFunctions.SelectedIndex = lbFunctions.Items.IndexOf(nh);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("you need atleast 2 points in order to calculate the polynomial");
            }
        }

        private void ChartFunction_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                ChartArea chartArea = ChartFunction.ChartAreas[0];
                double xValue = chartArea.AxisX.PixelPositionToValue(e.X);
                double yValue = chartArea.AxisY.PixelPositionToValue(e.Y);

                ChartMouseX.Text = "MouseX: " + xValue.ToString();
                ChartMouseY.Text = "MouseY: " + yValue.ToString();
            }
            catch
            {
                //do nothing
            }
        }

        private void ChartFunction_MouseDown(object sender, MouseEventArgs e)
        {
            string x = ChartMouseX.Text.Substring(8);
            string y = ChartMouseY.Text.Substring(8);

            double xValuePoly;
            double yValuePoly;
            double.TryParse(x, out xValuePoly);
            double.TryParse(y, out yValuePoly);

            points.Add(xValuePoly);
            points.Add(yValuePoly);
            myDrawer.DrawPolyPoint(xValuePoly, yValuePoly);
            lblNumberOfPoints.Text = "Total Points: " + (points.Count / 2).ToString();
        }
    }
}
