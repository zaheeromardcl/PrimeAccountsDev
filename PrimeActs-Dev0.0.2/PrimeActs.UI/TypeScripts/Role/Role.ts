/// <Number path="../../KJScripts/PagingTS.ts" />

class RolesSearch{
    CommonSearchCriteria: KnockoutObservable<any>;
    FromDate: KnockoutObservable<any>;
    ToDate: KnockoutObservable<any>;

    constructor(data)
    {
        const self = this;
        data = data || {};
        self.CommonSearchCriteria = ko.observable(data.CommonSearchCriteria);
        self.FromDate = ko.observable(data.FromDate);
        self.ToDate = ko.observable(data.ToDate);
    }
};

class Roles
{
    Id: KnockoutObservable<any>;
    Name: KnockoutObservable<any>;
    Description: KnockoutObservable<any>;

    constructor(data)
    {
        const self = this;
        data = data || {};
        self.Id = ko.observable(data.Id);
        self.Name = ko.observable(data.Name);
        self.Description = ko.observable(data.Description);   
    }
};

class RoleModel {
    Paging: KnockoutObservable<Paging>;
    roleSearch: KnockoutObservable<RolesSearch>;

    constructor(data) {
        var self = this;
        self.Paging = ko.observable(new Paging(data.RoleEditModels, data.SearchObject));
        self.roleSearch = ko.observable(new RolesSearch(data.SearchObject));
    }

    Search(RolesSearch) {
        const self = this;
        self.Paging().fetchEntitiesWithSearch("Role/Index", RolesSearch);
    };

    ClearSearch(userSearch) {
        var url = "/RolesAdmin/Index";
        window.location.href = url;
    };

    deleteRole(index) {
        var self = this;
        var item = self.Paging().entities.splice(index, 1)[0];

        const url = "/API/Role/RemoveApplicationRole/?roleID=" + item.Id;
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            success: function (data) {
                self.roleSearch().CommonSearchCriteria("");
                self.Paging().fetchEntitiesWithSearch("Role/Index", self.roleSearch);
            }
        }).fail(
            function (xhr, textStatus, err) {
                alert(err);
            });
    };
}
