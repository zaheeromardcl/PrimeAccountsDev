/// <reference path="../../Scripts/typings/knockout/knockout.d.ts" />
/// <reference path="../../Scripts/typings/knockout.validation/knockout.validation.d.ts" />
/// <reference path="../Util/SelectOption.ts" />
/// <reference path="../Ticket/TicketViewModel.ts" />
/// <reference path="../Ticket/CreateTicketViewModel.ts" />

class TransferCreateViewModel extends TicketViewModel {
    TicketSubTotal: KnockoutComputed<number>;

    FocusSalesPerson: KnockoutComputed<boolean>;
    CurrentRowIndex: KnockoutObservable<number>;

    ShowErrors: KnockoutObservable<boolean>;
    Errors: KnockoutValidationErrors;
    
    constructor(data, ticketItems: ITicketItemModel[]) {
        super(data, ticketItems);
        
        this.TicketReference.extend({ required: { message: " " } });
        this.CurrencyID.extend({ validation: { validator: this.validateGuid } });
        this.SalesPersonUserID.extend({ validation: { validator: this.validateGuid } });

        this.TicketSubTotal = ko.computed({
            read: () => {
                var ticketSubTotal: number = 0;
                var hasValue: boolean = false;
                for (var ticketItem of this.TicketItems()) {
                    if (ticketItem.TicketItemTotalPrice() != undefined) {
                        hasValue = true;
                        ticketSubTotal += ticketItem.TicketItemTotalPrice();
                    }
                }
                if (hasValue) {
                    return ticketSubTotal;
                }
                return undefined;
            }
        }).extend({ numeric: 2 });

        this.FocusSalesPerson = ko.computed({
            read: () => {
                return !this.validateGuid(this.SalesPersonUserID());
            }
        });

        this.Errors = ko.validation.group(this);
        this.ShowErrors = ko.observable(false);
    }

    validateGuid = function (val) {
        if (val == undefined || val == null || val == "" || val == "00000000-0000-0000-0000-000000000000")
            return false;
        return true;
    }

    serverErrors = ko.observableArray([]);
    showError = function (item) {
        if ((item == undefined || !item.isValid()) && this.ShowErrors()) {
            return true;
        }

        return false;
    }

    showRequired = function (item, field) {
        ko.validation.validateObservable(item);
        if (!this.showError(item)) {
            return item == undefined || !item.isValid();
        }
        return false;
    }

    showGuidFieldRequired = function (item) {
        return !this.validateGuid(item());
    }
}

class TransferItemCreateViewModel implements ITicketItemModel {
    TicketItemID: KnockoutObservable<string>;
    TicketID: KnockoutObservable<string>;
    TransferTypeID: KnockoutObservable<string>;
    DepartmentID: KnockoutObservable<string>;
    DepartmentName: KnockoutObservable<string>;
    ConsignmentItemID: KnockoutObservable<string>;    
    TicketItemDescription: KnockoutObservable<string>;   
    TicketItemQuantity: KnockoutObservable<number>;
    TicketItemUnitPrice: KnockoutObservable<number>;

    ProduceDescription: KnockoutObservable<string>;

    TicketItemPorterageValue: KnockoutComputed<number>;
    TicketItemTotalPrice: KnockoutComputed<number>;

    FocusDepartment: KnockoutComputed<boolean>;
    FocusProduce: KnockoutComputed<boolean>;

    IsSaving: KnockoutObservable<boolean>;
    IsDirty: KnockoutObservable<boolean>;
    DirtyCalculations: KnockoutComputed<void>;

    // Properties to track if user has visited related input fields
    DepartmentVisited: KnockoutObservable<boolean>;
    ConsignmentItemVisited: KnockoutObservable<boolean>;
    QuantityVisited: KnockoutObservable<boolean>;
    UnitPriceVisited: KnockoutObservable<boolean>;

    DepartmentInputDone: KnockoutComputed<boolean>;
    ConsignmentItemInputDone: KnockoutComputed<boolean>;
    QuantityInputDone: KnockoutComputed<boolean>;
    UnitPriceInputDone: KnockoutComputed<boolean>;
    TicketItemInputDone: KnockoutComputed<boolean>
    IncludeZeroCheckBox: KnockoutObservable<boolean>;

    ShowErrors: KnockoutObservable<boolean>;
    Errors: KnockoutValidationErrors;

