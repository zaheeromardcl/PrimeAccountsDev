///// <reference path="../Scripts/typings/jquery/jquery.d.ts" />
///// <reference path="../Scripts/typings/knockout/knockout.d.ts" />
/// <reference path="../util/addressmodel.ts" />
      
interface IBankAccountModel {
    BankAccountID: KnockoutObservable<string>;
    AccountName: KnockoutObservable<string>;
    SortCode1: KnockoutObservable<number>;
    SortCode2: KnockoutObservable<number>;
    SortCode3: KnockoutObservable<number>;
    IBAN: KnockoutObservable<string>;
    SWIFT: KnockoutObservable<string>;
}
interface IContactModel {
}
interface ICustomerLocationModel {
}
interface IStatementModel {
}
interface ICustomerDepartmentModel {

}
interface ISalesInvoiceModel {
    InvoiceID: KnockoutObservable<string>;
    InvoiceReference: KnockoutObservable<string>;
}
interface ITicketModel {
    TicketID: KnockoutObservable<string>;
}



class CustomerViewModel {
    CustomerDepartmentID: KnockoutObservable<string>;
    BankAccounts: KnockoutObservableArray<CustomerBankAccount>;
    CustomerDepartmentName: KnockoutObservable<string>;
    Contacts: KnockoutObservableArray<CustomerContact>;
    Tickets: KnockoutObservableArray<CustomerTicket>;
    SalesInvoices: KnockoutObservableArray<CustomerSalesInvoice>;
    CustomerDepartments: KnockoutObservableArray<ICustomerDepartmentModel> = ko.observableArray([]);
    Locations: KnockoutObservableArray<ICustomerLocationModel> = ko.observableArray([]);
    // BankAccounts: KnockoutObservableArray<IBankAccountModel> = ko.observableArray([]); 
    //Tickets: KnockoutObservableArray<ITicketModel> = ko.observableArray([]);
    //SalesInvoices: KnockoutObservableArray<ISalesInvoiceModel> = ko.observableArray([]);
    //Statements: KnockoutObservableArray<IStatementModel> = ko.observableArray([]);
        
    //calculate Balance from sales ledger entries.
    //decimal Balance { get; set; }

    constructor(data) {
        data = data || {};
        this.CustomerDepartmentID = ko.observable(data.CustomerDepartmentID);
        this.CustomerDepartmentName = ko.observable("");
        this.Contacts = ko.observableArray<CustomerContact>([]);
        this.Locations = ko.observableArray<CustomerLocation>([]);

        for (var i = 0; i < data.length; i++) {
            this.Contacts.push(new CustomerContact(data.Contacts[i]));
        }
        //for (var i = 0; i < data.Locations.length; i++) {
        for (var i = 0; i < data.length; i++) {
            this.Locations.push(new CustomerLocationModel(data.CustomerLocations[i]));
        }
        //debugger; ////////////////////////////
        this.BankAccounts = ko.observableArray<CustomerBankAccount>([]);
        for (var i = 0; i < data.length; i++) {
            this.BankAccounts.push(new CustomerBankAccount(data.BankAccounts[i]));
        }
        //debugger; ////////////////////////////
        this.Tickets = ko.observableArray<CustomerTicket>([]);
        for (var i = 0; i < data.length; i++) {
            this.Tickets.push(new CustomerTicket(data.Tickets[i]));
        }
        this.SalesInvoices = ko.observableArray<CustomerSalesInvoice>([]);
        for (var i = 0; i < data.length; i++) {
            this.SalesInvoices.push(new CustomerSalesInvoice(data.SalesInvoices[i]));
        }
        //this.CustomerDepartments = ko.observableArray<CustomerDepartment>([]);
        //for (var i = 0; i < data.length; i++) {
        //    this.CustomerDepartments.push(new CustomerDepartment(data.SalesInvoices[i]));
        //}
        
    }

