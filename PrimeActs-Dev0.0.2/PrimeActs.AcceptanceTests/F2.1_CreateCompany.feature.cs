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
    [NUnit.Framework.DescriptionAttribute("F2.1_CreateCompany Setup")]
    public partial class F2_1_CreateCompanySetupFeature
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
                "F2.1_CreateCompany Setup", "As a superadmin user I need to be able to add a company.",
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
        [NUnit.Framework.DescriptionAttribute("Create a company")]
        [NUnit.Framework.CategoryAttribute("mytag")]
        public virtual void CreateACompany()
        {
            var scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Create a company", new string[]
            {
                "mytag"
            });
#line 5
            this.ScenarioSetup(scenarioInfo);
#line 6
            testRunner.Given("I am in the role \'SuperAdminUser\'", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Given ");
#line 7
            testRunner.And("I have clicked on the menu option \'Company Settings\'", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 8
            testRunner.When("I press the \'Create Company\' button", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "When ");
#line 9
            testRunner.Then("the result should be that the \'Create Company Form\" should be displayed.",
                ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Create a company - select a parent company.")]
        public virtual void CreateACompany_SelectAParentCompany_()
        {
            var scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Create a company - select a parent company.",
                ((string[]) (null)));
#line 11
            this.ScenarioSetup(scenarioInfo);
#line 12
            testRunner.Given("I am in the role \'SuperAdminUser\'", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Given ");
#line 13
            testRunner.And("I am on the \'Create Company\' form", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)),
                "And ");
#line 14
            testRunner.When("I check the \'Has Parent Company\' box", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "When ");
#line 15
            testRunner.Then("a list of existing companies should be displayed in a drop down list.", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Create a company - validation with invalid details.")]
        public virtual void CreateACompany_ValidationWithInvalidDetails_()
        {
            var scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo(
                "Create a company - validation with invalid details.", ((string[]) (null)));
#line 23
            this.ScenarioSetup(scenarioInfo);
#line 24
            testRunner.Given("I am in the role \'SuperAdminUser\'", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Given ");
#line 25
            testRunner.And("I am on the \'Create Company\' form", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)),
                "And ");
#line 26
            testRunner.When("any of the fields are entered with invalid values or are incomplete", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "When ");
#line 27
            testRunner.Then("the \'Red Cross icon\' should be displayed and the appropriate field highlighted in" +
                            " red until the user enters valid data.", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Create a company - validation with valid details.")]
        public virtual void CreateACompany_ValidationWithValidDetails_()
        {
            var scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Create a company - validation with valid details.",
                ((string[]) (null)));
#line 17
            this.ScenarioSetup(scenarioInfo);
#line 18
            testRunner.Given("I am in the role \'SuperAdminUser\'", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Given ");
#line 19
            testRunner.And("I am on the \'Create Company\' form", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)),
                "And ");
#line 20
            testRunner.When("all fields are entered with valid values", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "When ");
#line 21
            testRunner.Then("the \'Green Tick icon\' should be displayed.", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}

#pragma warning restore

#endregion