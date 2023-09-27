var adminApp = angular.module('adminApp', ['ngSanitize', 'ngTable', 'ngResource', 'pascalprecht.translate']);
var reportApp = angular.module('reportApp', ['ngSanitize', 'ngTable', 'ngResource', 'pascalprecht.translate']);

adminApp.filter("jsonDate", function () {

    return function (x) {
        return new Date(parseInt(x.substr(6)));
    };
}).filter("dateFilter", function () {
    return function (item) {
        if (item != null) {
            return new Date(parseInt(item.substr(6)));
        }
        return "";
    };
}).filter("dateFilterB2B", function () {
    return function (item) {
        if (item != null) {
            return new Date(item);
        }
        return "";
    };
}).filter("dateFilterErp", function () {
    return function (item) {
        if (item != null) {
            return new Date(Date.parse(item.substr(0, 10)));
        }
        return "";
    };
}).filter("priceFilter", function () {
    return function (item) {
        if (item != null && item !== undefined) {
            return $.number(parseFloat(item), 2, ',', '.');
        }
        return "";
    };
});

adminApp.run(['$rootScope', '$http', '$interval', '$translate', function ($rootScope, $http, $interval, $translate) {

    $rootScope.authorityAccessList = [];
    $rootScope.getAuthorityAccessList = function (type) {
        $http({
            method: "POST",
            url: "/Admin/Home/GetAuthorityAccessList",
            headers: { "Content-Type": "Application/json;charset=utf-8" },
            data: { type: type }

        }).then(function (response) {
            $rootScope.authorityAccessList = response.data;
        });
    };

    $rootScope.checkAuthorityAccess = function (id) {
        return $rootScope.authorityAccessList.filter(s => s.Id == id).length > 0;
    }


    String.prototype.toLanguage = function () {
        return $translate.instant(this);
    };

    if (localStorage.getItem("CurrentCompanyInformation") != null) {
        $rootScope.CurrentCompanyInformation = $.parseJSON(localStorage.getItem("CurrentCompanyInformation"));
        $rootScope.CurrentSalesman = $.parseJSON(localStorage.getItem("CurrentSalesman"));
        $rootScope.CurrentLanguage = $.parseJSON(localStorage.getItem("CurrentLanguage"));
        $rootScope.CurrentLanguageList = $.parseJSON(localStorage.getItem("CurrentLanguageList"));
        $rootScope.CurrentAuthorityList = $.parseJSON(localStorage.getItem("CurrentAuthorityList"));
    }
    else
        $http({
            method: "POST",
            url: "/Admin/Home/GetCurrentSessions",
            headers: { "Content-Type": "Application/json;charset=utf-8" },
            data: {}
        }).then(function (response) {

            localStorage.setItem("CurrentCompanyInformation", JSON.stringify(response.data['CompanyInformation']));
            localStorage.setItem("CurrentSalesman", JSON.stringify(response.data['Salesman']));
            localStorage.setItem("CurrentLanguage", JSON.stringify(response.data['Language']));
            localStorage.setItem("CurrentLanguageList", JSON.stringify(response.data['LanguageList']));
            localStorage.setItem("CurrentAuthorityList", JSON.stringify(response.data['CurrentAuthorityList']));


            $rootScope.CurrentCompanyInformation = $.parseJSON(localStorage.getItem("CurrentCompanyInformation"));
            $rootScope.CurrentSalesman = $.parseJSON(localStorage.getItem("CurrentSalesman"));
            $rootScope.CurrentLanguage = $.parseJSON(localStorage.getItem("CurrentLanguage"));
            $rootScope.CurrentLanguageList = $.parseJSON(localStorage.getItem("CurrentLanguageList"));
            $rootScope.CurrentAuthorityList = $.parseJSON(localStorage.getItem("CurrentAuthorityList"));
        });



}]);



function translateFilterFactory($parse, $translate) {

    'use strict';

    var translateFilter = function (translationId, interpolateParams, interpolation, forceLanguage) {
        if (!angular.isObject(interpolateParams)) {
            var ctx = this || {
                '__SCOPE_IS_NOT_AVAILABLE': 'More info at https://github.com/angular/angular.js/commit/8863b9d04c722b278fa93c5d66ad1e578ad6eb1f'
            };
            interpolateParams = $parse(interpolateParams)(ctx);
        }

        return $translate.instant(translationId, interpolateParams, interpolation, forceLanguage);
    };

    if ($translate.statefulFilter()) {
        translateFilter.$stateful = true;
    }

    return translateFilter;
}

