///// <reference path="../Scripts/typings/jquery/jquery.d.ts" />
///// <reference path="../Scripts/typings/knockout/knockout.d.ts" />
/// <reference path="../util/addressmodel.ts" />

class SupplierMainModel {
    SupplierDepartmentViewModel: KnockoutObservable<SupplierDepartmentViewModel>;
    ConsignmentPagingModel: KnockoutObservable<ConsignmentPagingModel>;
    //TicketIndexPagingModel: KnockoutObservable<TicketIndexPagingModel>; //////////////////////////////
    PurchaseInvoicePagingModel: KnockoutObservable<PurchaseInvoicePagingModel>;
    IsEmpty: KnockoutComputed<boolean>;
    SelectedSupplierDepartmentID: KnockoutObservable<string>;
    validationModel: KnockoutObservable<any>;

    IsInvoicesVisible: KnockoutObservable<boolean>;
    IsRemittanceVisible: KnockoutObservable<boolean>;
    IsConsignmentsVisible: KnockoutObservable<boolean>;
    IsBankingVisible: KnockoutObservable<boolean>;
    IsLocationsVisible: KnockoutObservable<boolean>;
    IsContactsVisible: KnockoutObservable<boolean>;
    
    constructor() {
        this.SupplierDepartmentViewModel = ko.observable(new SupplierDepartmentViewModel());
        this.SelectedSupplierDepartmentID = ko.observable("").extend({ required: true });;
        
        this.validationModel = ko.validatedObservable({
            SelectedSupplierDepartmentID: this.SelectedSupplierDepartmentID
        });

        this.IsInvoicesVisible = ko.observable(false);
        this.IsRemittanceVisible = ko.observable(false);
        this.IsConsignmentsVisible = ko.observable(false);
        this.IsBankingVisible = ko.observable(false);
        this.IsLocationsVisible = ko.observable(false);
        this.IsContactsVisible = ko.observable(false);
        //this.ConsignmentPagingModel = ko.observable(new ConsignmentPagingModel(undefined, undefined));
        //this.PurchaseInvoicePagingModel = ko.observable(new PurchaseInvoicePagingModel(undefined, undefined));
    }

    initializeConsignmentPagingModel = function (data, subscriberTab) {
        data = data || {};
        this.ConsignmentPagingModel = ko.observable(new ConsignmentPagingModel(data, subscriberTab));
    }

    initializePurchaseInvoicePagingModel = function (data, subscriberTab) {
        data = data || {};
        this.PurchaseInvoicePagingModel = ko.observable(new PurchaseInvoicePagingModel(data, subscriberTab));
    }
    
    getSupplierDepartments(request, response) {
        //  alert('gETcUSTOMER');
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

    selectSupplierDepartment = function (event, ui) {
        let suppDeptId = ui.item.languageValue;
        
        var vm = ko.dataFor(event.target);

        var req = $.ajax({
            type: 'GET',
            url: '/API/SupplierDepartment/GetSupplierDepartmentVm',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: { "id": suppDeptId },
            error: function (jqXHR, textStatus, error) {
             
            }
        });

        req.done(function (data) {            
            vm.SupplierDepartmentViewModel().setProperties(data);
        });

        vm.ConsignmentPagingModel().ConsignmentSearch().SupplierDepartmentID(suppDeptId);
        vm.ConsignmentPagingModel().SearchBySupplierDepartmentId();

        vm.PurchaseInvoicePagingModel().PurchaseInvoiceSearch().SupplierDepartmentID(suppDeptId);
        vm.PurchaseInvoicePagingModel().SearchBySupplierDepartmentId();
        
        vm.SelectedSupplierDepartmentID(suppDeptId);
    };

    onSupplierChange = function (event, ui) {
        // if value is cleared, clear in ViewModel
        if (ui.item == null || ui.item.value == null || ui.item.value == '') {
            var vm = ko.dataFor(event.target);
            vm.SelectedSupplierDepartmentID("");
        }
    }

    ToggleContactsVisible = function () {
        this.IsContactsVisible(!this.IsContactsVisible());
        //alert('Supplier ToggleContactsVisible ' + this.IsContactsVisible());
    }
    ToggleLocationsVisible = function () { this.IsLocationsVisible(!this.IsLocationsVisible()); }
    ToggleRemittanceVisible = function () { this.IsRemittanceVisible(!this.IsRemittanceVisible()); }
    ToggleBankingVisible = function () { this.IsBankingVisible(!this.IsBankingVisible()); }
    ToggleConsignmentsVisible = function () {
        this.IsConsignmentsVisible(!this.IsConsignmentsVisible());
    }
    ToggleInvoicesVisible = function () { this.IsInvoicesVisible(!this.IsInvoicesVisible()); }
}

class SupplierDepartmentViewModel {
    SupplierDepartmentName: KnockoutObservable<string>;
    Commission: KnockoutObservable<number>;
    Handling: KnockoutObservable<number>;
    Locations: KnockoutObservableArray<SupplierLocationModel>;
    Contacts: KnockoutObservableArray<SupplierContactModel>;
    BankAccounts: KnockoutObservableArray<BankAccountModel>;

