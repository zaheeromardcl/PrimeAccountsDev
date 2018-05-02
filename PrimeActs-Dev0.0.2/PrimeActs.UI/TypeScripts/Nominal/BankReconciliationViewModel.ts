/// <reference path="ReconciliationItem.ts" />
/// <reference path="StatementImportItem.ts" />
/// <reference path="../../Scripts/typings/knockout/knockout.d.ts" /> 
/// <reference path="../../Scripts/typings/moment/moment.d.ts" />

class BankReconciliationViewModel {
    ReconciliationItems: KnockoutObservableArray<ReconciliationItem>;
    ReconciliationItemBalanceTotal: KnockoutObservable<number>;
    ReconciliationItemAmountTotal: KnockoutObservable<number>;
    StatementImportItems: KnockoutObservableArray<StatementImportItem>;
    StatementItemAmountTotal: KnockoutObservable<number>;
    CopyItems: KnockoutObservableArray<ReconciliationItem>;
    TestVar: KnockoutObservable<number>;
    SearchDescription: KnockoutObservable<string>;
    SearchRecAmount: KnockoutObservable<number>;
    SearchType: KnockoutObservable<string>;
    SearchStatementDescription: KnockoutObservable<string>;
    SearchStatementAmount: KnockoutObservable<number>;
    SearchStatementType: KnockoutObservable<string>;
    TestComputed: any;
    TestFilter: any;
    StatementFilter: any;
    DescriptionCopy: KnockoutObservable<boolean>;
    AmountCopy: KnockoutObservable<boolean>;
    TypeCopy: KnockoutObservable<boolean>;
    AllowMismatch: KnockoutObservable<boolean>;
    MatchEnabled: any;
    AutoHideMatched: KnockoutObservable<boolean>;
    CannotMatch: KnockoutObservable<boolean>;
    ExactMatchNumberSearch: KnockoutObservable<boolean>;
    FilterByItemAmount: (data: ReconciliationItem) => void;
    FilterByStatementAmount: (data: StatementImportItem) => void;
    ClearFilters: () => void;
    TotalStatementDebit: KnockoutObservable<number>;
    TotalStatementCredit: KnockoutObservable<number>;
    TotalStatementDebitMatched: KnockoutObservable<number>;
    TotalStatementCreditMatched: KnockoutObservable<number>;
    ComputeStatementDebit: () => void;
    ComputeStatementDebitMatched: () => void;
    ComputeStatementDebitUnMatched: () => void;
    ComputeStatementCredit: () => void;
    ComputeStatementCreditMatched: () => void;
    ComputeStatementCreditUnMatched: () => void;
    ComputeReconciliationDebit: () => void;
    ComputeReconciliationDebitMatched: () => void;
    ComputeReconciliationDebitUnMatched: () => void;
    ComputeReconciliationCredit: () => void;
    ComputeReconciliationCreditMatched: () => void;
    ComputeReconciliationCreditUnMatched: () => void;
    ComputeFirstStatementDate: () => any;//KnockoutObservable<Date>;
    SortDescription: KnockoutObservable<boolean>;
    SortAmount: KnockoutObservable<boolean>;
    SortType: KnockoutObservable<boolean>;
    SortDate: KnockoutObservable<boolean>;
    SortDescriptionClick: () => void;
    SortAmountClick: () => void;
    SortTypeClick: () => void;
    SortDateClick: () => void;
    SortNotifier: KnockoutObservable<boolean>;
    SortStatementDescription: KnockoutObservable<boolean>;
    SortStatementAmount: KnockoutObservable<boolean>;
    SortStatementType: KnockoutObservable<boolean>;
    SortStatementDate: KnockoutObservable<boolean>;
    SortStatementDescriptionClick: () => void;
    SortStatementAmountClick: () => void;
    SortStatementTypeClick: () => void;
    SortStatementDateClick: () => void;
    SortStatementNotifier: KnockoutObservable<boolean>;
    PostChangesClick: () => void;
    ReturnToHeaderClick: () => void;
    PostReconciliationChange: (ReconciliationItem) => void;
    FilterDecimalsHelper: (p1: number, P2: number) => boolean;
    BankStatementID: KnockoutObservable<string>;

