
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
            this.numAccumTime_sec = new System.Windows.Forms.NumericUpDown();
            this.labStatus = new System.Windows.Forms.Label();
            this.bInit = new System.Windows.Forms.Button();
            this.tbPortName = new System.Windows.Forms.TextBox();
            this.numDiscrLevel_mV = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numQuartzFrequency_kHz = new System.Windows.Forms.NumericUpDown();
            this.lbStrobeWidth = new System.Windows.Forms.Label();
            this.numStrobeWidth_perc = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numCh1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCh2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAccumTime_sec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDiscrLevel_mV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuartzFrequency_kHz)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStrobeWidth_perc)).BeginInit();
            this.SuspendLayout();
            // 
            // bSend
            // 
            this.bSend.Location = new System.Drawing.Point(12, 42);
            this.bSend.Name = "bSend";
            this.bSend.Size = new System.Drawing.Size(75, 23);
            this.bSend.TabIndex = 0;
            this.bSend.Text = "Send";
            this.bSend.UseVisualStyleBackColor = true;
            this.bSend.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbCommand
            // 
            this.tbCommand.Location = new System.Drawing.Point(45, 71);
            this.tbCommand.Name = "tbCommand";
            this.tbCommand.Size = new System.Drawing.Size(221, 20);
            this.tbCommand.TabIndex = 2;
            // 
            // bMeasure
            // 
            this.bMeasure.Location = new System.Drawing.Point(12, 134);
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
            this.numCh1.Location = new System.Drawing.Point(12, 275);
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
            this.numCh2.Location = new System.Drawing.Point(98, 275);
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
            this.numPol.Location = new System.Drawing.Point(185, 275);
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
            this.lbResult.Location = new System.Drawing.Point(16, 74);
            this.lbResult.Name = "lbResult";
            this.lbResult.Size = new System.Drawing.Size(16, 13);
            this.lbResult.TabIndex = 4;
            this.lbResult.Text = "In";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(45, 97);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(221, 20);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Out";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 168);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Acc time, sec";
            // 
            // numAccumTime_sec
            // 
            this.numAccumTime_sec.DecimalPlaces = 1;
            this.numAccumTime_sec.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numAccumTime_sec.Location = new System.Drawing.Point(12, 184);
            this.numAccumTime_sec.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numAccumTime_sec.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numAccumTime_sec.Name = "numAccumTime_sec";
            this.numAccumTime_sec.Size = new System.Drawing.Size(74, 20);
            this.numAccumTime_sec.TabIndex = 3;
            this.numAccumTime_sec.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // labStatus
            // 
            this.labStatus.AutoSize = true;
            this.labStatus.Location = new System.Drawing.Point(11, 255);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(16, 13);
            this.labStatus.TabIndex = 4;
            this.labStatus.Text = "In";
            // 
            // bInit
            // 
            this.bInit.Location = new System.Drawing.Point(12, 13);
            this.bInit.Name = "bInit";
            this.bInit.Size = new System.Drawing.Size(75, 23);
            this.bInit.TabIndex = 0;
            this.bInit.Text = "Init";
            this.bInit.UseVisualStyleBackColor = true;
            this.bInit.Click += new System.EventHandler(this.bInit_Click);
            // 
            // tbPortName
            // 
            this.tbPortName.Location = new System.Drawing.Point(98, 14);
            this.tbPortName.Name = "tbPortName";
            this.tbPortName.Size = new System.Drawing.Size(57, 20);
            this.tbPortName.TabIndex = 2;
            this.tbPortName.Text = "COM3";
            // 
            // numDiscrLevel_mV
            // 
            this.numDiscrLevel_mV.DecimalPlaces = 1;
            this.numDiscrLevel_mV.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numDiscrLevel_mV.Location = new System.Drawing.Point(98, 184);
            this.numDiscrLevel_mV.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numDiscrLevel_mV.Minimum = new decimal(new int[] {
            300,
            0,
            0,
            -2147483648});
            this.numDiscrLevel_mV.Name = "numDiscrLevel_mV";
            this.numDiscrLevel_mV.Size = new System.Drawing.Size(74, 20);
            this.numDiscrLevel_mV.TabIndex = 3;
            this.numDiscrLevel_mV.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(95, 168);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Signal discr level, mV";
            // 
            // numQuartzFrequency_kHz
            // 
            this.numQuartzFrequency_kHz.DecimalPlaces = 3;
            this.numQuartzFrequency_kHz.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numQuartzFrequency_kHz.Location = new System.Drawing.Point(12, 226);
            this.numQuartzFrequency_kHz.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numQuartzFrequency_kHz.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numQuartzFrequency_kHz.Name = "numQuartzFrequency_kHz";
            this.numQuartzFrequency_kHz.Size = new System.Drawing.Size(74, 20);
            this.numQuartzFrequency_kHz.TabIndex = 3;
            this.numQuartzFrequency_kHz.Value = new decimal(new int[] {
            50028,
            0,
            0,
            196608});
            // 
            // lbStrobeWidth
            // 
            this.lbStrobeWidth.AutoSize = true;
            this.lbStrobeWidth.Location = new System.Drawing.Point(9, 210);
            this.lbStrobeWidth.Name = "lbStrobeWidth";
            this.lbStrobeWidth.Size = new System.Drawing.Size(84, 13);
            this.lbStrobeWidth.TabIndex = 4;
            this.lbStrobeWidth.Text = "Quartz freq, kHz";
            // 
            // numStrobeWidth_perc
            // 
            this.numStrobeWidth_perc.DecimalPlaces = 3;
            this.numStrobeWidth_perc.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numStrobeWidth_perc.Location = new System.Drawing.Point(98, 226);
            this.numStrobeWidth_perc.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numStrobeWidth_perc.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numStrobeWidth_perc.Name = "numStrobeWidth_perc";
            this.numStrobeWidth_perc.Size = new System.Drawing.Size(74, 20);
            this.numStrobeWidth_perc.TabIndex = 3;
            this.numStrobeWidth_perc.Value = new decimal(new int[] {
            34217,
            0,
            0,
            196608});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(95, 210);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Ch A/B strobe width, %";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 307);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbStrobeWidth);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labStatus);
            this.Controls.Add(this.lbResult);
            this.Controls.Add(this.numPol);
            this.Controls.Add(this.numCh2);
            this.Controls.Add(this.numStrobeWidth_perc);
            this.Controls.Add(this.numQuartzFrequency_kHz);
            this.Controls.Add(this.numDiscrLevel_mV);
            this.Controls.Add(this.numAccumTime_sec);
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
            ((System.ComponentModel.ISupportInitialize)(this.numAccumTime_sec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDiscrLevel_mV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuartzFrequency_kHz)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStrobeWidth_perc)).EndInit();
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
        private System.Windows.Forms.NumericUpDown numAccumTime_sec;
        private System.Windows.Forms.Label labStatus;
        private System.Windows.Forms.Button bInit;
        private System.Windows.Forms.TextBox tbPortName;
        private System.Windows.Forms.NumericUpDown numDiscrLevel_mV;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numQuartzFrequency_kHz;
        private System.Windows.Forms.Label lbStrobeWidth;
        private System.Windows.Forms.NumericUpDown numStrobeWidth_perc;
        private System.Windows.Forms.Label label4;
    }
}

