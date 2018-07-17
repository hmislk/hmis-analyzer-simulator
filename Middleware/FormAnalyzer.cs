using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net;
using System.Web;

namespace Middleware
{
    public partial class FormAnalyzer : Form
    {

        #region variables

        SerialPort com = new SerialPort();
        string status = "";
        List<byte> message = new List<byte>();

        #endregion

        #region mainFunctions

        private void com_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int bytes = com.BytesToRead;
            byte[] buffer = new byte[bytes];
            com.Read(buffer, 0, bytes);
            message.AddRange(buffer);

            foreach (Byte b in buffer)
            {
                //status += (char)b;
                if (b == ByteEnq())
                {
                    com.Write(Ack());
                    status += DateTime.Now.ToString("dd/MMM/yy H:mm") + " Received <ENQ>. <ACK> sent." + Environment.NewLine;
                    message = new List<byte>();
                    this.Invoke(new EventHandler(DisplayText));
                }
                else if (b == ByteAck())
                {
                    status += DateTime.Now.ToString("dd/MMM/yy H:mm") + " Received <ACK>." + Environment.NewLine;
                    message = new List<byte>();
                    this.Invoke(new EventHandler(DisplayText));
                }
                else if (b == ByteEot() || b == ByteEtx())
                {
                    status += DateTime.Now.ToString("dd/MMM/yy H:mm") + " Received a message from Analyzer." + Environment.NewLine;
                    status += BytesToString(message) + Environment.NewLine;
                    this.Invoke(new EventHandler(DisplayText));
                    message = new List<byte>();
                    com.Write(Ack());
                }
            }
        }


        #endregion

        #region getDate

        private String Cr()
        {
            return Character(ByteCr());
        }


        private String Etb()
        {
            return Character(ByteEtb());
        }

        private String Lf()
        {
            return Character(ByteLf());
        }

        private String Ack()
        {
            return Character(ByteAck());
        }

        private String Nak()
        {
            return Character(ByteNak());
        }

        private String Stx()
        {
            return Character(ByteStx());
        }

        private String Etx()
        {
            return Character(ByteEtx());
        }

        private String Enq()
        {
            return Character(ByteEnq());
        }

        public byte ByteEnq()
        {
            return 5;
        }

        public byte ByteStx()
        {
            return 2;
        }

        public byte ByteAck()
        {
            return 6;
        }

        public byte ByteEtx()
        {
            return 3;
        }

        public byte ByteEot()
        {
            return 4;
        }

        public byte ByteNak()
        {
            return 21;
        }


        public byte ByteFs()
        {
            return 28;
        }

        public byte ByteLf()
        {
            return 10;
        }

        public byte ByteCr()
        {
            return 13;
        }

        public byte ByteEtb()
        {
            return 23;
        }

        public byte ByteGs()
        {
            return 29;
        }

        public byte ByteRs()
        {
            return 30;
        }

        public byte ByteUs()
        {
            return 31;
        }


        #endregion

        #region supportiveFunctions

        private String BytesToString(List<byte> bytesToConvert)
        {
            Byte[] temBytes = bytesToConvert.ToArray();
            String temStr = "";
            temStr = Encoding.ASCII.GetString(temBytes, 0, temBytes.Length);
            return temStr;
        }

        private String Character(int charNo)
        {
            char ack = (char)charNo;
            String m = ack.ToString();
            return m;
        }

        private String ResultAcceptanceMessage()
        {
            char stx = (char)2;
            char etx = (char)2;
            char fx = (char)63;

            String m = stx + "M" + fx + "A" + fx + fx + "E2" + etx;

            return m;
        }

        private String NoRequestMessage()
        {
            char stx = (char)2;
            char etx = (char)3;
            char fx = (char)63;
            //<STX>N<FS>6A<ETX>
            String m = stx + "N" + fx + "6A" + etx;
            return m;
        }