    setProperties = function (data) {
        data = data || {};
        this.Contacts([]);
        this.Locations([]);
        this.CustomerDepartmentID(data.CustomerDepartmentID);
        this.CustomerDepartmentName(data.CustomerDepartmentName);   
   
        if (data != null && data.Contacts != null && data.Contacts.length > 0)
        for (var i = 0; i < data.Contacts.length; i++) {
            this.Contacts.push(new ContactModel(data.Contacts[i]));
            }
        if (data != null && data.Locations != null && data.Locations.length > 0)
        {
            //for (var i = 0; i < data.Locations.length; i++) {
            for (var i = 0; i < data.length; i++) {
                this.Locations.push(new CustomerLocationModel(data.CustomerLocations[i]));
            }
            this.CustomerDepartmentName(this.Locations[0].CustomerDepartmentName);  
        }
        
    //    for (var i = 0; i < data.BankAccounts.length; i++) {
    //        this.BankAccounts.push(new BankAccountModel(data.BankAccounts[i]));
    //    }
    }  
}
//class CustomerDepartment implements ICustomerDepartmentModel {
//    //CustomerDeparm: KnockoutObservable<string>;
//    //TicketID: KnockoutObservable<string>;
//    //DepartmentID: KnockoutObservable<string>;
//    //TicketItemDescription: KnockoutObservable<string>;
//    //CurrencyAmount: KnockoutObservable<number>;
//    //TicketItemQuantity: KnockoutObservable<number>;
//    //TicketItemBrand: KnockoutObservable<string>;
//    //TicketItemWeight: KnockoutObservable<string>;
//    //TicketItemPorterageID: KnockoutObservable<string>;
//    //TicketItemMinPorterage: KnockoutObservable<number>;
//    //TicketItemPorterageValue: KnockoutObservable<number>;
//    //TicketItemPorterage: KnockoutObservable<number>;
//    //TicketItemSize: KnockoutObservable<number>;
//    //TicketItemUnitPrice: KnockoutObservable<number>;
//    //TicketItemTotalPrice: KnockoutObservable<number>;
//    //ConsignmentItemID: KnockoutObservable<string>;
//    //ConsignmentReference: KnockoutObservable<string>;
//    //ProduceID: KnockoutObservable<string>;
//    //Produce: KnockoutObservable<string>;
//    //SupplierID: KnockoutObservable<string>;
//    //SupplierName: KnockoutObservable<string>;
//    //OriginalTicketItemID: KnockoutObservable<string>;

//    //UnitCost: KnockoutComputed<number>;

//    constructor(data) {
//        data = data || {};

//        //    this.TicketItemID = ko.observable(data.TicketItemID);
//        //    this.TicketID = ko.observable(data.TicketID);
//        //    this.DepartmentID = ko.observable(data.DepartmentID);
//        //    this.TicketItemDescription = ko.observable(data.TicketItemDescription);
//        //    this.CurrencyAmount = ko.observable(data.CurrencyAmount);
//        //    this.TicketItemQuantity = ko.observable(data.TicketItemQuantity);
//        //    this.TicketItemBrand = ko.observable(data.TicketItemBrand);
//        //    this.TicketItemWeight = ko.observable(data.TicketItemWeight);
//        //    this.TicketItemPorterageID = ko.observable(data.TicketItemPorterageID);
//        //    this.TicketItemMinPorterage = ko.observable(data.TicketItemMinPorterage);
//        //    this.TicketItemPorterageValue = ko.observable(data.TicketItemPorterageValue);
//        //    this.TicketItemPorterage = ko.observable(data.TicketItemPorterage);
//        //    this.TicketItemSize = ko.observable(data.TicketItemSize);
//        //    this.TicketItemUnitPrice = ko.observable(data.TicketItemUnitPrice);
//        //    this.TicketItemTotalPrice = ko.observable(data.TicketItemTotalPrice);
//        //    this.ConsignmentItemID = ko.observable(data.ConsignmentItemID);
//        //    this.ConsignmentReference = ko.observable(data.ConsignmentReference);
//        //    this.ProduceID = ko.observable(data.ProduceID);
//        //    this.Produce = ko.observable(data.Produce);
//        //    this.SupplierID = ko.observable(data.SupplierID);
//        //    this.SupplierName = ko.observable(data.SupplierName);
//        //    this.OriginalTicketItemID = ko.observable(data.OriginalTicketItemID);

//        //    this.UnitCost = ko.computed({
//        //        read: () => {
//        //            var quantity = this.TicketItemQuantity();
//        //            if (quantity > 0) {
//        //                return this.TicketItemTotalPrice() / this.TicketItemQuantity();
//        //            }
//        //            return this.TicketItemTotalPrice();
//        //        }
//        //    });
//        //}

