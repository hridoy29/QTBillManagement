var app = angular.module('csmApp', ['ngRoute', 'ngCookies', 'ngAnimate', 'ngMaterial', 'jkAngularRatingStars', 'blockUI', 'angularUtils.directives.dirPagination']);

app.config(function ($routeProvider, blockUIConfig) {
    $routeProvider
        .when('/Home', {
            templateUrl: '/SPA/Home/Home.html',
            controller: 'HomeController',
            //resolve: {
            //    "check": function ($cookieStore) {
            //        var login = $cookieStore.get('UserData');
            //        if (angular.isUndefined(login) || login == null) {
            //            //window.location = '/Home/Login';
            //        }
            //    }
            //}
        })
        .when('/CustomerProfile', {
            templateUrl: '/SPA/CustomerProfile/CustomerProfile.html',
            controller: 'CustomerProfileCtrl',
            //resolve: {
            //    "check": function ($cookieStore) {
            //        var login = $cookieStore.get('UserData');
            //        if (angular.isUndefined(login) || login == null) {
            //            //window.location = '/Home/Login';
            //        }
            //    }
            //}
        })
        .when('/Package', {
            templateUrl: '/SPA/Package/Package.html',
            controller: 'PackageCtrl',
            //resolve: {
            //    "check": function ($cookieStore) {
            //        var login = $cookieStore.get('UserData');
            //        if (angular.isUndefined(login) || login == null) {
            //            //window.location = '/Home/Login';
            //        }
            //    }
            //}
        })
        .when('/Report', {
            templateUrl: '/SPA/Report/Report.html',
            controller: 'ReportCtrl',
            //resolve: {
            //    "check": function ($cookieStore) {
            //        var login = $cookieStore.get('UserData');
            //        if (angular.isUndefined(login) || login == null) {
            //            //window.location = '/Home/Login';
            //        }
            //    }
            //}
        })
        .when('/AdditionalFees', {
            templateUrl: '/SPA/AdditionalFees/AdditionalFees.html',
            controller: 'AdditionalFeesCtrl',
            //resolve: {
            //    "check": function ($cookieStore) {
            //        var login = $cookieStore.get('UserData');
            //        if (angular.isUndefined(login) || login == null) {
            //            //window.location = '/Home/Login';
            //        }
            //    }
            //}
        })
        .when('/Billing', {
            templateUrl: '/SPA/Billing/Billing.html',
            controller: 'BillingCtrl',
            //resolve: {
            //    "check": function ($cookieStore) {
            //        var login = $cookieStore.get('UserData');
            //        if (angular.isUndefined(login) || login == null) {
            //            //window.location = '/Home/Login';
            //        }
            //    }
            //}
        })
        .when('/Employee', {
            templateUrl: '/SPA/Employee/Employee.html',
            controller: 'EmployeeCtrl',
            //resolve: {
            //    "check": function ($cookieStore) {
            //        var login = $cookieStore.get('UserData');
            //        if (angular.isUndefined(login) || login == null) {
            //            //window.location = '/Home/Login';
            //        }
            //    }
            //}
        })
        .when('/Salary', {
            templateUrl: '/SPA/Salary/Salary.html',
            controller: 'SalaryCtrl',
            //resolve: {
            //    "check": function ($cookieStore) {
            //        var login = $cookieStore.get('UserData');
            //        if (angular.isUndefined(login) || login == null) {
            //            //window.location = '/Home/Login';
            //        }
            //    }
            //}
        })
        .when('/', {
            templateUrl: '/SPA/Home/Home.html',
            controller: 'HomeController',
            //resolve: {
            //    "check": function ($cookieStore) {
            //        var login = $cookieStore.get('UserData');
            //        if (angular.isUndefined(login) || login == null) {
            //            //window.location = '/Home/Login';
            //        }
            //    }
            //}
        })
        .otherwise({ redirectTo: '/' });

    blockUIConfig.template = '<div class="block-ui-overlay"></div><div class="block-ui-message-container"> <img src="../img/loading.gif" /> <h4><strong>LOADING...</strong></h4> </div>'
});