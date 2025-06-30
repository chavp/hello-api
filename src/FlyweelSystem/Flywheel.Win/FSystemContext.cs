
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Immutable;

namespace Flywheel.Win
{
    using Flywheel.Mappings;
    using Flywheel.Mappings.Queries;
    using Flywheel.Models;
    using Flywheel.Win.SystemContexts;
    using Flywheel.Win.ViewModels;
    using System.Xml;

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
            loadElements<SystemVM, ContainerVM>(e.RowIndex,
                dgvSystem, containerVMBindingSource, ElementType.Container);

        }

        private void dgvContainer_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            loadElements<ContainerVM, ComponentVM>(e.RowIndex,
                dgvContainer, componentVMBindingSource, ElementType.Component);
        }

        private void dgvSystem_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            saveElements<SystemContextVM>(e.RowIndex, dgvSystem);
        }

        private void dgvContainer_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            saveElements<SystemContextVM>(e.RowIndex, dgvContainer);
        }

        private void dgvComponent_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            saveElements<SystemContextVM>(e.RowIndex, dgvComponent);
        }

        private void btnDelSysCtx_Click(object sender, EventArgs e)
        {
            var eff = delElement<SystemContextVM>(dgvSystem);
            if (eff > 0)
            {
                loadElements<SystemVM, ContainerVM>(0,
                    dgvSystem, containerVMBindingSource, ElementType.Container);
            }
        }

        private void btnDelContainser_Click(object sender, EventArgs e)
        {
            var eff = delElement<ContainerVM>(dgvContainer);
            if (eff > 0)
            {
                loadElements<ContainerVM, ComponentVM>(0,
                    dgvContainer, componentVMBindingSource, ElementType.Component);
            }
        }

        private void btnDelComponent_Click(object sender, EventArgs e)
        {
            var eff = delElement<ComponentVM>(dgvContainer);
            if (eff > 0)
            {
                loadElements<ContainerVM, ComponentVM>(0,
                    dgvComponent, componentVMBindingSource, ElementType.Component);
            }
        }

        private int delElement<T>(DataGridView dgv)
            where T : SystemContextVM
        {
            var selected = getElements<T>(0, dgv);
            if (selected == null) return 0;

            using (var db = _flywheelsDbContextFactory.CreateDbContext())
            {
                var target = db.Elements.Single(x => x.Id == selected.Id);
                var re = db.ElementRelationships
                    .Count(x => x.FromElementId == target.Id
                    || x.ToElementId == target.Id)
                    ;
                if (re > 0)
                {
                    MessageBox.Show(this, "Can't delete element with relationships");
                    return 0;
                }
                db.Remove(selected);
                return db.SaveChanges();
            }
        }

        private void loadElements<T1, T2>(
                int rowIndex,
                DataGridView dgv,
                BindingSource ds,
                string toElementTypeCode)
                where T1 : SystemVM, new()
                where T2 : SystemVM, new()
        {
            ds.DataSource = null;
            if (dgv.Rows[rowIndex].DataBoundItem is T1 selected)
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

        private void saveElements<T>(
                int rowIndex,
                DataGridView dgv)
                where T : SystemContextVM
        {
            var selected = getElements<T>(rowIndex, dgv);
            if (selected != null)
            {
                var form = new FSaveSystemContext(selected, _flywheelsDbContextFactory);
                if (DialogResult.OK == form.ShowDialog(this))
                {

                }
            }
        }
        private T? getElements<T>(
                int rowIndex,
                DataGridView dgv)
                where T : SystemContextVM
        {
            if (dgv.Rows[rowIndex].DataBoundItem is T selected)
            {
                return selected;
            }
            return null;
        }
    }
}
