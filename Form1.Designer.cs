
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
            this.butSend = new System.Windows.Forms.Button();
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
            this.butInit = new System.Windows.Forms.Button();
            this.tbPortName = new System.Windows.Forms.TextBox();
            this.numDiscrLevel_mV = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numQuartzFrequency_kHz = new System.Windows.Forms.NumericUpDown();
            this.lbStrobeWidth = new System.Windows.Forms.Label();
            this.numStrobeWidth_perc = new System.Windows.Forms.NumericUpDown();
            this.cbRead = new System.Windows.Forms.CheckBox();
            this.numPhaseWidth_perc = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numCh1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCh2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAccumTime_sec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDiscrLevel_mV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuartzFrequency_kHz)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStrobeWidth_perc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPhaseWidth_perc)).BeginInit();
            this.SuspendLayout();
            // 
            // butSend
            // 
            this.butSend.Location = new System.Drawing.Point(12, 42);
            this.butSend.Name = "butSend";
            this.butSend.Size = new System.Drawing.Size(75, 23);
            this.butSend.TabIndex = 0;
            this.butSend.Text = "Send";
            this.butSend.UseVisualStyleBackColor = true;
            this.butSend.Click += new System.EventHandler(this.butSend_Click);
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
            this.numCh1.Location = new System.Drawing.Point(12, 294);
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
            this.numCh2.Location = new System.Drawing.Point(98, 294);
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
            this.numPol.Location = new System.Drawing.Point(185, 294);
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
            this.labStatus.Location = new System.Drawing.Point(11, 277);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(16, 13);
            this.labStatus.TabIndex = 4;
            this.labStatus.Text = "In";
            // 
            // butInit
            // 
            this.butInit.Location = new System.Drawing.Point(12, 13);
            this.butInit.Name = "butInit";
            this.butInit.Size = new System.Drawing.Size(75, 23);
            this.butInit.TabIndex = 0;
            this.butInit.Text = "Init";
            this.butInit.UseVisualStyleBackColor = true;
            this.butInit.Click += new System.EventHandler(this.bInit_Click);
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
            this.label3.Size = new System.Drawing.Size(95, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Signal discr lvl, mV";
            // 
            // numQuartzFrequency_kHz
            // 
            this.numQuartzFrequency_kHz.DecimalPlaces = 3;
            this.numQuartzFrequency_kHz.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numQuartzFrequency_kHz.Location = new System.Drawing.Point(12, 245);
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
            this.lbStrobeWidth.Location = new System.Drawing.Point(9, 216);
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
            this.numStrobeWidth_perc.Location = new System.Drawing.Point(98, 245);
            this.numStrobeWidth_perc.Maximum = new decimal(new int[] {
            90,
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
            // cbRead
            // 
            this.cbRead.AutoSize = true;
            this.cbRead.Checked = true;
            this.cbRead.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRead.Location = new System.Drawing.Point(98, 46);
            this.cbRead.Name = "cbRead";
            this.cbRead.Size = new System.Drawing.Size(52, 17);
            this.cbRead.TabIndex = 5;
            this.cbRead.Text = "Read";
            this.cbRead.UseVisualStyleBackColor = true;
            // 
            // numPhaseWidth_perc
            // 
            this.numPhaseWidth_perc.DecimalPlaces = 3;
            this.numPhaseWidth_perc.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numPhaseWidth_perc.Location = new System.Drawing.Point(185, 245);
            this.numPhaseWidth_perc.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.numPhaseWidth_perc.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numPhaseWidth_perc.Name = "numPhaseWidth_perc";
            this.numPhaseWidth_perc.Size = new System.Drawing.Size(74, 20);
            this.numPhaseWidth_perc.TabIndex = 3;
            this.numPhaseWidth_perc.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(183, 216);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Phase Width, %";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(98, 212);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(75, 37);
            this.richTextBox1.TabIndex = 6;
            this.richTextBox1.Text = "Ch A/B strobe width, %";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 327);
            this.Controls.Add(this.cbRead);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lbStrobeWidth);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labStatus);
            this.Controls.Add(this.lbResult);
            this.Controls.Add(this.numPol);
            this.Controls.Add(this.numCh2);
            this.Controls.Add(this.numPhaseWidth_perc);
            this.Controls.Add(this.numStrobeWidth_perc);
            this.Controls.Add(this.numQuartzFrequency_kHz);
            this.Controls.Add(this.numDiscrLevel_mV);
            this.Controls.Add(this.numAccumTime_sec);
            this.Controls.Add(this.numCh1);
            this.Controls.Add(this.tbPortName);
            this.Controls.Add(this.tbCommand);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.bMeasure);
            this.Controls.Add(this.butInit);
            this.Controls.Add(this.butSend);
            this.Controls.Add(this.richTextBox1);
            this.Name = "Form1";
            this.Text = "SR400";
            ((System.ComponentModel.ISupportInitialize)(this.numCh1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCh2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAccumTime_sec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDiscrLevel_mV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuartzFrequency_kHz)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStrobeWidth_perc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPhaseWidth_perc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button butSend;
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
        private System.Windows.Forms.Button butInit;
        private System.Windows.Forms.TextBox tbPortName;
        private System.Windows.Forms.NumericUpDown numDiscrLevel_mV;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numQuartzFrequency_kHz;
        private System.Windows.Forms.Label lbStrobeWidth;
        private System.Windows.Forms.NumericUpDown numStrobeWidth_perc;
        private System.Windows.Forms.CheckBox cbRead;
        private System.Windows.Forms.NumericUpDown numPhaseWidth_perc;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}

