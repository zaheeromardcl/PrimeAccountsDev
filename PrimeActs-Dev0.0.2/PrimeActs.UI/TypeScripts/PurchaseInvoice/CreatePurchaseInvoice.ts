/// <reference path="purchaseinvoicedetails.ts" />
class CreatePurchaseInvoiceViewModel {
    UseLookupTables: LookupTables;
    PurchaseInvoice: KnockoutObservable<PurchaseInvoiceDetailViewModel>;

    ChangeObservable: KnockoutComputed<void>;
    FileHasChanged: KnockoutObservable<void>;
    initDone: boolean;

    TabPanelName: KnockoutObservable<string>;
    TabPanelAlphaName: KnockoutComputed<string>;

    fileInput: KnockoutObservable<string>;
    FileName: KnockoutObservable<string>;

    SubscriberTab: any;
    SubscriberReplaceTab: any;

    TabContext: KnockoutObservable<AppUserContextModel>;
    
    PurchaseChargeTypes: KnockoutObservableArray<MyCustomOptionExtended>;

    constructor(tabPanelName, uploadGuidFolder, subscriberTab, subscriberReplaceTab, data, lookupTables: LookupTables) {
        this.UseLookupTables = lookupTables;
        this.TabPanelName = ko.observable(tabPanelName);
        this.TabPanelAlphaName = ko.computed({
            read: () => {
                return this.TabPanelName().replace(/[0-9]/g, '');
            }
        });
        
        this.PurchaseChargeTypes = ko.observableArray<MyCustomOptionExtended>([]);
        
        for (let i = 0; i < data.PurchaseChargeTypes.length; i++)
            this.PurchaseChargeTypes.push(new MyCustomOptionExtended(data.PurchaseChargeTypes[i].PurchaseChargeTypeID, data.PurchaseChargeTypes[i].PurchaseChargeTypeName, data.PurchaseChargeTypes[i].PurchaseChargeTypeCode));
        

        this.TabContext = ko.observable(new AppUserContextModel(data.UserContextModel, this.UseLookupTables));

        this.fileInput = ko.observable("");
        this.PurchaseInvoice = ko.observable(new PurchaseInvoiceDetailViewModel({ PurchaseInvoiceDate: data.PurchaseInvoiceDate, UploadFolder: uploadGuidFolder }, subscriberTab));

        this.initDone = false;
        this.ChangeObservable = ko.computed({
            read: () => {
                this.PurchaseInvoice().PurchaseInvoiceDate();
                this.PurchaseInvoice().SupplierDepartmentName();
                this.PurchaseInvoice().SupplierDepartmentID();
                this.PurchaseInvoice().PurchaseInvoiceReference();

                if (this.initDone) {
                    this.saveState(false);
                }
                else {
                    this.initDone = true;
                }
            }
        });

        this.FileHasChanged = ko.computed({
            read: () => {
                this.fileInput();

                //alert("hi2");
                //alert(this.fileInput());
            }
        });

        this.SubscriberTab = subscriberTab;
        this.SubscriberReplaceTab = subscriberReplaceTab;
    }

    getSupplierDepartments(request, response) {
        var text = request.term;
        $.ajax({
            type: 'GET',
            url: '/API/Supplier/AutoComplete/?search=' + text,
            data: {
                json: '{}',
                delay: 0.5,
                search: text

            },
            success: function (data) {
                response($.map(data, function (language) {
                    return {
                        languageValue: language.Id,
                        label: language.label
                    };
                }));
            }
        });
    };

    selectSupplierDepartment = function (event, ui) {
        let suppDeptId = ui.item.languageValue;

        var vm = ko.dataFor(event.target);

        var req = $.ajax({
            type: 'GET',
            url: '/API/SupplierDepartment/SupplierDepartmentWithConsignments',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: { "id": suppDeptId },
            success: function (result) {
                vm.PurchaseInvoice().Consignments(result.Consignments);
            },
            error: function (jqXHR, textStatus, error) {

            }
        });

        vm.PurchaseInvoice().SupplierDepartmentID(suppDeptId);
        vm.PurchaseInvoice().SupplierDepartmentName(ui.item.label);
        vm.PurchaseInvoice().PreviousSupplierDepartmentID(suppDeptId);
        vm.PurchaseInvoice().PreviousSupplierDepartmentName(ui.item.label);

    };

