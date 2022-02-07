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
            InitDevice();
        }

        SR400 sr400;

        public void InitDevice()
        {
            sr400 = new SR400("COM1");
            numAccumTime.DecimalPlaces = SR400.TimeDecPlaces;
            numAccumTime.Minimum = (decimal)Math.Pow(10, -SR400.TimeDecPlaces);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Task.Run(() => textBox1.Invoke((MethodInvoker)(() => textBox1.Text = sr400.SendCommandsOC(tbCommand.Text).Item2)));
        }

        private void bMeasure_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    labStatus.ForeColor = Color.Red;
                    labStatus.Text = "Reading";
                });
                var result = sr400.Measure((double)numAccumTime.Value);
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
}
