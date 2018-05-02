//-Settings_Add_&_View_Customer

var x__topic_cust = new ko.subscribable();

var s4__guid_cust = function () {
    function s4() {
        return Math.floor((1 + Math.random()) * 0x10000)
            .toString(16).substring(1);
    }
    return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
        s4() + '-' + s4() + s4() + s4();
}

var is__guid_cust = function (str_val) {
    if (str_val === undefined)
        return false;
    if (str_val[0] === "{") {
        str_val = str_val.substring(1, str_val.length - 1);
    }
    var regexGuid = /^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$/gi;
    return regexGuid.test(str_val);
}

// listbox
class LbxViewModel_cust {
    Id: KnockoutObservable<string>;
    label: KnockoutObservable<string>;
    value: KnockoutObservable<string>;
    constructor(data) {
        data = data || {};
        this.Id = ko.observable(data.Id);
        this.label = ko.observable(data.label);
        this.value = ko.observable(data.value);
    }
}

class CreateCustomerModel {
    customerModel: KnockoutObservable<CustomerModel>;
    constructor() {
        this.customerModel = ko.observable(new CustomerModel(null));
        this.customerModel().newContact(null, null);
        this.customerModel().newDepartment(null);
        this.customerModel().newLocation();
    }

    saveCustomerItems = function () {
        var self = this;
        if (self.customerModel().CustomerID() == undefined || self.customerModel().CustomerID() === "") {
            self.customerModel().saveCustomer("__create__");
        }
    }
}

class UpdateCustomerModel {
    customerModel: KnockoutObservable<CustomerModel>;
    constructor(data) {
        data = data || {};
        this.customerModel = ko.observable(new CustomerModel(data));
        this.customerModel().setCustomer(data);
        this.customerModel().setContact(data);
        this.customerModel().setDepartment(data);
        this.customerModel().setLocation(data);
    }

    saveCustomerItems = function () {
        var self = this;
        if (self.customerModel().CustomerID() == undefined || self.customerModel().CustomerID() === "") {
            self.customerModel().saveCustomer("__update__");
        }
    }
}

class ShowCustomerDetailsModel {
    customerModel: KnockoutObservable<CustomerModel>;
    constructor(data) {
        data = data || {};
        this.customerModel = ko.observable(new CustomerModel(data));
        this.customerModel().setCustomer(data);
        this.customerModel().setContact(data);
        this.customerModel().setDepartment(data);
        this.customerModel().setLocation(data);
    }
}

class CustomerLocations {
    koaCustomerLocations: KnockoutObservableArray<CustomerLocationVM>;
    constructor(data) {
        this.koaCustomerLocations = ko.observableArray<CustomerLocationVM>(data);
    }
}

class CustomerDepartments {
    koaCustomerDepartments: KnockoutObservableArray<CustomerDepartmentVM>;
    constructor(data) {
        this.koaCustomerDepartments = ko.observableArray<CustomerDepartmentVM>(data);
    }
}

class CustomerContacts {
    koaCustomerContacts: KnockoutObservableArray<CustomerContactVM>;
    constructor(data) {
        this.koaCustomerContacts = ko.observableArray<CustomerContactVM>(data);
    }
}

class CustomerModel {
    Errors: KnockoutValidationErrors;
    ShowErrors: KnockoutObservable<boolean>;
    CustomerID: KnockoutObservable<string>;
    CustomerCode: KnockoutObservable<string>;
    CustomerCompanyName: KnockoutObservable<string>;
    CustomerContacts: KnockoutObservableArray<CustomerContactVM>;
    CustomerLocations: KnockoutObservableArray<CustomerLocationVM>;
    CustomerDepartments: KnockoutObservableArray<CustomerDepartmentVM>;
    IsTransfer: KnockoutObservable<number>;
    Statements: KnockoutObservable<boolean>;
    ParentCustomerID: KnockoutObservable<string>;
    ParentCustomerName: KnockoutObservable<string>;
    CreditLimitCash: KnockoutObservable<number>;
    CreditLimitInvoice: KnockoutObservable<number>;
    CreditRatingID: KnockoutObservable<string>;
    CompanyID: KnockoutObservable<string>;
    CompanyName: KnockoutObservable<string>;
    CompanyOptions: KnockoutObservableArray<DropDownVM>;
    NoteID: KnockoutObservable<string>;
    Notes: KnockoutObservable<string>;
    NoteDescription: KnockoutObservable<string>;
    //TotalLocations: KnockoutObservable<number>
    //TotalDepartments: KnockoutObservable<number>
    //IsCustomerModelChanged: KnockoutObservable<boolean>;
    //IsCustomerSaveBtnEnabled: KnockoutObservable<boolean>;
    LbxGettingLocationsList: KnockoutObservable<number>;
    LbxGettingDepartmentsList: KnockoutObservable<number>;
    LbxRemovingLocationId: KnockoutObservable<string>;
    LbxRemovingDepartmentId: KnockoutObservable<string>;
    CreatedDate: KnockoutObservable<string>;
    CreatedBy: KnockoutObservable<string>;
    UpdatedDate: KnockoutObservable<string>;
    UpdatedBy: KnockoutObservable<string>;

    constructor(data) {
        data = data || {};
        this.Errors = ko.validation.group(this);
        this.ShowErrors = ko.observable(false);
        this.CustomerID = ko.observable("");
        this.CustomerCode = ko.observable("").extend({ required: true });
        this.CustomerCompanyName = ko.observable("").extend({ required: true });
        this.CustomerContacts = ko.observableArray<CustomerContactVM>([]);
        this.CustomerLocations = ko.observableArray<CustomerLocationVM>([]);
        this.CustomerDepartments = ko.observableArray<CustomerDepartmentVM>([]);
        this.IsTransfer = ko.observable(0);
        this.Statements = ko.observable(false);
        this.ParentCustomerID = ko.observable(data.ParentCustomerID);
        this.ParentCustomerName = ko.observable(data.ParentCustomerName);
        this.CreditLimitCash = ko.observable(data.CreditLimitCash).extend({ numeric: 2 });
        this.CreditLimitInvoice = ko.observable(data.CreditLimitInvoice).extend({ numeric: 2 });
        this.CreditRatingID = ko.observable(data.CreditRatingID);
        this.CompanyID = ko.observable(data.CompanyID);
        this.CompanyName = ko.observable(data.CompanyName);
        this.CompanyOptions = ko.observableArray<DropDownVM>([]);
        this.DDL_Company_options();
        this.NoteID = ko.observable(data.NoteID);
        this.Notes = ko.observable(data.Notes).extend({ required: true });
        this.NoteDescription = ko.observable(" "); // <-- Paul Edwards
        //this.TotalLocations = ko.observable(0);
        //this.TotalDepartments = ko.observable(0);
        //this.IsCustomerModelChanged = ko.observable(false);
        //this.IsCustomerSaveBtnEnabled = ko.observable(true); //false
        this.LbxGettingLocationsList = ko.observable(data.LbxGettingLocationsList);
        this.LbxGettingDepartmentsList = ko.observable(data.LbxGettingDepartmentsList);
        this.LbxRemovingLocationId = ko.observable(data.LbxRemovingLocationId);
        this.LbxRemovingDepartmentId = ko.observable(data.LbxRemovingDepartmentId);
        this.LbxGettingLocationsList.subscribe(this.pub_topic_lbx_Locations_list, this);
        this.LbxGettingDepartmentsList.subscribe(this.pub_topic_lbx_Departments_list, this);
        this.LbxRemovingLocationId.subscribe(this.pub_topic_removing_Location, this);
        this.LbxRemovingDepartmentId.subscribe(this.pub_topic_removing_Department, this);
        this.CreatedDate = ko.observable(data.CreatedDate);
        this.CreatedBy = ko.observable(data.CreatedBy);
        this.UpdatedDate = ko.observable(data.UpdatedDate);
        this.UpdatedBy = ko.observable(data.UpdatedBy);
    }

    pub_topic_lbx_Locations_list = function (x_no) {
        console.log('pub_topic_lbx_Locations_list', x_no, this.CustomerLocations());
        x__topic_cust.notifySubscribers(this.CustomerLocations(), 'topic_lbx_Locations_list');
    }

