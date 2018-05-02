class SalesLedgerEntry {
    SalesLedgerEntryID: KnockoutObservable<string>;
    SaleAmount: KnockoutObservable<number>;
    CreatedDate: KnockoutObservable<string>;
    SalesPersonName: KnockoutObservable<string>;
    CustomerDepartment: KnockoutObservable<string>;
    IsSelected: KnockoutObservable<boolean>;
    IsReconciled: KnockoutObservable<boolean>;

    constructor(data) {
        this.SalesLedgerEntryID = ko.observable(data.SalesLedgerEntryID);
        this.SaleAmount = ko.observable(data.SaleAmount);
        this.CreatedDate = ko.observable(data.CreatedDate);
        this.SalesPersonName = ko.observable(data.SalesPersonName);
        this.CustomerDepartment = ko.observable(data.CustomerDepartment);
        this.IsSelected = ko.observable(data.IsSelected || false);
        this.IsReconciled = ko.observable(data.IsReconciled || false);
    }
}

class DailyCashTicket {
    TicketID: KnockoutObservable<string>;
    TicketReference: KnockoutObservable<string>;
    SalesPersonName: KnockoutObservable<string>;
    AmountPaid: KnockoutObservable<number>;
    BalanceOwed: KnockoutObservable<number>;
    TicketTotal: KnockoutObservable<number>;
    CustomerDepartmentID: KnockoutObservable<string>;
    CreatedDate: KnockoutObservable<string>;
    SalesInvoiceID: KnockoutObservable<string>;
    DivisionID: KnockoutObservable<string>;
    IsSelected: KnockoutObservable<boolean>;
    IsReconciled: KnockoutObservable<boolean>;
    MatchedSalesLedger: KnockoutObservableArray<string>;

    constructor(data) {
        this.TicketID = ko.observable(data.TicketID);
        this.TicketReference = ko.observable(data.TicketReference);
        this.SalesPersonName = ko.observable(data.SalesPersonName);
        this.AmountPaid = ko.observable(data.AmountPaid);
        this.BalanceOwed = ko.observable(data.BalanceOwed);
        this.TicketTotal = ko.observable(data.TicketTotal);
        this.CustomerDepartmentID = ko.observable(data.CustomerDepartmentID);
        this.CreatedDate = ko.observable(data.CreatedDate);
        this.SalesInvoiceID = ko.observable(data.SalesInvoiceID);
        this.DivisionID = ko.observable(data.DivisionID);
        this.IsSelected = ko.observable(data.IsSelected || false);
        this.IsReconciled = ko.observable(data.IsReconciled || false);
        this.MatchedSalesLedger = ko.observableArray([]);
    }
}

class DailyCashTicketAllocationsPagingModel {
    AllowMismatch: KnockoutObservable<boolean>;
    Paging: KnockoutObservable<Paging>;
    SalesLedgerItemTotal: KnockoutObservable<number>;
    SalesLedgerEntries: KnockoutObservableArray<SalesLedgerEntry>;
    ExactMatchNumberSearch: KnockoutObservable<boolean>;
    TicketEntries: KnockoutObservableArray<DailyCashTicket>;
    MatchItemBalanceTotal: KnockoutObservable<number>;
    MatchItemAmountTotal: KnockoutObservable<number>;
    
    DailyCashSearch: KnockoutObservable<DailyCashSearch>;
    SearchClicked: KnockoutObservable<boolean>;
    IsSearchValid: KnockoutComputed<boolean>;

    paidType: KnockoutObservable<string>;

    //FilterByItemAmount: (data: ReconciliationItem) => void;
    FilterByItemAmount: (data: DailyCashTicket) => void;
    ClearFilters: () => void;
    MatchEnabled: any;
    FilterByStatementAmount: (data: StatementImportItem) => void;

    SortTicketDate: KnockoutObservable<boolean>;
    SortTicketDateClick: () => void;
    SortTicketReference: KnockoutObservable<boolean>;
    SortTicketReferenceClick: () => void;
    SortBalanceOwed: KnockoutObservable<boolean>;
    SortBalanceOwedClick: () => void;
    SortSalesPerson: KnockoutObservable<boolean>;
    SortSalesPersonClick: () => void;
    SortAmountPaid: KnockoutObservable<boolean>;
    SortAmountPaidClick: () => void;
    SortTotalPrice: KnockoutObservable<boolean>;
    SortTotalPriceClick: () => void;
    TicketFilter: any;
    SalesLedgerEntryFilter: any;
    SortEntrySalesPerson: KnockoutObservable<boolean>;
    SortEntrySalesPersonClick: () => void;
    SortSaleAmount: KnockoutObservable<boolean>;
    SortSaleAmountClick: () => void;
    SortCreatedDate: KnockoutObservable<boolean>;
    SortCreatedDateClick: () => void;
    SearchSaleAmount: KnockoutObservable<number>;
    FilterDecimalsHelper: (p1: number, P2: number) => boolean;
    OrderAscending: KnockoutObservable<boolean>;