    constructor(data) {
        data = data || {};

        this.TicketItemID = ko.observable(data.TicketItemID);
        this.TicketID = ko.observable(data.TicketID);
        this.TransferTypeID = ko.observable(data.TransferTypeID);
        this.DepartmentID = ko.observable(data.DepartmentID).extend({ required: true });
        this.DepartmentName = ko.observable(data.DepartmentName);
        this.TicketItemDescription = ko.observable(data.TicketItemDescription);
        this.TicketItemQuantity = ko.observable(data.TicketItemQuantity || 1).extend({ numeric: 2 }).extend({ required: true });
        this.TicketItemUnitPrice = ko.observable(data.TicketItemUnitPrice).extend({ numeric: 2 }).extend({ required: true });
        this.ConsignmentItemID = ko.observable(data.ConsignmentItemID).extend({ required: true });
        this.ProduceDescription = ko.observable(data.ProduceDescription);
        this.IncludeZeroCheckBox = ko.observable(false);

        this.TicketItemPorterageValue = ko.computed({
            read: () => {
                return 0;
            }
        });

        this.TicketItemTotalPrice = ko.computed({
            read: () => {
                if (this.TicketItemQuantity() != undefined && this.TicketItemUnitPrice() != undefined) {
                    return this.TicketItemQuantity() * this.TicketItemUnitPrice();
                }
                return undefined;
            }
        }).extend({ numeric: 2 });

        this.FocusDepartment = ko.computed({
            read: () => {
                return this.TicketItemID() == undefined && this.DepartmentID() == undefined;
            }
        });

        this.FocusProduce = ko.computed({
            read: () => {
                return !this.FocusDepartment() && this.ConsignmentItemID() == undefined;
            }
        });

        this.IsSaving = ko.observable(false);
        this.IsDirty = ko.observable(false);
        this.DirtyCalculations = ko.computed({
            read: () => {
                this.DepartmentID();
                this.ConsignmentItemID();
                this.TicketItemQuantity();
                this.TicketItemUnitPrice();

                this.IsDirty(true);
            }
        });
        this.resetDirtyFlag();

        this.DepartmentVisited = ko.observable(false);
        this.ConsignmentItemVisited = ko.observable(false);
        this.QuantityVisited = ko.observable(false);
        this.UnitPriceVisited = ko.observable(false);

        this.DepartmentInputDone = ko.computed({
            read: () => {
                return this.DepartmentID() != undefined || this.DepartmentVisited();
            }
        });
        this.ConsignmentItemInputDone = ko.computed({
            read: () => {
                return this.ConsignmentItemID() != undefined || this.ConsignmentItemVisited();
            }
        });
        this.QuantityInputDone = ko.computed({
            read: () => {
                return this.TicketItemQuantity() != undefined || this.QuantityVisited();
            }
        });
        this.UnitPriceInputDone = ko.computed({
            read: () => {
                return this.TicketItemUnitPrice() != undefined || this.UnitPriceVisited();
            }
        });

        this.TicketItemInputDone = ko.computed({
            read: () => {
                return this.DepartmentInputDone() && this.ConsignmentItemInputDone() && this.QuantityInputDone() && this.UnitPriceInputDone();
            }
        });

        this.Errors = ko.validation.group(this);
        this.ShowErrors = ko.observable(false);
    }

    resetDirtyFlag = function () {
        this.IsDirty(false);
    }

    serverErrors = ko.observableArray([]);
    showError = function (item) {
        if ((item == undefined || !item.isValid()) && this.ShowErrors()) {
            return true;
        }

        return false;
    }
}

class CreateTransferPageViewModel {
    UseLookupTables: LookupTables;
    TransferTypeList = ko.observableArray([]);
    TicketModel: KnockoutObservable<TransferCreateViewModel>;
    TransferResult: KnockoutObservable<TransferResultModel>;

    TabPanelName: KnockoutObservable<string>;
    TabPanelAlphaName: KnockoutComputed<string>;

    initDone: boolean;
    ChangeObservable: KnockoutComputed<void>;

    TabContext: KnockoutObservable<AppUserContextModel>;