    pub_topic_lbx_Departments_list = function (x_no) {
        console.log('pub_topic_lbx_Departments_list', x_no, this.CustomerDepartments());
        x__topic_cust.notifySubscribers(this.CustomerDepartments(), 'topic_lbx_Departments_list');
    }

    pub_topic_removing_Location = function (x_id) {
        console.log('LbxRemovingLocationId', x_id);
        x__topic_cust.notifySubscribers(x_id, 'topic_removing_Location');
    }

    pub_topic_removing_Department = function (x_id) {
        console.log('LbxRemovingDepartmentId', x_id);
        x__topic_cust.notifySubscribers(x_id, 'topic_removing_Department');
    }

    DDL_Company_options = function () {
        var self = this;
        $.ajax({
            type: "GET",
            url: "/api/Company/DropDown",
            data: {
                json: "{}",
                delay: 0.5
            },
            success: function (data) {
                if (data != null) {
                    data.forEach(function (_item) {
                        self.CompanyOptions.push(new DropDownVM(_item.Id, _item.label, _item.value));
                    });
                }
            }
        });
        return [];
    };

    AC_get_ParentCustomer = function (request, ui) {
        var text = request.term;
        if (text === " " || text === "") return;
        console.log('AC_get_ParentCustomer - request.term ', text);
        $.ajax({
            type: "GET",
            url: "/api/Customer/AutoCompleteForPC/?search=" + text,
            data: {
                json: "{}",
                delay: 0.5,
                search: text
            },
            success: function (data) {
                if (data == null) return;
                console.log('AC_get_ParentCustomer - data ', data);
                ui($.map(data, function (language) {
                    return {
                        languageValue: language.Id,
                        label: language.label,
                        value: language.value
                    };
                }));
            }
        });
    }

    AC_select_ParentCustomer = function (event, ui) {
        console.log('AC_select_ParentCustomer: ', ui.item.label + '  ' + ui.item.languageValue);
        var vm = ko.dataFor(event.target);
        vm.customerModel().ParentCustomerID(ui.item.languageValue);
        vm.customerModel().ParentCustomerName(ui.item.label);
        console.log('ParentCustomerName: ', vm.customerModel().ParentCustomerName());
    }

    setCustomer = function (data) {
        var model = this;
        model.CustomerID(data.CustomerID);
        model.CustomerCode(data.CustomerCode);
        model.CustomerCompanyName(data.CustomerCompanyName);
        model.CustomerLocations(data.CustomerLocations);
        model.CustomerDepartments(data.CustomerDepartments);
        model.CustomerContacts(data.CustomerContacts);
        model.IsTransfer(data.IsTransfer);
        model.Statements(data.Statements);
        model.ParentCustomerID(data.ParentCustomerID);
        model.ParentCustomerName(data.ParentCustomerName);
        model.CompanyID(data.CompanyID);
        model.CompanyName(data.CompanyName);
        model.CreditRatingID(data.CreditRatingID);
        model.CreditLimitCash(data.CreditLimitCash);
        model.CreditLimitInvoice(data.CreditLimitInvoice);
        model.NoteID(data.NoteID);
        model.Notes(data.Notes);
        model.DDL_Company_options();
        //this.saveState(false);
    }

    newLocation = function () {
        var x_guid = '_' + s4__guid_cust();
        var _model = new CustomerLocationVM({ CustomerID: this.CustomerID() });
        _model.CustomerLocationName("Provide Customer Location Name");
        _model.x_CustomerLocationID(x_guid);
        _model.CustomerLocationID(x_guid);
        _model.ItemDeleting(false);
        _model.ItemAdding(true);
        _model.CustomerLocationName.subscribe(_model.pub_topic_Location_id_name, _model);
        this.CustomerLocations.push(_model);
        this.LbxGettingLocationsList(this.CustomerLocations().length);
        //this.saveState(false);
    }

    setLocation = function (data) {
        var items = data.CustomerLocations;
        for (var item of items) {
            var _model = new CustomerLocationVM(item);
            _model.ItemAdding(false);
            _model.ItemDeleting(false);
            _model.CustomerLocationName.subscribe(_model.pub_topic_Location_id_name, _model);
            data.CustomerLocations.splice(items.indexOf(item), 1, _model);
        }
        this.LbxGettingLocationsList(items.length);
        //this.saveState(false);
    }

    rmvLocation = function (index: number) {
        var _model = this.CustomerLocations.splice(index, 1)[0];
        _model.ItemAdding(false);
        _model.ItemDeleting(true);
        this.CustomerLocations.remove(_model);
        this.LbxRemovingLocationId(_model.CustomerLocationID());
        //this.saveState(false);
    }

    newDepartment = function (_locations) {
        var x_guid = '_' + s4__guid_cust();
        var _model = new CustomerDepartmentVM({ CustomerID: this.CustomerID() }, _locations);
        _model.CustomerDepartmentName("Provide Customer Department Name");
        _model.x_CustomerDepartmentID(x_guid);
        _model.CustomerDepartmentID(x_guid);
        _model.ItemDeleting(false);
        _model.ItemAdding(true);
        cust_DDL_CreditTerm_options(_model);
        x__topic_cust.subscribe(_model.sub_topic_Location_id_name, _model, "topic_Location_id_name");
        _model.CustomerDepartmentName.subscribe(_model.pub_topic_Department_id_name, _model);
        x__topic_cust.subscribe(_model.sub_topic_lbx_Locations_list, _model, "topic_lbx_Locations_list");
        x__topic_cust.subscribe(_model.sub_topic_removing_Location, _model, "topic_removing_Location");
        this.CustomerDepartments.push(_model);
        this.LbxGettingDepartmentsList(this.CustomerDepartments().length);
        //this.saveState(false);
    }

    setDepartment = function (data) {
        var items = data.CustomerDepartments;
        var _locations = data.CustomerLocations;
        for (var item of items) {
            var _model = new CustomerDepartmentVM(item, _locations);
            _model.ItemAdding(false);
            _model.ItemDeleting(false);
            cust_DDL_CreditTerm_options(_model);
            cust_dep_Selected_CreditTerm_option(_model);
            _model.LbxLocationOptions(item.LbxLocationOptions);
            _model.SelectedLocationIds(item.SelectedLocationIds);
            cust_dep_Selected_LbxLocation_options(_model);
            x__topic_cust.subscribe(_model.sub_topic_Location_id_name, _model, "topic_Location_id_name");
            _model.CustomerDepartmentName.subscribe(_model.pub_topic_Department_id_name, _model);
            x__topic_cust.subscribe(_model.sub_topic_lbx_Locations_list, _model, "topic_lbx_Locations_list");
            x__topic_cust.subscribe(_model.sub_topic_removing_Location, _model, "topic_removing_Location");
            data.CustomerDepartments.splice(items.indexOf(item), 1, _model);
        }
        this.LbxGettingDepartmentsList(items.length);
        //this.saveState(false);
    }

    rmvDepartment = function (index: number) {
        var _model = this.CustomerDepartments.splice(index, 1)[0];
        _model.ItemAdding(false);
        _model.ItemDeleting(true);
        this.CustomerDepartments.remove(_model);
        this.LbxRemovingDepartmentId(_model.CustomerDepartmentID());
        //this.saveState(false);
    }

    newContact = function (_locations, _departments) {
        var _model = new CustomerContactVM({ CustomerID: this.CustomerID() }, _locations, _departments);
        _model.ItemAdding(true);
        _model.ItemDeleting(false);
        x__topic_cust.subscribe(_model.sub_topic_Location_id_name, _model, "topic_Location_id_name");
        x__topic_cust.subscribe(_model.sub_topic_lbx_Locations_list, _model, "topic_lbx_Locations_list");
        x__topic_cust.subscribe(_model.sub_topic_removing_Location, _model, "topic_removing_Location");
        x__topic_cust.subscribe(_model.sub_topic_Department_id_name, _model, "topic_Department_id_name");
        x__topic_cust.subscribe(_model.sub_topic_lbx_Departments_list, _model, "topic_lbx_Departments_list");
        x__topic_cust.subscribe(_model.sub_topic_removing_Department, _model, "topic_removing_Department");
        this.CustomerContacts.push(_model);
        //this.saveState(false);
    }

