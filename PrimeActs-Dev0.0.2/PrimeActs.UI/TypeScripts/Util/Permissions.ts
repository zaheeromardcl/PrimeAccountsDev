class UserPermission {
    PermissionID: string;
    CompanyID: string;
    DivisionID: string;
    DepartmentID: string;

    constructor(PermissionID: string, CompanyID: string, DivisionID: string, DepartmentID: string) {
        this.PermissionID = PermissionID;
        this.CompanyID = CompanyID;
        this.DivisionID = DivisionID;
        this.DepartmentID = DepartmentID;
    }
}
class UserPermissionCompany {
    PermissionID: string;
    CompanyID: string;
    CompanyName: string;
    IsActive: boolean;

    constructor(PermissionID: string, CompanyID: string, CompanyName: string, IsActive: boolean) {
        this.PermissionID = PermissionID;
        this.CompanyID = CompanyID;
        this.CompanyName = CompanyName;
        this.IsActive = IsActive;
    }
}
class UserPermissionDivision {
    PermissionID: string;
    DivisionID: string;
    DivisionName: string;
    IsActive: boolean;
    CompanyID: string;

    constructor(PermissionID: string, DivisionID: string, DivisionName: string, IsActive: boolean, CompanyID: string) {
        this.PermissionID = PermissionID;
        this.DivisionID = DivisionID;
        this.DivisionName = DivisionName;
        this.IsActive = IsActive;
        this.CompanyID = CompanyID;
    }
}
class UserPermissionDepartment {
    PermissionID: string;
    DepartmentID: string;
    DepartmentName: string;
    DepartmentCode: string;
    IsActive: boolean;
    DivisionID: string;

    constructor(PermissionID: string, DepartmentID: string, DepartmentName: string, IsActive: boolean, DivisionID: string, DepartmentCode: string) {
        this.PermissionID = PermissionID;
        this.DepartmentID = DepartmentID;
        this.DepartmentName = DepartmentName;
        this.DepartmentCode = DepartmentCode;
        this.IsActive = IsActive;
        this.DivisionID = DivisionID;
    }
}