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
    [NUnit.Framework.DescriptionAttribute("F2.5_AmendDivision")]
    public partial class F2_5_AmendDivisionFeature
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
                "F2.5_AmendDivision",
                "In order to amend division details, as a member of the admin role, I should be ab" +
                "le \nto amend division details.", ProgrammingLanguage.CSharp, ((string[]) (null)));
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
        [NUnit.Framework.DescriptionAttribute("Amend division form")]
        public virtual void AmendDivisionForm()
        {
            var scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Amend division form", ((string[]) (null)));
#line 12
            this.ScenarioSetup(scenarioInfo);
#line 13
            testRunner.Given("that I am an authorised user and a member of the Admin role", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Given ");
#line 14
            testRunner.And("I have clicked on a division name from the list of divisions on the \'Display Divi" +
                           "sions\' screen", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 15
            testRunner.And("I have edited the division details on the \'Amend Division Form\' screen", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 16
            testRunner.When("I press the \'Submit Division Details\' button", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "When ");
#line 17
            testRunner.Then("the result should be that the details are updated in the database", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Then ");
#line 18
            testRunner.And("the \'Amended Division Details\' should be displayed on the \'Confirm Amended Divisi" +
                           "on details\' screen.", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Select division to amend")]
        [NUnit.Framework.CategoryAttribute("mytag")]
        public virtual void SelectDivisionToAmend()
        {
            var scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Select division to amend", new string[]
            {
                "mytag"
            });
#line 7
            this.ScenarioSetup(scenarioInfo);
#line 8
            testRunner.Given("I am an authorised user and a member of the Admin role", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Given ");
#line 9
            testRunner.When("I select a division from the list of divisons", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "When ");
#line 10
            testRunner.Then("the result should be an \'Amend Division form\' screen is displayed for that compan" +
                            "y.", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Validation for amend division form")]
        public virtual void ValidationForAmendDivisionForm()
        {
            var scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Validation for amend division form",
                ((string[]) (null)));
#line 20
            this.ScenarioSetup(scenarioInfo);
#line 21
            testRunner.Given("that I am an authorised user and a member of the Admin role.", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Given ");
#line 22
            testRunner.And("I have clicked on a division name from the list of division on the \'Display divis" +
                           "ions\' screen", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 23
            testRunner.And("I have edited the division details with invalid entries on the \'Amend division Fo" +
                           "rm\' screen", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 24
            testRunner.When("I press on the  \'Submit division Details\' button", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "When ");
#line 25
            testRunner.Then("the result should be that a validation error message \'The entry is invalid, pleas" +
                            "e enter another\'.", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}

#pragma warning restore

#endregion