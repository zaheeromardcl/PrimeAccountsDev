/// <reference path="ConsignmentItemViewModel.ts" />
/// <reference path="../../Scripts/typings/knockout/knockout.d.ts" />
interface OrderFileEditModel {
    fileInput: KnockoutObservable<string>;
    ConsignmentFileID: KnockoutObservable<string>;
    FileID: KnockoutObservable<string>;
    FileName: KnockoutObservable<string>;
    ContentType: KnockoutObservable<string>;
    Consignment: KnockoutObservable<string>;
}

//interface KnockoutExtenders {
//    scrollFollow: (target: any, selector: string);
//}


class OrderViewModel {
    ConsignmentReference: KnockoutObservable<string>;
    ConsignmentID: KnockoutObservable<string>;
    ConsignmentDescription: KnockoutObservable<string>;
    ConsignmentFileEditModels: KnockoutObservableArray<any>;
    FileEditModels: KnockoutObservableArray<any>;
    DespatchDate: KnockoutObservable<Date>;
    ContractDate: KnockoutObservable<Date>;
    ReceivedDate: KnockoutObservable<Date>;
    Handling: KnockoutObservable<number>;
    Commission: KnockoutObservable<number>;
    ShowVehicleOnInvoice: KnockoutObservable<boolean>;
    Vehicle: KnockoutObservable<string>;
    VehicleDetail: KnockoutObservable<string>;
    
    IsSaved: KnockoutObservable<boolean>;
    DepartmentID: KnockoutObservable<string>;
    DepartmentName: KnockoutObservable<string>;
    SupplierReference: KnockoutObservable<string>;
    SupplierID: KnockoutObservable<string>;
    SupplierDepartmentName: KnockoutObservable<string>;
    SupplierCompanyName: KnockoutObservable<string>;
    SupplierDepartmentID: KnockoutObservable<string>;
    PortID: KnockoutObservable<string>;
    PortName: KnockoutObservable<string>;
    DisplayPort: KnockoutObservable<string>;
    DisplayDespatchLocation: KnockoutObservable<string>;
    PurchaseTypeID: KnockoutObservable<string>;
    PurchaseTypeDescription: KnockoutObservable<string>;
    CountryID: KnockoutObservable<string>;
    CountryName: KnockoutObservable<string>;
    DespatchID: KnockoutObservable<string>;
    DespatchName: KnockoutObservable<string>;
    NoteID: KnockoutObservable<string>;
    NoteText: KnockoutObservable<string>;
    SelectPurchaseType: KnockoutObservable<string>;
    SelectSupplierName: KnockoutObservable<string>;
    FileName: KnockoutObservable<string>;
    fileInput: KnockoutObservable<string>;
    MultipleConsignmentItems: KnockoutObservable<boolean>;
    DefaultDepartmentID: KnockoutObservable<string>;
    DefaultDepartmentName: KnockoutObservable<string>;   
    ConsignmentItems: KnockoutObservableArray<ConsignmentItemVM>;
   
    constructor(data, consignmentItems: ConsignmentItemVM[]) {
        data = data || {};
        this.ConsignmentReference = ko.observable(data.ConsignmentReference || "");
        this.ConsignmentDescription = ko.observable(data.ConsignmentReference || data.ConsignmentReference).extend({ required: true });
        this.ConsignmentID = ko.observable(data.ConsignmentID || "");
        this.ConsignmentReference = ko.observable(data.ConsignmentReference || "");
        //Date conversions
        var d = new Date();
        if (data.ContractDate != null) {
            var cD = data.ContractDate.substring(0, 10);
        }
        if (data.ReceivedDate != null) {
            var rD = data.ReceivedDate.substring(0, 10);
        }
        var x = 1;
        d.setDate(d.getDate() - 1);
        var d2 = function (n) { return n > 9 ? n : "0" + n; };
        var date = d2(d.getDate()) + "/" + d2(d.getMonth() + 1) + "/" + d.getFullYear();

        this.ContractDate = ko.observable(data.ContractDate || cD);
        this.CountryID = ko.observable(data.CountryID || "");
        this.CountryName = ko.observable(data.CountryName || "");
        this.DepartmentID = ko.observable(data.DepartmentID || "");
        this.DepartmentName = ko.observable(data.DepartmentName || "");
        this.DespatchDate = ko.observable(data.DespatchDate || new Date()).extend({ required: true });
        this.DespatchID = ko.observable(data.DespatchID || "");
        this.DespatchName = ko.observable(data.DespatchName || "");
        this.DisplayDespatchLocation = ko.observable(data.DisplayDespatchLocation || "");
        this.DisplayPort = ko.observable(data.DisplayPort || "");
        this.Handling = ko.observable(data.Handling || "0.00");
      
        this.IsSaved = ko.observable(data.IsSaved || "false");
        this.NoteID = ko.observable(data.NoteID || "");
        this.NoteText = ko.observable(data.NoteText || "");
        this.PortID = ko.observable(data.PortID || "");
        this.PortName = ko.observable(data.PortName || "");

        if (data.PortName != null) {
            var portN = "";
            var portNS = "";
            portN = data.PortName.indexOf("-", 0);
            portNS = data.PortName.substr(portN + 1, data.PortName.length);           
            this.PortName(portNS);
        }

        this.PurchaseTypeDescription = ko.observable(data.PurchaseTypeDescription || "");
        this.PurchaseTypeID = ko.observable(data.PurchaseTypeID || "");
        this.ReceivedDate = ko.observable(data.ReceivedDate || new Date()).extend({ required: true });
        this.SelectPurchaseType = ko.observable(data.SelectPurchaseType || "");
        this.ShowVehicleOnInvoice = ko.observable(data.ShowVehicleOnInvoice || "");
        this.SupplierCompanyName = ko.observable(data.SupplierCompanyName || "");
        this.SupplierDepartmentID = ko.observable(data.SupplierDepartmentID || "");
        this.SupplierDepartmentName = ko.observable(data.SupplierDepartmentName || "");
        this.SupplierID = ko.observable(data.SupplierID || "");
        this.SupplierReference = ko.observable(data.SupplierReference || "").extend({ required: true });
        this.Vehicle = ko.observable(data.Vehicle || "default");
        this.VehicleDetail = ko.observable(data.VehicleDetail || "default");
        this.Commission = ko.observable(data.Commission || "0.00");       
        this.ConsignmentItems = ko.observableArray([]);
        this.MultipleConsignmentItems = ko.observable(data.MultipleConsignmentItems || false);
        this.DefaultDepartmentID = ko.observable(data.DefaultDepartmentID || "");
        this.DefaultDepartmentName = ko.observable(data.DefaultDepartmentName || "");
        

        //ko.extenders.scrollFollow = function (target, selector) {
        //    target.subscribe(function (newval) {
        //        var el = document.querySelector(selector);

        //        // the scroll bar is all the way down, so we know they want to follow the text
        //        if (el.scrollTop == el.scrollHeight - el.clientHeight) {
        //            // have to push our code outside of this thread since the text hasn't updated yet
        //            setTimeout(function () { el.scrollTop = el.scrollHeight - el.clientHeight; }, 0);
        //        }
        //    });

        //    return target;
        //};       
       
        if (consignmentItems.length > 0) {
            for (var consignmentItem of consignmentItems) {
                if (consignmentItem != undefined) {
                    var test_undefined = consignmentItem.ConsignmentID();
                    if (test_undefined != "00000000-0000-0000-0000-000000000000") {
                        this.ConsignmentItems.push(consignmentItem);
                    }
                }
            }
        }
    }
} 