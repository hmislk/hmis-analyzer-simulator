﻿using Microsoft.Win32;
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
        SerialPort com = new SerialPort();
        string status = "";
        List<byte> messageInBytes = new List<byte>();

        

        private void com_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int bytes = com.BytesToRead;
            byte[] buffer = new byte[bytes];
            com.Read(buffer, 0, bytes);
            if (bytes == 1)
            {
                String temAscii = System.Text.Encoding.ASCII.GetString(new[] { buffer[0] });

                if (buffer[0] == byteEnq())
                {
                    status += " ENQ Received. ACK Sent";
                    com.Write(Ack());
                }

            }
            else
            {
                if (ContainEnd(buffer))
                {
                    thisMessage.AddRange(buffer);
                    allMessages.AddRange(thisMessage);
                    //SendDataToLimsAsync();
                    thisMessage = new List<byte>();
                    status += "End of a Message " + Environment.NewLine;
                }
                else
                {
                    thisMessage.AddRange(buffer);
                    status += "Part of a Message " + Environment.NewLine;
                }
            }
            this.Invoke(new EventHandler(DisplayText));
        }

        private Boolean ContainEnd(Byte[] bytesToCheck)
        {
            foreach (Byte b in bytesToCheck)
            {
                if (b == (byte)ASCII.ETX)
                {
                    return true;
                }
            }
            return false;
        }




        private async Task SendDataToLimsAsync()
        {
            status += "Trying to send Data to LIMS";
            foreach (String tm in msgsFromAnaToLims)
            {
                string longurl = url;
                var uriBuilder = new UriBuilder(longurl);

                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                query["username"] = txtUsername.Text;
                query["password"] = txtPassword.Text;

                query["msg"] = StringToStringOfBytes(tm);

                status += tm;

                uriBuilder.Query = query.ToString();
                longurl = uriBuilder.ToString();

                // var values = new Dictionary<string, string> { { "username", username }, { "password", password }, { "msg", msg } };
                // var content = new FormUrlEncodedContent(values);

                var response = await client.GetAsync(longurl);
                var responseString = await response.Content.ReadAsStringAsync();
                responseString = ExtractMessageFromHtml(responseString);
                status += "Received data from LIMS";
                msgsFromLimsToAna.Add(responseString);
                msgsFromAnaToLims.Remove(tm);
                this.Invoke(new EventHandler(DisplayText));
            }
        }


        #region functions

        private String EndOfChar()
        {
           return Etx();
        }

        private String Cr()
        {
            return Character(13);
        }


        private String Etb()
        {
            return Character(23);
        }

        private String Lf()
        {
            return Character(10);
        }

        private String Ack()
        {
            return Character(6);
        }

        private String Nak()
        {
            return Character(21);
        }

        private String Stx()
        {
            return Character(2);
        }

        private String Etx()
        {
            return Character(3);
        }

        private String Enq()
        {
            return Character(5);
        }

        private String Character(int charNo)
        {
            char ack = (char)charNo;
            String m = ack.ToString();
            return m;
        }

        

        private Boolean ContainsEndCharactor(String value, String endCharacter)
        {
            return value.Contains(endCharacter);
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

        private void FillMessages()
        {
            if (optBinary.Checked)
            {
                txtAnaToLims.Text = messagesBinary;
            }
            else
            {
                txtAnaToLims.Text = messagesString;
            }

        }

        #endregion


        #region FormEvents

        private byte byteEnq()
        {
            return 5;
        }

        private byte byteAck()
        {
            return 6;
        }

        private void DisplayText(object sender, EventArgs e)
        {
            DisplayText();
        }

        private void DisplayText()
        {
            txtLimsToAna.Text = "";
            txtAnaToLims.Text = "";
            foreach (String s in msgsFromAnaToLims)
            {
                txtAnaToLims.Text += s + Environment.NewLine;
            }
            foreach (String s in msgsFromLimsToAna)
            {
                txtLimsToAna.Text += s + Environment.NewLine;
            }
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


            key.CreateSubKey("Dimention");
            key = key.OpenSubKey("Dimention", true);



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
                txtUrl.Text = (String)key.GetValue("Url", "");
            }




        }


        private void FormDimensionSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", true);

            key.CreateSubKey("Middleware");
            key = key.OpenSubKey("Middleware", true);


            key.CreateSubKey("Dimention");
            key = key.OpenSubKey("Dimention", true);

            key.SetValue("Port", cmbPort.Text);
            key.SetValue("BaudRate", txtBaudRate.Text);
            key.SetValue("Parity", txtParity.Text);
            key.SetValue("StopBits", txtStopBits.Text);
            key.SetValue("Url", txtUrl.Text);

        }

        private void btnListStatus_Click(object sender, EventArgs e)
        {
            DisplayText();
        }

        private void btnClearStatus_Click(object sender, EventArgs e)
        {
            msgsFromAnaToLims = new List<string>();
            msgsFromLimsToAna = new List<string>();
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



        #endregion

        private void btnListMessages_Click(object sender, EventArgs e)
        {
            FillMessages();
        }

        private void btnClearMessages_Click(object sender, EventArgs e)
        {

            txtAnaToLims.Text = "";
            messagesString = "";
            messagesBinary = "";
            messageInBytes = new List<byte>();
        }

        private void btnOpen_Click_1(object sender, EventArgs e)
        {
            btnOpen.Enabled = false;
            btnClose.Enabled = true;
            url = txtUrl.Text;
            username = txtUsername.Text;
            password = txtPassword.Text;
            analyzer = Analyzer.SysMaxXsSeries;
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
    }
}
