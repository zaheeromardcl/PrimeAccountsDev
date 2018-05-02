/// <Number path="../../KJScripts/PagingTS.ts" />
class BankAccountPagingModel {
    Paging: KnockoutObservable<Paging>;
    BankAccountSearch: KnockoutObservable<BankAccountSearch>;
    SearchClicked: KnockoutObservable<boolean>;
    IsSearchValid: KnockoutComputed<boolean>;

    constructor(data) {
        this.Paging = ko.observable(new Paging(data.BankAccountEditModels, data.SearchObject));
        this.BankAccountSearch = ko.observable(new BankAccountSearch());
        this.SearchClicked = ko.observable(false);
        this.IsSearchValid = ko.computed({
            read: () => {
                return (this.SearchClicked() && !this.BankAccountSearch().validationModel.isValid())
            }
        });
    }

    Search = function () {
        this.SearchClicked(true);
        if (this.BankAccountSearch().validationModel.isValid()) {
            this.Paging().fetchEntitiesWithSearch("BankAccount/Index", this.BankAccountSearch);
        }
    }

    SearchBySupplierDepartmentId = function () {
        this.SearchClicked(false);
        this.BankAccountSearch().AccountNamme("");
        this.Paging().fetchEntitiesWithSearch("BankAccount/Index", this.BankAccountSearch);
    }

    Reset = function () {
        this.SearchClicked(false);
        // reset search box
        this.BankAccountSearch().BankAccountNumber("");
        // returns all entries
        this.Paging().fetchEntitiesWithSearch("BankAccount/Index", this.BankAccountSearch);
    }

    OpenBankAccountDetails = function (data) {
        var uri = "BankAccount/DetailsTab/" + data;

    }
}

class BankAccountSearch {
    AccountNamme: KnockoutObservable<string>;
    SupplierDepartmentID: KnockoutObservable<string>;
    validationModel: KnockoutObservable<any>;
    RecordsInDays: KnockoutObservable<string>;

    constructor() {
        this.AccountNamme = ko.observable("").extend({ required: true });
        this.AccountNamme.isModified(false);
        this.SupplierDepartmentID = ko.observable("0");
        this.RecordsInDays = ko.observable("LASTMONTH");
        this.validationModel = ko.validatedObservable({
            AccountNumber: this.AccountNamme
        });
    }
}