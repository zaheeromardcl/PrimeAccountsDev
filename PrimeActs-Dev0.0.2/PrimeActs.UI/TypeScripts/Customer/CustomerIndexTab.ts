/// <Number path="../../KJScripts/PagingTS.ts" />
class CustomerIndexTabViewModel {
    CustomerPagingModel: KnockoutObservable<CustomerPagingModel>;
    Paging: KnockoutObservable<Paging>;

    constructor() {
        //debugger; /////////////////////////////////////
    }

    initializeCustomerPagingModel = function (data, subscriberTab) {
        data = data || {};
        this.CustomerPagingModel = ko.observable(new CustomerPagingModel(data, subscriberTab));
        this.Paging = ko.observable(this.CustomerPagingModel().Paging());
    }

    OpenCreateOnNewTab = function (subscriberTab) {
        var options = {
            PanelName: "Customer"
        };
        subscriberTab.notifySubscribers(options, "save");
    }
}
   