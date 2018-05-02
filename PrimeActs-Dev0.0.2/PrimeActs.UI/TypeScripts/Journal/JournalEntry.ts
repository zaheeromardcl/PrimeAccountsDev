/// <reference path="../Ticket/DailyCashAllocations.ts" />
/*
To Do:
Must be tied to companyid as fed down from tab panel for customers/suppliers/nominal accounts
--Not sure if Supplier and customer ajax calls linit to company - will affect other parts of software
Option to limit to nominal accounts for particular division

Save rules
Saving
*/
class JournalEntryViewModel {
    PurchaseLedgerEntry: KnockoutObservableArray<PurchaseLedgerJournalEntry>;
    PurchaseLedgerTotal: KnockoutComputed<number>;
    PurchaseLedgerTotalNegative: KnockoutComputed<number>;

    SalesLedgerEntry: KnockoutObservableArray<SalesLedgerJournalEntry>;
    SalesLedgerTotal: KnockoutComputed<number>;

    TaxTotal: KnockoutComputed<number>;

    NominalLedgerEntry: KnockoutObservableArray<NominalLedgerJournalEntry>;
    NominalLedgerTotal: KnockoutComputed<number>;
    NominalLedgerBalance: KnockoutComputed<number>;

    EnableSave: KnockoutComputed<boolean>;

    //Defaults from server
    JournalEntryDate: KnockoutObservable<string>;
    JournalEntryYear: KnockoutObservable<number>;

    ShowErrors: KnockoutObservable<boolean>;
    Errors: KnockoutValidationErrors;

    constructor(data) {
        data = data || {};
        var today = new Date();
        var month = '' + (today.getMonth() + 1);
        if (month.length < 2) month = '0' + month;
        var day = '' + today.getDate();
        if (day.length < 2) day = '0' + day;
        var year = today.getFullYear();

        this.JournalEntryDate = ko.observable(year + '-' + month + '-' + day);
        //Database defaults 
        this.JournalEntryYear = ko.observable(year);

        //Purchase Ledger Entry
        this.PurchaseLedgerEntry = ko.observableArray<PurchaseLedgerJournalEntry>([
        ]);
        this.PurchaseLedgerTotal = ko.computed({
            owner: this,
            read: () => {
                var total = 0;
                if (this.PurchaseLedgerEntry) {
                    ko.utils.arrayForEach(this.PurchaseLedgerEntry(), function (entry) {
                        total += +entry.PurchaseAmount();
                    })
                }
                return total;
            }
        }).extend({ numeric: 2 })
        this.PurchaseLedgerTotalNegative = ko.computed({
            owner: this,
            read: () => {
                var total = 0;
                if (this.PurchaseLedgerEntry) {
                    ko.utils.arrayForEach(this.PurchaseLedgerEntry(), function (entry) {
                        total -= +entry.PurchaseAmount();
                    })
                }
                return total;
            }
        }).extend({ numeric: 2 })

        //Sales Ledger Entry
        this.SalesLedgerEntry = ko.observableArray<SalesLedgerJournalEntry>([
        ]);
        this.SalesLedgerTotal = ko.computed({
            owner: this,
            read: () => {
                var total = 0;
                if (this.SalesLedgerEntry) {
                    ko.utils.arrayForEach(this.SalesLedgerEntry(), function (entry) {
                        total += +entry.SalesLedgerEntryDetail().SaleAmount();
                    })
                }
                return total;
            }
        }).extend({ numeric: 2 })

        //Nominal Ledger 
        this.NominalLedgerEntry = ko.observableArray<NominalLedgerJournalEntry>([
        ]);
        this.NominalLedgerTotal = ko.computed({
            owner: this,
            read: () => {
                var total = 0;
                if (this.NominalLedgerEntry) {
                    ko.utils.arrayForEach(this.NominalLedgerEntry(), function (entry) {
                        total += +entry.NominalLedgerEntryAmount();
                    })
                }
                total = +total
                return total;
            }
        }).extend({ numeric: 2 })
        this.NominalLedgerBalance = ko.computed({
            owner: this,
            read: () => {
                return +this.NominalLedgerTotal() - this.PurchaseLedgerTotal() + this.SalesLedgerTotal();
            }
        }).extend({ numeric: 2 })
        
        this.EnableSave = ko.computed({
            owner: this,
            read: () => {
                var enableSave = (this.NominalLedgerBalance() == 0);
                if (this.NominalLedgerEntry().length == 0 && this.PurchaseLedgerEntry().length == 0 && this.SalesLedgerEntry().length == 0) {
                    enableSave = false;
                } else if (enableSave) {
                    var hasValue = false;
                    var hasNull = false;
                    if (this.PurchaseLedgerEntry) {
                        ko.utils.arrayForEach(this.PurchaseLedgerEntry(), function (entry) {
                            if (entry.PurchaseAmount() != 0) {
                                hasValue = true;
                            }
                            if (entry.PurchaseAmount() == null) {
                                hasNull = true;
                            }
                        })
                    }
                    if (this.SalesLedgerEntry) {
                        ko.utils.arrayForEach(this.SalesLedgerEntry(), function (entry) {
                            if (entry.SalesLedgerEntryDetail().SaleAmount() != 0) {
                                hasValue = true;
                            }
                            if (entry.SalesLedgerEntryDetail().SaleAmount() == null) {
                                hasNull = true;
                            }
                        })
                    }
                    if (this.NominalLedgerEntry) {
                        ko.utils.arrayForEach(this.NominalLedgerEntry(), function (entry) {
                            if (entry.NominalLedgerEntryAmount() != 0) {
                                hasValue = true;
                            }
                            if (entry.NominalLedgerEntryAmount() == null) {
                                hasNull = true;
                            }
                        })
                    }
                    enableSave = (hasValue && !hasNull);
                }
                return enableSave;
            }
        })
        

    }