        private byte[] StringsToBytes(String value)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(value);
            return bytes;
        }

        private String StringToStringOfBytes(String input)
        {
            String s = "";
            foreach (char c in input)
            {
                s += (int)c + "|";
            }
            return s;
        }


        private String ExtractMessageFromHtml(String html)
        {
            String s = html;
            int pFrom = s.IndexOf("#{") + "#{".Length;
            int pTo = s.LastIndexOf("}");
            String result = s.Substring(pFrom, pTo - pFrom);
            return result;
        }

        #endregion

        #region FormEvents

        private void DisplayText(object sender, EventArgs e)
        {
            DisplayText();
        }

        private void DisplayText()
        {

            txtStatus.Text = status;

        }

        public FormAnalyzer()
        {
            InitializeComponent();
        }

        private void FormDimensionSettings_Load(object sender, EventArgs e)
        {
            String[] ports = SerialPort.GetPortNames();
            cmbPort.Items.AddRange(ports);
            cmbPort.SelectedIndex = 0;
            btnClose.Enabled = false;

            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", true);

            key.CreateSubKey("Middleware");
            key = key.OpenSubKey("Middleware", true);


            key.CreateSubKey("Analyzer");
            key = key.OpenSubKey("Analyzer", true);



            if (key == null)
            {
                cmbPort.Text = "";
            }
            else
            {
                cmbPort.Text = (String)key.GetValue("Port");
                txtBaudRate.Text = (String)key.GetValue("BaudRate", "9600");
                txtBitLength.Text = (String)key.GetValue("BitLength", "8");
                txtStopBits.Text = (String)key.GetValue("StopBits", "One");
                txtParity.Text = (String)key.GetValue("Parity", "NONE");
            }




        }

        private void FormDimensionSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", true);

            key.CreateSubKey("Middleware");
            key = key.OpenSubKey("Middleware", true);


            key.CreateSubKey("Analyzer");
            key = key.OpenSubKey("Analyzer", true);

            key.SetValue("Port", cmbPort.Text);
            key.SetValue("BaudRate", txtBaudRate.Text);
            key.SetValue("Parity", txtParity.Text);
            key.SetValue("StopBits", txtStopBits.Text);


        }

        private void btnListStatus_Click(object sender, EventArgs e)
        {
            DisplayText();
        }

        private void btnClearStatus_Click(object sender, EventArgs e)
        {
            status = "";
            DisplayText();
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            btnOpen.Enabled = true;
            btnClose.Enabled = false;

            try
            {
                com.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClearMessages_Click(object sender, EventArgs e)
        {

            message = new List<byte>();
        }

        private void BtnOpen_Click(object sender, EventArgs e)
        {
            btnOpen.Enabled = false;
            btnClose.Enabled = true;
            try
            {
                com.PortName = cmbPort.Text;
                com.BaudRate = Int32.Parse(txtBaudRate.Text);
                com.DataBits = Int32.Parse(txtBitLength.Text);
                StopBits sb = StopBits.None;
                String strSb = txtStopBits.Text;

                if (strSb.Equals("None", StringComparison.InvariantCultureIgnoreCase))
                {
                    sb = StopBits.None;
                }
                else if (strSb.Equals("1", StringComparison.InvariantCultureIgnoreCase))
                {
                    sb = StopBits.One;
                }
                else if (strSb.Equals("One", StringComparison.InvariantCultureIgnoreCase))
                {
                    sb = StopBits.One;
                }
                else if (strSb.Equals("OnePointFive", StringComparison.InvariantCultureIgnoreCase))
                {
                    sb = StopBits.OnePointFive;
                }
                else if (strSb.Equals("Two", StringComparison.InvariantCultureIgnoreCase))
                {
                    sb = StopBits.Two;
                }
                else
                {
                    sb = StopBits.Two;
                }
                com.StopBits = StopBits.One;
                com.DtrEnable = true;
                com.RtsEnable = true;
                Parity p;
                String strParity = txtParity.Text;

                if (strSb.Equals("Even", StringComparison.InvariantCultureIgnoreCase))
                {
                    p = Parity.Even;
                }
                else if (strSb.Equals("Mark", StringComparison.InvariantCultureIgnoreCase))
                {
                    p = Parity.Mark;
                }
                else if (strSb.Equals("None", StringComparison.InvariantCultureIgnoreCase))
                {
                    p = Parity.None;
                }
                else if (strSb.Equals("Odd", StringComparison.InvariantCultureIgnoreCase))
                {
                    p = Parity.Odd;
                }
                else if (strSb.Equals("Space", StringComparison.InvariantCultureIgnoreCase))
                {
                    p = Parity.Space;
                }
                else
                {
                    p = Parity.None;
                }

                com.Parity = p;

                com.Open();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            com.DataReceived += new SerialDataReceivedEventHandler(com_DataReceived);
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            String tt = txtToSent.Text;
            String[] tts = tt.Split(new string[] { "+" }, StringSplitOptions.None);

            String temTxtToSent = "";
            foreach (String s in tts)
            {
                byte b = Byte.Parse(s);
                temTxtToSent += Character(b);
            }
            if (com.IsOpen)
            {
                com.Write(Stx() + temTxtToSent + Etx());
            }
        }


        private void BtnEnq_Click(object sender, EventArgs e)
        {
            com.Write(Enq());
            status += "Enq Sent";
        }

        #endregion

    }
}
