namespace Core.Selenium.UI
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
            this._tab = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this._grdScript = new System.Windows.Forms.DataGridView();
            this.clmOrden = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmCommand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmTarget = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmValor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clmEjecutarFinal = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clmComentario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this._grdSuites = new System.Windows.Forms.DataGridView();
            this.clmKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this._grdDatosExternos = new System.Windows.Forms.DataGridView();
            this.clmIterar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clmEjecutarFin = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clmInicio = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clmScriptValor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmScriptTarget = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmScriptCommand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmScriptId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this._txtNombrePrueba = new System.Windows.Forms.TextBox();
            this.btnEmpezar = new System.Windows.Forms.Button();
            this._tab.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._grdScript)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._grdSuites)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._grdDatosExternos)).BeginInit();
            this.SuspendLayout();
            // 
            // _tab
            // 
            this._tab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._tab.Controls.Add(this.tabPage1);
            this._tab.Controls.Add(this.tabPage2);
            this._tab.Location = new System.Drawing.Point(12, 44);
            this._tab.Name = "_tab";
            this._tab.SelectedIndex = 0;
            this._tab.Size = new System.Drawing.Size(1043, 546);
            this._tab.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this._grdScript);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1035, 518);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Script";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(944, 480);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Cargar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // _grdScript
            // 
            this._grdScript.AllowUserToAddRows = false;
            this._grdScript.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._grdScript.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmOrden,
            this.clmCommand,
            this.clmTarget,
            this.clmValor,
            this.dataGridViewCheckBoxColumn1,
            this.clmEjecutarFinal,
            this.dataGridViewCheckBoxColumn2,
            this.clmComentario,
            this.clmId});
            this._grdScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grdScript.Location = new System.Drawing.Point(3, 3);
            this._grdScript.Name = "_grdScript";
            this._grdScript.RowTemplate.Height = 25;
            this._grdScript.Size = new System.Drawing.Size(1029, 512);
            this._grdScript.TabIndex = 0;
            // 
            // clmOrden
            // 
            this.clmOrden.DataPropertyName = "Orden";
            this.clmOrden.HeaderText = "Orden";
            this.clmOrden.Name = "clmOrden";
            // 
            // clmCommand
            // 
            this.clmCommand.DataPropertyName = "command";
            this.clmCommand.HeaderText = "Comando";
            this.clmCommand.Name = "clmCommand";
            // 
            // clmTarget
            // 
            this.clmTarget.DataPropertyName = "target";
            this.clmTarget.HeaderText = "Target";
            this.clmTarget.Name = "clmTarget";
            // 
            // clmValor
            // 
            this.clmValor.DataPropertyName = "value";
            this.clmValor.HeaderText = "Valor";
            this.clmValor.Name = "clmValor";
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.DataPropertyName = "InicioSesion";
            this.dataGridViewCheckBoxColumn1.HeaderText = "Ejecutar Inicio";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCheckBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // clmEjecutarFinal
            // 
            this.clmEjecutarFinal.DataPropertyName = "FinSesion";
            this.clmEjecutarFinal.HeaderText = "Ejecutar Final";
            this.clmEjecutarFinal.Name = "clmEjecutarFinal";
            // 
            // dataGridViewCheckBoxColumn2
            // 
            this.dataGridViewCheckBoxColumn2.DataPropertyName = "Iterar";
            this.dataGridViewCheckBoxColumn2.HeaderText = "Iterar";
            this.dataGridViewCheckBoxColumn2.Name = "dataGridViewCheckBoxColumn2";
            // 
            // clmComentario
            // 
            this.clmComentario.DataPropertyName = "comment";
            this.clmComentario.HeaderText = "Comentario";
            this.clmComentario.Name = "clmComentario";
            // 
            // clmId
            // 
            this.clmId.DataPropertyName = "id";
            this.clmId.HeaderText = "Id";
            this.clmId.Name = "clmId";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1035, 518);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Datos Externos";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this._grdSuites);
            this.groupBox2.Location = new System.Drawing.Point(6, 270);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1023, 167);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Variables Inicio/fin";
            // 
            // _grdSuites
            // 
            this._grdSuites.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._grdSuites.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmKey,
            this.clmValue});
            this._grdSuites.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grdSuites.Location = new System.Drawing.Point(3, 19);
            this._grdSuites.Name = "_grdSuites";
            this._grdSuites.RowTemplate.Height = 25;
            this._grdSuites.Size = new System.Drawing.Size(1017, 145);
            this._grdSuites.TabIndex = 0;
            // 
            // clmKey
            // 
            this.clmKey.HeaderText = "Nombre";
            this.clmKey.Name = "clmKey";
            this.clmKey.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clmValue
            // 
            this.clmValue.HeaderText = "Valor";
            this.clmValue.Name = "clmValue";
            this.clmValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this._grdDatosExternos);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1023, 261);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos Externos";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(942, 232);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Cargar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // _grdDatosExternos
            // 
            this._grdDatosExternos.AllowUserToAddRows = false;
            this._grdDatosExternos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._grdDatosExternos.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grdDatosExternos.Location = new System.Drawing.Point(3, 19);
            this._grdDatosExternos.Name = "_grdDatosExternos";
            this._grdDatosExternos.RowTemplate.Height = 25;
            this._grdDatosExternos.Size = new System.Drawing.Size(1017, 239);
            this._grdDatosExternos.TabIndex = 0;
            // 
            // clmIterar
            // 
            this.clmIterar.HeaderText = "Iterar";
            this.clmIterar.Name = "clmIterar";
            // 
            // clmEjecutarFin
            // 
            this.clmEjecutarFin.HeaderText = "Ejecutar Fin";
            this.clmEjecutarFin.Name = "clmEjecutarFin";
            // 
            // clmInicio
            // 
            this.clmInicio.HeaderText = "Ejecutar Inicio";
            this.clmInicio.Name = "clmInicio";
            // 
            // clmScriptValor
            // 
            this.clmScriptValor.DataPropertyName = "value";
            this.clmScriptValor.HeaderText = "Valor";
            this.clmScriptValor.Name = "clmScriptValor";
            // 
            // clmScriptTarget
            // 
            this.clmScriptTarget.DataPropertyName = "target";
            this.clmScriptTarget.HeaderText = "Target";
            this.clmScriptTarget.Name = "clmScriptTarget";
            // 
            // clmScriptCommand
            // 
            this.clmScriptCommand.DataPropertyName = "command";
            this.clmScriptCommand.HeaderText = "Comando";
            this.clmScriptCommand.Name = "clmScriptCommand";
            // 
            // clmScriptId
            // 
            this.clmScriptId.DataPropertyName = "id";
            this.clmScriptId.HeaderText = "Id";
            this.clmScriptId.Name = "clmScriptId";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nombre Prueba:";
            // 
            // _txtNombrePrueba
            // 
            this._txtNombrePrueba.Location = new System.Drawing.Point(112, 6);
            this._txtNombrePrueba.Name = "_txtNombrePrueba";
            this._txtNombrePrueba.Size = new System.Drawing.Size(454, 23);
            this._txtNombrePrueba.TabIndex = 2;
            this._txtNombrePrueba.Click += new System.EventHandler(this._txtNombrePrueba_Click);
            // 
            // btnEmpezar
            // 
            this.btnEmpezar.Location = new System.Drawing.Point(973, 26);
            this.btnEmpezar.Name = "btnEmpezar";
            this.btnEmpezar.Size = new System.Drawing.Size(75, 23);
            this.btnEmpezar.TabIndex = 3;
            this.btnEmpezar.Text = "▶️";
            this.btnEmpezar.UseVisualStyleBackColor = true;
            this.btnEmpezar.Click += new System.EventHandler(this.btnEmpezar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 602);
            this.Controls.Add(this.btnEmpezar);
            this.Controls.Add(this._txtNombrePrueba);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._tab);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this._tab.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._grdScript)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._grdSuites)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._grdDatosExternos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TabControl _tab;
        private TabPage tabPage2;
        private DataGridView _grdDatosExternos;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private DataGridView _grdSuites;
        private TabPage tabPage1;
        private Button button1;
        private DataGridView _grdScript;
        private DataGridViewCheckBoxColumn clmIterar;
        private DataGridViewCheckBoxColumn clmEjecutarFin;
        private DataGridViewCheckBoxColumn clmInicio;
        private DataGridViewTextBoxColumn clmScriptValor;
        private DataGridViewTextBoxColumn clmScriptTarget;
        private DataGridViewTextBoxColumn clmScriptCommand;
        private DataGridViewTextBoxColumn clmScriptId;
        private Label label1;
        private TextBox _txtNombrePrueba;
        private Button btnEmpezar;
        private DataGridViewTextBoxColumn clmOrden;
        private DataGridViewTextBoxColumn clmCommand;
        private DataGridViewTextBoxColumn clmTarget;
        private DataGridViewTextBoxColumn clmValor;
        private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private DataGridViewCheckBoxColumn clmEjecutarFinal;
        private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn2;
        private DataGridViewTextBoxColumn clmComentario;
        private DataGridViewTextBoxColumn clmId;
        private Button button2;
        private DataGridViewTextBoxColumn clmKey;
        private DataGridViewTextBoxColumn clmValue;
    }
}