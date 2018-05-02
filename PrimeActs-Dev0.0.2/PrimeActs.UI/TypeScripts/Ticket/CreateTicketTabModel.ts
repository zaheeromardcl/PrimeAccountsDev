/// <reference path="../../Scripts/typings/jquery/jquery.d.ts" />
/// <reference path="../../Scripts/typings/knockout/knockout.d.ts" />
/// <reference path="../../Scripts/typings/bootstrap-notify/bootstrap-notify.d.ts" />
/// <reference path="../Util/SelectOption.ts" />
/// <reference path="TicketViewModel.ts" />
class TicketCreateTabModel extends TicketViewModel {
    SelectedPaymentType: KnockoutObservable<string>;
    AmountReceived: KnockoutObservable<number>;

    TicketSubTotal: KnockoutComputed<number>;
    TicketTotalPorterage: KnockoutComputed<number>;
    TicketVATTotal: KnockoutComputed<number>;
    TicketTotalPrice: KnockoutComputed<number>;

    ShowErrors: KnockoutObservable<boolean>;
    Errors: KnockoutValidationErrors;

    constructor(data, ticketItems: ITicketItemModel[]) {
        super(data, ticketItems);

        this.SelectedPaymentType = ko.observable("");
        this.AmountReceived = ko.observable(data.AmountReceived)
            .extend({ numeric: 2 })
            .extend({ validation: { validator: this.amountReceivedRequired, params: this } });

        this.CurrencyID.extend({ validation: { validator: this.validateGuid } });
        this.CustomerID.extend({ validation: { validator: this.validateGuid } });
        this.SalesPersonUserID.extend({ validation: { validator: this.validateGuid } });
        this.PONumber.extend({ required: true });
        this.Notes.extend({ required: true });

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
        this.TicketTotalPorterage = ko.computed({
            read: () => {
                var ticketTotalPorterage: number = 0;
                var hasValue: boolean = false;
                for (var ticketItem of this.TicketItems()) {
                    if (ticketItem.TicketItemPorterageValue() != undefined) {
                        hasValue = true;
                        ticketTotalPorterage += ticketItem.TicketItemPorterageValue();
                    }
                }
                if (hasValue) {
                    return ticketTotalPorterage;
                }
                return undefined;
            }
        }).extend({ numeric: 2 });
        this.TicketTotalPrice = ko.computed({
            read: () => {
                if (this.TicketSubTotal() != undefined && this.TicketTotalPorterage() != undefined) {
                    var subTotal = this.TicketSubTotal();
                    var porterage = this.TicketTotalPorterage();
                    return subTotal + porterage;
                }
                return undefined;
            }
        }).extend({ numeric: 2 });
        this.TicketVATTotal = ko.computed({
            read: () => {
                if (this.TicketTotalPrice() != undefined) {
                    var vatRate = 0;
                    return this.TicketTotalPrice() * vatRate;
                }
                return undefined;
            }
        }).extend({ numeric: 2 });

        this.Errors = ko.validation.group(this);
        this.ShowErrors = ko.observable(false);
    }

    validateGuid = function (val) {
        return val != undefined && val != null && val != "" && val != "00000000-0000-0000-0000-000000000000";
    }

    amountReceivedRequired = function (val, observable) {
        if (observable.SelectedPaymentType() == "R") {
            return val != null;
        }
        return true;
    }

    serverErrors = ko.observableArray([]);
    showError = function (item) {
        if ((item == undefined || !item.isValid()) && this.ShowErrors()) {
            return true;
        }

        return false;
    }
}

