namespace Flywheel.Win.SystemContexts
{
    partial class FSaveSystemContext
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
            components = new System.ComponentModel.Container();
            label1 = new Label();
            txtAlias = new TextBox();
            label2 = new Label();
            txtLabel = new TextBox();
            label3 = new Label();
            rtxDescr = new RichTextBox();
            label4 = new Label();
            txtTechn = new TextBox();
            btnSave = new Button();
            tbRels = new TabControl();
            tbAfferents = new TabPage();
            dgvAfferents = new DataGridView();
            elementValueBindingSource = new BindingSource(components);
            tbEfferents = new TabPage();
            dgvEfferents = new DataGridView();
            aliasDataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            labelDataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            elementTypeCodeDataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            partyTypeCodeDataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            descriptionDataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            technDataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            elementValueBindingSource1 = new BindingSource(components);
            aliasDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            labelDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            elementTypeCodeDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            partyTypeCodeDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            descriptionDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            technDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            tbRels.SuspendLayout();
            tbAfferents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAfferents).BeginInit();
            ((System.ComponentModel.ISupportInitialize)elementValueBindingSource).BeginInit();
            tbEfferents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEfferents).BeginInit();
            ((System.ComponentModel.ISupportInitialize)elementValueBindingSource1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 12);
            label1.Name = "label1";
            label1.Size = new Size(49, 25);
            label1.TabIndex = 0;
            label1.Text = "Alias";
            // 
            // txtAlias
            // 
            txtAlias.Location = new Point(82, 12);
            txtAlias.Name = "txtAlias";
            txtAlias.Size = new Size(632, 31);
            txtAlias.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 49);
            label2.Name = "label2";
            label2.Size = new Size(53, 25);
            label2.TabIndex = 2;
            label2.Text = "Label";
            // 
            // txtLabel
            // 
            txtLabel.Location = new Point(82, 49);
            txtLabel.Name = "txtLabel";
            txtLabel.Size = new Size(632, 31);
            txtLabel.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(9, 86);
            label3.Name = "label3";
            label3.Size = new Size(56, 25);
            label3.TabIndex = 4;
            label3.Text = "Descr";
            // 
            // rtxDescr
            // 
            rtxDescr.Location = new Point(82, 86);
            rtxDescr.Name = "rtxDescr";
            rtxDescr.Size = new Size(632, 134);
            rtxDescr.TabIndex = 5;
            rtxDescr.Text = "";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 226);
            label4.Name = "label4";
            label4.Size = new Size(56, 25);
            label4.TabIndex = 6;
            label4.Text = "Techn";
            // 
            // txtTechn
            // 
            txtTechn.Location = new Point(82, 226);
            txtTechn.Name = "txtTechn";
            txtTechn.Size = new Size(632, 31);
            txtTechn.TabIndex = 7;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(308, 263);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(112, 34);
            btnSave.TabIndex = 8;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // tbRels
            // 
            tbRels.Controls.Add(tbAfferents);
            tbRels.Controls.Add(tbEfferents);
            tbRels.Location = new Point(12, 303);
            tbRels.Name = "tbRels";
            tbRels.SelectedIndex = 0;
            tbRels.Size = new Size(708, 355);
            tbRels.TabIndex = 9;
            // 
            // tbAfferents
            // 
            tbAfferents.Controls.Add(dgvAfferents);
            tbAfferents.Location = new Point(4, 34);
            tbAfferents.Name = "tbAfferents";
            tbAfferents.Padding = new Padding(3);
            tbAfferents.Size = new Size(700, 317);
            tbAfferents.TabIndex = 0;
            tbAfferents.Text = "Afferents";
            tbAfferents.UseVisualStyleBackColor = true;
            // 
            // dgvAfferents
            // 
            dgvAfferents.AllowUserToAddRows = false;
            dgvAfferents.AllowUserToDeleteRows = false;
            dgvAfferents.AutoGenerateColumns = false;
            dgvAfferents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAfferents.Columns.AddRange(new DataGridViewColumn[] { aliasDataGridViewTextBoxColumn, labelDataGridViewTextBoxColumn, elementTypeCodeDataGridViewTextBoxColumn, partyTypeCodeDataGridViewTextBoxColumn, descriptionDataGridViewTextBoxColumn, technDataGridViewTextBoxColumn });
            dgvAfferents.DataSource = elementValueBindingSource;
            dgvAfferents.Dock = DockStyle.Fill;
            dgvAfferents.Location = new Point(3, 3);
            dgvAfferents.Name = "dgvAfferents";
            dgvAfferents.ReadOnly = true;
            dgvAfferents.RowHeadersWidth = 62;
            dgvAfferents.Size = new Size(694, 311);
            dgvAfferents.TabIndex = 0;
            // 
            // elementValueBindingSource
            // 
            elementValueBindingSource.DataSource = typeof(Mappings.Values.ElementValue);
            // 
            // tbEfferents
            // 
            tbEfferents.Controls.Add(dgvEfferents);
            tbEfferents.Location = new Point(4, 34);
            tbEfferents.Name = "tbEfferents";
            tbEfferents.Padding = new Padding(3);
            tbEfferents.Size = new Size(700, 317);
            tbEfferents.TabIndex = 1;
            tbEfferents.Text = "Efferents";
            tbEfferents.UseVisualStyleBackColor = true;
            // 
            // dgvEfferents
            // 
            dgvEfferents.AllowUserToAddRows = false;
            dgvEfferents.AllowUserToDeleteRows = false;
            dgvEfferents.AutoGenerateColumns = false;
            dgvEfferents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEfferents.Columns.AddRange(new DataGridViewColumn[] { aliasDataGridViewTextBoxColumn1, labelDataGridViewTextBoxColumn1, elementTypeCodeDataGridViewTextBoxColumn1, partyTypeCodeDataGridViewTextBoxColumn1, descriptionDataGridViewTextBoxColumn1, technDataGridViewTextBoxColumn1 });
            dgvEfferents.DataSource = elementValueBindingSource1;
            dgvEfferents.Dock = DockStyle.Fill;
            dgvEfferents.Location = new Point(3, 3);
            dgvEfferents.Name = "dgvEfferents";
            dgvEfferents.ReadOnly = true;
            dgvEfferents.RowHeadersWidth = 62;
            dgvEfferents.Size = new Size(694, 311);
            dgvEfferents.TabIndex = 0;
            // 
            // aliasDataGridViewTextBoxColumn1
            // 
            aliasDataGridViewTextBoxColumn1.DataPropertyName = "Alias";
            aliasDataGridViewTextBoxColumn1.HeaderText = "Alias";
            aliasDataGridViewTextBoxColumn1.MinimumWidth = 8;
            aliasDataGridViewTextBoxColumn1.Name = "aliasDataGridViewTextBoxColumn1";
            aliasDataGridViewTextBoxColumn1.ReadOnly = true;
            aliasDataGridViewTextBoxColumn1.Width = 150;
            // 
            // labelDataGridViewTextBoxColumn1
            // 
            labelDataGridViewTextBoxColumn1.DataPropertyName = "Label";
            labelDataGridViewTextBoxColumn1.HeaderText = "Label";
            labelDataGridViewTextBoxColumn1.MinimumWidth = 8;
            labelDataGridViewTextBoxColumn1.Name = "labelDataGridViewTextBoxColumn1";
            labelDataGridViewTextBoxColumn1.ReadOnly = true;
            labelDataGridViewTextBoxColumn1.Width = 150;
            // 
            // elementTypeCodeDataGridViewTextBoxColumn1
            // 
            elementTypeCodeDataGridViewTextBoxColumn1.DataPropertyName = "ElementTypeCode";
            elementTypeCodeDataGridViewTextBoxColumn1.HeaderText = "ElementTypeCode";
            elementTypeCodeDataGridViewTextBoxColumn1.MinimumWidth = 8;
            elementTypeCodeDataGridViewTextBoxColumn1.Name = "elementTypeCodeDataGridViewTextBoxColumn1";
            elementTypeCodeDataGridViewTextBoxColumn1.ReadOnly = true;
            elementTypeCodeDataGridViewTextBoxColumn1.Width = 150;
            // 
            // partyTypeCodeDataGridViewTextBoxColumn1
            // 
            partyTypeCodeDataGridViewTextBoxColumn1.DataPropertyName = "PartyTypeCode";
            partyTypeCodeDataGridViewTextBoxColumn1.HeaderText = "PartyTypeCode";
            partyTypeCodeDataGridViewTextBoxColumn1.MinimumWidth = 8;
            partyTypeCodeDataGridViewTextBoxColumn1.Name = "partyTypeCodeDataGridViewTextBoxColumn1";
            partyTypeCodeDataGridViewTextBoxColumn1.ReadOnly = true;
            partyTypeCodeDataGridViewTextBoxColumn1.Width = 150;
            // 
            // descriptionDataGridViewTextBoxColumn1
            // 
            descriptionDataGridViewTextBoxColumn1.DataPropertyName = "Description";
            descriptionDataGridViewTextBoxColumn1.HeaderText = "Description";
            descriptionDataGridViewTextBoxColumn1.MinimumWidth = 8;
            descriptionDataGridViewTextBoxColumn1.Name = "descriptionDataGridViewTextBoxColumn1";
            descriptionDataGridViewTextBoxColumn1.ReadOnly = true;
            descriptionDataGridViewTextBoxColumn1.Width = 150;
            // 
            // technDataGridViewTextBoxColumn1
            // 
            technDataGridViewTextBoxColumn1.DataPropertyName = "Techn";
            technDataGridViewTextBoxColumn1.HeaderText = "Techn";
            technDataGridViewTextBoxColumn1.MinimumWidth = 8;
            technDataGridViewTextBoxColumn1.Name = "technDataGridViewTextBoxColumn1";
            technDataGridViewTextBoxColumn1.ReadOnly = true;
            technDataGridViewTextBoxColumn1.Width = 150;
            // 
            // elementValueBindingSource1
            // 
            elementValueBindingSource1.DataSource = typeof(Mappings.Values.ElementValue);
            // 
            // aliasDataGridViewTextBoxColumn
            // 
            aliasDataGridViewTextBoxColumn.DataPropertyName = "Alias";
            aliasDataGridViewTextBoxColumn.HeaderText = "Alias";
            aliasDataGridViewTextBoxColumn.MinimumWidth = 8;
            aliasDataGridViewTextBoxColumn.Name = "aliasDataGridViewTextBoxColumn";
            aliasDataGridViewTextBoxColumn.ReadOnly = true;
            aliasDataGridViewTextBoxColumn.Width = 150;
            // 
            // labelDataGridViewTextBoxColumn
            // 
            labelDataGridViewTextBoxColumn.DataPropertyName = "Label";
            labelDataGridViewTextBoxColumn.HeaderText = "Label";
            labelDataGridViewTextBoxColumn.MinimumWidth = 8;
            labelDataGridViewTextBoxColumn.Name = "labelDataGridViewTextBoxColumn";
            labelDataGridViewTextBoxColumn.ReadOnly = true;
            labelDataGridViewTextBoxColumn.Width = 150;
            // 
            // elementTypeCodeDataGridViewTextBoxColumn
            // 
            elementTypeCodeDataGridViewTextBoxColumn.DataPropertyName = "ElementTypeCode";
            elementTypeCodeDataGridViewTextBoxColumn.HeaderText = "ElementType";
            elementTypeCodeDataGridViewTextBoxColumn.MinimumWidth = 8;
            elementTypeCodeDataGridViewTextBoxColumn.Name = "elementTypeCodeDataGridViewTextBoxColumn";
            elementTypeCodeDataGridViewTextBoxColumn.ReadOnly = true;
            elementTypeCodeDataGridViewTextBoxColumn.Width = 150;
            // 
            // partyTypeCodeDataGridViewTextBoxColumn
            // 
            partyTypeCodeDataGridViewTextBoxColumn.DataPropertyName = "PartyTypeCode";
            partyTypeCodeDataGridViewTextBoxColumn.HeaderText = "PartyType";
            partyTypeCodeDataGridViewTextBoxColumn.MinimumWidth = 8;
            partyTypeCodeDataGridViewTextBoxColumn.Name = "partyTypeCodeDataGridViewTextBoxColumn";
            partyTypeCodeDataGridViewTextBoxColumn.ReadOnly = true;
            partyTypeCodeDataGridViewTextBoxColumn.Width = 150;
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            descriptionDataGridViewTextBoxColumn.HeaderText = "Description";
            descriptionDataGridViewTextBoxColumn.MinimumWidth = 8;
            descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            descriptionDataGridViewTextBoxColumn.ReadOnly = true;
            descriptionDataGridViewTextBoxColumn.Width = 150;
            // 
            // technDataGridViewTextBoxColumn
            // 
            technDataGridViewTextBoxColumn.DataPropertyName = "Techn";
            technDataGridViewTextBoxColumn.HeaderText = "Techn";
            technDataGridViewTextBoxColumn.MinimumWidth = 8;
            technDataGridViewTextBoxColumn.Name = "technDataGridViewTextBoxColumn";
            technDataGridViewTextBoxColumn.ReadOnly = true;
            technDataGridViewTextBoxColumn.Width = 150;
            // 
            // FSaveSystemContext
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(732, 661);
            Controls.Add(tbRels);
            Controls.Add(btnSave);
            Controls.Add(txtTechn);
            Controls.Add(label4);
            Controls.Add(rtxDescr);
            Controls.Add(label3);
            Controls.Add(txtLabel);
            Controls.Add(label2);
            Controls.Add(txtAlias);
            Controls.Add(label1);
            Name = "FSaveSystemContext";
            StartPosition = FormStartPosition.CenterParent;
            Text = "FSaveSystemContext";
            tbRels.ResumeLayout(false);
            tbAfferents.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvAfferents).EndInit();
            ((System.ComponentModel.ISupportInitialize)elementValueBindingSource).EndInit();
            tbEfferents.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvEfferents).EndInit();
            ((System.ComponentModel.ISupportInitialize)elementValueBindingSource1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtAlias;
        private Label label2;
        private TextBox txtLabel;
        private Label label3;
        private RichTextBox rtxDescr;
        private Label label4;
        private TextBox txtTechn;
        private Button btnSave;
        private TabControl tbRels;
        private TabPage tbAfferents;
        private TabPage tbEfferents;
        private DataGridView dgvAfferents;
        private DataGridView dgvEfferents;
        private BindingSource elementValueBindingSource;
        private BindingSource elementValueBindingSource1;
        private DataGridViewTextBoxColumn aliasDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn labelDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn elementTypeCodeDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn partyTypeCodeDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn technDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn aliasDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn labelDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn elementTypeCodeDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn partyTypeCodeDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn technDataGridViewTextBoxColumn;
    }
}