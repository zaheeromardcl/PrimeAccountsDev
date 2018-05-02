class SBProduceDetails {

    producename: KnockoutObservable<string>;
    //ProduceGroupID: KnockoutObservable<string>;
    
    //QuantityReceived: KnockoutObservable<number>;
    //QuantitySold: KnockoutObservable<number>;
    QuantityExpected: KnockoutObservable<number>;
    //QuantityStock: KnockoutObservable<number>;
   QuantityAvailable: KnockoutObservable<number>;


    //consignments: KnockoutObservableArray<StockBoardConsignment>;
    //tickets: KnockoutObservableArray<StockBoardTicket>;

    constructor(data) {
        data = data || {};

        //this.ProduceGroupID = data.ProduceGroupID;
        this.producename = data.ProduceName;
        //this.QuantityReceived = data.QuantityReceived;
        //this.QuantitySold = data.QuantitySold;
        this.QuantityExpected = data.QuantityExpected;
        //this.QuantityStock = data.QuantityStock;
        this.QuantityAvailable= data.QuantityAvailable;

        //for (var x of data.RelatedConsignments) {
        //    this.consignments.push(data.RelatedConsignments[x]);
        //}
        //for (var y of data.RelatedTickets) {
        //    this.tickets.push(data.RelatedTickets[x]);
        //}

        
    }

    

}
class StockBoardTicket {
    TicketReference: KnockoutObservable<string>;
    TicketID: KnockoutObservable<string>;
    QtySold: KnockoutObservable<number>;

    constructor(data) {
        data = data || {};
        this.TicketReference = data.TicketReference;
        this.TicketID = data.TicketID;
        this.QtySold = data.QtySold;


    }
}

class StockBoardConsignment {
    ConsignmentReference: KnockoutObservable<string>;
    ConsignmentID: KnockoutObservable<string>;
    QtyReceived: KnockoutObservable<number>;

    constructor(data) {
        data = data || {};

        this.ConsignmentReference = data.ConsignmentReference;
        this.ConsignmentID = data.ConsignmentID;
        this.QtyReceived = data.QtyReceived;
       

    }
}

class StockBoardModel {
    ProduceCode: KnockoutObservable<string>;
    RemainingQty: KnockoutObservable<number>;
    ProduceGroupID: KnockoutObservable<string>;
    SupplierCompanyName: KnockoutObservable<string>;
    SupplierDepartmentName: KnockoutObservable<string>;
    ProduceGroupName: KnockoutObservable<string>;
    UserDivisionID: KnockoutObservable<string>;
    ProduceName: KnockoutObservable<string>;
    ConsignmentItemID: KnockoutObservable<string>;
    ConsignmentReference: KnockoutObservable<string>;
    TicketItemQuantity: KnockoutObservable<number>;
    RemainingQuantity: KnockoutObservable<number>;
    PorterageID: KnockoutObservable<string>;
    PorterageUnitAmount: KnockoutObservable<number>;
    PorterageMinimumAmount: KnockoutObservable<number>;
    ConsignmentItemDivisionID: KnockoutObservable<string>;
    ItemPackSize: KnockoutObservable<string>;
    ItemBrand: KnockoutObservable<string>;
    ItemPackWeight: KnockoutObservable<string>;
    ItemPackWeightUnit: KnockoutObservable<string>;
    ItemDepartmentID: KnockoutObservable<string>;
    ItemPackType: KnockoutObservable<string>;
    AvgUnitPrice: KnockoutObservable<number>;
    itemReceived: KnockoutObservable<boolean>;
    consDepartmentCode: KnockoutObservable<string>;
    Despatchdate: KnockoutObservable<string>;
    Invoiced: KnockoutObservable<boolean>;

  
 
    
    constructor(data) {
        data = data || {};
        this.ProduceCode = data.ProduceCode;
        this.ProduceName = data.ProduceName;
        this.RemainingQty = data.RemainingQty;
        this.ProduceGroupID = data.ProduceGroupID;
        this.SupplierCompanyName = data.SupplierCompanyName;
        this.SupplierDepartmentName = data.SupplierDepartmentName;
        this.ProduceGroupName = data.ProduceGroupName;
        this.UserDivisionID = data.UserDivisionID;
        this.ConsignmentItemID = data.ConsignmentItemID;
        this.ConsignmentReference = data.ConsignmentReference;
        this.TicketItemQuantity = data.TicketItemQuantity;
        this.PorterageID = data.PorterageID;
        this.PorterageUnitAmount = data.PorterageUnitAmount;

        this.PorterageMinimumAmount = data.PorterageMinimumAmount;
        this.ConsignmentItemDivisionID = data.ConsignmentItemDivisionID;
        this.ItemPackSize = data.ItemPackSize;
        this.ItemBrand = data.ItemBrand;
        this.RemainingQty = data.RemainingQty;
        this.ItemPackWeight = data.ItemPackWeight;
        this.ItemPackWeightUnit = data.ItemPackWeightUnit;
        this.ItemDepartmentID = data.ItemDepartmentID;
        this.ItemPackType = data.ItemPackType;
        this.AvgUnitPrice = data.AvgUnitPrice;
        this.itemReceived = data.itemReceived;
        this.consDepartmentCode = data.consDepartmentCode;
        this.Despatchdate = data.Despatchdate;
        this.Invoiced = data.Invoiced;
    
    }
        
  

 

}

