interface ITicketItemModel {
    TicketItemTotalPrice(): number;
    TicketItemPorterageValue(): number;
}

class TicketViewModel {
    TicketID: KnockoutObservable<string>;
    PONumber: KnockoutObservable<string>;
    TicketReference: KnockoutObservable<string>;
    CustomerCompanyName: KnockoutObservable<string>;
    CustomerDepartmentID: KnockoutObservable<string>;
    CustomerDepartmentName: KnockoutObservable<string>;
    NoteID: KnockoutObservable<string>;
    Notes: KnockoutObservable<string>;
    TicketDate: KnockoutObservable<string>;
    CurrencyID: KnockoutObservable<string>;
    CurrencyName: KnockoutObservable<string>;
    CurrencyRate: KnockoutObservable<number>;
    CustomerCurrencyID: KnockoutObservable<string>;
    SalesPersonUserID: KnockoutObservable<string>;
    SalesPersonName: KnockoutObservable<string>;
    SalesPersonDepartmentID: KnockoutObservable<string>;
    SalesPersonDepartmentName: KnockoutObservable<string>;
    SalesPersonDepartmentCode: KnockoutObservable<string>;
    IsCashSale: KnockoutObservable<boolean>;
    UpdatedBy: KnockoutObservable<string>;
    CreatedBy: KnockoutObservable<string>;
    CreatedUserName: KnockoutObservable<string>;
    UpdatedDate: KnockoutObservable<string>;
    CreatedDate: KnockoutObservable<string>;
   
    TicketSubTotal: KnockoutObservable<number>;
    TicketTotalPorterage: KnockoutObservable<number>;
    TicketVATTotal: KnockoutObservable<number>;
    TicketTotalPrice: KnockoutObservable<number>;
    //DivisionID

    TicketItems: KnockoutObservableArray<ITicketItemModel> = ko.observableArray([]);

    constructor(data, ticketItems: ITicketItemModel[]) {
        data = data || {};

        this.TicketID = ko.observable(data.TicketID);
        this.PONumber = ko.observable(data.PONumber);
        this.TicketReference = ko.observable(data.TicketReference);
        this.CustomerCompanyName = ko.observable(data.CustomerCompanyName);
        this.CustomerDepartmentID = ko.observable(data.CustomerDepartmentID);
        this.CustomerDepartmentName = ko.observable(data.CustomerDepartmentName);
        this.NoteID = ko.observable(data.NoteID);
        this.Notes = ko.observable(data.Notes);       
        this.TicketDate = ko.observable(data.TicketDate);
        this.CurrencyID = ko.observable(data.CurrencyID);
        this.CurrencyName = ko.observable(data.CurrencyName);
        this.CurrencyRate = ko.observable(data.CurrencyRate);
        this.CustomerCurrencyID = ko.observable(data.CustomerCurrencyID);
        this.SalesPersonUserID = ko.observable(data.SalesPersonUserID);
        this.SalesPersonName = ko.observable(data.SalesPersonName);
        this.SalesPersonDepartmentID = ko.observable(data.SalesPersonDepartmentID);
        this.SalesPersonDepartmentName = ko.observable(data.SalesPersonDepartmentName);
        this.SalesPersonDepartmentCode = ko.observable(data.SalesPersonDepartmentCode);
        this.IsCashSale = ko.observable(data.IsCashSale);
        this.CreatedDate = ko.observable(data.CreatedDate);        
        this.CreatedBy = ko.observable(data.CreatedBy);
        this.CreatedUserName = ko.observable(data.CreatedUserName);
        this.UpdatedDate = ko.observable(data.UpdatedDate);
        this.UpdatedBy = ko.observable(data.UpdatedBy);
        this.TicketSubTotal = ko.observable(data.TicketSubTotal);
        this.TicketTotalPorterage = ko.observable(data.TicketTotalPorterage);
        this.TicketVATTotal = ko.observable(data.TicketVATTotal);
        this.TicketTotalPrice = ko.observable(data.TicketTotalPrice);

        for (var ticketItem of ticketItems) {
            this.TicketItems.push(ticketItem);
        }
    }
}

