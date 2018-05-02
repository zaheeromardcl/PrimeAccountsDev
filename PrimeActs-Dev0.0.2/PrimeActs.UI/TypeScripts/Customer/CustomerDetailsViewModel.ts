/// <reference path="../../Scripts/typings/jquery/jquery.d.ts" />
/// <reference path="../../Scripts/typings/knockout/knockout.d.ts" />
/// <reference path="CustomerViewModel.ts" />

class CustomerDetailsViewModel {
    CustomerModel2: KnockoutObservable<CustomerViewModel2>;
    TabPanelName: KnockoutObservable<string>;
    TabPanelAlphaName: KnockoutComputed<string>;

    constructor(data, tabPanelName: string) {
        data = data || {};

        var customerItemModels = [];
        if (data.hasOwnProperty('CustomerItems')) {
            for (var customerItemEditModel of data.CustomerItems) {
                customerItemModels.push(new CustomerItemViewModel(customerItemEditModel));
            }
        }

        this.CustomerModel2 = ko.observable(new CustomerViewModel2(data, customerItemModels));
        this.TabPanelName = ko.observable(tabPanelName);
        this.TabPanelAlphaName = ko.computed({
            read: () => {
                return this.TabPanelName().replace(/[0-9]/g, '');
            }
        });
    }

    setStateOnServer = function (data: string, contentType: string, controllerState: string, uriParam: string, initial: boolean) {
        var panelId: string = this.TabPanelName().match(/\d+/g).join([]);
        $.ajax({
            type: 'POST',
            url: "/api/TabPanel",
            dataType: 'application/json',
            data: {
                id: panelId, jsondata: data, panelname: this.TabPanelName(), content_type: contentType, controller_state: controllerState, uriParam: uriParam, initial: initial
            },
            success: function (data) {
            },
            error: function (xhr) {
            }
        });
    }

    getState = function () {
        var self = this;
        var panelId: string = this.TabPanelName().match(/\d+/g).join([]);
        return $.ajax({
            type: 'GET',
            url: "/api/TabPanel",
            dataType: 'json',
            data: { "id": panelId, "name": this.TabPanelName() },
            success: function (data) {
                return data;
            },
            error: function (xhr) {
                console.log("failure in GET from State");
                console.log(xhr);
                return null;
            }
        });
    }

    pageLoadState = function () {
        var self = this;
        this.getState()
            .done(function (data) {
                if (data == null || data == "") {
                    self.saveState(true);
                }
                else {
                    var jsonData = JSON.parse(data);
                    self.mapRestoredState(jsonData);
                }
            });
    }

    saveState = function (initial: boolean) {
        var data = ko.toJSON(this.SupplierModel());
        this.setStateOnServer(data, this.TabPanelAlphaName(), "Details", this.CustomerModel().CustomerID(), initial);
    }

    mapRestoredState = function (data) {
        var customerItemModels = [];
        if (data.hasOwnProperty('CustomerItems')) {
            for (var customerItemEditModel of data.CustomerItems) {
                customerItemModels.push(new CustomerItemViewModel(customerItemEditModel));
            }
        }

        this.initDone = false;
        this.CustomerModel2(new CustomerViewModel2(data, customerItemModels));
    }

    openCreateCustomer = function (subscriberReplaceTab) {
        this.openTab(subscriberReplaceTab, "CreateCustomer");
    }

    openTab = function (subscriberReplaceTab, target) {
        var options = {
            PanelName: this.TabPanelName(),
            NewPanelName: target
        };
        subscriberReplaceTab.notifySubscribers(options, "save");
    }
}
 