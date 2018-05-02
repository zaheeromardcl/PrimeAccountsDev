/// <reference path="../../Scripts/typings/jquery/jquery.d.ts" />
/// <reference path="../../Scripts/typings/knockout/knockout.d.ts" />
/// <reference path="../../Scripts/typings/bootstrap-notify/bootstrap-notify.d.ts" />
/// <reference path="../../Scripts/typings/bootbox/bootbox.d.ts" />
///<reference path="ConsignmentItemViewModel.ts" />
/// <reference path="ConsignmentViewModel.ts" />
/// <reference path="../Util/MyCustomOption.ts" />
/// <reference path="createconsignmentmodel.ts" />

class ConsignmentEditViewModel {
    UseLookupTables: LookupTables;
    ConsignmentModel: KnockoutObservable<ConsignmentCreateModel>;
    TabPanelName: KnockoutObservable<string>;

    DoneInit: KnockoutObservable<boolean>;
    //purchaseTypeList = ko.observableArray([]);
    PurchaseTypeList: KnockoutComputed<MyCustomOptionFKExtended[]>;
    PackWtUnitList: KnockoutComputed<MyCustomOptionFKExtended[]>;
    hasConsignmentItems: KnockoutComputed<boolean>;
    ConsignmentItemIndex: KnockoutObservable<number>;
    isValid: KnockoutObservable<boolean>;
    // Persistent State Objects
    TabPanelAlphaName: KnockoutComputed<string>;
    initDone: boolean;
    ChangeObservable: KnockoutComputed<void>;
    HeaderCreated: KnockoutObservable<boolean>;
    defaultItemDepartmentId: KnockoutObservable<string>;

    TabContext: KnockoutObservable<AppUserContextModel>;

