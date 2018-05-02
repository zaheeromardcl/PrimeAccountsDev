
class InvoiceModel {
 
    InvoiceItemEditModels: KnockoutObservableArray<InvoiceItemEditModel> = ko.observableArray([]);
    InvoiceEditModel: KnockoutObservable<InvoiceEditModel>;
    TempInvoiceID: KnockoutObservable<string>;
    SelectedInvoiceItem: KnockoutObservable<number>;
    Id: KnockoutObservable<number>;
    SubTotal: KnockoutObservable<number>;
    VATAmount: KnockoutObservable<number>;
    Subtotal: KnockoutComputed<number>;
    VAT: KnockoutComputed<number>;
    Total: KnockoutComputed<number>;
    Logo: KnockoutObservable<string>;

    constructor(data) {

        data = data || {};
        this.InvoiceEditModel = ko.observable(data);
        this.TempInvoiceID = ko.observable(data.TempInvoiceID);
        this.SelectedInvoiceItem = ko.observable(data.Id);
        this.Id = ko.observable(data.Id);
        this.InvoiceItemEditModels = ko.observableArray([]);
        this.VATAmount = ko.observable(data.VATAmount);
        this.Logo = ko.observable(data.Logo);
        for (var i = 0; i < data.InvoiceItemEditModels.length; i++)
        {
            this.InvoiceItemEditModels.push(data.InvoiceItemEditModels[i]);
        }     
        this.Subtotal = ko.computed({

            read: () => {

                var temp = 0;

                for (var i = 0; i < data.InvoiceItemEditModels.length; i++) {

                    (Number(temp = Number(temp) + Number(data.InvoiceItemEditModels[i].TicketItemTotalPrice)))
                }  

                return (Number(temp))
            }
        });
        this.VAT = ko.computed({

            read: () => {

                return (Number(this.Subtotal() * 0.2))
            }
        });
        this.Total = ko.computed({

            read: () => {

                return (Number(Number(this.Subtotal()) + Number(this.VATAmount())))
            }
        });
        
    }
    fnSelectedItem(ItemModel) {
        
        this.SelectedInvoiceItem.prototype = ItemModel;
        
    }
    fnpostInvoiceItem(Formdata, id) {

        var InvoiceRowId = id();
        if (InvoiceRowId >> 0) {
            InvoiceRowId = (Number(InvoiceRowId) - 1);
        }
        else {
            id = 0;
        }

        if (Formdata != null) {
            var myInvoiceItem = Formdata.InvoiceItems()[InvoiceRowId];

            myInvoiceItem.SalesInvoiceID = InvoiceEditModel.prototype.SalesInvoiceID;
         
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                url: '/api/Invoice/CreateInvoiceItem/',
                data: ko.toJSON(myInvoiceItem),
                success: function (myInvoiceItem) {
                    

                }
            });

        }

        return Formdata;
    }
    fnpostInvoiceForm(Formdata) {
        var parmReturnedInvoiceID;
        var parmInvoiceID;

        if (Formdata != null) {

           
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                url: '/api/Invoice/CreateInvoice/',
                data: ko.toJSON(Formdata),
                dataType: "json",
                success: function (Formdata) {
                   
                    parmReturnedInvoiceID = Formdata.TempInvoiceID;
                    InvoiceEditModel.prototype.SalesInvoiceID = parmReturnedInvoiceID;
                    
                  
                }
            });
            this.Id.prototype = 1;
            this.InvoiceItemEditModels.push(new InvoiceItemEditModel(Formdata.InvoiceItemsAruna, this.Id.prototype));

        }
    }
    fnAddNewRow() {

        this.Id.prototype = this.Id.prototype + 1;
        this.InvoiceItemEditModels.push(new InvoiceItemEditModel(null, this.Id.prototype));
    }
    fnRemoveRow(InvoiceItemRowData) {

        if (this.InvoiceItemEditModels().length > 1) {
            this.InvoiceItemEditModels.splice(InvoiceItemRowData - 1, 1);
        }
    }

    //on press of Save Invoice
    fnpostInvoiceItemsForm(Formdata) {


        if (Formdata != null) {
            //Formdata.TempInvoiceID = InvoiceEditModel.prototype.InvoiceID;
            Formdata.InvoiceEditModel().SalesInvoiceID = InvoiceEditModel.prototype.SalesInvoiceID;
            Formdata.InvoiceEditModel().IsSaved = true;
            //setting the values of the selected Consignment and Department.
         
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                url: '/api/Invoice/UpdateInvoice/',
                data: ko.toJSON(Formdata.InvoiceEditModel()),
                success: function (Formdata) {
                   
                    window.location.href = "/Invoice/Details/?Id=" + InvoiceEditModel.prototype.SalesInvoiceID;

                }
            });

        }
    }

 
    fnCalcInvoiceSubTotal = function (InvoiceItemsCollection) {

        var RunningTotal = 0;
        for (var i = 0; i < InvoiceItemsCollection._latestValue.length; i++) {
            RunningTotal = RunningTotal + Number(InvoiceItemsCollection._latestValue[i].InvoiceItemTotalPrice());
        }
        return RunningTotal;

    }


}
class InvoiceEditModel {