    constructor(data) {
        data = data || {};
        this.ReconciliationItemBalanceTotal = ko.observable(data.ReconciliationItemBalanceTotal || 0);
        this.ReconciliationItemAmountTotal = ko.observable(data.ReconciliationItemAmountTotal || 0);
        this.StatementItemAmountTotal = ko.observable(data.StatementItemAmountTotal || 0);
        this.ReconciliationItems = ko.observableArray([]).extend({ notify: 'always' });;
        this.StatementImportItems = ko.observableArray([]).extend({ notify: 'always' });;
        this.TestVar = ko.observable(data.TestVar || 10);
        this.SearchDescription = ko.observable(data.SearchDescription || "");
        this.SearchRecAmount = ko.observable(data.SearchRecAmount || "");
        this.SearchType = ko.observable(data.SearchType || "");
        this.SearchStatementDescription = ko.observable(data.SearchStatementDescription || "");
        this.SearchStatementAmount = ko.observable(data.SearchStatementAmount || "");
        this.SearchStatementType = ko.observable(data.SearchStatementType || "");
        this.DescriptionCopy = ko.observable(data.DescriptionCopy || true);
        this.AmountCopy = ko.observable(data.AmountCopy || true);
        this.TypeCopy = ko.observable(data.TypeCopy || true);
        this.AutoHideMatched = ko.observable(data.AutoHideMatched || true);
        this.CannotMatch = ko.observable(data.CannotMatch || true);
        this.AllowMismatch = ko.observable(data.AllowMismatch || false);
        this.ExactMatchNumberSearch = ko.observable(data.AllowMismatch || false);
        this.SortDescription = ko.observable(data || false);
        this.SortAmount = ko.observable(data || false);
        this.SortType = ko.observable(data || false);
        this.SortDate = ko.observable(data || false);
        this.SortNotifier = ko.observable(data || false);
        this.SortStatementDescription = ko.observable(data || false);
        this.SortStatementAmount = ko.observable(data || false);
        this.SortStatementType = ko.observable(data || false);
        this.SortStatementDate = ko.observable(data || false);
        this.SortStatementNotifier = ko.observable(data || false);
        this.BankStatementID = ko.observable(data.BankStatementID || "");

        this.SortDescription(true);
        this.SortAmount(false);
        this.SortType(false);
        this.SortDate(false);
        this.SortStatementDescription(true);
        this.SortStatementAmount(false);
        this.SortStatementType(false);
        this.SortStatementDate(false);

        this.SearchDescription.subscribe((data) => {

            if (this.DescriptionCopy() == true) {
                this.SearchStatementDescription(data);
            }
        });

        this.SearchRecAmount.subscribe((data) => {
            //console.log(data);
            if (this.AmountCopy() == true) {
                this.SearchStatementAmount(data);
            }
        });

        this.SearchType.subscribe((data) => {
            //console.log(data);
            if (this.TypeCopy() == true) {
                this.SearchStatementType(data);
            }
        });

        this.AllowMismatch.subscribe((data) => {

        });

        this.MatchEnabled = ko.computed(() => {
            if (Number(this.ReconciliationItemAmountTotal().toFixed(2)) != 0 && Number(this.StatementItemAmountTotal().toFixed(2)) != 0) {
                let testnum = Number(this.ReconciliationItemAmountTotal().toFixed(2)) - Number(this.StatementItemAmountTotal().toFixed(2));

                if (this.AllowMismatch() == true) return false;
                return testnum == 0 ? false : true;
            } else return true;
        });

        // Statement Computed Totals
        this.ComputeFirstStatementDate = ko.computed(() => {

            //let t1 = this.StatementImportItems()[0];
            //let testdate = this.StatementImportItems()[0].Date;

            let testdate = ko.observable<Date>();
            var jsdate;
            var count = 0;

            return testdate;
        });



        this.ComputeStatementDebit = ko.pureComputed(() => {
            let totalval = 0;
            for (let statement of this.StatementImportItems()) {
                if (Number(statement.Amount) < 0) {
                    totalval = totalval + Number(statement.Amount);
                }
            }
            return totalval.toFixed(2);
        });

        this.ComputeStatementDebitMatched = ko.pureComputed(() => {
            let totalval = 0;
            for (let statement of this.StatementImportItems()) {
                if (Number(statement.Amount) < 0 && Boolean(statement.IsReconciled) == true) {
                    totalval = totalval + Number(statement.Amount);
                }
            }
            return totalval.toFixed(2);
        });

        this.ComputeStatementDebitUnMatched = ko.pureComputed(() => {
            let total = this.ComputeStatementDebit();
            let totalmatched = this.ComputeStatementDebitMatched();
            //return Number(total) - Number(totalmatched);
            let totalunmatched = Number(total) - Number(totalmatched);
            return totalunmatched.toFixed(2);
        });

        this.ComputeStatementCredit = ko.pureComputed(() => {
            let totalval = 0;
            for (let statement of this.StatementImportItems()) {
                if (Number(statement.Amount) > 0) {
                    totalval = totalval + Number(statement.Amount);
                }
            }
            return totalval.toFixed(2);
        });

        this.ComputeStatementCreditMatched = ko.pureComputed(() => {
            let totalval = 0;
            for (let statement of this.StatementImportItems()) {
                if (Number(statement.Amount) > 0 && Boolean(statement.IsReconciled) == true) {
                    totalval = totalval + Number(statement.Amount);
                }
            }
            return totalval.toFixed(2);
        });

        this.ComputeStatementCreditUnMatched = ko.pureComputed(() => {
            let total = this.ComputeStatementCredit();
            let totalmatched = this.ComputeStatementCreditMatched();
            let totalunmatched = Number(total) - Number(totalmatched);
            return totalunmatched.toFixed(2);
        });

        // Reconciliation Computed Totals
        this.ComputeReconciliationDebit = ko.pureComputed(() => {
            let totalval = 0;
            for (let statement of this.ReconciliationItems()) {
                if (Number(statement.Amount) < 0) {
                    totalval = totalval + Number(statement.Amount);
                }
            }
            return totalval.toFixed(2);
        });

        this.ComputeReconciliationDebitMatched = ko.pureComputed(() => {
            let totalval = 0;
            for (let statement of this.ReconciliationItems()) {
                if (Number(statement.Amount) < 0 && Boolean(statement.IsReconciled) == true) {
                    totalval = totalval + Number(statement.Amount);
                }
            }
            return totalval.toFixed(2);
        });

        this.ComputeReconciliationDebitUnMatched = ko.pureComputed(() => {
            let total = this.ComputeReconciliationDebit();
            let totalmatched = this.ComputeReconciliationDebitMatched();
            //return Number(total) - Number(totalmatched);
            let totalunmatched = Number(total) - Number(totalmatched);
            return totalunmatched.toFixed(2);
        });

        this.ComputeReconciliationCredit = ko.pureComputed(() => {
            let totalval = 0;
            for (let statement of this.StatementImportItems()) {
                if (Number(statement.Amount) > 0) {
                    totalval = totalval + Number(statement.Amount);
                }
            }
            return totalval.toFixed(2);
        });

        this.ComputeReconciliationCreditMatched = ko.pureComputed(() => {
            let totalval = 0;
            for (let statement of this.ReconciliationItems()) {
                if (Number(statement.Amount) > 0 && Boolean(statement.IsReconciled) == true) {
                    totalval = totalval + Number(statement.Amount);
                }
            }
            return totalval.toFixed(2);
        });

        this.ComputeReconciliationCreditUnMatched = ko.pureComputed(() => {
            let total = this.ComputeReconciliationCredit();
            let totalmatched = this.ComputeReconciliationCreditMatched();
            let totalunmatched = Number(total) - Number(totalmatched);
            return totalunmatched.toFixed(2);
        });


        this.TestComputed = ko.pureComputed(() => {
            return Number(this.ReconciliationItemAmountTotal().toFixed(2)) - Number(this.StatementItemAmountTotal().toFixed(2));
        });

        this.ReturnToHeaderClick = () => {
            var url = "BankReconciliationSelect?ID=" + this.BankStatementID(); // return ID of edited Statement, to set selected etc
            window.location.href = url;
            //window.history.back();
        }

        //Reconciliation Sorts - TODO - work on multiple filters at same time
        this.SortDescriptionClick = () => {

            this.SortDescription(!this.SortDescription());
            this.SortAmount(false);
            this.SortType(false);
            this.SortDate(false);
            this.SortNotifier(true);
        }

        this.SortAmountClick = () => {

            this.SortAmount(!this.SortAmount());
            this.SortDescription(false);
            this.SortType(false);
            this.SortDate(false);
            this.SortNotifier(true);
        }

        this.SortTypeClick = () => {
            this.SortType(!this.SortType());
            this.SortDescription(false);
            this.SortAmount(false);
            this.SortDate(false);
            this.SortNotifier(true);
        }

        this.SortDateClick = () => {

            this.SortDate(!this.SortDate());
            this.SortType(false);
            this.SortDescription(false);
            this.SortAmount(false);
            this.SortNotifier(true);
        }

        //Statement Sorts TODO - work on multiple filters at same time
        this.SortStatementDescriptionClick = () => {

            this.SortStatementDescription(!this.SortStatementDescription());
            this.SortStatementAmount(false);
            this.SortStatementType(false);
            this.SortStatementDate(false);
            this.SortStatementNotifier(true);
        }

        this.SortStatementAmountClick = () => {

            this.SortStatementAmount(!this.SortStatementAmount());
            this.SortStatementDescription(false);
            this.SortStatementType(false);
            this.SortStatementDate(false);
            this.SortStatementNotifier(true);
        }

        this.SortStatementTypeClick = () => {

            this.SortStatementType(!this.SortStatementType());
            this.SortStatementDescription(false);
            this.SortStatementAmount(false);
            this.SortStatementDate(false);
            this.SortStatementNotifier(true);
        }

        this.SortStatementDateClick = () => {

            this.SortStatementDate(!this.SortStatementDate());
            this.SortStatementType(false);
            this.SortStatementDescription(false);
            this.SortStatementAmount(false);
            this.SortStatementNotifier(true);
        }

        this.PostReconciliationChange = (reconciliationItem) => {
            var param = ko.toJSON(reconciliationItem);
            var promise =
                $.ajax({
                    url: "/Nominal/PostReconciliationChange/",
                    cache: false,
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    // data: ko.toJSON(this.ReconciliationItems),
                    data: param,
                    //            data: {
                    //                statements
                    //},
                    dataType: "json",
                    success: function (result) {
                       
                        //var redirectTarget = "ConsignmentDetails";
                        //subscriberReplaceTab.notifySubscribers({
                        //    PanelName: tabPanelName,
                        //    NewPanelName: redirectTarget,
                        //    UriParam: id
                        //}, "save");
                    },
                    error: function (jqXHR, textStatus, errorThrown) {

                    }
                }).fail(
                    function (xhr, textStatus, err) {
                        //alert(err);
                    });

        };

        this.PostChangesClick = () => {

            var statements = ko.toJSON(this.StatementImportItems);
            var nominals = ko.toJSON(this.ReconciliationItems);
            var param = {
                statementImportTestModel: statements,
                reconciliationTestModel: nominals
            };
            var strings = JSON.stringify({ 'StatementPostModel': param });
            var string2 = JSON.stringify({ statementImportTestModel: statements, reconciliationTestModel: nominals });

            var promise =
                $.ajax({
                    url: "/Nominal/PostStringChanges/",
                    cache: false,
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    // data: ko.toJSON(this.ReconciliationItems),
                    data: statements,
                    //            data: {
                    //                statements
                    //},
                    dataType: "json",
                    success: function (result) {
                       
                        //var redirectTarget = "ConsignmentDetails";
                        //subscriberReplaceTab.notifySubscribers({
                        //    PanelName: tabPanelName,
                        //    NewPanelName: redirectTarget,
                        //    UriParam: id
                        //}, "save");
                    },
                    error: function (jqXHR, textStatus, errorThrown) {

                    }
                }).fail(
                    function (xhr, textStatus, err) {
                        //alert(err);
                    });
        }

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


        // Filter and Sort Reconciliation Items
        this.TestFilter = ko.computed(() => {
            //return ko.utils.arrayFilter(this.ReconciliationItems(), (reconciliationItem: ReconciliationItem) =>
            var rtnArray = ko.utils.arrayFilter(this.ReconciliationItems(), (reconciliationItem: ReconciliationItem) =>
                (
                    (this.SearchDescription().length == 0 || reconciliationItem.Description.toString().toLowerCase().indexOf(this.SearchDescription().toLowerCase()) != -1)
                    &&
                    //(this.SearchStatementAmount() == 0 || Math.floor(Number(reconciliationItem.Amount)) == Math.floor(Number(this.SearchStatementAmount())))
                    (this.SearchStatementAmount() == 0 || this.FilterDecimalsHelper(Number(reconciliationItem.Amount), Number(this.SearchStatementAmount())))
                    &&
                    (this.SearchType().length == 0 || reconciliationItem.Type.toString().toLowerCase().indexOf(this.SearchType().toLowerCase()) != -1)
                    &&
                    (this.AutoHideMatched() == true && Boolean(reconciliationItem.IsReconciled) == false || this.AutoHideMatched() == false)
                //&&
                // (this.SortNotifier() == true)
                )
            );
            this.SortNotifier(false);
            this.ReconciliationItems.valueHasMutated();

            if (this.SortDescription() == true && this.SortAmount() == true) {
                var rtnArray1 = rtnArray.sort(function (left, right) {
                    return Number(left.Amount) == Number(right.Amount) ? 0 : (Number(left.Amount) < Number(right.Amount) ? -1 : 1)
                });

                var rtnArry2 = rtnArray1.sort(function (left, right) {
                    return left.Description == right.Description ? 0 : (left.Description < right.Description ? -1 : 1)
                });
                return rtnArry2;
            }

            if (this.SortDescription() == true) {
                //console.log("Sort by Description");
                rtnArray.sort(function (left, right) {
                    return left.Description == right.Description ? 0 : (left.Description < right.Description ? -1 : 1)
                });
            }

            if (this.SortAmount() == true) {
                //console.log("Sort by Amount");
                rtnArray.sort(function (left, right) {
                    return Number(left.Amount) == Number(right.Amount) ? 0 : (Number(left.Amount) < Number(right.Amount) ? -1 : 1)
                });
            }

            if (this.SortType() == true) {
                //console.log("Sort by Type");
                rtnArray.sort(function (left, right) {
                    return left.Type == right.Type ? 0 : (left.Type < right.Type ? -1 : 1)
                });
            }

            if (this.SortDate() == true) {
                console.log("Sort by Date"); // just comparing typescript dates did not sort, so interim convert to vanilla js dates 
                rtnArray.sort(function (left, right) {
                    let leftdatestr = left.Date.toString().split("/");
                    var rightdatestr = right.Date.toString().split("/");
                    var dateLeft = new Date(Number(leftdatestr[2]), Number(leftdatestr[1]) - 1, Number(leftdatestr[0]));
                    var dateRight = new Date(Number(rightdatestr[2]), Number(rightdatestr[1]) - 1, Number(rightdatestr[0]));

                    return dateLeft == dateRight ? 0 : (dateLeft < dateRight ? -1 : 1)
                });
            }

            return rtnArray;
        }
        );

        // Filter and Sort Statement Items
        this.StatementFilter = ko.computed(() => {
            var test = this.StatementImportItems();
            //return ko.utils.arrayFilter(this.StatementImportItems(), (statementItem: StatementImportItem) =>
            var rtnArray = ko.utils.arrayFilter(this.StatementImportItems(), (statementItem: StatementImportItem) =>
                (
                    (this.SearchStatementDescription().length == 0 || statementItem.Description.toString().toLowerCase().indexOf(this.SearchStatementDescription().toLowerCase()) != -1)
                    &&
                    //   (this.SearchStatementAmount() == 0 || Number(statementItem.Amount) == Number(this.SearchStatementAmount()))
                    (this.SearchStatementAmount() == 0 || this.FilterDecimalsHelper(Number(statementItem.Amount), Number(this.SearchStatementAmount())))
                    &&
                    (this.SearchStatementType().length == 0 || statementItem.Type.toString().toLowerCase().indexOf(this.SearchStatementType().toLowerCase()) != -1)
                    &&
                    (this.AutoHideMatched() == true && Boolean(statementItem.IsReconciled) == false || this.AutoHideMatched() == false)
                    &&
                    (this.CannotMatch() == false && Boolean(statementItem.HasPossibleMatchingNominal) == true || this.CannotMatch() == true && Boolean(statementItem.HasPossibleMatchingNominal) == false)
                )
            );

            if (this.SortStatementDescription() == true && this.SortStatementAmount() == true) {
                var rtnArray1 = rtnArray.sort(function (left, right) {
                    return Number(left.Amount) == Number(right.Amount) ? 0 : (Number(left.Amount) < Number(right.Amount) ? -1 : 1)
                });

                var rtnArry2 = rtnArray1.sort(function (left, right) {
                    return left.Description == right.Description ? 0 : (left.Description < right.Description ? -1 : 1)
                });
                return rtnArry2;
            }

            if (this.SortStatementDescription() == true) {
                //console.log("Sort by Description");
                rtnArray.sort(function (left, right) {
                    return left.Description == right.Description ? 0 : (left.Description < right.Description ? -1 : 1)
                });
            }

            if (this.SortStatementAmount() == true) {
                //console.log("Sort by Amount");
                rtnArray.sort(function (left, right) {
                    return Number(left.Amount) == Number(right.Amount) ? 0 : (Number(left.Amount) < Number(right.Amount) ? -1 : 1)
                });
            }

            if (this.SortStatementType() == true) {
                //console.log("Sort by Type");
                rtnArray.sort(function (left, right) {
                    return left.Type == right.Type ? 0 : (left.Type < right.Type ? -1 : 1)
                });
            }

            if (this.SortStatementDate() == true) {
                console.log("Sort by Date"); // just comparing typescript dates did not sort, so interim convert to vanilla js dates 
                rtnArray.sort(function (left, right) {
                    let leftdatestr = left.Date.toString().split("/");
                    var rightdatestr = right.Date.toString().split("/");
                    var dateLeft = new Date(Number(leftdatestr[2]), Number(leftdatestr[1]) - 1, Number(leftdatestr[0]));
                    var dateRight = new Date(Number(rightdatestr[2]), Number(rightdatestr[1]) - 1, Number(rightdatestr[0]));

                    return dateLeft == dateRight ? 0 : (dateLeft < dateRight ? -1 : 1)
                });
            }

            return rtnArray;
        });


        if (data.hasOwnProperty('ReconciliationItems')) {
            for (var reconciliationItem of data.ReconciliationItems) {
                if (reconciliationItem != undefined) {

                    this.ReconciliationItems.push(reconciliationItem);
                    if (reconciliationItem.IsSelected) {
                        var totalBal = this.ReconciliationItemBalanceTotal() + reconciliationItem.Balance;
                        var totalAmount = this.ReconciliationItemAmountTotal() + reconciliationItem.Amount;
                        this.ReconciliationItemBalanceTotal(totalBal);
                        this.ReconciliationItemAmountTotal(totalAmount);
                    }
                }
            }
        }

        if (data.hasOwnProperty('StatementImportItems')) {
            for (var statementImportItem of data.StatementImportItems) {
                if (statementImportItem != undefined) {

                    this.StatementImportItems.push(statementImportItem);
                }
            }
        }


        this.FilterByItemAmount = (data) => {
            let searchAmount = Number(data.Amount);
            this.SearchStatementAmount(searchAmount);
        }

        this.FilterByStatementAmount = (data) => {
            let searchAmount = Number(data.Amount);
            this.SearchRecAmount(searchAmount);
        }

        // Clear Filters
        this.ClearFilters = () => {
            this.SearchDescription("");
            this.SearchStatementDescription("");
            this.SearchType("");
            this.SearchStatementType("");
            let empty: any = ""; // make Number fields empty on Bootstrap
            this.SearchRecAmount(empty);
            this.SearchStatementAmount(empty);
            //this.TestFilter.$index = 100;
            //this.SearchRecAmount(Number(empty));
        }
    }