    constructor(data) {
        this.AllowMismatch = ko.observable(data.AllowMismatch || false);
        this.ExactMatchNumberSearch = ko.observable(data.AllowMismatch || true);
        this.SearchSaleAmount = ko.observable(data.SearchRecAmount || "");
        this.MatchItemBalanceTotal = ko.observable(data.MatchItemBalanceTotal || 0);
        this.MatchItemAmountTotal = ko.observable(data.MatchItemAmountTotal || 0);
        this.SalesLedgerItemTotal = ko.observable(data.SalesLedgerItemTotal || 0);
        if (this.SalesLedgerItemTotal() == Number.NaN) {
            this.SalesLedgerItemTotal(0);
        }

        this.paidType = ko.observable(data.paidType || "Paid");

        this.Paging = ko.observable(new Paging(data.TicketEditModels, data.SearchObject));

        this.SalesLedgerEntries = ko.observableArray([]);
        this.TicketEntries = ko.observableArray([]).extend({ notify: 'always' });
        
        for (let i = 0; i < data.SalesLedgerEntries.Results.length; i++)
        {
            this.SalesLedgerEntries.push(new SalesLedgerEntry(data.SalesLedgerEntries.Results[i]));
        }

        //for (let i = 0; i < data.TicketEditModelsVw.Results.length; i++) {
        //    this.TicketEntries.push(new DailyCashTicket(data.TicketEditModelsVw.Results[i]));
        //}

        for (let i = 0; i < data.TicketViewModelsVw.Results.length; i++) {
            this.TicketEntries.push(new DailyCashTicket(data.TicketViewModelsVw.Results[i]));
        }

        this.DailyCashSearch = ko.observable(new DailyCashSearch());
        this.SearchClicked = ko.observable(false);
        this.IsSearchValid = ko.computed({
            read: () => {
                return (this.SearchClicked() && !this.DailyCashSearch().validationModel.isValid());
            }
        });

        this.FilterByItemAmount = (data) => {
            let searchAmount = Number(data.AmountPaid());
            this.SearchSaleAmount(searchAmount);
        }

        // Clear Filters
        this.ClearFilters = () => {
            let empty: any = "";
            this.SearchSaleAmount(empty);
        }

        this.MatchEnabled = ko.computed(() => {
            //let test1 = this.MatchItemAmountTotal().toFixed(2);
            //if (Number(this.MatchItemAmountTotal().toFixed(2)) != 0 && Number(this.SalesLedgerItemAmountTotal().toFixed(2)) != 0) {
            //    let testnum = Number(this.MatchItemAmountTotal().toFixed(2)) - Number(this.SalesLedgerItemAmountTotal().toFixed(2));

            //    if (this.AllowMismatch() == true) return false;
            //    return testnum == 0 ? false : true;
            //} else return true;
            return false;
        });

        this.FilterByStatementAmount = (data) => {
            
        }

        this.SortTicketDate = ko.observable(false);

        this.SortTicketDateClick = () => {
            this.SortTicketDate(true);
            this.SortTicketReference(false);
            this.SortBalanceOwed(false);
            this.SortAmountPaid(false);
            this.SortTotalPrice(false);
            this.SortSalesPerson(false);

            this.SortEntrySalesPerson(false);
            this.SortSaleAmount(false);
            this.SortCreatedDate(false);

            this.OrderAscending(!this.OrderAscending());
        }

        this.SortTicketReference = ko.observable(false);

        this.SortTicketReferenceClick = () => {
            this.SortTicketReference(true);
            this.SortTicketDate(false);
            this.SortBalanceOwed(false);
            this.SortAmountPaid(false);
            this.SortTotalPrice(false);
            this.SortSalesPerson(false);

            this.SortEntrySalesPerson(false);
            this.SortSaleAmount(false);
            this.SortCreatedDate(false);

            this.OrderAscending(!this.OrderAscending());
        }

        this.SortSalesPerson = ko.observable(false);

        this.SortSalesPersonClick = () => {
            this.SortSalesPerson(true);
            this.SortTicketReference(false);
            this.SortBalanceOwed(false);
            this.SortAmountPaid(false);
            this.SortTotalPrice(false);
            this.SortTicketDate(false);
            
            this.SortEntrySalesPerson(false);
            this.SortSaleAmount(false);
            this.SortCreatedDate(false);

            this.OrderAscending(!this.OrderAscending());
        }

        this.SortBalanceOwed = ko.observable(false);

        this.SortBalanceOwedClick = () => {

            this.SortBalanceOwed(true);
            this.SortTicketReference(false);
            this.SortTicketDate(false);
            this.SortAmountPaid(false);
            this.SortTotalPrice(false);
            this.SortSalesPerson(false);

            this.SortEntrySalesPerson(false);
            this.SortSaleAmount(false);
            this.SortCreatedDate(false);

            this.OrderAscending(!this.OrderAscending());
        }

        this.SortAmountPaid = ko.observable(false);

        this.SortAmountPaidClick = () => {

            this.SortAmountPaid(true);
            this.SortTicketReference(false);
            this.SortBalanceOwed(false);
            this.SortTicketDate(false);
            this.SortTotalPrice(false);
            this.SortSalesPerson(false);
            
            this.SortEntrySalesPerson(false);
            this.SortSaleAmount(false);
            this.SortCreatedDate(false);

            this.OrderAscending(!this.OrderAscending());
        }

        this.SortTotalPrice = ko.observable(false);

        this.SortTotalPriceClick = () => {

            this.SortTotalPrice(true);
            this.SortTicketReference(false);
            this.SortBalanceOwed(false);
            this.SortAmountPaid(false);
            this.SortTicketDate(false);
            this.SortSalesPerson(false);

            this.SortEntrySalesPerson(false);
            this.SortSaleAmount(false);
            this.SortCreatedDate(false);

            this.OrderAscending(!this.OrderAscending());
        }

        this.SortTotalPriceClick = () => {

            this.SortTotalPrice(true);
            this.SortTicketReference(false);
            this.SortBalanceOwed(false);
            this.SortAmountPaid(false);
            this.SortTicketDate(false);
            this.SortSalesPerson(false);

            this.SortEntrySalesPerson(false);
            this.SortSaleAmount(false);
            this.SortCreatedDate(false);

            this.OrderAscending(!this.OrderAscending());
        }

        this.SortTotalPriceClick = () => {

            this.SortTotalPrice(true);
            this.SortTicketReference(false);
            this.SortBalanceOwed(false);
            this.SortAmountPaid(false);
            this.SortTicketDate(false);
            this.SortSalesPerson(false);

            this.SortEntrySalesPerson(false);
            this.SortSaleAmount(false);
            this.SortCreatedDate(false);

            this.OrderAscending(!this.OrderAscending());
        }

        this.SortEntrySalesPersonClick = () => {

            this.SortEntrySalesPerson(true);
            this.SortSaleAmount(false);
            this.SortCreatedDate(false);

            this.SortTotalPrice(false);
            this.SortTicketReference(false);
            this.SortBalanceOwed(false);
            this.SortAmountPaid(false);
            this.SortTicketDate(false);
            this.SortSalesPerson(false);

            this.OrderAscending(!this.OrderAscending());
        }

        this.SortCreatedDateClick = () => {

            this.SortCreatedDate(true);
            this.SortSaleAmount(false);
            this.SortEntrySalesPerson(false);

            this.SortTotalPrice(false);
            this.SortTicketReference(false);
            this.SortBalanceOwed(false);
            this.SortAmountPaid(false);
            this.SortTicketDate(false);
            this.SortSalesPerson(false);

            this.OrderAscending(!this.OrderAscending());
        }

        this.SortSaleAmountClick = () => {

            this.SortSaleAmount(true);
            this.SortCreatedDate(false);
            this.SortEntrySalesPerson(false);

            this.SortTotalPrice(false);
            this.SortTicketReference(false);
            this.SortBalanceOwed(false);
            this.SortAmountPaid(false);
            this.SortTicketDate(false);
            this.SortSalesPerson(false);

            this.OrderAscending(!this.OrderAscending());
        }

        this.OrderAscending = ko.observable(false);

        this.SortSaleAmount = ko.observable(false);
        this.SortCreatedDate = ko.observable(false);
        this.SortEntrySalesPerson = ko.observable(false);

        // Filter and Sort Statement Items
        this.TicketFilter = ko.computed(() => {
            this.OrderAscending();
            let self = this;
            
           // var rtnArray = this.Paging().entities();
            var rtnArray = this.TicketEntries();
            
            if (this.SortTicketReference() === true) {
                rtnArray.sort(function (left, right) {
                    return self.orderTheseTwo(self, left.TicketReference().toLowerCase(), right.TicketReference().toLowerCase());
                });
            }

            if (this.SortSalesPerson() === true) {
                rtnArray.sort(function (left, right) {
                    return self.orderTheseTwo(self, left.SalesPersonName().toLowerCase(), right.SalesPersonName().toLowerCase());
                });
            }

            if (this.SortBalanceOwed() == true) {
                rtnArray.sort(function (left, right) {
                    return self.orderTheseTwo(self, Number(left.BalanceOwed), Number(right.BalanceOwed));
                });
            }

            if (this.SortAmountPaid() == true) {
                rtnArray.sort(function (left, right) {
                    return self.orderTheseTwo(self, Number(left.AmountPaid), Number(right.AmountPaid));
                });
            }

            if (this.SortTotalPrice() == true) {
                rtnArray.sort(function (left, right) {
                    return self.orderTheseTwo(self, Number(left.TicketTotal), Number(right.TicketTotal));
                });
            }
            
            //if (this.SortTicketDate() == true) {
            //    rtnArray.sort(function (left, right) {
            //        return self.orderTheseTwo(self, self.reverseString(left.CreatedDate), self.reverseString(right.CreatedDate));
            //    });
            //}

            return rtnArray;
        });

        // Search Filter Helper Method to do filter exactly or floor to nearest integer
        this.FilterDecimalsHelper = function (p1: number, p2: number) {
            let ismatched = false;

            if (this.ExactMatchNumberSearch() == false) {
                if (p2 < 1) {
                    let n1 = Math.ceil(p1);
                    let n2 = Math.ceil(p2);
                    ismatched = n1 == n2;
                }
                else {
                    let n1 = Math.floor(p1);
                    let n2 = Math.floor(p2);
                    ismatched = n1 == n2;
                }
            }
            else {
                ismatched = p1 == p2;
            }
            return ismatched;
        }

        // Filter and Sort Statement Items
        this.SalesLedgerEntryFilter = ko.computed(() => {
            this.OrderAscending();
            let self = this;
            //this.SearchSaleAmount(60);
            // debugger;
            //var rtnArray = this.SalesLedgerEntries();
            var testArray = this.SalesLedgerEntries();
            var rtnArray = ko.utils.arrayFilter(self.SalesLedgerEntries(),(salesLedgerEntry: SalesLedgerEntry) =>
            (
                (this.SearchSaleAmount() == 0 || this.FilterDecimalsHelper(Number(salesLedgerEntry.SaleAmount()), Number(this.SearchSaleAmount())))
            )
            );


            if (this.SortEntrySalesPerson() === true) {
                rtnArray.sort(function (left, right) {
                    return self.orderTheseTwo(self, left.SalesPersonName().toLowerCase(), right.SalesPersonName().toLowerCase());
                });
            }
            
            if (this.SortSaleAmount() == true) {
                rtnArray.sort(function (left, right) {
                    return self.orderTheseTwo(self, Number(left.SaleAmount()), Number(right.SaleAmount()));
                });
            }

            if (this.SortCreatedDate() == true) {
                rtnArray.sort(function (left, right) {
                    return self.orderTheseTwo(self, self.reverseString(left.CreatedDate()), self.reverseString(right.CreatedDate()));
                });
            }

            return rtnArray;
        });
    }

