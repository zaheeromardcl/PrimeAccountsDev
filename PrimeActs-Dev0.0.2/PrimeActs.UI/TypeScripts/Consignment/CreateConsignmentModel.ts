/// <reference path="../../Scripts/typings/jquery/jquery.d.ts" />
/// <reference path="../../Scripts/typings/knockout/knockout.d.ts" />
/// <reference path="../../Scripts/typings/bootstrap-notify/bootstrap-notify.d.ts" />
/// <reference path="../../Scripts/typings/bootbox/bootbox.d.ts" />
///<reference path="ConsignmentItemViewModel.ts" />
/// <reference path="ConsignmentViewModel.ts" />
/// <reference path="../Util/SelectOption.ts" />
/// <reference path="../util/mycustomoption.ts" />


class ConsignmentCreateModel extends ConsignmentViewModel {
    ShowErrors: KnockoutObservable<boolean>;
    Errors: KnockoutValidationErrors;
    tedit = ko.observable(false);
    pedit = ko.observable(false);
    HeaderIsValid: KnockoutObservable<boolean>;
    SupplierHasFocus: KnockoutObservable<boolean>;

    AttachFilesVisible: KnockoutObservable<boolean>;
    NoMoreAttachments: KnockoutObservable<boolean>;

    UploadFolder: KnockoutObservable<string>;
    UploadedFileNames: KnockoutObservableArray<MyCustomOption>;
    UploadedFileNamesDeleted: KnockoutObservableArray<MyCustomOption>;
        
    //Errors: KnockoutValidationErrors;

    FilenamesWithURLs: KnockoutObservableArray<MyCustomOption>;

    constructor(data, consignmentItems: ConsignmentItemVM[]) {
        super(data, consignmentItems);
        this.Errors = ko.validation.group(this);
        this.ShowErrors = ko.observable(false);
        this.SupplierHasFocus = ko.observable(true);

        this.AttachFilesVisible = ko.observable(false);
        this.NoMoreAttachments = ko.observable(false);
        this.UploadFolder = ko.observable(data.UploadFolder);
        this.UploadedFileNames = ko.observableArray([]);
        this.UploadedFileNamesDeleted = ko.observableArray([]);
        //this.Errors = ko.validation.group(this);
        this.FilenamesWithURLs = ko.observableArray<MyCustomOption>([]);
        //debugger;
        if (data.FilenamesWithURLs != undefined)
            for (let i = 0; i < data.FilenamesWithURLs.length; i++) {
                this.FilenamesWithURLs.push(new MyCustomOption(data.FilenamesWithURLs[i].display, data.FilenamesWithURLs[i].itsValue));
            }
    }

    serverErrors = ko.observableArray([]);
    showError = function (item) {

        if ((item == undefined || !item.isValid()) && this.ShowErrors()) {
            console.log("showError -true");
            return true;
        }
        return false;
    }

    validateGuid = function (val) {
        return val != undefined && val != null && val != "" && val != "00000000-0000-0000-0000-000000000000";
    }

    showConsignmentDescriptionRequired = function (item) {
        return !this.validateConsignmentDescription(item(), this);
    }

    validateConsignmentDescription = function (val, observable) {
        if (val == undefined || val == null || val == "")
            return false;
        return true;
    }

    showSupplierReferenceRequired = function (item) {
        return !this.validateSupplierReference(item(), this);
    }

    validateSupplierReference = function (val, observable) {
        if (val == undefined || val == null || val == "")
            return false;
        return true;
    }

    showSupplierDepartmentNameRequired = function (item) {
        return !this.validateSupplierDepartmentName(item(), this);
    }

    validateSupplierDepartmentName = function (val, observable) {
        if (val == undefined || val == null || val == "")
            return false;
        return true;
    }

    addFileName = function (fileID, fileName) {
        //debugger;
        this.UploadedFileNames.push(new MyCustomOption(fileName, fileID));

        for (let i = 0; i < this.UploadedFileNamesDeleted().length; i++) {
            if (this.UploadedFileNamesDeleted()[i].display === fileName) {
                this.UploadedFileNamesDeleted.remove(this.UploadedFileNamesDeleted()[i]);
            }
        }
    }

    removeFileName = function (fileName) {
        //debugger;
        for (let i = 0; i < this.UploadedFileNames().length; i++) {
            if (this.UploadedFileNames()[i].display === fileName) {
                this.UploadedFileNamesDeleted.push(new MyCustomOption(fileName, this.UploadedFileNames()[i].itsValue));
                this.UploadedFileNames.remove(this.UploadedFileNames()[i]);
            }
        }

    }
    