    addNewPurchaseLedgerEntry(): void {
        this.PurchaseLedgerEntry.push(new PurchaseLedgerJournalEntry());
    }
    deletePurchaseLedgerEntry = (purchaseLedgerEntry: PurchaseLedgerJournalEntry) : void => {
        this.PurchaseLedgerEntry.remove(purchaseLedgerEntry);
    }
    addNewSalesLedgerEntry(): void {
        this.SalesLedgerEntry.push(new SalesLedgerJournalEntry());
    }
    deleteSalesLedgerEntry = (salesLedgerEntry: SalesLedgerJournalEntry): void => {
        this.SalesLedgerEntry.remove(salesLedgerEntry);
    }
    addNewNominalLedgerEntry(): void {
        if (this.NominalLedgerEntry().length == 0 || this.PurchaseLedgerEntry().length != 0 || this.SalesLedgerEntry().length != 0) {
            this.NominalLedgerEntry.push(new NominalLedgerJournalEntry(''));
        } else {
            this.NominalLedgerEntry.push(new NominalLedgerJournalEntry(this.NominalLedgerEntry()[0].NominalLedgerEntryReference()));
        }
    }
    deleteNominalLedgerEntry = (nominalLedgerEntry: NominalLedgerJournalEntry): void => {
        this.NominalLedgerEntry.remove(nominalLedgerEntry);
    }
    saveJournal(): void {
    };

}

class PurchaseLedgerJournalEntry {
    PurchaseLedgerEntryID: KnockoutObservable<string>;
    PurchaseLedgerEntryDescription: KnockoutObservable<string>;
    PurchaseAmount: KnockoutObservable<number>;
    CurrencyAmount: KnockoutObservable<number>;
    CurrencyID: KnockoutObservable<string>;
    ExchangeRate: KnockoutObservable<number>;
    PurchaseInvoiceID: KnockoutObservable<string>;
    SupplierDepartmentID: KnockoutObservable<string>;
    SupplierDepartmentName: KnockoutObservable<string>;
    PreviousSupplierDepartmentID: KnockoutObservable<string>;
    PreviousSupplierDepartmentName: KnockoutObservable<string>;
    SupplierHasFocus: KnockoutObservable<boolean>;
    //PurchaseLedgerEntryNote: KnockoutObservable<string>;
    TransactionTaxAmount: KnockoutObservable<number>;

