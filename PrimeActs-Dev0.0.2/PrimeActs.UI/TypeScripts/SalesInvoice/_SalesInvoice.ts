class SalesInvoicePagingModel {
    Paging: KnockoutObservable<Paging>;
    SalesInvoiceSearch: KnockoutObservable<SalesInvoiceSearch>;
    SearchClicked: KnockoutObservable<boolean>;
    IsSearchValid: KnockoutComputed<boolean>;

    constructor(data) {
        this.Paging = ko.observable(new Paging(data.ConsignmentEditModels, data.SearchObject));
        this.SalesInvoiceSearch = ko.observable(new SalesInvoiceSearch());
        this.SearchClicked = ko.observable(false);
        this.IsSearchValid = ko.computed({
            read: () => {
                return (this.SearchClicked() && !this.SalesInvoiceSearch().validationModel.isValid())
            }
        });
    }

    Search = function () {
        this.SearchClicked(true);
        if (this.SalesInvoiceSearch().validationModel.isValid()) {
            this.Paging().fetchEntitiesWithSearch("Invoice/Index", this.SalesInvoiceSearch);
        }
    }

    SearchBySupplierDepartmentId = function () {
        this.SearchClicked(false);
        this.SalesInvoiceSearch().SalesInvoiceReference("");
        this.Paging().fetchEntitiesWithSearch("Invoice/Index", this.SalesInvoiceSearch);
    }

    Reset = function () {
        this.SearchClicked(false);
        // reset search box
        this.SalesInvoiceSearch().SalesInvoiceReference("");
        // returns all entries
        this.Paging().fetchEntitiesWithSearch("Invoice/Index", this.SalesInvoiceSearch);
    }

    OpenConsignmentDetails = function (data) {
        var uri = "Invoice/DetailsTab/" + data;

    }
}

class SalesInvoiceSearch {
    SalesInvoiceReference: KnockoutObservable<string>;
    CustomerDepartmentID: KnockoutObservable<string>;
    validationModel: KnockoutObservable<any>;

    constructor() {
        this.SalesInvoiceReference = ko.observable("").extend({ required: true });
        this.SalesInvoiceReference.isModified(false);
        this.CustomerDepartmentID = ko.observable("0");
        this.validationModel = ko.validatedObservable({
            SalesInvoiceReference: this.SalesInvoiceReference
        });
    }
} 