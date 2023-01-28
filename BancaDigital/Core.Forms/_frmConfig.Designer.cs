namespace Core.Forms
{
    partial class _frmConfig
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
            this._txtTimeOut = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this._btnGuardar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._txtTimeOut)).BeginInit();
            this.SuspendLayout();
            // 
            // _txtTimeOut
            // 
            this._txtTimeOut.Location = new System.Drawing.Point(79, 21);
            this._txtTimeOut.Name = "_txtTimeOut";
            this._txtTimeOut.Size = new System.Drawing.Size(71, 23);
            this._txtTimeOut.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "TimeOut";
            // 
            // _btnGuardar
            // 
            this._btnGuardar.Location = new System.Drawing.Point(111, 136);
            this._btnGuardar.Name = "_btnGuardar";
            this._btnGuardar.Size = new System.Drawing.Size(75, 23);
            this._btnGuardar.TabIndex = 2;
            this._btnGuardar.Text = "Guardar";
            this._btnGuardar.UseVisualStyleBackColor = true;
            this._btnGuardar.Click += new System.EventHandler(this._btnGuardar_Click);
            // 
            // _frmConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 171);
            this.Controls.Add(this._btnGuardar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._txtTimeOut);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "_frmConfig";
            this.Text = "_frmConfig";
            ((System.ComponentModel.ISupportInitialize)(this._txtTimeOut)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NumericUpDown _txtTimeOut;
        private Label label1;
        private Button _btnGuardar;
    }
}