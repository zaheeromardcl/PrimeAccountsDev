/// <Number path="../../KJScripts/PagingTS.ts" />
class CompletedConsignmentsTabViewModel {
    ConsignmentPagingModel: KnockoutObservable<CompletedConsignmentPagingModel>;
    Paging: KnockoutObservable<Paging>;

    constructor() {

    }

    initializeConsignmentPagingModel = function (data, subscriberTab) {
        data = data || {};
        this.ConsignmentPagingModel = ko.observable(new CompletedConsignmentPagingModel(data, subscriberTab));
        this.Paging = ko.observable(this.ConsignmentPagingModel().Paging());
    }

    OpenCreateOnNewTab = function (subscriberTab) {

        var options = {
            PanelName: "Consignment"
        };
        subscriberTab.notifySubscribers(options, "save");
    }
}
