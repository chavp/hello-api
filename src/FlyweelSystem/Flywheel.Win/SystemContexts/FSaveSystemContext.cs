using Flywheel.Mappings;
using Flywheel.Mappings.Values;
using Flywheel.Models;
using Flywheel.Win.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text;

using Flywheel.Mappings.Queries;
using System.Collections.Immutable;

namespace Flywheel.Win.SystemContexts
{
    public partial class FSaveSystemContext : Form
    {
        SystemContextVM _targetSystemContextVM;
        protected readonly FlywheelsDbContextFactory _flywheelsDbContextFactory;

        public FSaveSystemContext(
            SystemContextVM targetSystemContextVM,
            FlywheelsDbContextFactory flywheelsDbContextFactory)
        {
            InitializeComponent();

            _targetSystemContextVM = targetSystemContextVM;
            _flywheelsDbContextFactory = flywheelsDbContextFactory;

            setData();
            loadRels();
        }

        private void setData()
        {
            txtAlias.Text = _targetSystemContextVM.Alias;
            txtLabel.Text = _targetSystemContextVM.Label;
            rtxDescr.Text = _targetSystemContextVM.Description;
            txtTechn.Text = _targetSystemContextVM.Technical;
        }
        private void getData(Models.Element elem)
        {
            _targetSystemContextVM.Alias = txtAlias.Text;
            _targetSystemContextVM.Label = txtLabel.Text;
            _targetSystemContextVM.Description = rtxDescr.Text;
            _targetSystemContextVM.Technical = txtTechn.Text;

            elem.Alias = _targetSystemContextVM.Alias;
            elem.Label = _targetSystemContextVM.Label;
            elem.Description = _targetSystemContextVM.Description;
            elem.Technical = _targetSystemContextVM.Technical;
        }
        private void loadRels()
        {
            dgvAfferents.DataSource = null;
            dgvEfferents.DataSource = null;

            var fromElementTypeCode = ElementType.Context;
            var toElementTypeCode = ElementType.System;
            using (var db = _flywheelsDbContextFactory.CreateDbContext())
            {
                var target = db.Elements
                    .Include(x => x.Namespace)
                    .Include(x => x.ElementType)
                    .Include(x => x.PartyType)
                    .Single(x => x.Id == _targetSystemContextVM.Id);

                // ElementType.Context, ElementType.System
                if (target.ElementType.Code == ElementType.System)
                {
                    fromElementTypeCode = ElementType.System;
                    toElementTypeCode = ElementType.Container;
                    //var ctx = getElement(_systemContextVM.Alias, ElementType.System, ElementType.Container);

                    //dgvAfferents.DataSource = ctx.AfferentElements;
                    //dgvEfferents.DataSource = ctx.EfferentElements;

                }
                else if (target.ElementType.Code == ElementType.Container)
                {
                    fromElementTypeCode = ElementType.System;
                    toElementTypeCode = ElementType.Component;
                }
                else if (target.ElementType.Code == ElementType.Component)
                {
                    fromElementTypeCode = ElementType.Container;
                    toElementTypeCode = ElementType.Component;
                }

                var ctx = getElement(target, fromElementTypeCode, toElementTypeCode);

                dgvAfferents.DataSource = ctx.AfferentElements;
                dgvEfferents.DataSource = ctx.EfferentElements;
            }
        }

        protected ElementValue getElement(Element target, string fromTypeCode, string toTypeCode)
        {
            using (var db = _flywheelsDbContextFactory.CreateDbContext())
            {
                //var sys = db.Elements
                //    .Include(x => x.ElementType)
                //    .Include(x => x.Namespace)
                //    .Include(x => x.PartyType)
                //    .Single(el => el.Alias == alias
                //    //&& el.ElementType.Code == fromTypeCode
                //    );


                var result = target.Convert();
                result.AfferentElements = db.GetAfferentElements(target.Id.Value, fromTypeCode, toTypeCode);
                result.EfferentElements = db.GetEfferentElements(target.Id.Value, fromTypeCode, toTypeCode);

                //var resultList = db.GetElements(sys.Id.Value, fromTypeCode, toTypeCode);

                //if (resultList.Any())
                //{
                //    foreach (var element in resultList)
                //    {
                //        element.AfferentElements = db.GetAfferentElements(element.ElementId, fromTypeCode, toTypeCode);
                //        element.EfferentElements = db.GetEfferentElements(element.ElementId, fromTypeCode, toTypeCode);
                //    }

                //    result.InboundElements = resultList
                //        .Where(x => x.Relationship.TypeCode == ElementRelationshipType.Inbound)
                //        .ToImmutableList();

                //    result.OutboundElements = resultList
                //       .Where(x => x.Relationship.TypeCode == ElementRelationshipType.Outbound)
                //       .ToImmutableList();
                //}

                return result;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtAlias.Text.Contains(" "))
            {
                MessageBox.Show(this, "Alias can't contain space.");
                return;
            }

            using (var db = _flywheelsDbContextFactory.CreateDbContext())
            {
                var target = db.Elements.Single(x => x.Id == _targetSystemContextVM.Id);
                getData(target);
                target.Update();
                db.SaveChanges();

                DialogResult = DialogResult.OK;
            }
        }
    }
}
