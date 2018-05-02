declare class StockBoardModel {
    ProduceGroupRows: KnockoutObservableArray<ProduceGroupModelRow>;
    constructor(data: any);
}
declare class ProduceGroupModelRow {
    ProduceGroups: KnockoutObservableArray<ProduceGroupModel>;
    constructor(data: any, startIndex: any, rowItemCount: any);
}
declare class ProduceGroupModel {
    ProduceGroupId: KnockoutObservable<string>;
    ProduceGroupName: KnockoutObservable<string>;
    ProduceItems: KnockoutObservableArray<ProduceModel>;
    TotalBalance: KnockoutComputed<number>;
    TotalSold: KnockoutComputed<number>;
    TotalOversold: KnockoutComputed<number>;
    constructor(data: any);
    BalanceStyle: () => string;
    OversoldStyle: () => string;
}
declare class ProduceModel {
    ProduceName: KnockoutObservable<string>;
    ExpectedQuantity: KnockoutObservable<number>;
    TicketItemQuantity: KnockoutObservable<number>;
    RemainingQuantity: KnockoutObservable<number>;
    Oversold: KnockoutComputed<number>;
    constructor(data: any);
    BalanceStyle: () => string;
    OversoldStyle: () => string;
}
