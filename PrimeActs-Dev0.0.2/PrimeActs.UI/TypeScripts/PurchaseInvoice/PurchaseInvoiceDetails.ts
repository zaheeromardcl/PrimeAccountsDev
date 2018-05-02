/// <reference path="../util/addressmodel.ts" />
/// <reference path="../util/mycustomoption.ts" />

class PurchaseInvoiceDetailViewModel {
    PurchaseInvoiceId: KnockoutObservable<string>;
    PurchaseInvoiceReference: KnockoutObservable<string>;
    PurchaseInvoiceDate: KnockoutObservable<string>;
    SupplierDepartmentName: KnockoutObservable<string>;
    SupplierCode: KnockoutObservable<string>;
    SupplierDepartmentID: KnockoutObservable<string>;
    Address: KnockoutObservable<AddressModel>;
    PurchaseInvoiceItems: KnockoutObservableArray<PurchaseInvoiceItemViewModel>;
    PurchaseInvoiceItemsSundry: KnockoutObservableArray<PurchaseInvoiceItemSundryViewModel>;
    HasItems: KnockoutObservable<boolean>;
    HasItemsSundry: KnockoutObservable<boolean>;
    Subtotal: KnockoutComputed<number>;
    Total: KnockoutObservable<number>;
    TotalOfItems: KnockoutComputed<number>;
    VAT: KnockoutObservable<number>;
    PreviousSupplierDepartmentID: KnockoutObservable<string>;
    PreviousSupplierDepartmentName: KnockoutObservable<string>;
    CreatedBy: KnockoutObservable<string>;
    CreatedDate: KnockoutObservable<string>;
    UpdatedBy: KnockoutObservable<string>;
    UpdatedDate: KnockoutObservable<string>;
    IsSaved: KnockoutObservable<boolean>;

    FocusAddItems: KnockoutObservable<boolean>;
    PurchaseInvoiceDateDirtyCalculations: KnockoutComputed<void>;

    IsSaving: KnockoutObservable<boolean>;
    IsDirty: KnockoutObservable<boolean>;
    DirtyCalculations: KnockoutComputed<void>;

    SupplierHasFocus: KnockoutObservable<boolean>;

    ShowErrors: KnockoutObservable<boolean>;
    Errors: KnockoutValidationErrors;

    fileName: KnockoutObservable<string>;

    Files: KnockoutObservable<FormData>;

    UploadFolder: KnockoutObservable<string>;
    UploadedFileNames: KnockoutObservableArray<MyCustomOption>;
    UploadedFileNamesDeleted: KnockoutObservableArray<MyCustomOption>;

    Created: KnockoutObservable<boolean>;

    AttachFilesVisible: KnockoutObservable<boolean>;
    NoMoreAttachments: KnockoutObservable<boolean>;

    Status: KnockoutObservable<string>;

    SupplierInvoiceAmount: KnockoutObservable<number>;

    ChargesIsVisible: KnockoutObservable<boolean>;

    NoteText: KnockoutObservable<string>;

    Consignments: KnockoutObservable<any>;

    SubscriberTab: any;

    isFresh: boolean;

    purchaseInvoiceType: KnockoutObservable<string>;