    constructor(data, tabPanelName: string, supplierDepartmentID: string, supplierDepartmentName: string, lookupTables: LookupTables) {

        data = data || {};

        this.UseLookupTables = lookupTables;
        this.PackWtUnitList = ko.computed({
            owner: this,
            read: () => {
                var filter = ''; //To do get the CompanyID from drop down at top of page
                //alert(filter);
                /*
                if (!this.DepartmentID() || this.DepartmentID() == '') {
                    filter = 'dd';
                }
        
                return ko.utils.arrayFilter(window.PorterageList(), function (i) {
                    return i.foreignKey == filter;
                });*/
                return this.UseLookupTables.primePackWtUnitList();
            }
        });

        this.ConsignmentItemIndex = ko.observable(-1);
        this.TabPanelName = ko.observable(tabPanelName);
        this.HeaderCreated = ko.observable(false);
        this.defaultItemDepartmentId = ko.observable('');
        this.TabPanelAlphaName = ko.computed({
            read: () => {
                return this.TabPanelName().replace(/[0-9]/g, '');
            }
        });

        this.TabContext = ko.observable(new AppUserContextModel(data.UserContextModel, this.UseLookupTables));

        var consignmentItemModels = [];
        if (data.ConsignmentEditModel.ConsignmentItemEditModels != undefined) {
            for (var i = 0; i < data.ConsignmentEditModel.ConsignmentItemEditModels.length; i++) {

                consignmentItemModels.push(new ConsignmentItemVM(data.ConsignmentEditModel.ConsignmentItemEditModels[i], this.UseLookupTables));
            }
        }
        // Load Data Into the Model              

        this.ConsignmentModel = ko.observable(new ConsignmentCreateModel(data.ConsignmentEditModel, consignmentItemModels));

        this.ConsignmentModel().SupplierDepartmentID(supplierDepartmentID);
        this.ConsignmentModel().SupplierDepartmentName(supplierDepartmentName);

        this.PurchaseTypeList = ko.computed({
            owner: this,
            read: () => {
                var filter = ''; //To do get the CompanyID from drop down at top of page
                //alert(filter);
                /*
                if (!this.DepartmentID() || this.DepartmentID() == '') {
                    filter = 'dd';
                }
        
                return ko.utils.arrayFilter(window.PorterageList(), function (i) {
                    return i.foreignKey == filter;
                });*/
                return this.UseLookupTables.primePurchaseTypeList();
            }
        });
        /*
        if (data.ConsignmentEditModel.purchaseTypeList !== null) {

            var arrayPTN = data.ConsignmentEditModel.purchaseTypeList.reverse();

            for (var i = 0; i < data.ConsignmentEditModel.purchaseTypeList.length; i++) {

                this.purchaseTypeList.push(new MyCustomOption(data.ConsignmentEditModel.purchaseTypeList[i].PurchaseTypeName, data.ConsignmentEditModel.purchaseTypeList[i].PurchaseTypeID));
            }
        }
         */           
        //this.TabPanelName = ko.observable(tabPanelName);
        //this.TabPanelAlphaName = ko.computed({
        //    read: () => {
        //        if (this.TabPanelName().length > 0) return this.TabPanelName().replace(/[0-9]/g, '');  else return "";
        //        }           
        //});

        this.ConsignmentModel().SupplierHasFocus(true);
        this.initDone = false;
        this.ChangeObservable = ko.computed({
            read: () => {
                this.ConsignmentModel().ConsignmentDescription();
                this.ConsignmentModel().ConsignmentReference();
                this.ConsignmentModel().CountryID();
                this.ConsignmentModel().CountryName();
                //this.ConsignmentModel().DepartmentID();
                this.ConsignmentModel().DivisionID();
                this.ConsignmentModel().SupplierID();
                this.ConsignmentModel().SupplierCompanyName();
                this.ConsignmentModel().SupplierDepartmentName();
                this.ConsignmentModel().SupplierDepartmentID();
                this.ConsignmentModel().NoteID();
                this.ConsignmentModel().NoteText();
                this.ConsignmentModel().PortID();
                //this.ConsignmentModel().PortName();
                this.ConsignmentModel().PurchaseTypeDescription();
                this.ConsignmentModel().ReceivedDate();
                this.ConsignmentModel().Commission();
                this.ConsignmentModel().VehicleDetail();
                this.ConsignmentModel().SupplierReference();
                this.ConsignmentModel().PurchaseTypeDescription();
                this.ConsignmentModel().PurchaseTypeID();
                if (this.initDone) {
                    this.saveState(false);
                }
                else {
                    this.initDone = true;
                }
            }
        });

        this.DoneInit = ko.observable(false);

        //ko.bindingHandlers.datepicker = {

        //    init: function (element, valueAccessor, allBindingsAccessor) {
        //        //initialize datepicker with some optional options
        //        var options = allBindingsAccessor().datepickerOptions || {};
        //        //options.defaultDate = new Date();
        //        $(element).datepicker(options);


        //        //handle the field changing
        //        ko.utils.registerEventHandler(element, "change", function () {
        //            var observable = valueAccessor();
        //            observable($(element).datepicker("getDate"));
        //        });

        //        //handle disposal (if KO removes by the template binding)
        //        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
        //            $(element).datepicker("destroy");
        //        });

        //    },
        //    //update the control when the view model changes
        //    update: function (element, valueAccessor) {
        //        var value = ko.utils.unwrapObservable(valueAccessor());
        //        var current = $(element).datepicker("getDate");


        //        //if (value - current !== 0) { DC
        //        $(element).datepicker("setDate", value);
        //        // }
        //    }
        //};

        this.hasConsignmentItems = ko.computed({
            read: () => {
                var countnum = this.ConsignmentModel().ConsignmentItems().length;
                console.log(countnum);
                return this.ConsignmentModel().ConsignmentItems().length > 0 ? true : false;
            }
        });

    }


    showConsignmentDescriptionRequired = function (item) {
        return !this.validateConsignmentDescription(item(), this);
    }

    validateConsignmentDescription = function (val, observable) {
        if (val === null) {
            return val != null;
        }
        return true;
    }

    removeConsignmentItem = function (index: number) {
        if (this.ConsignmentModel().ConsignmentItems().length > 1) {
            this.ConsignmentModel().ConsignmentItems.splice(index, 1);
        }
    }

    onConsignmentItemFocusOut = function (index: number) {
        this.saveState(false);
    }


