using FlyweelSystem.Tests.Mappings;
using FlyweelSystem.Tests.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyweelSystem.Tests
{
    public class C4Samples : TestBase
    {
        [Fact]
        public void SeedContext()
        {
            var nspAls = "C4Sample";
            var externalSystem = "ExternalContextSystem";
            using (var db = _sutDbContextFactory.CreateDbContext())
            using (var tran = db.Database.BeginTransaction())
            {
                var sysCtx = saveElement(db, nspAls, ElementType.Context, "InternetBankingContext", "Internet Banking System"
                    , descr: "");

                var personalBankingCustomer = saveElement(db, externalSystem, ElementType.System, "PersonalBankingCustomer"
                    , "Personal Banking Customer"
                    , descr: "A customer of the bank, with personal bank accounts."
                    , partyTypeCode: PartyType.Person);

                var internetBankingSystem = saveElement(db, nspAls, ElementType.System, "InternetBankingSystem"
                    , "Internet Banking System"
                    , descr: "Allows customers to view information about their bank accounts, and make payments."
                    );

                var mainframeBankingSystem = saveElement(db, externalSystem, ElementType.System, "MainframeBankingSystem"
                    , "Mainframe Banking System"
                    , descr: "Stores all of the core banking information about customers, accounts, transactions, etc."
                    );

                var emailSystem = saveElement(db, externalSystem, ElementType.System, "EmailSystem"
                    , "E-mail System"
                    , descr: "The internal Microsoft Exchange e-mail system."
                    );

                // Include
                saveContextRelationship(db, sysCtx, ElementRelationshipType.Outbound, personalBankingCustomer, $"Include {personalBankingCustomer.Alias}");
                saveContextRelationship(db, sysCtx, ElementRelationshipType.Inbound, mainframeBankingSystem, $"Include {mainframeBankingSystem.Alias}");
                saveContextRelationship(db, sysCtx, ElementRelationshipType.Inbound, emailSystem, $"Include {emailSystem.Alias}");
                saveContextRelationship(db, sysCtx, ElementRelationshipType.Inbound, internetBankingSystem, $"Include {internetBankingSystem.Alias}");

                // Rel System Context
                saveContextRelationship(db, personalBankingCustomer, ElementRelationshipType.OneWay, internetBankingSystem, $"Views account balances, and makes payments using");
                saveContextRelationship(db, internetBankingSystem, ElementRelationshipType.OneWay, mainframeBankingSystem, $"Gets account information from, and makes payments using");
                saveContextRelationship(db, internetBankingSystem, ElementRelationshipType.OneWay, emailSystem, $"Sends e-mail using");
                saveContextRelationship(db, emailSystem, ElementRelationshipType.OneWay, personalBankingCustomer, $"Sends e-mails to");

                var internetBankingSystem_WebApplication = saveElement(db, nspAls, ElementType.Container, $"{internetBankingSystem.Alias}.WebApplication"
                    , "Web Application"
                    , techn: "Java and Spring MVC"
                    , descr: "Delivers the static content and the Internet banking single page application."
                    );
                var internetBankingSystem_APIApplication = saveElement(db, nspAls, ElementType.Container, $"{internetBankingSystem.Alias}.APIApplication"
                    , "API Application"
                    , techn: "Java and Spring MVC"
                    , descr: "Provides Internet banking functionality via a JSON/HTTPS API."
                    );
                var internetBankingSystem_Database = saveElement(db, nspAls, ElementType.Container, $"{internetBankingSystem.Alias}.Database"
                    , "Database"
                    , techn: "Oracle Database Schema"
                    , descr: "Stores user registration information, hashed authentication credentials, access logs, etc."
                    , partyTypeCode: PartyType.Database
                    );
                var internetBankingSystem_SinglePageApplication = saveElement(db, nspAls, ElementType.Container, $"{internetBankingSystem.Alias}.SinglePageApplication"
                    , "Single-Page Application"
                    , techn: "JavaScript and Angular"
                    , descr: "Provides all of the Internet banking functionality to customers via their web browser."
                    );
                var internetBankingSystem_MobileApp = saveElement(db, nspAls, ElementType.Container, $"{internetBankingSystem.Alias}.MobileApp"
                    , "Mobile App"
                    , techn: "Xamarin"
                    , descr: "Provides a limited subset of the Internet banking functionality to customers via their mobile device."
                    );

                saveContextRelationship(db, internetBankingSystem, ElementRelationshipType.Inbound, internetBankingSystem_WebApplication, $"Include {internetBankingSystem_WebApplication.Alias}");
                saveContextRelationship(db, internetBankingSystem, ElementRelationshipType.Inbound, internetBankingSystem_APIApplication, $"Include {internetBankingSystem_APIApplication.Alias}");
                saveContextRelationship(db, internetBankingSystem, ElementRelationshipType.Inbound, internetBankingSystem_Database, $"Include {internetBankingSystem_Database.Alias}");
                saveContextRelationship(db, internetBankingSystem, ElementRelationshipType.Inbound, internetBankingSystem_SinglePageApplication, $"Include {internetBankingSystem_SinglePageApplication.Alias}");
                saveContextRelationship(db, internetBankingSystem, ElementRelationshipType.Inbound, internetBankingSystem_MobileApp, $"Include {internetBankingSystem_MobileApp.Alias}");

                saveContextRelationship(db, personalBankingCustomer, ElementRelationshipType.OneWay, internetBankingSystem_WebApplication, "Visits bigbank.com/ib using", techn: "HTTPS");
                saveContextRelationship(db, personalBankingCustomer, ElementRelationshipType.OneWay, internetBankingSystem_SinglePageApplication, "Views account balances, and makes payments using");
                saveContextRelationship(db, personalBankingCustomer, ElementRelationshipType.OneWay, internetBankingSystem_MobileApp, "Views account balances, and makes payments using");

                saveContextRelationship(db, internetBankingSystem_WebApplication, ElementRelationshipType.OneWay, internetBankingSystem_SinglePageApplication, "Delivers to the customer's web browser");
                saveContextRelationship(db, internetBankingSystem_SinglePageApplication, ElementRelationshipType.OneWay, internetBankingSystem_APIApplication, "Makes API calls to", techn: "JSON/HTTPS");
                saveContextRelationship(db, internetBankingSystem_MobileApp, ElementRelationshipType.OneWay, internetBankingSystem_APIApplication, "Makes API calls to", techn: "JSON/HTTPS");
                saveContextRelationship(db, internetBankingSystem_APIApplication, ElementRelationshipType.OneWay, internetBankingSystem_Database, "Reads from and writes to", techn: "SQL/TCP");
                saveContextRelationship(db, internetBankingSystem_APIApplication, ElementRelationshipType.OneWay, mainframeBankingSystem, "Makes API calls to", techn: "XML/HTTPS");
                saveContextRelationship(db, internetBankingSystem_APIApplication, ElementRelationshipType.OneWay, emailSystem, "Sends e-mail using");

                var internetBankingSystem_APIApplication_SignInController = saveElement(db, nspAls, ElementType.Component, $"{internetBankingSystem_APIApplication.Alias}.SignInController", "Sign In Controller", techn: "Spring MVC Rest Controller", descr: "Allows users to sign in to the Internet Banking System.");
                var internetBankingSystem_APIApplication_AccountsSummaryController = saveElement(db, nspAls, ElementType.Component, $"{internetBankingSystem_APIApplication.Alias}.AccountsSummaryController", "Accounts Summary Controller", techn: "Spring MVC Rest Controller", descr: "Provides customers with a summary of their bank accounts.");
                var internetBankingSystem_APIApplication_ResetPasswordController = saveElement(db, nspAls, ElementType.Component, $"{internetBankingSystem_APIApplication.Alias}.ResetPasswordController", "Reset Password Controller", techn: "Spring MVC Rest Controller", descr: "Allows users to reset their passwords with a single use URL.");
                var internetBankingSystem_APIApplication_SecurityComponent = saveElement(db, nspAls, ElementType.Component, $"{internetBankingSystem_APIApplication.Alias}.SecurityComponent", "Security Component", techn: "Spring Bean", descr: "Provides functionality related to signing in, changing passwords, etc.");
                var internetBankingSystem_APIApplication_MainframeBankingSystemFacade = saveElement(db, nspAls, ElementType.Component, $"{internetBankingSystem_APIApplication.Alias}.MainframeBankingSystemFacade", "Mainframe Banking System Facade", techn: "Spring Bean", descr: "A facade onto the mainframe banking system.");
                var internetBankingSystem_APIApplication_EmailComponent = saveElement(db, nspAls, ElementType.Component, $"{internetBankingSystem_APIApplication.Alias}.EmailComponent", "E-mail Component", techn: "Spring Bean", descr: "Sends e-mails to users.");

                saveContextRelationship(db, internetBankingSystem_APIApplication, ElementRelationshipType.Inbound, internetBankingSystem_APIApplication_SignInController, $"Include {internetBankingSystem_APIApplication_SignInController.Alias}");
                saveContextRelationship(db, internetBankingSystem_APIApplication, ElementRelationshipType.Inbound, internetBankingSystem_APIApplication_AccountsSummaryController, $"Include {internetBankingSystem_APIApplication_AccountsSummaryController.Alias}");
                saveContextRelationship(db, internetBankingSystem_APIApplication, ElementRelationshipType.Inbound, internetBankingSystem_APIApplication_ResetPasswordController, $"Include {internetBankingSystem_APIApplication_ResetPasswordController.Alias}");
                saveContextRelationship(db, internetBankingSystem_APIApplication, ElementRelationshipType.Inbound, internetBankingSystem_APIApplication_SecurityComponent, $"Include {internetBankingSystem_APIApplication_SecurityComponent.Alias}");
                saveContextRelationship(db, internetBankingSystem_APIApplication, ElementRelationshipType.Inbound, internetBankingSystem_APIApplication_MainframeBankingSystemFacade, $"Include {internetBankingSystem_APIApplication_MainframeBankingSystemFacade.Alias}");
                saveContextRelationship(db, internetBankingSystem_APIApplication, ElementRelationshipType.Inbound, internetBankingSystem_APIApplication_EmailComponent, $"Include {internetBankingSystem_APIApplication_EmailComponent.Alias}");

                saveContextRelationship(db, internetBankingSystem_SinglePageApplication, ElementRelationshipType.OneWay, internetBankingSystem_APIApplication_SignInController, "Makes API calls to", techn: "JSON/HTTPS");
                saveContextRelationship(db, internetBankingSystem_SinglePageApplication, ElementRelationshipType.OneWay, internetBankingSystem_APIApplication_AccountsSummaryController, "Makes API calls to", techn: "JSON/HTTPS");
                saveContextRelationship(db, internetBankingSystem_SinglePageApplication, ElementRelationshipType.OneWay, internetBankingSystem_APIApplication_ResetPasswordController, "Makes API calls to", techn: "JSON/HTTPS");
                saveContextRelationship(db, internetBankingSystem_MobileApp, ElementRelationshipType.OneWay, internetBankingSystem_APIApplication_SignInController, "Makes API calls to", techn: "JSON/HTTPS");
                saveContextRelationship(db, internetBankingSystem_MobileApp, ElementRelationshipType.OneWay, internetBankingSystem_APIApplication_AccountsSummaryController, "Makes API calls to", techn: "JSON/HTTPS");
                saveContextRelationship(db, internetBankingSystem_MobileApp, ElementRelationshipType.OneWay, internetBankingSystem_APIApplication_ResetPasswordController, "Makes API calls to", techn: "JSON/HTTPS");
                saveContextRelationship(db, internetBankingSystem_APIApplication_SignInController, ElementRelationshipType.OneWay, internetBankingSystem_APIApplication_SecurityComponent, "Uses");
                saveContextRelationship(db, internetBankingSystem_APIApplication_AccountsSummaryController, ElementRelationshipType.OneWay, internetBankingSystem_APIApplication_MainframeBankingSystemFacade, "Uses");
                saveContextRelationship(db, internetBankingSystem_APIApplication_ResetPasswordController, ElementRelationshipType.OneWay, internetBankingSystem_APIApplication_SecurityComponent, "Uses");
                saveContextRelationship(db, internetBankingSystem_APIApplication_ResetPasswordController, ElementRelationshipType.OneWay, internetBankingSystem_APIApplication_EmailComponent, "Uses");
                saveContextRelationship(db, internetBankingSystem_APIApplication_SecurityComponent, ElementRelationshipType.OneWay, internetBankingSystem_Database, "Reads from and writes to", techn: "SQL/TCP");
                saveContextRelationship(db, internetBankingSystem_APIApplication_MainframeBankingSystemFacade, ElementRelationshipType.OneWay, mainframeBankingSystem, "Makes API calls to", techn: "XML/HTTPS");
                saveContextRelationship(db, internetBankingSystem_APIApplication_EmailComponent, ElementRelationshipType.OneWay, emailSystem, "Sends e-mail using");

                db.SaveChanges();
                tran.Commit();
            }
        }

        [Fact]
        public void TestMermaid()
        {
            var mermaidSystemContext = getMermaidSystemContext("InternetBankingContext", 3);

            var mermaidSystem = getMermaidSystemContainers("InternetBankingSystem", 3);

            var mermaidContainer = getMermaidContainerComponents("InternetBankingSystem.APIApplication", 3);
        }
    }
}
