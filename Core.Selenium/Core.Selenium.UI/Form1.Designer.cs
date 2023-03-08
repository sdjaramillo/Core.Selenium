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
            components = new System.ComponentModel.Container();
            _tab = new TabControl();
            tabPage1 = new TabPage();
            _grdScript = new DataGridView();
            clmOrden = new DataGridViewTextBoxColumn();
            clmCommand = new DataGridViewTextBoxColumn();
            clmTarget = new DataGridViewTextBoxColumn();
            clmValor = new DataGridViewTextBoxColumn();
            dataGridViewCheckBoxColumn1 = new DataGridViewCheckBoxColumn();
            clmEjecutarFinal = new DataGridViewCheckBoxColumn();
            dataGridViewCheckBoxColumn2 = new DataGridViewCheckBoxColumn();
            clmComentario = new DataGridViewTextBoxColumn();
            clmId = new DataGridViewTextBoxColumn();
            _menuGridScript = new ContextMenuStrip(components);
            eliminarFilaToolStripMenuItem = new ToolStripMenuItem();
            agregarFilaToolStripMenuItem = new ToolStripMenuItem();
            tabPage2 = new TabPage();
            groupBox2 = new GroupBox();
            _grdSuites = new DataGridView();
            clmKey = new DataGridViewTextBoxColumn();
            clmValue = new DataGridViewTextBoxColumn();
            groupBox1 = new GroupBox();
            button2 = new Button();
            _grdDatosExternos = new DataGridView();
            tabPage3 = new TabPage();
            _txtJson = new RichTextBox();
            clmIterar = new DataGridViewCheckBoxColumn();
            clmEjecutarFin = new DataGridViewCheckBoxColumn();
            clmInicio = new DataGridViewCheckBoxColumn();
            clmScriptValor = new DataGridViewTextBoxColumn();
            clmScriptTarget = new DataGridViewTextBoxColumn();
            clmScriptCommand = new DataGridViewTextBoxColumn();
            clmScriptId = new DataGridViewTextBoxColumn();
            treeView1 = new TreeView();
            _mnuTree = new ContextMenuStrip(components);
            actualizarToolStripMenuItem = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            toolStripDropDownButton2 = new ToolStripDropDownButton();
            generarScriptToolStripMenuItem = new ToolStripMenuItem();
            guardarToolStripMenuItem = new ToolStripMenuItem();
            toolStripDropDownButton1 = new ToolStripDropDownButton();
            chromeToolStripMenuItem = new ToolStripMenuItem();
            edgeToolStripMenuItem = new ToolStripMenuItem();
            _txtUrl = new TextBox();
            label1 = new Label();
            label2 = new Label();
            _txtNombreTest = new TextBox();
            _tab.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_grdScript).BeginInit();
            _menuGridScript.SuspendLayout();
            tabPage2.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_grdSuites).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_grdDatosExternos).BeginInit();
            tabPage3.SuspendLayout();
            _mnuTree.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // _tab
            // 
            _tab.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _tab.Controls.Add(tabPage1);
            _tab.Controls.Add(tabPage2);
            _tab.Controls.Add(tabPage3);
            _tab.Location = new Point(280, 58);
            _tab.Name = "_tab";
            _tab.SelectedIndex = 0;
            _tab.Size = new Size(860, 652);
            _tab.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(_grdScript);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(852, 624);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Script";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // _grdScript
            // 
            _grdScript.AllowUserToAddRows = false;
            _grdScript.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _grdScript.Columns.AddRange(new DataGridViewColumn[] { clmOrden, clmCommand, clmTarget, clmValor, dataGridViewCheckBoxColumn1, clmEjecutarFinal, dataGridViewCheckBoxColumn2, clmComentario, clmId });
            _grdScript.ContextMenuStrip = _menuGridScript;
            _grdScript.Dock = DockStyle.Fill;
            _grdScript.Location = new Point(3, 3);
            _grdScript.Name = "_grdScript";
            _grdScript.RowTemplate.Height = 25;
            _grdScript.Size = new Size(846, 618);
            _grdScript.TabIndex = 0;
            // 
            // clmOrden
            // 
            clmOrden.DataPropertyName = "Orden";
            clmOrden.HeaderText = "Orden";
            clmOrden.Name = "clmOrden";
            // 
            // clmCommand
            // 
            clmCommand.DataPropertyName = "command";
            clmCommand.HeaderText = "Comando";
            clmCommand.Name = "clmCommand";
            // 
            // clmTarget
            // 
            clmTarget.DataPropertyName = "target";
            clmTarget.HeaderText = "Target";
            clmTarget.Name = "clmTarget";
            // 
            // clmValor
            // 
            clmValor.DataPropertyName = "value";
            clmValor.HeaderText = "Valor";
            clmValor.Name = "clmValor";
            // 
            // dataGridViewCheckBoxColumn1
            // 
            dataGridViewCheckBoxColumn1.DataPropertyName = "InicioSesion";
            dataGridViewCheckBoxColumn1.HeaderText = "Ejecutar Inicio";
            dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            dataGridViewCheckBoxColumn1.Resizable = DataGridViewTriState.True;
            dataGridViewCheckBoxColumn1.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // clmEjecutarFinal
            // 
            clmEjecutarFinal.DataPropertyName = "FinSesion";
            clmEjecutarFinal.HeaderText = "Ejecutar Final";
            clmEjecutarFinal.Name = "clmEjecutarFinal";
            // 
            // dataGridViewCheckBoxColumn2
            // 
            dataGridViewCheckBoxColumn2.DataPropertyName = "Iterar";
            dataGridViewCheckBoxColumn2.HeaderText = "Iterar";
            dataGridViewCheckBoxColumn2.Name = "dataGridViewCheckBoxColumn2";
            // 
            // clmComentario
            // 
            clmComentario.DataPropertyName = "comment";
            clmComentario.HeaderText = "Comentario";
            clmComentario.Name = "clmComentario";
            // 
            // clmId
            // 
            clmId.DataPropertyName = "id";
            clmId.HeaderText = "Id";
            clmId.Name = "clmId";
            // 
            // _menuGridScript
            // 
            _menuGridScript.Items.AddRange(new ToolStripItem[] { eliminarFilaToolStripMenuItem, agregarFilaToolStripMenuItem });
            _menuGridScript.Name = "_menuGridScript";
            _menuGridScript.Size = new Size(139, 48);
            // 
            // eliminarFilaToolStripMenuItem
            // 
            eliminarFilaToolStripMenuItem.Name = "eliminarFilaToolStripMenuItem";
            eliminarFilaToolStripMenuItem.Size = new Size(138, 22);
            eliminarFilaToolStripMenuItem.Text = "Eliminar Fila";
            eliminarFilaToolStripMenuItem.Click += eliminarFilaToolStripMenuItem_Click;
            // 
            // agregarFilaToolStripMenuItem
            // 
            agregarFilaToolStripMenuItem.Name = "agregarFilaToolStripMenuItem";
            agregarFilaToolStripMenuItem.Size = new Size(138, 22);
            agregarFilaToolStripMenuItem.Text = "Agregar Fila";
            agregarFilaToolStripMenuItem.Click += agregarFilaToolStripMenuItem_Click;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(groupBox2);
            tabPage2.Controls.Add(groupBox1);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(750, 624);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Datos Externos";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox2.Controls.Add(_grdSuites);
            groupBox2.Location = new Point(6, 270);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(738, 243);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Variables Globales";
            // 
            // _grdSuites
            // 
            _grdSuites.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _grdSuites.Columns.AddRange(new DataGridViewColumn[] { clmKey, clmValue });
            _grdSuites.Dock = DockStyle.Fill;
            _grdSuites.Location = new Point(3, 19);
            _grdSuites.Name = "_grdSuites";
            _grdSuites.RowTemplate.Height = 25;
            _grdSuites.Size = new Size(732, 221);
            _grdSuites.TabIndex = 0;
            // 
            // clmKey
            // 
            clmKey.HeaderText = "Nombre";
            clmKey.Name = "clmKey";
            clmKey.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // clmValue
            // 
            clmValue.HeaderText = "Valor";
            clmValue.Name = "clmValue";
            clmValue.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(_grdDatosExternos);
            groupBox1.Location = new Point(6, 6);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(735, 261);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Variables";
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button2.Location = new Point(654, 215);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 1;
            button2.Text = "Cargar";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // _grdDatosExternos
            // 
            _grdDatosExternos.AllowUserToAddRows = false;
            _grdDatosExternos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _grdDatosExternos.Dock = DockStyle.Fill;
            _grdDatosExternos.Location = new Point(3, 19);
            _grdDatosExternos.Name = "_grdDatosExternos";
            _grdDatosExternos.RowTemplate.Height = 25;
            _grdDatosExternos.Size = new Size(729, 239);
            _grdDatosExternos.TabIndex = 0;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(_txtJson);
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(750, 624);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Petición";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // _txtJson
            // 
            _txtJson.Dock = DockStyle.Fill;
            _txtJson.Location = new Point(3, 3);
            _txtJson.Name = "_txtJson";
            _txtJson.Size = new Size(744, 618);
            _txtJson.TabIndex = 1;
            _txtJson.Text = "";
            // 
            // clmIterar
            // 
            clmIterar.HeaderText = "Iterar";
            clmIterar.Name = "clmIterar";
            // 
            // clmEjecutarFin
            // 
            clmEjecutarFin.HeaderText = "Ejecutar Fin";
            clmEjecutarFin.Name = "clmEjecutarFin";
            // 
            // clmInicio
            // 
            clmInicio.HeaderText = "Ejecutar Inicio";
            clmInicio.Name = "clmInicio";
            // 
            // clmScriptValor
            // 
            clmScriptValor.DataPropertyName = "value";
            clmScriptValor.HeaderText = "Valor";
            clmScriptValor.Name = "clmScriptValor";
            // 
            // clmScriptTarget
            // 
            clmScriptTarget.DataPropertyName = "target";
            clmScriptTarget.HeaderText = "Target";
            clmScriptTarget.Name = "clmScriptTarget";
            // 
            // clmScriptCommand
            // 
            clmScriptCommand.DataPropertyName = "command";
            clmScriptCommand.HeaderText = "Comando";
            clmScriptCommand.Name = "clmScriptCommand";
            // 
            // clmScriptId
            // 
            clmScriptId.DataPropertyName = "id";
            clmScriptId.HeaderText = "Id";
            clmScriptId.Name = "clmScriptId";
            // 
            // treeView1
            // 
            treeView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            treeView1.ContextMenuStrip = _mnuTree;
            treeView1.Location = new Point(12, 58);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(262, 652);
            treeView1.TabIndex = 4;
            treeView1.AfterSelect += treeView1_AfterSelect;
            // 
            // _mnuTree
            // 
            _mnuTree.Items.AddRange(new ToolStripItem[] { actualizarToolStripMenuItem });
            _mnuTree.Name = "_mnuTree";
            _mnuTree.Size = new Size(127, 26);
            // 
            // actualizarToolStripMenuItem
            // 
            actualizarToolStripMenuItem.Name = "actualizarToolStripMenuItem";
            actualizarToolStripMenuItem.Size = new Size(126, 22);
            actualizarToolStripMenuItem.Text = "Actualizar";
            actualizarToolStripMenuItem.Click += actualizarToolStripMenuItem_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Dock = DockStyle.Top;
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripDropDownButton2, toolStripDropDownButton1 });
            statusStrip1.Location = new Point(0, 0);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1152, 22);
            statusStrip1.TabIndex = 5;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripDropDownButton2
            // 
            toolStripDropDownButton2.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownButton2.DropDownItems.AddRange(new ToolStripItem[] { generarScriptToolStripMenuItem, guardarToolStripMenuItem });
            toolStripDropDownButton2.Image = Properties.Resources.guardar;
            toolStripDropDownButton2.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            toolStripDropDownButton2.Size = new Size(61, 20);
            toolStripDropDownButton2.Text = "Archivo";
            // 
            // generarScriptToolStripMenuItem
            // 
            generarScriptToolStripMenuItem.Name = "generarScriptToolStripMenuItem";
            generarScriptToolStripMenuItem.Size = new Size(148, 22);
            generarScriptToolStripMenuItem.Text = "Generar Script";
            generarScriptToolStripMenuItem.Click += generarScriptToolStripMenuItem_Click;
            // 
            // guardarToolStripMenuItem
            // 
            guardarToolStripMenuItem.Image = Properties.Resources.guardar;
            guardarToolStripMenuItem.Name = "guardarToolStripMenuItem";
            guardarToolStripMenuItem.Size = new Size(148, 22);
            guardarToolStripMenuItem.Text = "Guardar";
            guardarToolStripMenuItem.Click += guardarToolStripMenuItem_Click;
            // 
            // toolStripDropDownButton1
            // 
            toolStripDropDownButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownButton1.DropDownItems.AddRange(new ToolStripItem[] { chromeToolStripMenuItem, edgeToolStripMenuItem });
            toolStripDropDownButton1.Image = Properties.Resources.edge;
            toolStripDropDownButton1.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            toolStripDropDownButton1.Size = new Size(62, 20);
            toolStripDropDownButton1.Text = "Ejecutar";
            // 
            // chromeToolStripMenuItem
            // 
            chromeToolStripMenuItem.Image = Properties.Resources.chrome;
            chromeToolStripMenuItem.Name = "chromeToolStripMenuItem";
            chromeToolStripMenuItem.Size = new Size(117, 22);
            chromeToolStripMenuItem.Text = "Chrome";
            chromeToolStripMenuItem.Click += chromeToolStripMenuItem_Click;
            // 
            // edgeToolStripMenuItem
            // 
            edgeToolStripMenuItem.Image = Properties.Resources.edge;
            edgeToolStripMenuItem.Name = "edgeToolStripMenuItem";
            edgeToolStripMenuItem.Size = new Size(117, 22);
            edgeToolStripMenuItem.Text = "Edge";
            edgeToolStripMenuItem.Click += edgeToolStripMenuItem_Click;
            // 
            // _txtUrl
            // 
            _txtUrl.Location = new Point(721, 6);
            _txtUrl.Name = "_txtUrl";
            _txtUrl.Size = new Size(412, 23);
            _txtUrl.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(682, 10);
            label1.Name = "label1";
            label1.Size = new Size(31, 15);
            label1.TabIndex = 7;
            label1.Text = "URL:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(661, 43);
            label2.Name = "label2";
            label2.Size = new Size(54, 15);
            label2.TabIndex = 9;
            label2.Text = "Nombre:";
            // 
            // _txtNombreTest
            // 
            _txtNombreTest.Location = new Point(721, 35);
            _txtNombreTest.Name = "_txtNombreTest";
            _txtNombreTest.Size = new Size(412, 23);
            _txtNombreTest.TabIndex = 8;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1152, 722);
            Controls.Add(label2);
            Controls.Add(_txtNombreTest);
            Controls.Add(label1);
            Controls.Add(_txtUrl);
            Controls.Add(statusStrip1);
            Controls.Add(treeView1);
            Controls.Add(_tab);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            _tab.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_grdScript).EndInit();
            _menuGridScript.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_grdSuites).EndInit();
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_grdDatosExternos).EndInit();
            tabPage3.ResumeLayout(false);
            _mnuTree.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TabControl _tab;
        private TabPage tabPage2;
        private DataGridView _grdDatosExternos;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private DataGridView _grdSuites;
        private TabPage tabPage1;
        private DataGridView _grdScript;
        private DataGridViewCheckBoxColumn clmIterar;
        private DataGridViewCheckBoxColumn clmEjecutarFin;
        private DataGridViewCheckBoxColumn clmInicio;
        private DataGridViewTextBoxColumn clmScriptValor;
        private DataGridViewTextBoxColumn clmScriptTarget;
        private DataGridViewTextBoxColumn clmScriptCommand;
        private DataGridViewTextBoxColumn clmScriptId;
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
        private TabPage tabPage3;
        private RichTextBox _txtJson;
        private RichTextBox richTextBox1;
        private ContextMenuStrip _menuGridScript;
        private ToolStripMenuItem eliminarFilaToolStripMenuItem;
        private ToolStripMenuItem agregarFilaToolStripMenuItem;
        private TreeView treeView1;
        private ContextMenuStrip _mnuTree;
        private ToolStripMenuItem actualizarToolStripMenuItem;
        private StatusStrip statusStrip1;
        private ToolStripDropDownButton toolStripDropDownButton2;
        private ToolStripMenuItem guardarToolStripMenuItem;
        private ToolStripDropDownButton toolStripDropDownButton1;
        private ToolStripMenuItem chromeToolStripMenuItem;
        private ToolStripMenuItem edgeToolStripMenuItem;
        private ToolStripMenuItem generarScriptToolStripMenuItem;
        private TextBox _txtUrl;
        private Label label1;
        private Label label2;
        private TextBox _txtNombreTest;
    }
}