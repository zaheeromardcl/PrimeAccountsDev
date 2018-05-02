interface ICustomerItemModel {
}

class CustomerViewModel2 {
    CustomerID: KnockoutObservable<string>;
    CustomerCode: KnockoutObservable<string>;
    CustomerCompanyName: KnockoutObservable<string>;
    ParentCustomerID: KnockoutObservable<string>;
    CompanyID: KnockoutObservable<string>;
    NoteID: KnockoutObservable<string>;
    IsTransfer: KnockoutObservable<boolean>;
    Statements: KnockoutObservable<boolean>;
    CreatedDate: KnockoutObservable<string>;
    CreatedBy: KnockoutObservable<string>;
    UpdatedDate: KnockoutObservable<string>;
    UpdatedBy: KnockoutObservable<string>;
    CustomerContacts: KnockoutObservableArray<CustomerContactVM>;
    CustomerLocations: KnockoutObservableArray<CustomerLocationVM>;
    CustomerDepartments: KnockoutObservableArray<CustomerDepartmentVM>;
    CustomerItems: KnockoutObservableArray<ICustomerItemModel> = ko.observableArray([]);

    constructor(data, customerItems: ICustomerItemModel[]) {
        data = data || {};
        this.CustomerID = ko.observable(data.CustomerID);
        this.CustomerCode = ko.observable(data.CustomerCode);
        this.CustomerCompanyName = ko.observable(data.vCompanyName);
        this.ParentCustomerID = ko.observable(data.ParentCustomerID);
        this.CompanyID = ko.observable(data.CompanyID);
        this.NoteID = ko.observable(data.NoteID);
        this.IsTransfer = ko.observable(data.IsTransfer);
        this.Statements = ko.observable(data.Statements);
        this.CreatedDate = ko.observable(data.CreatedDate);
        this.CreatedBy = ko.observable(data.CreatedBy);
        this.UpdatedDate = ko.observable(data.UpdatedDate);
        this.UpdatedBy = ko.observable(data.UpdatedBy);
        this.CustomerContacts = ko.observableArray<CustomerContactVM>([]);
        this.CustomerLocations = ko.observableArray<CustomerLocationVM>([]);
        this.CustomerDepartments = ko.observableArray<CustomerDepartmentVM>([]);
        for (var item of customerItems) {
            this.CustomerItems.push(item);
        }
    }
}

class CustomerItemViewModel implements ICustomerItemModel {
    CustomerItemID: KnockoutObservable<string>;
    CustomerID: KnockoutObservable<string>;
    CustomerCode: KnockoutObservable<string>;
    CustomerCompanyName: KnockoutObservable<string>;
    ParentCustomerID: KnockoutObservable<string>;
    CompanyID: KnockoutObservable<string>;
    NoteID: KnockoutObservable<string>;
    IsTransfer: KnockoutObservable<boolean>;
    Statements: KnockoutObservable<boolean>;
    CreatedDate: KnockoutObservable<string>;
    CreatedBy: KnockoutObservable<string>;
    UpdatedDate: KnockoutObservable<string>;
    UpdatedBy: KnockoutObservable<string>;

    constructor(data) {
        data = data || {};
        this.CustomerID = ko.observable(data.CustomerID);
        this.CustomerCode = ko.observable(data.CustomerCode);
        this.CustomerCompanyName = ko.observable(data.CustomerCompanyName);
        this.ParentCustomerID = ko.observable(data.ParentCustomerID);
        this.CompanyID = ko.observable(data.CompanyID);
        this.NoteID = ko.observable(data.NoteID);
        this.IsTransfer = ko.observable(data.IsTransfer);
        this.Statements = ko.observable(data.Statements);
        this.CreatedDate = ko.observable(data.CreatedDate);
        this.CreatedBy = ko.observable(data.CreatedBy);
        this.UpdatedDate = ko.observable(data.UpdatedDate);
        this.UpdatedBy = ko.observable(data.UpdatedBy);
    }
}
 