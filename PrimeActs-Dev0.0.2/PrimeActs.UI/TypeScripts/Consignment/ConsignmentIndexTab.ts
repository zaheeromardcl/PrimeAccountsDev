/// <Number path="../../KJScripts/PagingTS.ts" />
class ConsignmentIndextTabViewModel {
    ConsignmentPagingModel: KnockoutObservable<ConsignmentPagingModel>;
    Paging: KnockoutObservable<Paging>;
    
    constructor() {
        
    }
    
    initializeConsignmentPagingModel = function (data, subscriberTab) {
        data = data || {};
        this.ConsignmentPagingModel = ko.observable(new ConsignmentPagingModel(data, subscriberTab));
        this.Paging = ko.observable(this.ConsignmentPagingModel().Paging());
    }

    OpenCreateOnNewTab = function (subscriberTab) {
        
        var options = {
            PanelName: "Consignment"
        };
        subscriberTab.notifySubscribers(options, "save");
    }
}
