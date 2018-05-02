class ProduceDetailModel {
    UseLookupTables: LookupTables;
    TabContext: KnockoutObservable<AppUserContextModel>;

    constructor(data, lookupTables: LookupTables) {
        this.UseLookupTables = lookupTables;
        this.TabContext = ko.observable(new AppUserContextModel(data.UserContextModel, this.UseLookupTables));
        //this.TabContext().Enable(false);
    }
} 