    SalesInvoiceID: KnockoutObservable<number>;
    CustomerDepartmentID: KnockoutObservable<number>;
    CustomerDepartmentAddressID: KnockoutObservable<number>;
    SalesLedgerEntryID: KnockoutObservable<number>;
    DivisionAddressID: KnockoutObservable<number>;
    CurrencyID: KnockoutObservable<number>;
    NoteID: KnockoutObservable<number>;
    SalesInvoiceReference: KnockoutObservable<string>;
    CustomerDepartment: KnockoutObservable<string>;
    NoteText: KnockoutObservable<string>;
    CustomerDepartmentAddress1: KnockoutObservable<string>;
    CustomerDepartmentAddress2: KnockoutObservable<string>;
    CustomerDepartmentAddress3: KnockoutObservable<string>;
    ServerCode: KnockoutObservable<string>;
    SalesInvoiceDate: KnockoutObservable<string>;
    DivisionAddress1: KnockoutObservable<string>;
    DivisionAddress2: KnockoutObservable<string>;
    DivisionAddress3: KnockoutObservable<string>;
    Currency: KnockoutObservable<string>;
    ExchangeRate: KnockoutObservable<string>;
    IsCashSale: KnockoutObservable<boolean>;
    UpdatedBy: KnockoutObservable<string>;
    UpdatedDate: KnockoutObservable<string>;
    CreatedBy: KnockoutObservable<string>;
    CreatedDate: KnockoutObservable<string>;
    IsActive: KnockoutObservable<boolean>;
    IsSaved: KnockoutObservable<boolean>;
    VATReg: KnockoutObservable<string>;
    VATAmount: KnockoutObservable<number>;
    CompanyName: KnockoutObservable<number>;
            
    constructor(data) {

        data = data || {};
              
        this.SalesInvoiceID = ko.observable(data.InvoiceID).extend({ required: true });
        this.SalesInvoiceReference = ko.observable(data.SalesInvoiceReference);
        this.CustomerDepartmentID = ko.observable(data.CustomerDepartmentID);
        this.CustomerDepartment = ko.observable(data.CustomerDepartment);
        this.CustomerDepartmentAddressID = ko.observable(data.CustomerDepartmentAddressID);
        this.SalesLedgerEntryID = ko.observable(data.SalesLedgerEntryID);
        this.DivisionAddressID = ko.observable(data.DivisionAddressID);
        this.DivisionAddress1 = ko.observable(data.DivisionAddress1);
        this.DivisionAddress2 = ko.observable(data.DivisionAddress2);
        this.DivisionAddress3 = ko.observable(data.DivisionAddress3);
        this.CustomerDepartmentAddress1 = ko.observable(data.CustomerDepartmentAddress1);
        this.CustomerDepartmentAddress2 = ko.observable(data.CustomerDepartmentAddress2);
        this.CustomerDepartmentAddress3 = ko.observable(data.CustomerDepartmentAddress);
        this.NoteID = ko.observable(data.NoteID);
        this.NoteText = ko.observable(data.NoteText);
        this.SalesInvoiceDate = ko.observable(data.SalesInvoiceDate);
        this.CurrencyID = ko.observable(data.CurrencyID);
        this.Currency = ko.observable(data.Currency);
        this.ExchangeRate = ko.observable(data.ExchangeRate);
        this.ServerCode = ko.observable(data.ServerCode);
        this.IsCashSale = ko.observable(data.IsCashSale);
        this.UpdatedBy = ko.observable(data.UpdatedBy);
        this.UpdatedDate = ko.observable(data.UpdatedDate);
        this.CreatedBy = ko.observable(data.CreatedBy);
        this.CreatedDate = ko.observable(data.CreatedDate);
        this.IsActive = ko.observable(data.IsActive);
        this.IsSaved = ko.observable(data.IsSaved);
        this.VATReg = ko.observable(data.VATReg);
        this.VATAmount = ko.observable(data.VATAmount);
        this.CompanyName = ko.observable(data.CompanyName);
        
    }
        
};


