/// <reference path="../../Scripts/typings/jquery/jquery.d.ts" />
/// <reference path="../../Scripts/typings/knockout/knockout.d.ts" />

class BankReconciliationHeaderItem {
    BankStatementID: KnockoutObservable<string>;
    BankStatementImportDate: KnockoutObservable<Date>;
    BankStatementStartDate: KnockoutObservable<Date>;
    BankStatementEndDate: KnockoutObservable<Date>;
    BankStatementFileName: KnockoutObservable<string>;
    BankStatementReconciled: KnockoutObservable<boolean>;
    BankAccountID: KnockoutObservable<string>;
    IsSelected: KnockoutObservable<boolean>;
    IsActiveReconciliation: KnockoutObservable<boolean>;
    ShortFileName: KnockoutObservable<string>;
    OpeningBalance: KnockoutObservable<number>;
    CurrentBalance: KnockoutObservable<number>;
    CanBeReconciled: KnockoutObservable<boolean>;


    constructor(data) {
        data = data || {};
        this.BankStatementID = ko.observable(data.BankStatementID || "");
        this.BankStatementImportDate = ko.observable(data.BankStatementImportDate || "");
        this.BankStatementFileName = ko.observable(data.BankStatementFileName || "");
        this.BankStatementReconciled = ko.observable(data.BankStatementReconciled || false);
        this.BankAccountID = ko.observable(data.BankAccountID || "");
        this.BankStatementStartDate = ko.observable(data.BankStatementStartDate || "");
        this.BankStatementEndDate = ko.observable(data.BankStatementEndDate || "");
        this.IsSelected = ko.observable(data.IsSelected || false);
        this.IsActiveReconciliation = ko.observable(data.IsActiveReconciliation || false);
        this.ShortFileName = ko.observable(data.BankStatementFileName || "");
        this.OpeningBalance = ko.observable(data.OpeningBalance);
        this.CurrentBalance = ko.observable(data.CurrentBalance);
        this.CanBeReconciled = ko.observable(data.CanBeReconciled);

        var t1 = this.BankStatementFileName();
        if (t1.length > 0) {
            var n = t1.lastIndexOf('/');
            var t2 = t1.slice(n + 1);
            this.ShortFileName(t2);
        }

    }
}     