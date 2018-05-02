/// <reference path="../Scripts/typings/jquery/jquery.d.ts" />
/// <reference path="../Scripts/typings/knockout/knockout.d.ts" />
class OrderModel {
    // parent containing OrderEditModel and list of orderitemeditmodels;
    OrderItems: KnockoutObservableArray<OrderItemEditModel> = ko.observableArray([]);
    OrderEditModel: KnockoutObservable<OrderEditModel>;

    constructor(data) {

        data = data || {};
        this.OrderEditModel = ko.observable(data.OrderEditModel);
        this.OrderItems = ko.observableArray([]);
    }

    //get supplier
    getSupplier = function (request, response) {
        var text = request.term;
        if (text === " ")
            return;
        if (text === "")
            return;
        $.ajax({
            type: 'GET',
            url: '/api/Supplier/AutoComplete/?search=' + text,
            data: {
                json: '{}',
                delay: 0.5,
                search: text

            },
            success: function (data) {

                if (data == null)
                    return;
                response($.map(data, function (language) {
                    return {
                     //   languageValue: language.value, // ID is required for the GUID
                        languageValue: language.Id,
                        label: language.label
                    };
                }));
            }//,
            //  messages: {
            // noResults: '',
            // results: function () { }
            //}
        });
        //CreateOrderForm

        }

    createOrder = function () {

        var fCon = "";
            

        //Encoding in BASE64 does not work in Chrome - we need another way to do this
        //for (var i = 0; i < self.Consignment.FileEditModels._latestValue.length; i++) {

            //fCon = self.Consignment.FileEditModels._latestValue[i].FileContent;

           // self.Consignment.FileEditModels._latestValue[i].FileContent = utf8_to_b64(fCon);

       // }

        var PostConsignment = this.OrderEditModel();
        var parmConsignmentID = this.OrderEditModel().ConsignmentID;

        var errors = ko.validation.group(PostConsignment, { deep: true, observable: false });
        if (errors().length > 0) {
            errors.showAllMessages();
            return;

        } else {


           // if (PostConsignment().ConsignmentDescription() != "" && PostConsignment().DespatchDate != "" && PostConsignment().ConsignmentReference() != "" && PostConsignment().VehicleDetail() != "") {
                if ( PostConsignment.ConsignmentReference != "" ) {
            
                $.ajax({
                    url: "/Consignment/CreateConsignment/",
                    cache: false,
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    data: ko.toJSON(PostConsignment),
                    dataType: "json",
                    //data: ko.toJSON(PostConsignment),
                    success: function (PostConsignment) {
                        //error in line below
                        window.location.href = "/Consignment/ConsignmentItem/" + parmConsignmentID;
                    }
                }).fail(
                    function (xhr, textStatus, err) {
                       
                    });

            } else {

                //alert('Please Enter All the Values !!');
            }

        }
    };

    fnpostOrderForm(Formdata) {
    
        this.OrderItems.push(new OrderItemEditModel(Formdata.OrderItems, 1));
        this.createOrder();

    }

    //select supplier
    selectSupplier = function (event, ui) {
        (ui.item.languageValue);     
        var valueArray = ui.item.languageValue.split(",");
        
        // Set value in ViewModel
        var vm = ko.dataFor(event.target);
        vm.OrderEditModel().SupplierDepartmentID = valueArray[0];
       }

    getProduce = function (request, response) {
        var text = request.term;
        if (text === " ")
            return;
        if (text === "")
            return;
        $.ajax({
            type: 'GET',
            url: '/api/Produce/AutoComplete/?search=' + text,
            data: {
                json: '{}',
                delay: 0.5,
                search: text

            },
            success: function (data) {
                if (data == null)
                    return;
                response($.map(data, function (language) {
                    return {
                        languageValue: language.value,
                        label: language.label
                    };
                }));
            }//,
            //  messages: {
            // noResults: '',
            // results: function () { }
            //}
        });



    };