    orderTheseTwo = function (self, left, right) {
        if (left === right) return 0;
        else {
            if (self.OrderAscending())
                return left < right ? -1 : 1;
            else {
                return left > right ? -1 : 1;
            }
        }
    }

    reverseString = function (reverseIt) {
        //debugger;
        let dateParts = reverseIt.split(' ')[0].split('/');
        let reverseDate = dateParts[2] + dateParts[1] + dateParts[0] + reverseIt.split(' ')[1];
        return reverseDate;
    }

    Search = function () {
        this.SearchClicked(true);
        if (this.DailyCashSearch().validationModel.isValid()) {
            this.Paging().fetchEntitiesWithSearch("Ticket/Index", this.DailyCashSearch);
        }
    }

    SearchBySupplierDepartmentId = function () {
        this.SearchClicked(false);
        this.DailyCashSearch().TicketReference("");
        this.Paging().fetchEntitiesWithSearch("Ticket/Index", this.DailyCashSearch);
    }

    Reset = function () {
        this.SearchClicked(false);
        // reset search box
        this.DailyCashSearch().TicketReference("");
        // returns all entries
        this.Paging().fetchEntitiesWithSearch("Ticket/Index", this.DailyCashSearch);
    }

    OpenConsignmentDetails = function (data) {
        var uri = "Ticket/DetailsTab/" + data;

    }