    //FilterByItemAmount(index, data, root) {
    
    
    DoMatch(data) {
        for (let item of data.ReconciliationItems._latestValue) {
            if (item.IsSelected == true) {
                console.log(item.Description);

                if (item.ReconciledStatements.length > 0) {
                    item.ReconciledStatements.pop();
                }
                for (let statement of data.StatementImportItems._latestValue) {
                    if (statement.IsSelected == true) {
                        item.ReconciledStatements.push(statement.StatementId);
                        statement.IsReconciled = true;
                        statement.IsSelected = false;
                    }
                }
                item.IsReconciled = true;
                item.IsSelected = false;
                this.PostReconciliationChange(item); // save to Database
            }
        }

        // refresh grids
        this.refreshReconciliationItems(data);
        this.refreshStatementItems(data);
        this.ReconciliationItemAmountTotal(0);
        this.ReconciliationItemBalanceTotal(0);
        this.StatementItemAmountTotal(0);
    }


    refreshReconciliationItems(data) {
        var array = ko.observableArray([]);
        var lenval = data.ReconciliationItems._latestValue.length;
        for (var i = 0; i < lenval; i++) {
            array.push(data.ReconciliationItems._latestValue[i]);
        }

        data.ReconciliationItems.removeAll();
        ko.utils.arrayPushAll(data.ReconciliationItems(), array());
        data.ReconciliationItems.valueHasMutated();
    }

