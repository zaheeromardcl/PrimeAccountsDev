class DailyCashTicketPagingModel {
    PaidPaging: KnockoutObservable<DailyCashTicketPaging>;
    UnpaidPaging: KnockoutObservable<DailyCashTicketPaging>;
    paidType: KnockoutObservable<string>;

    constructor(data) {
       // debugger;
        this.paidType = ko.observable(data.paidType || "Paid");
        this.PaidPaging = ko.observable(new DailyCashTicketPaging(data.PaidTickets, "Paid"));
        this.UnpaidPaging = ko.observable(new DailyCashTicketPaging(data.UnpaidTickets, "Unpaid"));
    }
}

class DailyCashTicketPaging {
    Paging: KnockoutObservable<Paging>;
    TicketSearch: KnockoutObservable<TicketSearch>;
    SearchClicked: KnockoutObservable<boolean>;
    IsSearchValid: KnockoutComputed<boolean>;
    
    constructor(data, isPaid) {
        this.Paging = ko.observable(new Paging(data.TicketEditModels, data.SearchObject));
        this.TicketSearch = ko.observable(new TicketSearch(isPaid));
        this.SearchClicked = ko.observable(false);
        this.IsSearchValid = ko.computed({
            read: () => {
                return (this.SearchClicked() && !this.TicketSearch().validationModel.isValid());
            }
        });
    }

    Search = function () {
        this.SearchClicked(true);
        if (this.TicketSearch().validationModel.isValid()) {
            this.Paging().fetchEntitiesWithSearch("Ticket/Index", this.TicketSearch);
        }
    }

    SearchBySupplierDepartmentId = function () {
        this.SearchClicked(false);
        this.TicketSearch().TicketReference("");
        this.Paging().fetchEntitiesWithSearch("Ticket/Index", this.TicketSearch);
    }

    Reset = function () {
        this.SearchClicked(false);
        // reset search box
        this.TicketSearch().TicketReference("");
        // returns all entries
        this.Paging().fetchEntitiesWithSearch("Ticket/Index", this.TicketSearch);
    }

    OpenConsignmentDetails = function (data) {
        var uri = "Ticket/DetailsTab/" + data;

    }
}

class TicketSearch {
    TicketReference: KnockoutObservable<string>;
    CustomerDepartmentID: KnockoutObservable<string>;
    validationModel: KnockoutObservable<any>;
    IsPaid: KnockoutObservable<string>;

    constructor(isPaid) {
        this.IsPaid = ko.observable(isPaid);
        this.TicketReference = ko.observable("").extend({ required: true });
        this.TicketReference.isModified(false);
        this.CustomerDepartmentID = ko.observable("0");
        this.validationModel = ko.validatedObservable({
            TicketReference: this.TicketReference
        });
    }
} 