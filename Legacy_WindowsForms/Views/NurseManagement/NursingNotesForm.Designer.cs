namespace HospitalAutomation.Views.NurseManagement
{
    partial class NursingNotesForm
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
            this.groupBoxNote = new System.Windows.Forms.GroupBox();
            this.cmbNoteType = new System.Windows.Forms.ComboBox();
            this.lblNoteType = new System.Windows.Forms.Label();
            this.txtContent = new System.Windows.Forms.TextBox();
            this.lblContent = new System.Windows.Forms.Label();
            this.txtAssessment = new System.Windows.Forms.TextBox();
            this.lblAssessment = new System.Windows.Forms.Label();
            this.txtIntervention = new System.Windows.Forms.TextBox();
            this.lblIntervention = new System.Windows.Forms.Label();
            this.txtPatientResponse = new System.Windows.Forms.TextBox();
            this.lblPatientResponse = new System.Windows.Forms.Label();
            this.chkUrgent = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.dgvNotes = new System.Windows.Forms.DataGridView();
            this.groupBoxHistory = new System.Windows.Forms.GroupBox();
            this.groupBoxPatient.SuspendLayout();
            this.groupBoxNote.SuspendLayout();
            this.groupBoxHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNotes)).BeginInit();
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
            // groupBoxNote
            // 
            this.groupBoxNote.Controls.Add(this.chkUrgent);
            this.groupBoxNote.Controls.Add(this.txtPatientResponse);
            this.groupBoxNote.Controls.Add(this.lblPatientResponse);
            this.groupBoxNote.Controls.Add(this.txtIntervention);
            this.groupBoxNote.Controls.Add(this.lblIntervention);
            this.groupBoxNote.Controls.Add(this.txtAssessment);
            this.groupBoxNote.Controls.Add(this.lblAssessment);
            this.groupBoxNote.Controls.Add(this.txtContent);
            this.groupBoxNote.Controls.Add(this.lblContent);
            this.groupBoxNote.Controls.Add(this.cmbNoteType);
            this.groupBoxNote.Controls.Add(this.lblNoteType);
            this.groupBoxNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBoxNote.Location = new System.Drawing.Point(12, 100);
            this.groupBoxNote.Name = "groupBoxNote";
            this.groupBoxNote.Size = new System.Drawing.Size(580, 500);
            this.groupBoxNote.TabIndex = 1;
            this.groupBoxNote.TabStop = false;
            this.groupBoxNote.Text = "Not Bilgileri";
            // 
            // cmbNoteType
            // 
            this.cmbNoteType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNoteType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cmbNoteType.Location = new System.Drawing.Point(150, 27);
            this.cmbNoteType.Name = "cmbNoteType";
            this.cmbNoteType.Size = new System.Drawing.Size(200, 23);
            this.cmbNoteType.TabIndex = 1;
            // 
            // lblNoteType
            // 
            this.lblNoteType.AutoSize = true;
            this.lblNoteType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblNoteType.Location = new System.Drawing.Point(20, 30);
            this.lblNoteType.Name = "lblNoteType";
            this.lblNoteType.Size = new System.Drawing.Size(60, 15);
            this.lblNoteType.TabIndex = 0;
            this.lblNoteType.Text = "Not Türü:";
            // 
            // txtContent
            // 
            this.txtContent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtContent.Location = new System.Drawing.Point(150, 67);
            this.txtContent.Multiline = true;
            this.txtContent.Name = "txtContent";
            this.txtContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtContent.Size = new System.Drawing.Size(400, 80);
            this.txtContent.TabIndex = 3;
            // 
            // lblContent
            // 
            this.lblContent.AutoSize = true;
            this.lblContent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblContent.Location = new System.Drawing.Point(20, 70);
            this.lblContent.Name = "lblContent";
            this.lblContent.Size = new System.Drawing.Size(73, 15);
            this.lblContent.TabIndex = 2;
            this.lblContent.Text = "Not Ýçeriði:";
            // 
            // txtAssessment
            // 
            this.txtAssessment.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtAssessment.Location = new System.Drawing.Point(150, 167);
            this.txtAssessment.Multiline = true;
            this.txtAssessment.Name = "txtAssessment";
            this.txtAssessment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAssessment.Size = new System.Drawing.Size(400, 60);
            this.txtAssessment.TabIndex = 5;
            // 
            // lblAssessment
            // 
            this.lblAssessment.AutoSize = true;
            this.lblAssessment.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblAssessment.Location = new System.Drawing.Point(20, 170);
            this.lblAssessment.Name = "lblAssessment";
            this.lblAssessment.Size = new System.Drawing.Size(87, 15);
            this.lblAssessment.TabIndex = 4;
            this.lblAssessment.Text = "Deðerlendirme:";
            // 
            // txtIntervention
            // 
            this.txtIntervention.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtIntervention.Location = new System.Drawing.Point(150, 247);
            this.txtIntervention.Multiline = true;
            this.txtIntervention.Name = "txtIntervention";
            this.txtIntervention.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtIntervention.Size = new System.Drawing.Size(400, 60);
            this.txtIntervention.TabIndex = 7;
            // 
            // lblIntervention
            // 
            this.lblIntervention.AutoSize = true;
            this.lblIntervention.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblIntervention.Location = new System.Drawing.Point(20, 250);
            this.lblIntervention.Name = "lblIntervention";
            this.lblIntervention.Size = new System.Drawing.Size(123, 15);
            this.lblIntervention.TabIndex = 6;
            this.lblIntervention.Text = "Planlanan Müdahale:";
            // 
            // txtPatientResponse
            // 
            this.txtPatientResponse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtPatientResponse.Location = new System.Drawing.Point(150, 327);
            this.txtPatientResponse.Multiline = true;
            this.txtPatientResponse.Name = "txtPatientResponse";
            this.txtPatientResponse.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPatientResponse.Size = new System.Drawing.Size(400, 60);
            this.txtPatientResponse.TabIndex = 9;
            // 
            // lblPatientResponse
            // 
            this.lblPatientResponse.AutoSize = true;
            this.lblPatientResponse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblPatientResponse.Location = new System.Drawing.Point(20, 330);
            this.lblPatientResponse.Name = "lblPatientResponse";
            this.lblPatientResponse.Size = new System.Drawing.Size(81, 15);
            this.lblPatientResponse.TabIndex = 8;
            this.lblPatientResponse.Text = "Hasta Yanýtý:";
            // 
            // chkUrgent
            // 
            this.chkUrgent.AutoSize = true;
            this.chkUrgent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.chkUrgent.ForeColor = System.Drawing.Color.Red;
            this.chkUrgent.Location = new System.Drawing.Point(150, 407);
            this.chkUrgent.Name = "chkUrgent";
            this.chkUrgent.Size = new System.Drawing.Size(50, 19);
            this.chkUrgent.TabIndex = 10;
            this.chkUrgent.Text = "Acil";
            this.chkUrgent.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(200, 620);
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
            this.btnClear.Location = new System.Drawing.Point(320, 620);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 40);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "Temizle";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // dgvNotes
            // 
            this.dgvNotes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvNotes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNotes.Location = new System.Drawing.Point(10, 30);
            this.dgvNotes.Name = "dgvNotes";
            this.dgvNotes.ReadOnly = true;
            this.dgvNotes.RowHeadersVisible = false;
            this.dgvNotes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNotes.Size = new System.Drawing.Size(560, 610);
            this.dgvNotes.TabIndex = 0;
            // 
            // groupBoxHistory
            // 
            this.groupBoxHistory.Controls.Add(this.dgvNotes);
            this.groupBoxHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBoxHistory.Location = new System.Drawing.Point(600, 12);
            this.groupBoxHistory.Name = "groupBoxHistory";
            this.groupBoxHistory.Size = new System.Drawing.Size(580, 650);
            this.groupBoxHistory.TabIndex = 4;
            this.groupBoxHistory.TabStop = false;
            this.groupBoxHistory.Text = "Hemþire Notlarý Geçmiþi";
            // 
            // NursingNotesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.groupBoxHistory);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBoxNote);
            this.Controls.Add(this.groupBoxPatient);
            this.Name = "NursingNotesForm";
            this.Text = "Hemþire Notlarý";
            this.Load += new System.EventHandler(this.NursingNotesForm_Load);
            this.groupBoxPatient.ResumeLayout(false);
            this.groupBoxPatient.PerformLayout();
            this.groupBoxNote.ResumeLayout(false);
            this.groupBoxNote.PerformLayout();
            this.groupBoxHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNotes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxPatient;
        private System.Windows.Forms.ComboBox cmbPatient;
        private System.Windows.Forms.Label lblPatient;
        private System.Windows.Forms.GroupBox groupBoxNote;
        private System.Windows.Forms.ComboBox cmbNoteType;
        private System.Windows.Forms.Label lblNoteType;
        private System.Windows.Forms.TextBox txtContent;
        private System.Windows.Forms.Label lblContent;
        private System.Windows.Forms.TextBox txtAssessment;
        private System.Windows.Forms.Label lblAssessment;
        private System.Windows.Forms.TextBox txtIntervention;
        private System.Windows.Forms.Label lblIntervention;
        private System.Windows.Forms.TextBox txtPatientResponse;
        private System.Windows.Forms.Label lblPatientResponse;
        private System.Windows.Forms.CheckBox chkUrgent;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.DataGridView dgvNotes;
        private System.Windows.Forms.GroupBox groupBoxHistory;
    }
}