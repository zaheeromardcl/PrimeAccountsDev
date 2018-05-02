/// <reference path="../../KJScripts/PagingTS.ts" />
class ProducePagingModel
{
    UseLookupTables: LookupTables;
    Paging: KnockoutObservable<Paging>;
    ProduceSearch: KnockoutObservable<ProduceSearch>;
    SearchClicked: KnockoutObservable<boolean>;
    IsSearchValid: KnockoutComputed<boolean>;
    SubscriberTab: any;

    TabContext: KnockoutObservable<AppUserContextModel>;

    constructor(data, subscriberTab, lookupTables: LookupTables)
    {
        data = data || {};
        this.UseLookupTables = lookupTables;

        //this.TabContext = ko.observable(new AppUserContextModel(data.UserContextModel, true));
        this.TabContext = ko.observable(new AppUserContextModel(data.UserContextModel, this.UseLookupTables));

        this.Paging = ko.observable(new Paging(data.ProducePagingModel.ProduceEditModels, data.ProducePagingModel.SearchObject));
        this.ProduceSearch = ko.observable(new ProduceSearch(data.ProducePagingModel.SearchObject));        
        this.SearchClicked = ko.observable(false);
        this.IsSearchValid = ko.computed({
            read: () => {
                return (this.SearchClicked() && !this.ProduceSearch().validationModel.isValid())
            }
        });
        this.SubscriberTab = subscriberTab;
    }

    Search = function () {
        this.SearchClicked(true);
        if (this.ProduceSearch().validationModel.isValid()) {
            this.Paging().fetchEntitiesWithSearch("Produce/Index", this.ProduceSearch);    
        }
    }  

    Reset = function () {        
        this.SearchClicked(false);
        // reset search box
        this.ProduceSearch().ProduceNameOrCode("");
        // returns all entries
        this.Paging().fetchEntitiesWithSearch("Produce/Index", this.ProduceSearch);
    }

    OpenProduceDetails = function (data) {
        var options = {
            TabTitle: "loading...",
            PanelName: "ProduceDetails",
            UriParam: data
        };
        this.SubscriberTab.notifySubscribers(options, "save");
    }
}

class ProduceSearch
{
    ProduceNameOrCode: KnockoutObservable<string>;
    validationModel: KnockoutObservable<any>;

    constructor(data)
    {
        this.ProduceNameOrCode = ko.observable(data.ProduceNameOrCode).extend({ required: true });
        this.ProduceNameOrCode.isModified(false);
        this.validationModel = ko.validatedObservable({
            ProduceNameOrCode: this.ProduceNameOrCode
        });
    }


}