class ConsignmentItemArrival {
    ConsignmentItemArrivalID: KnockoutObservable<string>;
    NoteID: KnockoutObservable<string>;
    ConsignmentItemID: KnockoutObservable<string>;
    ConsignmentArrivalDate: KnockoutObservable<string>;
    ConsignmentArrivalDateString: KnockoutObservable<string>;
    Quantity: KnockoutObservable<number>;
    IsExpected: KnockoutObservable<boolean>;
    StockLocationID: KnockoutObservable<string>;
    UpdatedBy: KnockoutObservable<string>;
    UpdatedDate: KnockoutObservable<Date>;
    CreatedBy: KnockoutObservable<string>;
    CreatedDate: KnockoutObservable<Date>;
    IsActive: KnockoutObservable<boolean>;

    constructor(data) {
        data = data || {};
        this.ConsignmentItemArrivalID = ko.observable(data.ConsignmentItemArrivalID || "");
        this.ConsignmentItemID = ko.observable(data.ConsignmentItemID || "");
        this.NoteID = ko.observable(data.NoteID || "");
        this.ConsignmentArrivalDate = ko.observable(data.ConsignmentArrivalDate || "");
        this.ConsignmentArrivalDateString = ko.observable(data.ConsignmentArrivalDateString || "");
        this.ConsignmentItemID = ko.observable(data.ConsignmentItemID || "");
        this.Quantity = ko.observable(data.Quantity || 0);
        this.IsActive = ko.observable(data.IsActive || true);
        this.IsExpected = ko.observable(data.IsExpected || true);
        this.StockLocationID = ko.observable(data.StockLocationID || "");
    }

    showQuantityRequired = function (item) {
        return !this.validateQuantity(item(), this);
    }

    validateQuantity = function (val, observable) {
        if (val == undefined || val == null || val == "")
            return false;
        return true;
    }
}

class ConsignmentItemPriceReturn {
    ConsignmentItemPriceReturnID: KnockoutObservable<string>;
    ReturnUnitPrice: KnockoutObservable<number>;
    ReturnQuantity: KnockoutObservable<number>;
    ShowErrors: KnockoutObservable<boolean>;

    constructor(data) {
        data = data || {};
        this.ConsignmentItemPriceReturnID = ko.observable(data.ConsignmentItemPriceReturnID);
        this.ReturnQuantity = ko.observable(data.ReturnQuantity).extend({ required: true });;//.extend({ validation: { validator: this.QuantityPositive, params: this }, message: "Required if not UK" });
        this.ReturnUnitPrice = ko.observable(data.ReturnUnitPrice).extend({ numeric: 2 }).extend({ required: true });;
        this.ShowErrors = ko.observable(false);
    }

    showRequired = function (item, field) {
        ko.validation.validateObservable(item);
        if (!this.showError(item)) {
            return item == undefined || !item.isValid();
        }
        return false;
    }
    showError = function (item) {
        if ((item == undefined || !item.isValid()) && this.ShowErrors()) {
            return true;
        }

        return false;
    }

    //PricePositive = function (val, observable) {
    //    if (observable.ReturnUnitPrice() > 1) {
    //        return true;
    //    }
    //    return false;
    //}

    //QuantityPositive = function (val, observable) {
    //    if (observable.ReturnQuantity > 1) {
    //        return true;
    //    }
    //    return false;
    //}
}

class ConsignmentItemPriceReturnModelsCollection {
    ConsignmentItemPriceReturnModels: KnockoutObservableArray<ConsignmentItemPriceReturn>;
    ConsignmentItemPriceReturnModelsDeleted: KnockoutObservableArray<ConsignmentItemPriceReturn>;
    ConsignmentItemID: KnockoutObservable<string>;
    ConsignmentCreatedDate: KnockoutObservable<string>;
    constructor(data, consignmentID) {
        //debugger;
        this.ConsignmentItemID = ko.observable(consignmentID);
        this.ConsignmentCreatedDate = ko.observable("");
        this.ConsignmentItemPriceReturnModels = ko.observableArray([]);
        if (data.ConsignmentItemPriceReturnModels != undefined) {
            for (let i = 0; i < data.ConsignmentItemPriceReturnModels.length; i++) {
                this.ConsignmentItemPriceReturnModels.push(new ConsignmentItemPriceReturn(data.ConsignmentItemPriceReturnModels[i]));
            }
        }

        this.ConsignmentItemPriceReturnModelsDeleted = ko.observableArray([]);
        if (data.ConsignmentItemPriceReturnModelsDeleted != undefined) {
            for (let i = 0; i < data.ConsignmentItemPriceReturnModelsDeleted.length; i++) {
                this.ConsignmentItemPriceReturnModelsDeleted.push(new ConsignmentItemPriceReturn(data.ConsignmentItemPriceReturnModelsDeleted[i]));
            }
        }
    }
}