    myScope(index, data, root) {
        var scopetest = data.TicketEntries();
        //if (scopetest[index].IsReconciled._latestValue == undefined) {
        //    scopetest[index].IsReconciled(false);
        //}

        if (scopetest[index].IsReconciled._latestValue == false) {
            root.IsSelected(!root.IsSelected._latestValue);
            if (root.IsSelected._latestValue == true) {
                let totalBal = Number(data.MatchItemBalanceTotal._latestValue) + root.TicketTotal._latestValue;
                data.MatchItemBalanceTotal(totalBal);
                let totalAmount = Number(data.MatchItemAmountTotal._latestValue) + root.AmountPaid._latestValue;
                data.MatchItemAmountTotal(totalAmount);
            } else {
                let totalBal = Number(data.MatchItemBalanceTotal._latestValue) - root.TicketTotal._latestValue;
                data.MatchItemBalanceTotal(totalBal);
                let totalAmount = Number(data.MatchItemAmountTotal._latestValue) - root.AmountPaid._latestValue;
                data.MatchItemAmountTotal(totalAmount);
            }
            
            //data.TicketEntries.valueHasMutated();
            //data.TicketEntries.notifySubscribers(data.TicketEntries());

            var array = ko.observableArray([]);
            var lenval = data.TicketEntries._latestValue.length;
            for (var i = 0; i < lenval; i++) {
                array.push(data.TicketEntries._latestValue[i]);
            }

            //root.TicketEntries.removeAll();
            data.TicketEntries.removeAll();
            ko.utils.arrayPushAll(data.TicketEntries(), array());
            data.TicketEntries.valueHasMutated();
            data.MatchItemAmountTotal.valueHasMutated();
            data.MatchItemBalanceTotal.valueHasMutated();
            data.TicketEntries.valueHasMutated();
            data.TicketEntries.notifySubscribers(data.TicketEntries());
        }
    }

