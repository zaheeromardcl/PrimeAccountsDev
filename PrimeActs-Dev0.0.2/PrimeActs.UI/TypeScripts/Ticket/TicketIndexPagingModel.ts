/// <reference path="../../Scripts/typings/moment/moment.d.ts" />
/// <reference path="../../KJScripts/PagingTS.ts" />
/// <reference path="../Util/SelectOption.ts" />
class TicketIndexSearch {
    TicketReference: KnockoutObservable<string>;
    CustomerCode: KnockoutObservable<string>;
    CustomerName: KnockoutObservable<string>;
    FromDate: KnockoutObservable<Date>;
    ToDate: KnockoutObservable<Date>;
    RecordsInDays: KnockoutObservable<string>;

    constructor(data) {
        data = data || {};

        this.TicketReference = ko.observable(data.TicketRefernce);
        this.CustomerCode = ko.observable(data.CustomerCode);
        this.CustomerName = ko.observable(data.CustomerName);
        this.FromDate = ko.observable(data.FromDate);
        this.ToDate = ko.observable(data.ToDate);
        this.RecordsInDays = ko.observable(data.RecordsInDays);
    }
}

class TicketIndexPagingModel {
    Paging: KnockoutObservable<Paging>;
    TicketSearch: KnockoutObservable<TicketIndexSearch>;
    SubscriberTab: any;

    RecordsInDaysList: KnockoutObservableArray<SelectOption> = ko.observableArray([]);

    TabPanelName: KnockoutObservable<string>;
    TabPanelAlphaName: KnockoutComputed<string>;
    initDone: boolean;
    ChangeObservable: KnockoutComputed<void>;

    constructor(data, tabPanelName: string, subscriberTab) {
        data = data || {};

        this.Paging = ko.observable(new Paging(data.TicketEditModels, data.SearchObject));
        this.TicketSearch = ko.observable(new TicketIndexSearch(data.SearchObject));
        this.SubscriberTab = subscriberTab;

        this.RecordsInDaysList.push(new SelectOption("CURRENTMONTH", moment().format('MMMM')));
        this.RecordsInDaysList.push(new SelectOption("PREVIOUSMONTH", moment().subtract(30, 'days').format('MMMM')));
        this.RecordsInDaysList.push(new SelectOption("CURRENTYEAR", moment().format('YYYY')));
        this.RecordsInDaysList.push(new SelectOption("PREVIOUSYEAR", moment().subtract(1, 'year').format('YYYY')));

        this.TabPanelName = ko.observable(tabPanelName);
        this.TabPanelAlphaName = ko.computed({
            read: () => {
                return this.TabPanelName().replace(/[0-9]/g, '');
            }
        });

        this.initDone = false;
        this.ChangeObservable = ko.computed({
            read: () => {
                this.TicketSearch().TicketReference();
                this.TicketSearch().CustomerCode();
                this.TicketSearch().FromDate();
                this.TicketSearch().ToDate();
                this.TicketSearch().RecordsInDays();

                if (this.initDone) {
                    this.saveState(false);
                }
                else {
                    this.initDone = true;
                }
            }
        });
    }

    setStateOnServer = function (data: string, contentType: string, controllerState: string, initial: boolean, onSuccess) {
        var panelId: string = this.TabPanelName().match(/\d+/g).join([]);
        $.ajax({
            type: 'POST',
            url: "/api/TabPanel",
            dataType: 'application/json',
            data: {
                id: panelId, jsondata: data, panelname: this.TabPanelName(), content_type: contentType, controller_state: controllerState, initial: initial
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
        var data = ko.toJSON(this.TicketSearch());
        this.setStateOnServer(data, this.TabPanelAlphaName(), "Ticket", initial);
    }

    mapRestoredState = function (data) {
        this.initDone = false;
        this.TicketSearch(new TicketIndexSearch(data));
    }

    getCustomer(request, response) {
        var text = request.term;
        $.ajax({
            type: 'GET',
            url: '/api/Customer/AutoComplete/?search=' + text,
            data: {
                json: '{}',
                delay: 0.5,
                search: text

            },
            success: function (data) {
                response($.map(data, function (language) {
                    return {
                        languageValue: language.Id,
                        label: language.label
                    };
                }));
            }
        });
    };

    selectCustomer = function (event, ui) {
        (ui.item.languageValue);

        var vm = ko.dataFor(event.target);
        vm.TicketSearch().CustomerCode(ui.item.languageValue);
    };

    onCustomerChange = function (event, ui) {
        // if value is cleared, clear in ViewModel
        if (ui.item == null || ui.item.value == null || ui.item.value == '') {
            var vm = ko.dataFor(event.target);
            vm.TicketSearch().CustomerCode(undefined);
        }
    }

    search = function () {
        this.Paging().fetchEntitiesWithSearch("Ticket/Index", this.TicketSearch);
    }

    openCreateTicket = function () {
        var options = {
            PanelName: "CreateTicket",
        };
        this.SubscriberTab.notifySubscribers(options, "save");
    }

    openTicketDetails = function (id) {
        var options = {
            PanelName: "TicketDetails",
            UriParam: id
        };
        this.SubscriberTab.notifySubscribers(options, "save");
    }

    openTicketEdit = function (id) {
        var options = {
            PanelName: "TicketEdit",
            UriParam: id
        };
        this.SubscriberTab.notifySubscribers(options, "save");
    }
}