    refreshStatementItems(data) {
        var array = ko.observableArray([]);
        var lenval = data.StatementImportItems._latestValue.length;
        for (var i = 0; i < lenval; i++) {
            array.push(data.StatementImportItems._latestValue[i]);
        }

        data.StatementImportItems.removeAll();
        ko.utils.arrayPushAll(data.StatementImportItems(), array());
        data.StatementImportItems.valueHasMutated();
    }

    selectTest2 = (val) => {
        //console.log(this);
        this.TestVar(0);
        this.ReconciliationItems.valueHasMutated();
    }

    selectTest(index, data, root) {

        let self = this;
        //console.log(data.IsSelected);
        data.IsSelected = !data.IsSelected;
        //console.log(root);
        //root.valueHasMutated();
        //this.ReconciliationItems.valueHasMutated();
    }

    statementSelect(index, data, root) {
        //var f = function () { console.log(this); } // |this| is {}
        var f = () => console.log(this); // |this| is the instance of myScope
        if (!root.IsReconciled) {
            root.IsSelected = !root.IsSelected;
            if (root.IsSelected) {
                let totalBal = data.StatementItemAmountTotal._latestValue + root.Amount;
                totalBal = Number(totalBal.toFixed(2));
                data.StatementItemAmountTotal(totalBal);
            } else {
                let totalBal = data.StatementItemAmountTotal._latestValue - root.Amount;
                totalBal = Number(totalBal.toFixed(2));
                data.StatementItemAmountTotal(totalBal);
            }
             
            //data.StatementItemAmountTotal.valueHasMutated();
            data.StatementImportItems.valueHasMutated();
            data.StatementImportItems.notifySubscribers(data.StatementImportItems());

            var array = ko.observableArray([]);
            var lenval = data.StatementImportItems._latestValue.length;
            for (var i = 0; i < lenval; i++) {
                array.push(data.StatementImportItems._latestValue[i]);
            }

            data.StatementImportItems.removeAll();
            ko.utils.arrayPushAll(data.StatementImportItems(), array());
            data.StatementImportItems.valueHasMutated();
        }
        //data.valueHasMutated();
        f.call({});
    }

