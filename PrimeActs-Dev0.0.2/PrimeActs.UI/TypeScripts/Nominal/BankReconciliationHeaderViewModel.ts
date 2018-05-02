/// <reference path="BankReconciliationHeaderItem.ts" />
/// <reference path="../../Scripts/typings/knockout/knockout.d.ts" /> 
class BankReconciliationHeaderViewModel {
    BankReconciliationHeaderItems: KnockoutObservableArray<BankReconciliationHeaderItem>;
    BankStatementID: KnockoutObservable<string>;
    BankAccountID: KnockoutObservable<string>;
    BankStatementImportDate: KnockoutObservable<Date>;
    BankStatementStartDate: KnockoutObservable<Date>;
    BankStatementEndDate: KnockoutObservable<Date>;
    BankStatementFileName: KnockoutObservable<string>;
    BankStatementReconciled: KnockoutObservable<boolean>;
    CurrentBalance: KnockoutObservable<number>;
    OpeningBalance: KnockoutObservable<number>;
    PostReconciliationChange: () => void;
    UpdateHeader: () => void;
    ReconcileStatement: () => void;
    CurrentIndex: KnockoutObservable<number>;
    //BankReconciliationDate: KnockoutObservable<Date>;
    ExecuteReconciliationEdit: () => void;
    RemoveReconciliation: () => void;
    MaxFileSize: KnockoutObservable<number>;
    MaxNrOfFiles: KnockoutObservable<number>;
    MainFolder: KnockoutObservable<string>;
    UploadFolder: KnockoutObservable<string>;
    CanCallDetailsPage: KnockoutObservable<boolean>;
    ComputedShortFileName: () => void;
    ComputedCurrentBalance: () => void;
    CanBeReconciled: KnockoutObservable<boolean>;
    ComputedCanBeReconciled: () => boolean;
    ComputedCanBeDeleted: () => boolean;
    ComputedCanAdd: () => boolean;

    //CompletedClicked: () => boolean ;

    constructor(data) {
        data = data || {};
        this.BankReconciliationHeaderItems = ko.observableArray([]).extend({ notify: 'always' });;
        this.BankStatementID = ko.observable(data.BankStatementID || "");
        this.BankAccountID = ko.observable(data.BankAccountID || "");
        this.BankStatementImportDate = ko.observable(data.BankStatementImportDate || "");
        this.BankStatementFileName = ko.observable(data.BankStatementFileName || "");
        this.BankStatementReconciled = ko.observable(data.BankStatementReconciled || false);
        this.BankStatementStartDate = ko.observable(data.BankStatementStartDate || "");
        this.BankStatementEndDate = ko.observable(data.BankStatementEndDate || "");
        this.CurrentIndex = ko.observable(data.CurrentIndex || 0);
        this.MaxFileSize = ko.observable(data.MaxFileSize || 1024);
        this.MaxNrOfFiles = ko.observable(data.MaxNrOfFiles || 99);
        this.MainFolder = ko.observable(data.MainFolder || "");
        this.UploadFolder = ko.observable(data.UploadFolder || "");
        this.CanCallDetailsPage = ko.observable(data.CanCallDetailsPage);
        this.CurrentBalance = ko.observable(data.CurrentBalance || 0);
        this.OpeningBalance = ko.observable(data.OpeningBalance || 0);
        this.CanBeReconciled = ko.observable(data.CanBeReconciled || false);

        if (data.hasOwnProperty('BankReconciliationHeaderItems')) {
            for (let statement of data.BankReconciliationHeaderItems) {
                if (statement != undefined) {
                    let item: BankReconciliationHeaderItem = new BankReconciliationHeaderItem(statement);
                    if (item.IsActiveReconciliation() == true) { item.IsSelected(true) }
                    this.BankReconciliationHeaderItems.push(item);
                }
            }
        }

        this.ComputedCanAdd = ko.computed(() => {
           
            for (let bankReconciliationHeaderItem of this.BankReconciliationHeaderItems()) {
                if (bankReconciliationHeaderItem.IsActiveReconciliation() == true) {
                    return false;
                }
            }
            return true;
        });


        this.ComputedCanBeReconciled = ko.pureComputed(() => {
            // work in progress, Note to add other conditions
            return this.CanBeReconciled();
        });

        this.ComputedCanBeDeleted = ko.pureComputed(() => {
            // work in progress, Note to add other conditions
            if (this.BankStatementReconciled()) {
                return false;
            } else return true;
        });

        this.ComputedShortFileName = ko.pureComputed(() => {
            var t1 = this.BankStatementFileName();
            if (t1.length > 0) {
                var n = t1.lastIndexOf('/');
                var t2 = t1.slice(n + 1);
                return t2;
            }
            return '';
        });

        this.ComputedCurrentBalance = ko.pureComputed(() => {
            let temptotal = Number(this.CurrentBalance().toFixed(2)) + Number(this.OpeningBalance().toFixed(2));
            return Number(temptotal.toFixed(2));
        });
       

        this.PostReconciliationChange = () => {

            var self = this;

            var promise =
                $.ajax({
                    url: "/Nominal/AddBankStatementHeader/",
                    cache: false,
                    type: "GET",
                    contentType: "application/json; charset=utf-8",

                    dataType: "json",
                    success: function (result) {
                        let item: BankReconciliationHeaderItem = new BankReconciliationHeaderItem(result);
                        self.BankReconciliationHeaderItems.push(item);
                        self.BankReconciliationHeaderItems.valueHasMutated();
                        let index = self.BankReconciliationHeaderItems().length;

                        self.CurrentIndex(index - 1);
                        self.BankAccountID(result.BankAccountID);
                        self.BankStatementImportDate(result.BankStatementImportDate);
                        self.BankStatementStartDate(result.BankStatementStartDate);
                        self.BankStatementEndDate(result.BankStatementEndDate);
                        self.BankStatementReconciled(result.BankStatementReconciled);
                        self.BankStatementFileName(result.BankStatementFileName);
                        self.CurrentBalance(result.CurrentBalance);
                        self.OpeningBalance(result.OpeningBalance);
                        self.CanBeReconciled(result.CanBeReconciled);
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                    }
                }).fail(
                    function (xhr, textStatus, err) {
                    });
            // Go to Controller
            var url = "BankReconciliationSelect";
            window.location.href = url;
        };


        this.RemoveReconciliation = () => {

            let index = this.CurrentIndex();
            let self = this;

            var promise =
                $.ajax({
                    url: "/Nominal/RemoveBankStatementHeader/",
                    cache: false,
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    data: self.BankReconciliationHeaderItems()[index],
                    dataType: "json",
                    success: function (result) {
                        self.BankReconciliationHeaderItems.remove(self.BankReconciliationHeaderItems()[index]);
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                    }
                }).fail(
                    function (xhr, textStatus, err) {
                    });


        }

        this.UpdateHeader = () => {
            let self = this;
            let index = this.CurrentIndex();
            // let test = this.BankStatementStartDate();
            
            this.BankReconciliationHeaderItems()[index].BankStatementStartDate(self.BankStatementStartDate());
            self.BankReconciliationHeaderItems()[index].BankStatementEndDate(self.BankStatementEndDate());
            self.BankReconciliationHeaderItems()[index].BankStatementImportDate(self.BankStatementImportDate());
            self.BankReconciliationHeaderItems()[index].BankStatementReconciled(self.BankStatementReconciled());
            self.BankReconciliationHeaderItems()[index].BankStatementFileName(self.BankStatementFileName());
            self.BankReconciliationHeaderItems()[index].CurrentBalance(self.CurrentBalance());
            self.BankReconciliationHeaderItems()[index].OpeningBalance(self.OpeningBalance());
            self.BankReconciliationHeaderItems()[index].CanBeReconciled(self.CanBeReconciled());
            var t1 = self.BankStatementFileName();
            if (t1.length > 0) {
                var n = t1.lastIndexOf('/');
                var t2 = t1.slice(n + 1);
                self.BankReconciliationHeaderItems()[index].ShortFileName(t2);
            }
            self.BankReconciliationHeaderItems()[index].BankAccountID(self.BankAccountID());
            self.BankReconciliationHeaderItems.valueHasMutated();

            var promise =
                $.ajax({
                    url: "/Nominal/UpdateBankStatementHeader/",
                    cache: false,
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    data: self.BankReconciliationHeaderItems()[index],
                    dataType: "json",
                    success: function (result) {

                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                    }
                }).fail(
                    function (xhr, textStatus, err) {
                    });
        }

        this.ReconcileStatement = () => {

            var self = this;
           
            var url = "bankrecproto?ID=" + this.BankStatementID();
            window.location.href = url;

        }
    }