    //onSupplierChange = function (event, ui) {
    //    // if value is cleared, clear in ViewModel
    //    if (ui.item == null || ui.item.value == null || ui.item.value == '') {
    //        var vm = ko.dataFor(event.target);
    //        vm.SelectedSupplierDepartmentID("");
    //    }
    //}

    createPurchaseInvoice = function (data: CreatePurchaseInvoiceViewModel) {
        data.PurchaseInvoice().serverErrors.removeAll();

        // Re-validate
        data.PurchaseInvoice().Errors = ko.validation.group(data.PurchaseInvoice());
        if (data.PurchaseInvoice().Errors().length === 0 && !data.PurchaseInvoice().IsSaving()) {
            data.PurchaseInvoice().IsSaving(true);
            
            var promise = $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                url: '/api/PurchaseInvoice/CreatePurchaseInvoice',
                data: ko.toJSON(data.PurchaseInvoice())
            });

            promise.done(function (result) {
                data.PurchaseInvoice().resetDirtyFlag();
                data.PurchaseInvoice().PurchaseInvoiceId(result.PurchaseInvoiceID);
                data.PurchaseInvoice().CreatedBy(result.CreatedBy);
                data.PurchaseInvoice().CreatedDate(result.CreatedDate);
                data.PurchaseInvoice().UpdatedBy(result.UpdatedBy);
                data.PurchaseInvoice().UpdatedDate(result.UpdatedDate);

                var newItem = new PurchaseInvoiceItemViewModel({
                    PurchaseInvoiceID: data.PurchaseInvoice().PurchaseInvoiceId()
                });
                data.PurchaseInvoice().PurchaseInvoiceItems.push(newItem);

                var newItemSundry = new PurchaseInvoiceItemSundryViewModel({
                    PurchaseInvoiceID: data.PurchaseInvoice().PurchaseInvoiceId()
                });
                data.PurchaseInvoice().PurchaseInvoiceItemsSundry.push(newItemSundry);

                let uploadFolder = data.PurchaseInvoice().UploadFolder();

                let fileNames = new Array(data.PurchaseInvoice().UploadedFileNames().length);
                for (let i = 0; i < data.PurchaseInvoice().UploadedFileNames().length; i++)
                    fileNames.push(data.PurchaseInvoice().UploadedFileNames()[i].itsValue);
                let fileNamesDeleted = new Array(data.PurchaseInvoice().UploadedFileNames().length);
                for (let i = 0; i < data.PurchaseInvoice().UploadedFileNamesDeleted().length; i++)
                    fileNamesDeleted.push(data.PurchaseInvoice().UploadedFileNamesDeleted()[i].itsValue);
                
                var jsonToSned = {
                    MainID: data.PurchaseInvoice().PurchaseInvoiceId(),
                    itemType: 1,
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

                // commented out because the files are already uploaded to a temp. folder on server side otherwise this code would send the files data
                //var promise2 = $.ajax({
                //    type: "POST",
                //    url: '/api/File/AttachFiles?id=' + data.PurchaseInvoice().PurchaseInvoiceId() + '&itemType=1',
                //    contentType: false,
                //    processData: false,
                //    data: data.PurchaseInvoice().Files(),
                //    success: function (result) {

                //        //alert(JSON.stringify(result));

                //        //console.log(result);
                //    },
                //    error: function (xhr) {
                //        alert(xhr);
                //    }
                //});
                data.PurchaseInvoice().NoMoreAttachments(true);
                data.PurchaseInvoice().Created(true);
                data.PurchaseInvoice().IsSaving(false);
            });

            promise.fail(function (jqXHR, textStatus, errorThrown) {
                var result = JSON.parse(jqXHR.responseText);
                data.PurchaseInvoice().serverErrors.push(result.Message);
                data.PurchaseInvoice().IsSaving(false);
            });
        }
        else {
            data.PurchaseInvoice().ShowErrors(true);
            data.PurchaseInvoice().Errors.showAllMessages(true);
        }
    }

    onSupplierDepartmentFocusOut = function () {
        if (this.PurchaseInvoice().SupplierDepartmentName() !== this.PurchaseInvoice().PreviousSupplierDepartmentName() && this.PurchaseInvoice().SupplierDepartmentID() === this.PurchaseInvoice().PreviousSupplierDepartmentID()) {
            this.PurchaseInvoice().SupplierDepartmentID(undefined);
            this.PurchaseInvoice().SupplierDepartmentName(undefined);
        }

        this.onPurchaseInvoiceFocusOut();
    }

    onPurchaseDateFocusOut = function () {
        this.PurchaseInvoice().FocusAddItems(true);
        this.onPurchaseInvoiceFocusOut();
    }

    // only update mode (we have an invoice id already)
    onPurchaseInvoiceFocusOut = function () {
        // if nothing has changed then return
        if (!this.PurchaseInvoice().IsDirty()) {
            return;
        }
        //debugger;
        this.updatePurchaseInvoice(false);
    }

    updatePurchaseInvoice = function (isPayInvoice) {
        let self = this;
        
        if (self.PurchaseInvoice().IsSaving() || (isPayInvoice && self.PurchaseInvoice().PurchaseInvoiceItems().length === 0)) {
            return;
        }
        self.PurchaseInvoice().Errors = ko.validation.group(self.PurchaseInvoice());
        if (self.PurchaseInvoice().PurchaseInvoiceId() != undefined && self.PurchaseInvoice().Errors().length === 0) {
            self.PurchaseInvoice().IsSaving(true);

            var promise = $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                url: '/api/PurchaseInvoice/CreatePurchaseInvoice',
                data: ko.toJSON(this.PurchaseInvoice())
            });

            promise.done(function (result) {
                self.PurchaseInvoice().resetDirtyFlag();
                self.PurchaseInvoice().IsSaving(false);
                
                if (isPayInvoice) {
                    self.OpenDetails(self.PurchaseInvoice().PurchaseInvoiceId());
                    
                }
            });

            promise.fail(function (jqXHR, textStatus, errorThrown) {
                var result = JSON.parse(jqXHR.responseText);
                self.PurchaseInvoice().serverErrors.push(result.Message);
                self.PurchaseInvoice().IsSaving(false);
            });
        }
        else {
            this.PurchaseInvoice().ShowErrors(true);
            this.PurchaseInvoice().Errors.showAllMessages(true);
        }

    }

    payInvoice = function () {
        this.PurchaseInvoice().IsSaved(true);
        this.updatePurchaseInvoice(true);
    }

    addNewItemSundry = function () {
        var newItemSundry = new PurchaseInvoiceItemSundryViewModel({
            PurchaseInvoiceID: this.PurchaseInvoice().PurchaseInvoiceId()
        });
        this.PurchaseInvoice().PurchaseInvoiceItemsSundry.push(newItemSundry);
        this.saveState(false);
    }

    addNewItem = function () {
        var newItem = new PurchaseInvoiceItemViewModel({
            PurchaseInvoiceID: this.PurchaseInvoice().PurchaseInvoiceId()
        });
        this.PurchaseInvoice().PurchaseInvoiceItems.push(newItem);
        this.saveState(false);
    }

    removeItemSundry = function (index: number) {
        var item = this.PurchaseInvoice().PurchaseInvoiceItemsSundry.splice(index, 1)[0];
        if (item.PurchaseInvoiceItemID() !== undefined) {
            var self = this;
            var promise = $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                url: '/api/PurchaseInvoice/RemovePurchaseInvoiceItem/' + item.PurchaseInvoiceItemID()
            });
            promise.done(function (result) {
                if (self.PurchaseInvoice().PurchaseInvoiceItemsSundry().length === 0) {
                    self.addNewItemSundry();
                }
                else {
                    self.saveState(false);
                }
            });
        }
        else if (this.PurchaseInvoice().PurchaseInvoiceItemsSundry().length == 0) {
            this.addNewItemSundry();
        }
        else {
            this.saveState(false);
        }
    }

    removeItem = function (index: number) {
        var item = this.PurchaseInvoice().PurchaseInvoiceItems.splice(index, 1)[0];
        if (item.PurchaseInvoiceItemID() !== undefined) {
            var self = this;
            var promise = $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                url: '/api/PurchaseInvoice/RemovePurchaseInvoiceItem/' + item.PurchaseInvoiceItemID()
            });
            promise.done(function (result) {
                if (self.PurchaseInvoice().PurchaseInvoiceItems().length === 0) {
                    self.addNewItem();
                }
                else {
                    self.saveState(false);
                }
            });
            promise.fail(function (jqXHR, textStatus, errorThrown) {
                // failed to mark, re-add to Ticket
                this.TicketModel().TicketItems.push(item);
            });
        }
        else if (this.PurchaseInvoice().PurchaseInvoiceItems().length == 0) {
            this.addNewItem();
        }
        else {
            this.saveState(false);
        }
    }

    onConsignmentFocusOut = function (index: number) {
        let item = (this.PurchaseInvoice().PurchaseInvoiceItems()[index]);
        if (item.Description() !== item.PreviousDescription() && item.ConsignmentItemID() === item.PreviousConsignmentItemID()) {
            item.ConsignmentItemID(undefined);
            item.Description(undefined);
        }

        this.onFocusOut(index);
    }

    onEstCostFocusOut = function (index: number) {
        let item = (this.PurchaseInvoice().PurchaseInvoiceItems()[index]);
        if (item.EstimatedPurchaseCost() !== item.PreviousEstimatedPurchaseCost() && item.ConsignmentItemID() === item.PreviousConsignmentItemID()) {
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                url: '/api/PurchaseInvoice/PurchaseInvoiceItemForReview/',
                data: ko.toJSON(item),
                success: function (result) {
                }
            });
        }

        this.onFocusOut(index);
    }
    
    OpenDetails = function (data) {
        // the code below opens the details in a new tab
        //var options = {
        //    TabTitle: "loading...",
        //    PanelName: "PurchaseInvoiceDetails",
        //    UriParam: data
        //};
        //this.SubscriberTab.notifySubscribers(options, "save");

        // replace the tab
        let pn = this.TabPanelName();
        this.SubscriberReplaceTab.notifySubscribers({
            PanelName: pn,
            NewPanelName: "PurchaseInvoiceDetails",
            UriParam: data
        }, "save");
    }

    onFocusOutSundry = function (index: number) {
        let item = <PurchaseInvoiceDetailViewModel>(this.PurchaseInvoice().PurchaseInvoiceItemsSundry()[index]);
        this.savePurchaseInvoiceItemSundry(this, item);
    }

    savePurchaseInvoiceItemSundry = function (data: CreatePurchaseInvoiceViewModel, purchaseInvoiceItem: PurchaseInvoiceItemSundryViewModel) {
        if (purchaseInvoiceItem.IsSaving() || !purchaseInvoiceItem.IsDirty()) {
            return;
        }
        // Re-validate
        purchaseInvoiceItem.Errors = ko.validation.group(purchaseInvoiceItem);

        if (purchaseInvoiceItem.Errors().length === 0) {
            purchaseInvoiceItem.IsSaving(true);
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                url: '/api/PurchaseInvoice/SavePurchaseInvoiceItem/',
                data: ko.toJSON(purchaseInvoiceItem),
                success: function (result) {
                    //var createdTicketItem = new PurchaseInvoiceItemViewModel(result);
                    //createdTicketItem.resetDirtyFlag();
                    purchaseInvoiceItem.resetDirtyFlag();
                    purchaseInvoiceItem.PurchaseInvoiceItemID(result.PurchaseInvoiceItemID);
                    purchaseInvoiceItem.CreatedBy(result.CreatedBy);
                    purchaseInvoiceItem.CreatedDate(result.CreatedDate);
                    purchaseInvoiceItem.IsSaving(false);
                    //data.PurchaseInvoice().PurchaseInvoiceItems.replace(purchaseInvoiceItem, createdTicketItem);
                    data.saveState(false);
                }
            });
        }
        else {
            purchaseInvoiceItem.ShowErrors(true);
            purchaseInvoiceItem.Errors.showAllMessages(true);
        }
    }

    onFocusOut = function (index: number) {
        let item = <PurchaseInvoiceDetailViewModel>(this.PurchaseInvoice().PurchaseInvoiceItems()[index]);
        this.savePurchaseInvoiceItem(this, item);
    }

    savePurchaseInvoiceItem = function (data: CreatePurchaseInvoiceViewModel, purchaseInvoiceItem: PurchaseInvoiceItemViewModel) {
        if (purchaseInvoiceItem.IsSaving() || !purchaseInvoiceItem.IsDirty()) {
            return;
        }
        // Re-validate
        purchaseInvoiceItem.Errors = ko.validation.group(purchaseInvoiceItem);

        if (purchaseInvoiceItem.Errors().length === 0) {
            purchaseInvoiceItem.IsSaving(true);
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                url: '/api/PurchaseInvoice/SavePurchaseInvoiceItem/',
                data: ko.toJSON(purchaseInvoiceItem),
                success: function (result) {
                    //var createdTicketItem = new PurchaseInvoiceItemViewModel(result);
                    //createdTicketItem.resetDirtyFlag();
                    purchaseInvoiceItem.resetDirtyFlag();
                    purchaseInvoiceItem.PurchaseInvoiceItemID(result.PurchaseInvoiceItemID);
                    purchaseInvoiceItem.CreatedBy(result.CreatedBy);
                    purchaseInvoiceItem.CreatedDate(result.CreatedDate);
                    purchaseInvoiceItem.IsSaving(false);
                    //data.PurchaseInvoice().PurchaseInvoiceItems.replace(purchaseInvoiceItem, createdTicketItem);
                    data.saveState(false);
                }
            });
        }
        else {
            purchaseInvoiceItem.ShowErrors(true);
            purchaseInvoiceItem.Errors.showAllMessages(true);
        }
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
        this.PurchaseInvoice().SupplierHasFocus(true);
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

    saveState = function (initial: boolean) {
        var data = ko.toJSON(this.PurchaseInvoice());
        this.setStateOnServer(data, this.TabPanelAlphaName(), "CreatePurchaseInvoice", initial);
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

    mapRestoredState = function (data) {
        var purchaseInvoiceItemModels = [];
        var purchaseInvoiceItemSundryModels = [];
        if (data.hasOwnProperty('PurchaseInvoiceItems')) {
            for (var purchaseInvoiceItemEditModel of data.PurchaseInvoiceItems) {
                purchaseInvoiceItemModels.push(new PurchaseInvoiceItemViewModel(purchaseInvoiceItemEditModel));
            }
        }
        if (data.hasOwnProperty('PurchaseInvoiceItemsSundry')) {
            for (var purchaseInvoiceItemEditModel of data.PurchaseInvoiceItemsSundry) {
                purchaseInvoiceItemSundryModels.push(new PurchaseInvoiceItemSundryViewModel(purchaseInvoiceItemEditModel));
            }
        }

        this.initDone = false;
        // fix date format
        ////////debugger;
        //var date_format_check = moment(data.PurchaseInvoiceDate);
        //if (date_format_check.isValid()) {
        //    var date_format_fix = date_format_check.format('DD/MM/YYYY');
        //    data.PurchaseInvoiceDate = date_format_fix;
        //} else { // if bad date 
        //    var now_date = moment();
        //    now_date.add(1, 'd'); // add 1 day
        //    data.PurchaseInvoiceDate = now_date.format('DD/MM/YYYY'); // format
        //}

        this.PurchaseInvoice(new PurchaseInvoiceDetailViewModel(data, this.SubscriberTab));
        this.PurchaseInvoice().PurchaseInvoiceItems(purchaseInvoiceItemModels);
        this.PurchaseInvoice().PurchaseInvoiceItemsSundry(purchaseInvoiceItemSundryModels);
    }

    uploadImages = function (files, data: CreatePurchaseInvoiceViewModel) {
        
        if (files.length > 0) {
            var fdata = new FormData();
            for (var x = 0; x < files.length; x++) {
                fdata.append("file" + x, files[x]);
                // Could use the handler in here in order to add the files to the consignment!?
            }

            data.PurchaseInvoice().Files(fdata);

            //var promise = $.ajax({
            //    type: "POST",
            //    url: '/api/PurchaseInvoice/CreatePurchaseInvoice2',
            //    contentType: false,
            //    processData: false,
            //    data: data,
            //    success: function (result) {

            //        alert(JSON.stringify(result));

            //        //console.log(result);
            //    },
            //    error: function (xhr) {
            //        alert(xhr);
            //    }
            //});
        }
        else {
            alert("This browser doesn't support HTML5 file uploads!");
        }
    }
}