    //showProduceNameRequired = function (item) {
    //    return !this.validateProduceName(item(), this);
    //}

    //validateProduceName = function (val, observable) {
    //    if (val == undefined || val == null || val == "")
    //        return false;
    //    return true;
    //}

}

class CreateConsignmentViewModel {
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
    IsCreated: KnockoutObservable<boolean>;
    ConsignmentReferenceMissing: KnockoutComputed<boolean>;
    ConsignmentReferencePlaceHolder: KnockoutComputed<string>;

    arrTemp: string[];

    TabContext: KnockoutObservable<AppUserContextModel>;

    constructor(data, uploadGuidFolder, tabPanelName: string, supplierDepartmentID: string, supplierDepartmentName: string, lookupTables: LookupTables) {

        data = data || {};
        this.UseLookupTables = lookupTables;
        this.ConsignmentItemIndex = ko.observable(-1);
        this.TabPanelName = ko.observable(tabPanelName);
        this.HeaderCreated = ko.observable(false);
        this.defaultItemDepartmentId = ko.observable('');
        this.TabPanelAlphaName = ko.computed({
            read: () => {
                return this.TabPanelName().replace(/[0-9]/g, '');
            }
        });

        this.TabContext = ko.observable(new AppUserContextModel(data, this.UseLookupTables));
        this.TabContext().AddRequiredPermission('00760000-0000-0006-8386-130057526562');
        this.TabContext().AddRequiredPermission('5BEF290C-6B5F-439D-B32E-03D9F0FF4FCC');

        var consignmentItemModels = [];

        for (var i = 0; i < data.ConsignmentEditModel.ConsignmentItemEditModels.length; i++) {

            consignmentItemModels.push(new ConsignmentItemVM(data.ConsignmentEditModel.ConsignmentItemEditModels[i], this.UseLookupTables));
        }

        // Load Data Into the Model
        this.ConsignmentModel = ko.observable(new ConsignmentCreateModel(data.ConsignmentEditModel, consignmentItemModels));
       // debugger;
        /*
        var dep1 = this.ConsignmentModel().SelectedDepartmentId();
        var dep2 = this.TabContext().SelectedDepartmentId();
        if (dep2 != dep1)
        {
            this.TabContext().SelectDepartment(this.TabContext().SelectedDepartmentId());
        }
        else
        {
            this.ConsignmentModel().SelectedDepartmentId(this.TabContext().SelectedDepartmentId());
        }
        */

       // this.TabContext().SelectDepartment('76000100-0000-0070-9204-000068336078');

        //this.ConsignmentModel().SelectedCompanyId(this.TabContext().SelectedCompanyId());
        
        this.ConsignmentModel().DivisionID(this.TabContext().SelectedDivisionId());
        this.ConsignmentModel().UploadFolder(uploadGuidFolder);
        this.ConsignmentModel().SupplierDepartmentID(supplierDepartmentID);
        this.ConsignmentModel().SupplierDepartmentName(supplierDepartmentName);
        
        this.PackWtUnitList = ko.computed({
            owner: this,
            read: () => {
                var filter = this.TabContext().SelectedCompanyId();
                //alert(filter);
                return ko.utils.arrayFilter(this.UseLookupTables.primePackWtUnitList(), function (i) {
                    return i.foreignKey == filter;
                });
            }
        });
        this.PurchaseTypeList = ko.computed({
            owner: this,
            read: () => {
                var filter = this.TabContext().SelectedCompanyId();
                return ko.utils.arrayFilter(this.UseLookupTables.primePurchaseTypeList(), function (i) {
                    return i.foreignKey == filter;
                });
            }
        });

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
        this.IsCreated = ko.computed({
            read: () => {
                return this.ConsignmentModel().ConsignmentReference() != "";
            }
        });
        this.ConsignmentReferenceMissing = ko.computed({
            read: () => {
                return this.ConsignmentModel().ConsignmentReference() == "" && !this.TabContext().SelectedDivisionAutoGenerateConsignmentReference();
            }
        });
        this.ConsignmentReferencePlaceHolder = ko.computed({
            read: () => {
                if (this.TabContext().SelectedDivisionAutoGenerateConsignmentReference()){
                    return "Autonumber";
                }
                return "";
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

    onConsignmentFocusOut = function (data) {
        //debugger;
        this.updateConsingment();
    }

    updateConsingment = function () {
        //debugger;

        let self = this;

        if (self.ConsignmentModel().ConsignmentID() === "") return;
        //if (self.ConsignmentModel().ConsignmentItems().length === 0) return;
        // if nothing has changed then return
        if (!self.ConsignmentModel().IsDirty()) {
            //debugger;
            alert(self.ConsignmentModel().DespatchDate());
            return;
        }

        if (self.ConsignmentModel().IsSaving() || (self.ConsignmentModel().ConsignmentItems().length === 0)) {
            return;
        }

        if (self.ConsignmentModel().ConsignmentID() != "" && self.ConsignmentModel().DespatchDate != "" && self.ConsignmentModel().SupplierReference() != "" && self.ConsignmentModel().SelectPurchaseType() != "") {
            var jsonToSend = {
                ConsignmentID: self.ConsignmentModel().ConsignmentID(),
                DespatchDate: self.ConsignmentModel().DespatchDate(),
                SupplierReference: self.ConsignmentModel().SupplierReference(),
                PurchaseTypeID: self.ConsignmentModel().SelectPurchaseType()
            }

            var promise =
                $.ajax({
                    url: "/Consignment/UpdateConsignmentHeader/",
                    cache: false,
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify(jsonToSend)
                });
            promise.done(function (res) {
                //debugger;
                self.ConsignmentModel().IsDirty(false);
            });
        }
    }

    onConsignmentItemFocusOut = function (index: number) {
        this.saveState(false);
    }


    onConsignmentItemProduceFocusOut = (index: number) => { // if chars entered then try autofill
        this.saveState(false);  // save state as normal
        var self = this;
        var text = this.ConsignmentModel().ConsignmentItems()[index].ProduceName();
        if (text.length >= 1) {
            $.ajax({
                type: "GET",
                url: "/api/Produce/AutoComplete/?divisionid=" + self.TabContext().SelectedDivisionId() + "&search=" + text,
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
            })
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
                url: "/api/Consignment/CreateConsignmentItem/",
                cache: false,
                type: "POST",
                contentType: "application/json; charset=utf-8",
                //data: ko.toJSON(consignmentItem),
                data: ko.toJSON(post_consignment_items),
                dataType: "json",
                success: function (result) {
                    // debugger;
                    //this.updateHeader();           
                    //data.testCreateItem();
                    //window.location.href = "/Consignment/Details/" + result[0].ConsignmentID;
                    var redirectTarget = "ConsignmentDetails";
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
        // debugger;
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

        })
    }

    setStateOnServer = function (data: string, contentType: string, controllerState: string, initial: boolean, onSuccess) {

        var panelId: string = this.TabPanelName().match(/\d+/g).join([]);
        $.ajax({
            type: 'POST',
            url: "/api/TabPanel",
            dataType: 'application/json',
            data: {
                id: panelId, jsondata: data, panelname: this.TabPanelName(), content_type: contentType, controller_state: controllerState, initial: initial
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
        // 
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
        this.ConsignmentModel().SupplierHasFocus(true);
    }

    saveState = function (initial: boolean) {
      //  debugger;
        var data = ko.toJSON(this.ConsignmentModel());
        //console.log(this.ConsignmentModel().ConsignmentItems().length);
        var currentContext = this.TabContext();
        var currentCompanyId = currentContext.SelectedCompanyId();
        var currentDivisionId = currentContext.SelectedDivisionId();
        var currentDepartmentId = currentContext.SelectedDepartmentId();
        this.ConsignmentModel().SelectedDivisionId(currentCompanyId);
        this.ConsignmentModel().SelectedCompanyId(currentDivisionId);
        this.ConsignmentModel().SelectedDepartmentId(currentDepartmentId);
        var tab_alpha_name = this.TabPanelAlphaName();
        var b_save_state_success = true;

        this.setStateOnServer(data, tab_alpha_name, "Create", initial, b_save_state_success);
    }

    mapRestoredState = function (data) {

        var consignmentItemModels = [];
        var itemIndexer = 0;
        //debugger;
        if (data.hasOwnProperty('ConsignmentItems')) {
            for (var consignmentItemEditModel of data.ConsignmentItems) {
                var newConsignmentItemEditModel = new ConsignmentItemVM(consignmentItemEditModel, this.UseLookupTables);
                // consignmentItemModels.push(new ConsignmentItem(consignmentItemEditModel));

                //for (var savedArrivalItem of data.ConsignmentItems[itemIndexer].ConsignmentItemArrivals) {
                //   // debugger;
                //    var arrivaldate_format_check = "01/01/2016";//moment(savedArrivalItem.ConsignmentArrivalDate);
                //    //if (arrivaldate_format_check.isValid()) {
                //    //    var date_format_fix = arrivaldate_format_check.format('DD/MM/YYYY');
                //    //    savedArrivalItem.ConsignmentArrivalDate = date_format_fix;
                //    //} else { // if bad date
                //    //    var now_date = moment();
                //    //    savedArrivalItem.ConsignmentArrivalDate = now_date.format('DD/MM/YYYY'); // set to today
                //    //}

                //    var newConsignmentItemArrival = new ConsignmentItemArrival(savedArrivalItem); // Create ConsignmentArrivalItemObject

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
       
        var dep1 = this.ConsignmentModel().SelectedDepartmentId();
       // this.TabContext().SelectDepartment('76000100-0000-0070-9204-000068336078');
        this.TabContext().SelectDepartment(dep1);
    }

    uploadImages = function (files, data: CreatePurchaseInvoiceViewModel) {

        if (files.length > 0) {
            var fdata = new FormData();
            for (var x = 0; x < files.length; x++) {
                fdata.append("file" + x, files[x]);
            }

            data.PurchaseInvoice().Files(fdata);
        }
        else {
            alert("This browser doesn't support HTML5 file uploads!");
        }
    }

    stopEdit = function () {
        this.tedit(true);
    };

    pstopEdit = function () {
        this.pedit(false);
    };

    getDepartments = (request, ui) => {
        var text = request.term;
        if (text === " ")
            return;
        if (text === "")
            return;
        var departments = ko.computed({
            owner: this,
            read: () => {
                this.arrTemp = [];
                if (this.TabContext().IncludeInactiveOptions()) {
                    return ko.utils.arrayFilter(this.UseLookupTables.primePermissionDepartmentList(), (i) => {
                        if (this.arrTemp.indexOf(i.DepartmentID) == -1) {
                            this.arrTemp.push(i.DepartmentID);
                            return i.DivisionID == this.TabContext().SelectedDivisionId() && this.TabContext().RequiredPermissionIDs().indexOf(i.PermissionID) != -1 && i.DepartmentName.toLowerCase().indexOf(text.toLowerCase()) === 0;
                        } else {
                            return false;
                        }
                    });
                } else {
                    return ko.utils.arrayFilter(this.UseLookupTables.primePermissionDepartmentList(), (i) => {
                        if (this.arrTemp.indexOf(i.DepartmentID) == -1) {
                            this.arrTemp.push(i.DepartmentID);
                            return i.IsActive == true && i.DivisionID == this.TabContext().SelectedDivisionId() && this.TabContext().RequiredPermissionIDs().indexOf(i.PermissionID.toLowerCase()) != -1 && i.DepartmentName.toLowerCase().indexOf(text.toLowerCase()) === 0;
                        } else {
                            return false;
                        }
                    });
                }
            }
        });
        ui($.map(departments(), (d) => {
            return {
                languageValue: d.DepartmentID,
                label: d.DepartmentCode.trim() + ' - ' + d.DepartmentName,
                value: d.DepartmentCode,
                name: d.DepartmentName
            };
        }));
    };

    SelectDepartment = function (event, ui) {
        (ui.item.languageValue);
        var vm = ko.dataFor(event.target);
        vm.DepartmentID(ui.item.languageValue);
        vm.DepartmentName(ui.item.name);
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

    getProduce = (request, ui) => {
        var text = request.term;
        var self = this;
        if (text === " ")
            return;
        if (text === "")
            return;
        $.ajax({
            type: "GET",
            url: "/api/Produce/AutoComplete/?divisionID=" + self.TabContext().SelectedDivisionId() + "&search=" + text,
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

    getPorts = (request, ui) => {
        var text = request.term;
        $.ajax({
            type: "POST",
            url: '/Port/AutoComplete/?companyID=' + this.TabContext().SelectedCompanyId() + '&search=' + text,
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

    getSupplier = (request, ui) => {
        var text = request.term;
        $.ajax({
            type: "POST",
            url: "/Supplier/AutoComplete/?companyID=" + this.TabContext().SelectedCompanyId() + "&search=" + text,
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

    addFileNameWithURL = function (fileName, url) {
        this.ConsignmentModel().FilenamesWithURLs.push(new MyCustomOption(fileName, url));
        this.saveState(false);
    }

    removeFileNameWithURL = function (fileName) {
        for (let i = 0; i < this.FilenamesWithURLs().length; i++) {
            if (this.ConsignmentModel().FilenamesWithURLs()[i].display === fileName) {
                this.ConsignmentModel().FilenamesWithURLs.remove(this.FilenamesWithURLs()[i]);
            }
        }
        this.saveState(false);
    }

    updateHeader = function () {
        var PostConsignment = this.ConsignmentModel();
        PostConsignment.IsSaved(true);
        //  debugger;
        var promise =
            $.ajax({
                url: "/Consignment/UpdateConsignment/",
                cache: false,
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: ko.toJSON(PostConsignment),
                dataType: "json",
                //data: ko.toJSON(PostConsignment),
                success: function (PostConsignment) { }
            });
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
            //Consignmentreference not needed as set on server so new every time save
            //if (PostConsignment.ConsignmentDescription() != "" && PostConsignment.DespatchDate != "" && PostConsignment.ConsignmentReference() != "" && PostConsignment.VehicleDetail() != "") {
            if (PostConsignment.ConsignmentDescription() != "" && PostConsignment.DespatchDate != "" && PostConsignment.VehicleDetail() != "") {
                //this.HeaderCreated(true); // after set other Posts are update
                var promise =
                    $.ajax({
                        url: "/Consignment/CreateConsignment/",
                        cache: false,
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        data: ko.toJSON(data.ConsignmentModel()),
                        dataType: "json"
                    }).fail(
                        function (xhr, textStatus, err) {
                            //debugger;
                            alert(err);
                        });

                promise.done(function (result) {
                    //Use the new consignment number if supplied
                    data.ConsignmentModel().ConsignmentReference(result.ConsignmentReference);

                    //debugger;
                    data.addNewConsignmentItem();

                    //data.GetDBConsignmentItemModel(data.ConsignmentModel().ConsignmentID());
                    
                    let uploadFolder = data.ConsignmentModel().UploadFolder();
                    let fileNames = new Array(data.ConsignmentModel().UploadedFileNames().length);
                    for (let i = 0; i < data.ConsignmentModel().UploadedFileNames().length; i++)
                        fileNames.push(data.ConsignmentModel().UploadedFileNames()[i].itsValue);
                    let fileNamesDeleted = new Array(data.ConsignmentModel().UploadedFileNames().length);
                    for (let i = 0; i < data.ConsignmentModel().UploadedFileNamesDeleted().length; i++)
                        fileNamesDeleted.push(data.ConsignmentModel().UploadedFileNamesDeleted()[i].itsValue);


                    var jsonToSned = {
                        MainID: data.ConsignmentModel().ConsignmentID(),
                        itemType: 2,
                        fileNames: fileNames,
                        fileNamesDeleted: fileNamesDeleted,
                        uploadFolder: uploadFolder
                    };

                    var promise2 = $.ajax({
                        type: "POST",
                        url: '/api/File/AttachFilesFromServer',
                        contentType: 'application/json; charset=utf-8',
                        data: JSON.stringify(jsonToSned),
                        success: function (result) {
                        },
                        error: function (xhr) {
                            // alert(xhr);
                        }
                    });

                    promise2.done(function (result) {
                        var promise3 = $.ajax({
                            type: "POST",
                            url: '/api/File/DeleteFilesFromServer',
                            contentType: 'application/json; charset=utf-8',
                            data: JSON.stringify(jsonToSned),
                            success: function (result) {
                                //alert(JSON.stringify(result));

                                //console.log(result);
                            },
                            error: function (xhr) {
                                // alert(xhr);
                            }
                        });
                    });

                    PostConsignment.NoMoreAttachments(true);

                    data.saveState(false);
                });


            } else {

                //alert('Please Enter All the Values !!');
            }

        }
    };

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

    addNewConsignmentItem = () => {
        // ? go to Controller to get New?
        var consignment_item = new ConsignmentItemVM(null, this.UseLookupTables);
        var new_guid = this.guid();
        consignment_item.ConsignmentItemID(new_guid);
        consignment_item.ConsignmentID = this.ConsignmentModel().ConsignmentID;
        consignment_item.ProduceFocused(true);
        consignment_item.CountryName(this.ConsignmentModel().CountryName());
        consignment_item.OriginCountryID(this.ConsignmentModel().CountryID());
        consignment_item.DepartmentID(this.TabContext().SelectedDepartmentId());
        consignment_item.DepartmentName(this.TabContext().SelectedDepartmentText());
        consignment_item.DepartmentCode(this.TabContext().SelectedDepartmentCode());

        consignment_item.Porterage("0");
        consignment_item.Id = this.ConsignmentItemIndex;
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
        if (this.ConsignmentModel().ConsignmentItems()[parentindex].ConsignmentItemArrivals().length > 1) {
            this.ConsignmentModel().ConsignmentItems()[parentindex].ConsignmentItemArrivals.splice(index, 1);
            this.ConsignmentModel().ConsignmentItems()[parentindex].ConsignmentItemArrivals.valueHasMutated(); // notify Parent Array
        }
    }

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