    constructor(data, subscriberTab) {
        data = data || {};

        this.purchaseInvoiceType = ko.observable(data.purchaseInvoiceType || "Consignment");

        this.ChargesIsVisible = ko.observable(false);
        this.PurchaseInvoiceReference = ko.observable(data.PurchaseInvoiceReference).extend({ required: true });
        //debugger;
        //var month = new Date().getMonth() + 1;
        //var date = new Date().getDate() + "/" + month + "/" + new Date().getFullYear();

        this.fileName = ko.observable("");
        this.Files = ko.observable(undefined);
        this.UploadFolder = ko.observable(data.UploadFolder);
        this.UploadedFileNames = ko.observableArray([]);
        this.UploadedFileNamesDeleted = ko.observableArray([]);
        this.Created = ko.observable(false);
        this.AttachFilesVisible = ko.observable(false);
        this.NoMoreAttachments = ko.observable(false);

        this.Status = ko.observable(data.Status);
        this.SupplierInvoiceAmount = ko.observable(data.SupplierInvoiceAmount || 0.0).extend({ numeric: 2 }).extend({ required: true });

        this.SupplierCode = ko.observable(data.SupplierCode);

        this.PurchaseInvoiceDate = ko.observable(data.PurchaseInvoiceDate);
        this.SupplierDepartmentName = ko.observable(data.SupplierDepartmentName);
        this.SupplierDepartmentID = ko.observable(data.SupplierDepartmentID).extend({ required: true });;
        this.PurchaseInvoiceId = ko.observable(data.PurchaseInvoiceId);
        this.Address = ko.observable(new AddressModel(data.Address));
        this.PurchaseInvoiceItems = ko.observableArray<PurchaseInvoiceItemViewModel>([]);
        this.HasItems = ko.computed({ read: () => { return this.PurchaseInvoiceItems().length > 0; } });
        this.PurchaseInvoiceItemsSundry = ko.observableArray<PurchaseInvoiceItemSundryViewModel>([]);
        this.HasItemsSundry = ko.computed({ read: () => { return this.PurchaseInvoiceItemsSundry().length > 0; } });
        this.VAT = ko.observable(data.VAT || 0.0).extend({ numeric: 2 });

        this.PreviousSupplierDepartmentID = ko.observable(data.SupplierDepartmentID);
        this.PreviousSupplierDepartmentName = ko.observable(data.SupplierDepartmentName);
        this.CreatedBy = ko.observable(data.CreatedBy);
        this.CreatedDate = ko.observable(data.CreatedDate);
        this.UpdatedBy = ko.observable(data.UpdatedBy);
        this.UpdatedDate = ko.observable(data.UpdatedDate);
        this.NoteText = ko.observable(data.NoteText);

        this.Consignments = ko.observable(data.Consignments || []);

        this.SubscriberTab = subscriberTab;

        this.IsSaved = ko.observable(data.IsSaved);
        if (data.PurchaseInvoiceItems != undefined) {
            if (data.PurchaseInvoiceItems.length > 0)
                this.Created(true);
            for (var i = 0; i < data.PurchaseInvoiceItems.length; i++) {
                this.PurchaseInvoiceItems.push(new PurchaseInvoiceItemViewModel(data.PurchaseInvoiceItems[i]));
            }
        }

        this.isFresh = this.PurchaseInvoiceItems().length === 0;

        this.IsSaving = ko.observable(false);
        this.IsDirty = ko.observable(false);
        this.DirtyCalculations = ko.computed({
            read: () => {
                this.SupplierDepartmentID();
                this.SupplierDepartmentName();
                this.PurchaseInvoiceDate();
                this.PurchaseInvoiceReference();
                this.SupplierInvoiceAmount();

                this.IsDirty(true);
            }
        });

        this.FocusAddItems = ko.observable(false);
        this.PurchaseInvoiceDateDirtyCalculations = ko.computed({
            read: () => {
                this.PurchaseInvoiceDate();

                this.FocusAddItems(true);
            }
        });

        this.resetDirtyFlag();

        this.TotalOfItems = ko.computed({
            read: () => {
                let total: number = 0;
                for (var i = 0; i < this.PurchaseInvoiceItems().length; i++) {
                    total += (this.PurchaseInvoiceItems()[i]).TotalPriceReadOnly() || 0;
                }
                return total;
            }
        }).extend({ numeric: 2 });

        this.Total = ko.computed({
            read: () => {
                return this.TotalOfItems() + this.VAT();
            }
        }).extend({ numeric: 2 });

        this.Errors = ko.validation.group(this);
        this.ShowErrors = ko.observable(false);

        this.SupplierHasFocus = ko.observable(true);
    }

    resetDirtyFlag = function () {
        this.IsDirty(false);
        this.FocusAddItems(false);
    }

    serverErrors = ko.observableArray([]);

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

    validateGuid = function (val) {
        if (val == undefined || val == null || val == "" || val == "00000000-0000-0000-0000-000000000000")
            return false;
        return true;
    }

    showError = function (item) {
        if ((item == undefined || !item.isValid()) && this.ShowErrors()) {
            return true;
        }

        return false;
    }

    onClear = function (fileData) {
        if (confirm('Are you sure?')) {
            fileData.clear && fileData.clear();
        }
    }

    addFileName = function (fileID, fileName) {
        //debugger;
        this.UploadedFileNames.push(new MyCustomOption(fileName, fileID));

        for (let i = 0; i < this.UploadedFileNamesDeleted().length; i++) {
            if (this.UploadedFileNamesDeleted()[i].display === fileName) {
                this.UploadedFileNamesDeleted.remove(this.UploadedFileNamesDeleted()[i]);
            }
        }
    }

