namespace HospitalAutomation.Views.DoctorManagement
{
    partial class DoctorMedicalRecordsForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblPatient = new System.Windows.Forms.Label();
            this.cmbPatients = new System.Windows.Forms.ComboBox();
            this.lblPatientInfo = new System.Windows.Forms.Label();
            this.dgvMedicalRecords = new System.Windows.Forms.DataGridView();
            this.btnNewRecord = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.groupBoxPatient = new System.Windows.Forms.GroupBox();
            this.groupBoxRecords = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedicalRecords)).BeginInit();
            this.groupBoxPatient.SuspendLayout();
            this.groupBoxRecords.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(180)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(171, 26);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Týbbi Kayýtlar";
            // 
            // groupBoxPatient
            // 
            this.groupBoxPatient.Controls.Add(this.lblPatientInfo);
            this.groupBoxPatient.Controls.Add(this.cmbPatients);
            this.groupBoxPatient.Controls.Add(this.lblPatient);
            this.groupBoxPatient.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBoxPatient.Location = new System.Drawing.Point(20, 60);
            this.groupBoxPatient.Name = "groupBoxPatient";
            this.groupBoxPatient.Size = new System.Drawing.Size(950, 100);
            this.groupBoxPatient.TabIndex = 1;
            this.groupBoxPatient.TabStop = false;
            this.groupBoxPatient.Text = "Hasta Seçimi";
            // 
            // lblPatient
            // 
            this.lblPatient.AutoSize = true;
            this.lblPatient.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblPatient.Location = new System.Drawing.Point(20, 30);
            this.lblPatient.Name = "lblPatient";
            this.lblPatient.Size = new System.Drawing.Size(39, 15);
            this.lblPatient.TabIndex = 0;
            this.lblPatient.Text = "Hasta:";
            // 
            // cmbPatients
            // 
            this.cmbPatients.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPatients.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cmbPatients.FormattingEnabled = true;
            this.cmbPatients.Location = new System.Drawing.Point(80, 27);
            this.cmbPatients.Name = "cmbPatients";
            this.cmbPatients.Size = new System.Drawing.Size(300, 23);
            this.cmbPatients.TabIndex = 1;
            this.cmbPatients.SelectedIndexChanged += new System.EventHandler(this.CmbPatients_SelectedIndexChanged);
            // 
            // lblPatientInfo
            // 
            this.lblPatientInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblPatientInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblPatientInfo.Location = new System.Drawing.Point(20, 60);
            this.lblPatientInfo.Name = "lblPatientInfo";
            this.lblPatientInfo.Size = new System.Drawing.Size(900, 30);
            this.lblPatientInfo.TabIndex = 2;
            this.lblPatientInfo.Text = "Hasta seçiniz...";
            // 
            // groupBoxRecords
            // 
            this.groupBoxRecords.Controls.Add(this.btnExport);
            this.groupBoxRecords.Controls.Add(this.btnPrint);
            this.groupBoxRecords.Controls.Add(this.btnNewRecord);
            this.groupBoxRecords.Controls.Add(this.dgvMedicalRecords);
            this.groupBoxRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBoxRecords.Location = new System.Drawing.Point(20, 180);
            this.groupBoxRecords.Name = "groupBoxRecords";
            this.groupBoxRecords.Size = new System.Drawing.Size(950, 400);
            this.groupBoxRecords.TabIndex = 2;
            this.groupBoxRecords.TabStop = false;
            this.groupBoxRecords.Text = "Týbbi Kayýtlar";
            // 
            // dgvMedicalRecords
            // 
            this.dgvMedicalRecords.AllowUserToAddRows = false;
            this.dgvMedicalRecords.AllowUserToDeleteRows = false;
            this.dgvMedicalRecords.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMedicalRecords.BackgroundColor = System.Drawing.Color.White;
            this.dgvMedicalRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMedicalRecords.Location = new System.Drawing.Point(20, 30);
            this.dgvMedicalRecords.MultiSelect = false;
            this.dgvMedicalRecords.Name = "dgvMedicalRecords";
            this.dgvMedicalRecords.ReadOnly = true;
            this.dgvMedicalRecords.RowHeadersVisible = false;
            this.dgvMedicalRecords.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMedicalRecords.Size = new System.Drawing.Size(900, 320);
            this.dgvMedicalRecords.TabIndex = 0;
            this.dgvMedicalRecords.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvMedicalRecords_CellDoubleClick);
            // 
            // btnNewRecord
            // 
            this.btnNewRecord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnNewRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnNewRecord.ForeColor = System.Drawing.Color.White;
            this.btnNewRecord.Location = new System.Drawing.Point(20, 360);
            this.btnNewRecord.Name = "btnNewRecord";
            this.btnNewRecord.Size = new System.Drawing.Size(120, 30);
            this.btnNewRecord.TabIndex = 1;
            this.btnNewRecord.Text = "Yeni Kayýt";
            this.btnNewRecord.UseVisualStyleBackColor = false;
            this.btnNewRecord.Click += new System.EventHandler(this.BtnNewRecord_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(160, 360);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(100, 30);
            this.btnPrint.TabIndex = 2;
            this.btnPrint.Text = "Yazdýr";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnExport.ForeColor = System.Drawing.Color.Black;
            this.btnExport.Location = new System.Drawing.Point(280, 360);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(120, 30);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "Dýþa Aktar";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // DoctorMedicalRecordsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.groupBoxRecords);
            this.Controls.Add(this.groupBoxPatient);
            this.Controls.Add(this.lblTitle);
            this.Name = "DoctorMedicalRecordsForm";
            this.Text = "Týbbi Kayýtlar";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedicalRecords)).EndInit();
            this.groupBoxPatient.ResumeLayout(false);
            this.groupBoxPatient.PerformLayout();
            this.groupBoxRecords.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblPatient;
        private System.Windows.Forms.ComboBox cmbPatients;
        private System.Windows.Forms.Label lblPatientInfo;
        private System.Windows.Forms.DataGridView dgvMedicalRecords;
        private System.Windows.Forms.Button btnNewRecord;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.GroupBox groupBoxPatient;
        private System.Windows.Forms.GroupBox groupBoxRecords;
    }
}