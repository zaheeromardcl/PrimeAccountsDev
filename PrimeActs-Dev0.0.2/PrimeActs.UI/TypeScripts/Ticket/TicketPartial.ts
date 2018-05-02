/// <Number path="../../KJScripts/PagingTS.ts" />
class CustomerTicketPagingModel {
    Paging: KnockoutObservable<Paging>;
    CustomerTicketSearch: KnockoutObservable<CustomerTicketSearch>;
    SearchClicked: KnockoutObservable<boolean>;
    IsSearchValid: KnockoutComputed<boolean>;
    SubscriberTab: any;

    constructor(data, subscriberTab) {
        //this.Paging = ko.observable(new Paging(data.TicketModel, data.SearchObject)); ///////////////////////
        this.Paging = ko.observable(new Paging(data.TicketEditModels || {}, data.SearchObject));
        this.CustomerTicketSearch = ko.observable(new CustomerTicketSearch());
        this.SearchClicked = ko.observable(false);
        this.IsSearchValid = ko.computed({
            read: () => {
                return (this.SearchClicked() && !this.CustomerTicketSearch().validationModel.isValid())
            }
        });
        this.SubscriberTab = subscriberTab;
    }

    Search = function () {
        this.SearchClicked(true);
        if (this.CustomerTicketSearch().validationModel.isValid()) {
            this.Paging().fetchEntitiesWithSearch("Ticket/Index", this.CustomerTicketSearch);
        }
    }

    SearchByCustomerDepartmentId = function () {
        this.SearchClicked(false);
        this.CustomerTicketSearch().TicketReference("");
        this.Paging().fetchEntitiesWithSearch("Ticket/Index", this.CustomerTicketSearch);
    }

    Reset = function () {
        this.SearchClicked(false);
        // reset search box
        this.CustomerTicketSearch().TicketReference("");
        // returns all entries
        this.Paging().fetchEntitiesWithSearch("Ticket/Index", this.TicketReference);
    }

    OpenDetailsTab = function (data, ticketRef) {
        var options = {
            TabTitle: ticketRef,
            PanelName: "TicketDetails",
            UriParam: data
        };
        debugger; ////////////////////////////////////////////
        this.SubscriberTab.notifySubscribers(options, "save");
    }

    OpenTicketDetails = function (data, ticketRef) {
        console.log('OpenTicketDetails_data ', data);
        this.OpenDetailsTab(data, ticketRef);
    }

    OpenEditTab = function (data) {
        var options = {
            TabTitle: "loading...",
            PanelName: "TicketEdit",
            UriParam: data
        };
        this.SubscriberTab.notifySubscribers(options, "save");
    }

    OpenTicketEdit = function (data) {
        console.log('OpenTicketEdit_data ', data);
        this.OpenEditTab(data);
    }

    OpenCustomerTab = function (data) {
        var options = {
            TabTitle: "loading...",
            PanelName: "CustomerDetails",
            UriParam: data
        };
        this.SubscriberTab.notifySubscribers(options, "save");
    }

    OpenCustomerDetails = function (data) {
        console.log('OpenCustomerDetails_data ', data);
        //debugger; ////////////////////////////
        this.OpenCustomerTab(data);
    }
}

class CustomerTicketSearch {
    TicketReference: KnockoutObservable<string>;
    CustomerDepartmentID: KnockoutObservable<string>;
    validationModel: KnockoutObservable<any>;
    RecordsInDays: KnockoutObservable<string>;

    constructor() {
        this.TicketReference = ko.observable("").extend({ required: true });
        this.TicketReference.isModified(false);
        this.CustomerDepartmentID = ko.observable("0");
        this.RecordsInDays = ko.observable("LASTMONTH");
        this.validationModel = ko.validatedObservable({
            PurchaseInvoiceReference: this.TicketReference
        });
    }
}