    setContact = function (data) {
        var items = data.CustomerContacts;
        var _locations = data.CustomerLocations;
        var _departments = data.CustomerDepartments;
        for (var item of items) {
            var _model = new CustomerContactVM(item, _locations, _departments);
            _model.ItemAdding(false);
            _model.ItemDeleting(false);
            _model.LbxLocationOptions(item.LbxLocationOptions);
            _model.SelectedLocationIds(item.SelectedLocationIds);
            _model.LbxDepartmentOptions(item.LbxDepartmentOptions);
            _model.SelectedDepartmentIds(item.SelectedDepartmentIds);
            cust_con_Selected_LbxLocation_options(_model);
            cust_con_Selected_LbxDepartment_options(_model);
            x__topic_cust.subscribe(_model.sub_topic_Location_id_name, _model, "topic_Location_id_name");
            x__topic_cust.subscribe(_model.sub_topic_lbx_Locations_list, _model, "topic_lbx_Locations_list");
            x__topic_cust.subscribe(_model.sub_topic_removing_Location, _model, "topic_removing_Location");
            x__topic_cust.subscribe(_model.sub_topic_Department_id_name, _model, "topic_Department_id_name");
            x__topic_cust.subscribe(_model.sub_topic_lbx_Departments_list, _model, "topic_lbx_Departments_list");
            x__topic_cust.subscribe(_model.sub_topic_removing_Department, _model, "topic_removing_Department");
        }
        //this.saveState(false);
    }

    rmvContact = function (index: number) {
        var _item = this.CustomerContacts.splice(index, 1)[0];
        this.CustomerContacts.remove(_item);
        //this.saveState(false);
    }

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

    serverErrors = ko.observableArray([]);

    saveCustomer = (msg_text) => {
        console.log('saveCustomer(' + msg_text + ') - called');
        var _model = this;
        var json_data = ko.toJSON(_model);
        var request = $.ajax({
            type: 'POST',
            data: json_data,
            url: '/api/Customer/CreateCustomer',
            contentType: 'application/json; charset=utf-8',
            async: false
        });
        request.done(function (resp) {
            if (resp != null) {
                _model.CustomerID(resp.Data);
                json_data = JSON.stringify(resp);
            }
        });
        request.fail(function (jqXHR, textStatus, error) {
            if (textStatus != "success")
                alert(jqXHR.statusText + '\n' + jqXHR.responseText + '\n' + error);
        });
        request.always(function (jqXHR, textStatus) {
            var log_text = "saveCustomer - respons:\n" + json_data;
            console.log(log_text);
        });
    }
}

class CustomerLocationVM {
    CustomerLocationID: KnockoutObservable<string>;
    x_CustomerLocationID: KnockoutObservable<string>;
    CustomerLocationName: KnockoutObservable<string>;
    CustomerID: KnockoutObservable<string>;
    TelephoneNumber: KnockoutObservable<string>;
    FaxNumber: KnockoutObservable<string>;
    NoteID: KnockoutObservable<string>;
    Notes: KnockoutObservable<string>;
    NoteDescription: KnockoutObservable<string>;
    AddressID: KnockoutObservable<string>;
    AddressLine1: KnockoutObservable<string>;
    AddressLine2: KnockoutObservable<string>;
    AddressLine3: KnockoutObservable<string>;
    CountyCity: KnockoutObservable<string>;
    PostalTown: KnockoutObservable<string>;
    Postcode: KnockoutObservable<string>;
    ItemAdding: KnockoutObservable<boolean>;
    ItemDeleting: KnockoutObservable<boolean>;

    constructor(data) {
        data = data || {};
        this.CustomerLocationID = ko.observable(data.CustomerLocationID);
        this.x_CustomerLocationID = ko.observable(data.x_CustomerLocationID);
        this.CustomerLocationName = ko.observable(data.CustomerLocationName).extend({ required: true });
        this.CustomerID = ko.observable(data.CustomerID);
        this.TelephoneNumber = ko.observable(data.TelephoneNumber);
        this.FaxNumber = ko.observable(data.FaxNumber);
        this.NoteID = ko.observable(data.NoteID);
        this.Notes = ko.observable(data.Notes).extend({ required: true });
        this.NoteDescription = ko.observable(data.NoteDescription).extend({ required: true });
        this.AddressID = ko.observable(data.AddressID);
        this.AddressLine1 = ko.observable(data.AddressLine1).extend({ required: true });
        this.AddressLine2 = ko.observable(data.AddressLine2);
        this.AddressLine3 = ko.observable(data.AddressLine3);
        this.CountyCity = ko.observable(data.CountyCity).extend({ required: true });
        this.PostalTown = ko.observable(data.PostalTown).extend({ required: true });
        this.Postcode = ko.observable(data.Postcode).extend({ required: true });
        this.ItemAdding = ko.observable(data.ItemAdding);
        this.ItemDeleting = ko.observable(data.ItemDeleting);
    }

    pub_topic_Location_id_name = function (x_name) {
        var lbx_item = new LbxViewModel_cust({});
        lbx_item.Id(this.CustomerLocationID());
        lbx_item.label(x_name);
        lbx_item.value(this.CustomerLocationID());
        console.log('topic_Location_id_name ==> ****', lbx_item);
        x__topic_cust.notifySubscribers(lbx_item, 'topic_Location_id_name');
    }
}

class CustomerDepartmentVM {
    CustomerDepartmentID: KnockoutObservable<string>;
    x_CustomerDepartmentID: KnockoutObservable<string>;
    CustomerDepartmentName: KnockoutObservable<string>;
    CustomerID: KnockoutObservable<string>;
    EmailAddress: KnockoutObservable<string>;
    Commission: KnockoutObservable<number>;
    Handling: KnockoutObservable<number>;
    TransactionTaxReference: KnockoutObservable<string>;
    FactorRef: KnockoutObservable<string>;
    RebateType: KnockoutObservable<number>;
    RebateRate: KnockoutObservable<number>;
    NoteID: KnockoutObservable<string>;
    Notes: KnockoutObservable<string>;
    NoteDescription: KnockoutObservable<string>;
    CustomerTypeID: KnockoutObservable<string>;
    CustomerTypeDescription: KnockoutObservable<string>;
    SalesPersonUserID: KnockoutObservable<string>;
    SalesPersonUserName: KnockoutObservable<string>;
    RebateCustomerDepartmentID: KnockoutObservable<string>;
    RebateCustomerDepartmentName: KnockoutObservable<string>;
    CreditLimit: KnockoutObservable<number>;
    CreditTerm: KnockoutObservable<number>;
    InvoiceFrequency: KnockoutObservable<number>;
    InvoiceEmailAddress: KnockoutObservable<string>;
    InvoiceCustomerLocationID: KnockoutObservable<string>;
    InvoiceCustomerLocationName: KnockoutObservable<string>;
    // list of radio buttons representing each of RebateType Options
    RebateTypeOptions: KnockoutObservableArray<string>;
    RebateTypeRBobj: KnockoutObservable<RadioButtonVM>;
    UpdateRebateType: KnockoutComputed<void>;
    // DDL options
    CreditTermOptions: KnockoutObservableArray<MyCustomOption>;
    SalesPersonUserOptions: KnockoutObservableArray<DropDownVM>;
    RebateCustomerDepartmentOptions: KnockoutObservableArray<DropDownVM>;
    InvoiceCustomerLocationOptions: KnockoutObservableArray<DropDownVM>;
    /*
    EDIType: KnockoutObservable<number>;
    EDINumber: KnockoutObservable<string>;
    EDIIdent: KnockoutObservable<string>;
    */
    ItemAdding: KnockoutObservable<boolean>;
    ItemDeleting: KnockoutObservable<boolean>;
    SelectedLocationIds: KnockoutObservableArray<string>;
    LbxLocationOptions: KnockoutObservableArray<LbxViewModel_cust>;
    
