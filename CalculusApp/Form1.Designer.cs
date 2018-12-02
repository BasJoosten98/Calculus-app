﻿namespace CalculusApp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            this.tbSum = new System.Windows.Forms.TextBox();
            this.btnParseSum = new System.Windows.Forms.Button();
            this.tbXValue = new System.Windows.Forms.TextBox();
            this.btnCalculateForX = new System.Windows.Forms.Button();
            this.lblCalculateForXAnswer = new System.Windows.Forms.Label();
            this.ChartFunction = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tbXAxisValue = new System.Windows.Forms.TextBox();
            this.btnSetAxis = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbYAxisValue = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pbNodeStructure = new System.Windows.Forms.PictureBox();
            this.btnDerivativeByFunction = new System.Windows.Forms.Button();
            this.btnDerivativeByNewton = new System.Windows.Forms.Button();
            this.lbFunctions = new System.Windows.Forms.ListBox();
            this.gbSetAxis = new System.Windows.Forms.GroupBox();
            this.gbDerivative = new System.Windows.Forms.GroupBox();
            this.gbRienmannIntegral = new System.Windows.Forms.GroupBox();
            this.cbRienmannAbsolute = new System.Windows.Forms.CheckBox();
            this.lblRienmannAnswer = new System.Windows.Forms.Label();
            this.btnCalculateRienmann = new System.Windows.Forms.Button();
            this.tbToX = new System.Windows.Forms.TextBox();
            this.lblToX = new System.Windows.Forms.Label();
            this.tbFromX = new System.Windows.Forms.TextBox();
            this.lblFromX = new System.Windows.Forms.Label();
            this.tbDeltaX = new System.Windows.Forms.TextBox();
            this.lblDeltaX = new System.Windows.Forms.Label();
            this.lblHumanReadableString = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ChartFunction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNodeStructure)).BeginInit();
            this.gbSetAxis.SuspendLayout();
            this.gbDerivative.SuspendLayout();
            this.gbRienmannIntegral.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbSum
            // 
            this.tbSum.Location = new System.Drawing.Point(127, 8);
            this.tbSum.Name = "tbSum";
            this.tbSum.Size = new System.Drawing.Size(316, 20);
            this.tbSum.TabIndex = 0;
            // 
            // btnParseSum
            // 
            this.btnParseSum.Location = new System.Drawing.Point(12, 6);
            this.btnParseSum.Name = "btnParseSum";
            this.btnParseSum.Size = new System.Drawing.Size(95, 23);
            this.btnParseSum.TabIndex = 1;
            this.btnParseSum.Text = "Parse Sum";
            this.btnParseSum.UseVisualStyleBackColor = true;
            this.btnParseSum.Click += new System.EventHandler(this.btnParseSum_Click);
            // 
            // tbXValue
            // 
            this.tbXValue.Location = new System.Drawing.Point(127, 37);
            this.tbXValue.Name = "tbXValue";
            this.tbXValue.Size = new System.Drawing.Size(100, 20);
            this.tbXValue.TabIndex = 2;
            // 
            // btnCalculateForX
            // 
            this.btnCalculateForX.Location = new System.Drawing.Point(12, 35);
            this.btnCalculateForX.Name = "btnCalculateForX";
            this.btnCalculateForX.Size = new System.Drawing.Size(94, 23);
            this.btnCalculateForX.TabIndex = 3;
            this.btnCalculateForX.Text = "Calculate for x";
            this.btnCalculateForX.UseVisualStyleBackColor = true;
            this.btnCalculateForX.Click += new System.EventHandler(this.btnCalculateForX_Click);
            // 
            // lblCalculateForXAnswer
            // 
            this.lblCalculateForXAnswer.AutoSize = true;
            this.lblCalculateForXAnswer.Location = new System.Drawing.Point(249, 40);
            this.lblCalculateForXAnswer.Name = "lblCalculateForXAnswer";
            this.lblCalculateForXAnswer.Size = new System.Drawing.Size(42, 13);
            this.lblCalculateForXAnswer.TabIndex = 4;
            this.lblCalculateForXAnswer.Text = "Answer";
            // 
            // ChartFunction
            // 
            chartArea2.Name = "ChartArea1";
            this.ChartFunction.ChartAreas.Add(chartArea2);
            this.ChartFunction.Location = new System.Drawing.Point(13, 84);
            this.ChartFunction.Name = "ChartFunction";
            this.ChartFunction.Size = new System.Drawing.Size(455, 310);
            this.ChartFunction.TabIndex = 5;
            this.ChartFunction.Text = "chart1";
            // 
            // tbXAxisValue
            // 
            this.tbXAxisValue.Location = new System.Drawing.Point(33, 19);
            this.tbXAxisValue.Name = "tbXAxisValue";
            this.tbXAxisValue.Size = new System.Drawing.Size(41, 20);
            this.tbXAxisValue.TabIndex = 7;
            // 
            // btnSetAxis
            // 
            this.btnSetAxis.Location = new System.Drawing.Point(16, 51);
            this.btnSetAxis.Name = "btnSetAxis";
            this.btnSetAxis.Size = new System.Drawing.Size(127, 23);
            this.btnSetAxis.TabIndex = 11;
            this.btnSetAxis.Text = "Set Axis";
            this.btnSetAxis.UseVisualStyleBackColor = true;
            this.btnSetAxis.Click += new System.EventHandler(this.btnSetAxis_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(82, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Y";
            // 
            // tbYAxisValue
            // 
            this.tbYAxisValue.Location = new System.Drawing.Point(102, 19);
            this.tbYAxisValue.Name = "tbYAxisValue";
            this.tbYAxisValue.Size = new System.Drawing.Size(41, 20);
            this.tbYAxisValue.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "X";
            // 
            // pbNodeStructure
            // 
            this.pbNodeStructure.Location = new System.Drawing.Point(484, 11);
            this.pbNodeStructure.Name = "pbNodeStructure";
            this.pbNodeStructure.Size = new System.Drawing.Size(472, 472);
            this.pbNodeStructure.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbNodeStructure.TabIndex = 10;
            this.pbNodeStructure.TabStop = false;
            // 
            // btnDerivativeByFunction
            // 
            this.btnDerivativeByFunction.Location = new System.Drawing.Point(13, 19);
            this.btnDerivativeByFunction.Name = "btnDerivativeByFunction";
            this.btnDerivativeByFunction.Size = new System.Drawing.Size(126, 23);
            this.btnDerivativeByFunction.TabIndex = 11;
            this.btnDerivativeByFunction.Text = "Derivative by function";
            this.btnDerivativeByFunction.UseVisualStyleBackColor = true;
            this.btnDerivativeByFunction.Click += new System.EventHandler(this.btnDerivativeByFunction_Click);
            // 
            // btnDerivativeByNewton
            // 
            this.btnDerivativeByNewton.Location = new System.Drawing.Point(13, 51);
            this.btnDerivativeByNewton.Name = "btnDerivativeByNewton";
            this.btnDerivativeByNewton.Size = new System.Drawing.Size(126, 23);
            this.btnDerivativeByNewton.TabIndex = 12;
            this.btnDerivativeByNewton.Text = "Derivative by newton";
            this.btnDerivativeByNewton.UseVisualStyleBackColor = true;
            this.btnDerivativeByNewton.Click += new System.EventHandler(this.btnDerivativeByNewton_Click);
            // 
            // lbFunctions
            // 
            this.lbFunctions.FormattingEnabled = true;
            this.lbFunctions.Location = new System.Drawing.Point(13, 397);
            this.lbFunctions.Name = "lbFunctions";
            this.lbFunctions.Size = new System.Drawing.Size(128, 186);
            this.lbFunctions.TabIndex = 13;
            this.lbFunctions.SelectedIndexChanged += new System.EventHandler(this.lbFunctions_SelectedIndexChanged);
            // 
            // gbSetAxis
            // 
            this.gbSetAxis.BackColor = System.Drawing.Color.Transparent;
            this.gbSetAxis.Controls.Add(this.btnSetAxis);
            this.gbSetAxis.Controls.Add(this.tbYAxisValue);
            this.gbSetAxis.Controls.Add(this.label2);
            this.gbSetAxis.Controls.Add(this.tbXAxisValue);
            this.gbSetAxis.Controls.Add(this.label1);
            this.gbSetAxis.Location = new System.Drawing.Point(309, 400);
            this.gbSetAxis.Name = "gbSetAxis";
            this.gbSetAxis.Size = new System.Drawing.Size(159, 82);
            this.gbSetAxis.TabIndex = 14;
            this.gbSetAxis.TabStop = false;
            this.gbSetAxis.Text = "Axis";
            // 
            // gbDerivative
            // 
            this.gbDerivative.Controls.Add(this.btnDerivativeByNewton);
            this.gbDerivative.Controls.Add(this.btnDerivativeByFunction);
            this.gbDerivative.Location = new System.Drawing.Point(152, 400);
            this.gbDerivative.Name = "gbDerivative";
            this.gbDerivative.Size = new System.Drawing.Size(151, 82);
            this.gbDerivative.TabIndex = 15;
            this.gbDerivative.TabStop = false;
            this.gbDerivative.Text = "Derivative";
            // 
            // gbRienmannIntegral
            // 
            this.gbRienmannIntegral.Controls.Add(this.cbRienmannAbsolute);
            this.gbRienmannIntegral.Controls.Add(this.lblRienmannAnswer);
            this.gbRienmannIntegral.Controls.Add(this.btnCalculateRienmann);
            this.gbRienmannIntegral.Controls.Add(this.tbToX);
            this.gbRienmannIntegral.Controls.Add(this.lblToX);
            this.gbRienmannIntegral.Controls.Add(this.tbFromX);
            this.gbRienmannIntegral.Controls.Add(this.lblFromX);
            this.gbRienmannIntegral.Controls.Add(this.tbDeltaX);
            this.gbRienmannIntegral.Controls.Add(this.lblDeltaX);
            this.gbRienmannIntegral.Location = new System.Drawing.Point(148, 489);
            this.gbRienmannIntegral.Name = "gbRienmannIntegral";
            this.gbRienmannIntegral.Size = new System.Drawing.Size(320, 93);
            this.gbRienmannIntegral.TabIndex = 16;
            this.gbRienmannIntegral.TabStop = false;
            this.gbRienmannIntegral.Text = "Rienmann intergal";
            // 
            // cbRienmannAbsolute
            // 
            this.cbRienmannAbsolute.AutoSize = true;
            this.cbRienmannAbsolute.Location = new System.Drawing.Point(204, 19);
            this.cbRienmannAbsolute.Name = "cbRienmannAbsolute";
            this.cbRienmannAbsolute.Size = new System.Drawing.Size(67, 17);
            this.cbRienmannAbsolute.TabIndex = 8;
            this.cbRienmannAbsolute.Text = "Absolute";
            this.cbRienmannAbsolute.UseVisualStyleBackColor = true;
            // 
            // lblRienmannAnswer
            // 
            this.lblRienmannAnswer.AutoSize = true;
            this.lblRienmannAnswer.Location = new System.Drawing.Point(85, 70);
            this.lblRienmannAnswer.Name = "lblRienmannAnswer";
            this.lblRienmannAnswer.Size = new System.Drawing.Size(42, 13);
            this.lblRienmannAnswer.TabIndex = 7;
            this.lblRienmannAnswer.Text = "Answer";
            // 
            // btnCalculateRienmann
            // 
            this.btnCalculateRienmann.Location = new System.Drawing.Point(10, 65);
            this.btnCalculateRienmann.Name = "btnCalculateRienmann";
            this.btnCalculateRienmann.Size = new System.Drawing.Size(69, 23);
            this.btnCalculateRienmann.TabIndex = 6;
            this.btnCalculateRienmann.Text = "Calculate";
            this.btnCalculateRienmann.UseVisualStyleBackColor = true;
            this.btnCalculateRienmann.Click += new System.EventHandler(this.btnCalculateRienmann_Click);
            // 
            // tbToX
            // 
            this.tbToX.Location = new System.Drawing.Point(204, 39);
            this.tbToX.Name = "tbToX";
            this.tbToX.Size = new System.Drawing.Size(100, 20);
            this.tbToX.TabIndex = 5;
            // 
            // lblToX
            // 
            this.lblToX.AutoSize = true;
            this.lblToX.Location = new System.Drawing.Point(168, 42);
            this.lblToX.Name = "lblToX";
            this.lblToX.Size = new System.Drawing.Size(30, 13);
            this.lblToX.TabIndex = 4;
            this.lblToX.Text = "To X";
            // 
            // tbFromX
            // 
            this.tbFromX.Location = new System.Drawing.Point(55, 39);
            this.tbFromX.Name = "tbFromX";
            this.tbFromX.Size = new System.Drawing.Size(100, 20);
            this.tbFromX.TabIndex = 3;
            // 
            // lblFromX
            // 
            this.lblFromX.AutoSize = true;
            this.lblFromX.Location = new System.Drawing.Point(7, 42);
            this.lblFromX.Name = "lblFromX";
            this.lblFromX.Size = new System.Drawing.Size(40, 13);
            this.lblFromX.TabIndex = 2;
            this.lblFromX.Text = "From X";
            // 
            // tbDeltaX
            // 
            this.tbDeltaX.Location = new System.Drawing.Point(55, 17);
            this.tbDeltaX.Name = "tbDeltaX";
            this.tbDeltaX.Size = new System.Drawing.Size(100, 20);
            this.tbDeltaX.TabIndex = 1;
            // 
            // lblDeltaX
            // 
            this.lblDeltaX.AutoSize = true;
            this.lblDeltaX.Location = new System.Drawing.Point(7, 20);
            this.lblDeltaX.Name = "lblDeltaX";
            this.lblDeltaX.Size = new System.Drawing.Size(42, 13);
            this.lblDeltaX.TabIndex = 0;
            this.lblDeltaX.Text = "Delta X";
            // 
            // lblHumanReadableString
            // 
            this.lblHumanReadableString.AutoSize = true;
            this.lblHumanReadableString.Location = new System.Drawing.Point(13, 65);
            this.lblHumanReadableString.Name = "lblHumanReadableString";
            this.lblHumanReadableString.Size = new System.Drawing.Size(114, 13);
            this.lblHumanReadableString.TabIndex = 17;
            this.lblHumanReadableString.Text = "Human Readable Sum";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(963, 590);
            this.Controls.Add(this.pbNodeStructure);
            this.Controls.Add(this.lblHumanReadableString);
            this.Controls.Add(this.gbRienmannIntegral);
            this.Controls.Add(this.gbDerivative);
            this.Controls.Add(this.gbSetAxis);
            this.Controls.Add(this.lbFunctions);
            this.Controls.Add(this.ChartFunction);
            this.Controls.Add(this.lblCalculateForXAnswer);
            this.Controls.Add(this.btnCalculateForX);
            this.Controls.Add(this.tbXValue);
            this.Controls.Add(this.btnParseSum);
            this.Controls.Add(this.tbSum);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.ChartFunction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNodeStructure)).EndInit();
            this.gbSetAxis.ResumeLayout(false);
            this.gbSetAxis.PerformLayout();
            this.gbDerivative.ResumeLayout(false);
            this.gbRienmannIntegral.ResumeLayout(false);
            this.gbRienmannIntegral.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbSum;
        private System.Windows.Forms.Button btnParseSum;
        private System.Windows.Forms.TextBox tbXValue;
        private System.Windows.Forms.Button btnCalculateForX;
        private System.Windows.Forms.Label lblCalculateForXAnswer;
        private System.Windows.Forms.DataVisualization.Charting.Chart ChartFunction;
        private System.Windows.Forms.TextBox tbXAxisValue;
        private System.Windows.Forms.Button btnSetAxis;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbYAxisValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pbNodeStructure;
        private System.Windows.Forms.Button btnDerivativeByFunction;
        private System.Windows.Forms.Button btnDerivativeByNewton;
        private System.Windows.Forms.ListBox lbFunctions;
        private System.Windows.Forms.GroupBox gbSetAxis;
        private System.Windows.Forms.GroupBox gbDerivative;
        private System.Windows.Forms.GroupBox gbRienmannIntegral;
        private System.Windows.Forms.Label lblRienmannAnswer;
        private System.Windows.Forms.Button btnCalculateRienmann;
        private System.Windows.Forms.TextBox tbToX;
        private System.Windows.Forms.Label lblToX;
        private System.Windows.Forms.TextBox tbFromX;
        private System.Windows.Forms.Label lblFromX;
        private System.Windows.Forms.TextBox tbDeltaX;
        private System.Windows.Forms.Label lblDeltaX;
        private System.Windows.Forms.CheckBox cbRienmannAbsolute;
        private System.Windows.Forms.Label lblHumanReadableString;
    }
}
