/// <reference path="../util/mycustomoption.ts" />
class UserRole {
    UseLookupTables: LookupTables;
    UserRoleID: KnockoutObservable<string>;
    RoleID: KnockoutObservable<string>;
    UserID: KnockoutObservable<string>;
    Context: KnockoutObservable<AppUserContextModel>;

    constructor(data, companies, lookupTables) {
        this.UseLookupTables = lookupTables;
        this.UserRoleID = ko.observable(data.UserRoleID);

        this.RoleID = ko.observable(data.RoleID);
        this.UserID = ko.observable(data.UserID);

        //this.Context = ko.observable(new AppUserContextModel(companies, false));
        this.Context = ko.observable(new AppUserContextModel(companies, this.UseLookupTables));

        this.Context().SelectedCompanyId(data.CompanyId);
        this.Context().SelectedDivisionId(data.DivisionId);
        this.Context().SelectedDepartmentId(data.DepartmentId);
    }
}

class AppUserContextModel {
    UseLookupTables: LookupTables;
    SelectedCompanyId: KnockoutObservable<string>;
    SelectedDivisionId: KnockoutObservable<string>;
    SelectedDepartmentId: KnockoutObservable<string>;
    SelectedCompanyText: KnockoutObservable<string>;
    SelectedDivisionText: KnockoutObservable<string>;
    SelectedDivisionAutoGenerateConsignmentReference: KnockoutObservable<boolean>;
    SelectedDepartmentText: KnockoutObservable<string>;
    SelectedDepartmentCode: KnockoutObservable<string>;
    PreviousSelectedDepartmentId: KnockoutObservable<string>;
    Companies: KnockoutComputed<UserPermissionCompany[]>;
    Divisions: KnockoutComputed<UserPermissionDivision[]>;
    Departments: KnockoutComputed<UserPermissionDepartment[]>;
    RequiredPermissionIDs: KnockoutObservableArray<string>;
    IncludeInactiveOptions: KnockoutObservable<boolean>;

    ShowCompanies: KnockoutObservable<boolean>;
    ShowDepartments: KnockoutObservable<boolean>;
    ShowDivisions: KnockoutObservable<boolean>;
    IsCompanyVisible: KnockoutComputed<boolean>;
    IsDivisionVisible: KnockoutComputed<boolean>;
    IsDepartmentVisible: KnockoutComputed<boolean>;

    arrTemp: string[];

