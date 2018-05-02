
class LookupTables {
    //Default values
    primeDefaultCompanyID: string;
    primeDefaultDivisionID: string;
    primeDefaultDepartmentID: string;
    //Lookup tables
    primeCountryList: KnockoutObservableArray<MyCustomOptionFKExtended>;
    primeCurrencyList: KnockoutObservableArray<MyCustomOptionFKExtended>;
    primeCustomerTypeList: KnockoutObservableArray<MyCustomOptionFKExtended>;
    primeDespatchLocationList: KnockoutObservableArray<MyCustomOptionFKExtended>;
    primePackWtUnitList: KnockoutObservableArray<MyCustomOptionFKExtended>;
    primePortList: KnockoutObservableArray<MyCustomOptionFKExtended>;
    primePorterageList: KnockoutObservableArray<MyCustomOptionFKExtended>;
    primePurchaseChargeTypeList: KnockoutObservableArray<MyCustomOptionFKExtended>;
    primePurchaseTypeList: KnockoutObservableArray<MyCustomOptionFKExtended>;
    primeTransactionTaxLocationList: KnockoutObservableArray<MyCustomOptionFKExtended>;
    //Tables that act like lookup tables but are actualy standard tables that are rarely updated
    primeMasterGroupList: KnockoutObservableArray<MyCustomOptionFKExtended>;
    primeNominalAccountList: KnockoutObservableArray<MyCustomOptionFKExtended>;
    primePermissionList: KnockoutObservableArray<UserPermission>;
    primePermissionCompanyList: KnockoutObservableArray<UserPermissionCompany>;
    primePermissionDivisionList: KnockoutObservableArray<UserPermissionDivision>;
    primePermissionDepartmentList: KnockoutObservableArray<UserPermissionDepartment>;
    primeCompanyList: KnockoutObservableArray<MyCustomOptionFKExtended>;
    primeDivisionList: KnockoutObservableArray<MyCustomOptionFKExtended>;
    primeDepartmentList: KnockoutObservableArray<MyCustomOptionFKExtended>;
    constructor() {
        this.primeCountryList = ko.observableArray<MyCustomOptionFKExtended>([]);
        this.primeCurrencyList = ko.observableArray<MyCustomOptionFKExtended>([]);
        this.primeCustomerTypeList = ko.observableArray<MyCustomOptionFKExtended>([]);
        this.primeDespatchLocationList = ko.observableArray<MyCustomOptionFKExtended>([]);
        this.primePackWtUnitList = ko.observableArray<MyCustomOptionFKExtended>([]);
        this.primePortList = ko.observableArray<MyCustomOptionFKExtended>([]);
        this.primePorterageList = ko.observableArray<MyCustomOptionFKExtended>([]);
        this.primePurchaseChargeTypeList = ko.observableArray<MyCustomOptionFKExtended>([]);
        this.primePurchaseTypeList = ko.observableArray<MyCustomOptionFKExtended>([]);
        this.primeTransactionTaxLocationList = ko.observableArray<MyCustomOptionFKExtended>([]);
        this.primePermissionList = ko.observableArray<UserPermission>([]);
        this.primePermissionCompanyList = ko.observableArray<UserPermissionCompany>([]);
        this.primePermissionDivisionList = ko.observableArray<UserPermissionDivision>([]);
        this.primePermissionDepartmentList = ko.observableArray<UserPermissionDepartment>([]);
        this.primeCompanyList = ko.observableArray<MyCustomOptionFKExtended>([]);
        this.primeDivisionList = ko.observableArray<MyCustomOptionFKExtended>([]);
        this.primeDepartmentList = ko.observableArray<MyCustomOptionFKExtended>([]);

        var promise =
            $.ajax({
                url: "/API/LookupTables/GetLookupLists/",
                cache: false,
                type: "GET",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                success: (result) => {
                    this.primeDefaultCompanyID = result.DefaultCompanyID;
                    this.primeDefaultDivisionID = result.DefaultDivisionID;
                    this.primeDefaultDepartmentID = result.DefaultDepartmentID;
                    for (let item of result.Country) {
                        this.primeCountryList.push(new MyCustomOptionFKExtended(item.CountryID.toLowerCase(), item.CountryName, item.CountryCode, item.CompanyID.toLowerCase(), true));
                    }
                    for (let item of result.Currency) {
                        this.primeCurrencyList.push(new MyCustomOptionFKExtended(item.CurrencyID.toLowerCase(), item.CurrencyName, item.CurrencyCode, item.CompanyID.toLowerCase(), true));
                    }
                    for (let item of result.CustomerType) {
                        this.primeCustomerTypeList.push(new MyCustomOptionFKExtended(item.CustomerTypeID.toLowerCase(), item.CustomerName, item.CustomerTypeCode, item.CompanyID.toLowerCase(), true));
                    }
                    for (let item of result.DespatchLocation) {
                        this.primeDespatchLocationList.push(new MyCustomOptionFKExtended(item.DespatchLocationID.toLowerCase(), item.DespatchLocationName, item.DespatchLocationCode, item.CompanyID.toLowerCase(), true));
                    }
                    for (let item of result.PackWtUnit) {
                        this.primePackWtUnitList.push(new MyCustomOptionFKExtended(item.PackWtUnitID.toLowerCase(), item.WtUnit, item.KGMultiple, item.CompanyID.toLowerCase(), true));
                    }
                    for (let item of result.Port) {
                        this.primePortList.push(new MyCustomOptionFKExtended(item.PortID.toLowerCase(), item.Portname, item.PortCode, item.CompanyID.toLowerCase(), true));
                    }
                    for (let item of result.Porterage) {
                        this.primePorterageList.push(new MyCustomOptionFKExtended(item.PorterageID.toLowerCase(), item.PorterageCode, item.UnitPrice, item.DepartmentID.toLowerCase(), true));
                    }
                    for (let item of result.PurchaseChargeType) {
                        this.primePurchaseChargeTypeList.push(new MyCustomOptionFKExtended(item.PurchaseChargeTypeID.toLowerCase(), item.PurchaseChargeTypeName, item.PurchaseChargeTypeCode, item.CompanyID.toLowerCase(), true));
                    }
                    for (let item of result.PurchaseType) {
                        this.primePurchaseTypeList.push(new MyCustomOptionFKExtended(item.PurchaseTypeID.toLowerCase(), item.PurchaseTypeName, item.PurchaseTypeCode, item.CompanyID.toLowerCase(), true));
                    }
                    for (let item of result.TransactionTaxLocation) {
                        this.primeTransactionTaxLocationList.push(new MyCustomOptionFKExtended(item.TransactionTaxLocationID.toLowerCase(), item.TransactionTaxLocationName, item.TransactionTaxLocationNominalAccountID.toLowerCase(), item.CompanyID.toLowerCase(), true));
                    }
                    for (let item of result.Company) {
                        this.primeCompanyList.push(new MyCustomOptionFKExtended(item.CompanyID.toLowerCase(), item.CompanyName, '', '', item.IsActive));
                    }
                    for (let item of result.Division) {
                        this.primeDivisionList.push(new MyCustomOptionFKExtended(item.DivisionID.toLowerCase(), item.DivisionName, '', item.CompanyID.toLowerCase(), item.IsActive, item.AutogenerateConsignnmentReference));
                    }
                    for (let item of result.Department) {
                        this.primeDepartmentList.push(new MyCustomOptionFKExtended(item.DepartmentID.toLowerCase(), item.DepartmentName, item.DepartmentCode, item.DivisionID.toLowerCase(), item.IsActive));
                    }
                    //permissions
                    for (let item of result.PermissionDetail) {
                        var item1 = item;
                        this.primePermissionList.push(new UserPermission(item1.PermissionID, item1.CompanyID, item1.DivisionID, item1.DepartmentID));
                        var match = ko.utils.arrayFirst(this.primePermissionCompanyList(), (item2) => {
                            return item1.CompanyID == item2.CompanyID && item1.PermissionID == item2.PermissionID;
                        });
                        if (!match) {
                            var company = ko.utils.arrayFirst(this.primeCompanyList(), (item2) => {
                                return item2.itemID == item1.CompanyID;
                            });
                            this.primePermissionCompanyList.push(new UserPermissionCompany(item1.PermissionID, item1.CompanyID, company.itemName, company.isActive));
                        }
                        var match2 = ko.utils.arrayFirst(this.primePermissionDivisionList(), (item2) => {
                            return item1.DivisionID == item2.DivisionID && item1.PermissionID == item2.PermissionID;
                        });
                        if (!match2) {
                            var division = ko.utils.arrayFirst(this.primeDivisionList(), (item2) => {
                                return item2.itemID == item1.DivisionID;
                            });
                            this.primePermissionDivisionList.push(new UserPermissionDivision(item1.PermissionID, item1.DivisionID, division.itemName, division.isActive, division.foreignKey));
                        }
                        var match3 = ko.utils.arrayFirst(this.primePermissionDepartmentList(), (item2) => {
                            return item1.DepartmentID == item2.DepartmentID && item1.PermissionID == item2.PermissionID;
                        });
                        if (!match3) {
                            var department = ko.utils.arrayFirst(this.primeDepartmentList(), (item2) => {
                                return item2.itemID == item1.DepartmentID;
                            });
                            this.primePermissionDepartmentList.push(new UserPermissionDepartment(item1.PermissionID, item1.DepartmentID, department.itemName, department.isActive, department.foreignKey, department.itemCode));
                        }
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {

                    console.log(errorThrown);
                }
            }).fail(
                function (xhr, textStatus, err) {
                    console.log("error getting the item defaults" + err);
                    //alert(err);
                });
        promise.done(function (data) {
        });
    }
}