//    }
    
//}

class CustomerContact implements IContactModel {  
    ContactID: KnockoutObservable<string>;
    Title: KnockoutObservable<string>;
    FirstName: KnockoutObservable<string>;
    LastName: KnockoutObservable<string>;
    EmailAddress: KnockoutObservable<string>;  
    constructor(data) {
        data = data || {};
        this.ContactID = ko.observable(data.ContactID);
        this.Title = ko.observable(data.Title);
        this.FirstName = ko.observable(data.FirstName);
        this.LastName = ko.observable(data.LastName);
        this.EmailAddress = ko.observable(data.EmailAddress);
    }
}
class CustomerTicket implements ITicketModel {
    //TicketItemID: KnockoutObservable<string>;
    TicketID: KnockoutObservable<string>;
    //DepartmentID: KnockoutObservable<string>;
    //TicketItemDescription: KnockoutObservable<string>;
    //CurrencyAmount: KnockoutObservable<number>;
    //TicketItemQuantity: KnockoutObservable<number>;
    //TicketItemBrand: KnockoutObservable<string>;
    //TicketItemWeight: KnockoutObservable<string>;
    //TicketItemPorterageID: KnockoutObservable<string>;
    //TicketItemMinPorterage: KnockoutObservable<number>;
    //TicketItemPorterageValue: KnockoutObservable<number>;
    //TicketItemPorterage: KnockoutObservable<number>;
    //TicketItemSize: KnockoutObservable<number>;
    //TicketItemUnitPrice: KnockoutObservable<number>;
    //TicketItemTotalPrice: KnockoutObservable<number>;
    //ConsignmentItemID: KnockoutObservable<string>;
    //ConsignmentReference: KnockoutObservable<string>;
    //ProduceID: KnockoutObservable<string>;
    //Produce: KnockoutObservable<string>;
    //SupplierID: KnockoutObservable<string>;
    //SupplierName: KnockoutObservable<string>;
    //OriginalTicketItemID: KnockoutObservable<string>;

    //UnitCost: KnockoutComputed<number>;

    constructor(data) {
        data = data || {};
        this.TicketID = ko.observable(data.TicketID);
        //    this.TicketItemID = ko.observable(data.TicketItemID);
        //    this.TicketID = ko.observable(data.TicketID);
        //    this.DepartmentID = ko.observable(data.DepartmentID);
        //    this.TicketItemDescription = ko.observable(data.TicketItemDescription);
        //    this.CurrencyAmount = ko.observable(data.CurrencyAmount);
        //    this.TicketItemQuantity = ko.observable(data.TicketItemQuantity);
        //    this.TicketItemBrand = ko.observable(data.TicketItemBrand);
        //    this.TicketItemWeight = ko.observable(data.TicketItemWeight);
        //    this.TicketItemPorterageID = ko.observable(data.TicketItemPorterageID);
        //    this.TicketItemMinPorterage = ko.observable(data.TicketItemMinPorterage);
        //    this.TicketItemPorterageValue = ko.observable(data.TicketItemPorterageValue);
        //    this.TicketItemPorterage = ko.observable(data.TicketItemPorterage);
        //    this.TicketItemSize = ko.observable(data.TicketItemSize);
        //    this.TicketItemUnitPrice = ko.observable(data.TicketItemUnitPrice);
        //    this.TicketItemTotalPrice = ko.observable(data.TicketItemTotalPrice);
        //    this.ConsignmentItemID = ko.observable(data.ConsignmentItemID);
        //    this.ConsignmentReference = ko.observable(data.ConsignmentReference);
        //    this.ProduceID = ko.observable(data.ProduceID);
        //    this.Produce = ko.observable(data.Produce);
        //    this.SupplierID = ko.observable(data.SupplierID);
        //    this.SupplierName = ko.observable(data.SupplierName);
        //    this.OriginalTicketItemID = ko.observable(data.OriginalTicketItemID);

        //    this.UnitCost = ko.computed({
        //        read: () => {
        //            var quantity = this.TicketItemQuantity();
        //            if (quantity > 0) {
        //                return this.TicketItemTotalPrice() / this.TicketItemQuantity();
        //            }
        //            return this.TicketItemTotalPrice();
        //        }
        //    });
        //}
    }
}