var b2bApp = angular.module('b2bApp', ['ngSanitize', 'ngTable', 'pascalprecht.translate']).service("B2BServices", ['$http', function ($http) {
    this.getBasketCount = function () {
        $http({
            method: "POST",
            url: "/Home/GetBasketCount",
            headers: { "Content-Type": "Application/json;charset=utf-8" },
            data: {}

        }).then(function (response) {
            var basketCountText = '<i class="fa fa-shopping-cart fa-lg"></i>&nbsp;&nbsp;<span>' + ('Layout.Menu.Cart').toLanguage() + ' [' + response.data.CustomerCart + ']</span>';
            if (response.data.LoginType === 1)
                basketCountText += '<span> - [' + response.data.SalesmanCart + ']</span>';
            $('#layoutBasketCount').html(basketCountText);

            var total = response.data.CustomerCart;
            var basketCountHeadText = '<span>' + ('Layout.Menu.Cart').toLanguage() + '</span><span>[' + response.data.CustomerCart + ']</span>';
            //if (response.data.LoginType === 1) {
            //    basketCountHeadText = '<span>' + ('Layout.Menu.Cart').toLanguage() + '</span><span>[' + response.data.CustomerCart + '] - [' + response.data.SalesmanCart + ']</span>';
            //    total += response.data.SalesmanCart;
            //}
            $('#layoutBasketCountHead').html(basketCountHeadText);
            $('#layaoutBasketCountTotal').html(' <span>' + total + '</span>');



            var offerBasketCountText = '<i class="fa fa-shopping-cart fa-lg"></i>&nbsp;&nbsp;<span>Teklif Sepetim [' + response.data.OfferCart + ']</span>';
            $('#layoutOfferBasketCount').html(offerBasketCountText);
            var offerBasketCountHeadText = '<span>' + 'Teklif Sepeti' + '</span><span>[' + response.data.OfferCart + ']</span>';
            $('#layoutOfferBasketCountHead').html(offerBasketCountHeadText);
            if (response.data.OfferCart > 0)
                $('#layaoutOfferBasketCountTotal').html(' <span  style="background-color:red;color:white" >' + response.data.OfferCart + '</span>');
            else
                $('#layaoutOfferBasketCountTotal').html(' <span>' + response.data.OfferCart + '</span>');


        });


    };


}]).filter("dateFilter", function () {
    return function (item) {
        if (item != null) {
            return new Date(parseInt(item.substr(6)));
        }
        return "";
    };
}).filter("dateFilterErp", function () {
    return function (item) {
        if (item != null) {
            return new Date(Date.parse(item.substr(0, 10)));
        }
        return "";
    };
}).filter("dateFilterB2B", function () {
    return function (item) {
        if (item != null) {
            return new Date(item);
        }
        return "";
    };
}).filter("priceFilter", function () {
    return function (item) {
        if (item !== null && item !== undefined) {
            return $.number(parseFloat(item), 2, ',', '.');
        }
        return "";
    };
});

b2bApp.run(['$rootScope', '$http', '$translate', function ($rootScope, $http, $translate) {


    $http({
        method: "POST",
        url: "/Home/GetCurrentSessions",
        headers: { "Content-Type": "Application/json;charset=utf-8" },
        data: {}
    }).then(function (response) {
        $rootScope.CurrentSessions = response.data;
    });


    $http({
        method: "POST",
        url: "/Home/CustomerAuthorityPage",
        headers: { "Content-Type": "Application/json;charset=utf-8" },
        data: {}
    }).then(function (response) {
        $rootScope.CurrentMenuList = response.data.MenuList;
        $rootScope.CartAccsess = response.data.CartAccsess;
    });
    String.prototype.toLanguage = function () {
        return $translate.instant(this);
    };
    $rootScope.changeLanguage = function (key) {
        $translate.use(key);
    };

    //Para Birimi Kurları
    $rootScope.getCurrencyList = function (pageName) {
        $http({
            method: "POST",
            url: "/Home/GetCurrencyList",
            headers: { "Content-Type": "Application/json;charset=utf-8" },
            data: {
                pageName: pageName
            }
        }).then(function (response) {
            $rootScope.currencyList = response.data;
        });

    };

    $rootScope.backOrderCheck = function () {
        var currentTime = new Date()
        $http({
            method: "POST",
            url: "/Order/GetBackOrderData",
            headers: { "Content-Type": "Application/json;charset=utf-8" },
            data: { yearItemId: 9 }
        }).then(function (response) {
            $rootScope.backOrderList = response.data;
            $http({
                method: "POST",
                url: "/Home/BackOrderCheck",
                headers: { "Content-Type": "Application/json;charset=utf-8" },
                data: { backOrderList: $rootScope.backOrderList }
            }).then(function (response) {
                $rootScope.backOrderAvailableList = response.data;
            });

        });

    };
    $rootScope.$on('ngRepeatBackOrderAvailableListFinished', function (ngRepeatBackOrderAvailableListFinishedEvent) {
        if ($rootScope.backOrderAvailableList.length > 0) {
            $('#modal-reason').on('show.bs.modal', function () {
                $('.selectbox-reasonItem').SumoSelect();
            })
            $('#modal-reason').modal('show');
        }
    });


    $(document).ready(function () {
        $rootScope.backOrderCheck();
    });

}]);


b2bApp.config(["$translateProvider", function ($translateProvider) {
    $.ajax({
        url: '/Home/GetCurrentLanguageInfo',
        method: "POST",
        headers: { "Content-Type": "Application/json;charset=utf-8" },
        async: false,
        success: function (response) {
            $.ajax({
                url: '/Files/language/' + response.CurrentLanguage.UniqueSeoCode + '.json',
                dataType: 'json',
                async: false,
                data: {},
                success: function (data) {
                    $translateProvider.translations(response.CurrentLanguage.UniqueSeoCode, data);
                    $translateProvider.use(response.CurrentLanguage.UniqueSeoCode);
                    $translateProvider.useSanitizeValueStrategy('escape');

                }
            });
        }
    });
}]);

adminApp.config(["$translateProvider", function ($translateProvider) {
    $.ajax({
        url: '/Admin/Home/GetCurrentLanguageInfo',
        method: "POST",
        headers: { "Content-Type": "Application/json;charset=utf-8" },
        async: false,
        success: function (response) {
            $.ajax({
                url: '/Files/language/' + response.CurrentLanguage.UniqueSeoCode + '.json',
                dataType: 'json',
                async: false,
                data: {},
                success: function (data) {
                    $translateProvider.translations(response.CurrentLanguage.UniqueSeoCode, data);
                    $translateProvider.use(response.CurrentLanguage.UniqueSeoCode);
                    $translateProvider.useSanitizeValueStrategy('escape');
                }
            });
        }
    });
}]);