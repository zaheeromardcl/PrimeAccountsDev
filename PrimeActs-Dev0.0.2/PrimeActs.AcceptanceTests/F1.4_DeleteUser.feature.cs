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
    [NUnit.Framework.DescriptionAttribute("F1.4_DeleteUser")]
    public partial class F1_4_DeleteUserFeature
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
                "F1.4_DeleteUser", "In order to avoid lots of unused Users, the Admin role should be able to deactiva" +
                                   "te users.", ProgrammingLanguage.CSharp, ((string[]) (null)));
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
        [NUnit.Framework.DescriptionAttribute("1.4a Navigate to Manage Users screen")]
        [NUnit.Framework.CategoryAttribute("mytag")]
        public virtual void _1_4ANavigateToManageUsersScreen()
        {
            var scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("1.4a Navigate to Manage Users screen", new string[]
            {
                "mytag"
            });
#line 6
            this.ScenarioSetup(scenarioInfo);
#line 7
            testRunner.Given("I am an authorised user and a member of the Admin role", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Given ");
#line 8
            testRunner.When("I click on the Manage Users link on the Settings menu", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "When ");
#line 9
            testRunner.Then("the \'Manage User\' screen is displayed.", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("1.4b User clicks on Delete User Icon on the Manage User Screen")]
        public virtual void _1_4BUserClicksOnDeleteUserIconOnTheManageUserScreen()
        {
            var scenarioInfo =
                new TechTalk.SpecFlow.ScenarioInfo("1.4b User clicks on Delete User Icon on the Manage User Screen",
                    ((string[]) (null)));
#line 12
            this.ScenarioSetup(scenarioInfo);
#line 13
            testRunner.Given("I have an authorised user and a member of the \'Admin\' Role", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Given ");
#line 14
            testRunner.When("I click on the Delete icon on the Manage User screen for one user", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "When ");
#line 15
            testRunner.Then("a confirmation prompt \'Are you sure you want to delete this user?\'", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("1.4c User answers yes to the confirmation of deletion prompt")]
        public virtual void _1_4CUserAnswersYesToTheConfirmationOfDeletionPrompt()
        {
            var scenarioInfo =
                new TechTalk.SpecFlow.ScenarioInfo("1.4c User answers yes to the confirmation of deletion prompt",
                    ((string[]) (null)));
#line 17
            this.ScenarioSetup(scenarioInfo);
#line 18
            testRunner.Given("I have answered yes to the confirmation prompt", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Given ");
#line 19
            testRunner.When("I click on the \'Delete User\' button", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "When ");
#line 20
            testRunner.Then("a deactivated flag should be marked to true in the database for that user",
                ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "Then ");
#line 21
            testRunner.And("the user is prevented from authenticate from that point forward", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 22
            testRunner.And("the deletion confirmed screen should be displayed", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 23
            testRunner.And("the user is redirected to the Manager Users screen", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("1.4d User presses on Delete Users(multiple users) button")]
        public virtual void _1_4DUserPressesOnDeleteUsersMultipleUsersButton()
        {
            var scenarioInfo =
                new TechTalk.SpecFlow.ScenarioInfo("1.4d User presses on Delete Users(multiple users) button",
                    ((string[]) (null)));
#line 25
            this.ScenarioSetup(scenarioInfo);
#line 26
            testRunner.Given("I am an authorised user and a member of the Admin role", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Given ");
#line 27
            testRunner.And("I have browsed to the Manage Users screen", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 28
            testRunner.When("I click on \'Delete Users\' button", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)),
                "When ");
#line 29
            testRunner.Then("a checkbox for each user should be displayed on the screen", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Then ");
#line 30
            testRunner.And("a \'Select All\' option should be displayed at the top of the page", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("1.4e User selects multiple users to delete.")]
        public virtual void _1_4EUserSelectsMultipleUsersToDelete_()
        {
            var scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("1.4e User selects multiple users to delete.",
                ((string[]) (null)));
#line 32
            this.ScenarioSetup(scenarioInfo);
#line 33
            testRunner.Given("I am an authorised user and a member of the Admin role", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Given ");
#line 34
            testRunner.And("I have browsed to the Manager Users screen", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 35
            testRunner.When("I click on \'Delete Users\' button", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)),
                "When ");
#line 36
            testRunner.And("I select multiple users from the list", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 37
            testRunner.Then("a confirmation prompt \'Are you sure you want to delete these users?\' should be di" +
                            "splayed", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute(
            "1.4f User answers yes to the confirmation of deletion of multiple users prompt")]
        public virtual void _1_4FUserAnswersYesToTheConfirmationOfDeletionOfMultipleUsersPrompt()
        {
            var scenarioInfo =
                new TechTalk.SpecFlow.ScenarioInfo(
                    "1.4f User answers yes to the confirmation of deletion of multiple users prompt",
                    ((string[]) (null)));
#line 39
            this.ScenarioSetup(scenarioInfo);
#line 40
            testRunner.Given("I have selected multiple users from the Manage Users List", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Given ");
#line 41
            testRunner.When("I answer yes to the confirmation prompt of deletion of multiple users", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "When ");
#line 42
            testRunner.Then("the selected users are prevented from authenticating from that point forward",
                ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "Then ");
#line 43
            testRunner.And("the deletion confirmed screen should be displayed with a list of the deleted user" +
                           "s", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 44
            testRunner.And("the user is redirected to the Manager Users screen", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}

#pragma warning restore

#endregion