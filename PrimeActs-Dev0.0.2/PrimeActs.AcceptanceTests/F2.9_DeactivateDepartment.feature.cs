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
    [NUnit.Framework.DescriptionAttribute("F2")]
    public partial class F2Feature
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
            var featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "F2",
                "In order to avoid silly mistakes\nAs a math idiot\nI want to be told the sum of two" +
                " numbers", ProgrammingLanguage.CSharp, ((string[]) (null)));
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
        [NUnit.Framework.DescriptionAttribute("Deactivate division")]
        public virtual void DeactivateDivision()
        {
            var scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Deactivate division", ((string[]) (null)));
#line 6
            this.ScenarioSetup(scenarioInfo);
#line 7
            testRunner.Given("I am an authorised user and a member of the Admin role", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Given ");
#line 8
            testRunner.And("I have navigated to the \'Deactivate Department\' Screen", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 9
            testRunner.And("A list of active departments are displayed", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 10
            testRunner.When("I click on the checkbox next to the selected department \'TestDepartment\'",
                ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "When ");
#line 11
            testRunner.Then("the result should be the selected department is marked as deactivated in the data" +
                            "base", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "Then ");
#line 12
            testRunner.And("a Deactivation confirmation screen should be displayed\\.", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}

#pragma warning restore

#endregion