    myScope(index, data, root) {
        //var f = function () { console.log(this); } // |this| is {}
        var f = () => console.log(this); // |this| is the instance of myScope
        if (!root.IsReconciled) {
            root.IsSelected = !root.IsSelected;
            if (root.IsSelected) {
                let totalBal = data.ReconciliationItemBalanceTotal._latestValue + root.Balance;
                data.ReconciliationItemBalanceTotal(totalBal);
                let totalAmount = data.ReconciliationItemAmountTotal._latestValue + root.Amount;
                data.ReconciliationItemAmountTotal(totalAmount);
            } else {
                let totalBal = data.ReconciliationItemBalanceTotal._latestValue - root.Balance;
                data.ReconciliationItemBalanceTotal(totalBal);
                let totalAmount = data.ReconciliationItemAmountTotal._latestValue - root.Amount;
                data.ReconciliationItemAmountTotal(totalAmount);
            }

            data.ReconciliationItems.valueHasMutated();
            data.ReconciliationItems.notifySubscribers(data.ReconciliationItems());

            var array = ko.observableArray([]);
            var lenval = data.ReconciliationItems._latestValue.length;
            for (var i = 0; i < lenval; i++) {
                array.push(data.ReconciliationItems._latestValue[i]);
            }

            data.ReconciliationItems.removeAll();
            ko.utils.arrayPushAll(data.ReconciliationItems(), array());
            data.ReconciliationItems.valueHasMutated();
        }
        //data.valueHasMutated();
        f.call({});
    }


