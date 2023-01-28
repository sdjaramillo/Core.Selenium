namespace Core.Forms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._lstScripts = new System.Windows.Forms.ListBox();
            this._txtJson = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnGuardarJson = new System.Windows.Forms.Button();
            this._txtPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._rdEdge = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._rdChrome = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this._rdExcel = new System.Windows.Forms.RadioButton();
            this._rdJson = new System.Windows.Forms.RadioButton();
            this._grdExcel = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._grdExcel)).BeginInit();
            this.SuspendLayout();
            // 
            // _lstScripts
            // 
            this._lstScripts.FormattingEnabled = true;
            this._lstScripts.ItemHeight = 15;
            this._lstScripts.Location = new System.Drawing.Point(12, 54);
            this._lstScripts.Name = "_lstScripts";
            this._lstScripts.Size = new System.Drawing.Size(239, 454);
            this._lstScripts.TabIndex = 0;
            this._lstScripts.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // _txtJson
            // 
            this._txtJson.Location = new System.Drawing.Point(257, 54);
            this._txtJson.Name = "_txtJson";
            this._txtJson.Size = new System.Drawing.Size(737, 454);
            this._txtJson.TabIndex = 1;
            this._txtJson.Text = "";
            this._txtJson.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.Location = new System.Drawing.Point(180, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(55, 37);
            this.button1.TabIndex = 2;
            this.button1.Text = "▶️";
            this.button1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnGuardarJson
            // 
            this.btnGuardarJson.Location = new System.Drawing.Point(907, 478);
            this.btnGuardarJson.Name = "btnGuardarJson";
            this.btnGuardarJson.Size = new System.Drawing.Size(75, 23);
            this.btnGuardarJson.TabIndex = 4;
            this.btnGuardarJson.Text = "Guardar";
            this.btnGuardarJson.UseVisualStyleBackColor = true;
            this.btnGuardarJson.Click += new System.EventHandler(this.btnGuardarJson_Click);
            // 
            // _txtPath
            // 
            this._txtPath.Location = new System.Drawing.Point(628, 25);
            this._txtPath.Name = "_txtPath";
            this._txtPath.Size = new System.Drawing.Size(366, 23);
            this._txtPath.TabIndex = 5;
            this._txtPath.Click += new System.EventHandler(this._txtPath_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(540, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Ruta reporte";
            // 
            // _rdEdge
            // 
            this._rdEdge.AutoSize = true;
            this._rdEdge.Location = new System.Drawing.Point(6, 20);
            this._rdEdge.Name = "_rdEdge";
            this._rdEdge.Size = new System.Drawing.Size(51, 19);
            this._rdEdge.TabIndex = 9;
            this._rdEdge.TabStop = true;
            this._rdEdge.Text = "Edge";
            this._rdEdge.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._rdChrome);
            this.groupBox1.Controls.Add(this._rdEdge);
            this.groupBox1.Location = new System.Drawing.Point(13, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(161, 45);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Driver";
            // 
            // _rdChrome
            // 
            this._rdChrome.AutoSize = true;
            this._rdChrome.Location = new System.Drawing.Point(63, 20);
            this._rdChrome.Name = "_rdChrome";
            this._rdChrome.Size = new System.Drawing.Size(68, 19);
            this._rdChrome.TabIndex = 10;
            this._rdChrome.TabStop = true;
            this._rdChrome.Text = "Chrome";
            this._rdChrome.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this._rdExcel);
            this.groupBox2.Controls.Add(this._rdJson);
            this.groupBox2.Location = new System.Drawing.Point(257, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(239, 45);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Fuente Datos";
            // 
            // _rdExcel
            // 
            this._rdExcel.AutoSize = true;
            this._rdExcel.Location = new System.Drawing.Point(108, 20);
            this._rdExcel.Name = "_rdExcel";
            this._rdExcel.Size = new System.Drawing.Size(52, 19);
            this._rdExcel.TabIndex = 10;
            this._rdExcel.Text = "Excel";
            this._rdExcel.UseVisualStyleBackColor = true;
            this._rdExcel.CheckedChanged += new System.EventHandler(this._rdExcel_CheckedChanged);
            // 
            // _rdJson
            // 
            this._rdJson.AutoSize = true;
            this._rdJson.Checked = true;
            this._rdJson.Location = new System.Drawing.Point(6, 20);
            this._rdJson.Name = "_rdJson";
            this._rdJson.Size = new System.Drawing.Size(48, 19);
            this._rdJson.TabIndex = 9;
            this._rdJson.TabStop = true;
            this._rdJson.Text = "Json";
            this._rdJson.UseVisualStyleBackColor = true;
            this._rdJson.CheckedChanged += new System.EventHandler(this._rdJson_CheckedChanged);
            // 
            // _grdExcel
            // 
            this._grdExcel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._grdExcel.Location = new System.Drawing.Point(257, 54);
            this._grdExcel.Name = "_grdExcel";
            this._grdExcel.RowTemplate.Height = 25;
            this._grdExcel.Size = new System.Drawing.Size(737, 454);
            this._grdExcel.TabIndex = 12;
            this._grdExcel.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 518);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._txtPath);
            this.Controls.Add(this.btnGuardarJson);
            this.Controls.Add(this.button1);
            this.Controls.Add(this._lstScripts);
            this.Controls.Add(this._grdExcel);
            this.Controls.Add(this._txtJson);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._grdExcel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListBox _lstScripts;
        private RichTextBox _txtJson;
        private Button button1;
        private Button btnGuardarJson;
        private TextBox _txtPath;
        private Label label1;
        private RadioButton _rdEdge;
        private GroupBox groupBox1;
        private RadioButton _rdChrome;
        private GroupBox groupBox2;
        private RadioButton _rdExcel;
        private RadioButton _rdJson;
        private DataGridView _grdExcel;
    }
}