class ConsignmentItemVM {
    UseLookupTables: LookupTables;
    Id: KnockoutObservable<number>;
    ConsignmentID: KnockoutObservable<string>;
    ConsignmentItemID: KnockoutObservable<string>;
    BestBeforeDate: KnockoutObservable<Date>;
    Brand: KnockoutObservable<string>;
    Produce: KnockoutObservable<string>;
    Rotation: KnockoutObservable<string>;
    Pack: KnockoutObservable<string>;
    PackWeight: KnockoutObservable<string>;
    PackWtUnit: KnockoutObservable<string>;
    PackSize: KnockoutObservable<string>;
    PackPall: KnockoutObservable<string>;
    ExpectedQuantity: KnockoutObservable<string>;
    ReceivedQuantity: KnockoutObservable<string>;
    EstimatedPercentageProfit: KnockoutObservable<string>;
    EstimatedChargeCostPerPack: KnockoutObservable<string>;
    Returns: KnockoutObservable<string>;
    EstimatedPurchaseCostPerPack: KnockoutObservable<string>;
    IsSaved: KnockoutObservable<boolean>;
    QuantityExpected: KnockoutObservable<string>;
    PorterageID: KnockoutObservable<string>;
    //selectedOption:
    Porterage: KnockoutObservable<string>;
    ProduceID: KnockoutObservable<string>;
    ProduceName: KnockoutObservable<string>;
    ProduceFocused: KnockoutObservable<boolean>;
    DepartmentID: KnockoutObservable<string>;
    DepartmentName: KnockoutObservable<string>;
    DepartmentCode: KnockoutObservable<string>;
    PorterageListFiltered: KnockoutComputed<MyCustomOptionFKExtended[]>;
    PackWtUnitListFiltered: KnockoutComputed<MyCustomOptionFKExtended[]>;
    PackWtUnitID: KnockoutObservable<string>;
    //PackWtUnit: KnockouKnockoutObservable<string>; // Duplicate in original too?
    Consignment: KnockoutObservable<string>;
    ReceivedDate: KnockoutObservable<Date>;
    NoteID: KnockoutObservable<string>;
    NoteText: KnockoutObservable<string>;
    Department: KnockoutObservable<string>;
    PackType: KnockoutObservable<string>;
    title: KnockoutObservable<string>;
    CountryName: KnockoutObservable<string>;
    OriginCountryID: KnockoutObservable<string>;
    IsCountry: KnockoutObservable<boolean>;
    ShowErrors: KnockoutObservable<boolean>;
    Errors: KnockoutValidationErrors;
    isUK: KnockoutComputed<boolean>;
    hasPackWeight: KnockoutComputed<boolean>;
    ConsignmentItemArrivals: KnockoutObservableArray<ConsignmentItemArrival>;
    IsCostDisabled: KnockoutObservable<boolean>;
    IsInfoVisible: KnockoutObservable<boolean>;

    isReturnVisible: KnockoutObservable<boolean>;

    ReturnsCollection: KnockoutObservable<ConsignmentItemPriceReturnModelsCollection>;