    constructor(data, tabPanelName, lookupTables: LookupTables) {
        data = data || {};

        this.UseLookupTables = lookupTables;
        this.TabContext = ko.observable(new AppUserContextModel(data.UserContextModel, this.UseLookupTables));

        var ticketItemModels = [];
        if (data.TransferCreateModel.hasOwnProperty('TicketItems')) {
            for (var ticketItemEditModel of data.TransferCreateModel.TicketItems) {
                ticketItemModels.push(new TransferItemCreateViewModel(ticketItemEditModel));
            }
        }

        this.TicketModel = ko.observable(new TransferCreateViewModel(data.TransferCreateModel, ticketItemModels));
        for (var transferType of data.TransferTypeList) {
            this.TransferTypeList.push(new SelectOption(transferType.TransferTypeName, transferType.TransferTypeID));
        }
        this.TransferResult = ko.observable(new TransferResultModel(null));

        this.TabPanelName = ko.observable(tabPanelName);
        this.TabPanelAlphaName = ko.computed({
            read: () => {
                return this.TabPanelName().replace(/[0-9]/g, '');
            }
        });

        this.initDone = false;
        this.ChangeObservable = ko.computed({
            read: () => {
                this.TicketModel().TicketReference();
                this.TicketModel().TicketDate();
                this.TicketModel().CurrencyID();
                this.TicketModel().CustomerDepartmentID();
                this.TicketModel().SalesPersonUserID();
                this.TicketModel().PONumber();
                this.TicketModel().Notes();

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
        var data = ko.toJSON(this.TicketModel());
        this.setStateOnServer(data, this.TabPanelAlphaName(), "Create", initial);
    }

    mapRestoredState = function (data) {
        var ticketItemModels = [];
        if (data.hasOwnProperty('TicketItems')) {
            for (var ticketItemEditModel of data.TicketItems) {
                ticketItemModels.push(new TransferItemCreateViewModel(ticketItemEditModel));
            }
        }

        this.initDone = false;
        this.TicketModel(new TransferCreateViewModel(data, ticketItemModels));
    }

    getCurrency = function (request, ui) {
        var text = request.term;
        //if (text === " ")
        //    return;
        //if (text === "")
        //    return;
        $.ajax({
            type: 'GET',
            url: '/API/Currency/AutoComplete/?search=' + text,
            data: {
                json: '{}',
                delay: 0.5,
                search: text

            },
            success: function (data) {
                if (data == null)
                    return;
                ui($.map(data, function (language) {
                    return {
                        languageValue: language.Id,
                        label: language.label
                    };
                }));
            }
        });
    };

    selectCurrency = function (event, ui) {
        (ui.item.languageValue);

        var vm = ko.dataFor(event.target);
        vm.TicketModel().CurrencyName(ui.item.label);
        vm.TicketModel().CurrencyID(ui.item.languageValue);
    };

    onCurrencyChange = function (event, ui) {
        // if value is cleared, clear in ViewModel
        if (ui.item == null || ui.item.value == null || ui.item.value == '') {
            var vm = ko.dataFor(event.target);
            vm.TicketModel().CurrencyID(undefined);
        }
    }

    //get sales person id
    getSalesPerson = function (request, response) {
        var text = request.term;
        if (text === " ")
            return;
        if (text === "")
            return;
        $.ajax({
            type: 'GET',
            url: '/api/SalesPerson/AutoComplete/?search=' + text,
            data: {
                json: '{}',
                delay: 0.5,
                search: text
            },
            success: function (data) {
                if (data == null)
                    return;
                response($.map(data, function (language) {
                    return {
                        languageValue: language.value,
                        label: language.label
                    };
                }));
            }
        });
    };

    selectSalesPerson = function (event, ui) {
        (ui.item.languageValue);

        var vm = ko.dataFor(event.target);
        vm.TicketModel().SalesPersonName(ui.item.label);
        vm.TicketModel().SalesPersonUserID(ui.item.languageValue);
    };

    onSalesPersonChange = function (event, ui) {
        // if value is cleared, clear in ViewModel
        if (ui.item == null || ui.item.value == null || ui.item.value == '') {
            var vm = ko.dataFor(event.target);
            vm.TicketModel().SalesPersonUserID(undefined);
        }
    }

    getDepartment = function (request, ui) {
        var text = request.term;
        //if (text === " ")
        //    return;
        //if (text === "")
        //    return;
        $.ajax({
            type: 'GET',
            url: '/API/Department/AutoComplete/?search=' + text,
            data: {
                json: '{}',
                delay: 0.5,
                search: text

            },
            success: function (data) {
                if (data == null)
                    return;
                ui($.map(data, function (language) {
                    return {
                        languageValue: language.Id,
                        label: language.label
                    };
                }));
            }
        });
    }

    selectDepartment = function (event, ui) {
        (ui.item.languageValue);

        var vm = ko.dataFor(event.target);
        vm.DepartmentID(ui.item.languageValue);
        vm.DepartmentName(ui.item.label);
    }

    onDepartmentChange = function (event, ui) {
        // if value is cleared, clear in ViewModel
        if (ui.item == null || ui.item.value == null || ui.item.value == '') {
            var vm = ko.dataFor(event.target);
            vm.DepartmentID(undefined);
        }
    }

    getProduce = function (request, response) {
        var self = this;
        
        var term = request.term;
        if (term === " ")
            return;
        $.ajax({
            type: 'GET',
            url: '/api/Produce/AutoCompletePC/?search=' + term,
            data: {
                json: '{}',
                delay: 0.5,
                search: term
            },
            success: function (data) {
                if (data == null)
                    return;
                response($.map(data, function (language) {
                    return {
                        languageValue: language.value,
                        label: language.label
                    };
                }));
            }
        });
    }

    getProduceToggleZero = function (request, response) {
        var self = this;
       
        var term = request.term;
        if (term === " ")
            return;
        $.ajax({
            type: 'GET',
            url: '/api/Produce/AutoCompletePCZero/?search=' + term,
            data: {
                json: '{}',
                delay: 0.5,
                search: term
            },
            success: function (data) {
                if (data == null)
                    return;
                response($.map(data, function (language) {
                    return {
                        languageValue: language.value,
                        label: language.label
                    };
                }));
            }
        });
    }

    selectProduce = function (event, ui) {
        (ui.item.languageValue);

        var valueArray = ui.item.languageValue.split(",");
        var labelArray = ui.item.label.split("-");

        // Set value in ViewModel
        var vm = <TransferItemCreateViewModel>ko.dataFor(event.target);
        vm.ConsignmentItemID(valueArray[0]);

        vm.ProduceDescription(ui.item.label);

        var description = valueArray[4] + " " + labelArray[0] + " " + valueArray[5] + " " + valueArray[6];
        vm.TicketItemDescription(description);
    }

    onProduceChange = function (event, ui) {
        // if value is cleared, clear in ViewModel
        if (ui.item == null || ui.item.value == null || ui.item.value == '') {
            var vm = <TransferItemCreateViewModel>ko.dataFor(event.target);
            vm.ConsignmentItemID(undefined);
            vm.ProduceDescription(undefined);
            vm.TicketItemDescription(undefined);
        }
    }

    createTicket = function (data: CreateTransferPageViewModel) {
        data.TicketModel().serverErrors.removeAll();

        // Re-validate
        data.TicketModel().Errors = ko.validation.group(data.TicketModel());
        if (data.TicketModel().Errors().length == 0) {
            //debugger;
            var promise = $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                url: '/api/Ticket/CreateTransferTicket',
                data: ko.toJSON(data.TicketModel())
            });

            promise.done(function (result) {
                var ticketItems = [];
                var ticket = new TransferCreateViewModel(result, ticketItems);
                this.initDone = false;
                data.TicketModel(ticket);
                data.addNewTicketItem();
            });

            promise.fail(function (jqXHR, textStatus, errorThrown) {
                var result = JSON.parse(jqXHR.responseText);
                data.TicketModel().serverErrors.push(result.Message);
            });
        }
        else {
            data.TicketModel().ShowErrors(true);
            data.TicketModel().Errors.showAllMessages(true);
        }
    }

    //create = function (data: TransferModel) {
        
    //    data.Transfer().serverErrors.removeAll();

    //    var transferTypeName;
    //    for (var transferType of this.TransferTypeList()) {
    //        if (transferType.value == data.Transfer().TransferType()) {
    //            transferTypeName = transferType.text;
    //            break;
    //        }
    //    }

    //    // Re-validate
    //    data.Transfer().Errors = ko.validation.group(data.Transfer());

    //    if (data.Transfer().Errors().length == 0) {
    //        $.ajax({
    //            type: 'POST',
    //            contentType: 'application/json; charset=utf-8',
    //            url: '/api/Ticket/CreateTransferTicket',
    //            data: ko.toJSON(data.Transfer()),
    //            success: function (result) {
    //                if (result.StatusId == 0) {
    //                    data.Transfer().serverErrors.push(result.Message);
    //                    return;
    //                }
    //                else {
    //                    var transferResult = new TransferResultModel({
    //                        TransferTypeCode: data.Transfer().TransferType(),
    //                        TransferTypeName: transferTypeName,
    //                        TicketEditModel: result.TicketEditModel,
    //                        ConsignmentReference: data.Transfer().ConsignmentReference(),
    //                        TargetConsignmentReference: data.Transfer().TargetConsignmentReference(),
    //                        Quantity: data.Transfer().Quantity(),
    //                        UnitCost: data.Transfer().UnitCost()
    //                    });

    //                    data.Transfer(new TransferCreateViewModel({
    //                        TransferType: "R",
    //                        ConsignmentItemId: null,
    //                        Quantity: null,
    //                        TargetConsignmentItemId: null,
    //                        UnitCost: null,
    //                        ConsignmentReference: null,
    //                        TargetConsignmentReference: null
    //                    }));
    //                    $('#ConsignmentItemId').val('');
    //                    $('#TargetConsignmentItemId').val('');

    //                    data.TransferResult(transferResult);
    //                    if ($("#btnShowResults").attr('aria-expanded') == 'false') {
    //                        $("#btnShowResults").click();
    //                    }
    //                }
    //            }
    //        });
    //    }
    //    else {
    //        data.Transfer().ShowErrors(true);
    //        data.Transfer().Errors.showAllMessages(true);
    //    }
    //}
    addNewTicketItem = function () {
        this.TicketModel().TicketItems.push(new TransferItemCreateViewModel({
            TicketID: this.TicketModel().TicketID(),
            DepartmentID: this.TicketModel().SalesPersonDepartmentID(),
            DepartmentName: this.TicketModel().SalesPersonDepartmentName()
        }));
        this.saveState(false);
    }

    removeTicketItem = function (index: number) {
        var ticketItem = this.TicketModel().TicketItems.splice(index, 1)[0];
        if (ticketItem.TicketItemID() !== undefined) {
            var self = this;
            var promise = $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                url: '/api/Ticket/RemoveTicketItem/' + ticketItem.TicketItemID()
            });
            promise.done(function (result) {
                if (self.TicketModel().TicketItems().length == 0) {
                    self.addNewTicketItem();
                }
                else {
                    self.saveState(false);
                }
            });
            promise.fail(function (jqXHR, textStatus, errorThrown) {
                // failed to mark, re-add to Ticket
                this.TicketModel().TicketItems.push(ticketItem);
            });
        }
        else if (this.TicketModel().TicketItems().length == 0) {
            this.addNewTicketItem();
        }
        else {
            this.saveState(false);
        }
    }

    onDepartmentFocusOut = function (index: number) {
        var ticketItem = <TransferItemCreateViewModel>(this.TicketModel().TicketItems()[index]);
        ticketItem.DepartmentVisited(true);
        this.saveTicketItem(this, ticketItem);
    }

    onConsignmentItemFocusOut = function (index: number) {
        var ticketItem = <TransferItemCreateViewModel>(this.TicketModel().TicketItems()[index]);
        ticketItem.ConsignmentItemVisited(true);
        this.saveTicketItem(this, ticketItem);
    }

    onQuantityFocusOut = function (index: number) {
        var ticketItem = <TransferItemCreateViewModel>(this.TicketModel().TicketItems()[index]);
        ticketItem.QuantityVisited(true);
        this.saveTicketItem(this, ticketItem);
    }

    onUnitPriceFocusOut = function (index: number) {
        var ticketItem = <TransferItemCreateViewModel>(this.TicketModel().TicketItems()[index]);
        ticketItem.UnitPriceVisited(true);
        this.saveTicketItem(this, ticketItem);
    }

    saveTicketItem = function (data: CreateTicketViewModel, ticketItem: TransferItemCreateViewModel) {
        if (ticketItem.IsSaving() || !ticketItem.IsDirty() || !ticketItem.TicketItemInputDone()) {
            return;
        }
        // Re-validate
        ticketItem.Errors = ko.validation.group(ticketItem);

        if (ticketItem.Errors().length == 0) {
            ticketItem.IsSaving(true);
            var promise = $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                url: '/api/Ticket/SaveTicketItem/',
                data: ko.toJSON(ticketItem)
            });

            promise.done(function (result) {
                var createdTicketItem = new TransferItemCreateViewModel(result);
                data.TicketModel().TicketItems.replace(ticketItem, createdTicketItem);
                data.saveState(false);
            });

            promise.fail(function (jqXHR, textStatus, errorThrown) {
                //var result = JSON.parse(jqXHR.responseText);
                //data.TicketModel().serverErrors.push(result.Message);
            });
        }
        else {
            ticketItem.ShowErrors(true);
            ticketItem.Errors.showAllMessages(true);
        }
    }

    saveTicket = function (data: CreateTransferPageViewModel, subscriberReplaceTab) {
        var totalErrorCount = 0;

        var ticketItemsCount = data.TicketModel().TicketItems().length;
        for (var i = ticketItemsCount - 1; i >= 0; i--) {
            var ticketItem = <TransferItemCreateViewModel>data.TicketModel().TicketItems()[i];
            if (i == ticketItemsCount - 1   // last item
                && ticketItemsCount > 1     // ticket has more than 1 ticket items, and its not edited
                && !ticketItem.IsDirty()) {
                continue;
            }

            ticketItem.Errors = ko.validation.group(ticketItem);
            var errorCount = ticketItem.Errors().length;
            if (errorCount != 0) {
                ticketItem.ShowErrors(true);
                ticketItem.Errors.showAllMessages(true);
            }
            totalErrorCount += errorCount;
        }

        if (totalErrorCount == 0) {
            var ticketSubTotal = data.TicketModel().TicketSubTotal();
            if (ticketSubTotal !== 0) {
                var currencySymbol = "£";
                bootbox.alert({
                    title: "Error",
                    message: "Transfer ticket total is not 0 (" + currencySymbol + ticketSubTotal.toFixed(2) + "). Please add/update items to fix total to save ticket.",
                    closeButton: false
                });
            }
            else {
                data.saveTicketWorker(data, subscriberReplaceTab);
            }
        }
    }

    saveTicketWorker = function (data: CreateTransferPageViewModel, subscriberReplaceTab) {
        var promise = $.ajax({
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            url: '/api/Ticket/SaveTicket',
            data: ko.toJSON(data.TicketModel())
        });

        var donePromise = promise.then(function (result) {
            if (result.StatusId == 0) {
                data.TicketModel().serverErrors.push(result.Message);
                return $.Deferred().reject();
            }
            else {
                return result.TicketID;
            }
        });
        promise.fail(function (jqXHR, textStatus, errorThrown) {
             
        });

        donePromise.then(function (id) {
            data.redirect(data, id, "TransferDetails", subscriberReplaceTab);
        });
    }

    redirect = function (data, id, redirectTarget, subscriberReplaceTab) {
        subscriberReplaceTab.notifySubscribers({
            PanelName: data.TabPanelName(),
            NewPanelName: redirectTarget,
            UriParam: id
        }, "save");
    }
}