class StockboardHub {
//a list of all produces for that stockboard.
    producestocks: KnockoutObservableArray<SBProduceDetails>;

   // stocks: KnockoutObservableArray<StockBoardModel>;
    //grouped produces - should be an obsarray of producedetails.


    //updateStocks: KnockoutObservableArray<StockBoardModel>;
    //sortedStocks: KnockoutObservableArray<StockBoardModel>;

    //updateStocks: KnockoutObservableArray<StockBoardModel>;
    
    constructor(data) {
        data = data || {}
       // this.stocks = ko.observableArray([]);
        this.producestocks = ko.observableArray([]);

        for (var x of data) {
            //populate rows of produce with remaining qty
           // this.stocks.push(new x(data.stocks));
            this.producestocks.push(new x(data.producestocks));
        }

        //function updateStocks(data) {
        //    var self = this;
        //    for (var i = 0; i < data.length; i++ ) {
        //        self.producestocks().push(data[i]);
        //    }

        //}
    }
}


////Top level stockboard view made up of an array of Produce Group Rows.
//class StockBoardModel {
//    ProduceGroupRows: KnockoutObservableArray<ProduceGroupModelRow> = ko.observableArray([]);
//    StockboardID: KnockoutObservable<string>;
//    constructor(data) {
//        data = data || {};
//        var rowItemCount = 4;
//        for (var i = 0; i < data.ProduceGroups.length; i += rowItemCount) {
//            this.ProduceGroupRows.push(new ProduceGroupModelRow(data.ProduceGroups, i, rowItemCount));
//        }

//    }


//    getProduces(stockboardID) {
//        //populate produces via API Call.

//        var self = this;
//        $.ajax({
//            type: 'GET',
//            url: '/api/StockBoard/GetProduces/?StockboardID=' + stockboardID,
//            data: {
//                json: '{}',
//                delay: 0.5,
//                search: stockboardID
//            },
//            success: function (data) {
//                for (var i = 0; i < data.length; i++) {
//                    self.ProduceGroupRows().push(data[i]);
//                }

//            }
//        });
//    };



//}

////ProduceGroupModel Row -  made up of any array of Produce Groups.
//class ProduceGroupModelRow {
//    ProduceGroups: KnockoutObservableArray<ProduceGroupModel> = ko.observableArray([]);