    selectProduce = function (event, Produceui) {
        (Produceui.item.languageValue);
        

        var valueArray;

        valueArray = Produceui.item.languageValue;
        var labelArray;
        labelArray = Produceui.item.label;

        
      
    }
    getDepartment = function (request, Departmentui) {

        var text = request.term;
        //if (text === " ")
        //    return;
        //if (text === "")
        //    return;
        $.ajax({
            type: 'GET',
            url: '/API/Department/AutoComplete/?search=' + text,
            data: {
                json: '{}',
                delay: 0.5,
                search: text

            },
            success: function (data) {
                if (data == null)
                    return;
                Departmentui($.map(data, function (language) {
                    return {
                        languageValue: language.Id,
                        label: language.label
                    };
                }));
            }//,
  
        });
    };

    selectDepartment = function (event, Departmentui) {
        (Departmentui.item.languageValue);


        var DepartmentvalueArray;

        DepartmentvalueArray = Departmentui.item.languageValue;
        var DepartmentlabelArray;
        DepartmentlabelArray = Departmentui.item.label;



    }

}




class OrderEditModel {

    ConsignmentID: KnockoutObservable<number>;
    ConsignmentDescription: KnockoutObservable<string>;
    DespatchDate: KnockoutObservable<string>;
    ContractDate: KnockoutObservable<string>;
    //ConsignmentReference: KnockoutObservable<number>;
    ConsignmentReference: KnockoutObservable<string>;
    FileName: KnockoutObservable<string>;
    FileID: KnockoutObservable<number>;
    FileContent: KnockoutObservable<string>;
    Handling: KnockoutObservable<number>;
    Commission: KnockoutObservable<number>;
    ShowVehicleOnInvoice: KnockoutObservable<boolean>;
    Vehicle: KnockoutObservable<number>;
    VehicleDetail: KnockoutObservable<string>;
    DepartmentID: KnockoutObservable<number>;
    DepartmentName: KnockoutObservable<string>;
    SupplierReference: KnockoutObservable<string>;
    SupplierID: KnockoutObservable<number>;
    SupplierName: KnockoutObservable<string>;
    PortID: KnockoutObservable<number>;
    PortName: KnockoutObservable<string>;
    PurchaseTypeID: KnockoutObservable<number>;
    PurchaseTypeName: KnockoutObservable<string>;
    ReceivedDate: KnockoutObservable<number>;
    CountryID: KnockoutObservable<number>;
    CountryName: KnockoutObservable<string>;
    DespatchLocationID: KnockoutObservable<number>;
    DespatchLocationName: KnockoutObservable<string>;
    UpdatedBy: KnockoutObservable<number>;
    UpdatedDate: KnockoutObservable<string>;
    CreatedBy: KnockoutObservable<number>;
    CreatedDate: KnockoutObservable<string>;
    IsActive: KnockoutObservable<boolean>;
    SupplierDepartmentID: KnockoutObservable<number>; // DC

    constructor(data) {
        data = data || {};
        this.ConsignmentID = ko.observable(data.ConsignmentID);
        this.ConsignmentDescription = ko.observable(data.ConsignmentDescription);
        this.DespatchDate = ko.observable(data.DespatchDate);
        this.ContractDate = ko.observable(data.ContractDate);
        this.ConsignmentReference = ko.observable(data.ConsignmentReference);
        this.FileName = ko.observable(data.FileName);
        this.FileID = ko.observable(data.FileID);
        this.FileContent = ko.observable(data.FileContent);
        this.Handling = ko.observable(data.Handling);
        this.Commission = ko.observable(data.Commission);
        this.ShowVehicleOnInvoice = ko.observable(data.ShowVehicleOnInvoice || "");
        this.Vehicle = ko.observable(data.Vehicle || "default");
        this.VehicleDetail = ko.observable(data.VehicleDetail || "default");
        this.DepartmentID = ko.observable(data.DepartmentID);
        this.DepartmentName = ko.observable(data.DepartmentName);
        this.SupplierReference = ko.observable(data.SupplierReference);
        this.SupplierID = ko.observable(data.SupplierID);
        this.SupplierName = ko.observable(data.SupplierName);
        this.PortID = ko.observable(data.PortID);
        this.PortName = ko.observable(data.PortName);
        this.PurchaseTypeID = ko.observable(data.PurchaseTypeID);
        this.PurchaseTypeName = ko.observable(data.PurchaseTypeName);
        this.ReceivedDate = ko.observable(data.ReceivedDate);
        this.CountryID = ko.observable(data.CountryID);
        this.CountryName = ko.observable(data.CountryName);
        this.DespatchLocationID = ko.observable(data.DespatchLocationID);
        this.DespatchLocationName = ko.observable(data.DespatchLocationName);
        this.UpdatedBy = ko.observable(data.UpdatedBy);
        this.UpdatedDate = ko.observable(data.UpdatedDate);
        this.CreatedBy = ko.observable(data.CreatedBy);
        this.CreatedDate = ko.observable(data.CreatedDate);
        this.IsActive = ko.observable(data.IsActive);
        this.SupplierDepartmentID = ko.observable(data.SupplierDepartmentID);
    }

}

