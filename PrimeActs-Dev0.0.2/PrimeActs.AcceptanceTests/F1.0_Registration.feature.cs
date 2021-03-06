﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.9.0.77
//      SpecFlow Generator Version:1.9.0.0
//      Runtime Version:4.0.30319.42000
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace PrimeActs.AcceptanceTests
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Registration")]
    public partial class RegistrationFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "F1.0_Registration.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Registration", "In order to use the applicationa as an authenticated user\nI want to register a ne" +
                    "w account", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
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
        [NUnit.Framework.DescriptionAttribute("1.0a Register button is clicked on Login Page")]
        [NUnit.Framework.CategoryAttribute("mytag")]
        public virtual void _1_0ARegisterButtonIsClickedOnLoginPage()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("1.0a Register button is clicked on Login Page", new string[] {
                        "mytag"});
#line 6
this.ScenarioSetup(scenarioInfo);
#line 7
 testRunner.Given("I browse to Application Home Page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 8
 testRunner.When("I click on the Register Button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 9
 testRunner.Then("the blank Register Page should be displayed.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("1.0b User enters details on Register page")]
        [NUnit.Framework.TestCaseAttribute("User1", "Plummy78", "Plummy78", "user1@company.com", "user1fname", "user1lname", "user1nname", null)]
        public virtual void _1_0BUserEntersDetailsOnRegisterPage(string username, string password, string confirmpassword, string email, string firstname, string lastname, string nickname, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("1.0b User enters details on Register page", exampleTags);
#line 12
this.ScenarioSetup(scenarioInfo);
#line 13
testRunner.Given("I complete the Register Page with valid data", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 14
 testRunner.When(string.Format("I enter {0} in the Username field", username), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 15
  testRunner.And(string.Format("I enter {0} in the password field", password), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 16
  testRunner.And(string.Format("I enter the {0} in the confirm password field", confirmpassword), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 17
  testRunner.And(string.Format("I enter the {0} in the email field", email), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 18
  testRunner.And(string.Format("I enter the {0} in the Firstname field", firstname), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 19
  testRunner.And(string.Format("I enter the {0} in the Lastname field", lastname), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 20
  testRunner.And(string.Format("I enter the {0} in the Nickname field", nickname), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 21
  testRunner.And("the Register button is pressed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 22
testRunner.Then("the Home page should be displayed with a \'Welcome <firstmame>\' message should be " +
                    "displayed and an email should be sent to all users in the \'Administrators\' Role " +
                    "confirming the user has registered", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 23
testRunner.But(string.Format("an error message \'A user already exists with that {0}\' should be displayed if the" +
                        " username is already in use.", username), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "But ");
#line 24
testRunner.But(string.Format("an error message \'Password is not valid. Please enter a strong password\' should b" +
                        "e displayed if the {0} is not a strong password", password), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "But ");
#line 25
testRunner.But(string.Format("an error message \'Passwords do not match. Please re-enter your password\' should b" +
                        "e displayed if the {0} does not match the {1}", confirmpassword, password), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "But ");
#line 26
testRunner.But(string.Format("an error message \'Email is not valid.  Please re-enter your email address\' should" +
                        " be displayed if the {0} is not of the correct format", email), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "But ");
#line 27
testRunner.But(string.Format("an error message \'Please complete all the fields\' if any of the fields {0},{1}, {" +
                        "2}, {3},{4},{5},{6} are left blank", username, password, confirmpassword, email, firstname, lastname, nickname), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "But ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
