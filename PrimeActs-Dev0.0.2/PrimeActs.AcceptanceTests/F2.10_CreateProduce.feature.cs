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
    [NUnit.Framework.DescriptionAttribute("2.10_Create Produce")]
    public partial class _2_10_CreateProduceFeature
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
                "2.10_Create Produce",
                "In order to use the system with current stock\nthere is a need to add produce to t" +
                "he stock using the application.", ProgrammingLanguage.CSharp, ((string[]) (null)));
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
        [NUnit.Framework.DescriptionAttribute("Invalid attempt to add produce as an admin")]
        public virtual void InvalidAttemptToAddProduceAsAnAdmin()
        {
            var scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Invalid attempt to add produce as an admin",
                ((string[]) (null)));
#line 27
            this.ScenarioSetup(scenarioInfo);
#line 28
            testRunner.Given("the user has logged in as a member with SuperAdmin role", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Given ");
#line 29
            testRunner.And("invalid produce details are entered in the Create Produce form", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 30
            testRunner.When("the Create Produce button is pressed", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "When ");
#line 31
            testRunner.Then("a validation error message Please enter valid details should be displayed.",
                ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Navigate to Create Produce Screen")]
        public virtual void NavigateToCreateProduceScreen()
        {
            var scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Navigate to Create Produce Screen",
                ((string[]) (null)));
#line 13
            this.ScenarioSetup(scenarioInfo);
#line 14
            testRunner.Given("the user has logged in as a member with SuperAdmin role", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Given ");
#line 15
            testRunner.When("the Create Produce link is clicked", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)),
                "When ");
#line 16
            testRunner.Then("the Create Produce form should be displayed.", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Navigate to Manage Produce Screen")]
        [NUnit.Framework.CategoryAttribute("myProduce")]
        public virtual void NavigateToManageProduceScreen()
        {
            var scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Navigate to Manage Produce Screen", new string[]
            {
                "myProduce"
            });
#line 6
            this.ScenarioSetup(scenarioInfo);
#line 7
            testRunner.Given("the user has logged in as a member with SuperAdmin role", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Given ");
#line 8
            testRunner.When("the produce link is clicked", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)),
                "When ");
#line 9
            testRunner.Then("the produce screen should be displayed.", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Successfully create the new produce")]
        public virtual void SuccessfullyCreateTheNewProduce()
        {
            var scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Successfully create the new produce",
                ((string[]) (null)));
#line 20
            this.ScenarioSetup(scenarioInfo);
#line 21
            testRunner.Given("the user has logged in as a member with SuperAdmin role", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Given ");
#line 22
            testRunner.And("valid produce details are entered in the Create Produce form", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 23
            testRunner.When("the Create Produce button is pressed", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "When ");
#line 24
            testRunner.Then("the user is redirected to details page with produce creation confirmation",
                ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}

#pragma warning restore

#endregion