    salesLedgerSelect(index, data, root) {
        
        //debugger;
        if (root.IsReconciled._latestValue == false) {
        root.IsSelected(!root.IsSelected._latestValue);
        let totalBal: number = 0;
        if (data.SalesLedgerItemTotal._latestValue == Number.NaN) {
            data.SalesLedgerItemTotal(0);
        }
        if (root.IsSelected._latestValue == true) {
                totalBal = Number(data.SalesLedgerItemTotal._latestValue) + root.SaleAmount._latestValue;
                totalBal = Number(totalBal.toFixed(2));
                data.SalesLedgerItemTotal(totalBal);
            } else {
                totalBal = Number(data.SalesLedgerItemTotal._latestValue) - root.SaleAmount._latestValue;
                totalBal = Number(totalBal.toFixed(2));
                data.SalesLedgerItemTotal(totalBal);
            }

            //data.SalesLedgerEntries.valueHasMutated();
            //data.SalesLedgerEntries.notifySubscribers(data.SalesLedgerEntries());

            var array = ko.observableArray([]);
            var lenval = data.SalesLedgerEntries._latestValue.length;
            for (var i = 0; i < lenval; i++) {
                array.push(data.SalesLedgerEntries._latestValue[i]);
            }
        
            data.SalesLedgerEntries.removeAll();
            ko.utils.arrayPushAll(data.SalesLedgerEntries(), array());
            data.SalesLedgerEntries.valueHasMutated();
            data.SalesLedgerItemTotal.valueHasMutated();
            data.SalesLedgerEntries.notifySubscribers(data.SalesLedgerEntries());
         }
    }

