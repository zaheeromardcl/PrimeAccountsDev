
/// <reference path="../../Scripts/typings/knockout/knockout.d.ts" /> 


class DisectionReportParameters {
    StartDate: string;
    EndDate: string;
    ProduceName: string;
    ProduceID: string;
    DepartmentName: string;
    DepartmentID: string;
    ProduceGroupStartID: string;
    ProduceGroupEndID: string;
    ProduceGroupStartName: string;
    ProduceGroupEndName: string;
    SelectedDivisionId: string;
    SelectedCompanyId: string;
    SelectedDepartmentId: string;
}



class DisectionReportViewModel {
    StartDate: KnockoutObservable<string>;
    EndDate: KnockoutObservable<string>;
    ProduceName: KnockoutObservable<string>;
    ProduceID: KnockoutObservable<string>;
    DepartmentName: KnockoutObservable<string>;
    DepartmentID: KnockoutObservable<string>;
    ProduceGroupStartID: KnockoutObservable<string>;
    ProduceGroupEndID: KnockoutObservable<string>;
    ProduceGroupStartName: KnockoutObservable<string>;
    ProduceGroupEndName: KnockoutObservable<string>;
    RequestClick: (any) => void;
    ViewFileClick: (any) => void;
    RunViewFileClick: (any) => void;
    DisectionReport: KnockoutObservable<string>;
    SelectProduce: (any) => void;
    GetProduce: (request: any, ui: any) => any;
    GetRunParameters: (DisectionReportViewModel) => DisectionReportParameters;
    TabContext: KnockoutObservable<AppUserContextModel>;
    SelectAllDept: KnockoutObservable<boolean>;
    SelectAllProduce: KnockoutObservable<boolean>;

    constructor(data) {
        data = data || {};
        this.StartDate = ko.observable(data.StartDate || "");
        this.EndDate = ko.observable(data.StartDate || "");
        this.ProduceName = ko.observable(data.ProduceName || "");
        this.ProduceID = ko.observable(data.ProduceID || "");
        this.DepartmentName = ko.observable(data.DepartmentName || "");
        this.DepartmentID = ko.observable(data.DepartmentID || "");
        this.DisectionReport = ko.observable(data.DisectionReport || "");
        this.ProduceGroupStartID = ko.observable(data.ProduceGroupStartID || "");
        this.ProduceGroupEndID = ko.observable(data.ProduceGroupEndID || "");
        this.ProduceGroupStartName = ko.observable(data.ProduceGroupStartName || "");
        this.ProduceGroupEndName = ko.observable(data.ProduceGroupEndName || "");
        this.TabContext = ko.observable(new AppUserContextModel(data.UserContextModel, true));
        this.SelectAllDept = ko.observable(false);
        this.SelectAllProduce = ko.observable(false);

        this.RequestClick = (data) => {
           // data.DisectionReport("");
          //  var param = ko.toJSON(data);
         //   data.DisectionReport("");
            
            var promise =
                $.ajax({
                    url: "/demo/PrintDisection",
                    cache: false,
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    // data: ko.toJSON(this.ReconciliationItems),
                    data: ko.toJSON(this.GetRunParameters(data)), // POST needs JSON, GET doesn't
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
                       alert(err);
                    });
        };

        this.GetRunParameters = (data) => {
            let lowval: string = "a";
            let highval: string = "x";

            let piparm = new DisectionReportParameters();
           
            piparm.StartDate = data.StartDate();
            piparm.EndDate = data.EndDate();
            piparm.ProduceName = data.ProduceName();
            piparm.ProduceID = data.ProduceID();
            piparm.DepartmentName = data.DepartmentName();
            //piparm.DepartmentID = data.DepartmentID();
            piparm.ProduceGroupStartID = data.ProduceGroupStartID();
            piparm.ProduceGroupStartName = data.ProduceGroupStartName();
            piparm.ProduceGroupEndID = data.ProduceGroupEndID();
            piparm.ProduceGroupEndName = data.ProduceGroupEndName();
            piparm.SelectedCompanyId = data.TabContext().SelectedCompanyId();
            piparm.SelectedDivisionId = data.TabContext().SelectedDivisionId();
            piparm.SelectedDepartmentId = data.TabContext().SelectedDepartmentId();
            piparm.DepartmentID = data.TabContext().SelectedDepartmentId();
            if (data.SelectAllDept() == true) {
                piparm.DepartmentID = null;
            }

            if (data.SelectAllProduce() == true) {
                piparm.ProduceGroupStartName = lowval;
                piparm.ProduceGroupEndName = highval;
            }
            
            return piparm;
        }

        this.ViewFileClick = (data) => {
            //let piparm = new DisectionReportParameters();
            //piparm.StartDate = data.StartDate();
            //piparm.EndDate = data.EndDate();
            //piparm.ProduceName = data.ProduceName();
            //piparm.ProduceID = data.ProduceID();
            //piparm.DepartmentName = data.DepartmentName();
            //piparm.DepartmentID = data.DepartmentID();

            //var param = ko.toJSON(piparm);
            var self = data;
            var promise =
                $.ajax({
                    type: "GET",
                    url: "/demo/DisplayPrintDisection",
                    data: this.GetRunParameters(data),
                    cache: false,
                    success: function (data) {
                        if (data != null) {
                            self.DisectionReport(data);
                            return;
                        }
                    }
                });
        };
        this.RunViewFileClick = (data) => {
            var param = ko.toJSON(data);
            var self = data;
            var promise =
                $.ajax({
                    type: "GET",
                    url: "/demo/RunDisplayPrintDisection",
                    data: this.GetRunParameters(data),
                    success: function (data) {
                        if (data != null) {
                            self.DisectionReport(data);
                            //self.DepartmentID("");
                            return;
                        }
    }
                });
        };

        this.GetProduce = (request, ui) =>
        {
            var text = request.term;
            var self = this;


            if (text === " ")
                return;

            if (text === "")
                return;

            $.ajax({
                type: "GET",
                url: "/api/Produce/AutoComplete/?search=" + text,
                data: {
                    json: "{}",
                    delay: 0.5,
                    search: text
                },
                success: function (data) {
                    if (data == null)
                        return;
                    ui($.map(data, function (language) {
                        return {
                            languageValue: language.Id,
                            label: language.label
                        };
                    }));
                }
            });
        }
    }

