class LoginViewModel {
    Username: KnockoutObservable<string>;
    Password: KnockoutObservable<string>;
    RememberMe: KnockoutObservable<boolean>;

    Errors: KnockoutValidationErrors;
    ShowErrors: KnockoutObservable<boolean>;

    constructor(data) {
        data = data || {};
        this.Username = ko.observable(data.Username).extend({ required: { message: "Username is required." } });
        this.Password = ko.observable(data.Password).extend({ required: { message: "Password is required." } });
        this.RememberMe = ko.observable(data.RememberMe);

        this.Errors = ko.validation.group(this);
        this.ShowErrors = ko.observable(false);
    }

    ServerErrors = ko.observableArray([]);
    showError = function (item) {
        if ((item == undefined || !item.isValid()) && this.ShowErrors()) {
            return true;
        }

        return false;
    }
}

class LoginPageViewModel {
    Login: KnockoutObservable<LoginViewModel>;

    constructor(data) {
        data = data || {};

        this.Login = ko.observable(new LoginViewModel(data));
    }

    login = function () {
        var data = this;
        data.Login().ServerErrors.removeAll();

        if (data.Login().isValid()) {
            var promise = $.ajax({
                type: 'POST',
                cache: false,
                contentType: 'application/json; charset=utf-8',
                url: '/api/Account/Login/',
                data: ko.toJSON(data.Login())
            });

            promise.done(function (result) {
                window.location.href = result.Url;
            });

            promise.fail(function (jqXHR, textStatus, errorThrown) {
                console.log(jqXHR);
                console.log(JSON.parse(jqXHR.responseText));
                var result = JSON.parse(jqXHR.responseText);
                data.Login().ServerErrors.push(result.Message);
            });
        }
        else {
            data.Login().ShowErrors(true);
            data.Login().Errors.showAllMessages(true);
        }
    }
}