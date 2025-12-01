namespace HospitalAutomation.Views.NurseManagement
{
    partial class VitalSignsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBoxPatient = new System.Windows.Forms.GroupBox();
            this.cmbPatient = new System.Windows.Forms.ComboBox();
            this.lblPatient = new System.Windows.Forms.Label();
            this.groupBoxVitals = new System.Windows.Forms.GroupBox();
            this.txtSystolic = new System.Windows.Forms.TextBox();
            this.lblSystolic = new System.Windows.Forms.Label();
            this.txtDiastolic = new System.Windows.Forms.TextBox();
            this.lblDiastolic = new System.Windows.Forms.Label();
            this.txtHeartRate = new System.Windows.Forms.TextBox();
            this.lblHeartRate = new System.Windows.Forms.Label();
            this.txtTemperature = new System.Windows.Forms.TextBox();
            this.lblTemperature = new System.Windows.Forms.Label();
            this.txtRespiratoryRate = new System.Windows.Forms.TextBox();
            this.lblRespiratoryRate = new System.Windows.Forms.Label();
            this.txtOxygenSaturation = new System.Windows.Forms.TextBox();
            this.lblOxygenSaturation = new System.Windows.Forms.Label();
            this.txtWeight = new System.Windows.Forms.TextBox();
            this.lblWeight = new System.Windows.Forms.Label();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.lblHeight = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.lblNotes = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.dgvVitalSigns = new System.Windows.Forms.DataGridView();
            this.groupBoxHistory = new System.Windows.Forms.GroupBox();
            this.groupBoxPatient.SuspendLayout();
            this.groupBoxVitals.SuspendLayout();
            this.groupBoxHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVitalSigns)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxPatient
            // 
            this.groupBoxPatient.Controls.Add(this.cmbPatient);
            this.groupBoxPatient.Controls.Add(this.lblPatient);
            this.groupBoxPatient.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBoxPatient.Location = new System.Drawing.Point(12, 12);
            this.groupBoxPatient.Name = "groupBoxPatient";
            this.groupBoxPatient.Size = new System.Drawing.Size(400, 80);
            this.groupBoxPatient.TabIndex = 0;
            this.groupBoxPatient.TabStop = false;
            this.groupBoxPatient.Text = "Hasta Seçimi";
            // 
            // cmbPatient
            // 
            this.cmbPatient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPatient.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cmbPatient.Location = new System.Drawing.Point(80, 27);
            this.cmbPatient.Name = "cmbPatient";
            this.cmbPatient.Size = new System.Drawing.Size(300, 23);
            this.cmbPatient.TabIndex = 1;
            this.cmbPatient.SelectedIndexChanged += new System.EventHandler(this.CmbPatient_SelectedIndexChanged);
            // 
            // lblPatient
            // 
            this.lblPatient.AutoSize = true;
            this.lblPatient.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblPatient.Location = new System.Drawing.Point(20, 30);
            this.lblPatient.Name = "lblPatient";
            this.lblPatient.Size = new System.Drawing.Size(42, 15);
            this.lblPatient.TabIndex = 0;
            this.lblPatient.Text = "Hasta:";
            // 
            // groupBoxVitals
            // 
            this.groupBoxVitals.Controls.Add(this.txtNotes);
            this.groupBoxVitals.Controls.Add(this.lblNotes);
            this.groupBoxVitals.Controls.Add(this.txtHeight);
            this.groupBoxVitals.Controls.Add(this.lblHeight);
            this.groupBoxVitals.Controls.Add(this.txtWeight);
            this.groupBoxVitals.Controls.Add(this.lblWeight);
            this.groupBoxVitals.Controls.Add(this.txtOxygenSaturation);
            this.groupBoxVitals.Controls.Add(this.lblOxygenSaturation);
            this.groupBoxVitals.Controls.Add(this.txtRespiratoryRate);
            this.groupBoxVitals.Controls.Add(this.lblRespiratoryRate);
            this.groupBoxVitals.Controls.Add(this.txtTemperature);
            this.groupBoxVitals.Controls.Add(this.lblTemperature);
            this.groupBoxVitals.Controls.Add(this.txtHeartRate);
            this.groupBoxVitals.Controls.Add(this.lblHeartRate);
            this.groupBoxVitals.Controls.Add(this.txtDiastolic);
            this.groupBoxVitals.Controls.Add(this.lblDiastolic);
            this.groupBoxVitals.Controls.Add(this.txtSystolic);
            this.groupBoxVitals.Controls.Add(this.lblSystolic);
            this.groupBoxVitals.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBoxVitals.Location = new System.Drawing.Point(12, 100);
            this.groupBoxVitals.Name = "groupBoxVitals";
            this.groupBoxVitals.Size = new System.Drawing.Size(580, 400);
            this.groupBoxVitals.TabIndex = 1;
            this.groupBoxVitals.TabStop = false;
            this.groupBoxVitals.Text = "Vital Bulgular";
            // 
            // txtSystolic
            // 
            this.txtSystolic.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtSystolic.Location = new System.Drawing.Point(200, 27);
            this.txtSystolic.Name = "txtSystolic";
            this.txtSystolic.Size = new System.Drawing.Size(100, 21);
            this.txtSystolic.TabIndex = 1;
            // 
            // lblSystolic
            // 
            this.lblSystolic.AutoSize = true;
            this.lblSystolic.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblSystolic.Location = new System.Drawing.Point(20, 30);
            this.lblSystolic.Name = "lblSystolic";
            this.lblSystolic.Size = new System.Drawing.Size(141, 15);
            this.lblSystolic.TabIndex = 0;
            this.lblSystolic.Text = "Sistolik Basýnç (mmHg):";
            // 
            // txtDiastolic
            // 
            this.txtDiastolic.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtDiastolic.Location = new System.Drawing.Point(200, 67);
            this.txtDiastolic.Name = "txtDiastolic";
            this.txtDiastolic.Size = new System.Drawing.Size(100, 21);
            this.txtDiastolic.TabIndex = 3;
            // 
            // lblDiastolic
            // 
            this.lblDiastolic.AutoSize = true;
            this.lblDiastolic.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblDiastolic.Location = new System.Drawing.Point(20, 70);
            this.lblDiastolic.Name = "lblDiastolic";
            this.lblDiastolic.Size = new System.Drawing.Size(150, 15);
            this.lblDiastolic.TabIndex = 2;
            this.lblDiastolic.Text = "Diastolik Basýnç (mmHg):";
            // 
            // txtHeartRate
            // 
            this.txtHeartRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtHeartRate.Location = new System.Drawing.Point(200, 107);
            this.txtHeartRate.Name = "txtHeartRate";
            this.txtHeartRate.Size = new System.Drawing.Size(100, 21);
            this.txtHeartRate.TabIndex = 5;
            // 
            // lblHeartRate
            // 
            this.lblHeartRate.AutoSize = true;
            this.lblHeartRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblHeartRate.Location = new System.Drawing.Point(20, 110);
            this.lblHeartRate.Name = "lblHeartRate";
            this.lblHeartRate.Size = new System.Drawing.Size(90, 15);
            this.lblHeartRate.TabIndex = 4;
            this.lblHeartRate.Text = "Nabýz (atým/dk):";
            // 
            // txtTemperature
            // 
            this.txtTemperature.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtTemperature.Location = new System.Drawing.Point(200, 147);
            this.txtTemperature.Name = "txtTemperature";
            this.txtTemperature.Size = new System.Drawing.Size(100, 21);
            this.txtTemperature.TabIndex = 7;
            // 
            // lblTemperature
            // 
            this.lblTemperature.AutoSize = true;
            this.lblTemperature.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblTemperature.Location = new System.Drawing.Point(20, 150);
            this.lblTemperature.Name = "lblTemperature";
            this.lblTemperature.Size = new System.Drawing.Size(57, 15);
            this.lblTemperature.TabIndex = 6;
            this.lblTemperature.Text = "Ateþ (°C):";
            // 
            // txtRespiratoryRate
            // 
            this.txtRespiratoryRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtRespiratoryRate.Location = new System.Drawing.Point(200, 187);
            this.txtRespiratoryRate.Name = "txtRespiratoryRate";
            this.txtRespiratoryRate.Size = new System.Drawing.Size(100, 21);
            this.txtRespiratoryRate.TabIndex = 9;
            // 
            // lblRespiratoryRate
            // 
            this.lblRespiratoryRate.AutoSize = true;
            this.lblRespiratoryRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblRespiratoryRate.Location = new System.Drawing.Point(20, 190);
            this.lblRespiratoryRate.Name = "lblRespiratoryRate";
            this.lblRespiratoryRate.Size = new System.Drawing.Size(119, 15);
            this.lblRespiratoryRate.TabIndex = 8;
            this.lblRespiratoryRate.Text = "Solunum Sayýsý (/dk):";
            // 
            // txtOxygenSaturation
            // 
            this.txtOxygenSaturation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtOxygenSaturation.Location = new System.Drawing.Point(200, 227);
            this.txtOxygenSaturation.Name = "txtOxygenSaturation";
            this.txtOxygenSaturation.Size = new System.Drawing.Size(100, 21);
            this.txtOxygenSaturation.TabIndex = 11;
            // 
            // lblOxygenSaturation
            // 
            this.lblOxygenSaturation.AutoSize = true;
            this.lblOxygenSaturation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblOxygenSaturation.Location = new System.Drawing.Point(20, 230);
            this.lblOxygenSaturation.Name = "lblOxygenSaturation";
            this.lblOxygenSaturation.Size = new System.Drawing.Size(141, 15);
            this.lblOxygenSaturation.TabIndex = 10;
            this.lblOxygenSaturation.Text = "Oksijen Saturasyonu (%):";
            // 
            // txtWeight
            // 
            this.txtWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtWeight.Location = new System.Drawing.Point(200, 267);
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.Size = new System.Drawing.Size(100, 21);
            this.txtWeight.TabIndex = 13;
            // 
            // lblWeight
            // 
            this.lblWeight.AutoSize = true;
            this.lblWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblWeight.Location = new System.Drawing.Point(20, 270);
            this.lblWeight.Name = "lblWeight";
            this.lblWeight.Size = new System.Drawing.Size(58, 15);
            this.lblWeight.TabIndex = 12;
            this.lblWeight.Text = "Kilo (kg):";
            // 
            // txtHeight
            // 
            this.txtHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtHeight.Location = new System.Drawing.Point(200, 307);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(100, 21);
            this.txtHeight.TabIndex = 15;
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblHeight.Location = new System.Drawing.Point(20, 310);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(59, 15);
            this.lblHeight.TabIndex = 14;
            this.lblHeight.Text = "Boy (cm):";
            // 
            // txtNotes
            // 
            this.txtNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtNotes.Location = new System.Drawing.Point(200, 347);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(350, 40);
            this.txtNotes.TabIndex = 17;
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblNotes.Location = new System.Drawing.Point(20, 350);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(43, 15);
            this.lblNotes.TabIndex = 16;
            this.lblNotes.Text = "Notlar:";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(200, 520);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 40);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Kaydet";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(320, 520);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 40);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "Temizle";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // dgvVitalSigns
            // 
            this.dgvVitalSigns.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVitalSigns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVitalSigns.Location = new System.Drawing.Point(10, 30);
            this.dgvVitalSigns.Name = "dgvVitalSigns";
            this.dgvVitalSigns.ReadOnly = true;
            this.dgvVitalSigns.RowHeadersVisible = false;
            this.dgvVitalSigns.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVitalSigns.Size = new System.Drawing.Size(560, 510);
            this.dgvVitalSigns.TabIndex = 0;
            // 
            // groupBoxHistory
            // 
            this.groupBoxHistory.Controls.Add(this.dgvVitalSigns);
            this.groupBoxHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBoxHistory.Location = new System.Drawing.Point(600, 12);
            this.groupBoxHistory.Name = "groupBoxHistory";
            this.groupBoxHistory.Size = new System.Drawing.Size(580, 550);
            this.groupBoxHistory.TabIndex = 4;
            this.groupBoxHistory.TabStop = false;
            this.groupBoxHistory.Text = "Vital Bulgular Geçmiþi";
            // 
            // VitalSignsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.groupBoxHistory);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBoxVitals);
            this.Controls.Add(this.groupBoxPatient);
            this.Name = "VitalSignsForm";
            this.Text = "Vital Bulgular Takibi";
            this.Load += new System.EventHandler(this.VitalSignsForm_Load);
            this.groupBoxPatient.ResumeLayout(false);
            this.groupBoxPatient.PerformLayout();
            this.groupBoxVitals.ResumeLayout(false);
            this.groupBoxVitals.PerformLayout();
            this.groupBoxHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVitalSigns)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxPatient;
        private System.Windows.Forms.ComboBox cmbPatient;
        private System.Windows.Forms.Label lblPatient;
        private System.Windows.Forms.GroupBox groupBoxVitals;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.TextBox txtWeight;
        private System.Windows.Forms.Label lblWeight;
        private System.Windows.Forms.TextBox txtOxygenSaturation;
        private System.Windows.Forms.Label lblOxygenSaturation;
        private System.Windows.Forms.TextBox txtRespiratoryRate;
        private System.Windows.Forms.Label lblRespiratoryRate;
        private System.Windows.Forms.TextBox txtTemperature;
        private System.Windows.Forms.Label lblTemperature;
        private System.Windows.Forms.TextBox txtHeartRate;
        private System.Windows.Forms.Label lblHeartRate;
        private System.Windows.Forms.TextBox txtDiastolic;
        private System.Windows.Forms.Label lblDiastolic;
        private System.Windows.Forms.TextBox txtSystolic;
        private System.Windows.Forms.Label lblSystolic;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.DataGridView dgvVitalSigns;
        private System.Windows.Forms.GroupBox groupBoxHistory;
    }
}