class TicketItemCreateTabModel implements ITicketItemModel {
    TicketItemID: KnockoutObservable<string>;
    TicketID: KnockoutObservable<string>;
    DepartmentID: KnockoutObservable<string>;
    DepartmentName: KnockoutObservable<string>;
    TicketItemDescription: KnockoutObservable<string>;
    CurrencyAmount: KnockoutObservable<number>;
    TicketItemQuantity: KnockoutObservable<number>;
    TicketItemBrand: KnockoutObservable<string>;
    TicketItemWeight: KnockoutObservable<string>;
    TicketItemPorterageID: KnockoutObservable<string>;
    TicketItemMinPorterage: KnockoutObservable<number>;
    TicketItemPorterage: KnockoutObservable<number>;
    TicketItemSize: KnockoutObservable<number>;
    TicketItemUnitPrice: KnockoutObservable<number>;
    ConsignmentItemID: KnockoutObservable<string>;
    ConsignmentReference: KnockoutObservable<string>;
    ProduceID: KnockoutObservable<string>;
    Produce: KnockoutObservable<string>;
    ProduceDescription: KnockoutObservable<string>;
    SupplierID: KnockoutObservable<string>;
    SupplierName: KnockoutObservable<string>;
    OriginalTicketItemID: KnockoutObservable<string>;

    TicketItemPorterageValue: KnockoutComputed<number>;
    TicketItemTotalPrice: KnockoutComputed<number>;

    IsSaving: KnockoutObservable<boolean>;
    IsDirty: KnockoutObservable<boolean>;
    DirtyCalculations: KnockoutComputed<void>;

    FocusDepartment: KnockoutComputed<boolean>;

    // Properties to track if user has visited related input fields
    DepartmentVisited: KnockoutObservable<boolean>;
    ConsignmentItemVisited: KnockoutObservable<boolean>;
    QuantityVisited: KnockoutObservable<boolean>;
    UnitPriceVisited: KnockoutObservable<boolean>;

    DepartmentInputDone: KnockoutComputed<boolean>;
    ConsignmentItemInputDone: KnockoutComputed<boolean>;
    QuantityInputDone: KnockoutComputed<boolean>;
    UnitPriceInputDone: KnockoutComputed<boolean>;
    TicketItemInputDone: KnockoutComputed<boolean>;

    ShowErrors: KnockoutObservable<boolean>;
    Errors: KnockoutValidationErrors;

