class ReconciliationItem {
    RecId: KnockoutObservable<string>;
    Description: KnockoutObservable<string>;
    Date: KnockoutObservable<Date>;
    Type: KnockoutObservable<string>;
    Amount: KnockoutObservable<number>;
    Balance: KnockoutObservable<number>;
    IsSelected: KnockoutObservable<boolean>;
    Test: KnockoutObservable<string>;
    IsReconciled: KnockoutObservable<boolean>;
    isComputed: KnockoutComputed<boolean>;
    ReconciledStatements: KnockoutObservableArray<string>;
    UnReconcileCheck: KnockoutObservable<boolean>;
    
    constructor(data) {
        data = data || {};
        this.RecId = ko.observable(data.RecId || "");
        this.Description = ko.observable(data.Description || "");
        this.Date = ko.observable(data.Date || "");
        this.Type = ko.observable(data.Type || "");
        this.Amount = ko.observable(data.Amount || 0);
        this.Balance = ko.observable(data.Balance || 0);
        this.IsSelected = ko.observable(data.IsSelected);
        this.Test = ko.observable(data.Test);
        this.IsReconciled = ko.observable(data.IsReconciled); // example show in list but not select button
        this.ReconciledStatements = ko.observableArray([]);
        this.UnReconcileCheck = ko.observable(data.UnReconcileCheck || false);
        

        this.isComputed = ko.computed({ // did not work?
            read: () => {
                if (this.IsSelected()) return true;
                else return true;
                //return this.IsSelected ? true : false;
            }
        });
    }
} 