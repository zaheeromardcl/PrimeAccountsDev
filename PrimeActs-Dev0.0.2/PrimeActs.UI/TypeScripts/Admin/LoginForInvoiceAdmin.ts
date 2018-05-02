class LoginInvoiceAdminViewModel
{
    Password: KnockoutObservable<string>
    Errors: KnockoutValidationErrors;
    ShowErrors: KnockoutObservable<boolean>;
    ServerErrors = ko.observableArray([]);
    TabPanelName: KnockoutObservable<string>;

    SubscriberReplaceTab: any;

    IsAdminAuthenticated: KnockoutObservable<boolean>;

    constructor() {
        this.Password = ko.observable("").extend({ required: { message: "Password is required." } });

        this.Errors = ko.validation.group(this);
        this.ShowErrors = ko.observable(false);
        this.IsAdminAuthenticated = ko.observable(false);
    }

    // we might reuse this if we want a separate login tab being replaced after login
    //constructor(tabPanelName, subscriberReplaceTab) {
    //    this.TabPanelName = ko.observable(tabPanelName);
    //    this.SubscriberReplaceTab = subscriberReplaceTab;

    //    this.Password = ko.observable("").extend({ required: { message: "Password is required." } });

    //    this.Errors = ko.validation.group(this);
    //    this.ShowErrors = ko.observable(false);
    //}

    login = function () {
        let data = this;
        let password = data.Password();
        let dataToSend = {password: password};

        if (data.isValid()) {
            
            var promise = $.ajax({
                type: 'POST',
                cache: false,
                contentType: 'application/json; charset=utf-8',
                url: '/api/Account/LoginInvoiceAdmin/',
                data: JSON.stringify(dataToSend)
            });

            promise.done(function (result) {
                //window.location.href = result.Url;
                data.IsAdminAuthenticated(true);
                //data.OpenDetails(password);
            });

            promise.fail(function (jqXHR, textStatus, errorThrown) {
                var result = JSON.parse(jqXHR.responseText);
                data.ServerErrors.push(result.Message);
                data.IsAdminAuthenticated(false);
            });
        }
        else {
            data.ShowErrors(true);
            data.Errors.showAllMessages(true);
        }
    }

    showError = function (item) {
        if ((item == undefined || !item.isValid()) && this.ShowErrors()) {
            return true;
        }

        return false;
    }

    OpenDetails = function (data) {
        // the code below opens the details in a new tab
        //debugger;
        //data = "00760000-0000-0006-8353-081962302751";
        //let pn = this.TabPanelName();
        //var options = {
        //    TabTitle: "loading...",
        //    PanelName: pn,
        //    NewPanelName: "PurchaseInvoiceDetails",
        //    UriParam: data
        //};
        //this.SubscriberReplaceTab.notifySubscribers(options, "save");

        // replace the tab
        
        let pn = this.TabPanelName();
        this.SubscriberReplaceTab.notifySubscribers({
            PanelName: pn,
            NewPanelName: "InvoiceAdmin",
            UriParam: data
        }, "save");
    }
}