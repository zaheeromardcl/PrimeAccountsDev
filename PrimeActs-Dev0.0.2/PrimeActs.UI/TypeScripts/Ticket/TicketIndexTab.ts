/// <Number path="../../KJScripts/PagingTS.ts" />
class TicketIndexTabViewModel {
    TicketPagingModel: KnockoutObservable<CustomerTicketPagingModel>;
    Paging: KnockoutObservable<Paging>;

    constructor() {
    }

    initializeTicketPagingModel = function (data, subscriberTab) {
        data = data || {};
        this.TicketPagingModel = ko.observable(new CustomerTicketPagingModel(data, subscriberTab));
        this.Paging = ko.observable(this.TicketPagingModel().Paging());
    }

    OpenCreateOnNewTab = function (subscriberTab) {
        var options = {
            PanelName: "Ticket"
        };
        subscriberTab.notifySubscribers(options, "save");
    }
}
 