    constructor(data, _locations) {
        data = data || {};
        _locations = _locations || {};
        this.CustomerDepartmentID = ko.observable(data.CustomerDepartmentID);
        this.x_CustomerDepartmentID = ko.observable(data.x_CustomerDepartmentID);
        this.CustomerDepartmentName = ko.observable(data.CustomerDepartmentName).extend({ required: true });
        this.CustomerID = ko.observable(data.CustomerID);
        this.EmailAddress = ko.observable(data.EmailAddress);
        this.Commission = ko.observable(data.Commission).extend({ numeric: 2 });
        this.Handling = ko.observable(data.Handling).extend({ numeric: 2 });
        this.TransactionTaxReference = ko.observable(data.TransactionTaxReference);
        this.FactorRef = ko.observable(data.FactorRef);
        this.RebateType = ko.observable(data.RebateType);
        this.RebateRate = ko.observable(data.RebateRate).extend({ numeric: 2 });
        this.NoteID = ko.observable(data.NoteID);
        this.Notes = ko.observable(data.Notes).extend({ required: true });
        this.NoteDescription = ko.observable(data.NoteDescription).extend({ required: true });
        this.CustomerTypeID = ko.observable(data.CustomerTypeID); // AC
        this.CustomerTypeDescription = ko.observable(data.CustomerTypeDescription);
        this.SalesPersonUserID = ko.observable(data.SalesPersonUserID); // DDL
        this.SalesPersonUserName = ko.observable(data.SalesPersonUserName);
        this.RebateCustomerDepartmentID = ko.observable(data.RebateCustomerDepartmentID); // DDL
        this.RebateCustomerDepartmentName = ko.observable(data.RebateCustomerDepartmentName);
        this.CreditLimit = ko.observable(data.CreditLimit).extend({ numeric: 2 });;
        this.CreditTerm = ko.observable(0); // DDL
        this.InvoiceFrequency = ko.observable(data.InvoiceFrequency);
        this.InvoiceEmailAddress = ko.observable(data.InvoiceEmailAddress);
        this.InvoiceCustomerLocationID = ko.observable(data.InvoiceCustomerLocationID); // DDL
        this.InvoiceCustomerLocationName = ko.observable(data.InvoiceCustomerLocationName);
        // list of radio buttons representing each of RebateType Options
        this.RebateTypeOptions = ko.observableArray(['Percentage', 'Value']);
        this.RebateTypeRBobj = ko.observable<RadioButtonVM>(new RadioButtonVM('Percentage', 0));
        this.UpdateRebateType = ko.computed({
            read: () => {
                this.RebateTypeRBobj();
                this.RebateType(this.RebateTypeRBobj().rbIndx);
            }
        });
        // DDL options - init !
        this.RebateCustomerDepartmentOptions = ko.observableArray<DropDownVM>([]);
        this.DDL_RebateCustomerDepartment_options();
        this.SalesPersonUserOptions = ko.observableArray<DropDownVM>([]);
        this.DDL_SalesPersonUser_options();
        this.CreditTermOptions = ko.observableArray<MyCustomOption>([]);
        this.CreditTermOptions.push(new MyCustomOption("1 day", "1"));
        this.CreditTermOptions.push(new MyCustomOption("5 days", "5"));
        this.CreditTermOptions.push(new MyCustomOption("1 week", "7"));
        this.CreditTermOptions.push(new MyCustomOption("2 weeks", "14"));
        this.CreditTermOptions.push(new MyCustomOption("3 weeks", "21"));
        this.CreditTermOptions.push(new MyCustomOption("1 month", "30"));
        this.CreditTermOptions.push(new MyCustomOption("2 months", "60"));
        this.CreditTermOptions.push(new MyCustomOption("3 months", "90"));
        /*         ~~~EDI~~~  I was told not to implement      */
        this.ItemAdding = ko.observable(data.ItemAdding);
        this.ItemDeleting = ko.observable(data.ItemDeleting);
        this.SelectedLocationIds = ko.observableArray<string>([]).extend({ notify: 'always' });
        this.LbxLocationOptions = ko.observableArray<LbxViewModel_cust>([]).extend({ notify: 'always' });
        this.InvoiceCustomerLocationOptions = ko.observableArray<DropDownVM>([]).extend({ notify: 'always' });
        //this.DDL_InvoiceCustomerLocation_options();
    }

    pub_topic_Department_id_name = function (x_name) {
        //debugger; ////////////////////////////////////
        var lbx_item = new LbxViewModel_cust({});
        lbx_item.Id(this.CustomerDepartmentID());
        lbx_item.label(x_name);
        lbx_item.value(this.CustomerDepartmentID());
        console.log('topic_Department_id_name ==> ****', lbx_item);
        x__topic_cust.notifySubscribers(lbx_item, 'topic_Department_id_name');
    }

    sub_topic_Location_id_name = function (x_itm: LbxViewModel_cust) {
        console.log("topic_Location_id_name --> depa", x_itm);
        // lbx
        var x_len = this.LbxLocationOptions().length;
        if (x_len > 0) {
            var x_id = x_itm.Id();
            console.log(x_id);
            var del_id = "??? --- ()() --- !!!";
            var _remove = false;
            this.LbxLocationOptions.remove(item => {
                try {
                    del_id = item.Id()(); // <--- ??? --- ()() --- !!! Paul Edwards solution
                } catch (e) {
                    del_id = item.Id(); // <--- ??? --- () --- !!! Paul Edwards solution
                }
                _remove = (x_id === del_id);
                console.log(x_id, _remove, del_id, item);
                return _remove;
            });
            this.LbxLocationOptions.valueHasMutated();
        }
        this.LbxLocationOptions.push(x_itm);
        this.LbxLocationOptions.valueHasMutated();
        console.log(this.LbxLocationOptions());
        // ddl
        var d_len = this.InvoiceCustomerLocationOptions().length;
        if (d_len > 0) {
            var d_id = x_itm.Id();
            console.log(d_id);
            var del_id = "??? --- ()() --- !!!";
            var _remove = false;
            this.InvoiceCustomerLocationOptions.remove(item => {
                try {
                    del_id = item.Id()(); // <--- ??? --- ()() --- !!! Paul Edwards solution
                } catch (e) {
                    del_id = item.Id(); // <--- ??? --- () --- !!! Paul Edwards solution
                }
                _remove = (d_id === del_id);
                console.log(d_id, _remove, del_id, item);
                return _remove;
            });
            this.InvoiceCustomerLocationOptions.valueHasMutated();
        }
        this.InvoiceCustomerLocationOptions.push(x_itm);
        this.InvoiceCustomerLocationOptions.valueHasMutated();
    }

    sub_topic_lbx_Locations_list = function (_locations: KnockoutObservableArray<CustomerLocationVM>) {
        // lbx
        var lbx_options = ko.observableArray([]);
        this.LbxLocationOptions.removeAll();
        var total = _locations.length;
        console.log('Department sub_topic_lbx_Locations_list', total);
        for (var i = 0; i < total; i++) {
            var item = new CustomerLocationVM(_locations[i]);
            var obj_item = ko.toJS(item);
            if (obj_item.CustomerLocationID !== undefined) {
                var lbx_item = new LbxViewModel_cust({});
                lbx_item.Id(obj_item.CustomerLocationID);
                lbx_item.label(obj_item.CustomerLocationName);
                lbx_item.value(obj_item.CustomerLocationID);
                lbx_options.push(lbx_item);
            }
        }
        console.log('lbx_options', lbx_options());
        ko.utils.arrayPushAll(this.LbxLocationOptions, lbx_options());
        this.LbxLocationOptions.valueHasMutated();
        console.log('LbxLocationOptions', this.LbxLocationOptions());
        // ddl
        var ddl_options = ko.observableArray([]);
        this.InvoiceCustomerLocationOptions.removeAll();
        var total = _locations.length;
        console.log('Contact sub_topic_lbx_Locations_list', total);
        for (var i = 0; i < total; i++) {
            var item = new CustomerLocationVM(_locations[i]);
            var obj_item = ko.toJS(item);
            if (obj_item.CustomerLocationID !== undefined) {
                var ddl_item = new LbxViewModel_cust({});
                ddl_item.Id(obj_item.CustomerLocationID);
                ddl_item.label(obj_item.CustomerLocationName);
                ddl_item.value(obj_item.CustomerLocationID);
                ddl_options.push(ddl_item);
            }
        }
        console.log('ddl_options', ddl_options());
        ko.utils.arrayPushAll(this.InvoiceCustomerLocationOptions, ddl_options());
        this.InvoiceCustomerLocationOptions.valueHasMutated();
        console.log('InvoiceCustomerLocationOptions', this.InvoiceCustomerLocationOptions());
    }

