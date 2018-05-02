/// <Number path="../../KJScripts/PagingTS.ts" />
class PurchaseInvoicePagingModel {
    Paging: KnockoutObservable<Paging>;
    PurchaseInvoiceSearch: KnockoutObservable<PurchaseInvoiceSearch>;
    SearchClicked: KnockoutObservable<boolean>;
    IsSearchValid: KnockoutComputed<boolean>;
    SubscriberTab: any;

    constructor(data, subscriberTab) {
        //debugger;
        data = data || {};
        this.Paging = ko.observable(new Paging(data.PurchaseInvoiceEditModels || {}, data.SearchObject));
        this.PurchaseInvoiceSearch = ko.observable(new PurchaseInvoiceSearch());
        this.SearchClicked = ko.observable(false);
        this.IsSearchValid = ko.computed({
            read: () => {
                return (this.SearchClicked() && !this.PurchaseInvoiceSearch().validationModel.isValid())
            }
        });
        this.SubscriberTab = subscriberTab;
    }

    Search = function () {
        this.SearchClicked(true);
        if (this.PurchaseInvoiceSearch().validationModel.isValid()) {
            this.Paging().fetchEntitiesWithSearch("PurchaseInvoice/Index", this.PurchaseInvoiceSearch);
        }
    }

    SearchBySupplierDepartmentId = function () {
        this.SearchClicked(false);
        this.PurchaseInvoiceSearch().PurchaseInvoiceReference("");
        this.Paging().fetchEntitiesWithSearch("PurchaseInvoice/Index", this.PurchaseInvoiceSearch);
    }

    Reset = function () {
        this.SearchClicked(false);
        // reset search box
        this.PurchaseInvoiceSearch().PurchaseInvoiceReference("");
        // returns all entries
        this.Paging().fetchEntitiesWithSearch("PurchaseInvoice/Index", this.PurchaseInvoiceSearch);
    }
    
    OpenDetails = function (data) {
        var options = {
            TabTitle: "loading...",
            PanelName: "PurchaseInvoiceDetails",
            UriParam: data
        };
        this.SubscriberTab.notifySubscribers(options, "save");
    }
}

class PurchaseInvoiceSearch {
    PurchaseInvoiceReference: KnockoutObservable<string>;
    SupplierDepartmentID: KnockoutObservable<string>;
    validationModel: KnockoutObservable<any>;
    RecordsInDays: KnockoutObservable<string>;

    constructor() {
        this.PurchaseInvoiceReference = ko.observable("").extend({ required: true });
        this.PurchaseInvoiceReference.isModified(false);
        this.SupplierDepartmentID = ko.observable("");
        this.RecordsInDays = ko.observable("LASTMONTH");
        this.validationModel = ko.validatedObservable({
            PurchaseInvoiceReference: this.PurchaseInvoiceReference
        });
    }
}