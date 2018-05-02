/// <reference path="../../Scripts/typings/jquery/jquery.d.ts" />
/// <reference path="../../Scripts/typings/knockout/knockout.d.ts" />
/// <reference path="ConsignmentViewModel.ts" />
/// <reference path="ConsignmentItemViewModel.ts" />
class ConsignmentDetailsViewModel {
    UseLookupTables: LookupTables;
    ConsignmentModel: KnockoutObservable<ConsignmentViewModel>;
    TabPanelName: KnockoutObservable<string>;
    TabPanelAlphaName: KnockoutComputed<string>;

    constructor(data, tabPanelName: string, lookupTables: LookupTables) {
        data = data || {};

        this.UseLookupTables = lookupTables;
        //debugger;
        var consignmentItemModels = [];
        if (data.hasOwnProperty('ConsignmentItems')) {
            for (var consignmentItemEditModel of data.ConsignmentItems) {
                consignmentItemModels.push(new ConsignmentItemVM(consignmentItemEditModel, this.UseLookupTables));
            }
        }

        this.ConsignmentModel = ko.observable(new ConsignmentViewModel(data, consignmentItemModels));
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
        var data = ko.toJSON(this.ConsignmentModel());
        this.setStateOnServer(data, this.TabPanelAlphaName(), "Details", this.ConsignmentModel().ConsignmentID(), initial);
    }

    mapRestoredState = function (data) {
        var consignmentItemModels = [];
        if (data.hasOwnProperty('ConsignmentItems')) {
            for (var consignmentItemEditModel of data.ConsignmentItems) {
                consignmentItemModels.push(new ConsignmentItemVM(consignmentItemEditModel, this.UseLookupTables));
            }
        }

        this.initDone = false;
        this.ConsignmentModel(new ConsignmentViewModel(data, consignmentItemModels));
    }

    openCreateConsignment = function (subscriberReplaceTab) {
        this.openTab(subscriberReplaceTab, "Consignment");
    }

    openTab = function (subscriberReplaceTab, target) {
        var options = {
            PanelName: this.TabPanelName(),
            NewPanelName: target
        };
        subscriberReplaceTab.notifySubscribers(options, "save");
    }
}