    constructor(data, lookupTables: LookupTables) {
        data = data || {};

        this.UseLookupTables = lookupTables;
        this.Id = ko.observable(0);
        this.ConsignmentID = ko.observable(data.ConsignmentID || "");
        this.ConsignmentItemID = ko.observable(data.ConsignmentItemID || "");

        this.ReturnsCollection = ko.observable(new ConsignmentItemPriceReturnModelsCollection(data.ReturnsCollection || {}, this.ConsignmentItemID));
        //debugger;
        this.isReturnVisible = ko.observable(data.isReturnVisible || this.ReturnsCollection().ConsignmentItemPriceReturnModels().length > 0);
        this.CountryName = ko.observable(data.CountryName || "United Kingdom");
        this.OriginCountryID = ko.observable(data.OriginCountryID); // dc05052016                
        //this.BestBeforeDate = ko.observable(data.BestBeforeDate || new Date.format("MM/dd/yyy")); // moment.js <-----
        this.BestBeforeDate = ko.observable(data.BestBeforeDate || "");
        this.Brand = ko.observable(data.Brand || "").extend({ required: { message: "Brand is required" } });
        this.ProduceName = ko.observable(data.Produce || "");
        this.Produce = ko.observable(data.Produce || "");
        this.Department = ko.observable(data.Department || "");
        this.Rotation = ko.observable(data.Rotation || "");
        this.PackType = ko.observable(data.PackType || "");
        // debugger;
        this.PackWtUnitID = ko.observable(data.PackWtUnitID || "");

        this.IsCostDisabled = ko.observable(data.IsCostDisabled || false);
        this.IsInfoVisible = ko.observable(false);
        //this.WtUnit = ko.observable(data.WtUnit || "");
        //this.PorterageID = ko.observable(data.PorterageID || "00760000-0000-0001-0006-017528069450"); // test DC remove may 3
        //this.PorterageCode = ko.observable(data.PorterageCode || "");
        //this.PackWeight = ko.observable(data.PackWeight || "").extend({ required: { message: "PackWeight is required" } });
        
        this.PackWeight = ko.observable(data.PackWeight || "").extend({ validation: { validator: this.packWeightRequired, params: this }, message: "Required if not UK" });
        this.PackWtUnit = ko.observable(data.PackWtUnit || "");
        //this.AggPack = ko.observableArray([data.PackWeight, " ", data.PackWtUnit]);
        this.PackSize = ko.observable(data.PackSize || "");
        this.PackPall = ko.observable(data.PackPall || "");
        this.ExpectedQuantity = ko.observable(data.ExpectedQuantity || "0").extend({ required: { message: "Expected Qty is required" } });
        this.ReceivedQuantity = ko.observable(data.ReceivedQuantity || "");
        this.EstimatedPercentageProfit = ko.observable(data.EstimatedPercentageProfit || "");
        this.EstimatedChargeCostPerPack = ko.observable(data.EstimatedChargeCostPerPack || "");
        this.Returns = ko.observable(data.Returns || "");
        this.EstimatedPurchaseCostPerPack = ko.observable(data.EstimatedPurchaseCostPerPack || "");

        this.PorterageID = ko.observable(data.PorterageID || "");
        this.Porterage = ko.observable(data.Porterage || "");
        this.ProduceID = ko.observable(data.ProduceID || "");
        //this.ProduceName = ko.observable(data.ProduceName || "");
        this.QuantityExpected = ko.observable(data.QuantityExpected || "");
        this.Consignment = ko.observable(data.Consignment || "");
        this.NoteID = ko.observable(data.NoteID || "");
        this.NoteText = ko.observable(data.NoteText || "");
        this.IsCountry = ko.observable(data.IsCountry || false);
        this.DepartmentID = ko.observable(data.DepartmentID || "").extend({ validation: { validator: this.validateGuid } });
        this.DepartmentName = ko.observable(data.DepartmentName || "");
        this.DepartmentCode = ko.observable(data.DepartmentCode || "");
        this.ProduceFocused = ko.observable(false);
        this.PorterageListFiltered = ko.computed({
            owner: this,
            read: () => {
                var filter = this.DepartmentID();
                //alert(filter);
                if (!this.DepartmentID() || this.DepartmentID() == '') {
                    filter = 'dd';
                }
                return ko.utils.arrayFilter(this.UseLookupTables.primePorterageList(), function (i) {
                    return i.foreignKey == filter;
                });
            }
        });
        this.ConsignmentItemArrivals = ko.observableArray([]);
        if (data.ConsignmentItemArrivals != undefined) {
            for (let i = 0; i < data.ConsignmentItemArrivals.length; i++) {
                this.ConsignmentItemArrivals.push(new ConsignmentItemArrival(data.ConsignmentItemArrivals[i]));
            }
        }

        this.Errors = ko.validation.group(this);
        this.ShowErrors = ko.observable(false);
        this.isUK = ko.computed({
            read: () => {
                return this.CountryName() === "United Kingdom" ? true : false;
                //return this.OriginCountryID() == "00760000-0000-0001-0006-817528069450"  ? true : false; commented out by AT
            }
        });
        this.PackWtUnitID = ko.observable(data.PackWtUnitID || "").extend({
            required: {
                onlyIf: () => { return !this.isUK(); /*this.OriginCountryName != 'United Kingdom';*/ }
            }
        });
        this.hasPackWeight = ko.computed({
            read: () => {
                if (this.PackWeight() === undefined || this.PackWeight() === null || this.PackWeight() === "") {
                    return false;
                } else return true;
            }
        });
    }
    serverErrors = ko.observableArray([]);
    showError = function (item) {
        // if ((item == undefined || !item.isValid()) && this.ShowErrors()) {
        if ((item == undefined) && this.ShowErrors()) {
            return true;
        }

        return false;
    }

    addReturn = function () {
        this.ReturnsCollection().ConsignmentItemPriceReturnModels.push(new ConsignmentItemPriceReturn(undefined));
    }

