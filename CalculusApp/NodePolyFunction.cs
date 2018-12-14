using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculusApp
{
    class NodePolyFunction
    {
        public double answer;
        public List<double> xValues;

        public NodePolyFunction(List<double> x, double ans)
        {
            answer = ans;
            xValues = x;
        }

        public void divideBy(double number)
        {
            for(int i = 0; i< xValues.Count; i++)
            {
                xValues[i] /= number;
            }
            answer /= number;
        }
        public void timesBy(double number)
        {
            for (int i = 0; i < xValues.Count; i++)
            {
                xValues[i] *= number;
            }
            answer *= number;
        }
        public void minusBy(double timesNumber, NodePolyFunction target)
        {
            NodePolyFunction targetClone = target.Clone();
            targetClone.timesBy(timesNumber);

            for (int i = 0; i < xValues.Count; i++)
            {
                xValues[i] -= targetClone.xValues[i];
            }
            answer -= targetClone.answer;
        }
        public NodePolyFunction Clone()
        {
            double ans = answer;
            List<double> x = new List<double>();
            for(int i = 0; i < xValues.Count; i++)
            {
                x.Add(xValues[i]);
            }
            NodePolyFunction newNode = new NodePolyFunction(x, ans);
            return newNode;
        }

    }
}
