namespace WindowsFormsApp_Bai3
{
    partial class FormComPort
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
            this.cboComPort = new System.Windows.Forms.ComboBox();
            this.btnKetNoi = new System.Windows.Forms.Button();
            this.txtMaNhanDuoc = new System.Windows.Forms.TextBox();
            this.dgvKetQua = new System.Windows.Forms.DataGridView();
            this.lblThongBao = new System.Windows.Forms.Label();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.serialPort2 = new System.IO.Ports.SerialPort(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKetQua)).BeginInit();
            this.SuspendLayout();
            // 
            // cboComPort
            // 
            this.cboComPort.FormattingEnabled = true;
            this.cboComPort.Location = new System.Drawing.Point(282, 111);
            this.cboComPort.Name = "cboComPort";
            this.cboComPort.Size = new System.Drawing.Size(121, 24);
            this.cboComPort.TabIndex = 0;
            // 
            // btnKetNoi
            // 
            this.btnKetNoi.Location = new System.Drawing.Point(444, 112);
            this.btnKetNoi.Name = "btnKetNoi";
            this.btnKetNoi.Size = new System.Drawing.Size(75, 23);
            this.btnKetNoi.TabIndex = 1;
            this.btnKetNoi.Text = "Kết nối";
            this.btnKetNoi.UseVisualStyleBackColor = true;
            // 
            // txtMaNhanDuoc
            // 
            this.txtMaNhanDuoc.Location = new System.Drawing.Point(282, 154);
            this.txtMaNhanDuoc.Multiline = true;
            this.txtMaNhanDuoc.Name = "txtMaNhanDuoc";
            this.txtMaNhanDuoc.Size = new System.Drawing.Size(237, 55);
            this.txtMaNhanDuoc.TabIndex = 2;
            // 
            // dgvKetQua
            // 
            this.dgvKetQua.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKetQua.Location = new System.Drawing.Point(119, 254);
            this.dgvKetQua.Name = "dgvKetQua";
            this.dgvKetQua.RowHeadersWidth = 51;
            this.dgvKetQua.RowTemplate.Height = 24;
            this.dgvKetQua.Size = new System.Drawing.Size(593, 150);
            this.dgvKetQua.TabIndex = 3;
            // 
            // lblThongBao
            // 
            this.lblThongBao.AutoSize = true;
            this.lblThongBao.Location = new System.Drawing.Point(142, 224);
            this.lblThongBao.Name = "lblThongBao";
            this.lblThongBao.Size = new System.Drawing.Size(44, 16);
            this.lblThongBao.TabIndex = 5;
            this.lblThongBao.Text = "label2";
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Location = new System.Drawing.Point(552, 115);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(44, 16);
            this.lblTrangThai.TabIndex = 6;
            this.lblTrangThai.Text = "label1";
            // 
            // FormComPort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblTrangThai);
            this.Controls.Add(this.lblThongBao);
            this.Controls.Add(this.dgvKetQua);
            this.Controls.Add(this.txtMaNhanDuoc);
            this.Controls.Add(this.btnKetNoi);
            this.Controls.Add(this.cboComPort);
            this.Name = "FormComPort";
            this.Text = "Comp";
            ((System.ComponentModel.ISupportInitialize)(this.dgvKetQua)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboComPort;
        private System.Windows.Forms.Button btnKetNoi;
        private System.Windows.Forms.TextBox txtMaNhanDuoc;
        private System.Windows.Forms.DataGridView dgvKetQua;
        private System.IO.Ports.SerialPort MyPort;
        private System.Windows.Forms.Label lblThongBao;
        private System.Windows.Forms.Label lblTrangThai;
        private System.IO.Ports.SerialPort serialPort1;
        private System.IO.Ports.SerialPort serialPort2;
    }
}