    statementSelect(index, root, data) {

        var self = this;
        let CurrentIndex = <number>index;
        root.CurrentIndex(CurrentIndex);

        let test = <string>root.BankReconciliationHeaderItems._latestValue[index].BankStatementFileName();
        let BankStatementID = <string>root.BankReconciliationHeaderItems._latestValue[index].BankStatementID();
        root.BankStatementID(BankStatementID);

        let BankAccountID = <string>root.BankReconciliationHeaderItems._latestValue[index].BankAccountID();
        root.BankAccountID(BankAccountID);

        let filename = <string>root.BankReconciliationHeaderItems._latestValue[index].BankStatementFileName();
        if (filename.length > 0) root.BankStatementFileName(filename); else root.BankStatementFileName("");

        let importdate = <string>root.BankReconciliationHeaderItems._latestValue[index].BankStatementImportDate();
        if (importdate.length > 0) root.BankStatementImportDate(importdate); else root.BankStatementImportDate("");

        let startdate = <string>root.BankReconciliationHeaderItems._latestValue[index].BankStatementStartDate();
        if (startdate.length > 0) root.BankStatementStartDate(startdate); else root.BankStatementStartDate("");

        let enddate = <string>root.BankReconciliationHeaderItems._latestValue[index].BankStatementEndDate();
        if (enddate.length > 0) root.BankStatementEndDate(enddate); else root.BankStatementEndDate("");

        let reconciled = <boolean>root.BankReconciliationHeaderItems._latestValue[index].BankStatementReconciled();
        root.BankStatementReconciled(reconciled);

        let cancalldetailspage = <boolean>root.BankReconciliationHeaderItems._latestValue[index].IsActiveReconciliation();
        root.CanCallDetailsPage(cancalldetailspage);

        let openingbalance = <number>root.BankReconciliationHeaderItems._latestValue[index].OpeningBalance();
        root.OpeningBalance(openingbalance);

        let currentbalance = <number>root.BankReconciliationHeaderItems._latestValue[index].CurrentBalance();
        root.CurrentBalance(currentbalance);

        let canbereconciled = <boolean>root.BankReconciliationHeaderItems._latestValue[index].CanBeReconciled();
        root.CanBeReconciled(canbereconciled);

        for (let statement of root.BankReconciliationHeaderItems._latestValue) {
            statement.IsSelected(false);
        }
        data.IsSelected(true);
        root.BankReconciliationHeaderItems.valueHasMutated();
    }

}

    
//}   