/// <Number path="../../KJScripts/PagingTS.ts" />

class UserSearch{

    ToDate: KnockoutObservable<any>;
    FromDate: KnockoutObservable<any>;
    CommonSearchCriteria: KnockoutObservable<any>;

    constructor(data) {
    const self = this;
    data = data || {};
    self.CommonSearchCriteria = ko.observable(data.CommonSearchCriteria);
    self.FromDate = ko.observable(data.FromDate);
    self.ToDate = ko.observable(data.ToDate);
    }
};

class User{
    Id: KnockoutObservable<any>;
    Email: KnockoutObservable<string>;
    UserName: KnockoutObservable<string>;
    Firstname: KnockoutObservable<string>;
    Lastname: KnockoutObservable<string>;
    Nickname: KnockoutObservable<string>;
    IsActive: KnockoutObservable<any>;
    LastLoggedOn: KnockoutObservable<any>;
    DepartmentId: KnockoutObservable<any>;
    CompanyId: KnockoutObservable<any>;
    DivisionId: KnockoutObservable<any>;

    constructor(data) {
    const self = this;
    data = data || {};
    self.Id = ko.observable(data.Id);
    self.Email = ko.observable(data.Email || "");
    self.UserName = ko.observable(data.UserName || "");
    self.Firstname = ko.observable(data.Firstname || "");
    self.Lastname = ko.observable(data.Lastname || "");
    self.Nickname = ko.observable(data.Nickname || "");
    self.IsActive = ko.observable(data.IsActive);
    self.LastLoggedOn = ko.observable(data.LastLoggedOn);
    self.DepartmentId = ko.observable(data.DepartmentId);
    self.CompanyId = ko.observable(data.CompanyId);
    self.DivisionId = ko.observable(data.DivisionId);
    }
};

class UserModel{
    Paging: KnockoutObservable<Paging>;
    userSearch: KnockoutObservable<UserSearch>;

    constructor(data) {
        data = data || {};
        var self = this;
        self.Paging = ko.observable(new Paging(data.UserEditModels, data.SearchObject));
        self.userSearch = ko.observable(new UserSearch(data.SearchObject));
    }
    
    Search(userSearch) {
        const self = this;
        
        self.Paging().fetchEntitiesWithSearch("UsersAdmin/Index", userSearch);
    };
    ClearSearch(userSearch) {
        var url = "/UsersAdmin/Index";
        
        window.location.href = url;
    };

    DeleteUser(index) {
        var self = this;

        var item = self.Paging().entities.splice(index, 1)[0];
        var dataToSend = { ApplicationUserId: item.Id };

        const url = "/api/UsersAdmin/DeleteUser/" + item.Id;

        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            //data: item.Id,//JSON.stringify(dataToSend),
            success: function (result) {

                self.Paging().entities.remove(item);
            }
        }).fail(
            function (xhr, textStatus, err) {
                //alert(err);
            });

    }
    
};