    constructor(data) {
        data = data || {};

        this.TicketItemID = ko.observable(data.TicketItemID);
        this.TicketID = ko.observable(data.TicketID);
        this.DepartmentID = ko.observable(data.DepartmentID).extend({ required: true });
        this.DepartmentName = ko.observable(data.DepartmentName);
        this.TicketItemDescription = ko.observable(data.TicketItemDescription);
        this.CurrencyAmount = ko.observable(data.CurrencyAmount);
        this.TicketItemQuantity = ko.observable(data.TicketItemQuantity).extend({ numeric: 2 }).extend({ required: true });
        this.TicketItemBrand = ko.observable(data.TicketItemBrand);
        this.TicketItemWeight = ko.observable(data.TicketItemWeight);
        this.TicketItemPorterageID = ko.observable(data.TicketItemPorterageID);
        this.TicketItemMinPorterage = ko.observable(data.TicketItemMinPorterage);
        this.TicketItemPorterage = ko.observable(data.TicketItemPorterage).extend({ numeric: 2 });
        this.TicketItemSize = ko.observable(data.TicketItemSize);
        this.TicketItemUnitPrice = ko.observable(data.TicketItemUnitPrice).extend({ numeric: 2 }).extend({ required: true });
        this.ConsignmentItemID = ko.observable(data.ConsignmentItemID).extend({ required: true });
        this.ConsignmentReference = ko.observable(data.ConsignmentReference);
        this.ProduceID = ko.observable(data.ProduceID);
        this.Produce = ko.observable(data.Produce);
        this.ProduceDescription = ko.observable(data.ProduceDescription);
        this.SupplierID = ko.observable(data.SupplierID);
        this.SupplierName = ko.observable(data.SupplierName);
        this.OriginalTicketItemID = ko.observable(data.OriginalTicketItemID);

        this.TicketItemPorterageValue = ko.computed({
            read: () => {
                if (this.TicketItemQuantity() != undefined && this.TicketItemPorterage() != undefined) {
                    var porterageValue = this.TicketItemQuantity() * this.TicketItemPorterage();
                    if (porterageValue < this.TicketItemMinPorterage()) {
                        porterageValue = this.TicketItemMinPorterage();
                    }
                    return porterageValue;
                }
                return undefined;
            }
        }).extend({ numeric: 2 });
        this.TicketItemTotalPrice = ko.computed({
            read: () => {
                if (this.TicketItemQuantity() != undefined && this.TicketItemUnitPrice() != undefined) {
                    return this.TicketItemQuantity() * this.TicketItemUnitPrice();
                }
                return undefined;
            }
        }).extend({ numeric: 2 });

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

        this.FocusDepartment = ko.computed({
            read: () => {
                return this.TicketItemID() == undefined;
            }
        });

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
                return this.ConsignmentItemID() != undefined || this.DepartmentVisited();
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

class CreateTicketTabViewModel {
    PaymentTypeList = ko.observableArray([]);
    TicketModel: KnockoutObservable<TicketCreateTabModel>;
    TabPanelName: KnockoutObservable<string>;
    TabPanelAlphaName: KnockoutComputed<string>;
    DoneInit: KnockoutObservable<boolean>;

    constructor(data, tabPanelName: string) {
        data = data || {};

        var ticketItemModels = [];
        if (data.TicketCreateModel.hasOwnProperty('TicketItems')) {
            for (var ticketItemEditModel of data.TicketCreateModel.TicketItems) {
                ticketItemModels.push(new TicketItemCreateTabModel(ticketItemEditModel));
            }
        }

        this.TicketModel = ko.observable(new TicketCreateTabModel(data.TicketCreateModel, ticketItemModels));
        for (var paymentType of data.PaymentTypeList) {
            this.PaymentTypeList.push(new SelectOption(paymentType.PaymentTypeName, paymentType.PaymentTypeCode));
            if (paymentType.Default) {
                this.TicketModel().SelectedPaymentType(paymentType.PaymentTypeCode);
            }
        }
        this.TabPanelName = ko.observable(tabPanelName);
        this.TabPanelAlphaName = ko.computed({
            read: () => {
                return this.TabPanelName().replace(/[0-9]/g, '');
            }
        });
        this.DoneInit = ko.observable(false);
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
        $.ajax({
            type: 'GET',
            url: "/api/TabPanel",
            dataType: 'json',
            data: { "id": panelId, "name": this.TabPanelName() },
            success: function (data) {
                var jsonData = JSON.parse(data);
                self.mapRestoredState(jsonData);
            },
            error: function (xhr) {
                console.log("failure in GET from State");
                console.log(xhr);
            }
        });
    }

    pageLoadState = function () {
        var data = ko.toJSON(this.TicketModel());
        this.setStateOnServer(data, this.TabPanelAlphaName(), "Create", true, function () {
            this.DoneInit(true);
            this.restoreState();
        });
    }

    saveState = function () {
        var data = ko.toJSON(this.TicketModel());
        this.setStateOnServer(data, this.TabPanelAlphaName(), "Create", true);
    }

    restoreState = function () {
        this.getState();
    }

    mapRestoredState = function (data) {
        var ticketItemModels = [];
        if (data.TicketCreateModel.hasOwnProperty('TicketItems')) {
            for (var ticketItemEditModel of data.TicketCreateModel.TicketItems) {
                ticketItemModels.push(new TicketItemCreateTabModel(ticketItemEditModel));
            }
        }

        this.TicketModel(new TicketCreateTabModel(data.TicketCreateModel, ticketItemModels));
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
        vm.TicketModel().CurrencyID(ui.item.languageValue);
        vm.TicketModel().CurrencyName(ui.item.label);
    };

    onCurrencyChange = function (event, ui) {
        // if value is cleared, clear in ViewModel
        if (ui.item == null || ui.item.value == null || ui.item.value == '') {
            var vm = ko.dataFor(event.target);
            vm.TicketModel().CurrencyID(undefined);
        }
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
        vm.TicketModel().CustomerID(ui.item.languageValue);
    };

    onCustomerChange = function (event, ui) {
        // if value is cleared, clear in ViewModel
        if (ui.item == null || ui.item.value == null || ui.item.value == '') {
            var vm = ko.dataFor(event.target);
            vm.TicketModel().CustomerID(undefined);
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
        vm.TicketModel().SalesPersonUserID(ui.item.languageValue);
        vm.TicketModel().SalesPersonName(ui.item.label);
    };

    onSalesPersonChange = function (event, ui) {
        // if value is cleared, clear in ViewModel
        if (ui.item == null || ui.item.value == null || ui.item.value == '') {
            var vm = ko.dataFor(event.target);
            vm.TicketModel().SalesPersonUserID(undefined);
            vm.TicketModel().SalesPersonName(undefined);
        }
    }

    createReceipt = function (data: CreateTicketViewModel) {
        data.TicketModel().serverErrors.removeAll();

        // Set cash sale
        data.TicketModel().IsCashSale(data.TicketModel().SelectedPaymentType() == "C");

        // Re-validate
        data.TicketModel().Errors = ko.validation.group(data.TicketModel());

        if (data.TicketModel().Errors().length == 0) {
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                url: '/api/Ticket/CreateReceiptTicket',
                data: ko.toJSON(data.TicketModel()),
                success: function (result) {
                    if (result.StatusId == 0) {
                        data.TicketModel().serverErrors.push(result.Message);
                        return;
                    }
                    else {
                        if ($("#btnShowResults").attr('aria-expanded') == 'false') {
                            $("#btnShowResults").click();
                        }
                    }
                }
            });
        }
        else {
            data.TicketModel().ShowErrors(true);
            data.TicketModel().Errors.showAllMessages(true);
        }
    }

    createTicket = function (data: CreateTicketViewModel) {
        data.TicketModel().serverErrors.removeAll();

        // Set cash sale
        var selectedPaymentType = data.TicketModel().SelectedPaymentType();
        data.TicketModel().IsCashSale(selectedPaymentType == "C");

        // Re-validate
        data.TicketModel().Errors = ko.validation.group(data.TicketModel());

        if (data.TicketModel().Errors().length == 0) {
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                url: '/api/Ticket/CreateTicket',
                data: ko.toJSON(data.TicketModel()),
                success: function (result) {
                    if (result.StatusId == 0) {
                        data.TicketModel().serverErrors.push(result.Message);
                        return;
                    }
                    else {
                        var ticketItems = [new TicketItemCreateTabModel({ TicketID: result.TicketID })];
                        var ticket = new TicketCreateTabModel(result, ticketItems);
                        ticket.SelectedPaymentType(selectedPaymentType);
                        data.TicketModel(ticket);
                        //if ($("#btnShowResults").attr('aria-expanded') == 'false') {
                        //    $("#btnShowResults").click();
                        //}
                    }
                }
            });
        }
        else {
            data.TicketModel().ShowErrors(true);
            data.TicketModel().Errors.showAllMessages(true);
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

    getProduce = function (request, ui) {
        var text = request.term;
        if (text === " ")
            return;
        if (text === "")
            return;
        $.ajax({
            type: 'GET',
            url: '/api/Produce/AutoCompletePC/?search=' + text,
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

        var vm = ko.dataFor(event.target);
        vm.ConsignmentItemID(valueArray[0]);
        vm.TicketItemPorterageID(valueArray[1]);
        vm.TicketItemPorterage(Number(valueArray[2]));
        vm.TicketItemMinPorterage(Number(valueArray[3]));
        vm.TicketItemBrand(valueArray[4]);
        vm.TicketItemSize(valueArray[6]);
        vm.TicketItemWeight(valueArray[5]);

        vm.Produce(labelArray[0]);
        vm.ProduceDescription(ui.item.label);
    };

    onProduceChange = function (event, ui) {
        // if value is cleared, clear in ViewModel
        if (ui.item == null || ui.item.value == null || ui.item.value == '') {
            var vm = ko.dataFor(event.target);
            vm.ConsignmentItemID(undefined);
            vm.TicketItemMinPorterage(undefined);
            vm.TicketItemPorterage(undefined);
            vm.TicketItemPorterageID(undefined);
            vm.TicketItemBrand(undefined);
            vm.TicketItemSize(undefined);
            vm.TicketItemWeight(undefined);

            vm.Produce(undefined);
        }
    }

    addNewTicketItem = function () {
        this.TicketModel().TicketItems.push(new TicketItemCreateTabModel({ TicketID: this.TicketModel().TicketID() }));
    }

    removeTicketItem = function (index: number) {
        if (this.TicketModel().TicketItems().length > 1) {
            this.TicketModel().TicketItems.splice(index, 1);
        }
    }

    onDepartmentFocusOut = function (index: number) {
        var ticketItem = <TicketItemCreateTabModel>(this.TicketModel().TicketItems()[index]);
        ticketItem.DepartmentVisited(true);
        this.saveTicketItem(this, ticketItem);
    }

    onConsignmentItemFocusOut = function (index: number) {
        var ticketItem = <TicketItemCreateTabModel>(this.TicketModel().TicketItems()[index]);
        ticketItem.ConsignmentItemVisited(true);
        this.saveTicketItem(this, ticketItem);
    }

    onQuantityFocusOut = function (index: number) {
        var ticketItem = <TicketItemCreateTabModel>(this.TicketModel().TicketItems()[index]);
        ticketItem.QuantityVisited(true);
        this.saveTicketItem(this, ticketItem);
    }

    onUnitPriceFocusOut = function (index: number) {
        var ticketItem = <TicketItemCreateTabModel>(this.TicketModel().TicketItems()[index]);
        ticketItem.UnitPriceVisited(true);
        this.saveTicketItem(this, ticketItem);
    }

    saveTicketItem = function (data: CreateTicketViewModel, ticketItem: TicketItemCreateTabModel) {
        if (ticketItem.IsSaving() || !ticketItem.IsDirty() || !ticketItem.TicketItemInputDone()) {
            return;
        }
        // Re-validate
        ticketItem.Errors = ko.validation.group(ticketItem);

        if (ticketItem.Errors().length == 0) {
            ticketItem.IsSaving(true);
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                url: '/api/Ticket/SaveTicketItem/',
                data: ko.toJSON(ticketItem),
                success: function (result) {
                    var createdTicketItem = new TicketItemCreateTabModel(result);
                    data.TicketModel().TicketItems.replace(ticketItem, createdTicketItem);
                }
            });
        }
        else {
            ticketItem.ShowErrors(true);
            ticketItem.Errors.showAllMessages(true);
        }
    }

    saveTicket = function (data: CreateTicketViewModel) {
        var totalErrorCount = 0;

        var ticketItemsCount = data.TicketModel().TicketItems().length;
        for (var i = ticketItemsCount - 1; i >= 0; i--) {
            var ticketItem = <TicketItemCreateTabModel>data.TicketModel().TicketItems()[i];
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
            var promise = $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                url: '/api/Ticket/SaveTicket',
                data: ko.toJSON(data.TicketModel()),
                success: function (result) {
                },
                error: function (jqXHR, textStatus, errorThrown) {
                }
            });

            promise.done(function (data) {
                var id = data.TicketID;
                $.ajax({
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    url: '/api/Ticket/PrintReceipt/' + id,
                    success: function (result) {
                        $.notify({
                            message: "Print request successfully submitted."
                        }, {
                                type: "info",
                                delay: 3000,
                            });
                        window.location.href = '../Ticket/Details/' + id;
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        $.notify({
                            message: "Error submitting print request."
                        }, {
                                type: "danger",
                                delay: 3000
                            });
                        var receiptViewModel = jqXHR.responseJSON;
                    }
                });
            });
        }
    }
}