    unMatch(index, data, root) {
        var f = () => console.log(this); // |this| is the instance of myScope
        if (root.IsReconciled) {
           
            //root.IsReconciled = !root.IsReconciled;
            root.IsReconciled(!root.IsReconciled);
            //data.PostReconciliationChange(root); // save to Database

            for (let ledger of root.MatchedSalesLedger._latestValue) {

                for (let item of data.SalesLedgerEntries._latestValue) {
                    if (item.SalesLedgerEntryID == ledger) {
                        item.IsReconciled(false);
                    }
                }
            }

            for (var i = 0; i < root.MatchedSalesLedger._latestValue.length; i++) {
                root.MatchedSalesLedger.pop();
            }
        }

        // left list
        var array = ko.observableArray([]);
        var lenval = data.TicketEntries._latestValue.length;
        for (var i = 0; i < lenval; i++) {
            array.push(data.TicketEntries._latestValue[i]);
        }

        data.TicketEntries.removeAll();
        ko.utils.arrayPushAll(data.TicketEntries(), array());
        data.TicketEntries.valueHasMutated();

        // right List
        array = ko.observableArray([]);
        lenval = data.SalesLedgerEntries._latestValue.length;
        for (var i = 0; i < lenval; i++) {
            array.push(data.SalesLedgerEntries._latestValue[i]);
        }

        data.SalesLedgerEntries.removeAll();
        ko.utils.arrayPushAll(data.SalesLedgerEntries(), array());
        data.SalesLedgerEntries.valueHasMutated();
    }

    unMatchByStatement(index, data, root) {
        if (root.IsReconciled) {
            
            root.IsReconciled(!root.IsReconciled);
            //data.PostReconciliationChange(root); // save to Database

            for (let ticket of data.TicketEntries._latestValue) {
                if (ticket.IsReconciled == true) {
                    for (let item of data.MatchedSalesLedger._latestValue) {
                        if (item.SalesLedgerEntryID == root.SalesLedgerEntryID) {
                            ticket.MatchedSalesLedger.pop();
                            item.IsReconciled(false);
                        }
                    }
                }
            }

            for (var i = 0; i < root.MatchedSalesLedger._latestValue.length; i++) {
                root.MatchedSalesLedger.pop();
            }
        }
    }

    DoMatch(data) {
        for (let item of data.TicketEntries._latestValue) {
            if (item.IsSelected._latestValue == true) {
                
                if (item.MatchedSalesLedger._latestValue.length > 0) {
                    item.MatchedSalesLedger.pop();
                }
                for (let ledger of data.SalesLedgerEntries._latestValue) {
                    if (ledger.IsSelected._latestValue == true) {
                        item.MatchedSalesLedger.push(ledger.SalesLedgerEntryID);
                        //ledger.IsReconciled = true;
                        //ledger.IsSelected = false;
                        ledger.IsReconciled(true);
                        ledger.IsSelected(false);
                    }
                }
                //item.IsReconciled = true;
                //item.IsSelected = false;
                item.IsReconciled(true);
                item.IsSelected(false);

                this.refreshStatementItems(data);
                this.refreshTicketItems(data);
                this.SalesLedgerItemTotal(0);
                this.MatchItemAmountTotal(0);
                this.MatchItemBalanceTotal(0);
                //this.PostReconciliationChange(item); // save to Database
            }
        }
    }

    refreshTicketItems(data) {
        var array = ko.observableArray([]);
        var lenval = data.TicketEntries._latestValue.length;
        for (var i = 0; i < lenval; i++) {
            array.push(data.TicketEntries._latestValue[i]);
        }

        data.TicketEntries.removeAll();
        ko.utils.arrayPushAll(data.TicketEntries(), array());
        data.TicketEntries.valueHasMutated();
    }

    refreshStatementItems(data) {
        var array = ko.observableArray([]);
        var lenval = data.SalesLedgerEntries._latestValue.length;
        for (var i = 0; i < lenval; i++) {
            array.push(data.SalesLedgerEntries._latestValue[i]);
        }

        data.SalesLedgerEntries.removeAll();
        ko.utils.arrayPushAll(data.SalesLedgerEntries(), array());
        data.SalesLedgerEntries.valueHasMutated();
    }

    statementSelect(index, data, root) {}
}



class DailyCashSearch {
    TicketReference: KnockoutObservable<string>;
    CustomerDepartmentID: KnockoutObservable<string>;
    validationModel: KnockoutObservable<any>;

    constructor() {
        this.TicketReference = ko.observable("").extend({ required: true });
        this.TicketReference.isModified(false);
        this.CustomerDepartmentID = ko.observable("0");
        this.validationModel = ko.validatedObservable({
            TicketReference: this.TicketReference
        });
    }
} 