    removeFileName = function (fileName) {
        //debugger;
        for (let i = 0; i < this.UploadedFileNames().length; i++) {
            if (this.UploadedFileNames()[i].display === fileName) {
                this.UploadedFileNamesDeleted.push(new MyCustomOption(fileName, this.UploadedFileNames()[i].itsValue));
                this.UploadedFileNames.remove(this.UploadedFileNames()[i]);
            }
        }

    }

    ToggleChargesVisible = function () {
        //if (this.ChargesIsVisible() === false)
        //    alert("sdfsdfs");
        //else
        //    alert("aaaa");

        this.ChargesIsVisible(!this.ChargesIsVisible());
    }

    OpenConsignmentDetails = function (data) {
        var options = {
            TabTitle: "loading...",
            PanelName: "ConsignmentDetails",
            UriParam: data
        };
        this.SubscriberTab.notifySubscribers(options, "save");
    }
}

class PurchaseInvoiceItemSundryViewModel {
    PurchaseInvoiceItemID: KnockoutObservable<string>;
    PurchaseInvoiceID: KnockoutObservable<string>;
    
    Quantity: KnockoutObservable<number>;
    UnitPrice: KnockoutObservable<number>;
    PurchaseInvoiceItemChargeTypeID: KnockoutObservable<string>;

    CreatedBy: KnockoutObservable<string>;
    CreatedDate: KnockoutObservable<string>;
    UpdatedBy: KnockoutObservable<string>;
    UpdatedDate: KnockoutObservable<string>;

    Description: KnockoutObservable<string>;
    PreviousDescription: KnockoutObservable<string>;

    IsSaving: KnockoutObservable<boolean>;
    IsDirty: KnockoutObservable<boolean>;
    DirtyCalculations: KnockoutComputed<void>;
    PriceObservable: KnockoutComputed<void>;

    ShowErrors: KnockoutObservable<boolean>;
    Errors: KnockoutValidationErrors;
    
    TotalPrice: KnockoutObservable<number>;
    TotalPriceReadOnly: KnockoutObservable<number>;
    TotalPriceObservable: KnockoutObservable<number>;

    constructor(data) {
        data = data || {};

        this.PurchaseInvoiceID = ko.observable(data.PurchaseInvoiceID);
        this.PurchaseInvoiceItemID = ko.observable(data.PurchaseInvoiceItemID);
        this.Description = ko.observable(data.Description).extend({ required: true });
        
        this.PurchaseInvoiceItemChargeTypeID = ko.observable(data.PurchaseInvoiceItemChargeTypeID || "76000200-0000-0070-9204-000068336078"); // purchase cost by default
        this.Quantity = ko.observable(data.Quantity || 1).extend({ numeric: 2 }).extend({ required: true });
        this.UnitPrice = ko.observable(data.UnitPrice).extend({ numeric: 2 }).extend({ required: true });
        
        this.CreatedBy = ko.observable(data.CreatedBy);
        this.CreatedDate = ko.observable(data.CreatedDate);
        this.UpdatedBy = ko.observable(data.UpdatedBy);
        this.UpdatedDate = ko.observable(data.UpdatedDate);
        
        this.PreviousDescription = ko.observable(data.Description);

        this.TotalPrice = ko.observable(data.TotalPrice).extend({ numeric: 2 });
        this.TotalPriceReadOnly = ko.observable(data.TotalPrice).extend({ numeric: 2 });
        this.IsSaving = ko.observable(false);
        this.IsDirty = ko.observable(false);
        this.DirtyCalculations = ko.computed({
            read: () => {
                this.Quantity();
                this.UnitPrice();
                this.TotalPrice();
                this.Description();
                this.PurchaseInvoiceItemChargeTypeID();
                //debugger;
                this.IsDirty(true);
            }
        });
        this.resetDirtyFlag();
        
        this.TotalPriceObservable = ko.computed({
            read: () => {
                if (this.Quantity() != undefined && this.UnitPrice() != undefined) {
                    this.TotalPrice(this.Quantity() * this.UnitPrice());
                    return this.Quantity() * this.UnitPrice();
                }
                return undefined;
            }
        }).extend({ numeric: 2 });
        
        this.Errors = ko.validation.group(this);
        this.Errors.showAllMessages(false);
        this.ShowErrors = ko.observable(false);
    }

    showError = function (item) {
        if ((item == undefined || !item.isValid()) && this.ShowErrors()) {
            return true;
        }

        return false;
    }

