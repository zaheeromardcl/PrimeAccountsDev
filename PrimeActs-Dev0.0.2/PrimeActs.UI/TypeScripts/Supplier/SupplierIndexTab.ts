/// <Number path="../../KJScripts/PagingTS.ts" />
class SupplierIndexTabViewModel {
    SupplierPagingModel: KnockoutObservable<SupplierPagingModel>;
    Paging: KnockoutObservable<Paging>;

    constructor() {
    //debugger; /////////////////////////////////////
    }

    initializeSupplierPagingModel = function (data, subscriberTab) {
        data = data || {};
        this.SupplierPagingModel = ko.observable(new SupplierPagingModel(data, subscriberTab));
        this.Paging = ko.observable(this.SupplierPagingModel().Paging());
    }

    OpenCreateOnNewTab = function (subscriberTab) {
        var options = {
            PanelName: "Supplier"
        };
        subscriberTab.notifySubscribers(options, "save");
    }
}
