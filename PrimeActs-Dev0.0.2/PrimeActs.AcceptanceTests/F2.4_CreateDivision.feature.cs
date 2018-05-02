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
    [NUnit.Framework.DescriptionAttribute("Division Setup")]
    public partial class DivisionSetupFeature
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
                "Division Setup", "As a superadmin user I need to be able to add a division.",
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
        [NUnit.Framework.DescriptionAttribute("Create a division")]
        [NUnit.Framework.CategoryAttribute("myDivision")]
        public virtual void CreateADivision()
        {
            var scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Create a division", new string[]
            {
                "myDivision"
            });
#line 5
            this.ScenarioSetup(scenarioInfo);
#line 6
            testRunner.Given("user is registered as SuperAdmin User", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Given ");
#line 7
            testRunner.When("the user goes to the create division screen", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "When ");
#line 8
            testRunner.Then("the result should be that the Create Division Form screen is displayed", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Then ");
#line 9
            testRunner.And("a default \'Select a company\' is selected in the Company dropdown.", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Create division not accessible other then super admin")]
        public virtual void CreateDivisionNotAccessibleOtherThenSuperAdmin()
        {
            var scenarioInfo =
                new TechTalk.SpecFlow.ScenarioInfo("Create division not accessible other then super admin",
                    ((string[]) (null)));
#line 31
            this.ScenarioSetup(scenarioInfo);
#line 32
            testRunner.Given("user is registered as Admin User", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)),
                "Given ");
#line 33
            testRunner.When("the user tries to access create division page", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "When ");
#line 34
            testRunner.Then("User should be shown redirected to not authorized page.", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Create division shoud return error if company is not selected")]
        [NUnit.Framework.CategoryAttribute("myDivisionCompany")]
        public virtual void CreateDivisionShoudReturnErrorIfCompanyIsNotSelected()
        {
            var scenarioInfo =
                new TechTalk.SpecFlow.ScenarioInfo("Create division shoud return error if company is not selected",
                    new string[]
                    {
                        "myDivisionCompany"
                    });
#line 19
            this.ScenarioSetup(scenarioInfo);
#line 20
            testRunner.Given("user is registered as SuperAdmin User", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Given ");
#line 21
            testRunner.And("the user hasn\'t selected company", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)),
                "And ");
#line 22
            testRunner.When("the user goes to the create division with missing company", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "When ");
#line 23
            testRunner.Then("User should be shown the error message \'\'Company\' should not be empty.\'",
                ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Create division shoud return error if division name is missing")]
        [NUnit.Framework.CategoryAttribute("myDivisionName")]
        public virtual void CreateDivisionShoudReturnErrorIfDivisionNameIsMissing()
        {
            var scenarioInfo =
                new TechTalk.SpecFlow.ScenarioInfo("Create division shoud return error if division name is missing",
                    new string[]
                    {
                        "myDivisionName"
                    });
#line 12
            this.ScenarioSetup(scenarioInfo);
#line 13
            testRunner.Given("user is registered as SuperAdmin User", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Given ");
#line 14
            testRunner.And("the user hasn\'t entered division name", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 15
            testRunner.When("the user goes to the create division with missing division name", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "When ");
#line 16
            testRunner.Then("User should be shown the error message \'\'Division Name\' should not be empty.\'",
                ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("On successfull creation of division, user redirected to index")]
        public virtual void OnSuccessfullCreationOfDivisionUserRedirectedToIndex()
        {
            var scenarioInfo =
                new TechTalk.SpecFlow.ScenarioInfo("On successfull creation of division, user redirected to index",
                    ((string[]) (null)));
#line 25
            this.ScenarioSetup(scenarioInfo);
#line 26
            testRunner.Given("user is registered as SuperAdmin User", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Given ");
#line 27
            testRunner.And("the user enteres valid division details.", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 28
            testRunner.When("the user goes to the create division", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "When ");
#line 29
            testRunner.Then("User should be shown redirected to division index", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}

#pragma warning restore

#endregion