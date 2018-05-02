class MyCustomOption {
    display: string;
    itsValue: string;

    constructor(display: string, itsValue: string) {
        this.display = display;
        this.itsValue = itsValue;
    }
}
class MyCustomOptionExtended {
    itemID: string;
    itemValue: string;
    itemCode: string;

    constructor(itemID: string, itemValue: string, itemCode: string) {
        this.itemID = itemID;
        this.itemValue = itemValue;
        this.itemCode = itemCode;
    }
}
class MyCustomOptionFKExtended {
    itemID: string;
    itemName: string;
    itemCode: string;
    foreignKey: string;
    isActive: boolean;
    option: boolean;
    nameCode: KnockoutComputed<string>;
    codeName: KnockoutComputed<string>;

    constructor(itemID: string, itemName: string, itemCode: string, foreignKey: string, isActive: boolean, option: boolean = null) {
        this.itemID = itemID;
        this.itemName = itemName;
        this.itemCode = itemCode;
        this.foreignKey = foreignKey;
        this.isActive = isActive;
        this.option = option;
        this.nameCode = ko.computed({
            owner: this,
            read: () => {
                let retVal: string = this.itemName + ' (' + this.itemCode + ')'
                return retVal;
            }
        });
        this.codeName = ko.computed({
            owner: this,
            read: () => {
                let retVal: string = this.itemCode + ' - ' + this.itemName
                return retVal;
            }
        });
    }
}
