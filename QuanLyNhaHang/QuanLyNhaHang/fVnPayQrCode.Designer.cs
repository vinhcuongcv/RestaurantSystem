namespace QuanLyNhaHang
{
    partial class fVnPayQrCode
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
            this.cbNganHang = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txbSoTaiKhoan = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbTemplate = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.nmMoney = new System.Windows.Forms.NumericUpDown();
            this.txbTenTaiKhoan = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txbThongTinThem = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnCreate = new System.Windows.Forms.Button();
            this.pbQR = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nmMoney)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbQR)).BeginInit();
            this.SuspendLayout();
            // 
            // cbNganHang
            // 
            this.cbNganHang.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cbNganHang.FormattingEnabled = true;
            this.cbNganHang.Location = new System.Drawing.Point(21, 96);
            this.cbNganHang.Name = "cbNganHang";
            this.cbNganHang.Size = new System.Drawing.Size(257, 28);
            this.cbNganHang.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(18, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ngân hàng :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(319, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Số tài khoản : ";
            // 
            // txbSoTaiKhoan
            // 
            this.txbSoTaiKhoan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txbSoTaiKhoan.Location = new System.Drawing.Point(322, 96);
            this.txbSoTaiKhoan.Multiline = true;
            this.txbSoTaiKhoan.Name = "txbSoTaiKhoan";
            this.txbSoTaiKhoan.Size = new System.Drawing.Size(273, 33);
            this.txbSoTaiKhoan.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(640, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "Template :";
            // 
            // cbTemplate
            // 
            this.cbTemplate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cbTemplate.FormattingEnabled = true;
            this.cbTemplate.Items.AddRange(new object[] {
            "compact",
            "compact2",
            "qr_only",
            "print"});
            this.cbTemplate.Location = new System.Drawing.Point(643, 96);
            this.cbTemplate.Name = "cbTemplate";
            this.cbTemplate.Size = new System.Drawing.Size(257, 28);
            this.cbTemplate.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.Location = new System.Drawing.Point(18, 154);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 18);
            this.label4.TabIndex = 6;
            this.label4.Text = "Số tiền :";
            // 
            // nmMoney
            // 
            this.nmMoney.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.nmMoney.Location = new System.Drawing.Point(21, 187);
            this.nmMoney.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.nmMoney.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nmMoney.Name = "nmMoney";
            this.nmMoney.Size = new System.Drawing.Size(257, 27);
            this.nmMoney.TabIndex = 7;
            this.nmMoney.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // txbTenTaiKhoan
            // 
            this.txbTenTaiKhoan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txbTenTaiKhoan.Location = new System.Drawing.Point(322, 184);
            this.txbTenTaiKhoan.Multiline = true;
            this.txbTenTaiKhoan.Name = "txbTenTaiKhoan";
            this.txbTenTaiKhoan.Size = new System.Drawing.Size(273, 33);
            this.txbTenTaiKhoan.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label5.Location = new System.Drawing.Point(319, 156);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 18);
            this.label5.TabIndex = 8;
            this.label5.Text = "Tên tài khoản :";
            // 
            // txbThongTinThem
            // 
            this.txbThongTinThem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txbThongTinThem.Location = new System.Drawing.Point(643, 184);
            this.txbThongTinThem.Multiline = true;
            this.txbThongTinThem.Name = "txbThongTinThem";
            this.txbThongTinThem.Size = new System.Drawing.Size(257, 33);
            this.txbThongTinThem.TabIndex = 11;
            this.txbThongTinThem.Text = "THANH TOÁN HÓA ĐƠN";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label6.Location = new System.Drawing.Point(640, 156);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(118, 18);
            this.label6.TabIndex = 10;
            this.label6.Text = "Thông tin thêm : ";
            // 
            // btnCreate
            // 
            this.btnCreate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(99)))), ((int)(((byte)(102)))));
            this.btnCreate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnCreate.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnCreate.Location = new System.Drawing.Point(21, 415);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(205, 67);
            this.btnCreate.TabIndex = 12;
            this.btnCreate.Text = "CREATE QRCODE";
            this.btnCreate.UseVisualStyleBackColor = false;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // pbQR
            // 
            this.pbQR.Location = new System.Drawing.Point(473, 243);
            this.pbQR.Name = "pbQR";
            this.pbQR.Size = new System.Drawing.Size(427, 417);
            this.pbQR.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbQR.TabIndex = 13;
            this.pbQR.TabStop = false;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(99)))), ((int)(((byte)(102)))));
            this.label7.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label7.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label7.Location = new System.Drawing.Point(-6, -2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(930, 56);
            this.label7.TabIndex = 14;
            this.label7.Text = "QR CODE VNPAY";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fVnPayQrCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 681);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.pbQR);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.txbThongTinThem);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txbTenTaiKhoan);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.nmMoney);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbTemplate);
            this.Controls.Add(this.txbSoTaiKhoan);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbNganHang);
            this.Name = "fVnPayQrCode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "fVnPayQrCode";
            ((System.ComponentModel.ISupportInitialize)(this.nmMoney)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbQR)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbNganHang;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txbSoTaiKhoan;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbTemplate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nmMoney;
        private System.Windows.Forms.TextBox txbTenTaiKhoan;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txbThongTinThem;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.PictureBox pbQR;
        private System.Windows.Forms.Label label7;
    }
}