
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Immutable;

namespace Flywheel.Win
{
    using Flywheel.Mappings.Queries;
    using Flywheel.Mappings;
    using Flywheel.Models;
    using Flywheel.Win.ViewModels;
    using static System.ComponentModel.Design.ObjectSelectorEditor;

    public partial class FSystemContext : Form
    {
        protected readonly IConfigurationRoot _config = null;
        protected readonly FlywheelsDbContextFactory _flywheelsDbContextFactory;

        public FSystemContext()
        {
            InitializeComponent();

            _config = new ConfigurationBuilder()
.AddJsonFile("appsettings.json")
.Build();

            _flywheelsDbContextFactory = new FlywheelsDbContextFactory(_config.GetConnectionString("flywheels_db"));
        }

        private async void FSystemContext_Load(object sender, EventArgs e)
        {
            await load_namespaceVMBindingSourceAsync();
        }

        private void cmbNamespace_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSystemContext.Enabled = false;
            systemContextVMBindingSource.DataSource = null;
            containerVMBindingSource.DataSource = null;
            componentVMBindingSource.DataSource = null;
            if (cmbNamespace.SelectedItem is NamespaceVM selected)
            {
                loadData<SystemContextVM>(systemContextVMBindingSource, selected.Id, ElementType.Context);
            }
            cmbSystemContext.SelectedIndex = -1;
            cmbSystemContext.Enabled = true;
        }
        private void dgvSystem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            containerVMBindingSource.DataSource = null;
            componentVMBindingSource.DataSource = null;
        }

        private void dgvContainer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            componentVMBindingSource.DataSource = null;
        }

        private async Task load_namespaceVMBindingSourceAsync()
        {
            cmbNamespace.Enabled = false;

            using (var db = _flywheelsDbContextFactory.CreateDbContext())
            {
                var dataList = db.Namespaces.Select(x => new NamespaceVM
                {
                    Id = x.Id.Value,
                    Alias = x.Alias,
                    Description = x.Description,
                    Label = x.Label,
                })
                .ToImmutableList();

                namespaceVMBindingSource.DataSource = dataList;

            }

            cmbNamespace.SelectedIndex = -1;
            cmbNamespace.Enabled = true;
        }

        private void cmbSystemContext_SelectedIndexChanged(object sender, EventArgs e)
        {
            systemVMBindingSource.DataSource = null;
            containerVMBindingSource.DataSource = null;
            componentVMBindingSource.DataSource = null;

            if (cmbSystemContext.SelectedItem is SystemContextVM selected)
            {
                loadData<SystemVM>(systemVMBindingSource, selected.Id, selected.ElementTypeCode, ElementType.System);
            }
        }

        private void dgvSystem_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            loadElements<SystemVM, ContainerVM>(e, 
                dgvSystem, containerVMBindingSource, ElementType.Container);
        }

        private void dgvContainer_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            loadElements<ContainerVM, ComponentVM>(e,
                dgvContainer, componentVMBindingSource, ElementType.Component);
        }

        private void loadElements<T1, T2>(
                DataGridViewCellMouseEventArgs e,
                DataGridView dgv,
                BindingSource ds,
                string toElementTypeCode)
                where T1 : SystemVM, new()
                where T2 : SystemVM, new()
        {
            ds.DataSource = null;
            if (dgv.Rows[e.RowIndex].DataBoundItem is T1 selected)
            {
                if (selected.RelationshipCode != ElementRelationshipType.Inbound) return;

                loadData<T2>(ds, selected.Id, selected.ElementTypeCode, toElementTypeCode);
            }
        }

        private void loadData<T>(
            BindingSource ds,
            Guid targetId, string fromElementTypeCode, string toElementTypeCode)
            where T : SystemVM, new()
        {
            using (var db = _flywheelsDbContextFactory.CreateDbContext())
            {
                var resultList = db.GetElements(targetId, fromElementTypeCode, toElementTypeCode);
                if (!resultList.Any()) return;

                ds.DataSource = resultList.Select(x => new T
                {
                    Id = x.ElementId,
                    Alias = x.Alias,
                    Label = x.Label,
                    Description = x.Description,
                    Technical = x.Techn,
                    PartyTypeCode = x.PartyTypeCode ?? x.ElementTypeCode,
                    PartyTypeId = x.PartyTypeId,
                    ElementTypeId = x.ElementTypeId,
                    RelationshipCode = x.Relationship.TypeCode
                });

            }
        }

        private void loadData<T>(
            BindingSource ds,
            Guid namespaceIdId, 
            string elementTypeCode
            )
            where T : SystemContextVM, new()
        {
            using (var db = _flywheelsDbContextFactory.CreateDbContext())
            {
                var data = db.Elements
                    .Include(x => x.ElementType)
                    .Where(x => x.NamespaceId == namespaceIdId
                        && x.ElementType.Code == elementTypeCode)
                    .Select(x => new T
                    {
                        Id = x.Id.Value,
                        NamespaceId = x.NamespaceId,
                        Alias = x.Alias,
                        Label = x.Label,
                        ElementTypeId = x.ElementTypeId.Value,
                        ElementTypeCode = x.ElementType.Code,
                        Description = x.Description,
                        PartyTypeId = x.PartyTypeId,
                        Technical = x.Technical,
                    })
                    .ToImmutableList();

                ds.DataSource = data;
            }
        }

    }
}
