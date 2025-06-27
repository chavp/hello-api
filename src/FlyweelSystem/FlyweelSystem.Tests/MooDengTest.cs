

namespace FlyweelSystem.Tests
{
    using Flywheel.Models;

    public class MooDengTest : TestBase
    {
        [Fact]
        public void SeedContext()
        {
            var nspAlies = "MyZoo";
            var externalNspAlies = "TheWorld";
            using (var db = _sutDbContextFactory.CreateDbContext())
            using(var tran = db.Database.BeginTransaction()) 
            {
                // Context
                var moodengContext = saveElement(db, nspAlies, ElementType.Context, "MooDeng", "MooDeng System Context");

                // System
                var moodengSys = saveElement(db, nspAlies, ElementType.System, "MooDeng", "MooDeng System");
                var customerSys = saveElement(db, externalNspAlies, ElementType.System, "Customer", "Customer", partyTypeCode: PartyType.Person);

                // Container
                var moodengWeb = saveElement(db, nspAlies, ElementType.Container, "MooDengWeb", "MooDeng Web Apps", techn: "ASP.NET");
                var moodengApi = saveElement(db, nspAlies,  ElementType.Container, "MooDengApi", "MooDeng API", techn: "ASP.NET");
                var moodengDb = saveElement(db, nspAlies, ElementType.Container, "MooDengDb", "MooDeng Database", partyTypeCode: PartyType.Database, techn: "MSSQL Server");

                var moodengUiWeb = saveElement(db, nspAlies, ElementType.Component, "MooDengWeb", "MooDeng Web", techn: "Blazor ASP.NET");
                var moodengApiWeb = saveElement(db, nspAlies, ElementType.Component, "MooDengApi", "MooDeng API", techn: "ASP.NET Core");
                var moodengServices = saveElement(db, nspAlies, ElementType.Component, "MooDengServices", "MooDeng Domain Services", techn: "C#.NET");
                var moodengModels = saveElement(db, nspAlies, ElementType.Component, "MooDengModels", "MooDeng Domain Models", techn: "C#.NET");
                var moodengMappings = saveElement(db, nspAlies, ElementType.Component, "MooDengMappings", "MooDeng Model Mappings", techn: "C#.NET, EF");

                // System Context Include
                saveContextRelationship(db, moodengContext, ElementRelationshipType.Inbound, moodengSys, "MooDeng Web");
                saveContextRelationship(db, moodengContext, ElementRelationshipType.Outbound, customerSys, "Customer");

                // System Dependency
                saveContextRelationship(db, customerSys, ElementRelationshipType.TwoWay, moodengSys, "Visiting");

                // Container Include
                saveContextRelationship(db, moodengSys, ElementRelationshipType.Inbound, moodengWeb, "UI Web Apps");
                saveContextRelationship(db, moodengSys, ElementRelationshipType.Inbound, moodengApi, "Web API");
                saveContextRelationship(db, moodengSys, ElementRelationshipType.Inbound, moodengDb, "Database");

                // Container Dependency
                saveContextRelationship(db, moodengWeb, ElementRelationshipType.OneWay, moodengApi, "Call API");
                saveContextRelationship(db, moodengApi, ElementRelationshipType.OneWay, moodengDb, "DB Connection");

                saveContextRelationship(db, customerSys, ElementRelationshipType.TwoWay, moodengWeb, "Visiting");

                // Component Include
                saveContextRelationship(db, moodengWeb, ElementRelationshipType.Inbound, moodengUiWeb, "UI Web App");
                saveContextRelationship(db, moodengApi, ElementRelationshipType.Inbound, moodengApiWeb, "Web API Apps");
                saveContextRelationship(db, moodengApi, ElementRelationshipType.Inbound, moodengServices, "MooDeng Services Lib");
                saveContextRelationship(db, moodengApi, ElementRelationshipType.Inbound, moodengModels, "MooDeng Models Lib");
                saveContextRelationship(db, moodengApi, ElementRelationshipType.Inbound, moodengMappings, "MooDeng Mapping Lib");

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
            var mermaidSystemContext = getMermaidSystemContext("MooDeng", 3);

            var mermaidSystem = getMermaidSystemContainers("MooDeng", 3);

            var apiMermaidContainer = getMermaidContainerComponents("MooDengApi", 3);
            var uiMermaidContainer = getMermaidContainerComponents("MooDengWeb", 3);
        }
    }
}