class InvoiceItemEditModel {

    Editing: KnockoutObservable<boolean>;
    Id: KnockoutObservable<number>;
    SalesInvoiceID: KnockoutObservable<number>;
    SalesInvoiceItemID: KnockoutObservable<number>;
    SalesInvoiceItemDescription: KnockoutObservable<string>;
    TicketItemID: KnockoutObservable<number>;
    CurrencyID: KnockoutObservable<number>;
    Currency: KnockoutObservable<string>;
    ExchangeRate: KnockoutObservable<number>;
    SalesInvoiceItemVAT: KnockoutObservable<number>;
    SalesInvoiceItemLineTotal: KnockoutObservable<number>;
    UpdatedBy: KnockoutObservable<string>;
    UpdatedDate: KnockoutObservable<string>;
    CreatedBy: KnockoutObservable<string>;
    CreatedDate: KnockoutObservable<string>;
    IsActive: KnockoutObservable<boolean>;
    errors: KnockoutObservable<string>;
    showerror: KnockoutObservable<string>;
    postInvoiceItemData: KnockoutObservable<string>;
    porterageValue: KnockoutObservable<number>;
    TicketNumber: KnockoutObservable<string>;
    TicketItemQty: KnockoutObservable<number>;
    TicketItemUnitPrice: KnockoutComputed<number>;
    TicketItemTotalPrice: KnockoutObservable<number>;
   
constructor(data, id) {

        data = data || {};
        this.Id = ko.observable(id);
        this.SalesInvoiceID = ko.observable(data.SalesInvoiceID);
        this.SalesInvoiceItemID = ko.observable(data.SalesInvoiceItemID);
        this.SalesInvoiceItemDescription = ko.observable(data.SalesInvoiceItemDescription);
        this.TicketItemID = ko.observable(data.TicketItemID);
        this.CurrencyID = ko.observable(data.CurrencyID);
        this.Currency = ko.observable(data.Currency);
        this.ExchangeRate = ko.observable(data.ExchangeRate);
        this.SalesInvoiceItemVAT = ko.observable(data.SalesInvoiceItemVAT);
        this.SalesInvoiceItemLineTotal = ko.observable(data.SalesInvoiceItemLineTotal);
        this.UpdatedBy = ko.observable(data.UpdatedBy);
        this.UpdatedDate = ko.observable(data.UpdatedDate);
        this.CreatedBy = ko.observable(data.CreatedBy);
        this.CreatedDate = ko.observable(data.CreatedDate);
        this.IsActive = ko.observable(data.IsActive);
        this.errors = ko.observable(data.errors);
        this.showerror = ko.observable(data.showerror);
        this.Editing = ko.observable(false);
        this.porterageValue = ko.observable(data.porterageValue);
        this.TicketNumber = ko.observable(data.TicketNumber);
        this.TicketItemQty = ko.observable(data.TicketItemQty);
        this.TicketItemTotalPrice = ko.observable(data.TicketItemTotalPrice);
        this.TicketItemUnitPrice = ko.computed({
            read: () => {
  
                return (Number(this.TicketItemTotalPrice() / this.TicketItemQty()))
            }
        });

    }
}


  