    constructor() {
        this.SupplierDepartmentName = ko.observable("");
        this.Commission = ko.observable(0.0);
        this.Handling = ko.observable(0.0);
        this.Locations = ko.observableArray<SupplierLocationModel>([]);

        //debugger; /////////////////////////////////////////////////////////////
        this.Contacts = ko.observableArray<SupplierContactModel>([]);
        this.BankAccounts = ko.observableArray<BankAccountModel>([]);
    }

    setProperties = function (data) {
        data = data || {};
        this.SupplierDepartmentName(data.SupplierDepartmentName);
        this.Commission(data.Commission);
        this.Handling(data.Handling);

        this.Locations([]);
        this.Contacts([]);
        this.BankAccounts([]);

        for (var i = 0; i < data.SupplierLocations.length; i++) {
            this.Locations.push(new SupplierLocationModel(data.SupplierLocations[i]));
        }
        for (var i = 0; i < data.SupplierContacts.length; i++) {
            this.Contacts.push(new SupplierContactModel(data.SupplierContacts[i]));
        }
        for (var i = 0; i < data.BankAccounts.length; i++) {
            this.BankAccounts.push(new BankAccountModel(data.BankAccounts[i]));
        }
    }
}

class SupplierLocationModel {
    SupplierLocationId: KnockoutObservable<string>;
    SupplierLocationName: KnockoutObservable<string>;
    Telephone: KnockoutObservable<string>;
    FaxNumber: KnockoutObservable<string>;
    Address: KnockoutObservable<AddressModel>;

    constructor(data) {
        this.SupplierLocationId = ko.observable(data.SupplierLocationId);
        this.SupplierLocationName = ko.observable(data.SupplierLocationName);
        this.Telephone = ko.observable(data.Telephone);
        this.FaxNumber = ko.observable(data.FaxNumber);
        this.Address = ko.observable(new AddressModel(data.Address));
    }
}

class ContactModel {
    FirstName: KnockoutObservable<string>;
    LastName: KnockoutObservable<string>;
    Title: KnockoutObservable<string>;
    EmailAddress: KnockoutObservable<string>;

    constructor(contact) {
        this.FirstName = ko.observable(contact.FirstName);
        this.LastName = ko.observable(contact.LastName);
        this.Title = ko.observable(contact.Title);
        this.EmailAddress = ko.observable(contact.EmailAddress);
    }
}

class SupplierContactModel {
    Contact: KnockoutObservable<ContactModel>;

    constructor(data) {
        this.Contact = ko.observable(new ContactModel(data.Contact));
    }
}

class BankAccountModel {
    AccountName     : KnockoutObservable<string>;
    Sortcode1       : KnockoutObservable<string>;
    Sortcode2       : KnockoutObservable<string>;
    Sortcode3       : KnockoutObservable<string>;
    AccountNumber: KnockoutObservable<string>;
    Sortcode: KnockoutComputed<string>;

    constructor(data) {
        this.AccountName = ko.observable(data.AccountName);
        this.Sortcode1 = ko.observable(data.Sortcode1);
        this.Sortcode2 = ko.observable(data.Sortcode2);
        this.Sortcode3 = ko.observable(data.Sortcode3);
        this.AccountNumber = ko.observable(data.AccountNumber);
        this.Sortcode = ko.computed(function() {
            return this.Sortcode1() + "-" + this.Sortcode2() + "-" + this.Sortcode3();
        }, this);
    }
}
