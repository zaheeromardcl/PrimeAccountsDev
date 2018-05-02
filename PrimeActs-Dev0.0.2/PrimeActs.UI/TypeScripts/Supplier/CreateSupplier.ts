//-Settings_Add_&_View_Supplier

var x___topic = new ko.subscribable();

var s4___guid = function () {
    function s4() {
        return Math.floor((1 + Math.random()) * 0x10000)
            .toString(16).substring(1);
    }
    return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
        s4() + '-' + s4() + s4() + s4();
}

var is___guid = function (str_val) {
    if (str_val === undefined)
        return false;
    if (str_val[0] === "{") {
        str_val = str_val.substring(1, str_val.length - 1);
    }
    var regexGuid = /^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$/gi;
    return regexGuid.test(str_val);
}

// listbox
class LbxViewModel {
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

class CreateSupplierModel {
    supplierModel: KnockoutObservable<SupplierModel>;
    constructor() {
        this.supplierModel = ko.observable(new SupplierModel(null));
        this.supplierModel().newContact(null, null);
        this.supplierModel().newDepartment(null);
        this.supplierModel().newLocation();
    }

    saveSupplierItems = function () {
        var self = this;
        if (self.supplierModel().SupplierID() == undefined || self.supplierModel().SupplierID() === "") {
            self.supplierModel().saveSupplier("__create__");
        }
    }
}

class UpdateSupplierModel {
    supplierModel: KnockoutObservable<SupplierModel>;
    constructor(data) {
        data = data || {};
        this.supplierModel = ko.observable(new SupplierModel(data));
        this.supplierModel().setSupplier(data);
        this.supplierModel().setContact(data);
        this.supplierModel().setDepartment(data);
        this.supplierModel().setLocation(data);
    }

    saveSupplierItems = function () {
        var self = this;
        if (self.supplierModel().SupplierID() !== undefined && self.supplierModel().SupplierID() !== "") {
            self.supplierModel().saveSupplier("__update__");
        }
    }
}

class ShowSupplierDetailsModel {
    supplierModel: KnockoutObservable<SupplierModel>;
    constructor(data) {
        //debugger; /////////////////////////////
        data = data || {};
        this.supplierModel = ko.observable(new SupplierModel(data));
        this.supplierModel().setSupplier(data);
        this.supplierModel().setContact(data);
        this.supplierModel().setDepartment(data);
        this.supplierModel().setLocation(data);
    }
}

class SupplierLocations {
    koaSupplierLocations: KnockoutObservableArray<SupplierLocationVM>;
    constructor(data) {
        this.koaSupplierLocations = ko.observableArray<SupplierLocationVM>(data);
    }
}

class SupplierDepartments {
    koaSupplierDepartments: KnockoutObservableArray<SupplierDepartmentVM>;
    constructor(data) {
        this.koaSupplierDepartments = ko.observableArray<SupplierDepartmentVM>(data);
    }
}

class SupplierContacts {
    koaSupplierContacts: KnockoutObservableArray<SupplierContactVM>;
    constructor(data) {
        this.koaSupplierContacts = ko.observableArray<SupplierContactVM>(data);
    }
}

class SupplierModel {
    Errors: KnockoutValidationErrors;
    ShowErrors: KnockoutObservable<boolean>;
    SupplierID: KnockoutObservable<string>;
    SupplierCode: KnockoutObservable<string>;
    SupplierCompanyName: KnockoutObservable<string>;
    SupplierContacts: KnockoutObservableArray<SupplierContactVM>;
    SupplierLocations: KnockoutObservableArray<SupplierLocationVM>;
    SupplierDepartments: KnockoutObservableArray<SupplierDepartmentVM>;
    IsFactor: KnockoutObservable<boolean>;
    IsHaulier: KnockoutObservable<boolean>;
    ParentSupplierID: KnockoutObservable<string>;
    ParentSupplierName: KnockoutObservable<string>;
    CompanyID: KnockoutObservable<string>;
    CompanyName: KnockoutObservable<string>;
    CompanyOptions: KnockoutObservableArray<LbxViewModel>;
    NoteID: KnockoutObservable<string>;
    Notes: KnockoutObservable<string>;
    NoteDescription: KnockoutObservable<string>;
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
        this.SupplierID = ko.observable("");
        this.SupplierCode = ko.observable("").extend({ required: true });
        this.SupplierCompanyName = ko.observable("").extend({ required: true });
        this.SupplierContacts = ko.observableArray<SupplierContactVM>([]);
        this.SupplierLocations = ko.observableArray<SupplierLocationVM>([]);
        this.SupplierDepartments = ko.observableArray<SupplierDepartmentVM>([]);
        this.IsFactor = ko.observable(false);
        this.IsHaulier = ko.observable(false);
        this.ParentSupplierID = ko.observable(data.ParentSupplierID);
        this.ParentSupplierName = ko.observable(data.ParentSupplierName);
        this.CompanyID = ko.observable(data.CompanyID);
        this.CompanyName = ko.observable(data.CompanyName);
        this.CompanyOptions = ko.observableArray<LbxViewModel>([]);
        this.DDL_Company_options();
        this.NoteID = ko.observable(data.NoteID);
        this.Notes = ko.observable(data.Notes);
        this.NoteDescription = ko.observable(" "); // <-- Paul Edwards
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
        console.log('pub_topic_lbx_Locations_list', x_no, this.SupplierLocations());
        //debugger; /////////////////////////////
        x___topic.notifySubscribers(this.SupplierLocations(), 'topic_lbx_Locations_list');
    }

