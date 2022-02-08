
namespace SR_400
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
            this.bSend = new System.Windows.Forms.Button();
            this.tbCommand = new System.Windows.Forms.TextBox();
            this.bMeasure = new System.Windows.Forms.Button();
            this.numCh1 = new System.Windows.Forms.NumericUpDown();
            this.numCh2 = new System.Windows.Forms.NumericUpDown();
            this.numPol = new System.Windows.Forms.NumericUpDown();
            this.lbResult = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numAccumTime = new System.Windows.Forms.NumericUpDown();
            this.labStatus = new System.Windows.Forms.Label();
            this.bInit = new System.Windows.Forms.Button();
            this.tbPortName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numCh1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCh2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAccumTime)).BeginInit();
            this.SuspendLayout();
            // 
            // bSend
            // 
            this.bSend.Location = new System.Drawing.Point(62, 61);
            this.bSend.Name = "bSend";
            this.bSend.Size = new System.Drawing.Size(75, 23);
            this.bSend.TabIndex = 0;
            this.bSend.Text = "Send";
            this.bSend.UseVisualStyleBackColor = true;
            this.bSend.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbCommand
            // 
            this.tbCommand.Location = new System.Drawing.Point(95, 90);
            this.tbCommand.Name = "tbCommand";
            this.tbCommand.Size = new System.Drawing.Size(221, 20);
            this.tbCommand.TabIndex = 2;
            // 
            // bMeasure
            // 
            this.bMeasure.Location = new System.Drawing.Point(62, 153);
            this.bMeasure.Name = "bMeasure";
            this.bMeasure.Size = new System.Drawing.Size(75, 23);
            this.bMeasure.TabIndex = 0;
            this.bMeasure.Text = "Measure";
            this.bMeasure.UseVisualStyleBackColor = true;
            this.bMeasure.Click += new System.EventHandler(this.bMeasure_Click);
            // 
            // numCh1
            // 
            this.numCh1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numCh1.Location = new System.Drawing.Point(62, 183);
            this.numCh1.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numCh1.Minimum = new decimal(new int[] {
            999999999,
            0,
            0,
            -2147483648});
            this.numCh1.Name = "numCh1";
            this.numCh1.Size = new System.Drawing.Size(75, 20);
            this.numCh1.TabIndex = 3;
            // 
            // numCh2
            // 
            this.numCh2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numCh2.Location = new System.Drawing.Point(62, 209);
            this.numCh2.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numCh2.Minimum = new decimal(new int[] {
            999999999,
            0,
            0,
            -2147483648});
            this.numCh2.Name = "numCh2";
            this.numCh2.Size = new System.Drawing.Size(75, 20);
            this.numCh2.TabIndex = 3;
            // 
            // numPol
            // 
            this.numPol.DecimalPlaces = 3;
            this.numPol.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numPol.Location = new System.Drawing.Point(62, 235);
            this.numPol.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numPol.Minimum = new decimal(new int[] {
            999999999,
            0,
            0,
            -2147483648});
            this.numPol.Name = "numPol";
            this.numPol.Size = new System.Drawing.Size(75, 20);
            this.numPol.TabIndex = 3;
            // 
            // lbResult
            // 
            this.lbResult.AutoSize = true;
            this.lbResult.Location = new System.Drawing.Point(66, 93);
            this.lbResult.Name = "lbResult";
            this.lbResult.Size = new System.Drawing.Size(16, 13);
            this.lbResult.TabIndex = 4;
            this.lbResult.Text = "In";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(95, 116);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(221, 20);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(66, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Out";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(143, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Time, sec";
            // 
            // numAccumTime
            // 
            this.numAccumTime.DecimalPlaces = 1;
            this.numAccumTime.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numAccumTime.Location = new System.Drawing.Point(203, 155);
            this.numAccumTime.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numAccumTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numAccumTime.Name = "numAccumTime";
            this.numAccumTime.Size = new System.Drawing.Size(74, 20);
            this.numAccumTime.TabIndex = 3;
            this.numAccumTime.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // labStatus
            // 
            this.labStatus.AutoSize = true;
            this.labStatus.Location = new System.Drawing.Point(145, 187);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(16, 13);
            this.labStatus.TabIndex = 4;
            this.labStatus.Text = "In";
            // 
            // bInit
            // 
            this.bInit.Location = new System.Drawing.Point(62, 32);
            this.bInit.Name = "bInit";
            this.bInit.Size = new System.Drawing.Size(75, 23);
            this.bInit.TabIndex = 0;
            this.bInit.Text = "Init";
            this.bInit.UseVisualStyleBackColor = true;
            this.bInit.Click += new System.EventHandler(this.bInit_Click);
            // 
            // tbPortName
            // 
            this.tbPortName.Location = new System.Drawing.Point(148, 33);
            this.tbPortName.Name = "tbPortName";
            this.tbPortName.Size = new System.Drawing.Size(57, 20);
            this.tbPortName.TabIndex = 2;
            this.tbPortName.Text = "COM3";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 303);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labStatus);
            this.Controls.Add(this.lbResult);
            this.Controls.Add(this.numPol);
            this.Controls.Add(this.numCh2);
            this.Controls.Add(this.numAccumTime);
            this.Controls.Add(this.numCh1);
            this.Controls.Add(this.tbPortName);
            this.Controls.Add(this.tbCommand);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.bMeasure);
            this.Controls.Add(this.bInit);
            this.Controls.Add(this.bSend);
            this.Name = "Form1";
            this.Text = "SR-400";
            ((System.ComponentModel.ISupportInitialize)(this.numCh1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCh2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAccumTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bSend;
        private System.Windows.Forms.TextBox tbCommand;
        private System.Windows.Forms.Button bMeasure;
        private System.Windows.Forms.NumericUpDown numCh1;
        private System.Windows.Forms.NumericUpDown numCh2;
        private System.Windows.Forms.NumericUpDown numPol;
        private System.Windows.Forms.Label lbResult;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numAccumTime;
        private System.Windows.Forms.Label labStatus;
        private System.Windows.Forms.Button bInit;
        private System.Windows.Forms.TextBox tbPortName;
    }
}

