using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SR_400
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SR400 sr400;

        public void InitDevice()
        {
            butInit.ForeColor = Color.Black;
            sr400 = new SR400(tbPortName.Text, (double)numDiscrLevel_mV.Value, (double)numQuartzFrequency_kHz.Value,
                (double)numStrobeWidth_perc.Value, (double)numPhaseWidth_perc.Value);
            numAccumTime_sec.DecimalPlaces = SR400.TimeDecPlaces;
            numAccumTime_sec.Minimum = (decimal)Math.Pow(10, -SR400.TimeDecPlaces);
            butInit.ForeColor = Color.Blue;
        }

        private void butSend_Click(object sender, EventArgs e)
        {
            if (sr400 != null && tbCommand.Text.Length > 0)
            {
                Task.Run(() =>
                {
                    textBox1.Invoke((MethodInvoker)(() => textBox1.Text = sr400.SendCommandsOC(tbCommand.Text, cbRead.Checked).Item2));
                });
            }
        }

        private void bMeasure_Click(object sender, EventArgs e)
        {
            if (sr400 != null)
            {
                Task.Run(() =>
                {
                    this.Invoke((MethodInvoker)delegate ()
                    {
                        labStatus.ForeColor = Color.Red;
                        labStatus.Text = "Reading";
                    });
                    var result = sr400.Measure((double)numAccumTime_sec.Value);
                    this.Invoke((MethodInvoker)delegate ()
                    {
                        numCh1.Value = (decimal)result.Ch1;
                        numCh2.Value = (decimal)result.Ch2;
                        numPol.Value = (decimal)result.Pol;
                        labStatus.ForeColor = Color.Green;
                        labStatus.Text = "OK";
                    });
                });
            }
        }

        private void bInit_Click(object sender, EventArgs e)
        {
            InitDevice();
        }
    }
}
