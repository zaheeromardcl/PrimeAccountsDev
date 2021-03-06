﻿#region Designer generated code

#region

using TechTalk.SpecFlow;

#endregion

#pragma warning disable

namespace PrimeActs.AcceptanceTests
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("F2.2_AmendCompany")]
    public partial class F2_2_AmendCompanyFeature
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
                "F2.2_AmendCompany",
                "In order to amend company title, as a member of the admin role, I should be able " +
                "\nto amend company title.", ProgrammingLanguage.CSharp, ((string[]) (null)));
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
        [NUnit.Framework.DescriptionAttribute("Amend user form")]
        public virtual void AmendUserForm()
        {
            var scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Amend user form", ((string[]) (null)));
#line 12
            this.ScenarioSetup(scenarioInfo);
#line 13
            testRunner.Given("that I am an authorised user and a member of the Admin role", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Given ");
#line 14
            testRunner.And("I have clicked on a username from the list of users on the \'Display Users\' screen" +
                           "", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 15
            testRunner.And("I have edited the user details on the \'Amend User Form\' screen", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 16
            testRunner.When("I click on \'Submit User Details\' button", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "When ");
#line 17
            testRunner.Then("the result should be that the details are updated in the database", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Then ");
#line 18
            testRunner.And("the \'Amended Details\' should be displayed on the \'Confirm Amended details\' screen" +
                           ".", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Select user to amend")]
        [NUnit.Framework.CategoryAttribute("mytag")]
        public virtual void SelectUserToAmend()
        {
            var scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Select user to amend", new string[]
            {
                "mytag"
            });
#line 7
            this.ScenarioSetup(scenarioInfo);
#line 8
            testRunner.Given("I am an authorised user and a member of the Admin role", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Given ");
#line 9
            testRunner.When("I select a user from the list of users", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "When ");
#line 10
            testRunner.Then("the result should be an \'Amend User form\' screen displayed for that user.",
                ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Validation for user form")]
        public virtual void ValidationForUserForm()
        {
            var scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Validation for user form", ((string[]) (null)));
#line 20
            this.ScenarioSetup(scenarioInfo);
#line 21
            testRunner.Given("that I am an authorised user and a member of the Admin role.", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Given ");
#line 22
            testRunner.And("I have clicked on a Username from the list of users on the \'Display Users\' screen" +
                           "", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 23
            testRunner.And("I have edited the User details with invalid entries on the \'Amend User Form\' scre" +
                           "en", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 24
            testRunner.When("I click on \'Submit User Details\' button", ((string) (null)),
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