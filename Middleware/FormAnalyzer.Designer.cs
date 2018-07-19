namespace Middleware
{
    partial class FormAnalyzer
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
            this.components = new System.ComponentModel.Container();
            this.cmbPort = new System.Windows.Forms.ComboBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.txtBaudRate = new System.Windows.Forms.TextBox();
            this.txtBitLength = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtStopBits = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtParity = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtToSent = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.BtnEnq = new System.Windows.Forms.Button();
            this.btnDimFirstPolMessage = new System.Windows.Forms.Button();
            this.BtnDimConversationalPollMessage = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmbPort
            // 
            this.cmbPort.FormattingEnabled = true;
            this.cmbPort.Location = new System.Drawing.Point(101, 17);
            this.cmbPort.Name = "cmbPort";
            this.cmbPort.Size = new System.Drawing.Size(154, 21);
            this.cmbPort.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Baud Rate";
            // 
            // txtBaudRate
            // 
            this.txtBaudRate.Location = new System.Drawing.Point(101, 47);
            this.txtBaudRate.Name = "txtBaudRate";
            this.txtBaudRate.Size = new System.Drawing.Size(154, 20);
            this.txtBaudRate.TabIndex = 6;
            // 
            // txtBitLength
            // 
            this.txtBitLength.Location = new System.Drawing.Point(101, 73);
            this.txtBitLength.Name = "txtBitLength";
            this.txtBitLength.Size = new System.Drawing.Size(154, 20);
            this.txtBitLength.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Bit Length";
            // 
            // txtStopBits
            // 
            this.txtStopBits.Location = new System.Drawing.Point(101, 99);
            this.txtStopBits.Name = "txtStopBits";
            this.txtStopBits.Size = new System.Drawing.Size(154, 20);
            this.txtStopBits.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 106);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Stop Bits";
            // 
            // txtParity
            // 
            this.txtParity.Location = new System.Drawing.Point(101, 125);
            this.txtParity.Name = "txtParity";
            this.txtParity.Size = new System.Drawing.Size(154, 20);
            this.txtParity.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 132);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Parity";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(55, 352);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(97, 23);
            this.btnClear.TabIndex = 26;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClearStatus_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(180, 151);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 28;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click_1);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(101, 151);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 27;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.BtnOpen_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "Port";
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(270, 46);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtStatus.Size = new System.Drawing.Size(522, 329);
            this.txtStatus.TabIndex = 34;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(267, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 35;
            this.label2.Text = "Status";
            // 
            // txtToSent
            // 
            this.txtToSent.Location = new System.Drawing.Point(8, 180);
            this.txtToSent.Multiline = true;
            this.txtToSent.Name = "txtToSent";
            this.txtToSent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtToSent.Size = new System.Drawing.Size(247, 166);
            this.txtToSent.TabIndex = 36;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(158, 352);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(97, 23);
            this.btnSend.TabIndex = 37;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.BtnSend_Click);
            // 
            // BtnEnq
            // 
            this.BtnEnq.Location = new System.Drawing.Point(798, 46);
            this.BtnEnq.Name = "BtnEnq";
            this.BtnEnq.Size = new System.Drawing.Size(196, 23);
            this.BtnEnq.TabIndex = 38;
            this.BtnEnq.Text = "Enq";
            this.BtnEnq.UseVisualStyleBackColor = true;
            this.BtnEnq.Click += new System.EventHandler(this.BtnEnq_Click);
            // 
            // btnDimFirstPolMessage
            // 
            this.btnDimFirstPolMessage.Location = new System.Drawing.Point(798, 75);
            this.btnDimFirstPolMessage.Name = "btnDimFirstPolMessage";
            this.btnDimFirstPolMessage.Size = new System.Drawing.Size(196, 23);
            this.btnDimFirstPolMessage.TabIndex = 39;
            this.btnDimFirstPolMessage.Text = "Dim First Poll Message";
            this.btnDimFirstPolMessage.UseVisualStyleBackColor = true;
            this.btnDimFirstPolMessage.Click += new System.EventHandler(this.BtnDimFirstPolMessage_Click);
            // 
            // BtnDimConversationalPollMessage
            // 
            this.BtnDimConversationalPollMessage.Location = new System.Drawing.Point(798, 104);
            this.BtnDimConversationalPollMessage.Name = "BtnDimConversationalPollMessage";
            this.BtnDimConversationalPollMessage.Size = new System.Drawing.Size(196, 23);
            this.BtnDimConversationalPollMessage.TabIndex = 40;
            this.BtnDimConversationalPollMessage.Text = "Dim Conversational Poll Message";
            this.BtnDimConversationalPollMessage.UseVisualStyleBackColor = true;
            this.BtnDimConversationalPollMessage.Click += new System.EventHandler(this.BtnDimConversationalPollMessage_Click);
            // 
            // FormAnalyzer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 387);
            this.Controls.Add(this.BtnDimConversationalPollMessage);
            this.Controls.Add(this.btnDimFirstPolMessage);
            this.Controls.Add(this.BtnEnq);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtToSent);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.txtParity);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtStopBits);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtBitLength);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtBaudRate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbPort);
            this.Name = "FormAnalyzer";
            this.Text = "Analyzer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormDimensionSettings_FormClosing);
            this.Load += new System.EventHandler(this.FormDimensionSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cmbPort;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBaudRate;
        private System.Windows.Forms.TextBox txtBitLength;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtStopBits;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtParity;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtToSent;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button BtnEnq;
        private System.Windows.Forms.Button btnDimFirstPolMessage;
        private System.Windows.Forms.Button BtnDimConversationalPollMessage;
    }
}