    sub_topic_removing_Location = function (x_id) {
        console.log("sub_topic_removing_Location --> depa", x_id);
        // lbx
        var x_len = this.LbxLocationOptions().length;
        if (x_len > 0) {
            console.log(x_id);
            var del_id = "??? --- ()() --- !!!";
            var _remove = false;
            this.LbxLocationOptions.remove(item => {
                try {
                    del_id = item.Id()(); // <--- ??? --- ()() --- !!! Paul Edwards solution
                } catch (e) {
                    del_id = item.Id(); // <--- ??? --- () --- !!! Paul Edwards solution
                }
                _remove = (x_id === del_id);
                console.log(x_id, _remove, del_id, item);
                return _remove;
            });
            this.LbxLocationOptions.valueHasMutated();
        }
        console.log(this.LbxLocationOptions());
        // ddl
        var d_len = this.InvoiceCustomerLocationOptions().length;
        if (d_len > 0) {
            console.log(x_id);
            var del_id = "??? --- ()() --- !!!";
            var _remove = false;
            this.InvoiceCustomerLocationOptions.remove(item => {
                try {
                    del_id = item.Id()(); // <--- ??? --- ()() --- !!! Paul Edwards solution
                } catch (e) {
                    del_id = item.Id(); // <--- ??? --- () --- !!! Paul Edwards solution
                }
                _remove = (x_id === del_id);
                console.log(x_id, _remove, del_id, item);
                return _remove;
            });
            this.InvoiceCustomerLocationOptions.valueHasMutated();
        }
        console.log(this.InvoiceCustomerLocationOptions());
    }

    is_newXloc_item = function (x_itm_id) {
        var x_len = this.LbxLocationOptions().length;
        for (var i = 0; i < x_len; i++) {
            var x_opt_id = JSON.stringify(this.LbxLocationOptions()[i].Id);
            if (x_itm_id === x_opt_id)
                return false;
        }
        return true;
    }

    AC_get_CustomerType = function (request, ui) {
        var text = request.term;
        console.log('AC_get_CustomerType - request.term ', text);
        if (text === " " || text === "") return;
        var ex_msg = '';
        var resp_json = {};
        var _ajax = $.ajax({
            type: "GET",
            url: "/api/CustomerType/AutoComplete/?search=" + text,
            data: {
                json: "{}",
                delay: 0.5,
                search: text
            }
        });
        _ajax.done(function (resp) {
            resp_json = JSON.stringify(resp);
            //debugger; /////////////////////
            if (resp == null) return;
            ui($.map(resp, function (language) {
                    return {
                        languageValue: language.Id,
                        label: language.label,
                        value: language.value
                    };
                }));
        });
        _ajax.fail(function (jqXHR, textStatus, error) {
            if (textStatus != "success") {
                ex_msg = 'jqXHR.statusText: ' +
                jqXHR.statusText + '\n Response: ' +
                jqXHR.responseText + '\n Error: ' + error;
                alert(ex_msg);
            }
        });
        _ajax.always(function (jqXHR, textStatus) {
            if (textStatus === "success")
                console.log("AC_get_CustomerType - respons:\n" + JSON.stringify(resp_json));
            else console.log(ex_msg);
        });
    }

    AC_select_CustomerType = function (event, ui) {
        console.log('AC_select_CustomerType: ', ui.item.label + '  ' + ui.item.languageValue);
        var vm = ko.dataFor(event.target);
        vm.CustomerTypeID(ui.item.languageValue);
        vm.CustomerTypeDescription(ui.item.label);
    }

    DDL_RebateCustomerDepartment_options = function () {
        var self = this;
        $.ajax({
            type: "GET",
            url: "/api/CustomerDepartment/DDLRebateCustomerDepartment",
            data: {
                json: "{}",
                delay: 0.5
            },
            success: function (data) {
                console.log('DDL_RebateCustomerDepartment_options - data ', data);
                //debugger; //////////////////
                if (data != null) {
                    data.forEach(function (item) {
                        self.RebateCustomerDepartmentOptions.push(new DropDownVM(item.Id, item.label, item.value));
                    });
}
            },
            error: function (request, status, error) {
                var err = eval("(" + request.responseText + ")");
                alert(err.Message + '\n' + request.responseText);
                console.log(err.Message + '\n' + request.responseText);
            } 
        });
        return [];
    }

    DDL_SalesPersonUser_options = function () {
        var self = this;
        $.ajax({
            type: "GET",
            url: "/api/SalesPerson/DropDown",
            data: {
                json: "{}",
                delay: 0.5
            },
            success: function (data) {
                console.log('DDL_SalesPersonUser_options - data ', data);
                //debugger; //////////////////
                if (data != null) {
                    data.forEach(function (item) {
                        self.SalesPersonUserOptions.push(new DropDownVM(item.Id, item.label, item.value));
                    });
                }
            },
            error: function (request, status, error) {
                var err = eval("(" + request.responseText + ")");
                alert(err.Message + '\n' + request.responseText);
                console.log(err.Message + '\n' + request.responseText);
            }
        });
        return [];
    }

    //DDL_InvoiceCustomerLocation_options = function () {
    //    var self = this;
    //    var cdId = '76009305-0000-0070-9704-000068336078'; ///////////////////////
    //    $.ajax({
    //        type: "GET",
    //        url: "/api/CustomerLocation/DDLInvoiceCustomerLocation/?cdId=" + cdId, //////
    //        data: {
    //            json: "{}",
    //            delay: 0.5
    //        },
    //        success: function (data) {
    //            console.log('DDL_InvoiceCustomerLocation_options - data ', data);
    //            //debugger; //////////////////
    //            if (data != null) {
    //                data.forEach(function (item) {
    //                    self.InvoiceCustomerLocationOptions.push(new DropDownVM(item.Id, item.label, item.value));
    //                });
    //            }
    //        },
    //        error: function (request, status, error) {
    //            var err = eval("(" + request.responseText + ")");
    //            alert(err.Message + '\n' + request.responseText);
    //            console.log(err.Message + '\n' + request.responseText);
    //        }
    //    });
    //    return [];
    //}
}

var cust_DDL_CreditTerm_options = function (item: CustomerDepartmentVM) {
    if (item.CreditTermOptions == undefined)
        item.CreditTermOptions = ko.observableArray<MyCustomOption>([]);
    item.CreditTermOptions.push(new MyCustomOption("1 day", "1"));
    item.CreditTermOptions.push(new MyCustomOption("5 days", "5"));
    item.CreditTermOptions.push(new MyCustomOption("1 week", "7"));
    item.CreditTermOptions.push(new MyCustomOption("2 weeks", "14"));
    item.CreditTermOptions.push(new MyCustomOption("3 weeks", "21"));
    item.CreditTermOptions.push(new MyCustomOption("1 month", "30"));
    item.CreditTermOptions.push(new MyCustomOption("2 months", "60"));
    item.CreditTermOptions.push(new MyCustomOption("3 months", "90"));
}

var cust_dep_Selected_CreditTerm_option = function (item: CustomerDepartmentVM) {
    var selected = item.CreditTerm;
    switch (selected.toString()) {
        case '90': $("#credit_term option[value='90']").attr('selected', 'selected'); break;
        case '60': $("#credit_term option[value='60']").attr('selected', 'selected'); break;
        case '30': $("#credit_term option[value='30']").attr('selected', 'selected'); break;
        case '21': $("#credit_term option[value='21']").attr('selected', 'selected'); break;
        case '14': $("#credit_term option[value='14']").attr('selected', 'selected'); break;
        case '7': $("#credit_term option[value='7']").attr('selected', 'selected'); break;
        case '5': $("#credit_term option[value='5']").attr('selected', 'selected'); break;
        default: $("#credit_term option[value='1']").attr('selected', 'selected');
    }
}

