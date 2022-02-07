//See pp. 5-6 of the SR400's manual for brief counting logic explanation.

using DevicesBase;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SR_400
{
    public class SR400 : ComPort
    {
        /// <summary>
        /// Discriminator SR400: discrimination level in volts 
        /// </summary>
        private static double _discriminatorLevel = -12e-3;

        //==============================================
        // Initialization
        //==============================================

        /// <summary>
        /// Without DtrEnable it can't read.
        /// </summary>
        /// <param name="pName"></param>
        public SR400(string pName) :
            base(null, null, pName, 9600, System.IO.Ports.Parity.None,
                8, System.IO.Ports.StopBits.Two, System.IO.Ports.Handshake.None,
                2000, 2000, 0, false, true)
        {
            // 'SETUP COM1: PORT TO 9600 BAUD, NO PARITY, 8 DATA BITS, 2 STOP BITS,
            // 'IGNORE CTS (CLEAR TO SEND), DSR (DATA SET READY),
            // 'AND CD (CARRIER DETECT)
            Initialize();
        }

        public bool Initialize()
        {
            Enabled = false;
            WaitForAccess();
            if (OpenConnection())
            {
                FlushIOBuffer();
                SpecifySettings();
                Enabled = true;
                CloseConnection();
            }
            OpenAccess();
            return Enabled;
        }

        //==============================================
        // Commands, Top
        //==============================================

        public (double Ch1, double Ch2, double Pol) Measure(double accumTimeSec, double accumNr = 1)
        {
            double ch1 = 0d, ch2 = 0d, pol = 0d;
            WaitForAccess();
            if (OpenConnection())
            {
                ch1 = 0d;
                ch2 = 0d;
                for (int k = 0; k < accumNr; k++)
                {
                    var answer = CountAB(accumTimeSec);
                    ch1 += answer.Ch1;
                    ch2 += answer.Ch2;
                }
                pol = CalculatePolarization(ch1, ch2);
                CloseConnection();
            }
            OpenAccess();
            return (ch1, ch2, pol);
        }

        /// <summary>
        /// It must be remembered that these are settings for 42kHz quartz, if there is another quartz, 
        /// then you need to change the Gate parameters for each channel 
        /// </summary>
        /// <param name="accumTime"></param>
        /// <param name="D_level"></param>
        public void SpecifySettings()
        {
            SetCountMode(0); 
            SetNumberOfPeriodsInScan(1); 
            //SetCounterPreSet(2, accumTime * 10000000.0); 
            //"for chanell A setup";
            SetCounterInput(0, 1);
            SetGateDelay(0, 0d); //default is 0
            SetGateMode(0, 0); //When there is a normal signal for the Gate, it will need to be specified 
            SetGateWidth(0, 4E-6);
            SetDiscriminatorSlope(0, 1); //fall
            SetDiscriminatorLevel(0, _discriminatorLevel);
            // GateWidth A = 4E-6 sec
            //"for chanell B setup";
            SetCounterInput(1, 2);
            SetGateDelay(1, 0d); //default is 0 //1, 12E-6
            SetGateMode(1, 0); //When there is a normal signal for the Gate, it will need to be specified 
            SetGateWidth(1, 4E-6);
            SetDiscriminatorSlope(1, 1); //fall
            SetDiscriminatorLevel(1, _discriminatorLevel);
            // GateWidth B = 4E-6 sec
            Thread.Sleep(2);
        }

        public (double Ch1, double Ch2, double Pol) CountAB(double accumTimeSec)
        {
            double ch1 = 0d, ch2 = 0d;
            List<double> mSDAccumTimeValues = GetMSDArray(accumTimeSec);
            foreach(double value in mSDAccumTimeValues)
            {
                StartCountingAndWait(value);
                var answer = ReadCountsAB();
                ch1 += answer.Ch1;
                ch2 += answer.Ch2;
            }
            double pol = CalculatePolarization(ch1, ch2);
            return (ch1, ch2, pol);
        }

        private double CalculatePolarization(double ch1, double ch2)
        {
            double pol = 0d;
            if (ch1 >= 0 && ch2 >= 0 && (ch1 + ch2) > 0)
                pol = (ch1 - ch2) / (ch1 + ch2);
            return pol;
        }

        public const int TimeDecPlaces = 3;

        private List<double> GetMSDArray(double value)
        {
            List<double> list = new List<double>();
            string sValue = value.ToString("F" + TimeDecPlaces, CultureInfo.InvariantCulture);
            int startDegree = sValue.Split('.')[0].Length - 1;
            string sDigits = sValue.Replace(".", "");
            for (int i = startDegree; i >= -TimeDecPlaces; i--)
            {
                char c = sDigits[-i + startDegree];
                double dec = Math.Pow(10, i);
                int digit = int.Parse(c.ToString());
                if (digit != 0)
                    list.Add(digit * dec);
            }
            return list;
        }

        /// <summary>
        /// Start Counting based on most significant digit of set accumulation Time.
        /// </summary>
        /// <param name="accumTimeSec"></param>
        private void StartCountingAndWait(double accumTimeSec)
        {
            SetCounterPreSet(2, accumTimeSec * 10000000.0); 
            //var result = ReadCounterPreSet(2);
            ResetCounter();
            StartCount(); 
            Thread.Sleep((int)(accumTimeSec * 1000.0));
            while (ReadSpecificPrimaryStatusBit(1).PrimaryStatusBit != 1) //check Data Ready
                Thread.Sleep(5);
        }

        private (double Ch1, double Ch2) ReadCountsAB()
        {
            int A = ReadLastCountInCounterA().Count;
            int B = ReadLastCountInCounterB().Count;
            return (A, B);
        }

        //==============================================
        // SR400 commands' implementation is given below
        // Mode
        //==============================================

        /// <summary>
        /// CM j Set COUNT to mode j; A,B(0), A-B(1), A+B(2) for preset T, or A FOR B preset(3).<br/>
        /// Setting the counter mode also performs a counter reset.
        /// </summary>
        /// <param name="modeIndex"></param>
        /// <returns></returns>
        private bool SetCountMode(int modeIndex)
        {
            return SendCommand("CM", modeIndex).Success;
        }

        /// <summary>
        /// CM Read COUNT mode; A,B(0), A-B(1), A+B(2) for preset T, or A FOR B preset(3).
        /// </summary>
        /// <returns></returns>
        private (bool Success, int CountMode) ReadCountMode()
        {
            return ReadIntAfterSendCommand("CM");
        }

        /// <summary>
        /// CI i, j Set counter i to input j; 10 MHz(0), INP 1(1), INP 2(2), TRIG(3).
        /// </summary>
        /// <param name="counterIndex"></param>
        /// <param name="inputIndex"></param>
        /// <returns></returns>
        private bool SetCounterInput(int counterIndex, int inputIndex)
        {
            return SendCommand("CI", counterIndex, inputIndex).Success;
        }

        /// <summary>
        /// CI i Read input for counter i; 10 MHz(0), INP 1(1), INP 2(2), TRIG(3).
        /// </summary>
        /// <param name="counterIndex"></param>
        /// <returns></returns>
        private (bool Success, int CounterInput) ReadCounterInput(int counterIndex)
        {
            return ReadIntAfterSendCommand("CI", counterIndex);
        }

        /// <summary>
        /// CP i, n Set counter i preSET to 1 &lt;= n &lt;= 9E11.<br/>
        /// The number of count PERIODS or data points in a scan may be set from 1 to 2000. The duration of
        /// one count period is determined by the preset condition.<br/><br/>
        /// The CP command sets and reads the counter presets.If i = 1 then counter B is selected, if i = 2
        /// counter T is selected.The parameter i is required. If n is included, then counter i is preset to n where
        /// n is a value from 1 to 9E11. n may be expressed in any format but must be greater than or equal to 1
        /// and only the most significant digit is used.For example, "CP2,10" , "CP2,1E1" , "CP2,0.1E2" ,
        /// "CP2,12" all set T SET to 1E1. Changing a counter preset while counting causes the counters
        /// to pause.A counter start command or keypress resumes counting.If n is absent, then the preset
        /// value of counter i is returned.In the above example, the string "1E1" is returned.Note that n
        /// is the number of cycles of the 10 MHz clock, not seconds
        /// </summary>
        /// <param name="counterIndex"></param>
        /// <param name="preSetValue"></param>
        /// <returns></returns>
        private bool SetCounterPreSet(int counterIndex, double preSetValue)
        {
            return SendCommand("CP", counterIndex, preSetValue).Success;
        }

        /// <summary>
        /// CP i Read counter i preSET.
        /// </summary>
        /// <param name="counterIndex"></param>
        /// <returns></returns>
        private (bool Success, int CounterPreset) ReadCounterPreSet(int counterIndex)
        {
            return ReadIntAfterSendCommand("CP", counterIndex);
        }

        /// <summary>
        /// NP m Set Number of PERIODS in a scan to 1 &lt;= m &lt;= 2000.
        /// </summary>
        /// <param name="periodsNr"></param>
        /// <returns></returns>
        private bool SetNumberOfPeriodsInScan(int periodsNr)
        {
            return SendCommand("NP", periodsNr).Success;
        }

        /// <summary>
        /// NP If m is absent, the NP command returns the programmed number of periods in a scan(N PERIODS).
        /// </summary>
        /// <returns></returns>
        private (bool Success, int NumberOfPeriodsInScan) ReadNumberOfPeriodsInScan()
        {
            return ReadIntAfterSendCommand("NP");
        }

        /// <summary>
        /// NN Read current count period number or scan position.
        /// </summary>
        /// <returns></returns>
        private (bool Success, int CurrentPeriodNumber) ReadCurrentPeriodNumberOrScanPosition()
        {
            return ReadIntAfterSendCommand("NN");
        }

        /// <summary>
        /// NE j Set end of scan mode to mode j; START(1) or STOP(0).
        /// </summary>
        /// <param name="modeIndex"></param>
        /// <returns></returns>
        private bool SetScanEndMode(int modeIndex)
        {
            return SendCommand("NE", modeIndex).Success;
        }

        /// <summary>
        /// NE Read the scan end mode; START(1) or STOP(0).
        /// </summary>
        /// <returns></returns>
        private (bool Success, int ScanEndMode) ReadScanEndMode()
        {
            return ReadIntAfterSendCommand("NE");
        }

        /// <summary>
        /// DT x Set DWELL time to 2E-3 <= x <= 6E1 s or EXTERNAL(0).
        /// </summary>
        /// <param name="dwellTime"></param>
        /// <returns></returns>
        private bool SetDwellTime(double dwellTime)
        {
            return SendCommand("DT", dwellTime).Success;
        }

        /// <summary>
        /// DT Read DWELL time.
        /// </summary>
        /// <returns></returns>
        private (bool Success, int DwellTime) ReadDwellTime()
        {
            return ReadIntAfterSendCommand("DT");
        }

        /// <summary>
        /// AS j Set D/A to source j; A(0), B(1), A-B(2), A+B(3).
        /// </summary>
        /// <param name="sourceIndex"></param>
        /// <returns></returns>
        private bool SetDAOutputToSource(int sourceIndex)
        {
            return SendCommand("AS", sourceIndex).Success;
        }

        /// <summary>
        /// AS Read D/A Output; A(0), B(1), A-B(2), A+B(3).
        /// </summary>
        /// <returns></returns>
        private (bool Success, int DAOutputSource) ReadDAOutputToSource()
        {
            return ReadIntAfterSendCommand("AS");
        }

        /// <summary>
        /// AM j Set front panel D/A Output to RANGE j; LOG(0) or LINEAR(1-7).
        /// </summary>
        /// <param name="rangeIndex"></param>
        /// <returns></returns>
        private bool SetFrontPanelDAOutputToRange(int rangeIndex)
        {
            return SendCommand("AM", rangeIndex).Success;
        }

        /// <summary>
        /// AM Read front panel D/A Output RANGE; LOG(0) or LINEAR(1-7).
        /// </summary>
        /// <returns></returns>
        private (bool Success, int DAOutputRange) ReadFrontPanelDAOutputRange()
        {
            return ReadIntAfterSendCommand("AM");
        }

        /// <summary>
        /// SD j Set DISPLAY to mode j; CONTINUOUS(0) or HOLD(1).
        /// </summary>
        /// <param name="modeIndex"></param>
        /// <returns></returns>
        private bool SetDisplayMode(int modeIndex)
        {
            return SendCommand("SD", modeIndex).Success;
        }

        /// <summary>
        /// SD Read DISPLAY mode; CONTINUOUS(0) or HOLD(1).
        /// </summary>
        /// <returns></returns>
        private (bool Success, int DisplayMode) ReadDisplayMode()
        {
            return ReadIntAfterSendCommand("SD");
        }

        //==============================================
        // Levels
        //==============================================

        /// <summary>
        /// TS j Set TRIG to SLOPE j; RISE(0) or FALL(1).
        /// </summary>
        /// <param name="slopeIndex"></param>
        /// <returns></returns>
        private bool SetTriggerSlope(int slopeIndex)
        {
            return SendCommand("TS", slopeIndex).Success;
        }

        /// <summary>
        /// TS Read TRIG SLOPE; RISE(0) or FALL(1).
        /// </summary>
        /// <returns></returns>
        private (bool Success, int TriggerSlope) ReadTriggerSlope()
        {
            return ReadIntAfterSendCommand("TS");
        }

        /// <summary>
        /// TL v Set TRIG LVL to -2.000 &lt;= v &lt;= 2.000 V.
        /// </summary>
        /// <param name="trigLevel"></param>
        /// <returns></returns>
        private bool SetTriggerLevel(double trigLevel)
        {
            return SendCommand("TL", trigLevel).Success;
        }

        /// <summary>
        /// TL Read TRIG LVL.
        /// </summary>
        /// <returns></returns>
        private (bool Success, double TriggerLevel) ReadTriggerLevel()
        {
            return ReadDoubleAfterSendCommand("TL");
        }

        /// <summary>
        /// DS i, j Set DISC i to SLOPE j; RISE(0) or FALL(1).
        /// </summary>
        /// <param name="discriminatorIndex"></param>
        /// <param name="slopeIndex"></param>
        /// <returns></returns>
        private bool SetDiscriminatorSlope(int discriminatorIndex, int slopeIndex)
        {
            return SendCommand("DS", discriminatorIndex, slopeIndex).Success;
        }

        /// <summary>
        /// DS i Read DISC i SLOPE; RISE(0) or FALL(1).
        /// </summary>
        /// <param name="discriminatorIndex"></param>
        /// <returns></returns>
        private (bool Success, int DiscriminatorSlope) ReadDiscriminatorSlope(int discriminatorIndex)
        {
            return ReadIntAfterSendCommand("DS", discriminatorIndex);
        }

        /// <summary>
        /// DM i, j Set DISC i to mode j; FIXED(0) or SCAN(1).
        /// </summary>
        /// <param name="discriminatorIndex"></param>
        /// <param name="modeIndex"></param>
        /// <returns></returns>
        private bool SetDiscriminatorMode(int discriminatorIndex, int modeIndex)
        {
            return SendCommand("DM", discriminatorIndex, modeIndex).Success;
        }

        /// <summary>
        /// DM i Read DISC i mode; FIXED(0) or SCAN(1).
        /// </summary>
        /// <param name="discriminatorIndex"></param>
        /// <returns></returns>
        private (bool Success, int DiscriminatorMode) ReadDiscriminatorMode(int discriminatorIndex)
        {
            return ReadIntAfterSendCommand("DM", discriminatorIndex);
        }

        /// <summary>
        /// DY i, v Set DISC i scan step to -0.0200 &lt;= v &lt;= 0.0200 V.
        /// </summary>
        /// <param name="discriminatorIndex"></param>
        /// <param name="scanStep"></param>
        /// <returns></returns>
        private bool SetDiscriminatorScanStep(int discriminatorIndex, double scanStep)
        {
            return SendCommand("DY", discriminatorIndex, scanStep).Success;
        }

        /// <summary>
        /// DY i Read DISC i scan step.
        /// </summary>
        /// <param name="discriminatorIndex"></param>
        /// <returns></returns>
        private (bool Success, double DiscriminatorScanStep) ReadDiscriminatorScanStep(int discriminatorIndex)
        {
            return ReadDoubleAfterSendCommand("DY", discriminatorIndex);
        }

        /// <summary>
        /// DL i, v Set DISC i LVL to -0.3000 &lt;= v &lt;= 0.3000 V.
        /// </summary>
        /// <param name="discriminatorIndex"></param>
        /// <param name="discLevel"></param>
        /// <returns></returns>
        private bool SetDiscriminatorLevel(int discriminatorIndex, double discLevel)
        {
            return SendCommand("DL", discriminatorIndex, discLevel).Success;
        }

        /// <summary>
        /// DL i Read DISC i LVL.
        /// </summary>
        /// <param name="discriminatorIndex"></param>
        /// <returns></returns>
        private (bool Success, double DiscriminatorLevel) ReadDiscriminatorLevel(int discriminatorIndex)
        {
            return ReadDoubleAfterSendCommand("DL", discriminatorIndex);
        }

        /// <summary>
        /// DZ i Read current DISC i LVL(during scan).
        /// </summary>
        /// <param name="discriminatorIndex"></param>
        /// <returns></returns>
        private (bool Success, double CurrentDiscriminatorLevel) ReadCurrentDiscriminatorLevel(int discriminatorIndex)
        {
            return ReadDoubleAfterSendCommand("DZ", discriminatorIndex);
        }

        /// <summary>
        /// PM k, j Set PORT k(1 or 2) to mode j; FIXED(0) or SCAN(1).
        /// </summary>
        /// <param name="portIndex"></param>
        /// <param name="modeIndex"></param>
        /// <returns></returns>
        private bool SetPortMode(int portIndex, int modeIndex)
        {
            return SendCommand("PM", portIndex, modeIndex).Success;
        }

        /// <summary>
        /// PM k Read PORT k(1 or 2) mode; FIXED(0) or SCAN(1).
        /// </summary>
        /// <param name="portIndex"></param>
        /// <returns></returns>
        private (bool Success, int PortMode) ReadPortMode(int portIndex)
        {
            return ReadIntAfterSendCommand("PM", portIndex);
        }

        /// <summary>
        /// PY k, v Set PORT k(1 or 2) scan step to -0.500 &lt;= v &lt;= 0.500 V.
        /// </summary>
        /// <param name="portIndex"></param>
        /// <param name="scanStep"></param>
        /// <returns></returns>
        private bool SetPortScanStep(int portIndex, double scanStep)
        {
            return SendCommand("PY", portIndex, scanStep).Success;
        }

        /// <summary>
        /// PY k Read PORT k(1 or 2) scan step.
        /// </summary>
        /// <param name="portIndex"></param>
        /// <returns></returns>
        private (bool Success, double PortScanStep) ReadPortScanStep(int portIndex)
        {
            return ReadDoubleAfterSendCommand("PY", portIndex);
        }

        /// <summary>
        /// PL k, v Set PORT k(1 or 2) LVL to -10.000 &lt;= v &lt;= 10.000 V.<br/>
        /// Set Port Output Voltage.
        /// </summary>
        /// <param name="portIndex"></param>
        /// <param name="portOutputLevel"></param>
        /// <returns></returns>
        private bool SetPortOutputLevel(int portIndex, double portOutputLevel)
        {
            return SendCommand("PL", portIndex, portOutputLevel).Success;
        }

        /// <summary>
        /// PL k Read PORT k(1 or 2) LVL.
        /// </summary>
        /// <param name="portIndex"></param>
        /// <returns></returns>
        private (bool Success, double PortOutputLevel) ReadPortOutputLevel(int portIndex)
        {
            return ReadDoubleAfterSendCommand("PL", portIndex);
        }

        /// <summary>
        /// PZ k Read current PORT k(1 or 2) LVL(during scan).
        /// </summary>
        /// <param name="portIndex"></param>
        /// <returns></returns>
        private (bool Success, double CurrentPortOutputLevel) ReadCurrentPortOutputLevel(int portIndex)
        {
            return ReadDoubleAfterSendCommand("PZ", portIndex);
        }

        //==============================================
        // Gates
        //==============================================

        /// <summary>
        /// GM i,j Set GATE i to mode j; CW(0), FIXED(1), or SCAN(2).
        /// </summary>
        /// <param name="gateIndex"></param>
        /// <param name="gateMode"></param>
        /// <returns></returns>
        private bool SetGateMode(int gateIndex, int gateMode)
        {
            return SendCommand("GM", gateIndex, gateMode).Success;
        }

        /// <summary>
        /// GM i Read GATE i mode; CW(0), FIXED(1), or SCAN(2).
        /// </summary>
        /// <param name="gateIndex"></param>
        /// <returns></returns>
        private (bool Success, int GateMode) ReadGateMode(int gateIndex)
        {
            return ReadIntAfterSendCommand("GM", gateIndex);
        }

        /// <summary>
        /// GY i,t Set GATE i DELAY scan step to 0 &lt;= t &lt;= 99.92E-3 s.
        /// </summary>
        /// <param name="gateIndex"></param>
        /// <param name="scanStep"></param>
        /// <returns></returns>
        private bool SetGateDelayScanStep(int gateIndex, double scanStep)
        {
            return SendCommand("GY", gateIndex, scanStep).Success;
        }

        /// <summary>
        /// GY i Read GATE i DELAY scan step in seconds.
        /// </summary>
        /// <param name="gateIndex"></param>
        /// <returns></returns>
        private (bool Success, double GateDelayScanStep) ReadGateDelayScanStep(int gateIndex)
        {
            return ReadDoubleAfterSendCommand("GY", gateIndex);
        }

        /// <summary>
        /// GD i,t Set GATE i DELAY to 0 &lt;= t &lt;= 999.2E-3 s.
        /// </summary>
        /// <param name="gateIndex"></param>
        /// <param name="delaySec"></param>
        /// <returns></returns>
        private bool SetGateDelay(int gateIndex, double delaySec)
        {
            return SendCommand("GD", gateIndex, delaySec).Success;
        }

        /// <summary>
        /// GD i Read GATE i DELAY in seconds.
        /// </summary>
        /// <param name="gateIndex"></param>
        /// <returns></returns>
        private (bool Success, double GateDelay) ReadGateDelay(int gateIndex)
        {
            return ReadDoubleAfterSendCommand("GD", gateIndex);
        }

        /// <summary>
        /// GZ i Read current GATE i DELAY position (during a scan).
        /// </summary>
        /// <param name="gateIndex"></param>
        /// <returns></returns>
        private (bool Success, double CurrentGateDelay) ReadCurrentGateDelay(int gateIndex)
        {
            return ReadDoubleAfterSendCommand("GZ", gateIndex);
        }

        /// <summary>
        /// GW i,t Set GATE i WIDTH to 0.005E-6 &lt;= t &lt;= 999.2E-3 s.
        /// </summary>
        /// <param name="gateIndex"></param>
        /// <param name="gateWidthSec"></param>
        /// <returns></returns>
        private bool SetGateWidth(int gateIndex, double gateWidthSec)
        {
            return SendCommand("GW", gateIndex, gateWidthSec).Success;
        }

        /// <summary>
        /// GW i Read GATE i WIDTH in seconds.
        /// </summary>
        /// <param name="gateIndex"></param>
        /// <returns></returns>
        private (bool Success, double GateWidthSec) ReadGateWidth(int gateIndex)
        {
            return ReadDoubleAfterSendCommand("GW", gateIndex);
        }

        //==============================================
        // Front panel
        //==============================================

        /// <summary>
        /// CS Count start, same as START key.
        /// </summary>
        /// <returns></returns>
        private bool StartCount()
        {
            return SendCommand("CS").Success;
        }

        /// <summary>
        /// CH Count pause, same as STOP key while counting.
        /// </summary>
        /// <returns></returns>
        private bool PauseCount()
        {
            return SendCommand("CH").Success;
        }

        /// <summary>
        /// CR Count reset, same as STOP key pressed twice.
        /// </summary>
        /// <returns></returns>
        private bool ResetCounter()
        {
            return SendCommand("CR").Success;
        }

        /// <summary>
        /// CK j Simulate key press j.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private bool SimulateKeyPress(int key)
        {
            return SendCommand("CK", key).Success;
        }

        /// <summary>
        /// RR Rotate knob right (CW or UP) 1 step.
        /// </summary>
        /// <returns></returns>
        private bool RotateKnobRight()
        {
            return SendCommand("RR").Success;
        }

        /// <summary>
        /// RL Rotate knob left(CCW or DOWN) 1 step.
        /// </summary>
        /// <returns></returns>
        private bool RotateKnobLeft()
        {
            return SendCommand("RL").Success;
        }

        /// <summary>
        /// SC Read cursor position.Left(0), right(1), or inactive(2).
        /// </summary>
        /// <returns></returns>
        private (bool Success, int CursorPosition) ReadCursorPosition()
        {
            return ReadIntAfterSendCommand("SC");
        }

        /// <summary>
        /// MI j Set front panel to mode j; local(0), remote(1), locked-out(2). RS-232 only.
        /// </summary>
        /// <param name="modeIndex"></param>
        /// <returns></returns>
        private bool SetFrontPanelMode(int modeIndex)
        {
            return SendCommand("MI", modeIndex).Success;
        }

        /// <summary>
        /// MS string Display string on menu line.
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        private bool DisplayStringOnMenuLine(string label)
        {
            return SendCommand("MS", label).Success;
        }

        /// <summary>
        /// MS Return menu line to normal display.
        /// </summary>
        /// <returns></returns>
        private bool ReturnMenuLineToNormalDisplay()
        {
            return SendCommand("MS").Success;
        }

        /// <summary>
        /// MD j, k Display line k of menu j.
        /// </summary>
        /// <param name="menuIndex"></param>
        /// <param name="lineIndex"></param>
        /// <returns></returns>
        private bool DisplayLineOfMenu(int menuIndex, int lineIndex)
        {
            return SendCommand("MD", menuIndex, lineIndex).Success;
        }

        /// <summary>
        /// MM Read menu number of display (j).
        /// </summary>
        /// <returns></returns>
        private (bool Success, int MenuNumberOfDisplay) ReadMenuNumberOfDisplay()
        {
            return ReadIntAfterSendCommand("MM");
        }

        /// <summary>
        ///  ML Read menu line of display (k).
        /// </summary>
        /// <returns></returns>
        private (bool Success, int MenuLineOfDisplay) ReadMenuLineOfDisplay()
        {
            return ReadIntAfterSendCommand("ML");
        }

        //==============================================
        // Store / Recall
        //==============================================

        /// <summary>
        /// ST m Store instrument settings to location m(1 to 9).
        /// </summary>
        /// <param name="locationIndex"></param>
        /// <returns></returns>
        private bool StoreInstrumentSettingsToLocation(int locationIndex)
        {
            locationIndex = Math.Min(9, Math.Max(1, locationIndex));
            return SendCommand("ST", locationIndex).Success;
        }

        /// <summary>
        /// RC m Recall instrument settings from location m(1 to 9).
        /// </summary>
        /// <param name="locationIndex"></param>
        /// <returns></returns>
        private bool RecallInstrumentSettingsFromLocation(int locationIndex)
        {
            locationIndex = Math.Min(9, Math.Max(1, locationIndex));
            return SendCommand("RC", locationIndex).Success;
        }

        /// <summary>
        /// RC 0 Recall default settings
        /// </summary>
        /// <returns></returns>
        private bool RecallDefaultSettings()
        {
            return SendCommand("RC", 0).Success;
        }

        //==============================================
        // Interface
        //==============================================

        /// <summary>
        /// CL Clear instrument.
        /// </summary>
        /// <returns></returns>
        private bool ClearInstrument()
        {
            return SendCommand("CL").Success;
        }

        /// <summary>
        /// SS Read status byte.<br/>
        /// Bit Description<br/>
        /// 0 Parameter changed<br/>
        /// 1 Data ready<br/>
        /// 2 Scan finished<br/>
        /// 3 Counter overflow<br/>
        /// 4 Rate error<br/>
        /// 5 Recall error<br/>
        /// 6 Service request<br/>
        /// 7 Command error
        /// </summary>
        /// <returns></returns>
        private (bool Success, int PrimaryStatusByte) ReadPrimaryStatusByte()
        {
            return ReadIntAfterSendCommand("SS");
        }

        /// <summary>
        /// SS j Read bit j(0-7) of status byte.<br/>
        /// Bit Description<br/>
        /// 0 Parameter changed<br/>
        /// 1 Data ready<br/>
        /// 2 Scan finished<br/>
        /// 3 Counter overflow<br/>
        /// 4 Rate error<br/>
        /// 5 Recall error<br/>
        /// 6 Service request<br/>
        /// 7 Command error
        /// </summary>
        /// <param name="bitIndex"></param>
        /// <returns></returns>
        private (bool Success, int PrimaryStatusBit) ReadSpecificPrimaryStatusBit(int bitIndex)
        {
            bitIndex = Math.Min(7, Math.Max(0, bitIndex));
            return ReadIntAfterSendCommand("SS", bitIndex);
        }

        /// <summary>
        /// SI Read secondary status byte.<br/>
        /// Bit Description<br/>
        /// 0 Triggered<br/>
        /// 1 Inhibited<br/>
        /// 2 Counting<br/>
        /// 3-7 Unused
        /// </summary>
        /// <returns></returns>
        private (bool Success, int SecondaryStatusByte) ReadSecondaryStatusByte()
        {
            return ReadIntAfterSendCommand("SI");
        }

        /// <summary>
        /// SI j Read bit j(0-2) of secondary status byte.<br/>
        /// Bit Description<br/>
        /// 0 Triggered<br/>
        /// 1 Inhibited<br/>
        /// 2 Counting<br/>
        /// 3-7 Unused
        /// </summary>
        /// <param name="bitIndex"></param>
        /// <returns></returns>
        private (bool Success, int SecondaryStatusBit) ReadSpecificSecondaryStatusBit(int bitIndex)
        {
            bitIndex = Math.Min(2, Math.Max(0, bitIndex));
            return ReadIntAfterSendCommand("SI", bitIndex);
        }

        /// <summary>
        /// SV m Set GPIB SRQ mask to 0 <= m <= 255.
        /// </summary>
        /// <param name="mask"></param>
        /// <returns></returns>
        private bool SetGpibSrqMask(int mask)
        {
            return SendCommand("SV", mask).Success;
        }

        /// <summary>
        /// SV Read GPIB SRQ mask.
        /// </summary>
        /// <returns></returns>
        private (bool Success, int GpibSrqMask) ReadGpibSrqMask()
        {
            return ReadIntAfterSendCommand("SV");
        }

        /// <summary>
        /// SW m Set RS-232 character wait interval to m*3.33 ms 0 <= m <= 25. RS-232 only.
        /// </summary>
        /// <param name="waitIntervalMultiplier"></param>
        /// <returns></returns>
        private bool SetWaitInterval(int waitIntervalMultiplier)
        {
            return SendCommand("SW", waitIntervalMultiplier).Success;
        }

        /// <summary>
        /// SW Read RS-232 character wait interval as m, where interval = m*3.33 ms, 0 <= m <= 25. RS-232 only.
        /// </summary>
        /// <returns></returns>
        private (bool Success, int WaitInterval) ReadWaitInterval()
        {
            return ReadIntAfterSendCommand("SW");
        }

        /// <summary>
        /// SE j, k, l, m Set RS-232 terminator sequence to j, k, l, m (ASCII codes). RS-232 only.
        /// </summary>
        /// <param name="asciiCode1"></param>
        /// <param name="asciiCode2"></param>
        /// <param name="asciiCode3"></param>
        /// <param name="asciiCode4"></param>
        /// <returns></returns>
        private bool SetRs232TerminatorSequence(int asciiCode1, int asciiCode2, int asciiCode3, int asciiCode4)
        {
            return SendCommand("SE", asciiCode1, asciiCode2, asciiCode3, asciiCode4).Success;
        }

        /// <summary>
        /// SE Clear RS-232 terminator sequence to defaults.RS-232 only.
        /// </summary>
        /// <param name="mask"></param>
        /// <returns></returns>
        private bool ClearRs232TerminatorSequenceToDefaults()
        {
            return SendCommand("SE").Success;
        }

        //==============================================
        // Data
        //==============================================

        /// <summary>
        /// QA Read last count in counter A.<br/><br/>
        /// The QA command reads the most recent complete
        /// data point from counter A, QB reads the most recent data point from counter B.QA and QB
        /// commands should only be sent after checking the Data Ready status bit. This bit is set at the end of
        /// each complete count period and signals the availability of valid data. The Data Ready status
        /// bit is reset after it is read.Sending QA or QB commands without polling the Data Ready status
        /// can cause data points to be read multiple times. Note that QA and QB do not reset the Data Ready status.<br/><br/>
        /// If data is not ready, the QA and QB commands return -1. If counter B is preset, QB returns -1.
        /// </summary>
        /// <returns></returns>
        private (bool Success, int Count) ReadLastCountInCounterA()
        {
            return ReadIntAfterSendCommand("QA");
        }

        /// <summary>
        /// QB Read last count in counter B.<br/><br/>
        /// The QB command reads the most recent complete
        /// data point from counter A, QB reads the most recent data point from counter B.QA and QB
        /// commands should only be sent after checking the Data Ready status bit. This bit is set at the end of
        /// each complete count period and signals the availability of valid data. The Data Ready status
        /// bit is reset after it is read.Sending QA or QB commands without polling the Data Ready status
        /// can cause data points to be read multiple times. Note that QA and QB do not reset the Data Ready status.<br/><br/>
        /// If data is not ready, the QA and QB commands return -1. If counter B is preset, QB returns -1.
        /// </summary>
        /// <returns></returns>
        private (bool Success, int Count) ReadLastCountInCounterB()
        {
            return ReadIntAfterSendCommand("QB");
        }

        /// <summary>
        /// QA m Read from scan buffer point m(1-2000) for counter A.
        /// </summary>
        /// <param name="pointIndex"></param>
        /// <returns></returns>
        private (bool Success, int Count) ReadFromScanBufferPointForCounterA(int pointIndex)
        {
            return ReadIntAfterSendCommand("QA", pointIndex);
        }

        /// <summary>
        /// QB m Read from scan buffer point m(1-2000) for counter B.
        /// </summary>
        /// <param name="pointIndex"></param>
        /// <returns></returns>
        private (bool Success, int Count) ReadFromScanBufferPointForCounterB(int pointIndex)
        {
            return ReadIntAfterSendCommand("QB", pointIndex);
        }

        /// <summary>
        /// EA Send entire counter A buffer.
        /// </summary>
        /// <returns></returns>
        private bool SendEntireCounterABuffer()
        {
            return SendCommand("EA").Success;
        }

        /// <summary>
        /// EB Send entire counter B buffer.
        /// </summary>
        /// <returns></returns>
        private bool SendEntireCounterBBuffer()
        {
            return SendCommand("EB").Success;
        }

        /// <summary>
        /// ET Send entire counter A and B buffer.
        /// </summary>
        /// <returns></returns>
        private bool SendEntireCounterAandBBuffer()
        {
            return SendCommand("ET").Success;
        }

        /// <summary>
        /// FA Start scan and send N PERIODS data points from counter A.
        /// </summary>
        /// <returns></returns>
        private bool StartScanAndSendNPeriodsDataPointsFromCounterA()
        {
            return SendCommand("FA").Success;
        }

        /// <summary>
        /// FB Start scan and send N PERIODS data points from counter B.
        /// </summary>
        /// <returns></returns>
        private bool StartScanAndSendNPeriodsDataPointsFromCounterB()
        {
            return SendCommand("FB").Success;
        }

        /// <summary>
        /// FT Start scan and send N PERIODS data points from both counters.
        /// </summary>
        /// <returns></returns>
        private bool StartScanAndSendNPeriodsDataPointsFromBothCounters()
        {
            return SendCommand("FT").Success;
        }

        /// <summary>
        /// XA Read current contents of counter A.<br/>
        /// The X commands read the counter contents regardless of the count state.An X command sent 
        /// while counting returns the current counter contents
        /// </summary>
        /// <returns></returns>
        private (bool Success, int Count) ReadCurrentContentsOfCounterA()
        {
            return ReadIntAfterSendCommand("XA");
        }

        /// <summary>
        /// XB Read current contents of counter B.
        /// The X commands read the counter contents regardless of the count state.An X command sent 
        /// while counting returns the current counter contents
        /// </summary>
        /// <returns></returns>
        private (bool Success, int Count) ReadCurrentContentsOfCounterB()
        {
            return ReadIntAfterSendCommand("XB");
        }

        //==============================================
        // Commands, Base
        //==============================================

        //p. 50
        public (bool Success, string Response) SendCommandsOC(string Command)
        {
            (bool success, string response) result = (false, "");
            WaitForAccess();
            if (OpenConnection())
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                result = SendCommandRaw(Command, Command.Contains(','));
                stopwatch.Stop();
                result.response += " || " + stopwatch.Elapsed.TotalMilliseconds;
                CloseConnection();
            }
            OpenAccess();
            return result;
        }

        private (bool Success, int Value) ReadIntAfterSendCommand(string commandCode, params object[] parameters)
        {
            var result = SendCommand(commandCode, true, parameters);
            double.TryParse(result.Response, out double value); //because int.TryParse(...) 1E7 does not read 
            return (result.Success, (int)value);
        }

        private (bool Success, double Value) ReadDoubleAfterSendCommand(string commandCode, params object[] parameters)
        {
            var result = SendCommand(commandCode, true, parameters);
            double.TryParse(result.Response, out double value);
            return (result.Success, value);
        }

        private (bool Success, string Response) SendCommand(string commandCode, params object[] parameters)
        {
            return SendCommand(commandCode, false, parameters);
        }

        /// <summary>
        /// A bit expensive solution with boxing and unboxing
        /// </summary>
        /// <param name="commandCode"></param>
        /// <param name="read"></param>
        /// <param name="minPacketLength"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private (bool Success, string Response) SendCommand(string commandCode, bool read = false, params object[] parameters)
        {
            StringBuilder command = new StringBuilder(commandCode);
            int pNr = parameters.Length;
            for (int i = 0; i < pNr; i++)
            {
                if (i == 0)
                    command.Append(' ');
                else
                    command.Append(',');
                object p = parameters[i];
                switch (p)
                {
                    case double dNumber:
                        command.Append(ToScientific(dNumber));
                        break;
                    default:
                        command.Append(p.ToString());
                        break;
                }
            }
            //command.Append(';');
            return SendCommandRaw(command.ToString(), read);
        }

        private string ToScientific(double value)
        {
            return value.ToString("E5", CultureInfo.InvariantCulture);
        }

        private const string _closingCharacters = "\r";

        private (bool Success, string Response) SendCommandRaw(string command, bool read = false) //no error checking 
        {
            (bool Success, string Response) response = (false, "");
            Write(command + "\r");
            if (read) //the answer must contain '\r' in the end
            {
                response = ReadUntilClosingCharacters(_closingCharacters);
                response.Response = Regex.Replace(response.Response, @"(\n|\r)", "");
            }
            return response;
        }
    }
}
