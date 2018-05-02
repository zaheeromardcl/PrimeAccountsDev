/// <reference path="../../Scripts/typings/jquery/jquery.d.ts" />
/// <reference path="../../Scripts/typings/knockout/knockout.d.ts" />

class ReceiptTicketDetailsViewModel {
    TicketID: KnockoutObservable<string>;
    TicketReference: KnockoutObservable<string>;
    TicketDate: KnockoutObservable<string>;
    CurrencyID: KnockoutObservable<string>;
    CurrencyName: KnockoutObservable<string>;
    CurrencySymbol: KnockoutObservable<string>;
    CustomerDepartmentID: KnockoutObservable<string>;
    CustomerCompanyName: KnockoutObservable<string>;
    SalesPersonUserID: KnockoutObservable<string>;
    SalesPersonName: KnockoutObservable<string>;
    NoteID: KnockoutObservable<string>;
    Notes: KnockoutObservable<string>;
    UpdatedBy: KnockoutObservable<string>;
    UpdatedDate: KnockoutObservable<string>;
    CreatedBy: KnockoutObservable<string>;
    CreatedDate: KnockoutObservable<string>;
    AmountReceived: KnockoutObservable<number>;

    constructor(data) {
        this.TicketID = ko.observable(data.TicketID);
        this.TicketReference = ko.observable(data.TicketReference);
        this.TicketDate = ko.observable(data.TicketDate);
        this.CurrencyID = ko.observable(data.CurrencyID);
        this.CurrencyName = ko.observable(data.CurrencyName);
        this.CurrencySymbol = ko.observable(data.CurrencySymbol);
        this.CustomerDepartmentID = ko.observable(data.CustomerDepartmentID);
        this.CustomerCompanyName = ko.observable(data.CustomerCompanyName);
        this.SalesPersonUserID = ko.observable(data.SalesPersonUserID);
        this.SalesPersonName = ko.observable(data.SalesPersonName);
        this.NoteID = ko.observable(data.NoteID);
        this.Notes = ko.observable(data.Notes);
        this.UpdatedBy = ko.observable(data.UpdatedBy);
        this.UpdatedDate = ko.observable(data.UpdatedDate);
        this.CreatedBy = ko.observable(data.CreatedBy);
        this.CreatedDate = ko.observable(data.CreatedDate);
        this.AmountReceived = ko.observable(data.AmountReceived)
            .extend({ numeric: 2 });
    }
}

class ReceiptTicketDetailsTabViewModel {
    TicketModel: KnockoutObservable<ReceiptTicketDetailsViewModel>;
    TabPanelName: KnockoutObservable<string>;
    TabPanelAlphaName: KnockoutComputed<string>;

    constructor(data, tabPanelName: string) {
        data = data || {};

        this.TicketModel = ko.observable(new ReceiptTicketDetailsViewModel(data));
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
        var data = ko.toJSON(this.TicketModel());
        this.setStateOnServer(data, this.TabPanelAlphaName(), "Details", this.TicketModel().TicketID(), initial);
    }

    mapRestoredState = function (data) {
        this.initDone = false;
        this.TicketModel(new ReceiptTicketDetailsViewModel(data));
    }

    openCreateTicket = function (subscriberReplaceTab) {
        this.openTab(subscriberReplaceTab, "CreateTicket");
    }

    openTab = function (subscriberReplaceTab, target) {
        var options = {
            PanelName: this.TabPanelName(),
            NewPanelName: target
        };
        subscriberReplaceTab.notifySubscribers(options, "save");
    }
}
