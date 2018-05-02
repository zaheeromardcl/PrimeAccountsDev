/// <Number path="../../KJScripts/PagingTS.ts" />
class SupplierPagingModel {
    Paging: KnockoutObservable<Paging>;
    SupplierSearch: KnockoutObservable<SupplierSearch>;
    SearchClicked: KnockoutObservable<boolean>;
    IsSearchValid: KnockoutComputed<boolean>;
    SubscriberTab: any;

    constructor(data, subscriberTab) {
        this.Paging = ko.observable(new Paging(data.SupplierEditModels || {}, data.SearchObject));
        this.SupplierSearch = ko.observable(new SupplierSearch());
        this.SearchClicked = ko.observable(false);
        this.IsSearchValid = ko.computed({
            read: () => {
                return (this.SearchClicked() && !this.SupplierSearch().validationModel.isValid())
            }
        });
        this.SubscriberTab = subscriberTab;
    }

    Search = function () {
        this.SearchClicked(true);
        if (this.SupplierSearch().validationModel.isValid()) {
            this.Paging().fetchEntitiesWithSearch("Supplier/Index", this.SupplierSearch);
        }
    }

    SearchBySupplierName = function () {
        this.SearchClicked(false);
        this.SupplierSearch().SupplierName("");
        this.Paging().fetchEntitiesWithSearch("Supplier/Index", this.SupplierSearch);
    }

    Reset = function () {
        this.SearchClicked(false);
        // reset search box
        this.SupplierSearch().SupplierName("");
        // returns all entries
        this.Paging().fetchEntitiesWithSearch("Supplier/Index", this.SupplierName);
    }

    OpenDetailsTab = function (data, supplierName) {
        var options = {
            TabTitle: supplierName,
            PanelName: "SupplierDetails",
            UriParam: data
        };
        //debugger; ////////////////////////////////////////////
        this.SubscriberTab.notifySubscribers(options, "save");
    }

    OpenSupplierDetails = function (data, supplierName) {
        console.log('OpenSupplierDetails_data ', data);
        this.OpenDetailsTab(data, supplierName);
    }

    OpenEditTab = function (data) {
        var options = {
            TabTitle: "loading...",
            PanelName: "SupplierEdit",
            UriParam: data
        };
        this.SubscriberTab.notifySubscribers(options, "save");
    }

    OpenSupplierEdit = function (data) {
        console.log('OpenSupplierEdit_data ', data);
        this.OpenEditTab(data);
    }
}

class SupplierSearch {
    SupplierName: KnockoutObservable<string>;
    SupplierDepartmentID: KnockoutObservable<string>;
    validationModel: KnockoutObservable<any>;
    RecordsInDays: KnockoutObservable<string>;

    constructor() {
        this.SupplierName = ko.observable("").extend({ required: true });
        this.SupplierName.isModified(false);
        this.SupplierDepartmentID = ko.observable("0");
        this.RecordsInDays = ko.observable("LASTMONTH");
        this.validationModel = ko.validatedObservable({
            SupplierCompanyName: this.SupplierName
        });
    }
} 
