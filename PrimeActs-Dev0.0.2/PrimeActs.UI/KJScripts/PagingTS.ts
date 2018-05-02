/// <reference path="../Scripts/typings/jquery/jquery.d.ts" />
/// <reference path="../Scripts/typings/knockout/knockout.d.ts" />
class QueryOptions {
    currentPage :   KnockoutObservable<any>;
    totalPages:     KnockoutObservable<any>;
    pageSize:       KnockoutObservable<any>;
    sortField:      KnockoutObservable<any>;
    sortOrder:      KnockoutObservable<any>;

    constructor() {
        this.currentPage = ko.observable();
        this.totalPages = ko.observable();
        this.pageSize = ko.observable();
        this.sortField = ko.observable();
        this.sortOrder = ko.observable();
    }

}

class Paging {
    entities: KnockoutObservableArray<any>;
    queryOptions: QueryOptions;
    buildPreviousClass: any;
    buildNextClass: any;
    SearchObject: any;

//public nextPage: (data, event) => void;
    //public updateResultList: (vals, result) => void;


    constructor(data, SearchObject) {
        //debugger; ///////////////////////////////////////
        this.entities = ko.observableArray(data.Results);
        this.queryOptions = new QueryOptions();
        this.queryOptions.currentPage(data.QueryOptions.CurrentPage);
        this.queryOptions.totalPages(data.QueryOptions.TotalPages);
        this.queryOptions.pageSize(data.QueryOptions.PageSize);
        this.queryOptions.sortField(data.QueryOptions.SortField);
        this.queryOptions.sortOrder(data.QueryOptions.SortOrder);
        this.SearchObject = ko.observable(SearchObject);
        this.buildPreviousClass = ko.computed({
            owner: this,
            read: () => {
                var className = "previous";

                if (this.queryOptions.currentPage() === 1)
                    className += " disabled";

                return className;
            }
        });
        this.buildNextClass = ko.computed({
            owner: this,
            read: () => {
                var className = "next";

                if (this.queryOptions.currentPage() === this.queryOptions.totalPages)
                    className += " disabled";

                return className;
            }
        });
    }

    nextPage = (data, event) => {
        //debugger;
        if (data.Paging().queryOptions.currentPage() < data.Paging().queryOptions.totalPages()) {
            data.Paging().queryOptions.currentPage(data.Paging().queryOptions.currentPage() + 1);

            data.Paging().fetchEntities(data, event);
        }
    };
    updateResultList = (resultList) => {
        //debugger; //2 ts
        this.queryOptions.currentPage(resultList.QueryOptions.CurrentPage);
        this.queryOptions.totalPages (resultList.QueryOptions.TotalPages);
        this.queryOptions.pageSize   ( resultList.QueryOptions.PageSize );
        this.queryOptions.sortField  ( resultList.QueryOptions.SortField);
        this.queryOptions.sortOrder  ( resultList.QueryOptions.SortOrder);
        this.entities(resultList.Results);
    };

    sortEntitiesBy(data, event) {
        //debugger;
        const sortField = $(event.target)[0].dataset["sortField"];
        const sortType = $(event.target)[0].dataset["sortType"];

        if (sortField === data.Paging().queryOptions.sortField() && data.Paging().queryOptions.sortOrder() === "ASC") {
            data.Paging().queryOptions.sortOrder("DESC");
            //temporally
            if (sortType === "date") {
                data.Paging()
                    .entities.sort(function (left, right) {
                        return data.Paging().reverseString(left[sortField]) === data.Paging().reverseString(right[sortField])
                            ? 0
                            : data.Paging().reverseString(left[sortField]) > data.Paging().reverseString(right[sortField]) ? -1 : 1;
                    });
            }
            else if (sortType === "nr"){
                data.Paging()
                    .entities.sort(function (left, right) {
                        return (left[sortField] != undefined && right[sortField] != undefined && left[sortField] === right[sortField])
                            ? 0
                            : ((right[sortField] != undefined) && (left[sortField] == undefined || left[sortField] > right[sortField]) ? -1 : 1);
                    });
            }
            else {
                data.Paging()
                    .entities.sort(function (left, right) {
                        return (left[sortField] != undefined && right[sortField] != undefined && left[sortField].toString().toLowerCase() === right[sortField].toString().toLowerCase())
                            ? 0
                            : ((right[sortField] != undefined) && (left[sortField] == undefined || left[sortField].toString().toLowerCase() > right[sortField].toString().toLowerCase()) ? -1 : 1);
                    });
            }
        }
        else
        {
            data.Paging().queryOptions.sortOrder("ASC");
            if (sortType === "date") {
                //debugger;
                data.Paging()
                    .entities.sort(function(left, right) {
                        return data.Paging().reverseString(left[sortField]) === data.Paging().reverseString(right[sortField])
                            ? 0
                            : (data.Paging().reverseString(left[sortField]) < data.Paging().reverseString(right[sortField]) ? -1 : 1);
                    });
            }
            else if (sortType === "nr") {
                data.Paging()
                    .entities.sort(function (left, right) {
                        return (left[sortField] != undefined && right[sortField] != undefined && left[sortField] === right[sortField])
                            ? 0
                            : ((right[sortField] != undefined) && (left[sortField] == undefined || left[sortField] < right[sortField]) ? -1 : 1);
                    });
            }
            else {
                data.Paging()
                    .entities.sort(function (left, right) {
                        return (left[sortField] != undefined && right[sortField] != undefined && left[sortField].toString().toLowerCase() === right[sortField].toString().toLowerCase())
                            ? 0
                            : ((right[sortField] != undefined) && (left[sortField] == undefined || left[sortField].toString().toLowerCase() < right[sortField].toString().toLowerCase()) ? -1 : 1);
                    });
            }
        }

        data.Paging().queryOptions.sortField(sortField);
//        data.Paging().queryOptions.currentPage(1);
        
        //this.fetchEntities(null, event);
    }