    unMatch(index, data, root) {
        //var f = function () { console.log(this); } // |this| is {}
        var f = () => console.log(this); // |this| is the instance of myScope
        if (root.IsReconciled) {
            // root.IsSelected = !root.IsSelected;
            
            root.IsReconciled = !root.IsReconciled;
            data.PostReconciliationChange(root); // save to Database

            for (let statement of root.ReconciledStatements) {

                for (let statementitem of data.StatementImportItems._latestValue) {
                    if (statementitem.StatementId == statement) {
                        statementitem.IsReconciled = false;
                    }
                }
            }

            for (var i = 0; i < root.ReconciledStatements.length; i++) {
                root.ReconciledStatements.pop();
            }

        }

        // data.ReconciliationItems.valueHasMutated();
        // data.ReconciliationItems.notifySubscribers(data.ReconciliationItems());

        var array = ko.observableArray([]);
        var lenval = data.ReconciliationItems._latestValue.length;
        for (var i = 0; i < lenval; i++) {
            array.push(data.ReconciliationItems._latestValue[i]);
        }

        data.ReconciliationItems.removeAll();
        ko.utils.arrayPushAll(data.ReconciliationItems(), array());
        data.ReconciliationItems.valueHasMutated();


        
        // }
        // this.refreshReconciliationItems(root);
        //  this.refreshStatementItems(root);
        //data.valueHasMutated();
        f.call({});
    }

