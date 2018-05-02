/// <Number path="../../KJScripts/PagingTS.ts" />
class CustomerPagingModel {
    Paging: KnockoutObservable<Paging>;
    CustomerSearch: KnockoutObservable<CustomerSearch>;
    SearchClicked: KnockoutObservable<boolean>;
    IsSearchValid: KnockoutComputed<boolean>;
    SubscriberTab: any;

    constructor(data, subscriberTab) {
        this.Paging = ko.observable(new Paging(data.CustomerEditModels || {}, data.SearchObject));
        this.CustomerSearch = ko.observable(new CustomerSearch());
        this.SearchClicked = ko.observable(false);
        this.IsSearchValid = ko.computed({
            read: () => {
                return (this.SearchClicked() && !this.CustomerSearch().validationModel.isValid())
            }
        });
        this.SubscriberTab = subscriberTab;
    }

    Search = function () {
        this.SearchClicked(true);
        if (this.CustomerSearch().validationModel.isValid()) {
            this.Paging().fetchEntitiesWithSearch("Customer/Index", this.CustomerSearch);
        }
    }

    SearchByCustomerName = function () {
        this.SearchClicked(false);
        this.CustomerSearch().CustomerName("");
        this.Paging().fetchEntitiesWithSearch("Customer/Index", this.CustomerSearch);
    }

    Reset = function () {
        this.SearchClicked(false);
        // reset search box
        this.CustomerSearch().CustomerName("");
        // returns all entries
        this.Paging().fetchEntitiesWithSearch("Customer/Index", this.CustomerName);
    }

    OpenDetailsTab = function (data, customerName) {
        var options = {
            TabTitle: customerName,
            PanelName: "CustomerDetails",
            UriParam: data
        };
        //debugger; ////////////////////////////////////////////
        this.SubscriberTab.notifySubscribers(options, "save");
    }

    OpenCustomerDetails = function (data, customerName) {
        console.log('OpenCustomerDetails_data ', data);
        this.OpenDetailsTab(data, customerName);
    }

    OpenEditTab = function (data) {
        var options = {
            TabTitle: "loading...",
            PanelName: "CustomerEdit",
            UriParam: data
        };
        this.SubscriberTab.notifySubscribers(options, "save");
    }

    OpenCustomerEdit = function (data) {
        console.log('OpenCustomerEdit_data ', data);
        this.OpenEditTab(data);
    }
}

class CustomerSearch {
    CustomerName: KnockoutObservable<string>;
    CustomerDepartmentID: KnockoutObservable<string>;
    validationModel: KnockoutObservable<any>;
    RecordsInDays: KnockoutObservable<string>;

    constructor() {
        this.CustomerName = ko.observable("").extend({ required: true });
        this.CustomerName.isModified(false);
        this.CustomerDepartmentID = ko.observable("0");
        this.RecordsInDays = ko.observable("LASTMONTH");
        this.validationModel = ko.validatedObservable({
            CustomerCompanyName: this.CustomerName
        });
    }
} 
 