namespace Flywheel.Win
{
    partial class FSystemContext
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
            cmbNamespace = new ComboBox();
            namespaceVMBindingSource = new BindingSource(components);
            lblBoundary = new Label();
            lblSystemContext = new Label();
            cmbSystemContext = new ComboBox();
            systemContextVMBindingSource = new BindingSource(components);
            dgvSystem = new DataGridView();
            labelDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            PartyTypeCode = new DataGridViewTextBoxColumn();
            RelationshipCode = new DataGridViewTextBoxColumn();
            descriptionDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            systemVMBindingSource = new BindingSource(components);
            label1 = new Label();
            dgvContainer = new DataGridView();
            labelDataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            partyTypeCodeDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            relationshipCodeDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            technicalDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            descriptionDataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            containerVMBindingSource = new BindingSource(components);
            label2 = new Label();
            label3 = new Label();
            dgvComponent = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            componentVMBindingSource = new BindingSource(components);
            ((System.ComponentModel.ISupportInitialize)namespaceVMBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)systemContextVMBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvSystem).BeginInit();
            ((System.ComponentModel.ISupportInitialize)systemVMBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvContainer).BeginInit();
            ((System.ComponentModel.ISupportInitialize)containerVMBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvComponent).BeginInit();
            ((System.ComponentModel.ISupportInitialize)componentVMBindingSource).BeginInit();
            SuspendLayout();
            // 
            // cmbNamespace
            // 
            cmbNamespace.DataSource = namespaceVMBindingSource;
            cmbNamespace.DisplayMember = "Display";
            cmbNamespace.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbNamespace.FormattingEnabled = true;
            cmbNamespace.Location = new Point(133, 15);
            cmbNamespace.Name = "cmbNamespace";
            cmbNamespace.Size = new Size(196, 33);
            cmbNamespace.TabIndex = 0;
            cmbNamespace.SelectedIndexChanged += cmbNamespace_SelectedIndexChanged;
            // 
            // lblBoundary
            // 
            lblBoundary.AutoSize = true;
            lblBoundary.Location = new Point(23, 15);
            lblBoundary.Name = "lblBoundary";
            lblBoundary.Size = new Size(104, 25);
            lblBoundary.TabIndex = 1;
            lblBoundary.Text = "Namespace";
            // 
            // lblSystemContext
            // 
            lblSystemContext.AutoSize = true;
            lblSystemContext.Location = new Point(387, 15);
            lblSystemContext.Name = "lblSystemContext";
            lblSystemContext.Size = new Size(130, 25);
            lblSystemContext.TabIndex = 2;
            lblSystemContext.Text = "SystemContext";
            // 
            // cmbSystemContext
            // 
            cmbSystemContext.DataSource = systemContextVMBindingSource;
            cmbSystemContext.DisplayMember = "Display";
            cmbSystemContext.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSystemContext.FormattingEnabled = true;
            cmbSystemContext.Location = new Point(523, 15);
            cmbSystemContext.Name = "cmbSystemContext";
            cmbSystemContext.Size = new Size(531, 33);
            cmbSystemContext.TabIndex = 3;
            cmbSystemContext.SelectedIndexChanged += cmbSystemContext_SelectedIndexChanged;
            // 
            // systemContextVMBindingSource
            // 
            systemContextVMBindingSource.DataSource = typeof(ViewModels.SystemContextVM);
            // 
            // dgvSystem
            // 
            dgvSystem.AllowUserToAddRows = false;
            dgvSystem.AllowUserToDeleteRows = false;
            dgvSystem.AutoGenerateColumns = false;
            dgvSystem.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSystem.Columns.AddRange(new DataGridViewColumn[] { labelDataGridViewTextBoxColumn, PartyTypeCode, RelationshipCode, descriptionDataGridViewTextBoxColumn });
            dgvSystem.DataSource = systemVMBindingSource;
            dgvSystem.Location = new Point(133, 62);
            dgvSystem.Name = "dgvSystem";
            dgvSystem.ReadOnly = true;
            dgvSystem.RowHeadersWidth = 62;
            dgvSystem.Size = new Size(921, 241);
            dgvSystem.TabIndex = 4;
            dgvSystem.CellClick += dgvSystem_CellClick;
            dgvSystem.RowHeaderMouseClick += dgvSystem_RowHeaderMouseClick;
            // 
            // labelDataGridViewTextBoxColumn
            // 
            labelDataGridViewTextBoxColumn.DataPropertyName = "Label";
            labelDataGridViewTextBoxColumn.HeaderText = "Label";
            labelDataGridViewTextBoxColumn.MinimumWidth = 8;
            labelDataGridViewTextBoxColumn.Name = "labelDataGridViewTextBoxColumn";
            labelDataGridViewTextBoxColumn.ReadOnly = true;
            labelDataGridViewTextBoxColumn.Width = 200;
            // 
            // PartyTypeCode
            // 
            PartyTypeCode.DataPropertyName = "PartyTypeCode";
            PartyTypeCode.HeaderText = "Type";
            PartyTypeCode.MinimumWidth = 8;
            PartyTypeCode.Name = "PartyTypeCode";
            PartyTypeCode.ReadOnly = true;
            PartyTypeCode.Width = 150;
            // 
            // RelationshipCode
            // 
            RelationshipCode.DataPropertyName = "RelationshipCode";
            RelationshipCode.HeaderText = "Rel";
            RelationshipCode.MinimumWidth = 8;
            RelationshipCode.Name = "RelationshipCode";
            RelationshipCode.ReadOnly = true;
            RelationshipCode.Width = 150;
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            descriptionDataGridViewTextBoxColumn.HeaderText = "Description";
            descriptionDataGridViewTextBoxColumn.MinimumWidth = 8;
            descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            descriptionDataGridViewTextBoxColumn.ReadOnly = true;
            descriptionDataGridViewTextBoxColumn.Width = 300;
            // 
            // systemVMBindingSource
            // 
            systemVMBindingSource.DataSource = typeof(ViewModels.SystemVM);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(50, 62);
            label1.Name = "label1";
            label1.Size = new Size(77, 25);
            label1.TabIndex = 5;
            label1.Text = "Systems";
            // 
            // dgvContainer
            // 
            dgvContainer.AllowUserToAddRows = false;
            dgvContainer.AllowUserToDeleteRows = false;
            dgvContainer.AutoGenerateColumns = false;
            dgvContainer.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvContainer.Columns.AddRange(new DataGridViewColumn[] { labelDataGridViewTextBoxColumn1, partyTypeCodeDataGridViewTextBoxColumn, relationshipCodeDataGridViewTextBoxColumn, technicalDataGridViewTextBoxColumn, descriptionDataGridViewTextBoxColumn1 });
            dgvContainer.DataSource = containerVMBindingSource;
            dgvContainer.Location = new Point(133, 321);
            dgvContainer.Name = "dgvContainer";
            dgvContainer.ReadOnly = true;
            dgvContainer.RowHeadersWidth = 62;
            dgvContainer.Size = new Size(921, 304);
            dgvContainer.TabIndex = 6;
            dgvContainer.CellClick += dgvContainer_CellClick;
            dgvContainer.RowHeaderMouseClick += dgvContainer_RowHeaderMouseClick;
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
            // partyTypeCodeDataGridViewTextBoxColumn
            // 
            partyTypeCodeDataGridViewTextBoxColumn.DataPropertyName = "PartyTypeCode";
            partyTypeCodeDataGridViewTextBoxColumn.HeaderText = "Type";
            partyTypeCodeDataGridViewTextBoxColumn.MinimumWidth = 8;
            partyTypeCodeDataGridViewTextBoxColumn.Name = "partyTypeCodeDataGridViewTextBoxColumn";
            partyTypeCodeDataGridViewTextBoxColumn.ReadOnly = true;
            partyTypeCodeDataGridViewTextBoxColumn.Width = 150;
            // 
            // relationshipCodeDataGridViewTextBoxColumn
            // 
            relationshipCodeDataGridViewTextBoxColumn.DataPropertyName = "RelationshipCode";
            relationshipCodeDataGridViewTextBoxColumn.HeaderText = "Rel";
            relationshipCodeDataGridViewTextBoxColumn.MinimumWidth = 8;
            relationshipCodeDataGridViewTextBoxColumn.Name = "relationshipCodeDataGridViewTextBoxColumn";
            relationshipCodeDataGridViewTextBoxColumn.ReadOnly = true;
            relationshipCodeDataGridViewTextBoxColumn.Width = 150;
            // 
            // technicalDataGridViewTextBoxColumn
            // 
            technicalDataGridViewTextBoxColumn.DataPropertyName = "Technical";
            technicalDataGridViewTextBoxColumn.HeaderText = "Technical";
            technicalDataGridViewTextBoxColumn.MinimumWidth = 8;
            technicalDataGridViewTextBoxColumn.Name = "technicalDataGridViewTextBoxColumn";
            technicalDataGridViewTextBoxColumn.ReadOnly = true;
            technicalDataGridViewTextBoxColumn.Width = 150;
            // 
            // descriptionDataGridViewTextBoxColumn1
            // 
            descriptionDataGridViewTextBoxColumn1.DataPropertyName = "Description";
            descriptionDataGridViewTextBoxColumn1.HeaderText = "Description";
            descriptionDataGridViewTextBoxColumn1.MinimumWidth = 8;
            descriptionDataGridViewTextBoxColumn1.Name = "descriptionDataGridViewTextBoxColumn1";
            descriptionDataGridViewTextBoxColumn1.ReadOnly = true;
            descriptionDataGridViewTextBoxColumn1.Width = 300;
            // 
            // containerVMBindingSource
            // 
            containerVMBindingSource.DataSource = typeof(ViewModels.ContainerVM);
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(31, 321);
            label2.Name = "label2";
            label2.Size = new Size(96, 25);
            label2.TabIndex = 7;
            label2.Text = "Containers";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 639);
            label3.Name = "label3";
            label3.Size = new Size(115, 25);
            label3.TabIndex = 8;
            label3.Text = "Components";
            // 
            // dgvComponent
            // 
            dgvComponent.AllowUserToAddRows = false;
            dgvComponent.AllowUserToDeleteRows = false;
            dgvComponent.AutoGenerateColumns = false;
            dgvComponent.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvComponent.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4, dataGridViewTextBoxColumn5 });
            dgvComponent.DataSource = componentVMBindingSource;
            dgvComponent.Location = new Point(133, 639);
            dgvComponent.Name = "dgvComponent";
            dgvComponent.ReadOnly = true;
            dgvComponent.RowHeadersWidth = 62;
            dgvComponent.Size = new Size(921, 297);
            dgvComponent.TabIndex = 9;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.DataPropertyName = "Label";
            dataGridViewTextBoxColumn1.HeaderText = "Label";
            dataGridViewTextBoxColumn1.MinimumWidth = 8;
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            dataGridViewTextBoxColumn1.Width = 300;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.DataPropertyName = "PartyTypeCode";
            dataGridViewTextBoxColumn2.HeaderText = "Type";
            dataGridViewTextBoxColumn2.MinimumWidth = 8;
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.ReadOnly = true;
            dataGridViewTextBoxColumn2.Width = 150;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.DataPropertyName = "RelationshipCode";
            dataGridViewTextBoxColumn3.HeaderText = "Rel";
            dataGridViewTextBoxColumn3.MinimumWidth = 8;
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.ReadOnly = true;
            dataGridViewTextBoxColumn3.Width = 150;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.DataPropertyName = "Technical";
            dataGridViewTextBoxColumn4.HeaderText = "Technical";
            dataGridViewTextBoxColumn4.MinimumWidth = 8;
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.ReadOnly = true;
            dataGridViewTextBoxColumn4.Width = 150;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewTextBoxColumn5.DataPropertyName = "Description";
            dataGridViewTextBoxColumn5.HeaderText = "Description";
            dataGridViewTextBoxColumn5.MinimumWidth = 8;
            dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            dataGridViewTextBoxColumn5.ReadOnly = true;
            dataGridViewTextBoxColumn5.Width = 150;
            // 
            // componentVMBindingSource
            // 
            componentVMBindingSource.DataSource = typeof(ViewModels.ComponentVM);
            // 
            // FSystemContext
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1331, 948);
            Controls.Add(dgvComponent);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(dgvContainer);
            Controls.Add(label1);
            Controls.Add(dgvSystem);
            Controls.Add(cmbSystemContext);
            Controls.Add(lblSystemContext);
            Controls.Add(lblBoundary);
            Controls.Add(cmbNamespace);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "FSystemContext";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SystemContext";
            Load += FSystemContext_Load;
            ((System.ComponentModel.ISupportInitialize)namespaceVMBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)systemContextVMBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvSystem).EndInit();
            ((System.ComponentModel.ISupportInitialize)systemVMBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvContainer).EndInit();
            ((System.ComponentModel.ISupportInitialize)containerVMBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvComponent).EndInit();
            ((System.ComponentModel.ISupportInitialize)componentVMBindingSource).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbNamespace;
        private Label lblBoundary;
        private BindingSource namespaceVMBindingSource;
        private Label lblSystemContext;
        private ComboBox cmbSystemContext;
        private BindingSource systemContextVMBindingSource;
        private DataGridView dgvSystem;
        private BindingSource systemVMBindingSource;
        private DataGridViewTextBoxColumn labelDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn PartyTypeCode;
        private DataGridViewTextBoxColumn RelationshipCode;
        private DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private Label label1;
        private DataGridView dgvContainer;
        private Label label2;
        private BindingSource containerVMBindingSource;
        private Label label3;
        private DataGridView dgvComponent;
        private BindingSource componentVMBindingSource;
        private DataGridViewTextBoxColumn labelDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn partyTypeCodeDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn relationshipCodeDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn technicalDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
    }
}
