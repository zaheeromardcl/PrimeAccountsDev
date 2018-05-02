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
    [NUnit.Framework.DescriptionAttribute("F3.0_ConsignmentDetails")]
    public partial class F3_0_ConsignmentDetailsFeature
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
                "F3.0_ConsignmentDetails", "As a user, it should be possible to add inventory items to a consignment.",
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
        [NUnit.Framework.DescriptionAttribute("Consignment #123434 created with no items")]
        [NUnit.Framework.CategoryAttribute("myConsignmentDetails")]
        [NUnit.Framework.TestCaseAttribute("AARD", "1.23", "1.34", "2.34", "2.34", "Purleys", "4", "8", "1.23", "1.24",
            "2", "1", "10", "10", null)]
        public virtual void Consignment123434CreatedWithNoItems(string produce, string estimatedpurchasecost,
            string estimatedChargeCostPerPack, string estimatedPercentageProfit, string returns, string brand,
            string pack, string packSize, string packWeight, string packPall, string packWtUnit, string porterage,
            string expectedQuantity, string recievedQuantity, string[] exampleTags)
        {
            var @__tags = new string[]
            {
                "myConsignmentDetails"
            };
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            var scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Consignment #123434 created with no items", @__tags);
#line 5
            this.ScenarioSetup(scenarioInfo);
#line 6
            testRunner.Given("that the user is an authenticated user with the appropriate role", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Given ");
#line 7
            testRunner.And("the user has navigated to the CreateConsignment Items page", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 8
            testRunner.And(string.Format("the user has completed the {0} field", produce), ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 9
            testRunner.And("the user has comppleted the <EstimatedPurchaseCost> field", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 10
            testRunner.And(string.Format("the user has completed the {0} field", estimatedChargeCostPerPack),
                ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 11
            testRunner.And(string.Format("the user has comppleted the {0} field", estimatedPercentageProfit),
                ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 12
            testRunner.And(string.Format("the user has completed the {0} field", returns), ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 13
            testRunner.And(string.Format("the user has completed the {0} field", brand), ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 14
            testRunner.And(string.Format("the user has completed the {0} field", pack), ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 15
            testRunner.And(string.Format("the user has completed the {0} field", packSize), ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 16
            testRunner.And(string.Format("the user has completed the {0} field", packWeight), ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 17
            testRunner.And(string.Format("the user has completed the {0} field", packPall), ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 18
            testRunner.And(string.Format("the user has completed the {0} field", packWtUnit), ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 19
            testRunner.And(string.Format("the user has completed the {0} field", porterage), ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 20
            testRunner.And(string.Format("the user has completed the {0} field", expectedQuantity), ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 21
            testRunner.And("the user has completed the <ReceivedQuantity> field", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 22
            testRunner.And("the user has clicked on the <CreateConsignmentItem> button", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 23
            testRunner.Then("the consignment items details should be saved to the database", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Then ");
#line 24
            testRunner.And("the consignment details page updated to show the new items.", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}

#pragma warning restore

#endregion