class TicketItemViewModel implements ITicketItemModel {
    TicketItemID: KnockoutObservable<string>;
    TicketID: KnockoutObservable<string>;
    DepartmentID: KnockoutObservable<string>;
    TicketItemDescription: KnockoutObservable<string>;
    CurrencyAmount: KnockoutObservable<number>;
    TicketItemQuantity: KnockoutObservable<number>;
    DepartmentName: KnockoutObservable<string>;
    DepartmentCode: KnockoutObservable<string>;
    TicketItemBrand: KnockoutObservable<string>;
    TicketItemWeight: KnockoutObservable<string>;
    TicketItemPorterageID: KnockoutObservable<string>;
    TicketItemMinPorterage: KnockoutObservable<number>;
    TicketItemPorterageValue: KnockoutObservable<number>;
    TicketItemPorterage: KnockoutObservable<number>;
    TicketItemSize: KnockoutObservable<number>;
    TicketItemUnitPrice: KnockoutObservable<number>;
    TicketItemTotalPrice: KnockoutObservable<number>;
    ConsignmentItemID: KnockoutObservable<string>;
    ConsignmentReference: KnockoutObservable<string>;
    ProduceID: KnockoutObservable<string>;
    Produce: KnockoutObservable<string>;
    SupplierID: KnockoutObservable<string>;
    SupplierName: KnockoutObservable<string>;
    OriginalTicketItemID: KnockoutObservable<string>;

    TransferTypeID: KnockoutObservable<string>;
    TransferTypeName: KnockoutObservable<string>;

    UnitCost: KnockoutComputed<number>;

    constructor(data) {
        data = data || {};

        this.TicketItemID = ko.observable(data.TicketItemID);
        this.TicketID = ko.observable(data.TicketID);
        this.DepartmentID = ko.observable(data.DepartmentID);
        this.TicketItemDescription = ko.observable(data.TicketItemDescription);
        this.CurrencyAmount = ko.observable(data.CurrencyAmount);
        this.TicketItemQuantity = ko.observable(data.TicketItemQuantity);
        this.TicketItemBrand = ko.observable(data.TicketItemBrand);
        this.TicketItemWeight = ko.observable(data.TicketItemWeight);
        this.TicketItemPorterageID = ko.observable(data.TicketItemPorterageID);
        this.TicketItemMinPorterage = ko.observable(data.TicketItemMinPorterage);
        this.TicketItemPorterageValue = ko.observable(data.TicketItemPorterageValue);
        this.TicketItemPorterage = ko.observable(data.TicketItemPorterage);
        this.TicketItemSize = ko.observable(data.TicketItemSize);
        this.TicketItemUnitPrice = ko.observable(data.TicketItemUnitPrice);
        this.TicketItemTotalPrice = ko.observable(data.TicketItemTotalPrice);
        this.ConsignmentItemID = ko.observable(data.ConsignmentItemID);
        this.ConsignmentReference = ko.observable(data.ConsignmentReference);
        this.ProduceID = ko.observable(data.ProduceID);
        this.Produce = ko.observable(data.Produce);
        this.SupplierID = ko.observable(data.SupplierID);
        this.SupplierName = ko.observable(data.SupplierName);
        this.OriginalTicketItemID = ko.observable(data.OriginalTicketItemID);
        this.DepartmentCode = ko.observable(data.TicketItemDepartment);
        this.DepartmentName = ko.observable(data.DepartmentName);
        this.TransferTypeID = ko.observable(data.TransferTypeID);
        this.TransferTypeName = ko.observable(data.TransferTypeName);

        this.UnitCost = ko.computed({
            read: () => {
                var quantity = this.TicketItemQuantity();
                if (quantity > 0) {
                    return this.TicketItemTotalPrice() / this.TicketItemQuantity();
                }
                return this.TicketItemTotalPrice();
            }
        });
    }
}