    constructor(data, lookupTables) {
        data = data || {};

        this.UseLookupTables = lookupTables;
        this.SelectedCompanyId = ko.observable("");
        this.SelectedDivisionId = ko.observable("");
        this.SelectedDepartmentId = ko.observable("");
        this.PreviousSelectedDepartmentId = ko.observable("");
        this.SelectedCompanyText = ko.observable("");
        this.SelectedDivisionText = ko.observable("");
        this.SelectedDepartmentText = ko.observable("");
        this.SelectedDepartmentCode = ko.observable("");
        this.ShowCompanies = ko.observable(true);
        this.ShowDepartments = ko.observable(true);
        this.ShowDivisions = ko.observable(true);
        this.RequiredPermissionIDs = ko.observableArray<string>([]);
        this.IncludeInactiveOptions = ko.observable(false);

        if (this.UseLookupTables.primeDefaultCompanyID != "") {
            this.SelectedCompanyId(this.UseLookupTables.primeDefaultCompanyID);
            this.UpdateCompanies();
        }
        if (this.UseLookupTables.primeDefaultDivisionID != "") {
            this.SelectedDivisionId(this.UseLookupTables.primeDefaultDivisionID);
            this.UpdateDivisions();
        }
        if (this.UseLookupTables.primeDefaultDepartmentID != "") {
            this.SelectedDepartmentId(this.UseLookupTables.primeDefaultDepartmentID);
            this.UpdateDepartments();
        }


        this.Companies = ko.computed({
            owner: this,
            read: () => {
                this.arrTemp = [];
                if (this.IncludeInactiveOptions()) {
                    return ko.utils.arrayFilter(this.UseLookupTables.primePermissionCompanyList(), (i) => {
                        if (this.arrTemp.indexOf(i.CompanyID) == -1) {
                            this.arrTemp.push(i.CompanyID);
                            return this.RequiredPermissionIDs().indexOf(i.PermissionID) != -1;
                        } else {
                            return false;
                        }
                    });
                } else {
                    return ko.utils.arrayFilter(this.UseLookupTables.primePermissionCompanyList(), (i) => {
                        if (this.arrTemp.indexOf(i.CompanyID) == -1) {
                            this.arrTemp.push(i.CompanyID);
                            return i.IsActive == true && this.RequiredPermissionIDs().indexOf(i.PermissionID.toLowerCase()) != -1;
                        } else {
                            return false;
                        }
                    });
                }
            }
        });
        this.Divisions = ko.computed({
            owner: this,
            read: () => {
                this.arrTemp = [];
                if (this.IncludeInactiveOptions()) {
                    return ko.utils.arrayFilter(this.UseLookupTables.primePermissionDivisionList(), (i) => {
                        if (this.arrTemp.indexOf(i.DivisionID) == -1) {
                            this.arrTemp.push(i.DivisionID);
                            return i.CompanyID == this.SelectedCompanyId() && this.RequiredPermissionIDs().indexOf(i.PermissionID.toLowerCase()) != -1;
                        } else {
                            return false;
                        }
                    });
                } else {
                    return ko.utils.arrayFilter(this.UseLookupTables.primePermissionDivisionList(), (i) => {
                        if (this.arrTemp.indexOf(i.DivisionID) == -1) {
                            this.arrTemp.push(i.DivisionID);
                            return i.IsActive == true && i.CompanyID == this.SelectedCompanyId() && this.RequiredPermissionIDs().indexOf(i.PermissionID.toLowerCase()) != -1;
                        } else {
                            return false;
                        }
                    });
                }
            }
        });
        this.Departments = ko.computed({
            owner: this,
            read: () => {
                this.arrTemp = [];
                if (this.IncludeInactiveOptions()) {
                    return ko.utils.arrayFilter(this.UseLookupTables.primePermissionDepartmentList(), (i) => {
                        if (this.arrTemp.indexOf(i.DepartmentID) == -1) {
                            this.arrTemp.push(i.DepartmentID);
                            return i.DivisionID == this.SelectedDivisionId() && this.RequiredPermissionIDs().indexOf(i.PermissionID) != -1;
                        } else {
                            return false;
                        }
                    });
                } else {
                    return ko.utils.arrayFilter(this.UseLookupTables.primePermissionDepartmentList(), (i) => {
                        if (this.arrTemp.indexOf(i.DepartmentID) == -1) {
                            this.arrTemp.push(i.DepartmentID);
                            return i.IsActive == true && i.DivisionID == this.SelectedDivisionId() && this.RequiredPermissionIDs().indexOf(i.PermissionID.toLowerCase()) != -1;
                        } else {
                            return false;
                        }
                    });
                }
            }
        });

        this.IsCompanyVisible = ko.pureComputed({
            owner: this,
            read: () => {
                //return this.ShowCompanies() && (this.Companies().length > 1);
                return true;
            }
        });
        this.IsDivisionVisible = ko.pureComputed({
            owner: this,
            read: () => {
                return this.ShowDivisions() && (this.Divisions().length > 1);
            }
        });
        this.IsDepartmentVisible = ko.pureComputed({
            owner: this,
            read: () => {
                return this.ShowDepartments() && (this.Departments().length > 1);
            }
        });
    }
    SelectCompany = function (companyID: string) {
        this.SelectedCompanyId(companyID);
        this.UpdateCompanies();
    }
    SelectDivision = function (divisionID: string) {
        this.SelectedDivisionId(divisionID);
        this.UpdateDivisions();
    }
    SelectDepartment = function (departmentID: string) {
        this.SelectedDepartmentId(departmentID);
        this.UpdateDepartments();
    }

    UpdateDepartments = () => {
        var filter = this.SelectedDepartmentId();
        var selectedDepartment = ko.utils.arrayFilter(this.UseLookupTables.primeDepartmentList(), (i) => {
            return i.itemID == filter;
        });
        if (selectedDepartment.length == 0) {
            this.SelectedDepartmentText('');
            this.SelectedDepartmentId('');
        } else {
            this.SelectedDepartmentText(selectedDepartment[0].itemName);
            this.SelectedDepartmentCode(selectedDepartment[0].itemCode);
        }
    }

    UpdateDivisions = () => {
        var filter = this.SelectedDivisionId();
        var selectedDivision = ko.utils.arrayFilter(this.UseLookupTables.primeDivisionList(), (i) => {
            return i.itemID == filter;
        });
        if (selectedDivision.length == 0) {
            this.SelectedDivisionText('');
            this.SelectedDivisionId('');
            this.SelectedDivisionAutoGenerateConsignmentReference = ko.observable(true);
        } else {
            this.SelectedDivisionText(selectedDivision[0].itemName);
            this.SelectedDivisionAutoGenerateConsignmentReference = ko.observable(selectedDivision[0].option);
        }
        if (this.Departments) {
            if (this.Departments().length > 0) {
                this.SelectedDepartmentId(this.Departments()[0].DepartmentID);
            } else {
                this.SelectedDepartmentId('');
            }
            this.UpdateDepartments();
        }
    }

    UpdateCompanies = () => {
        var filter = this.SelectedCompanyId();
        var selectedCompany = ko.utils.arrayFilter(this.UseLookupTables.primeCompanyList(), (i) => {
            return i.itemID == filter;
        });
        if (selectedCompany.length == 0) {
            this.SelectedCompanyText('');
            this.SelectedCompanyId('');
        } else {
            this.SelectedCompanyText(selectedCompany[0].itemName);
        }
        if (this.Divisions) {
            if (this.Divisions.length > 0) {
                this.SelectedDivisionId(this.Divisions()[0].DivisionID);
            } else {
                this.SelectedDivisionId('');
            }
            this.UpdateDivisions();
        }
    }
    AddRequiredPermission = (permissionID: string) => {
        this.RequiredPermissionIDs.push(permissionID.toLowerCase());
    }
}