    selectProduce = function (event, ui) {
        (ui.item.languageValue);

        var vm = ko.dataFor(event.target);
        vm.ProduceName(ui.item.label);
        vm.ProduceID(ui.item.languageValue);
    };

    getProduce = function (request, ui) {

        var text = request.term;
        var self = this;


        if (text === " ")
            return;

        if (text === "")
            return;

        $.ajax({
            type: "GET",
            url: "/api/Produce/AutoComplete/?search=" + text,
            data: {
                json: "{}",
                delay: 0.5,
                search: text
            },
            success: function (data) {
                if (data == null)
                    return;
                ui($.map(data, function (language) {
                    return {
                        languageValue: language.Id,
                        label: language.label
                    };
                }));
            }
        });

    };

    selectDepartment = function (event, ui) {
        (ui.item.languageValue);

        var vm = ko.dataFor(event.target);
        vm.DepartmentName(ui.item.label);
        vm.DepartmentID(ui.item.languageValue);
    };

    getDepartment = function (request, ui) {

        var text = request.term;
        var self = this;


        if (text === " ")
            return;

        if (text === "")
            return;

        $.ajax({
            type: "GET",
            url: "/api/Department/AutoComplete/?search=" + text,
            data: {
                json: "{}",
                delay: 0.5,
                search: text
            },
            success: function (data) {
                if (data == null)
                    return;
                ui($.map(data, function (language) {
                    return {
                        languageValue: language.Id,
                        label: language.label
                    };
                }));
            }
        });

    };

    selectProduceGroupStart = function (event, ui) {
        (ui.item.languageValue);

        var vm = ko.dataFor(event.target);
        vm.ProduceGroupStartName(ui.item.label);
        vm.ProduceGroupStartID(ui.item.languageValue);
    };

    getProduceGroupStart = function (request, ui) {

        var text = request.term;
        var self = this;


        if (text === " ")
            return;

        if (text === "")
            return;

        $.ajax({
            type: "GET",
            url: "/api/ProduceGroup/AutoCompleteSelect/?search=" + text,
            data: {
                json: "{}",
                delay: 0.5,
                search: text
            },
            success: function (data) {
                if (data == null)
                    return;
                ui($.map(data, function (language) {
                    return {
                        languageValue: language.Id,
                        label: language.label
                    };
                }));
            }
        });

    };

    selectProduceGroupEnd = function (event, ui) {
        (ui.item.languageValue);

        var vm = ko.dataFor(event.target);
        vm.ProduceGroupEndName(ui.item.label);
        vm.ProduceGroupEndID(ui.item.languageValue);
    };

    getProduceGroupEnd = function (request, ui) {

        var text = request.term;
        var self = this;


        if (text === " ")
            return;

        if (text === "")
            return;

        $.ajax({
            type: "GET",
            url: "/api/ProduceGroup/AutoCompleteSelect/?search=" + text,
            data: {
                json: "{}",
                delay: 0.5,
                search: text
            },
            success: function (data) {
                if (data == null)
                    return;
                ui($.map(data, function (language) {
                    return {
                        languageValue: language.Id,
                        label: language.label
                    };
                }));
            }
        });

    };

    onConsignmentItemProduceFocusOut = function (index: number) { // if chars entered then try autofill
        //this.saveState(false);  // save state as normal
        var self = this;
        //var text = this.ConsignmentModel().ConsignmentItems()[index].ProduceName();

        //if (text.length >= 1) {
        //    $.ajax({
        //        type: "GET",
        //        url: "/api/Produce/AutoComplete/?search=" + text,
        //        data: {
        //            json: "{}",
        //            delay: 0.5,
        //            search: text
        //        },
        //        success: function (data) {
        //            if (data == null)
        //                return;
        //            if (data.length === 1) { // only if one autocomplete returned then use it
        //                self.ConsignmentModel().ConsignmentItems()[index].ProduceName(data[0].label);
        //                self.ConsignmentModel().ConsignmentItems()[index].ProduceID(data[0].Id);
        //            }
        //        }
        //    })
        //}
    }
   
}