var cust_dep_Selected_LbxLocation_options = function (item: CustomerDepartmentVM) {
    var length = item.SelectedLocationIds.length;
    for (var i = 0; i < length; i++) {
        var selected = item.SelectedLocationIds[i];
        $('#lbx_loc_of_dep').children('option[value="' + selected + '"]').attr('selected', 'selected');
    }
}

class CustomerContactVM  {
    CustomerContactID: KnockoutObservable<string>;
    CustomerID: KnockoutObservable<string>;
    ContactID: KnockoutObservable<string>;
    // Contact fields
    ContactType: KnockoutObservable<string>;
    FirstName: KnockoutObservable<string>;
    LastName: KnockoutObservable<string>;
    Title: KnockoutObservable<string>;
    ContactReference: KnockoutObservable<string>;
    EmailAddress: KnockoutObservable<string>;
    DDITelephoneNo: KnockoutObservable<string>;
    MobileNo: KnockoutObservable<string>;
    NoteID: KnockoutObservable<string>;
    Notes: KnockoutObservable<string>;
    NoteDescription: KnockoutObservable<string>;
    SortOrder: KnockoutObservable<string>; // <-- ???
    ItemAdding: KnockoutObservable<boolean>;
    ItemDeleting: KnockoutObservable<boolean>;
    SelectedLocationIds: KnockoutObservableArray<string>;
    SelectedDepartmentIds: KnockoutObservableArray<string>;
    LbxLocationOptions: KnockoutObservableArray<LbxViewModel_cust>;
    LbxDepartmentOptions: KnockoutObservableArray<LbxViewModel_cust>;

    constructor(data, _locations, _departments) {
        data = data || {};
        _locations = _locations || {};
        _departments = _departments || {};
        this.CustomerContactID = ko.observable(data.CustomerContactID);
        this.CustomerID = ko.observable(data.CustomerID);
        this.ContactID = ko.observable(data.ContactID);
        // Contact fields
        this.ContactType = ko.observable(data.ContactType).extend({ required: true });
        this.FirstName = ko.observable(data.FirstName).extend({ required: true });
        this.LastName = ko.observable(data.LastName).extend({ required: true });
        this.Title = ko.observable(data.Title);
        this.ContactReference = ko.observable(" "); // <-- Paul Edwards
        this.EmailAddress = ko.observable(data.EmailAddress);
        this.DDITelephoneNo = ko.observable(data.DDITelephoneNo);
        this.MobileNo = ko.observable(data.MobileNo);
        this.NoteID = ko.observable(data.NoteID);
        this.Notes = ko.observable(data.Notes);
        this.NoteDescription = ko.observable(" "); // <-- Paul Edwards       
        this.SortOrder = ko.observable(data.SupplierContactID); // <-- Paul Edwards
        this.ItemAdding = ko.observable(data.ItemAdding);
        this.ItemDeleting = ko.observable(data.ItemDeleting);
        this.SelectedLocationIds = ko.observableArray<string>([]).extend({ notify: 'always' });
        this.SelectedDepartmentIds = ko.observableArray<string>([]).extend({ notify: 'always' });
        this.LbxLocationOptions = ko.observableArray<LbxViewModel_cust>([]).extend({ notify: 'always' });
        this.LbxDepartmentOptions = ko.observableArray<LbxViewModel_cust>([]).extend({ notify: 'always' });
    }

    sub_topic_Location_id_name = function (x_itm: LbxViewModel_cust) {
        console.log("topic_Location_id_name --> cont", x_itm);
        var x_len = this.LbxLocationOptions().length;
        //debugger; ///////////////////////////////////
        if (x_len > 0) {
            var x_id = x_itm.Id();
            console.log(x_id);
            var del_id = "??? --- ()() --- !!!";
            var _remove = false;
            this.LbxLocationOptions.remove(item => {
                try {
                    del_id = item.Id()(); // <--- ??? --- ()() --- !!! Paul Edwards solution !!! --- ???
                } catch (e) {
                    del_id = item.Id(); // <--- ??? --- () --- !!! Paul Edwards solution !!! --- ???
                }
                _remove = (x_id === del_id);
                console.log(x_id, _remove, del_id, item);
                return _remove;
            });
            this.LbxLocationOptions.valueHasMutated();
        }
        this.LbxLocationOptions.push(x_itm);
        this.LbxLocationOptions.valueHasMutated();
        console.log(this.LbxLocationOptions());
    }
    /*
    sub_topic_Location_id_name = function(x_itm: LbxViewModel_cust) {
        console.log("topic_Location_id_name --> cont", x_itm);
        var x_len = this.LbxLocationOptions().length;
        //debugger; ///////////////////////////////////
        if (x_len === 0)
            this.LbxLocationOptions.push(x_itm);
        else {
            this.LbxLocationOptions.splice(this.LbxLocationOptions.indexOf(x_itm), 1, x_itm);
            this.LbxLocationOptions.valueHasMutated();
        }
        console.log(this.LbxLocationOptions());
    }
    */
    sub_topic_Department_id_name = function (x_itm: LbxViewModel_cust) {
        console.log("topic_Department_id_name --> cont", x_itm);
        var x_len = this.LbxDepartmentOptions().length;
        //debugger; ///////////////////////////////////
        if (x_len === 0)
            this.LbxDepartmentOptions.push(x_itm);
        else for (var i = 0; i < x_len; i++) {
            var x_itm_id = JSON.stringify(x_itm.Id());
            var x_opt_id = JSON.stringify(this.LbxDepartmentOptions()[i].Id());
            if (x_itm_id === x_opt_id) {
                this.LbxDepartmentOptions()[i].label(x_itm.label());
                this.LbxDepartmentOptions()[i].label.valueHasMutated();
            }
            else if (this.is_newXdep_item(x_itm_id))
                this.LbxDepartmentOptions.push(x_itm);
            this.LbxDepartmentOptions.valueHasMutated();
            //console.log(this.LbxDepartmentOptions());
        }
    }

    sub_topic_lbx_Locations_list = function (_locations: KnockoutObservableArray<CustomerLocationVM>) {
        var lbx_options = ko.observableArray([]);
        this.LbxLocationOptions.removeAll();
        var total = _locations.length;
        console.log('Contact sub_topic_lbx_Locations_list', total);
        for (var i = 0; i < total; i++) {
            var item = new CustomerLocationVM(_locations[i]);
            var obj_item = ko.toJS(item);
            if (obj_item.CustomerLocationID !== undefined) {
                var lbx_item = new LbxViewModel_cust({});
                lbx_item.Id(obj_item.CustomerLocationID);
                lbx_item.label(obj_item.CustomerLocationName);
                lbx_item.value(obj_item.CustomerLocationID);
                lbx_options.push(lbx_item);
            }
        }
        console.log('lbx_options', lbx_options());
        ko.utils.arrayPushAll(this.LbxLocationOptions, lbx_options());
        this.LbxLocationOptions.valueHasMutated();
        console.log('LbxLocationOptions', this.LbxLocationOptions());
    }
    /*
    sub_topic_lbx_Locations_list = function (_locations: KnockoutObservableArray<SupplierLocationVM>) {
        var lbx_len = this.LbxLocationOptions().length;
        var total = _locations.length;
        console.log('Contact:');
        //debugger; ///////////////////////////////////
        if (lbx_len !== total) {
            for (var i = 0; i < total; i++) {
                var item = new SupplierLocationVM(_locations[i]);
                var x_id = item.SupplierLocationID();
                if (x_id !== undefined) {
                    console.log('index', i, 'x_id', x_id); //////////////////////
                    var lbx_item = new LbxViewModel_cust({});
                    lbx_item.Id(x_id);
                    lbx_item.value(x_id);
                    if (i < lbx_len) {
                        var x_name = item.SupplierLocationName();
                        console.log('index', i, 'x_name', x_name); //////////////////////
                        lbx_item.label(x_name);
                        this.LbxLocationOptions.splice(i, 1, lbx_item);
                    }
                    else
                        if (i === lbx_len || i === total - 1) {
                            lbx_item.label("!!! Supplier Location Name " + (i + 1).toString());
                            this.LbxLocationOptions.push(lbx_item);
                        }
                    console.log('index', i, 'lbx_loc', this.LbxLocationOptions()[i]); ////////////////////////
                }
                else console.log('--------!?!----------'); //////////////////////
            }
            this.LbxLocationOptions.valueHasMutated();
        }
        console.log('total', total, 'lbx-locs', this.LbxLocationOptions());
    }
    */

