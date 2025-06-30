using Flywheel.Mappings;
using Flywheel.Mappings.Values;
using Flywheel.Models;
using Flywheel.Win.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text;

using Flywheel.Mappings.Queries;
using System.Collections.Immutable;
using System.Collections.Concurrent;

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
            txtElementType.Text = _targetSystemContextVM.ElementTypeCode;

            using (var db = _flywheelsDbContextFactory.CreateDbContext())
            {
                var partyTypeVMs = new List<PartyTypeVM>();
                partyTypeVMs.Add(new PartyTypeVM
                {
                    Code = "-",
                });
                var partyTypes = db.PartyTypes.Select(x => new PartyTypeVM
                {
                    Id = x.Id,
                    Code = x.Code,
                }).ToImmutableList();
                partyTypeVMs.AddRange(partyTypes);
                cmbPartyType.DataSource = partyTypeVMs.ToImmutableList();
                cmbPartyType.SelectedIndex = 0;
            }

            if (_targetSystemContextVM.PartyTypeId.HasValue)
            {
                cmbPartyType.SelectedValue = _targetSystemContextVM.PartyTypeId;
            }
        }
        private void getData(Models.Element elem)
        {
            elem.Alias = txtAlias.Text;
            elem.Label = txtLabel.Text;
            elem.Description = rtxDescr.Text;
            elem.Technical = txtTechn.Text;

            if (cmbPartyType.SelectedItem is PartyTypeVM selected)
            {
                elem.PartyTypeId = selected.Id;
            }
            else
            {
                elem.PartyTypeId = null;
            }

            _targetSystemContextVM.Alias = elem.Alias;
            _targetSystemContextVM.Label = elem.Label;
            _targetSystemContextVM.Description = elem.Description;
            _targetSystemContextVM.Technical = elem.Technical;
            _targetSystemContextVM.PartyTypeId = elem.PartyTypeId;
        }

        ElementValue? _elementValue = null;
        private void loadRels()
        {
            _elementValue = null;
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
                    fromElementTypeCode = ElementType.Container;
                    toElementTypeCode = ElementType.Component;
                }
                else if (target.ElementType.Code == ElementType.Component)
                {
                    fromElementTypeCode = ElementType.Container;
                    toElementTypeCode = ElementType.Component;
                }

                _elementValue = getElement(target, fromElementTypeCode, toElementTypeCode);

                dgvAfferents.DataSource = _elementValue.AfferentElements;
                dgvEfferents.DataSource = _elementValue.EfferentElements;
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

                var resultList = db.GetElements(target.Id.Value, fromTypeCode, toTypeCode);

                if (resultList.Any())
                {
                    foreach (var element in resultList)
                    {
                        element.AfferentElements = db.GetAfferentElements(element.ElementId, fromTypeCode, toTypeCode);
                        element.EfferentElements = db.GetEfferentElements(element.ElementId, fromTypeCode, toTypeCode);
                    }

                    result.InboundElements = resultList
                        .Where(x => x.Relationship.TypeCode == ElementRelationshipType.Inbound)
                        .ToImmutableList();

                    result.OutboundElements = resultList
                       .Where(x => x.Relationship.TypeCode == ElementRelationshipType.Outbound)
                       .ToImmutableList();
                }

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

        private void btnMermaid_Click(object sender, EventArgs e)
        {
            rtxDiagrame.Clear();
            if (_elementValue != null)
            {
                if(_elementValue.ElementTypeCode == ElementType.System)
                {
                    //var mermaid = buildC4Mermaid("C4Context", "Enterprise_Boundary", 3, _elementValue);
                    var mermaid = buildC4Mermaid("C4Container", "System_Boundary", 3, _elementValue);
                    rtxDiagrame.Text = mermaid;
                }
                else if (_elementValue.ElementTypeCode == ElementType.Container)
                {
                    var mermaid = buildC4Mermaid("C4Component", "Container_Boundary", 3, _elementValue);
                    rtxDiagrame.Text = mermaid;
                }
            }
        }

        private string buildC4Mermaid(string c4Name, string boundaryName, int shapeInRow, ElementValue sys)
        {
            var c4Mermaid = new StringBuilder();
            c4Mermaid.AppendLine(c4Name);
            c4Mermaid.AppendLine($"\ttitle {sys.Label}");

            // add comps
            c4Mermaid.AppendLine(comps(1, boundaryName, sys));

            // add Rels
            c4Mermaid.AppendLine(rels(1, sys));

            c4Mermaid.AppendLine($"\tUpdateLayoutConfig($c4ShapeInRow=\"{shapeInRow}\", $c4BoundaryInRow=\"1\")");

            return c4Mermaid.ToString();
        }
        private string comps(uint repeatTabs,
    string boundaryName,
    ElementValue sys)
        {
            var tabs = "\t".Repeat(repeatTabs);
            var resp = new StringBuilder();

            // Out Boundary
            foreach (var eleOut in sys.OutboundElements)
            {
                resp.AppendLine(getMermaidElement(tabs, sys.NamespaceId, eleOut));
            }

            var dicOutElems = new ConcurrentDictionary<Guid, ElementValue>();
            foreach (var e in sys.InboundElements)
            {
                foreach (var eIn in e.AfferentElements)
                {
                    if (dicOutElems.TryAdd(eIn.ElementId, eIn)
                           && !sys.InboundElements.Any(x => x.ElementId == eIn.ElementId)
                           && !sys.OutboundElements.Any(x => x.ElementId == eIn.ElementId))
                    {
                        resp.AppendLine(getMermaidElement(tabs, sys.NamespaceId, eIn));
                    }
                }

                foreach (var eleOut in e.EfferentElements)
                {
                    if (dicOutElems.TryAdd(eleOut.ElementId, eleOut)
                        && !sys.InboundElements.Any(x => x.ElementId == eleOut.ElementId)
                        && !sys.OutboundElements.Any(x => x.ElementId == eleOut.ElementId))
                    {
                        resp.AppendLine(getMermaidElement(tabs, sys.NamespaceId, eleOut));
                    }
                }

            }

            // In Boundary
            resp.AppendLine($"{tabs}{boundaryName}(b0, \"{sys.Alias}\") "
                + Environment.NewLine + tabs + "{ ");
            foreach (var e in sys.InboundElements)
            {
                resp.AppendLine(getMermaidElement($"{tabs}\t", sys.NamespaceId, e));
            }
            resp.AppendLine(tabs + "}");

            return resp.ToString();
        }
        private string rels(uint repeatTabs, ElementValue sys)
        {
            var tabs = "\t".Repeat(repeatTabs);
            var resp = new StringBuilder();

            var allElements = sys.InboundElements
                                .Concat(sys.OutboundElements);


            foreach (var e in allElements)
            {
                // add Rels Out Boundary
                foreach (var eleOut in e.EfferentElements)
                {
                    resp.AppendLine(getMermaidRel(tabs, e, eleOut.Relationship, eleOut));
                }

                // add Rels In Boundary
                foreach (var inOut in e.AfferentElements)
                {
                    if (!allElements.Any(x => x.ElementId == inOut.ElementId))
                    {
                        resp.AppendLine(getMermaidRel(tabs, inOut, inOut.Relationship, e));
                    }
                }
            }
            return resp.ToString();
        }

        protected string getMermaidRel(string tabs,
            ElementValue fromElement, RelationshipValue re, ElementValue toElement)
        {
            return re.TypeCode switch
            {
                ElementRelationshipType.TwoWay => $"{tabs}BiRel({fromElement.Alias}, {toElement.Alias}, \"{re.Label}\", \"{re.Techn}\")",
                ElementRelationshipType.OneWay => $"{tabs}Rel({fromElement.Alias}, {toElement.Alias}, \"{re.Label}\", \"{re.Techn}\")",
                _ => throw new Exception("Invalid Mermaid Object")
            };
        }

        protected string getMermaidElement(string tabs, Guid namespaceId, ElementValue ctx)
        {
            var inNamespace = (namespaceId == ctx.NamespaceId);
            var ext = inNamespace ? "" : "_Ext";
            var system = "System";
            var container = "Container";
            if (ctx.PartyTypeCode == PartyType.Person)
            {
                system = "Person";
            }
            if (ctx.PartyTypeCode == PartyType.Database)
            {
                system = "SystemDb";
                container = "ContainerDb";
            }

            return ctx.ElementTypeCode switch
            {
                ElementType.System => $"{tabs}{system}{ext}({ctx.Alias}, \"{ctx.Label}\", \"{ctx.Description}\")",
                ElementType.Container => $"{tabs}{container}{ext}({ctx.Alias}, \"{ctx.Label}\", \"{ctx.Techn}\", \"{ctx.Description}\")",
                ElementType.Component => $"{tabs}Component{ext}({ctx.Alias}, \"{ctx.Label}\", \"{ctx.Techn}\", \"{ctx.Description}\")",
                _ => throw new Exception("Invalid Mermaid Object")
            };
        }
    }
}