    ShowErrors: KnockoutObservable<boolean>;
    Errors: KnockoutValidationErrors;

    constructor() {
        this.PurchaseLedgerEntryID = ko.observable('');
        this.PurchaseLedgerEntryDescription = ko.observable('');
        this.PurchaseAmount = ko.observable(null).extend({ numeric: 2 }).extend({ required: true });
        this.CurrencyAmount = ko.observable(null).extend({ numeric: 2 });
        this.CurrencyID = ko.observable(null);
        this.ExchangeRate = ko.observable(null);
        this.PurchaseInvoiceID = ko.observable('');
        this.SupplierDepartmentID = ko.observable('').extend({ required: true });
        this.SupplierDepartmentName = ko.observable('').extend({ required: true });
        this.PreviousSupplierDepartmentID = ko.observable('');
        this.PreviousSupplierDepartmentName = ko.observable('');
        this.SupplierHasFocus = ko.observable(true);
        //this.PurchaseLedgerEntryNote = ko.observable('');
        this.TransactionTaxAmount = ko.observable(0);

        this.Errors = ko.validation.group(this);
        this.ShowErrors = ko.observable(false);
    }

    getSupplierDepartments = (request: any, response: any): void => {
        var text = request.term;
        $.ajax({
            type: 'GET',
            url: '/API/Supplier/AutoComplete/?search=' + text,
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

    selectSupplierDepartment = (event: any, ui: any): void => {
        var suppDeptId = ui.item.languageValue;
        this.SupplierDepartmentID(suppDeptId);
        this.SupplierDepartmentName(ui.item.label);
        this.PreviousSupplierDepartmentID(suppDeptId);
        this.PreviousSupplierDepartmentName(ui.item.label);
    };

    onSupplierDepartmentFocusOut = (event: any): void => {
        if (this.SupplierDepartmentName() !== this.PreviousSupplierDepartmentName() && this.SupplierDepartmentID() === this.PreviousSupplierDepartmentID()) {
            this.SupplierDepartmentID(undefined);
            this.SupplierDepartmentName(undefined);
        }
    }

    serverErrors = ko.observableArray([]);
    showRequired = (item: any, field: any): boolean => {
        ko.validation.validateObservable(item);
        if (!this.showError(item)) {
            return item == undefined || !item.isValid();
        }
        return false;
    }

    showGuidFieldRequired = (item: any): boolean => {
        return !this.validateGuid(item());
    }

    validateGuid = (val: any): boolean => {
        if (val == undefined || val == null || val == "" || val == "00000000-0000-0000-0000-000000000000")
            return false;
        return true;
    }

    showError = (item: any): boolean => {
        if ((item == undefined || !item.isValid()) && this.ShowErrors()) {
            return true;
        }
        return false;
    }

}

class SalesLedgerJournalEntry {
    SalesLedgerEntryDetail: KnockoutObservable<SalesLedgerEntry>;
    LedgerEntryTypeID: KnockoutObservable<string>;
    SalesLedgerEntryDescription: KnockoutObservable<string>;
    CurrencyAmount: KnockoutObservable<number>;
    CurrencyID: KnockoutObservable<string>;
    ExchangeRate: KnockoutObservable<number>;
    CustomerDepartmentID: KnockoutObservable<string>;
    CustomerDepartmentName: KnockoutObservable<string>;
    PreviousCustomerDepartmentID: KnockoutObservable<string>;
    PreviousCustomerDepartmentName: KnockoutObservable<string>;
    CustomerHasFocus: KnockoutObservable<boolean>;
    SalesLedgerEntryNote: KnockoutObservable<string>;
    SalesInvoiceAllocation: KnockoutObservableArray<InvoiceAllocation>;
    AllocatedList: KnockoutComputed<string>;
    SaleAmount: KnockoutComputed<number>;
    
    ShowErrors: KnockoutObservable<boolean>;
    Errors: KnockoutValidationErrors;

    constructor() {
        this.SalesLedgerEntryDetail = ko.observable(new SalesLedgerEntry({ SalesLedgerEntryID: '', SaleAmount: null, CreatedDate: '', SalesPersonName: '', CustomerDepartment: '' }));
        this.SalesLedgerEntryDetail().SaleAmount = ko.observable(null).extend({ numeric: 2 }).extend({ required: true });
        this.LedgerEntryTypeID = ko.observable('');
        this.SalesLedgerEntryDescription = ko.observable('');
        this.CurrencyAmount = ko.observable(null);
        this.CurrencyID = ko.observable(null);
        this.ExchangeRate = ko.observable(null);
        this.CustomerDepartmentID = ko.observable('');
        this.CustomerDepartmentName = ko.observable('');
        this.PreviousCustomerDepartmentID = ko.observable('');
        this.PreviousCustomerDepartmentName = ko.observable('');
        this.CustomerHasFocus = ko.observable(true);
        this.SalesLedgerEntryNote = ko.observable('');

        this.Errors = ko.validation.group(this);
        this.ShowErrors = ko.observable(false);
    }

    getCustomerDepartments = (request: any, response: any): void => {
        var text = request.term;
        $.ajax({
            type: 'GET',
            url: '/API/Customer/AutoComplete/?search=' + text,
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

    selectCustomerDepartment = (event: any, ui: any): void => {
        var custDeptId = ui.item.languageValue;
        this.CustomerDepartmentID(custDeptId);
        this.CustomerDepartmentName(ui.item.label);
        this.PreviousCustomerDepartmentID(custDeptId);
        this.PreviousCustomerDepartmentName(ui.item.label);
    };

    onCustomerDepartmentFocusOut = (event: any): void => {
        if (this.CustomerDepartmentName() !== this.PreviousCustomerDepartmentName() && this.CustomerDepartmentID() === this.PreviousCustomerDepartmentID()) {
            this.CustomerDepartmentID(undefined);
            this.CustomerDepartmentName(undefined);
        }
    }

    serverErrors = ko.observableArray([]);
    showRequired = (item: any, field: any): boolean => {
        ko.validation.validateObservable(item);
        if (!this.showError(item)) {
            return item == undefined || !item.isValid();
        }
        return false;
    }

    showGuidFieldRequired = (item: any): boolean => {
        return !this.validateGuid(item());
    }

    validateGuid = (val: any): boolean => {
        if (val == undefined || val == null || val == "" || val == "00000000-0000-0000-0000-000000000000")
            return false;
        return true;
    }

    showError = (item: any): boolean => {
        if ((item == undefined || !item.isValid()) && this.ShowErrors()) {
            return true;
        }
        return false;
    }
}

class NominalLedgerJournalEntry {
    NominalAccountID: KnockoutObservable<string>;
    NominalAccountName: KnockoutObservable<string>;
    PreviousNominalAccountID: KnockoutObservable<string>;
    PreviousNominalAccountName: KnockoutObservable<string>;
    NominalAccountHasFocus: KnockoutObservable<boolean>;
    NominalLedgerEntryReference: KnockoutObservable<string>;
    NominalLedgerEntryAmount: KnockoutObservable<number>;
    NominalLedgerEntryDate: KnockoutObservable<string>;
    NominalLedgerEntryDescription: KnockoutObservable<string>;
    CurrencyAmount: KnockoutObservable<number>;
    CurrencyID: KnockoutObservable<string>;
    ExchangeRate: KnockoutObservable<number>;
    DescriptionPlaceHolder: KnockoutObservable<string>;

    ShowErrors: KnockoutObservable<boolean>;
    Errors: KnockoutValidationErrors;

    constructor(firstReference: string) {
        this.NominalAccountID = ko.observable('').extend({ required: true });
        this.NominalAccountName = ko.observable('').extend({ required: true });
        this.PreviousNominalAccountID = ko.observable('');
        this.PreviousNominalAccountName = ko.observable('');
        this.NominalAccountHasFocus = ko.observable(true);
        this.NominalLedgerEntryReference = ko.observable(firstReference);
        this.NominalLedgerEntryAmount = ko.observable(null).extend({ numeric: 2 }).extend({ required: true });
        this.NominalLedgerEntryDescription = ko.observable('');
        this.CurrencyAmount = ko.observable(null).extend({ numeric: 2 });
        this.CurrencyID = ko.observable('');
        this.ExchangeRate = ko.observable(null).extend({ numeric: 2 });
        
        this.DescriptionPlaceHolder = ko.computed({
            owner: this,
            read: () => {
                var str = 'Description: negative value = credit, positive value = debit';
                if (this.NominalLedgerEntryAmount() > 0) {
                    str = 'Description of debit (positive amount)';
                } 
                if (this.NominalLedgerEntryAmount() < 0) {
                    str = 'Description of credit (negative amount)';
                } 
                return str;
            }
        }).extend({ numeric: 2 })

        this.Errors = ko.validation.group(this);
        this.ShowErrors = ko.observable(false);
    }

    getNominalAccounts = (request: any, response: any): void => {
        var text = request.term;
        $.ajax({
            type: 'GET',
            url: '/API/NominalAccount/AutoComplete/?companyId=EA3CDB19-D647-4E87-AA83-3A7E523F16C8&search=' + text,
            data: {
                json: '{}',
                delay: 0.5,
                search: text,
                companyId: "EA3CDB19-D647-4E87-AA83-3A7E523F16C8"
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

    selectNominalAccount = (event: any, ui: any): void => {
        var nomActId = ui.item.languageValue;
        this.NominalAccountID(nomActId);
        this.NominalAccountName(ui.item.label);
        this.PreviousNominalAccountID(nomActId);
        this.PreviousNominalAccountName(ui.item.label);
    };

    onNominalAccountFocusOut = (event: any): void => {
        if (this.NominalAccountName() !== this.PreviousNominalAccountName() && this.NominalAccountID() === this.PreviousNominalAccountID()) {
            this.NominalAccountID(undefined);
            this.NominalAccountName(undefined);
        }
    }

    serverErrors = ko.observableArray([]);
    showRequired = (item: any, field: any): boolean => {
        ko.validation.validateObservable(item);
        if (!this.showError(item)) {
            return item == undefined || !item.isValid();
        }
        return false;
    }

    showGuidFieldRequired = (item: any): boolean => {
        return !this.validateGuid(item());
    }

    validateGuid = (val: any): boolean => {
        if (val == undefined || val == null || val == "" || val == "00000000-0000-0000-0000-000000000000")
            return false;
        return true;
    }

    showError = (item: any): boolean => {
        if ((item == undefined || !item.isValid()) && this.ShowErrors()) {
            return true;
        }
        return false;
    }
}

class InvoiceAllocation {
    InvoiceID: KnockoutObservable<string>;
    InvoiceDate: KnockoutObservable<string>;
    InvoiceReference: KnockoutObservable<string>;
    InvoiceBalance: KnockoutObservable<number>;
    AllocatedAmount: KnockoutObservable<number>;
    InvoiceNewBalance: KnockoutObservable<number>;
    constructor(invoiceID: string, allocatedAmount: number) {
        this.InvoiceID = ko.observable(invoiceID);
        this.AllocatedAmount = ko.observable(allocatedAmount).extend({ numeric: 2 }).extend({ required: true });
    }
}