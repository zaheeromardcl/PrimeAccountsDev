interface ISupplierItemModel {
}

class SupplierViewModel {
    SupplierID: KnockoutObservable<string>;
    SupplierCode: KnockoutObservable<string>;
    SupplierCompanyName: KnockoutObservable<string>;
    ParentSupplierID: KnockoutObservable<string>;
    CompanyID: KnockoutObservable<string>;
    NoteID: KnockoutObservable<string>;
    IsHaulier: KnockoutObservable<boolean>;
    IsFactor: KnockoutObservable<boolean>;
    CreatedDate: KnockoutObservable<string>;
    CreatedBy: KnockoutObservable<string>;
    UpdatedDate: KnockoutObservable<string>;
    UpdatedBy: KnockoutObservable<string>;
    SupplierContacts: KnockoutObservableArray<SupplierContactVM>;
    SupplierLocations: KnockoutObservableArray<SupplierLocationVM>;
    SupplierDepartments: KnockoutObservableArray<SupplierDepartmentVM>;
    SupplierItems: KnockoutObservableArray<ISupplierItemModel> = ko.observableArray([]);

    constructor(data, supplierItems: ISupplierItemModel[]) {
        data = data || {};
        this.SupplierID = ko.observable(data.SupplierID);
        this.SupplierCode = ko.observable(data.SupplierCode);
        this.SupplierCompanyName = ko.observable(data.SupplierCompanyName);
        this.ParentSupplierID = ko.observable(data.ParentSupplierID);
        this.CompanyID = ko.observable(data.CompanyID);
        this.NoteID = ko.observable(data.NoteID);
        this.IsHaulier = ko.observable(data.IsHaulier);
        this.IsFactor = ko.observable(data.IsFactor);
        this.CreatedDate = ko.observable(data.CreatedDate);
        this.CreatedBy = ko.observable(data.CreatedBy);
        this.UpdatedDate = ko.observable(data.UpdatedDate);
        this.UpdatedBy = ko.observable(data.UpdatedBy);
        this.SupplierContacts = ko.observableArray<SupplierContactVM>([]);
        this.SupplierLocations = ko.observableArray<SupplierLocationVM>([]);
        this.SupplierDepartments = ko.observableArray<SupplierDepartmentVM>([]);
        for (var item of supplierItems) {
            this.SupplierItems.push(item);
        }
    }
}

class SupplierItemViewModel implements ISupplierItemModel {
    SupplierItemID: KnockoutObservable<string>;
    SupplierID: KnockoutObservable<string>;
    SupplierCode: KnockoutObservable<string>;
    SupplierCompanyName: KnockoutObservable<string>;
    ParentSupplierID: KnockoutObservable<string>;
    CompanyID: KnockoutObservable<string>;
    NoteID: KnockoutObservable<string>;
    IsHaulier: KnockoutObservable<boolean>;
    IsFactor: KnockoutObservable<boolean>;
    CreatedDate: KnockoutObservable<string>;
    CreatedBy: KnockoutObservable<string>;
    UpdatedDate: KnockoutObservable<string>;
    UpdatedBy: KnockoutObservable<string>;

    constructor(data) {
        data = data || {};
        this.SupplierID = ko.observable(data.SupplierID);
        this.SupplierCode = ko.observable(data.SupplierCode);
        this.SupplierCompanyName = ko.observable(data.SupplierCompanyName);
        this.ParentSupplierID = ko.observable(data.ParentSupplierID);
        this.CompanyID = ko.observable(data.CompanyID);
        this.NoteID = ko.observable(data.NoteID);
        this.IsHaulier = ko.observable(data.IsHaulier);
        this.IsFactor = ko.observable(data.IsFactor);
        this.CreatedDate = ko.observable(data.CreatedDate);
        this.CreatedBy = ko.observable(data.CreatedBy);
        this.UpdatedDate = ko.observable(data.UpdatedDate);
        this.UpdatedBy = ko.observable(data.UpdatedBy);
    }
}
