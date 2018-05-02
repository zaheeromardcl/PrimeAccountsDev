/// <reference path="../../KJScripts/PagingTS.ts" />
class ConsignmentIndexPagingModel {
    Paging: KnockoutObservable<Paging>;
    ConsignmentSearch: KnockoutObservable<ConsignmentIndexSearchIndex>;
    SearchClicked: KnockoutObservable<boolean>;
    IsSearchValid: KnockoutComputed<boolean>;
    SubscriberTab: any;

    constructor(data, subscriberTab) {
        data = data || {};
        this.Paging = ko.observable(new Paging(data.ConsignmentIndex || {}, data.SearchObject));
        
        this.ConsignmentSearch = ko.observable(new ConsignmentSearch());
        this.SearchClicked = ko.observable(false);
        this.IsSearchValid = ko.computed({
            read: () => {
                return (this.SearchClicked() && !this.ConsignmentSearch().validationModel.isValid())
            }
        });
        this.SubscriberTab = subscriberTab;
    }

    Search = function () {
        this.SearchClicked(true);
        if (this.ConsignmentSearch().validationModel.isValid()) {
            this.Paging().fetchEntitiesWithSearch("Consignment/Index", this.ConsignmentSearch);
        }
    }

    SearchBySupplierDepartmentId = function () {
        this.SearchClicked(false);
        this.ConsignmentSearch().ConsignmentReference("");
        this.Paging().fetchEntitiesWithSearch("Consignment/Index", this.ConsignmentSearch);
    }

    Reset = function () {
        this.SearchClicked(false);
        // reset search box
        this.ConsignmentSearch().ConsignmentReference("");
        // returns all entries
        this.Paging().fetchEntitiesWithSearch("Consignment/Index", this.ConsignmentSearch);
    }
    
    OpenConsignmentDetails = function (data) {
        var options = {
            TabTitle: "loading...",
            PanelName: "ConsignmentDetails",
            UriParam: data
        };
        this.SubscriberTab.notifySubscribers(options, "save");
    }

    OpenConsignmentEdit = function (data) {
        var options = {
            TabTitle: "loading...",
            PanelName: "ConsignmentEdit",
            UriParam: data
        };
        this.SubscriberTab.notifySubscribers(options, "save");
    }
}

class ConsignmentIndexSearchIndex {
    ConsignmentReference: KnockoutObservable<string>;
    SupplierDepartmentID: KnockoutObservable<string>;
    validationModel: KnockoutObservable<any>;
    RecordsInDays: KnockoutObservable<string>;
    SupplierDepartmentNameOrConsignmentReference: KnockoutObservable<string>;

    constructor() {
        this.ConsignmentReference = ko.observable("").extend({ required: true });
        this.ConsignmentReference.isModified(false);
        this.SupplierDepartmentID = ko.observable("");
        this.SupplierDepartmentNameOrConsignmentReference = ko.observable("");
        this.RecordsInDays = ko.observable("LASTMONTH");
        this.validationModel = ko.validatedObservable({
            ConsignmentReference: this.ConsignmentReference
        });
    }
}

//class CompletedConsignmentPagingModel {
//    Paging: KnockoutObservable<Paging>;
//    CompletedConsignmentSearch: KnockoutObservable<CompletedConsignmentSearch>;
//    SearchClicked: KnockoutObservable<boolean>;
//    IsSearchValid: KnockoutComputed<boolean>;
//    SubscriberTab: any;

//    constructor(data, subscriberTab) {
//        this.Paging = ko.observable(new Paging(data.ConsignmentEditModels, data.SearchObject));
//        this.CompletedConsignmentSearch = ko.observable(new CompletedConsignmentSearch(data.SearchObject.FromDateStr));
//        this.SearchClicked = ko.observable(false);
//        this.IsSearchValid = ko.computed({
//            read: () => {
//                return (this.SearchClicked());
//                //return (this.SearchClicked() && !this.CompletedConsignmentSearch().validationModel.isValid());
//            }
//        });
//        this.SubscriberTab = subscriberTab;
//    }

//    Search = function () {
//        this.SearchClicked(true);
//        //if (this.CompletedConsignmentSearch().validationModel.isValid()) {
//        this.Paging().fetchEntitiesWithSearch("/Consignment/IndexSimplified", this.CompletedConsignmentSearch);
//        //}
//    }

//    SearchBySupplierDepartmentId = function () {
//        this.SearchClicked(false);
//        this.CompletedConsignmentSearch().SupplierDepartmentNameOrConsignmentReference("");
//        this.Paging().fetchEntitiesWithSearch("Consignment/Index", this.CompletedConsignmentSearch);
//    }

//    Reset = function () {
//        this.SearchClicked(false);
//        // reset search box
//        this.CompletedConsignmentSearch().SupplierDepartmentNameOrConsignmentReference("");
//        // returns all entries
//        this.Paging().fetchEntitiesWithSearch("/Consignment/IndexSimplified", this.CompletedConsignmentSearch);
//    }

//    OpenConsignmentDetails = function (data) {
//        var options = {
//            TabTitle: "loading...",
//            PanelName: "ConsignmentDetails",
//            UriParam: data
//        };
//        this.SubscriberTab.notifySubscribers(options, "save");
//    }
//}

//class CompletedConsignmentSearch {
//    validationModel: KnockoutObservable<any>;
//    RecordsInDays: KnockoutObservable<string>;
//    SupplierDepartmentNameOrConsignmentReference: KnockoutObservable<string>;
//    CompletedConsignmentsOnly: KnockoutObservable<boolean>;
//    FromDate: KnockoutObservable<string>;
//    FromDateStr: KnockoutObservable<string>;
//    DateObservable: KnockoutComputed<any>;

//    constructor(data) {
//        this.SupplierDepartmentNameOrConsignmentReference = ko.observable("");//.extend({ required: true });
//        //this.SupplierDepartmentNameOrConsignmentReference.isModified(false);
//        this.RecordsInDays = ko.observable("LASTWEEK");
//        this.CompletedConsignmentsOnly = ko.observable(true);
//        this.FromDate = ko.observable(data);
//        this.FromDateStr = ko.observable(data);
        
//        //this.validationModel = ko.validatedObservable({
//        //    SupplierDepartmentNameOrConsignmentReference: this.SupplierDepartmentNameOrConsignmentReference
//        //});


//    }
//}

class ConsignmentIndexSearch {
    ConsignmentReference: KnockoutObservable<string>;
    SupplierDepartmentID: KnockoutObservable<string>;
    validationModel: KnockoutObservable<any>;
    RecordsInDays: KnockoutObservable<string>;
    SupplierDepartmentNameOrConsignmentReference: KnockoutObservable<string>;

    constructor() {
        this.ConsignmentReference = ko.observable("").extend({ required: true });
        this.ConsignmentReference.isModified(false);
        this.SupplierDepartmentID = ko.observable("");
        this.SupplierDepartmentNameOrConsignmentReference = ko.observable("");
        this.RecordsInDays = ko.observable("LASTMONTH");
        this.validationModel = ko.validatedObservable({
            ConsignmentReference: this.ConsignmentReference
        });
    }
}




//class dummySearchObj {



//} 

//class dummyResultList {
//    QueryOptions: KnockoutObservable<dummyQueryOptions>;
//    Results: any;

//    constructor() {
//         this.QueryOptions = ko.observable(new dummyQueryOptions());
//    }
//}

//class dummyQueryOptions {
//    CurrentPage: number = 1;
//    TotalPages: number;
//    PageSize: number = 15;
//    SortField: string = "ID";
//    SortOrder: string;

//    constructor() { }
//}