    resetDirtyFlag = function () {
        this.IsDirty(false);
    }
}

class PurchaseInvoiceItemViewModel {
    PurchaseInvoiceItemID: KnockoutObservable<string>;
    PurchaseInvoiceID: KnockoutObservable<string>;
    ConsignmentItemID: KnockoutObservable<string>;
    Description: KnockoutObservable<string>;
    ExchangeRate: KnockoutObservable<string>;
    PurchaseDate: KnockoutObservable<string>;
    Currency: KnockoutObservable<string>;
    Quantity: KnockoutObservable<number>;
    UnitPrice: KnockoutObservable<number>;
    EstimatedPurchaseCost: KnockoutObservable<number>;
    PreviousEstimatedPurchaseCost: KnockoutObservable<number>;

    PurchaseInvoiceItemChargeTypeID: KnockoutObservable<string>;
    ConsignmentPurchaseTypeID: KnockoutObservable<string>;

    ConsignmentPurchaseTypeIDObservable: KnockoutComputed<void>;

    CurrencyAmount: KnockoutObservable<string>;

    FocusConsignment: KnockoutComputed<boolean>;
    CreatedBy: KnockoutObservable<string>;
    CreatedDate: KnockoutObservable<string>;
    UpdatedBy: KnockoutObservable<string>;
    UpdatedDate: KnockoutObservable<string>;
    PreviousConsignmentItemID: KnockoutObservable<string>;
    PreviousDescription: KnockoutObservable<string>;

    IsSaving: KnockoutObservable<boolean>;
    IsDirty: KnockoutObservable<boolean>;
    DirtyCalculations: KnockoutComputed<void>;
    PriceObservable: KnockoutComputed<void>;

    ShowErrors: KnockoutObservable<boolean>;
    Errors: KnockoutValidationErrors;

    IncludeZeroCheckBox: KnockoutObservable<boolean>;
    TotalPrice: KnockoutObservable<number>;
    TotalPriceReadOnly: KnockoutObservable<number>;
    TotalPriceObservable: KnockoutObservable<number>;

    Alias: KnockoutObservable<string>;
    Notes: KnockoutObservable<any>;

    //FocusUCost: KnockoutComputed<boolean>;
    //FocusQty: KnockoutComputed<boolean>;