class CustomerSalesInvoice implements ISalesInvoiceModel {
    InvoiceID: KnockoutObservable<string>;
    InvoiceReference: KnockoutObservable<string>;
    constructor(data) {
        data = data || {};
            this.InvoiceReference = ko.observable(data.InvoiceReference);
            this.InvoiceID = ko.observable(data.InvoiceID);      
    }
}

class CustomerLocation implements ICustomerLocationModel {
    LocationID: KnockoutObservable<string>;
    constructor(data) {
        data = data || {};
        this.LocationID = ko.observable(data.LocationID);      
    }
}

class CustomerLocationModel {
    CustomerLocationId: KnockoutObservable<string>;
    CustomerLocationName: KnockoutObservable<string>;
    Telephone: KnockoutObservable<string>;
    FaxNumber: KnockoutObservable<string>;
    Address: KnockoutObservable<AddressModel>;
    constructor(data) {
        this.CustomerLocationId = ko.observable(data.CustomerLocationId); // ko.observable(data.SupplierLocationId);
        this.CustomerLocationName = ko.observable(data.CustomerLocationName); // ko.observable(data.SupplierLocationName);
        this.Telephone = ko.observable(data.Telephone);
        this.FaxNumber = ko.observable(data.FaxNumber);
        this.Address = ko.observable(new AddressModel(data.Address));
    }
}

class CustomerBankAccount implements IBankAccountModel{
    BankAccountID: KnockoutObservable<string>;   
    AccountName: KnockoutObservable<string>;
    SortCode1: KnockoutObservable<number>;
    SortCode2: KnockoutObservable<number>;
    SortCode3: KnockoutObservable<number>;
    IBAN: KnockoutObservable<string>;
    SWIFT: KnockoutObservable<string>;

    constructor(data) {
        data = data || {};
        this.BankAccountID = ko.observable(data.BankAccountID);
        this.AccountName = ko.observable(data.AccountName);
        this.SortCode1 = ko.observable(data.SortCode1);
        this.SortCode2 = ko.observable(data.SortCode2);
        this.SortCode3 = ko.observable(data.SortCode3);
        this.IBAN = ko.observable(data.IBAN);
        this.SWIFT = ko.observable(data.SWIFT);         
    }
}

class TicketsViewModel {
    CustomerTicketID: KnockoutObservable<string>;
    CustomerDepartmentID: KnockoutObservable<string>;
    ConsignmentID: KnockoutObservable<string>;
    ProductID: KnockoutObservable<string>;
    Quantity: KnockoutObservable<number>;
    UnitPrice: KnockoutObservable<number>;
    LineTotal: KnockoutObservable<number>;
    Initial: boolean;

    constructor(data) {
        data = data || {};
        this.Initial = true;
    }
}

class RebatesViewModel {
    CustomerDepartmentID: KnockoutObservable<string>;
    RebateRate: KnockoutObservable<number>;
    RebateType: KnockoutObservable<string>;
    RebateCustomerDepartmentID: KnockoutObservable<string>;
    CustomerDepartmentList: KnockoutObservableArray<SelectOption>;
    IsSaving: KnockoutObservable<boolean>;
    IsDirty: KnockoutObservable<boolean>;
    DirtyCalculations: KnockoutComputed<void>;
    WhenItsDirty: KnockoutComputed<void>;
    ShowErrors: KnockoutObservable<boolean>;
    Errors: KnockoutValidationErrors;
    Initial: boolean;

