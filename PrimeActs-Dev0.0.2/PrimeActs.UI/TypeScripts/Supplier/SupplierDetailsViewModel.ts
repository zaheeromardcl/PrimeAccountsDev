/// <reference path="../../Scripts/typings/jquery/jquery.d.ts" />
/// <reference path="../../Scripts/typings/knockout/knockout.d.ts" />
/// <reference path="SupplierViewModel.ts" />

class SupplierDetailsViewModel {
    SupplierModel: KnockoutObservable<SupplierViewModel>;
    TabPanelName: KnockoutObservable<string>;
    TabPanelAlphaName: KnockoutComputed<string>;

    constructor(data, tabPanelName: string) {
        data = data || {};

        var supplierItemModels = [];
        if (data.hasOwnProperty('SupplierItems')) {
            for (var supplierItemEditModel of data.SupplierItems) {
                supplierItemModels.push(new SupplierItemViewModel(supplierItemEditModel));
            }
        }

        this.SupplierModel = ko.observable(new SupplierViewModel(data, supplierItemModels));
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
        this.setStateOnServer(data, this.TabPanelAlphaName(), "Details", this.SupplierModel().SupplierID(), initial);
    }

    mapRestoredState = function (data) {
        var supplierItemModels = [];
        if (data.hasOwnProperty('SupplierItems')) {
            for (var supplierItemEditModel of data.SupplierItems) {
                supplierItemModels.push(new SupplierItemViewModel(supplierItemEditModel));
            }
        }

        this.initDone = false;
        this.SupplierModel(new SupplierViewModel(data, supplierItemModels));
    }

    openCreateSupplier = function (subscriberReplaceTab) {
        this.openTab(subscriberReplaceTab, "CreateSupplier");
    }

    openTab = function (subscriberReplaceTab, target) {
        var options = {
            PanelName: this.TabPanelName(),
            NewPanelName: target
        };
        subscriberReplaceTab.notifySubscribers(options, "save");
    }
}
 