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
    [NUnit.Framework.DescriptionAttribute("Forgot my password")]
    public partial class ForgotMyPasswordFeature
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
                "Forgot my password",
                "If the user has forgotten their password \nthey should be able to reset it using t" +
                "he application.", ProgrammingLanguage.CSharp, ((string[]) (null)));
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
        [NUnit.Framework.DescriptionAttribute("1.5a On logging in the user has forgotten their password.")]
        [NUnit.Framework.CategoryAttribute("mytag")]
        public virtual void _1_5AOnLoggingInTheUserHasForgottenTheirPassword_()
        {
            var scenarioInfo =
                new TechTalk.SpecFlow.ScenarioInfo("1.5a On logging in the user has forgotten their password.",
                    new string[]
                    {
                        "mytag"
                    });
#line 6
            this.ScenarioSetup(scenarioInfo);
#line 7
            testRunner.Given("I have forgotten my password", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)),
                "Given ");
#line 8
            testRunner.When("I click on the Forgot My Password link on the Login Form", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "When ");
#line 9
            testRunner.Then("the Forgot my Password Form should be displayed.", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("1.5b  When the user starts the forget my password process.")]
        [NUnit.Framework.TestCaseAttribute("username1", null)]
        public virtual void _1_5BWhenTheUserStartsTheForgetMyPasswordProcess_(string username, string[] exampleTags)
        {
            var scenarioInfo =
                new TechTalk.SpecFlow.ScenarioInfo("1.5b  When the user starts the forget my password process.",
                    exampleTags);
#line 11
            this.ScenarioSetup(scenarioInfo);
#line 12
            testRunner.Given("that I have clicked on the \'Forgot my password\' link on the \'Login Form\'",
                ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "Given ");
#line 13
            testRunner.When("I enter <username> in the Username field", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "When ");
#line 14
            testRunner.And("I press the Reset my password button", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)),
                "And ");
#line 15
            testRunner.Then("an automatically generated email with a link to enable me to \'reset my password\' " +
                            "should be sent to my email address.", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)),
                "Then ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("1.5c when the user has recieved the \'Reset My password Email\'")]
        public virtual void _1_5CWhenTheUserHasRecievedTheResetMyPasswordEmail()
        {
            var scenarioInfo =
                new TechTalk.SpecFlow.ScenarioInfo("1.5c when the user has recieved the \'Reset My password Email\'",
                    ((string[]) (null)));
#line 21
            this.ScenarioSetup(scenarioInfo);
#line 22
            testRunner.Given("that the user has recieved the auto-generated email", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Given ");
#line 23
            testRunner.When("the user clicks on the link in the email", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "When ");
#line 24
            testRunner.Then("the result should be that the \'Reset My Password Form\' screen should be displayed" +
                            ".", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("1.5d when the user enters a new valid password and confirms it.")]
        [NUnit.Framework.TestCaseAttribute("Password1!", null)]
        public virtual void _1_5DWhenTheUserEntersANewValidPasswordAndConfirmsIt_(string password, string[] exampleTags)
        {
            var scenarioInfo =
                new TechTalk.SpecFlow.ScenarioInfo("1.5d when the user enters a new valid password and confirms it.",
                    exampleTags);
#line 26
            this.ScenarioSetup(scenarioInfo);
#line 27
            testRunner.Given("that the user has clicked on the link in the email", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Given ");
#line 28
            testRunner.And("the \'Reset My Password Form\' screen is displayed", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 29
            testRunner.When(string.Format("the user enters the new {0} into the password fields", password),
                ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "When ");
#line 30
            testRunner.And("clicks on the \'Reset my Password\' button", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 31
            testRunner.Then("the password should be updated in the database", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Then ");
#line 32
            testRunner.And("the \'Password Reset\' confirmation screen should be displayed.", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("1.5e when the user enters a new Invalid password.")]
        [NUnit.Framework.TestCaseAttribute("password", null)]
        public virtual void _1_5EWhenTheUserEntersANewInvalidPassword_(string password, string[] exampleTags)
        {
            var scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("1.5e when the user enters a new Invalid password.",
                exampleTags);
#line 37
            this.ScenarioSetup(scenarioInfo);
#line 38
            testRunner.Given("that the user has clicked on the link in the email", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Given ");
#line 39
            testRunner.And("the \'Reset My Password Form\' screen is displayed", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 40
            testRunner.When("the user enters a new invalid  <passwprd> into the password fields", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "When ");
#line 41
            testRunner.And("clicks on the \'Reset my Password\' button", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 42
            testRunner.Then("the result should be that the validation error message \'Invalid password, please " +
                            "enter a valid one\' should be displayed", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Then ");
#line 43
            testRunner.And("the Password and Confirm Password fields on the \'Reset my Password form\' should b" +
                           "e reset to blank.", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}

#pragma warning restore

#endregion