    constructor(data) {
        data = data || {};
        this.Initial = true;
        this.DirtyCalculations = undefined
        this.IsDirty = ko.observable(false);
        this.RebateRate = ko.observable(data.RebateRate);
        var rebateType = "" + data.RebateType;
        this.RebateType = ko.observable(rebateType);
        this.CustomerDepartmentID = ko.observable(data.CustomerDepartmentID);
        this.CustomerDepartmentList = ko.observableArray<SelectOption>([]);
        if (data.CustomerDepartmentEditModels != undefined) {
            for (var i = 0; i < data.CustomerDepartmentEditModels.length; i++) {
                this.CustomerDepartmentList.push(new SelectOption(data.CustomerDepartmentEditModels[i].CustomerDepartmentID,
                    data.CustomerDepartmentEditModels[i].CustomerDepartmentName));
            }
        }
        this.RebateCustomerDepartmentID = ko.observable(data.RebateCustomerDepartmentID);
        
        this.IsSaving = ko.observable(false);

        this.DirtyCalculations = ko.computed({
            read: () => {
                this.RebateRate();
                this.RebateType();
                this.RebateCustomerDepartmentID();
                //debugger; ///////////////////////////////
                this.IsDirty(true);
            }
        });

        this.resetDirtyFlag();

        this.WhenItsDirty = ko.computed({
            read: () => {
                if (this.IsDirty() && !this.Initial) {
                    //debugger; ////////////////////////////
                    this.onRebateChange();
                }
            }
        });
    }

    onRebateChange() {
        let self = this;
        if (this.IsSaving() /*|| !this.IsDirty()*/) {
            return;
        }
        // Re-validate
        this.Errors = ko.validation.group(this);

        if (this.Errors().length === 0) {
            this.IsSaving(true);
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                url: '/api/CustomerDepartment/UpdateRebate/',
                data: ko.toJSON(this),
                success: function (result) {
                    //self = new RebatesViewModel(result);
                    self.setValuesForProperties(result);
                    //self.saveState(false);
                    self.resetDirtyFlag();
                    self.IsSaving(false);
                },
                error: function (jqXHR, textStatus, error) {
                    self.IsSaving(false);
                    alert(error);
                }
            });
        }
        else {
            this.ShowErrors(true);
            this.Errors.showAllMessages(true);
        }
    }

    setValuesForProperties(data) {
        data = data || {};
        this.RebateRate(data.RebateRate);
        var rebateType = "" + data.RebateType;
        this.RebateType(rebateType);
        this.CustomerDepartmentID(data.CustomerDepartmentID);
    }

    resetDirtyFlag = function () {
        this.IsDirty(false);
    }

    serverErrors = ko.observableArray([]);

    showRequired = function (item, field) {
        ko.validation.validateObservable(item);
        if (!this.showError(item)) {
            return item == undefined || !item.isValid();
        }
        return false;
    }

    showError = function (item) {
        if ((item == undefined || !item.isValid()) && this.ShowErrors()) {
            return true;
        }

        return false;
    }
}

class CustomerMainModel {
    CustomerDepartmentID: KnockoutObservable<string>;
    CustomerDetailsVM: KnockoutObservable<CustomerViewModel>; //////////////////////////////
    TicketPagingModel: KnockoutObservable<CustomerTicketPagingModel>; //////////////////////
    //CustomerTicketPaging: KnockoutObservable<CustomerTicketPagingModel>;
    Paging: KnockoutObservable<Paging>;
    validationModel: KnockoutObservable<any>;
    IsTicketsVisible: KnockoutObservable<boolean>;
    IsContactsVisible: KnockoutObservable<boolean>;
    IsLocationsVisible: KnockoutObservable<boolean>;
    RebatesModel: KnockoutObservable<RebatesViewModel>;

    constructor(data, tabPanelName:string) {

        data = data || {};
        this.CustomerDepartmentID = ko.observable(data.CustomerDepartmentID).extend({ required: true });       
        this.CustomerDetailsVM = ko.observable(new CustomerViewModel(data));
        this.CustomerDetailsVM().CustomerDepartmentID(data.CustomerDepartmentID);
        this.RebatesModel = ko.observable(new RebatesViewModel(data));
        //this.SalesInvoicePaging = ko.observable(this.SalesInvoicePagingModel().Paging());
        //this.TicketPaging = ko.observable(this.TicketPagingModel().Paging());
        //this.SalesInvoicePagingModel = ko.observable(new SalesInvoicePagingModel(data));
        //this.CustomerTicketPagingModel = ko.observable( new CustomerTicketPagingModel(data, subscriberTab));
        //this.TicketPagingModel = ko.observable(new CustomerTicketPagingModel(data, subscriberTab)); /////////////////
        this.validationModel = ko.validatedObservable({
            CustomerDepartmentID: this.CustomerDepartmentID
        });     
        this.IsTicketsVisible = ko.observable(false);
        this.IsContactsVisible = ko.observable(false);
        this.IsLocationsVisible = ko.observable(false);
    }
  