    sub_topic_lbx_Departments_list = function (_departments: KnockoutObservableArray<CustomerDepartmentVM>) {
        var lbx_options = ko.observableArray([]);
        this.LbxDepartmentOptions.removeAll();
        var total = _departments.length;
        console.log('Contact sub_topic_lbx_Departments_list', total);
        //debugger; ///////////////////////////////////
        for (var i = 0; i < total; i++) {
            var item = new CustomerDepartmentVM(_departments[i], null);
            var obj_item = ko.toJS(item);
            if (obj_item.CustomerDepartmentID !== undefined) {
                var lbx_item = new LbxViewModel_cust({});
                lbx_item.Id(obj_item.CustomerDepartmentID);
                lbx_item.label(obj_item.CustomerDepartmentName);
                lbx_item.value(obj_item.CustomerDepartmentID);
                lbx_options.push(lbx_item);
            }
        }
        console.log('lbx_options', lbx_options());
        ko.utils.arrayPushAll(this.LbxDepartmentOptions, lbx_options());
        this.LbxDepartmentOptions.valueHasMutated();
        console.log('LbxDepartmentOptions', this.LbxDepartmentOptions());
    }
    /*
    sub_topic_lbx_Departments_list = function (_departments: KnockoutObservableArray<SupplierDepartmentVM>) {
        var lbx_len = this.LbxDepartmentOptions().length;
        var total = _departments.length;
        console.log('Contact:');
        //debugger; ///////////////////////////////////
        if (lbx_len !== total) {
            for (var i = 0; i < total; i++) {
                var item = new SupplierDepartmentVM(_departments[i], null); ////////////////////
                var x_id = item.SupplierDepartmentID();
                if (x_id !== undefined) {
                    console.log('index', i, 'x_id', x_id); //////////////////////
                    var lbx_item = new LbxViewModel_cust({});
                    lbx_item.Id(x_id);
                    lbx_item.value(x_id);
                    if (i < lbx_len) {
                        var x_name = item.SupplierDepartmentName();
                        console.log('index', i, 'x_name', x_name); //////////////////////
                        lbx_item.label(x_name);
                        this.LbxDepartmentOptions.splice(i, 1, lbx_item);
                    }
                    else
                        if (i === lbx_len || i === total - 1) {
                            lbx_item.label("!!! Supplier Department Name " + (i + 1).toString());
                            this.LbxDepartmentOptions.push(lbx_item);
                        }
                    console.log('index', i, 'lbx_dep', this.LbxDepartmentOptions()[i]); ////////////////////////
                }
                else console.log('--------!?!----------'); ////////////////////
            }
            this.LbxDepartmentOptions.valueHasMutated();
        }
        console.log('total', total, 'lbx-deps', this.LbxDepartmentOptions());
    }
    */

    sub_topic_removing_Location = function (x_id) {
        console.log("sub_topic_removing_Location --> cont", x_id);
        var x_len = this.LbxLocationOptions().length;
        //debugger; ///////////////////////////////////
        if (x_len > 0) {
            console.log(x_id);
            var del_id = "??? --- ()() --- !!!";
            var _remove = false;
            this.LbxLocationOptions.remove(item => {
                try {
                    del_id = item.Id()(); // <--- ??? --- ()() --- !!! Paul Edwards solution
                } catch (e) {
                    del_id = item.Id(); // <--- ??? --- () --- !!! Paul Edwards solution
                }
                _remove = (x_id === del_id);
                console.log(x_id, _remove, del_id, item);
                return _remove;
            });
            this.LbxLocationOptions.valueHasMutated();
        }
        console.log(this.LbxLocationOptions());
    }
    /*
    sub_topic_removing_Location = function(x_id) {
        var _remove = false;
        var x_len = this.LbxLocationOptions().length;
        //debugger; ///////////////////////////////////
        if (x_len > 0)
            this.LbxLocationOptions.remove(function (x_item) {
                var del_id = JSON.stringify(x_id);
                var item_id = JSON.stringify(x_item.Id());
                _remove = item_id === del_id;
                return _remove;
            });
        this.LbxLocationOptions.valueHasMutated();
        console.log(_remove, x_id, this.LbxLocationOptions());
    }
    */
    sub_topic_removing_Department = function (x_id) {
        var _remove = false;
        var x_len = this.LbxDepartmentOptions().length;
        //debugger; ///////////////////////////////////
        if (x_len > 0)
            this.LbxDepartmentOptions.remove(function (x_item) {
                var del_id = JSON.stringify(x_id);
                var item_id = JSON.stringify(x_item.Id());
                _remove = item_id === del_id;
                return _remove;
            });
        this.LbxDepartmentOptions.valueHasMutated();
        console.log(_remove, x_id, this.LbxDepartmentOptions());
    }

    is_newXloc_item = function (x_itm_id) {
        var x_len = this.LbxLocationOptions().length;
        for (var i = 0; i < x_len; i++) {
            var x_opt_id = JSON.stringify(this.LbxLocationOptions()[i].Id);
            if (x_itm_id === x_opt_id)
                return false;
        }
        return true;
    }

    is_newXdep_item = function (x_itm_id) {
        var x_len = this.LbxDepartmentOptions().length;
        for (var i = 0; i < x_len; i++) {
            var x_opt_id = JSON.stringify(this.LbxDepartmentOptions()[i].Id());
            if (x_itm_id === x_opt_id)
                return false;
        }
        return true;
    }
}

var cust_con_Selected_LbxLocation_options = function (item: CustomerContactVM) {
    var length = item.SelectedLocationIds.length;
    for (var i = 0; i < length; i++) {
        var selected = item.SelectedLocationIds[i];
        $('#lbx_loc_of_con').children('option[value="' + selected + '"]').attr('selected', 'selected');
    }
}

var cust_con_Selected_LbxDepartment_options = function (item: CustomerContactVM) {
    var length = item.SelectedDepartmentIds.length;
    for (var i = 0; i < length; i++) {
        var selected = item.SelectedDepartmentIds[i];
        $('#lbx_dep_of_con').children('option[value="' + selected + '"]').attr('selected', 'selected');
    }
}

