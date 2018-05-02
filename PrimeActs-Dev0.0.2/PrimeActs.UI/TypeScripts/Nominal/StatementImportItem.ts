class StatementImportItem {
    StatementId: KnockoutObservable<string>;
    AccountNumber: KnockoutObservable<string>;
    Description: KnockoutObservable<string>;
    Narrative1: KnockoutObservable<string>;
    Narrative2: KnockoutObservable<string>;
    Date: KnockoutObservable<Date>;
    Type: KnockoutObservable<string>;
    Amount: KnockoutObservable<number>;
    IsSelected: KnockoutObservable<boolean>;
    IsReconciled: KnockoutObservable<boolean>;
    UnReconcileCheck: KnockoutObservable<boolean>;
    HasPossibleMatchingNominal: KnockoutObservable<boolean>;


    constructor(data) {
        data = data || {};
        this.StatementId = ko.observable(data.StatementId || "");
        this.AccountNumber = ko.observable(data.AccountNumber || "");
        this.Description = ko.observable(data.Description || "");
        this.Narrative1 = ko.observable(data.Narrative1 || "");
        this.Narrative2 = ko.observable(data.Narrative2 || "");
        this.Date = ko.observable(data.Date || "");
        this.Type = ko.observable(data.Type || "");
        this.Amount = ko.observable(data.Amount || 0);
        this.IsSelected = ko.observable(data.IsSelected);
        this.IsReconciled = ko.observable(data.IsReconciled); // example show in list but not select button
        this.UnReconcileCheck = ko.observable(data.UnReconcileCheck || false);
        this.HasPossibleMatchingNominal = ko.observable(data.HasPossibleMatchingNominal || false);
    }
}    