    onConsignmentItemProduceFocusOut = function (index: number) { // if chars entered then try autofill
        this.saveState(false);  // save state as normal
        var self = this;
        var text = this.ConsignmentModel().ConsignmentItems()[index].ProduceName();

        if (text.length >= 1) {
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
                    if (data.length === 1) { // only if one autocomplete returned then use it
                        self.ConsignmentModel().ConsignmentItems()[index].ProduceName(data[0].label);
                        self.ConsignmentModel().ConsignmentItems()[index].ProduceID(data[0].Id);
                    }
                }
            });
        }
    }

    onOriginItemFocusOut = function (index: number) {
        this.saveState(false);

        var consignmentItem = <ConsignmentItemVM>(this.ConsignmentModel().ConsignmentItems()[index]);
        //this.saveConsignmentItem(this, consignmentItem);
        // If Multiple Consignment Items Enabled then add another line otherwise is single entry        
        if (this.ConsignmentModel().MultipleConsignmentItems() === true) {
            this.addNewConsignmentItem();
        }
    }

    printTest = function () {
        window.open("data:application/print;," + encodeURI("hello\tworld!\ntext mode is cool"));
    }

    updateHeader = function () {
        var PostConsignment = this.ConsignmentModel();
        //  debugger;
        var promise =
            $.ajax({
                url: "/Consignment/UpdateConsignmentDates/",
                cache: false,
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: ko.toJSON(PostConsignment),
                dataType: "json",
                //data: ko.toJSON(PostConsignment),
                success: function (PostConsignment) { }
            });
    }

    saveAllConsignmentItems = function (subscriberReplaceTab) {
        // alert();
        //debugger;
        var self = this;
        var totalErrorCount = 0;

        var consignmentItemsCount = this.ConsignmentModel().ConsignmentItems().length;
        for (var i = consignmentItemsCount - 1; i >= 0; i--) {
            var consignmentItem = <ConsignmentItemVM>this.ConsignmentModel().ConsignmentItems()[i];

            //this.ConsignmentModel().ConsignmentItems()[i].ConsignmentItemArrivals()[0].Quantity(this.ConsignmentModel().ConsignmentItems()[i].ExpectedQuantity());

            if (i == consignmentItemsCount - 1   // last item
                && consignmentItemsCount > 1     // ticket has more than 1 ticket items, and its not edited
            ) {
                continue;
            }

            consignmentItem.Errors = ko.validation.group(consignmentItem);
            var errorCount = consignmentItem.Errors().length;
            if (errorCount != 0) {
                consignmentItem.ShowErrors(true);
                consignmentItem.Errors.showAllMessages(true);
            }
            totalErrorCount += errorCount;
        }

        if (totalErrorCount > 0) {
            return;// DC 09/09/2016 should be enabled
        }

        var consignmentItemModels = [];
        for (var consignmentItemEditModel of this.ConsignmentModel().ConsignmentItems) {
            if (consignmentItemEditModel.SupplierDepartmentID != "") {
                consignmentItemModels.push(new ConsignmentItemVM(consignmentItemEditModel, this.UseLookupTables));
            }
        }

        // debugger;
        var post_consignment_items = this.ConsignmentModel().ConsignmentItems();
        var tabPanelName = self.TabPanelName();
        var id = self.ConsignmentModel().ConsignmentID();

        self.updateHeader();

        var promise =
            $.ajax({
                url: "/api/Consignment/EditConsignmentItem/",
                cache: false,
                type: "POST",
                contentType: "application/json; charset=utf-8",
                //data: ko.toJSON(consignmentItem),
                data: ko.toJSON(post_consignment_items),
                dataType: "json",
                success: function (result) {
                    //alert("saved");
                    var redirectTarget = "ConsignmentDetails";


                    self.saveState(false);
                    subscriberReplaceTab.notifySubscribers({
                        PanelName: tabPanelName,
                        NewPanelName: redirectTarget,
                        UriParam: id
                    }, "save");
                },
                error: function (jqXHR, textStatus, errorThrown) {

                }
            }).fail(
                function (xhr, textStatus, err) {
                    //alert(err);
                });
    }

    saveConsignmentItem = function (data: ConsignmentCreateModel, consignmentItem: ConsignmentItemVM) {

        var post_consignment_items = this.ConsignmentModel().ConsignmentItems();
        var promise =
            $.ajax({
                url: "/api/Consignment/CreateConsignmentItem/",
                cache: false,
                type: "POST",
                contentType: "application/json; charset=utf-8",
                //data: ko.toJSON(consignmentItem),
                data: ko.toJSON(post_consignment_items),
                dataType: "json",
                success: function (result) {
                    //data.testCreateItem();
                },
                error: function (jqXHR, textStatus, errorThrown) {

                }
            }).fail(
                function (xhr, textStatus, err) {
                    //alert(err);
                });
        promise.done(function (data) {

        });
    }

    setStateOnServer = function (data: string, contentType: string, controllerState: string, uriParam: string, initial: boolean) {

        var panelId: string = this.TabPanelName().match(/\d+/g).join([]);
        $.ajax({
            type: 'POST',
            url: "/api/TabPanel",
            dataType: 'application/json',
            data: {
                id: panelId, jsondata: data, panelname: this.TabPanelName(), content_type: contentType, controller_state: controllerState, uriParam: uriParam, initial: initial
            },
            success: function (data) {
            },
            error: function (xhr) {
            }
        });
    }

    getState = function () {
        var self = this;
        var panelId: string = this.TabPanelName().match(/\d+/g).join([]);
        return $.ajax({
            type: 'GET',
            url: "/api/TabPanel",
            dataType: 'json',
            data: { "id": panelId, "name": this.TabPanelName() },
            success: function (data) {
                return data;
            },
            error: function (xhr) {
                console.log("failure in GET from State");
                console.log(xhr);
                return null;
            }
        });
    }

    pageLoadState = function () {

        var self = this;
        this.getState()
            .done(function (data) {
                if (data == null || data == "") {
                    self.saveState(true);
                }
                else {
                    var jsonData = JSON.parse(data);
                    self.mapRestoredState(jsonData);
                }
            });
        //this.ConsignmentModel().SupplierHasFocus(true);
    }

    saveState = function (initial: boolean) {
        var data = ko.toJSON(this.ConsignmentModel());
        //console.log(this.ConsignmentModel().ConsignmentItems().length);

        var tab_alpha_name = this.TabPanelAlphaName();
        var b_save_state_success = true;

        this.setStateOnServer(data, tab_alpha_name, "ConsignmentEdit", this.ConsignmentModel().ConsignmentID(), initial);
    }

    mapRestoredState = function (data) {

        var consignmentItemModels = [];
        var itemIndexer = 0;
        if (data.hasOwnProperty('ConsignmentItems')) {
            for (var consignmentItemEditModel of data.ConsignmentItems) {
                var newConsignmentItemEditModel = new ConsignmentItemVM(consignmentItemEditModel, this.UseLookupTables);
                // consignmentItemModels.push(new ConsignmentItem(consignmentItemEditModel));

                //for (var savedArrivalItem of data.ConsignmentItems[itemIndexer].ConsignmentItemArrivals) {
                //    // debugger;
                //    var arrivaldate_format_check = "01/01/2016";//moment(savedArrivalItem.ConsignmentArrivalDate);
                //    //if (arrivaldate_format_check.isValid()) {
                //    //    var date_format_fix = arrivaldate_format_check.format('DD/MM/YYYY');
                //    //    savedArrivalItem.ConsignmentArrivalDate = date_format_fix;
                //    //} else { // if bad date
                //    //    var now_date = moment();
                //    //    savedArrivalItem.ConsignmentArrivalDate = now_date.format('DD/MM/YYYY'); // set to today
                //    //}

                //    var newConsignmentItemArrival = new ConsignmentItemArrival(savedArrivalItem); // Create ConsignmentArrivalItemObject
                //    debugger;
                //    newConsignmentItemEditModel.ConsignmentItemArrivals.push(newConsignmentItemArrival);
                //}
                consignmentItemModels.push(newConsignmentItemEditModel);
                itemIndexer = itemIndexer + 1;
            }
            //this.GetDBConsignmentItemModel(data.ConsignmentID);
        }

        // ## Fix Date Formating on Mppings back ##

        // Received Date

        //date_format_check = moment(data.ReceivedDate);
        //if (date_format_check.isValid()) {
        //    var date_format_fix = date_format_check.format('DD/MM/YYYY');
        //    data.ReceivedDate = date_format_fix;
        //} else { // if bad date
        //    var now_date = moment();
        //    data.ReceivedDate = now_date.format('DD/MM/YYYY'); // set to today
        //}

        // Despatch Date        
        //var date_format_check = moment(data.DespatchDate);
        //if (date_format_check.isValid()) {
        //    var date_format_fix = date_format_check.format('DD/MM/YYYY');
        //    data.DespatchDate = date_format_fix;
        //} else { // if bad date 
        //    var now_date = moment();
        //    now_date.add(1, 'd'); // add 1 day
        //    data.DespatchDate = now_date.format('DD/MM/YYYY'); // format
        //}



        //this.initDone = false;
        this.ConsignmentModel(new ConsignmentCreateModel(data, consignmentItemModels));

    }

    stopEdit = function () {
        this.tedit(true);
    };

    pstopEdit = function () {
        this.pedit(false);
    };

    getDepartments = function (request, ui) {
        var text = request.term;
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
                        label: language.label,
                        //value: language.value
                    };
                }));
            }
        });
    };

    SelectDepartment = function (event, ui) {
        (ui.item.languageValue);

        //(ui.item.languageValue);
        //var id = self.ConsignmentItem.Id();
        //var depID = ui.item.languageValue;
        //var dep = ui.item.label;
        //var length = self.ConsignmentItems().length;

        //self.ConsignmentItems()[id - 2].DepartmentID(depID);
        //self.ConsignmentItems()[id - 2].DepartmentName(dep);
        var vm = ko.dataFor(event.target);
        vm.DepartmentID(ui.item.languageValue);
        vm.DepartmentName(ui.item.label);
    };


    SelectPurchaseType = function (event, ui) {
        var vm = ko.dataFor(event.target);
        vm.ConsignmentModel().PurchaseTypeID(ui.item.languageValue);
    };

    SelectPackWtUnit = function (event, ui) {

        //console.log("Select Pack Unit");
        var vm = ko.dataFor(event.target);
        //vm.ConsignmentModel().PurchaseTypeID(ui.item.languageValue);
    };

    RowSelectPackWtUnit = function (index) {

        //console.log("Select Pack Unit");
        //var vm = ko.dataFor(event.target);
        //vm.ConsignmentModel().PurchaseTypeID(ui.item.languageValue);
    };

    SelectPorterage = function (event, ui) {
        var vm = ko.dataFor(event.target);
        //vm.ConsignmentModel().PurchaseTypeID(ui.item.languageValue);
    };

    selectPort = function (event, ui) {
        (ui.item.languageValue);
        var vm = ko.dataFor(event.target);
        vm.ConsignmentModel().PortID(ui.item.languageValue);
        vm.ConsignmentModel().PortName(ui.item.label);
    };

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



    getPorts = function (request, ui) {
        var text = request.term;
        $.ajax({
            type: "POST",
            url: '/Port/AutoComplete/?search=' + text,
            data: {
                json: '{}',
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

    getSupplier = function (request, ui) {

        var text = request.term;
        $.ajax({
            type: "POST",
            url: "/Supplier/AutoComplete/?search=" + text,
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
                        //label: language.label.substring(0, language.label.length - 13),
                        label: language.label,
                        value: language.value,
                        countryName: language.CountryName,
                        countryID: language.CountryID
                    };
                }));
            }
        });

    };

    selectSupplier = function (event, ui) {

        //this.Consignment.SupplierDepartmentID(ui.item.languageValue);
        var vm = ko.dataFor(event.target);

        vm.ConsignmentModel().SupplierDepartmentID(ui.item.languageValue);
        vm.ConsignmentModel().SupplierDepartmentName(ui.item.label);
        vm.ConsignmentModel().CountryName(ui.item.countryName);
        vm.ConsignmentModel().CountryID(ui.item.countryID);

        var data = ui.item.value;
        // Populate mode variables here and then manipulate the string the user sees
        var length = data.length;
        //var sdID = data.Id;//data[0].label.substring(length - 36, length);
        //var str = data.substring(1, length - 13);
        var str = data.substring(0, length - 13);
        var commHand = data.substring(length - 12, length);
        var arr = commHand.split("-");

        var comm = arr[0].trim();
        var hand = arr[1].trim();

        //this.Consignment.Commission(comm);
        // this.Consignment.Handling(hand);

        vm.ConsignmentModel().Commission(comm);
        vm.ConsignmentModel().Handling(hand);

        //Trick here change the selected value to the label.
        ui.item.value = str; //ui.item.label;
        if (this.initDone) {
            this.saveState(false);
        }
    };

    getCountry = (request, ui) => {
        var text = request.term;
        if (text === " ")
            return;
        if (text === "")
            return;
        /*$.ajax({
            type: "GET",
            url: "/api/Country/AutoComplete/?search=" + text,
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
                        label: language.label,
                        value: language.value
                    };
                }));
            }
        });*/
        var filtered = ko.utils.arrayFilter(this.UseLookupTables.primeCountryList(), (i) => {
            return i.itemName.toLowerCase().substring(0, text.length) == text.toLowerCase();
        });
        ui($.map(filtered, function (countries) {
            return {
                languageValue: countries.itemID,
                label: countries.itemName,
                value: countries.itemName
            };
        }));
    };

    SelectCountry = function (event, ui) {
        (ui.item.languageValue);
        var vm = ko.dataFor(event.target);
        vm.OriginCountryID(ui.item.languageValue);
        vm.CountryName(ui.item.label);
        //(ui.item.languageValue);

        //var id = self.SelectedItem._latestValue.Id();
        //var countryID = ui.item.languageValue;
        //var country = ui.item.label;
        //var length = self.ConsignmentItems().length;
        //if (id > 0) {
        //    self.ConsignmentItems()[id - 1].OriginCountryID(countryID);
        //    self.ConsignmentItems()[id - 1].CountryName(country);

        //    if (self.ConsignmentItems()[id - 1].CountryName._latestValue == "United Kingdom") {

        //        self.ConsignmentItems()[id - 1].IsCountry(false);

        //    } else {

        //        self.ConsignmentItems()[id - 1].IsCountry(true);
        //    }
        //}

    };

    Reset = function () {

    }

    convertDataURIToBinary = function (dataURI) {
        var BASE64_MARKER = ";base64,";
        var base64Index = dataURI.indexOf(BASE64_MARKER) + BASE64_MARKER.length;
        var base64 = dataURI.substring(base64Index);
        var raw = window.atob(base64);
        var rawLength = raw.length;
        var array = new Uint8Array(new ArrayBuffer(rawLength));

        for (var i = 0; i < rawLength; i++) {
            array[i] = raw.charCodeAt(i);
        }
        return array;
    };

    utf8_to_b64 = function (str) {

        //return window.btoa(unescape(encodeURIComponent(str)));  TYPESCRIPT NOT LIKE

    }

    create = function (data: CreateConsignmentViewModel, subscriberReplaceTab) {
        var self = this;
        var fCon = "";

        //Encoding in BASE64 does not work in Chrome - we need another way to do this

        //DC TBD
        //for (var i = 0; i < this.ConsignmentModel().FileEditModels._latestValue.length; i++) {

        //    fCon = this.ConsignmentModel().FileEditModels._latestValue[i].FileContent;
        //   // this.ConsignmentModel().FileEditModels._latestValue[i].FileContent = utf8_to_b64(fCon);

        //}

        var PostConsignment = this.ConsignmentModel();
        var parmConsignmentID = this.ConsignmentModel().ConsignmentID();

        var errors = ko.validation.group(PostConsignment, { deep: true, observable: false });
        if (errors().length > 0) {
            errors.showAllMessages();
            return;

        } else {

            if (PostConsignment.ConsignmentDescription() != "" && PostConsignment.DespatchDate != "" && PostConsignment.ConsignmentReference() != "" && PostConsignment.VehicleDetail() != "") {

                //this.HeaderCreated(true); // after set other Posts are update

                var promise =
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
                            // window.location.href = "/Consignment/ConsignmentItem/" + parmConsignmentID;
                            data.addNewConsignmentItem();

                            //data.GetDBConsignmentItemModel(data.ConsignmentModel().ConsignmentID());
                            data.saveState(false);
                        },
                        error: function (jqXHR, textStatus, errorThrown) {

                        }
                    }).fail(
                        function (xhr, textStatus, err) {
                            //alert(err);
                        });
                promise.always(function (data) {

                })


            } else {

                //alert('Please Enter All the Values !!');
            }

        }
    };

    //getNewConsignmentItem = function () {
    //    var promise =
    //        $.ajax({
    //            url: "/Consignment/CreateConsignment/",
    //            cache: false,
    //            type: "POST",
    //            contentType: "application/json; charset=utf-8",
    //            data: ko.toJSON(PostConsignment),
    //            dataType: "json",              
    //            success: function (PostConsignment) {
    //                //error in line below
    //                //  window.location.href = "/Consignment/ConsignmentItem/" + parmConsignmentID;
    //                data.addNewConsignmentItem();
    //            },
    //            error: function (jqXHR, textStatus, errorThrown) {
    //                
    //            }
    //        }).fail(
    //            function (xhr, textStatus, err) {
    //                //alert(err);
    //            });
    //    promise.done(function (data) {

    //    })
    //}

    guid = function () {
        return "xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx".replace(/[xy]/g, function (c) {
            var r = Math.random() * 16 | 0, v = c == "x" ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
    }

    scrollToBottom = function () {

        var documentHeight = document.documentElement.offsetHeight;
        var viewportHeight = window.innerHeight;
        window.scrollTo(0, documentHeight - viewportHeight);
    }

    addNewConsignmentItem = function () {

        // ? go to Controller to get New?
        var consignment_item = new ConsignmentItemVM(null, this.UseLookupTables);
        var new_guid = this.guid();
        consignment_item.ConsignmentItemID(new_guid);
        consignment_item.ConsignmentID = this.ConsignmentModel().ConsignmentID();
        //consignment_item.PorterageID("00760000-0000-0001-0006-017528069450"); // dc testing
        // consignment_item.PackWtUnitID("00760000-0000-0001-0006-817528069450"); // dc testing
        consignment_item.ProduceFocused(true);
        consignment_item.CountryName(this.ConsignmentModel().CountryName());
        consignment_item.OriginCountryID(this.ConsignmentModel().CountryID());
        consignment_item.DepartmentID(this.ConsignmentModel().DefaultDepartmentID());
        consignment_item.DepartmentName(this.ConsignmentModel().DefaultDepartmentName());
        consignment_item.DepartmentCode(this.ConsignmentModel().DepartmentCode());

        consignment_item.Porterage("0");
        consignment_item.Id = this.ConsignmentItemIndex();
        //Consignment Item Arrival

        var new_guid_arrival = this.guid();
        var consignment_item_arrival = new ConsignmentItemArrival(null);
        consignment_item_arrival.ConsignmentArrivalDate("");//this.ConsignmentModel().ReceivedDate());
        consignment_item_arrival.ConsignmentItemID(consignment_item.ConsignmentItemID());
        consignment_item_arrival.ConsignmentItemArrivalID(new_guid_arrival);
        consignment_item.ConsignmentItemArrivals().push(consignment_item_arrival);

        this.ConsignmentModel().ConsignmentItems.push(consignment_item);
        var new_index_tmp = this.ConsignmentItemIndex();
        new_index_tmp = ++new_index_tmp;
        this.ConsignmentItemIndex(new_index_tmp);
        this.HeaderCreated(true);
        //this.ConsignmentModel().SupplierHasFocus(false);
        this.scrollToBottom();
    }

    addNewArrivalItem = function (parentindex: number) { // From Knockout UI
        var new_guid_arrival = this.guid();
        var consignment_item_arrival = new ConsignmentItemArrival(null);
        consignment_item_arrival.ConsignmentArrivalDate("");
        consignment_item_arrival.ConsignmentItemID(this.ConsignmentModel().ConsignmentItems()[0].ConsignmentItemID());
        consignment_item_arrival.ConsignmentItemArrivalID(new_guid_arrival);
        this.ConsignmentModel().ConsignmentItems()[parentindex].ConsignmentItemArrivals().push(consignment_item_arrival);
        this.ConsignmentModel().ConsignmentItems()[parentindex].ConsignmentItemArrivals.valueHasMutated(); // notify Parent Array
    }

    removeArrivalItem = function (index: number, parentindex: number) {
        //  //debugger;
        if (this.ConsignmentModel().ConsignmentItems()[parentindex].ConsignmentItemArrivals().length > 1) {
            this.ConsignmentModel().ConsignmentItems()[parentindex].ConsignmentItemArrivals.splice(index, 1);
            this.ConsignmentModel().ConsignmentItems()[parentindex].ConsignmentItemArrivals.valueHasMutated(); // notify Parent Array
        }
    }
    //addNewArrivalItem = function (data) {

    //    // ? go to Controller to get New?
    //    var consignment_item = new ConsignmentItemArrival (data);
    //    var new_guid = this.guid();
    //    consignment_item.ConsignmentItemID(new_guid);       
    //    var cur_arrival_index = this.ConsignmentItemIndex();
    //    this.ConsignmentModel().ConsignmentItems[cur_arrival_index].ConsignmentItemArrivals.push(consignment_item);

    //    //new_index_tmp = ++new_index_tmp;
    //    //this.ConsignmentItemIndex(new_index_tmp);
    //    this.HeaderCreated(true);
    //    //this.ConsignmentModel().SupplierHasFocus(false);
    //    //this.scrollToBottom();
    //}

    // Cancel product details
    cancel = function () {
        if (this.ConsignmentModel() === null) {

            // window.location.href = "/Consignment/CreateConsignment/";
            // Notify Panels to Clear Panel
        } else {

            this.Reset();
            // window.location.href = "/Consignment/CreateConsignment/";
            // Notify Panels to Clear Panel
        }

    };
}