    pub_topic_lbx_Departments_list = function (x_no) {
        console.log('pub_topic_lbx_Departments_list', x_no, this.SupplierDepartments());
        //debugger; /////////////////////////////
        x___topic.notifySubscribers(this.SupplierDepartments(), 'topic_lbx_Departments_list');
    }

    pub_topic_removing_Location = function (x_id) {
        console.log('LbxRemovingLocationId', x_id);
        x___topic.notifySubscribers(x_id, 'topic_removing_Location');
    }

    pub_topic_removing_Department = function (x_id) {
        console.log('LbxRemovingDepartmentId', x_id);
        x___topic.notifySubscribers(x_id, 'topic_removing_Department');
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
                        self.CompanyOptions.push(new LbxViewModel(_item));
                    });
                }
            }
        });
        return [];
    }

    AC_get_ParentSupplier = function (request, ui) {
        var text = request.term;
        if (text === " " || text === "") return;
        console.log('AC_get_ParentSupplier - request.term ', text);
        $.ajax({
            type: "GET",
            url: "/api/Supplier/AutoCompleteForPS/?search=" + text,
            data: {
                json: "{}",
                delay: 0.5,
                search: text
            },
            success: function (data) {
                if (data == null) return;
                console.log('AC_get_ParentSupplier - data ', data);
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

    AC_select_ParentSupplier = function (event, ui) {
        console.log('AC_select_ParentSupplier: ', ui.item.label + '  ' + ui.item.languageValue);
        var vm = ko.dataFor(event.target);
        vm.supplierModel().ParentSupplierID(ui.item.languageValue);
        vm.supplierModel().ParentSupplierName(ui.item.label);
        console.log('ParentSupplierName: ', vm.supplierModel().ParentSupplierName());
    }

    setSupplier = function (data) {
        var model = this;
        model.SupplierID(data.SupplierID);
        model.SupplierCompanyName(data.SupplierCompanyName);
        model.SupplierCode(data.SupplierCode);
        model.SupplierContacts(data.SupplierContacts);
        model.SupplierLocations(data.SupplierLocations);
        model.SupplierDepartments(data.SupplierDepartments);
        model.IsFactor(data.IsFactor);
        //model.IsSupplierModelChanged(false);
        //model.IsSupplierSaveBtnEnabled(true); // false
        model.IsHaulier(data.IsHaulier);
        model.ParentSupplierID(data.ParentSupplierID);
        model.ParentSupplierName(data.ParentSupplierName);
        model.CompanyID(data.CompanyID);
        model.CompanyName(data.CompanyName);
        model.NoteID(data.NoteID);
        model.Notes(data.Notes);
        model.DDL_Company_options();
        //this.saveState(false);
    }

    newLocation = function () {
        var x_guid = '_' + s4___guid();
        var _model = new SupplierLocationVM({ SupplierID: this.SupplierID() });
        _model.SupplierLocationName("Provide Supplier Location Name");
        _model.x_SupplierLocationID(x_guid);
        _model.SupplierLocationID(x_guid);
        _model.ItemDeleting(false);
        _model.ItemAdding(true);
        //debugger; /////////////////////////
        _model.SupplierLocationName.subscribe(_model.pub_topic_Location_id_name, _model);
        this.SupplierLocations.push(_model);
        this.LbxGettingLocationsList(this.SupplierLocations().length);
        //this.saveState(false);
    }

    setLocation = function (data) {
        var items = data.SupplierLocations;
        for (var item of items) {
            var _model = new SupplierLocationVM(item);
            _model.ItemAdding(false);
            _model.ItemDeleting(false);
            //debugger; //////////////////////
            _model.SupplierLocationName.subscribe(_model.pub_topic_Location_id_name, _model);
            data.SupplierLocations.splice(items.indexOf(item), 1, _model);
        }
        this.LbxGettingLocationsList(items.length);
        //this.saveState(false);
    }

    rmvLocation = function (index: number) {
        var _model = this.SupplierLocations.splice(index, 1)[0];
        _model.ItemAdding(false);
        _model.ItemDeleting(true);
        this.SupplierLocations.remove(_model);
        this.LbxRemovingLocationId(_model.SupplierLocationID());
        //this.saveState(false);
    }

    newDepartment = function (_locations) {
        var x_guid = '_' + s4___guid();
        var _model = new SupplierDepartmentVM({ SupplierID: this.SupplierID() }, _locations);
        _model.SupplierDepartmentName("Provide Supplier Department Name");
        _model.x_SupplierDepartmentID(x_guid);
        _model.SupplierDepartmentID(x_guid);
        _model.ItemDeleting(false);
        _model.ItemAdding(true);
        DDL_CreditTerm_options(_model);
        //debugger; //////////////////////////////
        x___topic.subscribe(_model.sub_topic_Location_id_name, _model, "topic_Location_id_name");
        _model.SupplierDepartmentName.subscribe(_model.pub_topic_Department_id_name, _model);
        x___topic.subscribe(_model.sub_topic_lbx_Locations_list, _model, "topic_lbx_Locations_list");
        x___topic.subscribe(_model.sub_topic_removing_Location, _model, "topic_removing_Location");
        this.SupplierDepartments.push(_model);
        this.LbxGettingDepartmentsList(this.SupplierDepartments().length);
        //this.saveState(false);
    }

    setDepartment = function (data) {
        var items = data.SupplierDepartments;
        var _locations = data.SupplierLocations;
        for (var item of items) {
            var _model = new SupplierDepartmentVM(item, _locations);
            _model.ItemAdding(false);
            _model.ItemDeleting(false);
            DDL_CreditTerm_options(_model);
            dep_Selected_CreditTerm_option(_model);
            _model.LbxLocationOptions(item.LbxLocationOptions);
            _model.SelectedLocationIds(item.SelectedLocationIds);
            dep_Selected_LbxLocation_options(_model);
            //debugger; //////////////////////////////
            x___topic.subscribe(_model.sub_topic_Location_id_name, _model, "topic_Location_id_name");
            _model.SupplierDepartmentName.subscribe(_model.pub_topic_Department_id_name, _model);
            x___topic.subscribe(_model.sub_topic_lbx_Locations_list, _model, "topic_lbx_Locations_list");
            x___topic.subscribe(_model.sub_topic_removing_Location, _model, "topic_removing_Location");
            data.SupplierDepartments.splice(items.indexOf(item), 1, _model);
        }
        this.LbxGettingDepartmentsList(items.length);
        //this.saveState(false);
    }

    rmvDepartment = function (index: number) {
        var _model = this.SupplierDepartments.splice(index, 1)[0];
        _model.ItemAdding(false);
        _model.ItemDeleting(true);
        this.SupplierDepartments.remove(_model);
        this.LbxRemovingDepartmentId(_model.SupplierDepartmentID());
        //this.saveState(false);
    }

    newContact = function (_locations, _departments) {
        var _model = new SupplierContactVM({ SupplierID: this.SupplierID() }, _locations, _departments);
        _model.ItemAdding(true);
        _model.ItemDeleting(false);
        x___topic.subscribe(_model.sub_topic_Location_id_name, _model, "topic_Location_id_name");
        x___topic.subscribe(_model.sub_topic_lbx_Locations_list, _model, "topic_lbx_Locations_list");
        x___topic.subscribe(_model.sub_topic_removing_Location, _model, "topic_removing_Location");
        x___topic.subscribe(_model.sub_topic_Department_id_name, _model, "topic_Department_id_name");
        x___topic.subscribe(_model.sub_topic_lbx_Departments_list, _model, "topic_lbx_Departments_list");
        x___topic.subscribe(_model.sub_topic_removing_Department, _model, "topic_removing_Department");
        this.SupplierContacts.push(_model);
        //this.saveState(false);
    }

    setContact = function (data) {
        var items = data.SupplierContacts;
        var _locations = data.SupplierLocations;
        var _departments = data.SupplierDepartments;
        for (var item of items) {
            var _model = new SupplierContactVM(item, _locations, _departments);
            _model.ItemAdding(false);
            _model.ItemDeleting(false);
            _model.LbxLocationOptions(item.LbxLocationOptions);
            _model.SelectedLocationIds(item.SelectedLocationIds);
            _model.LbxDepartmentOptions(item.LbxDepartmentOptions);
            _model.SelectedDepartmentIds(item.SelectedDepartmentIds);
            con_Selected_LbxLocation_options(_model);
            con_Selected_LbxDepartment_options(_model);
            //debugger; //////////////////////////////
            x___topic.subscribe(_model.sub_topic_Location_id_name, _model, "topic_Location_id_name");
            x___topic.subscribe(_model.sub_topic_lbx_Locations_list, _model, "topic_lbx_Locations_list");
            x___topic.subscribe(_model.sub_topic_removing_Location, _model, "topic_removing_Location");
            x___topic.subscribe(_model.sub_topic_Department_id_name, _model, "topic_Department_id_name");
            x___topic.subscribe(_model.sub_topic_lbx_Departments_list, _model, "topic_lbx_Departments_list");
            x___topic.subscribe(_model.sub_topic_removing_Department, _model, "topic_removing_Department");
        }
        //this.saveState(false);
    }

    rmvContact = function (index: number) {
        var _item = this.SupplierContacts.splice(index, 1)[0];
        this.SupplierContacts.remove(_item);
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

    saveSupplier = function (msg_text) {
        console.log('saveSupplier(' + msg_text + ') - called');
        var _model = this;
        var json_data = ko.toJSON(_model);
        var request = $.ajax({
            type: 'POST',
            data: json_data,
            url: '/api/Supplier/CreateSupplier',
            contentType: 'application/json; charset=utf-8',
            async: false
        });
        request.done(function (resp) {
            if (resp != null) {
                _model.SupplierID(resp.Data);
                json_data = JSON.stringify(resp);
            }
        });
        request.fail(function (jqXHR, textStatus, error) {
            if (textStatus != "success")
                alert(jqXHR.statusText + '\n' + jqXHR.responseText + '\n' + error);
        });
        request.always(function (jqXHR, textStatus) {
            var log_text = "saveSupplier - respons:\n" + json_data;
            console.log(log_text);
        });
    }

}

interface ISupplierLocationVM {
}

class SupplierLocationVM implements ISupplierLocationVM {
    SupplierLocationID: KnockoutObservable<string>;
    x_SupplierLocationID: KnockoutObservable<string>;
    SupplierLocationName: KnockoutObservable<string>;
    SupplierID: KnockoutObservable<string>;
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
        this.SupplierLocationID = ko.observable(data.SupplierLocationID);
        this.x_SupplierLocationID = ko.observable(data.x_SupplierLocationID);
        this.SupplierLocationName = ko.observable(data.SupplierLocationName).extend({ required: true });
        this.SupplierID = ko.observable(data.SupplierID);
        this.TelephoneNumber = ko.observable(data.TelephoneNumber);
        this.FaxNumber = ko.observable(data.FaxNumber);
        this.NoteID = ko.observable(data.NoteID);
        this.Notes = ko.observable(data.Notes);
        this.NoteDescription = ko.observable(" "); // <-- Paul Edwards
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
        //debugger; ///////////////////////////////
        var lbx_item = new LbxViewModel({});
        lbx_item.Id(this.SupplierLocationID());
        lbx_item.label(x_name);
        lbx_item.value(this.SupplierLocationID());
        console.log('topic_Location_id_name ==> ****', lbx_item);
        x___topic.notifySubscribers(lbx_item, 'topic_Location_id_name');
    }
}

interface ISupplierDepartmentVM {
}

class SupplierDepartmentVM implements ISupplierDepartmentVM {
    SupplierDepartmentID: KnockoutObservable<string>;
    x_SupplierDepartmentID: KnockoutObservable<string>;
    SupplierDepartmentName: KnockoutObservable<string>;
    SupplierID: KnockoutObservable<string>;
    Commission: KnockoutObservable<number>;
    Handling: KnockoutObservable<number>;
    CountryID: KnockoutObservable<string>;
    CountryName: KnockoutObservable<string>;
    GivesRebate: KnockoutObservable<boolean>;
    RebateAmount: KnockoutObservable<number>;
    EmailAddress: KnockoutObservable<string>;
    FactorSupplierDepartmentID: KnockoutObservable<string>;
    IsTransactionTaxExempt: KnockoutObservable<boolean>;
    TransactionTaxReference: KnockoutObservable<string>;
    NoteID: KnockoutObservable<string>;
    Notes: KnockoutObservable<string>;
    NoteDescription: KnockoutObservable<string>;
    CreditLimit: KnockoutObservable<number>;
    CreditTerm: KnockoutObservable<number>;
    CreditTermOptions: KnockoutObservableArray<MyCustomOption>;
    ItemAdding: KnockoutObservable<boolean>;
    ItemDeleting: KnockoutObservable<boolean>;
    SelectedLocationIds: KnockoutObservableArray<string>;
    LbxLocationOptions: KnockoutObservableArray<LbxViewModel>;

    constructor(data, _locations) {
        data = data || {};
        _locations = _locations || {};
        this.SupplierDepartmentID = ko.observable(data.SupplierDepartmentID);
        this.x_SupplierDepartmentID = ko.observable(data.x_SupplierDepartmentID);
        this.SupplierDepartmentName = ko.observable(data.SupplierDepartmentName).extend({ required: true });
        this.SupplierID = ko.observable(data.SupplierID);
        this.Commission = ko.observable(data.Commission).extend({ numeric: 2 });
        this.Handling = ko.observable(data.Handling).extend({ numeric: 2 });
        this.CountryID = ko.observable(data.CountryID);
        this.CountryName = ko.observable(data.CountryName);
        this.GivesRebate = ko.observable(data.GivesRebate);
        this.RebateAmount = ko.observable(data.RebateAmount).extend({ numeric: 2 });;
        this.EmailAddress = ko.observable(data.EmailAddress);
        this.FactorSupplierDepartmentID = ko.observable(data.FactorSupplierDepartmentID);
        this.IsTransactionTaxExempt = ko.observable(data.IsTransactionTaxExempt);
        this.TransactionTaxReference = ko.observable(data.TransactionTaxReference);
        this.NoteID = ko.observable(data.NoteID);
        this.Notes = ko.observable(data.Notes);
        this.NoteDescription = ko.observable(" "); // <-- Paul Edwards
        this.CreditLimit = ko.observable(data.CreditLimit).extend({ numeric: 2 });;
        this.CreditTerm = ko.observable(0);
        this.CreditTermOptions = ko.observableArray<MyCustomOption>([]);
        this.ItemAdding = ko.observable(data.ItemAdding);
        this.ItemDeleting = ko.observable(data.ItemDeleting);
        this.SelectedLocationIds = ko.observableArray<string>([]).extend({ notify: 'always' });
        this.LbxLocationOptions = ko.observableArray<LbxViewModel>([]).extend({ notify: 'always' });
    }

    pub_topic_Department_id_name = function (x_name) {
        //debugger; ////////////////////////////////////
        var lbx_item = new LbxViewModel({});
        lbx_item.Id(this.SupplierDepartmentID());
        lbx_item.label(x_name);
        lbx_item.value(this.SupplierDepartmentID());
        console.log('topic_Department_id_name ==> ****', lbx_item);
        x___topic.notifySubscribers(lbx_item, 'topic_Department_id_name');
    }
    /*
    sub_topic_Location_id_name = function (x_itm: LbxViewModel) {
        console.log("topic_Location_id_name --> depa", x_itm);
        var x_len = this.LbxLocationOptions().length;
        //debugger; ///////////////////////////////////
        var _remove = false;
        var x_id = x_itm.Id();
        var del_id;
        if (x_len > 0)
            this.LbxLocationOptions.remove(function (x_item) {
                var x_id = JSON.stringify(x_id);
                var del_id = JSON.stringify(x_item.Id());
                _remove = (x_id === del_id);
                return _remove;
            });
        this.LbxLocationOptions.push(x_itm);
        this.LbxLocationOptions.valueHasMutated();
        console.log(x_id, _remove, del_id, this.LbxLocationOptions());
    }
    */
    sub_topic_Location_id_name = function (x_itm: LbxViewModel) {
        console.log("topic_Location_id_name --> depa", x_itm);
        var x_len = this.LbxLocationOptions().length;
        //debugger; ///////////////////////////////////
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
    }

    sub_topic_lbx_Locations_list = function (_locations: KnockoutObservableArray<SupplierLocationVM>) {
        var lbx_options = ko.observableArray([]);
        this.LbxLocationOptions.removeAll();
        var total = _locations.length;
        console.log('Department sub_topic_lbx_Locations_list', total);
        //debugger; ///////////////////////////////////
        for (var i = 0; i < total; i++) {
            var item = new SupplierLocationVM(_locations[i]);
            var obj_item = ko.toJS(item);
            if (obj_item.SupplierLocationID !== undefined) {
                var lbx_item = new LbxViewModel({});
                lbx_item.Id(obj_item.SupplierLocationID);
                lbx_item.label(obj_item.SupplierLocationName);
                lbx_item.value(obj_item.SupplierLocationID);
                lbx_options.push(lbx_item);
            }
        }
        console.log('lbx_options', lbx_options());
        ko.utils.arrayPushAll(this.LbxLocationOptions, lbx_options());
        this.LbxLocationOptions.valueHasMutated();
        console.log('LbxLocationOptions', this.LbxLocationOptions());
    }

    sub_topic_removing_Location = function (x_id) {
        console.log("sub_topic_removing_Location --> depa", x_id);
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
    sub_topic_removing_Location = function (x_id) {
        var x_len = this.LbxLocationOptions().length;
        //debugger; ///////////////////////////////////
        var _remove = false;
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

    is_newXloc_item = function (x_itm_id) {
        var x_len = this.LbxLocationOptions().length;
        for (var i = 0; i < x_len; i++) {
            var x_opt_id = JSON.stringify(this.LbxLocationOptions()[i].Id);
            if (x_itm_id === x_opt_id)
                return false;
        }
        return true;
    }

    getCountry_ofSupplierDept = function (request, ui) {
        var text = request.term;
        if (text === " " || text === "")
            return;
        $.ajax({
            type: "GET",
            url: "/api/Country/AutoComplete/?search=" + text,
            data: {
                json: "{}",
                delay: 0.5,
                search: text
            },
            success: function (data) {
                if (data == null)
                    return;
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

    selectCountry_ofSupplierDept = function (event, ui) {
        console.log('selectCountry_ofSupplierDept: ', ui.item.label + '  ' + ui.item.languageValue);
        var vm = ko.dataFor(event.target);
        vm.CountryID(ui.item.languageValue);
        vm.CountryName(ui.item.label);
    }
}

var DDL_CreditTerm_options = function (item: SupplierDepartmentVM) {
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

var dep_Selected_CreditTerm_option = function (item: SupplierDepartmentVM) {
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

var dep_Selected_LbxLocation_options = function (item: SupplierDepartmentVM) {
    var length = item.SelectedLocationIds.length;
    for (var i = 0; i < length; i++) {
        var selected = item.SelectedLocationIds[i];
        $('#lbx_loc_of_dep').children('option[value="' + selected + '"]').attr('selected', 'selected');
    }
}

interface ISupplierContactVM {
}

class SupplierContactVM implements ISupplierContactVM {
    SupplierContactID: KnockoutObservable<string>;
    SupplierID: KnockoutObservable<string>;
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
    SortOrder: KnockoutObservable<string>; // <-- SupplierContactID
    ItemAdding: KnockoutObservable<boolean>;
    ItemDeleting: KnockoutObservable<boolean>;
    SelectedLocationIds: KnockoutObservableArray<string>;
    SelectedDepartmentIds: KnockoutObservableArray<string>;
    LbxLocationOptions: KnockoutObservableArray<LbxViewModel>;
    LbxDepartmentOptions: KnockoutObservableArray<LbxViewModel>;

    constructor(data, _locations, _departments) {
        data = data || {};
        _locations = _locations || {};
        _departments = _departments || {};
        this.SupplierContactID = ko.observable(data.SupplierContactID);
        this.SupplierID = ko.observable(data.SupplierID);
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
        this.LbxLocationOptions = ko.observableArray<LbxViewModel>([]).extend({ notify: 'always' });
        this.LbxDepartmentOptions = ko.observableArray<LbxViewModel>([]).extend({ notify: 'always' });
    }

    sub_topic_Location_id_name = function (x_itm: LbxViewModel) {
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
    sub_topic_Location_id_name = function(x_itm: LbxViewModel) {
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
    sub_topic_Department_id_name = function (x_itm: LbxViewModel) {
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

    sub_topic_lbx_Locations_list = function (_locations: KnockoutObservableArray<SupplierLocationVM>) {
        var lbx_options = ko.observableArray([]);
        this.LbxLocationOptions.removeAll();
        var total = _locations.length;
        console.log('Contact sub_topic_lbx_Locations_list', total);
        //debugger; ///////////////////////////////////
        for (var i = 0; i < total; i++) {
            var item = new SupplierLocationVM(_locations[i]);
            var obj_item = ko.toJS(item);
            if (obj_item.SupplierLocationID !== undefined) {
                var lbx_item = new LbxViewModel({});
                lbx_item.Id(obj_item.SupplierLocationID);
                lbx_item.label(obj_item.SupplierLocationName);
                lbx_item.value(obj_item.SupplierLocationID);
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
                    var lbx_item = new LbxViewModel({});
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

    sub_topic_lbx_Departments_list = function (_departments: KnockoutObservableArray<SupplierDepartmentVM>) {
        var lbx_options = ko.observableArray([]);
        this.LbxDepartmentOptions.removeAll();
        var total = _departments.length;
        console.log('Contact sub_topic_lbx_Departments_list', total);
        //debugger; ///////////////////////////////////
        for (var i = 0; i < total; i++) {
            var item = new SupplierDepartmentVM(_departments[i], null);
            var obj_item = ko.toJS(item);
            if (obj_item.SupplierDepartmentID !== undefined) {
                var lbx_item = new LbxViewModel({});
                lbx_item.Id(obj_item.SupplierDepartmentID);
                lbx_item.label(obj_item.SupplierDepartmentName);
                lbx_item.value(obj_item.SupplierDepartmentID);
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
                    var lbx_item = new LbxViewModel({});
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

var con_Selected_LbxLocation_options = function (item: SupplierContactVM) {
    var length = item.SelectedLocationIds.length;
    for (var i = 0; i < length; i++) {
        var selected = item.SelectedLocationIds[i];
        $('#lbx_loc_of_con').children('option[value="' + selected + '"]').attr('selected', 'selected');
    }
}

var con_Selected_LbxDepartment_options = function (item: SupplierContactVM) {
    var length = item.SelectedDepartmentIds.length;
    for (var i = 0; i < length; i++) {
        var selected = item.SelectedDepartmentIds[i];
        $('#lbx_dep_of_con').children('option[value="' + selected + '"]').attr('selected', 'selected');
    }
}

enum ActionType {
    DEL_item,
    INS_item,
    UPD_item,
}

/******** 
class _KoTest {
    _koModel: KnockoutObservable<_KoModel>;
    constructor(data) {
        data = data || {};
        this._koModel = ko.observable(new _KoModel(data));
    }
}

class _KoModel {
    //TestString: KnockoutObservable<string>;
    //constructor(data) {
    //    data = data || {};
    //    this.TestString = ko.observable(data.TestString);
    //}
    SupplierModel: KnockoutObservable<SupplierModel>;
    constructor(data) {
        //data = data || {}; /////////////
        this.SupplierModel = ko.observable(new SupplierModel(null));
        // init new Items -------------------------------------------------
        //this.supplierModel().newLocation();
        //this.supplierModel().newDepartment(this.supplierModel().SupplierLocations());
        //this.supplierModel().newContact(this.supplierModel().SupplierLocations(), this.supplierModel().SupplierDepartments());
        var vm_JSON = ko.toJSON(this.SupplierModel());
        console.log('vm_JSON', vm_JSON);
        debugger; //////////////////////////////
    }
    saveSupplierItems = function () {
        var self = this;
        if (self.supplierModel().SupplierID() == undefined || self.supplierModel().SupplierID() === "") {
            self.supplierModel().saveSupplier("__create__");
        }
    }
}
**********/
    //IsSupplierModelChanged: KnockoutObservable<boolean>;
    //IsSupplierSaveBtnEnabled: KnockoutObservable<boolean>;
        //this.IsSupplierModelChanged = ko.observable(false);
        //this.IsSupplierSaveBtnEnabled = ko.observable(true); // false
    //updValOf_isChanged = function () {
    //    var model = this;
    //    console.log('updValOf_isChanged() - called');
    //    if (model.SupplierID() !== undefined && model.SupplierID() !== "") {
    //        var resp_json = {};
    //        var request = $.ajax({
    //            type: 'GET',
    //            contentType: 'application/json; charset=utf-8',
    //            url: '/api/Supplier/GetSupplierModelBySupplierID/?supplierID=' + model.SupplierID(),
    //            async: false
    //        });
    //        request.done(function (resp) {
    //            //debugger; /////////////////////
    //            if (resp != null) {
    //                if (model.SupplierCompanyName() != resp.SupplierCompanyName
    //                    || model.SupplierCode() != resp.SupplierCode
    //                    || model.IsFactor() != resp.IsFactor)
    //                    model.IsSupplierModelChanged(true);
    //                else model.IsSupplierModelChanged(false);
    //                resp_json = JSON.stringify(resp);
    //            }
    //            console.log('IsSupplierModelChanged: ', model.IsSupplierModelChanged());
    //        });
    //        request.fail(function (jqXHR, textStatus, error) {
    //            if (textStatus != "success")
    //                alert(jqXHR.statusText + '\n' + jqXHR.responseText + '\n' + error);
    //        });
    //        request.always(function (jqXHR, textStatus) {
    //            var log_text = "updValOf_isChanged - respons:\n" + resp_json;
    //            console.log(log_text);
    //        });
    //    }
    //    else console.log('SupplierID is not available for updValOf_isChanged');
    //}