class OrderItemEditModel {//extends Model {
    ConsignmentID: KnockoutObservable<number>;
    ConsignmentItemID: KnockoutObservable<number>;
    BestBeforeDate: KnockoutObservable<string>;
    Brand: KnockoutObservable<string>;
    Department: KnockoutObservable<string>;
    Rotation: KnockoutObservable<string>;
    PackType: KnockoutObservable<string>;
    PackWtUnitID: KnockoutObservable<number>;
    WtUnit: KnockoutObservable<string>;
    PorterageID: KnockoutObservable<number>;
    PorterageCode: KnockoutObservable<string>;
    PackWeight: KnockoutObservable<number>;
    PackWtUnit: KnockoutObservable<number>;

    PackSize: KnockoutObservable<string>;
    PackPall: KnockoutObservable<string>;
    ExpectedQuantity: KnockoutObservable<number>;
    ReceivedQuantity: KnockoutObservable<number>;
    EstimatedPercentageProfit: KnockoutObservable<number>;
    EstimatedChargeCostPerPack: KnockoutObservable<number>;
    Returns: KnockoutObservable<number>;
    EstimatedPurchaseCostPerPack: KnockoutObservable<number>;
    IsActive: KnockoutObservable<boolean>;
    ProduceID: KnockoutObservable<number>;
    ProduceName: KnockoutObservable<string>;
    NoteID: KnockoutObservable<number>;
    NoteText: KnockoutObservable<string>;

    constructor(data, id) {
       
        data = data || {};
        this.ConsignmentID = ko.observable(data.ConsignmentID);
        this.ConsignmentItemID = ko.observable(data.ConsignmentItemID);
        this.BestBeforeDate = ko.observable(data.BestBeforeDate);
        this.Brand = ko.observable(data.Brand);
        this.Department = ko.observable(data.Department);
        this.Rotation = ko.observable(data.Rotation);
        this.PackType = ko.observable(data.PackType);
        this.PackWtUnitID = ko.observable(data.PackWtUnitID);
        this.WtUnit = ko.observable(data.WtUnit);
        this.PorterageID = ko.observable(data.PorterageID);
        this.PorterageCode = ko.observable(data.PorterageCode);
        this.PackWeight = ko.observable(data.PackWeight);
        this.PackWtUnit = ko.observable(data.PackWtUnit);
        this.PackSize = ko.observable(data.PackSize);
        this.PackPall = ko.observable(data.PackPall);
        this.ExpectedQuantity = ko.observable(data.ExpectedQuantity);
        this.ReceivedQuantity = ko.observable(data.ReceivedQuantity);
        this.EstimatedPercentageProfit = ko.observable(data.EstimatedPercentageProfit);
        this.EstimatedChargeCostPerPack = ko.observable(data.EstimatedChargeCostPerPack);
        this.Returns = ko.observable(data.Returns);
        this.EstimatedPurchaseCostPerPack = ko.observable(data.EstimatedPurchaseCostPerPack);
        this.IsActive = ko.observable(data.IsActive);
        this.ProduceID = ko.observable(data.ProduceID);
        this.ProduceName = ko.observable(data.ProduceName);
        this.NoteID = ko.observable(data.NoteID);
        this.NoteText = ko.observable(data.NoteText);
   
    }
   
}


  