//    constructor(data, startIndex, rowItemCount) {
//        data = data || {};
//        var endIndex = startIndex + rowItemCount;
//        if (data.length < endIndex)
//            endIndex = data.length;
//        for (var i = startIndex; i < endIndex; i++) {
//            this.ProduceGroups.push(new ProduceGroupModel(data[i]));
//        }
//    }
//}
////ProduceGroupModel  -  made up of any array of Produce Groups
//class ProduceGroupModel {
//    ProduceGroupId: KnockoutObservable<string>;
//    ProduceGroupName: KnockoutObservable<string>;
//    ProduceItems: KnockoutObservableArray<ProduceModel> = ko.observableArray([]);
//    TotalBalance: KnockoutComputed<number>;
//    TotalSold: KnockoutComputed<number>;
//    TotalOversold: KnockoutComputed<number>;

//    constructor(data) {
//        data = data || {};

//        this.ProduceGroupId = ko.observable(data.ProduceGroupId);
//        this.ProduceGroupName = ko.observable(data.ProduceGroupName);
//        for (var i = 0; i < data.ProduceItems.length; i++) {
//            this.ProduceItems.push(new ProduceModel(data.ProduceItems[i]));
//        }

//        this.TotalBalance = ko.computed({
//            read: () => {
//                var totalBalance = 0;
//                for (var i = 0; i < this.ProduceItems().length; i++) {
//                    totalBalance += this.ProduceItems()[i].RemainingQuantity();
//                }
//                return Number(totalBalance);
//            }
//        });
//        this.TotalSold = ko.computed({
//            read: () => {
//                var totalSold = 0;
//                for (var i = 0; i < this.ProduceItems().length; i++) {
//                    totalSold += this.ProduceItems()[i].TicketItemQuantity();
//                }
//                return Number(totalSold);
//            }
//        });
//        this.TotalOversold = ko.computed({
//            read: () => {
//                var totalOversold = 0;
//                for (var i = 0; i < this.ProduceItems().length; i++) {
//                    totalOversold += this.ProduceItems()[i].Oversold();
//                }
//                return Number(totalOversold);
//            }
//        });
//    }

//    BalanceStyle = () => {
//        var style = "font-weight:bold;";
//        if (this.TotalBalance() < 0) {
//            style += " color:red;";
//        }
//        return style;
//    }

//    OversoldStyle = () => {
//        var style = "font-weight:bold;";
//        if (this.TotalOversold() > 0) {
//            style += " color:red;";
//        }
//        return style;
//    }
//}
////ProduceGroupModel  -  made up of any array of Produce.
//class ProduceModel {
//    ProduceName: KnockoutObservable<string>;
//    ExpectedQuantity: KnockoutObservable<number>;
//    TicketItemQuantity: KnockoutObservable<number>;
//    RemainingQuantity: KnockoutObservable<number>;
//    Oversold: KnockoutComputed<number>;

//    constructor(data) {
//        data = data || {};

//        this.ProduceName = ko.observable(data.ProduceName);
//        this.ExpectedQuantity = ko.observable(data.ExpectedQuantity);
//        this.TicketItemQuantity = ko.observable(data.TicketItemQuantity);
//        this.RemainingQuantity = ko.observable(data.RemainingQuantity);
//        this.Oversold = ko.computed({
//            read: () => {
//                var oversold = 0;
//                if (this.RemainingQuantity() < 0) {
//                    oversold = this.RemainingQuantity() * -1;
//                }
//                return Number(oversold);
//            }
//        });
//    }

//    BalanceStyle = () => {
//        var style = "font-weight:bold;";
//        if (this.RemainingQuantity() < 0) {
//            style += " color:red;";
//        }
//        return style;
//    }

//    OversoldStyle = () => {
//        var style = "font-weight:bold;";
//        if (this.Oversold() > 0) {
//            style += " color:red;";
//        }
//        return style;
//    }

   
//} 
//module app {

//    var StockboardHub = $.connection.stockBoardHub;
//    //var vm = new StockBoardModel();

//    $.connection.hub.logging = true;
//    $.connection.hub.start();

//    StockboardHub.client.liveStockboardMini = counters => {
//       // vm.addStock(produce);
//    }
//}





    // the generated client-side hub proxy