class TransferResultModel {
    TransferTypeName: KnockoutObservable<string>;
    TicketViewModel: KnockoutObservable<TicketViewModel>;
    Message: KnockoutComputed<string>;

    TransferTypeCode: string;
    ConsignmentReference: string;
    TargetConsignmentReference: string;
    Item: number;
    Quantity: number;
    UnitCost: number;

    constructor(data: any) {
        data = data || {};

        this.TransferTypeName = ko.observable(data.TransferTypeName);

        var ticketItemModels = [];
        if (data.hasOwnProperty('TicketEditModel') && data.TicketEditModel.hasOwnProperty('TicketItems')) {
            for (var ticketItemEditModel of data.TicketEditModel.TicketItems) {
                ticketItemModels.push(new TicketItemViewModel(ticketItemEditModel));
            }
        }
        this.TicketViewModel = ko.observable(new TicketViewModel(data.TicketEditModel, ticketItemModels));

        this.TransferTypeCode = data.TransferTypeCode;
        this.ConsignmentReference = data.ConsignmentReference;
        this.TargetConsignmentReference = data.TargetConsignmentReference;
        this.Item = data.TicketEditModel == undefined ? "" :
            data.TicketEditModel.TicketItems[0].TicketItemDescription;
        this.Quantity = data.Quantity;
        this.UnitCost = data.UnitCost;

        this.Message = ko.computed({
            read: () => {
                var message = "";
                switch (this.TransferTypeCode) {
                    case "R":
                        message = this.Quantity + " x "+ this.Item + " have successfully been removed from consignment ref: " + this.ConsignmentReference;
                        break;
                    case "M":
                        message = this.Quantity + " x " + this.Item + " priced £" + this.UnitCost + " have been moved from consignment ref: " + this.ConsignmentReference + " to consignment ref: " + this.TargetConsignmentReference;
                        break;
                }
                return message;
            }
        });
    }
}