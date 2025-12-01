namespace HospitalAutomation.Views.NurseManagement
{
    partial class MedicationTrackingForm
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
            this.groupBoxMedication = new System.Windows.Forms.GroupBox();
            this.cmbMedication = new System.Windows.Forms.ComboBox();
            this.lblMedication = new System.Windows.Forms.Label();
            this.txtDosage = new System.Windows.Forms.TextBox();
            this.lblDosage = new System.Windows.Forms.Label();
            this.dtpScheduled = new System.Windows.Forms.DateTimePicker();
            this.lblScheduled = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.lblNotes = new System.Windows.Forms.Label();
            this.txtSideEffects = new System.Windows.Forms.TextBox();
            this.lblSideEffects = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnAdminister = new System.Windows.Forms.Button();
            this.dgvMedications = new System.Windows.Forms.DataGridView();
            this.groupBoxHistory = new System.Windows.Forms.GroupBox();
            this.groupBoxPatient.SuspendLayout();
            this.groupBoxMedication.SuspendLayout();
            this.groupBoxHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedications)).BeginInit();
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
            // groupBoxMedication
            // 
            this.groupBoxMedication.Controls.Add(this.txtSideEffects);
            this.groupBoxMedication.Controls.Add(this.lblSideEffects);
            this.groupBoxMedication.Controls.Add(this.txtNotes);
            this.groupBoxMedication.Controls.Add(this.lblNotes);
            this.groupBoxMedication.Controls.Add(this.cmbStatus);
            this.groupBoxMedication.Controls.Add(this.lblStatus);
            this.groupBoxMedication.Controls.Add(this.dtpScheduled);
            this.groupBoxMedication.Controls.Add(this.lblScheduled);
            this.groupBoxMedication.Controls.Add(this.txtDosage);
            this.groupBoxMedication.Controls.Add(this.lblDosage);
            this.groupBoxMedication.Controls.Add(this.cmbMedication);
            this.groupBoxMedication.Controls.Add(this.lblMedication);
            this.groupBoxMedication.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBoxMedication.Location = new System.Drawing.Point(12, 100);
            this.groupBoxMedication.Name = "groupBoxMedication";
            this.groupBoxMedication.Size = new System.Drawing.Size(580, 400);
            this.groupBoxMedication.TabIndex = 1;
            this.groupBoxMedication.TabStop = false;
            this.groupBoxMedication.Text = "Ýlaç Bilgileri";
            // 
            // cmbMedication
            // 
            this.cmbMedication.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMedication.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cmbMedication.Location = new System.Drawing.Point(150, 27);
            this.cmbMedication.Name = "cmbMedication";
            this.cmbMedication.Size = new System.Drawing.Size(300, 23);
            this.cmbMedication.TabIndex = 1;
            // 
            // lblMedication
            // 
            this.lblMedication.AutoSize = true;
            this.lblMedication.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblMedication.Location = new System.Drawing.Point(20, 30);
            this.lblMedication.Name = "lblMedication";
            this.lblMedication.Size = new System.Drawing.Size(32, 15);
            this.lblMedication.TabIndex = 0;
            this.lblMedication.Text = "Ýlaç:";
            // 
            // txtDosage
            // 
            this.txtDosage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtDosage.Location = new System.Drawing.Point(150, 77);
            this.txtDosage.Name = "txtDosage";
            this.txtDosage.Size = new System.Drawing.Size(200, 21);
            this.txtDosage.TabIndex = 3;
            // 
            // lblDosage
            // 
            this.lblDosage.AutoSize = true;
            this.lblDosage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblDosage.Location = new System.Drawing.Point(20, 80);
            this.lblDosage.Name = "lblDosage";
            this.lblDosage.Size = new System.Drawing.Size(30, 15);
            this.lblDosage.TabIndex = 2;
            this.lblDosage.Text = "Doz:";
            // 
            // dtpScheduled
            // 
            this.dtpScheduled.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dtpScheduled.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dtpScheduled.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpScheduled.Location = new System.Drawing.Point(150, 127);
            this.dtpScheduled.Name = "dtpScheduled";
            this.dtpScheduled.ShowUpDown = true;
            this.dtpScheduled.Size = new System.Drawing.Size(200, 21);
            this.dtpScheduled.TabIndex = 5;
            // 
            // lblScheduled
            // 
            this.lblScheduled.AutoSize = true;
            this.lblScheduled.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblScheduled.Location = new System.Drawing.Point(20, 130);
            this.lblScheduled.Name = "lblScheduled";
            this.lblScheduled.Size = new System.Drawing.Size(107, 15);
            this.lblScheduled.TabIndex = 4;
            this.lblScheduled.Text = "Planlanan Zaman:";
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cmbStatus.Location = new System.Drawing.Point(150, 177);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(200, 23);
            this.cmbStatus.TabIndex = 7;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblStatus.Location = new System.Drawing.Point(20, 180);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(46, 15);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "Durum:";
            // 
            // txtNotes
            // 
            this.txtNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtNotes.Location = new System.Drawing.Point(150, 227);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(400, 60);
            this.txtNotes.TabIndex = 9;
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblNotes.Location = new System.Drawing.Point(20, 230);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(43, 15);
            this.lblNotes.TabIndex = 8;
            this.lblNotes.Text = "Notlar:";
            // 
            // txtSideEffects
            // 
            this.txtSideEffects.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtSideEffects.Location = new System.Drawing.Point(150, 327);
            this.txtSideEffects.Multiline = true;
            this.txtSideEffects.Name = "txtSideEffects";
            this.txtSideEffects.Size = new System.Drawing.Size(400, 60);
            this.txtSideEffects.TabIndex = 11;
            // 
            // lblSideEffects
            // 
            this.lblSideEffects.AutoSize = true;
            this.lblSideEffects.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblSideEffects.Location = new System.Drawing.Point(20, 330);
            this.lblSideEffects.Name = "lblSideEffects";
            this.lblSideEffects.Size = new System.Drawing.Size(71, 15);
            this.lblSideEffects.TabIndex = 10;
            this.lblSideEffects.Text = "Yan Etkiler:";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(150, 520);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 40);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Kaydet";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnAdminister
            // 
            this.btnAdminister.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnAdminister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdminister.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnAdminister.ForeColor = System.Drawing.Color.White;
            this.btnAdminister.Location = new System.Drawing.Point(290, 520);
            this.btnAdminister.Name = "btnAdminister";
            this.btnAdminister.Size = new System.Drawing.Size(120, 40);
            this.btnAdminister.TabIndex = 3;
            this.btnAdminister.Text = "Ýlaç Verildi";
            this.btnAdminister.UseVisualStyleBackColor = false;
            this.btnAdminister.Click += new System.EventHandler(this.BtnAdminister_Click);
            // 
            // dgvMedications
            // 
            this.dgvMedications.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMedications.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMedications.Location = new System.Drawing.Point(10, 30);
            this.dgvMedications.Name = "dgvMedications";
            this.dgvMedications.ReadOnly = true;
            this.dgvMedications.RowHeadersVisible = false;
            this.dgvMedications.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMedications.Size = new System.Drawing.Size(560, 510);
            this.dgvMedications.TabIndex = 0;
            // 
            // groupBoxHistory
            // 
            this.groupBoxHistory.Controls.Add(this.dgvMedications);
            this.groupBoxHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBoxHistory.Location = new System.Drawing.Point(600, 12);
            this.groupBoxHistory.Name = "groupBoxHistory";
            this.groupBoxHistory.Size = new System.Drawing.Size(580, 550);
            this.groupBoxHistory.TabIndex = 4;
            this.groupBoxHistory.TabStop = false;
            this.groupBoxHistory.Text = "Ýlaç Geçmiþi";
            // 
            // MedicationTrackingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.groupBoxHistory);
            this.Controls.Add(this.btnAdminister);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBoxMedication);
            this.Controls.Add(this.groupBoxPatient);
            this.Name = "MedicationTrackingForm";
            this.Text = "Ýlaç Takibi";
            this.Load += new System.EventHandler(this.MedicationTrackingForm_Load);
            this.groupBoxPatient.ResumeLayout(false);
            this.groupBoxPatient.PerformLayout();
            this.groupBoxMedication.ResumeLayout(false);
            this.groupBoxMedication.PerformLayout();
            this.groupBoxHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedications)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxPatient;
        private System.Windows.Forms.ComboBox cmbPatient;
        private System.Windows.Forms.Label lblPatient;
        private System.Windows.Forms.GroupBox groupBoxMedication;
        private System.Windows.Forms.ComboBox cmbMedication;
        private System.Windows.Forms.Label lblMedication;
        private System.Windows.Forms.TextBox txtDosage;
        private System.Windows.Forms.Label lblDosage;
        private System.Windows.Forms.DateTimePicker dtpScheduled;
        private System.Windows.Forms.Label lblScheduled;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.TextBox txtSideEffects;
        private System.Windows.Forms.Label lblSideEffects;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnAdminister;
        private System.Windows.Forms.DataGridView dgvMedications;
        private System.Windows.Forms.GroupBox groupBoxHistory;
    }
}