    //initializeCustomerTicketPaging = function (data, subscriberTab) {
    //    data = data || {};
    //    this.CustomerTicketPaging = ko.observable(new CustomerTicketPagingModel(data, subscriberTab));
    //    this.Paging = ko.observable(this.CustomerTicketPaging().Paging());
    //}
    initializeTicketPagingModel = function (data, subscriberTab) {
        data = data || {};
        this.TicketPagingModel = ko.observable(new CustomerTicketPagingModel(data, subscriberTab));
    }

    //Autocomplete for Customers
    getCustomers(request, response) {
        //  alert('gETcUSTOMER');
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
        let custDepartmentID = ui.item.languageValue;
        console.log('selectCustomer custDepartmentID ', custDepartmentID);
     
        var vm = ko.dataFor(event.target);
        var req = $.ajax({
            type: 'GET',
            url: '/API/CustomerDepartment/GetCustomerDepartment',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: { "id": custDepartmentID },
            error: function (jqXHR, textStatus, error) {
                alert(error);
            }
        });

        req.done(function (data) {
            vm.CustomerDepartmentID(custDepartmentID);

            vm.CustomerDetailsVM().setProperties(data);
        
            vm.RebatesModel(new RebatesViewModel(data));
            vm.RebatesModel().resetDirtyFlag();
            vm.RebatesModel().Initial = false;
        });
        
        //  vm.CustomerTicketPaging().TicketSearch().CustomerDepartmentID(custDepartmentID);
        //  vm.CustomerTicketPaging().SearchByCustomerDepartmentId();

        vm.CustomerDetailsVM().CustomerDepartmentID(ui.item.languageValue); ///////////////
        console.log('vm.CustomerDetailsVM().CustomerDepartmentID() ', vm.CustomerDetailsVM().CustomerDepartmentID());
        vm.CustomerDepartmentID(custDepartmentID);
        console.log('vm.CustomerDepartmentID() ', vm.CustomerDepartmentID());

        // populate Locations
        vm.CustomerDetailsVM().Locations([]);
        console.log('calling getLocations in selectCustomer'); /////////////////
        vm.getLocations(custDepartmentID);
        //vm.getLocations(vm.CustomerDetailsVM().CustomerDepartmentID());

        // populate with contacts
        vm.CustomerDetailsVM().Contacts([]);
        console.log('calling getContacts in selectCustomer'); ////////////////
        vm.getContacts(custDepartmentID);
        //vm.getContacts(vm.CustomerDepartmentID());
        // vm.getContacts(vm.CustomerDepartmentID);
        // vm.getContacts(vm.CustomerDetailsVM().CustomerDepartmentID());

        //  //populate with bank accounts
        //  vm.CustomerDetailsVM().BankAccounts ([]);
        //  vm.getBankAccounts(vm.CustomerDetailsVM().CustomerDepartmentID());

        //  //populate last weeks tickets 
        ////  vm.CustomerDetails().Tickets([]);
        // // vm.getTickets(vm.CustomerDetails().CustomerDepartmentID());


        //  //populate last weeks SalesInvoices
        //  vm.CustomerDetailsVM().SalesInvoices([]);
        //  vm.getSalesInvoices(vm.CustomerDetailsVM().CustomerDepartmentID());

        //  //populate Departments
        //  vm.CustomerDetailsVM().CustomerDepartments([]);
        //  vm.getDepartments(vm.CustomerDetailsVM().CustomerDepartmentID());
    };

    onCustomerChange = function (event, ui) {
        // if value is cleared, clear in ViewModel
        if (ui.item == null || ui.item.value == null || ui.item.value == '') {
            var vm = ko.dataFor(event.target);
            vm.CustomerDepartmentID("");
        }
    }

