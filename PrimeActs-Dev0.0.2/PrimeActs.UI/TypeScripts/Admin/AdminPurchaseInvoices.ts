/// <Number path="../../KJScripts/PagingTS.ts" />
/// <reference path="../util/selectoption.ts" />
/// <reference path="loginforinvoiceadmin.ts" />

class AdminPurchaseInvoiceModel {
    Paging: KnockoutObservable<Paging>;
    PurchaseInvoiceSearch: KnockoutObservable<PurchaseInvoiceSearch>;
    SearchClicked: KnockoutObservable<boolean>;
    IsSearchValid: KnockoutComputed<boolean>;
    SubscriberTab: any;
    Statuses: KnockoutObservableArray<SelectOption>;

    Login: KnockoutObservable<LoginInvoiceAdminViewModel>;

    constructor(data, subscriberTab) {
        this.Login = ko.observable(new LoginInvoiceAdminViewModel());

        this.Statuses = ko.observableArray<SelectOption>([]);
        this.Statuses.push(new SelectOption("OK", "OK"));
        this.Statuses.push(new SelectOption("Total", "Total"));
        this.Statuses.push(new SelectOption("PendingQuery", "PendingQuery"));
        this.Statuses.push(new SelectOption("TotalAndPendingQuery", "TotalAndPendingQuery"));
        this.Statuses.push(new SelectOption("Approved", "Approved"));
        this.Statuses.push(new SelectOption("Rejected", "Rejected"));
        
        this.Paging = ko.observable(new Paging(data.PurchaseInvoiceEditModels, data.SearchObject));
        this.PurchaseInvoiceSearch = ko.observable(new PurchaseInvoiceSearch());
        this.SearchClicked = ko.observable(false);
        this.IsSearchValid = ko.computed({
            read: () => {
                return (this.SearchClicked() && !this.PurchaseInvoiceSearch().validationModel.isValid())
            }
        });

        this.SubscriberTab = subscriberTab;
    }

    SelectOptionColor = function(status) {
        let css = "";
        switch (status) {
            case "OK":
                css = "";
                break;
            case "Approved":
                css = "label-success";
                break;
            case "Rejected":
                css = "label-warning";
                break;
            case "Total":
                css = "label-info";
                break;
            case "PendingQuery":
                css = "label-info";
                break;
            case "TotalAndPendingQuery":
                css = "label-info";
                break;
            default:
                css = "";
        }
        
        return css;
    }

    UpdateClass = function (control, status) {
        let css = this.SelectOptionColor(status);

        control.className = "form-control " + css;
    }

    UpdateAll = function (data, status) {
        for (let i = 0; i < data.length; i++) {
            if (data[i].Status !== "OK") {
                
                data[i].Status = status;
                this.UpdatePurchaseInvoice(data[i]);
            }
        }
    }

    UpdatePurchaseInvoice = function (data) {
        if (data.Status !== data.PreviousStatus) {
            let self = this;
            var promise = $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                url: '/api/InvoiceAdmin/UpdatePurchaseInvoice?id='+data.PurchaseInvoiceID+'&status='+data.Status
            });

            promise.done(function (result) {
                self.Paging().reloadPage();
            });

            promise.fail(function (jqXHR, textStatus, errorThrown) {
                alert("failed to update status");
            });
        }
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
