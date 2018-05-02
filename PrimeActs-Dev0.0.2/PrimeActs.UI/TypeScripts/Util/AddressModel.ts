class AddressModel {
    AddressLines: KnockoutObservable<string>;
    AddressLine1: KnockoutObservable<string>;
    AddressLine2: KnockoutObservable<string>;
    AddressLine3: KnockoutObservable<string>;
    Postcode: KnockoutObservable<string>;
    PostalTown: KnockoutObservable<string>;
    CountyCity: KnockoutObservable<string>;

    constructor(data) {
        data = data || {};
        this.AddressLine1 = ko.observable(data.AddressLine1);
        this.AddressLine2 = ko.observable(data.AddressLine2);
        this.AddressLine3 = ko.observable(data.AddressLine3);
        this.AddressLines = ko.observable(data.AddressLine1 + " " + data.AddressLine2 + " " + data.AddressLine3);
        this.Postcode = ko.observable(data.Postcode);
        this.PostalTown = ko.observable(data.PostalTown);
        this.CountyCity = ko.observable(data.CountyCity);
    }
}