    getBankAccounts(customerDepartmentID, response) {
        var self = this;

        $.ajax({
            type: 'GET',
            url: '/api/BankAccount/GetAllCustomerDepartmentBankAccounts/?CustomerDepartmentID=' + customerDepartmentID,
            data: {
                json: '{}',
                delay: 0.5,
                search: customerDepartmentID
            },
            success: function (data) {
                self.CustomerDetailsVM().CustomerDepartmentID(customerDepartmentID);

                for (var i = 0; i < data.length; i++) {
                    self.CustomerDetailsVM().BankAccounts.push(data[i]);
                }
            }

        });

}
    getTickets(customerDepartmentID, response) {
        var self = this;

        $.ajax({
            type: 'GET',
            url: '/API/Ticket/GetAllCustomerDepartmentTickets/?CustomerDepartmentID=' + customerDepartmentID,
            data: {
                json: '{}',
                delay: 0.5,
                search: customerDepartmentID
            },
            success: function (data) {
                self.CustomerDetailsVM().CustomerDepartmentID(customerDepartmentID);

                for (var i = 0; i < data.length; i++) {
                    self.CustomerDetailsVM().Tickets.push(data[i]);
                }
            }

        });
    }
    getSalesInvoices(customerDepartmentID, response) {
        //populate Tickets
        var self = this;

        $.ajax({
            type: 'GET',
            url: '/API/Invoice/GetAllCustomerDepartmentSalesInvoices/?CustomerDepartmentID=' + customerDepartmentID,
            data: {
                json: '{}',
                delay: 0.5,
                search: customerDepartmentID
            },
            success: function (data) {
                self.CustomerDetailsVM().CustomerDepartmentID(customerDepartmentID);

                for (var i = 0; i < data.length; i++) {
                    self.CustomerDetailsVM().SalesInvoices.push(data[i]);
                }
            }

        });
    };
    getDepartments(customerDepartmentID, response) {
        //populate departments
        
        var self = this;
        $.ajax({
            type: 'GET',
            url: '/api/CustomerDepartment/GetCustomerDepartments/?CustomerDepartmentID=' + customerDepartmentID,
            data: {
                json: '{}',
                delay: 0.5,
                search: customerDepartmentID
            },
            success: function (data) {
                self.CustomerDetailsVM().CustomerDepartmentID(customerDepartmentID);

                for (var i = 0; i < data.length; i++) {
                    self.CustomerDetailsVM().CustomerDepartments.push(data[i]);
                }

            }
        });
    };

    getContacts(customerDepartmentID, response) {
        //Populate Contacts 
        var self = this;
        $.ajax({
            type: 'GET',
            url: '/api/Contact/GetAllCustomerDepartmentContacts/?CustomerDepartmentID=' + customerDepartmentID,
            data: {
                json: '{}',
                delay: 0.5,
                search: customerDepartmentID
            },
            success: function (data) {
                self.CustomerDetailsVM().CustomerDepartmentID(customerDepartmentID);
                console.log('getContacts (cusDepId='+ customerDepartmentID +')'); /////////
                console.log('getContacts data success'); //////////////////////////////////
                for (var i = 0; i < data.length; i++) {
                    self.CustomerDetailsVM().Contacts.push(data[i]);
                    console.log(data[i]); /////////////////////////////////////////////////
                }
            }
        });
    };

    getLocations(customerDepartmentID, response) {
        //Populate Locations 
        var self = this;
        $.ajax({
            type: 'GET',
            url: '/api/CustomerLocation/GetCustomerLocationModels/?CustomerDepartmentID=' + customerDepartmentID,
            data: {
                json: '{}',
                delay: 0.5,
                search: customerDepartmentID
            },
            success: function (data) {
                self.CustomerDetailsVM().CustomerDepartmentID(customerDepartmentID);
                console.log('getLocations (cusDepId=' + customerDepartmentID + ')'); ///////
                console.log('getLocations data success'); //////////////////////////////////
                for (var i = 0; i < data.length; i++) {
                    self.CustomerDetailsVM().Locations.push(data[i]);
                    console.log(data[i]); /////////////////////////////////////////////////
                }
            },
            error: function (data) {
                console.log('!!! getLocations failed !!!'); ////////////////////////
                console.log(data); /////////////////////////////////////////////////
            }
        });
    };

    ToggleLocationsVisible = function () {
        this.IsLocationsVisible(!this.IsLocationsVisible());
        //alert('Customer ToggleLocationsVisible ' + this.IsLocationsVisible());
    }
    ToggleContactsVisible = function () {
        this.IsContactsVisible(!this.IsContactsVisible());
    }
    ToggleTicketsVisible = function () {
        this.IsTicketsVisible(!this.IsTicketsVisible());    
    }
}
 