    constructor(data) {
        data = data || {};

        this.PurchaseInvoiceID = ko.observable(data.PurchaseInvoiceID);
        this.PurchaseInvoiceItemID = ko.observable(data.PurchaseInvoiceItemID);
        this.ConsignmentItemID = ko.observable(data.ConsignmentItemID);
        this.Description = ko.observable(data.Description);
        this.ExchangeRate = ko.observable(data.ExchangeRate);

        this.Alias = ko.observable(data.Alias);
        this.Notes = ko.observable(data.Notes);
        //var finalDate = null;
        //var date_format_check = moment(data.PurchaseDate);
        //if (date_format_check.isValid()) {
        //    finalDate = date_format_check.format('DD/MM/YYYY');
        //} else { // if bad date
        //    var now_date = moment();
        //    finalDate = now_date.format('DD/MM/YYYY');
        //}

        //var month = new Date().getMonth() + 1;
        //var date = new Date().getDate() + "/" + month + "/" + new Date().getFullYear();
        //this.PurchaseDate = ko.observable(data.PurchaseDate || date);
        //this.PurchaseDate = ko.observable(finalDate);
        this.PurchaseDate = ko.observable(data.PurchaseDate);

        this.Currency = ko.observable(data.Currency);

        this.PurchaseInvoiceItemChargeTypeID = ko.observable(data.PurchaseInvoiceItemChargeTypeID || "76000200-0000-0070-9204-000068336078"); // purchase cost by default
        this.ConsignmentPurchaseTypeID = ko.observable("");

        this.Quantity = ko.observable(data.Quantity || 1).extend({ numeric: 2 }).extend({ required: true });
        this.EstimatedPurchaseCost = ko.observable(data.EstimatedPurchaseCost).extend({ numeric: 2 }).extend({ required: true });
        this.PreviousEstimatedPurchaseCost = ko.observable(data.EstimatedPurchaseCost).extend({ numeric: 2 });
        this.UnitPrice = ko.observable(data.UnitPrice).extend({ numeric: 2 }).extend({ required: true });
        this.CurrencyAmount = ko.observable(data.FXTotalPrice);
        this.ConsignmentItemID = ko.observable(data.ConsignmentItemID).extend({ required: true });
        this.CreatedBy = ko.observable(data.CreatedBy);
        this.CreatedDate = ko.observable(data.CreatedDate);
        this.UpdatedBy = ko.observable(data.UpdatedBy);
        this.UpdatedDate = ko.observable(data.UpdatedDate);
        this.PreviousConsignmentItemID = ko.observable(data.ConsignmentItemID);
        this.PreviousDescription = ko.observable(data.Description);

        this.TotalPrice = ko.observable(data.TotalPrice).extend({ numeric: 2 });
        this.TotalPriceReadOnly = ko.observable(data.TotalPrice).extend({ numeric: 2 });
        this.IsSaving = ko.observable(false);
        this.IsDirty = ko.observable(false);
        this.DirtyCalculations = ko.computed({
            read: () => {
                this.ConsignmentItemID();
                this.Quantity();
                this.UnitPrice();
                this.TotalPrice();
                this.PurchaseInvoiceItemChargeTypeID();
                //debugger;
                this.IsDirty(true);
            }
        });
        this.resetDirtyFlag();

        this.IncludeZeroCheckBox = ko.observable(false);
        this.TotalPriceObservable = ko.computed({
            read: () => {
                if (this.Quantity() != undefined && this.UnitPrice() != undefined) {
                    this.TotalPrice(this.Quantity() * this.UnitPrice());
                    return this.Quantity() * this.UnitPrice();
                }
                return undefined;
            }
        }).extend({ numeric: 2 });

        this.PriceObservable = ko.computed({
            read: () => {
                //debugger;
                this.UnitPrice(this.EstimatedPurchaseCost() / this.Quantity());
            }
        });

        this.ConsignmentPurchaseTypeIDObservable = ko.computed({
            read: () => {
                this.ConsignmentPurchaseTypeID();
                // if purchase type is CP then they manually type in the Est.Pur.Cost
                if (this.ConsignmentPurchaseTypeID() === "76008070-0600-0070-9204-000068336078") {
                    this.EstimatedPurchaseCost(0);
                }
            }
        });

        this.FocusConsignment = ko.computed({
            read: () => {
                return this.PurchaseInvoiceItemID() == undefined;
            }
        });
        //this.FocusUCost = ko.computed({
        //    read: () => {
        //        return !this.FocusConsignment() && this.PurchaseInvoiceItemId() == undefined;
        //    }
        //});
        //this.FocusQty = ko.computed({
        //    read: () => {
        //        return !this.FocusUCost() && this.PurchaseInvoiceItemId() == undefined;
        //    }
        //});

        this.Errors = ko.validation.group(this);
        this.ShowErrors = ko.observable(false);
    }

    getProduce = function (request, response) {
        var term = request.term;
        if (term === " ")
            return;
        $.ajax({
            type: 'GET',
            url: '/api/Produce/AutoCompletePurchaseInvoicePC/?search=' + term,
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

    getProduceToggleZero = function (request, ui) {
        var text = request.term;
        if (text === " ")
            return;
        if (text === "")
            return;
        $.ajax({
            type: 'GET',
            url: '/api/Produce/AutoCompletePurchaseInvoicePCZero/?search=' + text,
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
        var labelArray = ui.item.label.split(",");

        // Set value in ViewModel
        var vm = <PurchaseInvoiceItemViewModel>ko.dataFor(event.target);
        vm.ConsignmentItemID(valueArray[0]);
        vm.PreviousConsignmentItemID(valueArray[0]);
        vm.Description(ui.item.label);
        vm.PreviousDescription(ui.item.label);

        $.ajax({
            type: 'GET',
            url: '/api/Consignment/ConsignmentItemBasic/',
            data: { id: valueArray[0] },
            success: function (data) {
                vm.EstimatedPurchaseCost(data.EstimatedPurchaseCost);
                vm.PreviousEstimatedPurchaseCost(data.EstimatedPurchaseCost);
                vm.Quantity(data.QuantityExpected);
                //alert(data.PurchaseTypeID);
                vm.ConsignmentPurchaseTypeID(data.PurchaseTypeID);
            }
        });
    }

    showError = function (item) {
        if ((item == undefined || !item.isValid()) && this.ShowErrors()) {
            return true;
        }

        return false;
    }

    resetDirtyFlag = function () {
        this.IsDirty(false);
    }
}