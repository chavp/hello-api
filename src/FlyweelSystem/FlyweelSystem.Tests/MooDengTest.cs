using FlyweelSystem.Tests.Mappings;
using FlyweelSystem.Tests.Models;
using FlyweelSystem.Tests.Values;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System.Collections.Immutable;
using System.Text;

namespace FlyweelSystem.Tests
{
    public class MooDengTest : TestBase
    {
        [Fact]
        public void SeedContext()
        {
            var boundaryName = "MyZoo";
            var externalBound = "TheWorld";
            using (var db = _sutDbContextFactory.CreateDbContext())
            using(var tran = db.Database.BeginTransaction()) 
            {
                // Context
                var moodengContext = saveElement(db, boundaryName, Models.ElementType.Context, "MooDeng", "MooDeng System Context");

                // System
                var moodengSys = saveElement(db, boundaryName, Models.ElementType.System, "MooDeng", "MooDeng System");
                var customerSys = saveElement(db, externalBound, Models.ElementType.System, "Customer", "Customer", partyTypeCode: PartyType.Person);

                // Container
                var moodengWeb = saveElement(db, boundaryName, Models.ElementType.Container, "MooDengWeb", "MooDeng Web Apps");
                var moodengApi = saveElement(db, boundaryName, Models.ElementType.Container, "MooDengApi", "MooDeng API");
                var moodengDb = saveElement(db, boundaryName, Models.ElementType.Container, "MooDengDb", "MooDeng Database", partyTypeCode: PartyType.Database);

                var moodengUiWeb = saveElement(db, boundaryName, Models.ElementType.Component, "MooDengWeb", "MooDeng Web");
                var moodengApiWeb = saveElement(db, boundaryName, Models.ElementType.Component, "MooDengApi", "MooDeng API");
                var moodengServices = saveElement(db, boundaryName, Models.ElementType.Component, "MooDengServices", "MooDeng Domain Services");
                var moodengModels = saveElement(db, boundaryName, Models.ElementType.Component, "MooDengModels", "MooDeng Domain Models");
                var moodengMappings = saveElement(db, boundaryName, Models.ElementType.Component, "MooDengMappings", "MooDeng Model Mappings");

                // System Context Include
                saveContextRelationship(db, moodengContext, ElementRelationshipType.Include, moodengSys, "MooDeng Web");
                saveContextRelationship(db, moodengContext, ElementRelationshipType.Include, customerSys, "Customer");

                // System Dependency
                saveContextRelationship(db, customerSys, ElementRelationshipType.TwoWay, moodengSys, "Visiting");

                // Container Include
                saveContextRelationship(db, moodengSys, ElementRelationshipType.Include, moodengWeb, "UI Web Apps");
                saveContextRelationship(db, moodengSys, ElementRelationshipType.Include, moodengApi, "Web API");
                saveContextRelationship(db, moodengSys, ElementRelationshipType.Include, moodengDb, "Database");

                // Container Dependency
                saveContextRelationship(db, moodengWeb, ElementRelationshipType.OneWay, moodengApi, "Call API");
                saveContextRelationship(db, moodengApi, ElementRelationshipType.OneWay, moodengDb, "DB Connection");

                saveContextRelationship(db, customerSys, ElementRelationshipType.TwoWay, moodengWeb, "Visiting");

                // Component Include
                saveContextRelationship(db, moodengWeb, ElementRelationshipType.Include, moodengUiWeb, "UI Web App");
                saveContextRelationship(db, moodengApi, ElementRelationshipType.Include, moodengApiWeb, "Web API Apps");
                saveContextRelationship(db, moodengApi, ElementRelationshipType.Include, moodengServices, "MooDeng Services Lib");
                saveContextRelationship(db, moodengApi, ElementRelationshipType.Include, moodengModels, "MooDeng Models Lib");
                saveContextRelationship(db, moodengApi, ElementRelationshipType.Include, moodengMappings, "MooDeng Mapping Lib");

                // Component Dependency
                saveContextRelationship(db, moodengUiWeb, ElementRelationshipType.OneWay, moodengApiWeb, "REST");
                saveContextRelationship(db, moodengApiWeb, ElementRelationshipType.OneWay, moodengServices, "Reference");
                saveContextRelationship(db, moodengServices, ElementRelationshipType.OneWay, moodengModels, "Reference");
                saveContextRelationship(db, moodengServices, ElementRelationshipType.OneWay, moodengMappings, "Reference");

                saveContextRelationship(db, moodengMappings, ElementRelationshipType.OneWay, moodengDb, "Persist");

                db.SaveChanges();
                tran.Commit();
            }
        }

        [Fact]
        public void TestContext()
        {
            var mermaidSystemContext = getMermaidContext("MooDeng");

            var mermaidSystem = getMermaidSystem("MooDeng");

            var apiMermaidContainer = getMermaidContainer("MooDengApi");
            var uiMermaidContainer = getMermaidContainer("MooDengWeb");
        }
    }
}