/// <reference path="../../KJScripts/PagingTS.ts" />
class StockBoardPaging {
    Paging: KnockoutObservable<Paging>;
    StockBoardSearch: KnockoutObservable<StockBoardSearch>;
    SearchClicked: KnockoutObservable<boolean>;
    IsSearchValid: KnockoutComputed<boolean>;
    SubscriberTab: any;

    constructor(data, subscriberTab) {
        data = data || {};
        this.Paging = ko.observable(new Paging(data.StockBoardEditModels, data.SearchObject));
        this.StockBoardSearch = ko.observable(new StockBoardSearch(data.SearchObject));
        this.SearchClicked = ko.observable(false);
        this.IsSearchValid = ko.computed({
            read: () => {
                return (this.SearchClicked() && !this.StockBoardSearch().validationModel.isValid())
            }
        });
        this.SubscriberTab = subscriberTab;
    }

    Search = function () {
        this.SearchClicked(true);
        if (this.ProduceSearch().validationModel.isValid()) {
            this.Paging().fetchEntitiesWithSearch("StockBoard/Index", this.StockBoardSearch);
        }
    }

    Reset = function () {
        this.SearchClicked(false);
        // reset search box
        this.StockBoardSearch().StockBoardName("");
        // returns all entries
        this.Paging().fetchEntitiesWithSearch("StockBoard/Index", this.StockBoardSearch);
    }

    OpenStockBoardDetails = function (data) {
        var options = {
            TabTitle: "loading...",
            PanelName: "StockBoardDetails",
            UriParam: data
        };
        this.SubscriberTab.notifySubscribers(options, "save");
    }
}

class StockBoardSearch {
    StockBoardName: KnockoutObservable<string>;
    validationModel: KnockoutObservable<any>;

    constructor(data) {
        this.StockBoardName = ko.observable(data.StockBoardName).extend({ required: true });
        this.StockBoardName.isModified(false);
        this.validationModel = ko.validatedObservable({
            ProduceNameOrCode: this.StockBoardName
        });
    }


} 