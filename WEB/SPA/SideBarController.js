app.controller("SideBarController", function ($scope, $cookieStore, $window, $location) {
    $scope.loginUserSb = [];
    $scope.loginUserSb = $cookieStore.get('UserData');
    ResetMenuCSS();

    var path = $location.path();
    if (path === '/Home')
        $scope.isHome = true;
    else if (path === '/CustomerProfile')
        $scope.isProfile = true;
    else if (path === '/Package')
        $scope.isPackage = true;
    else if (path === '/AdditionalFees')
        $scope.isAdditionalFees = true;
    else if (path === '/Billing')
        $scope.isBilling = true;
    else if (path === '/Employee')
        $scope.isEmployee = true;
    else if (path === '/Salary')
        $scope.isSalary = true;
    else if (path === '/Report')
        $scope.isReport= true;
    else
        $scope.isHome = true;

    function ResetMenuCSS() {
        //Menu
        $scope.isHome = false;
        $scope.isProfile = false;
        $scope.isPackage = false;
        $scope.isAdditionalFees = false;
        $scope.isBilling = false;
        $scope.isEmployee = false;
        $scope.isSalary = false;
        $scope.isReport = false;
    };

    $scope.setActiveMenu = function (menu) {
        ResetMenuCSS();

        if (menu === 'home')
            $scope.isHome = true;

        else if (menu === 'profile') {
            $scope.isProfile = true;
        }
        else if (menu === 'package') {
            $scope.isPackage = true;
        }
        else if (menu === 'additionalFees') {
            $scope.isAdditionalFees = true;
        }
        else if (menu === 'billing') {
            $scope.isBilling = true;
        }
        else if (menu === 'employee') {
            $scope.isEmployee = true;
        }
        else if (menu === 'salary') {
            $scope.isSalary = true;
        }
        else if (menu === 'report') {
            $scope.isReport = true;
        }
    };
});