// radio button's index & text
class RadioButtonVM {
    rbText: string;
    rbIndx: number;
    constructor(text: string, index: number) {
        this.rbText = text;
        this.rbIndx = index;
    }
}
// ItemViewModel of DDL
class DropDownVM {
    Id: string;
    label: string;
    value: string;
    constructor(Id: string, label: string, value: string) {
        this.Id = Id;
        this.label = label;
        this.value = value;
    }
}
/*
    Update_isCustomerModelChanged = function () {
        console.log('Update_isCustomerModelChanged() - called');
        var self = this;
        if (self.CustomerID() !== undefined && self.CustomerID() !== "") {
            var resp_json = {};
            var request = $.ajax({
                type: 'GET',
                contentType: 'application/json; charset=utf-8',
                url: '/api/Customer/GetCustomerModelByCustomerID/?customerID=' + self.CustomerID(),
                //data: { customerID: self.CustomerID() },
                async: false //-must-
            });
            request.done(function (resp) {
                //debugger; /////////////////////
                if (resp != null) {
                    if (self.CustomerCompanyName() != resp.CustomerCompanyName
                        || self.CustomerCode() != resp.CustomerCode
                        || self.IsTransfer() != resp.IsTransfer) //////////////////////////
                        self.IsCustomerModelChanged(true);
                    else self.IsCustomerModelChanged(false);
                    resp_json = JSON.stringify(resp);
                }
            });
            request.fail(function (jqXHR, textStatus, error) {
                if (textStatus != "success")
                    alert(jqXHR.statusText + '\n' + jqXHR.responseText + '\n' + error);
            });
            request.always(function (jqXHR, textStatus) {
                var log_text = "Update_isCustomerModelChanged - respons:\n" + resp_json;
                console.log(log_text);
            });
        }
        else console.log('CustomerID is not available for Update_isCustomerModelChanged');
    }
*/

    //prevLoc = function (index: number) {
    //    var model = this;
    //    var items = model.CustomerLocations();
    //    var length = items.length;
    //    if (index !== undefined && length > 0) {
    //        for (var li of items) li.RowIsVisible(false);
    //        var x = index - 1;
    //        if (x >= 0 && x < length) {
    //            items[x].RowIsVisible(true);
    //            console.log('prev_loc@ ', x + ' ' + items[x].CustomerLocationName());
    //        }
    //        else {
    //            items[index].RowIsVisible(true);
    //            console.log('prev_loc@ ', index + ' ' + items[index].CustomerLocationName());
    //        }
    //        //this.saveState(false);
    //    }
    //    else bootbox.alert("No Customer Locations"); ////////////////
    //}

    //nextLoc = function (index: number) {
    //    var model = this;
    //    var items = model.CustomerLocations();
    //    var length = items.length;
    //    if (index !== undefined && length > 0) {
    //        for (var li of items) li.RowIsVisible(false);
    //        var x = index + 1;
    //        if (x >= 0 && x < length) {
    //            items[x].RowIsVisible(true);
    //            console.log('next_loc@ ', x + ' ' + items[x].CustomerLocationName());
    //        }
    //        else {
    //            items[index].RowIsVisible(true);
    //            console.log('next_loc@ ', index + ' ' + items[index].SupplierLocationName());
    //        }
    //        //this.saveState(false);
    //    }
    //    else bootbox.alert("No Customer Locations"); ////////////////
    //}

    //saveLocationsWorker = function () {
    //    var model = this;
    //    console.log('saveLocationsWorker() - called with ', model);
    //    var resp_json = {};
    //    if (model.CustomerID() !== undefined && model.CustomerID() !== "") {
    //        model.CustomerLocations().forEach(function (item) {
    //            item.CustomerID(model.CustomerID());
    //        });
    //        var list = new CustomerLocations(model.CustomerLocations());
    //        var request = $.ajax({
    //            type: 'POST',
    //            contentType: 'application/json; charset=utf-8',
    //            url: '/api/CustomerLocation/CreateLocations/', /////////////
    //            data: ko.toJSON(list)
    //        });
    //        request.done(function (resp) {
    //            //debugger; /////////////////////
    //            model.CustomerLocations.removeAll();
    //            if (resp != null) {
    //                resp_json = JSON.stringify(resp);
    //                if (resp.CustomerLocations != null) {
    //                    for (let i = 0; i < resp.CustomerLocations.length; i++)
    //                        model.editLoc(resp.CustomerLocations[i]);
    //                    bootbox.alert("Customer locations saved!"); /////////////
    //                }
    //            }
    //        });
    //        request.fail(function (jqXHR, textStatus, error) {
    //            if (textStatus != "success")
    //                alert(jqXHR.statusText + '\n' + jqXHR.responseText + '\n' + error);
    //        });
    //        request.always(function (jqXHR, textStatus) {
    //            var log_text = "saveLocationsWorker - respons:\n" + resp_json;
    //            console.log(log_text);
    //        });
    //    }
    //    else console.log('CustomerID is not available for saveLocationsWorker');
    //}

    //prevDep = function (index: number) {
    //    var model = this;
    //    var items = model.CustomerDepartments();
    //    var length = items.length;
    //    if (index !== undefined && length > 0) {
    //        for (var li of items) li.RowIsVisible(false);
    //        var x = index - 1;
    //        if (x >= 0 && x < length) {
    //            items[x].RowIsVisible(true);
    //            console.log('prev_dep@ ', x + ' ' + items[x].CustomerDepartmentName());
    //        }
    //        else {
    //            items[index].RowIsVisible(true);
    //            console.log('prev_dep@ ', index + ' ' + items[index].CustomerDepartmentName());
    //        }
    //        //this.saveState(false);
    //    }
    //    else bootbox.alert("No Customer Departments"); ////////////////
    //}

    //nextDep = function (index: number) {
    //    var items = this.CustomerDepartments();
    //    var length = items.length;
    //    if (index !== undefined && length > 0) {
    //        for (var li of items) li.RowIsVisible(false);
    //        var x = index + 1;
    //        if (x >= 0 && x < length) {
    //            items[x].RowIsVisible(true);
    //            console.log('next_dep@ ', x + ' ' + items[x].CustomerDepartmentName());
    //        }
    //        else {
    //            items[index].RowIsVisible(true);
    //            console.log('next_dep@ ', index + ' ', items[index].CustomerDepartmentName());
    //        }
    //        //this.saveState(false);
    //    }
    //    else bootbox.alert("No Customer Departments"); ////////////////
    //}

    //saveDepartmentsWorker = function () {
    //    var model = this;
    //    console.log('saveDepartmentsWorker() - called with ', model);
    //    var resp_json = {};
    //    if (model.CustomerID() !== undefined && model.CustomerID() !== "") {
    //        model.CustomerDepartments().forEach(function (item) {
    //            item.CustomerID(model.CustomerID());
    //        });
    //        var list = new CustomerDepartments(model.CustomerDepartments());
    //        var request = $.ajax({
    //            type: 'POST',
    //            contentType: 'application/json; charset=utf-8',
    //            url: '/api/CustomerDepartment/CreateDepartments/', ////////////////
    //            data: ko.toJSON(list)
    //        });
    //        request.done(function (resp) {
    //            //debugger; /////////////////////
    //            model.CustomerDepartments.removeAll();
    //            if (resp != null) {
    //                resp_json = JSON.stringify(resp);
    //                if (resp.CustomerDepartments != null) {
    //                    for (let i = 0; i < resp.CustomerDepartments.length; i++)
    //                        model.editDep(resp.CustomerDepartments[i]);
    //                    bootbox.alert("Customer departments saved!"); ////////////////
    //                }
    //            }
    //        });
    //        request.fail(function (jqXHR, textStatus, error) {
    //            if (textStatus != "success")
    //                alert(jqXHR.statusText + '\n' + jqXHR.responseText + '\n' + error);
    //        });
    //        request.always(function (jqXHR, textStatus) {
    //            var log_text = "saveDepartmentsWorker - respons:\n" + resp_json;
    //            console.log(log_text);
    //        });
    //    }
    //    else console.log('CustomerID is not available for saveDepartmentsWorker');
    //}

    //newDep = function () {
    //    var model = this;
    //    var items = model.CustomerDepartments();
    //    var _length = items.length;
    //    console.log('deps_total ', _length);
    //    if (_length > 0) for (var x of items)
    //        x.RowIsVisible(false);
    //    var item = new CustomerDepartmentVM({ CustomerID: model.CustomerID() });
    //    item.RowIsVisible(true);
    //    item.RowCurrent(_length);
    //    _length = _length + 1;
    //    model.TotalDepartments(_length);
    //    model.CustomerDepartments().push(item); //--> idx=_length
    //    model.CustomerDepartments().valueHasMutated;
    //    console.log('new_added@ ', item.RowCurrent(), ' /_total ', this.TotalDepartments(), items);
    //    //this.saveState(false);
    //}

    //editDep = function (data) {
    //    var model = this;
    //    var items = model.CustomerDepartments();
    //    for (var x of items) x.RowIsVisible(false);
    //    var item = new CustomerDepartmentVM(data);
    //    item.RowIsVisible(true);
    //    items.push(item);
    //    //this.saveState(false);
    //    console.log('editDep ', item);
    //}

    //rmvDep = function (index: number) {
    //    var model = this;
    //    var items = model.CustomerDepartments();
    //    var item = items.splice(index, 1)[0];
    //    items.remove(item);
    //    if (items.length == 0) model.newDep();
    //    //this.saveState(false);
    //    console.log('rmvDep ', item);
    //}