    reverseString(reverseIt) {
        //debugger;
        let dateParts = reverseIt.split(' ')[0].split('/');
        let reverseDate = dateParts[2] + dateParts[1] + dateParts[0] + reverseIt.split(' ')[1];
        return reverseDate;
    }

    reloadPage() {
        let url = `/api/PurchaseInvoice/Index`;

        url += `?sortField=${this.queryOptions.sortField()}`;
        url += `&sortOrder=${this.queryOptions.sortOrder()}`;
        url += `&currentPage=${this.queryOptions.currentPage()}`;
        url += `&pageSize=${this.queryOptions.pageSize()}`;
        //for (var prop in this.SearchObject) {
        //    // important check that this is objects own property 
        //     //not from prototype prop inherited
        //    if (obj.hasOwnProperty(prop)) {
        //      //  alert(prop + " = " + obj[prop]);
        //    }
        //}
        var self = this;
        $.ajax({
            dataType: "json",
            url: url
        }).then(function (data) {
            self.updateResultList(data);
        }).fail(() => {
            $(".body-content").prepend("<div class=\"alert alert-danger\"><strong>Error!</strong> There was an error fetching the data.</div>");
        });
    }

    previousPage(data, event) {


        if (data.Paging().queryOptions.currentPage() > 1) {
            data.Paging().queryOptions.currentPage(data.Paging().queryOptions.currentPage() - 1);
            data.Paging().fetchEntities(data, event);
        }
    }


    fetchEntities(vals, event) {
        let url = `/api/${$(event.target).attr("href")}`;
        
        //url += `?sortField=${vals.Paging().queryOptions.sortField()}`;
        //url += `&sortOrder=${vals.Paging().queryOptions.sortOrder()}`;
        url += `?currentPage=${vals.Paging().queryOptions.currentPage()}`;
        url += `&pageSize=${vals.Paging().queryOptions.pageSize()}`;
        //for (var prop in this.SearchObject) {
        //    // important check that this is objects own property 
        //     //not from prototype prop inherited
        //    if (obj.hasOwnProperty(prop)) {
        //      //  alert(prop + " = " + obj[prop]);
        //    }
        //}
        $.ajax({
            dataType: "json",
            url: url
        }).then(function(data) {
            vals.Paging().updateResultList(data);
        }).fail(() => {
            $(".body-content").prepend("<div class=\"alert alert-danger\"><strong>Error!</strong> There was an error fetching the data.</div>");
        });
    }

    fetchEntitiesWithSearch = function (page, searchObject) {
        var url = "/api/" + page;
        url += "?sortField=" + this.queryOptions.sortField();
        url += "&sortOrder=" + this.queryOptions.sortOrder();
        url += "&currentPage=" + "1";
        url += "&pageSize=" + this.queryOptions.pageSize();
        const obj = []; //self.SearchObject = searchObject;
        for (let prop in searchObject()) {
            //if (self.SearchObject.hasOwnProperty(prop)) {
            if (searchObject()[prop] != null) {
                url += "&" + prop + "=" + searchObject()[prop]();
            }
            this.SearchObject()[prop] = searchObject()[prop]();
            //}
        }
        var self = this;
        $.ajax({
            dataType: "json",
            url: url
        }).done(
            function (data) {
                //debugger;//2 ts
                self.updateResultList(data);
            },
            function () {
                $(".body-content").prepend("<div class=\"alert alert-danger\"><strong>Error!</strong> There was an error fetching the data.</div>");
            });
    };

    buildSortIcon(sortField) {
        //debugger;
        let self = this;
        return ko.pureComputed(function () {
            //debugger;
            var sortIcon = "sort";

            if (self.queryOptions.sortField() === sortField) {
                sortIcon += "-by-alphabet";
                if (self.queryOptions.sortOrder() === "DESC")
                    sortIcon += "-alt";
            }

            return `glyphicon glyphicon-${sortIcon}`;
        });
    }
}
