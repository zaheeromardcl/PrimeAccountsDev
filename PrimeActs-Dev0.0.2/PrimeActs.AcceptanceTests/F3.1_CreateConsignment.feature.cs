#region Designer generated code

#region

using TechTalk.SpecFlow;

#endregion

#pragma warning disable

namespace PrimeActs.AcceptanceTests
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("F3.1_CreateConsignment")]
    public partial class F3_1_CreateConsignmentFeature
    {
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }

        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }

        private static TechTalk.SpecFlow.ITestRunner testRunner;

        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            var featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"),
                "F3.1_CreateConsignment",
                "In order to add consignments to the system, we need a create consignment view.",
                ProgrammingLanguage.CSharp, ((string[]) (null)));
            testRunner.OnFeatureStart(featureInfo);
        }

        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }

        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }

        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("1a User completes the Consignment form part 1 with valid entries.")]
        [NUnit.Framework.CategoryAttribute("myConsignments")]
        [NUnit.Framework.TestCaseAttribute("123456", "Department1", "input.txt", "This is a consignment", "SUP01",
            "Supplier 1 Name", "SupplierRef", "DCOD1", "01/01/2015", "OP", "UK", "Shipmentdetails", "0.00", "0.00",
            "Vehicle01", "this is a vehicle description", "PORTCODE1", "01/02/2015", "01/03/2015", null)]
        public virtual void _1AUserCompletesTheConsignmentFormPart1WithValidEntries_(
            string consignmentReference,
            string department,
            string file,
            string consignmentDescription,
            string supplierCode,
            string supplierName,
            string supplierReference,
            string despatchCode,
            string despatchDate,
            string consignmentType,
            string countryOfOrigin,
            string shipment,
            string commission,
            string handling,
            string vehicleShipment,
            string vehicleDetail,
            string port,
            string contractDate,
            string recievedDate,
            string[] exampleTags)
        {
            var @__tags = new string[]
            {
                "myConsignments"
            };
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            var scenarioInfo =
                new TechTalk.SpecFlow.ScenarioInfo("1a User completes the Consignment form part 1 with valid entries.",
                    @__tags);
#line 5
            this.ScenarioSetup(scenarioInfo);
#line 6
            testRunner.Given("that the user is an authenticated user with the appropriate role", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Given ");
#line 7
            testRunner.And("the system has generated a <ConsignmentReference>", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 8
            testRunner.And(string.Format("the user has selected a valid {0}", department), ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 9
            testRunner.And("the user has uploaded a valid <file>", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)),
                "And ");
#line 10
            testRunner.And(string.Format("the user has filled in the {0}", consignmentDescription), ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 11
            testRunner.And(string.Format("the user has selected the {0} field", supplierCode), ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 12
            testRunner.And(string.Format("the system has generated a {0}", supplierName), ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 13
            testRunner.And("the user has completed the <Supplier Ref> field", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 14
            testRunner.And(string.Format("the user has completed the {0} field", despatchCode), ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 15
            testRunner.And(string.Format("the user has completed the {0} field", despatchDate), ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 16
            testRunner.And(string.Format("the user has completed the {0} field", consignmentType), ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 17
            testRunner.And(string.Format("the user has completed the {0} field", countryOfOrigin), ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 18
            testRunner.And(string.Format("the user has completed the {0} field", shipment), ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 19
            testRunner.And("the user has completed the <Vehicle Name> field", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 20
            testRunner.And(string.Format("the user has completed the {0} field", vehicleDetail), ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 21
            testRunner.And(string.Format("the user has completed the {0} field", port), ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 22
            testRunner.And("the user has completed the <Contract Date> field", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 23
            testRunner.And("the user has completed the <Recieved Date> field", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 24
            testRunner.When("the user presses the Create Consignment button", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "When ");
#line 25
            testRunner.Then("all the form values should be saved to the database", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Then ");
#line 26
            testRunner.And("the details panel for the Consignmeent Created should be displayed.", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}

#pragma warning restore

#endregion