/// <Number path="../../KJScripts/PagingTS.ts" />
class ConsignmentIndextTabIndexViewModel {
    ConsignmentIndexPagingModel: KnockoutObservable<ConsignmentIndexPagingModel>;
    Paging: KnockoutObservable<Paging>;
    
    constructor() {
        
    }
    
    initializeConsignmentPagingModel = function (data, subscriberTab) {
        data = data || {};
        this.ConsignmentIndexPagingModel = ko.observable(new ConsignmentIndexPagingModel(data, subscriberTab));
        this.Paging = ko.observable(this.ConsignmentIndexPagingModel().Paging());
    }

    OpenCreateOnNewTab = function (subscriberTab) {
        
        var options = {
            PanelName: "Consignment"
        };
        subscriberTab.notifySubscribers(options, "save");
    }
}
