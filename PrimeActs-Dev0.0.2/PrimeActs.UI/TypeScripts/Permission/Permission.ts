/// <Number path="../../KJScripts/PagingTS.ts" />

class PermissionsSearch
{
    CommonSearchCriteria: KnockoutObservable<any>;
    ToDate: KnockoutObservable<any>;
    FromDate: KnockoutObservable<any>;

    constructor(data) {
        const self = this;
        data = data || {};
        self.CommonSearchCriteria = ko.observable(data.CommonSearchCriteria);
        self.FromDate = ko.observable(data.FromDate);
        self.ToDate = ko.observable(data.ToDate);
    }
};

class PermissionsModel
{
    Paging: KnockoutObservable<Paging>;
    permissionSearch: KnockoutObservable<PermissionsSearch>;

    constructor(data) {
        this.Paging = ko.observable(new Paging(data.PermissionEditModels, data.SearchObject));
        this.permissionSearch = ko.observable(new PermissionsSearch(data.SearchObject));
    }

    Search(PermissionsSearch) {
        const self = this;
        self.Paging().fetchEntitiesWithSearch("Permission/Index", PermissionsSearch);
    };

    ClearSearch(userSearch) {
        let url = "/Permissions/Index";
        window.location.href = url;
    };
}