    removeReturn = function (index: number) {
        var item = this.ReturnsCollection().ConsignmentItemPriceReturnModels.splice(index, 1)[0];

        this.ReturnsCollection().ConsignmentItemPriceReturnModelsDeleted.push(item);
        this.ReturnsCollection().ConsignmentItemPriceReturnModels.remove(item);
    }

    deleteReturns = function () {
        this.ReturnsCollection().ConsignmentItemPriceReturnModels([]);

    }

    saveReturns = function (consignmentCreatedDate) {
        //debugger;
        var self = this;
        self.ReturnsCollection().ConsignmentCreatedDate = consignmentCreatedDate;

        var promise =
            $.ajax({
                url: "/api/Consignment/EditReturnsConsignmentItem/",
                cache: false,
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: ko.toJSON(self.ReturnsCollection()),
                dataType: "json",
                success: function (result) {

                },
                error: function (jqXHR, textStatus, errorThrown) {

                }
            }).fail(
                function (xhr, textStatus, err) {
                    //alert(err);
                });
        promise.done(function (data) {
            //debugger;
            self.ReturnsCollection().ConsignmentItemPriceReturnModels([]);
            if (data.ConsignmentItemPriceReturnModels != undefined) {
                for (let i = 0; i < data.ConsignmentItemPriceReturnModels.length; i++) {
                    self.ReturnsCollection().ConsignmentItemPriceReturnModels.push(new ConsignmentItemPriceReturn(data.ConsignmentItemPriceReturnModels[i]));
                }
            }
            self.ReturnsCollection().ConsignmentItemPriceReturnModelsDeleted([]);
        });
    }

    validateGuid = function (val) {
        return val != undefined && val != null && val != "" && val != "00000000-0000-0000-0000-000000000000";
    }

    validatePackWeightNotUK = function (val) {

        return val != undefined && val != null && val != "" && val != "00000000-0000-0000-0000-000000000000";
    }

    packWeightRequired = function (val, observable) {
        if (observable.CountryName() !== "United Kingdom") {
            if (val === undefined || val === null || val === "") return false;
        }
        return true;
    }

    showProduceNameRequired = function (item) {
        return !this.validateProduceName(item(), this);
    }

    validateProduceName = function (val, observable) {
        if (val == undefined || val == null || val == "")
            return false;
        return true;
    }

    showDepartmentRequired = function (item) {
        return !this.validateDepartmentRow(item(), this);
    }

    validateDepartmentRow = function (val, observable) {
        if (val == undefined || val == null || val == "")
            return false;
        return true;
    }

    showQuantityExpectedRequired = function (item) {
        return !this.validateQuantityExpected(item(), this);
    }

    validateQuantityExpected = function (val, observable) {
        if (val == undefined || val == null || val == "") {
            // debugger;
            return false;
        }
        //debugger;
        return true;
    }

    showExpectedQuantityRequired = function (item) {
        return !this.validateExpectedQuantity(item(), this);
    }

    validateExpectedQuantity = function (val, observable) {
        if (val == undefined || val == null || val == "")
            return false;
        return true;
    }

    showReturnRequired = function (item) {
        return !this.validateReturn(item(), this);
    }

    validateReturn = function (val, observable) {
        if (val == undefined || val == null || val == "" || val < 1) {
            return false;
        }
        return true;
    }

    showCountryNameRequired = function (item) {
        return !this.validateExpectedQuantity(item(), this);
    }

    validateCountryName = function (val, observable) {
        if (val == undefined || val == null || val == "")
            return false;
        return true;
    }

    showBrandRequired = function (item) {
        return !this.validateBrand(item(), this);
    }

    validateBrand = function (val, observable) {
        if (val == undefined || val == null || val == "")
            return false;
        return true;
    }
    /*
    //Brought here without the PackWtUnitList section as unable to filter and get from parent
    GetDBPorterage = function () {
        var self = this;
        //debugger;
        var promise =
            $.ajax({
                url: "/Consignment/GetPackWtUnitAndPorterageList/",
                cache: false,
                type: "GET",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                success: function (result) {
                    //alert(result);
                    //var list = '';
                    // Porterage Selection
                    for (var i = 0; i < result.Porterage.length; i++) {
                        self.PorterageListUnfiltered.push(new MyCustomOptionFKExtended(result.Porterage[i].PorterageCode, result.Porterage[i].PorterageID, result.Porterage[i].DepartmentID));
                        //list = list + result.Porterage[i].PorterageCode + ', ';
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {

                    console.log(errorThrown);
                }
            }).fail(
                function (xhr, textStatus, err) {
                    console.log("error getting the item defaults" + err);
                    //alert(err);
                });
        promise.done(function (data) {
        });
    }
    */
}

 