    unMatchByStatement(index, data, root) {
        //var f = function () { console.log(this); } // |this| is {}
        var f = () => console.log(this); // |this| is the instance of myScope
        if (root.IsReconciled) {
            // root.IsSelected = !root.IsSelected;
            
            root.IsReconciled = !root.IsReconciled;

            for (let recon of data.ReconciliationItems._latestValue) {
                if (recon.IsReconciled == true) {
                    for (let statement of recon.ReconciledStatements) {
                        if (statement == root.StatementId) {
                            recon.ReconciledStatements.pop();
                            recon.IsReconciled = false;
                        }
                    }
                }
            }


            //for (let statement of root.ReconciledStatements) { 

            //    for (let statementitem of data.StatementImportItems._latestValue) {
            //        if (statementitem.StatementId == statement) {
            //            statementitem.IsReconciled = false;
            //        }
            //    }

            //}

            //for (var i = 0; i < root.ReconciledStatements.length; i++) {
            //    root.ReconciledStatements.pop();
            //}

        }

        // data.ReconciliationItems.valueHasMutated();
        // data.ReconciliationItems.notifySubscribers(data.ReconciliationItems());

        var array = ko.observableArray([]);
        var lenval = data.ReconciliationItems._latestValue.length;
        for (var i = 0; i < lenval; i++) {
            array.push(data.ReconciliationItems._latestValue[i]);
        }

        data.ReconciliationItems.removeAll();
        ko.utils.arrayPushAll(data.ReconciliationItems(), array());
        data.ReconciliationItems.valueHasMutated();


        array = ko.observableArray([]);
        lenval = data.StatementImportItems._latestValue.length;
        for (var i = 0; i < lenval; i++) {
            array.push(data.StatementImportItems._latestValue[i]);
        }

        data.StatementImportItems.removeAll();
        ko.utils.arrayPushAll(data.StatementImportItems(), array());
        data.StatementImportItems.valueHasMutated();
        // }
        // this.refreshReconciliationItems(root);
        //  this.refreshStatementItems(root);
        //data.valueHasMutated();
        f.call({});
    }
}
    
