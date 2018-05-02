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
    [NUnit.Framework.DescriptionAttribute("F1.1")]
    public partial class F1_1Feature
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
            var featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "F1.1",
                "In order to avoid authenticate the user\r\nAs a user with an associated role\r\nI wan" +
                "t to be told the username and password", ProgrammingLanguage.CSharp, ((string[]) (null)));
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
        [NUnit.Framework.DescriptionAttribute("1.1a User loads the login page")]
        [NUnit.Framework.CategoryAttribute("mytag")]
        public virtual void _1_1AUserLoadsTheLoginPage()
        {
            var scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("1.1a User loads the login page", new string[]
            {
                "mytag"
            });
#line 7
            this.ScenarioSetup(scenarioInfo);
#line 8
            testRunner.Given("I browse to the the Login page", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)),
                "Given ");
#line 9
            testRunner.When("I load the page", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "When ");
#line 10
            testRunner.Then("the focus should be on the username field.", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("1.1b User logs in with valid credentials.")]
        public virtual void _1_1BUserLogsInWithValidCredentials_()
        {
            var scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("1.1b User logs in with valid credentials.",
                ((string[]) (null)));
#line 12
            this.ScenarioSetup(scenarioInfo);
#line 13
            testRunner.Given("I complete the Login form with valid credentials", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Given ");
#line 14
            testRunner.When("I press on the Login button", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)),
                "When ");
#line 15
            testRunner.Then("I will be logged in as valid user", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)),
                "Then ");
#line 16
            testRunner.And("my account will be assigned to an role", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("1.1c User enters invalid credentials")]
        [NUnit.Framework.TestCaseAttribute("Aruna.invalidC", "roses2212C", null)]
        [NUnit.Framework.TestCaseAttribute("Aruna.invalidB", "Roses2212B", null)]
        [NUnit.Framework.TestCaseAttribute("Aruna.invalid", "Roses2212A", null)]
        public virtual void _1_1CUserEntersInvalidCredentials(string username, string password, string[] exampleTags)
        {
            var scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("1.1c User enters invalid credentials", exampleTags);
#line 25
            this.ScenarioSetup(scenarioInfo);
#line 26
            testRunner.Given(string.Format("I complete the Login form with {0} and {1}", username, password),
                ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "Given ");
#line 27
            testRunner.When("I move focus off either field", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)),
                "When ");
#line 28
            testRunner.Then("the fields should be highlighted red", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Then ");
#line 29
            testRunner.And("I will be shown an error message \'Please enter valid credentials\'", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 30
            testRunner.And("a record of the login will be stored in the database.", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("1.1d User enters valid credentials that are not in the database.")]
        [NUnit.Framework.TestCaseAttribute("Aruna.notpresentA", "roses2212C", null)]
        [NUnit.Framework.TestCaseAttribute("Aruna.notpresentB", "Roses2212B", null)]
        [NUnit.Framework.TestCaseAttribute("Aruna.notpresentC", "Roses2212A", null)]
        public virtual void _1_1DUserEntersValidCredentialsThatAreNotInTheDatabase_(string username, string password,
            string[] exampleTags)
        {
            var scenarioInfo =
                new TechTalk.SpecFlow.ScenarioInfo("1.1d User enters valid credentials that are not in the database.",
                    exampleTags);
#line 38
            this.ScenarioSetup(scenarioInfo);
#line 39
            testRunner.Given("I am on the Login page", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "Given ");
#line 40
            testRunner.And(string.Format("I have completed the form with valid {0} and {1}", username, password),
                ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 41
            testRunner.When("I press the Login button", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "When ");
#line 42
            testRunner.Then("I will be shown an error message \'User details not found.  Please enter valid det" +
                            "ails or Register\'", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("1.1e User leaves either field blank")]
        [NUnit.Framework.TestCaseAttribute("\'\'", "password1!", null)]
        [NUnit.Framework.TestCaseAttribute("username3", "\'\'", null)]
        public virtual void _1_1EUserLeavesEitherFieldBlank(string username, string password, string[] exampleTags)
        {
            var scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("1.1e User leaves either field blank", exampleTags);
#line 51
            this.ScenarioSetup(scenarioInfo);
#line 52
            testRunner.Given("I am on the Login page", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "Given ");
#line 53
            testRunner.And(string.Format("I have left the {0} or {1} fields blank", username, password),
                ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 54
            testRunner.When("I move focus away from either field", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)),
                "When ");
#line 55
            testRunner.Then(
                string.Format("I will be shown an error message \'Please enter a value for {0} or {1} field.\'",
                    username, password), ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}

#pragma warning restore

#endregion