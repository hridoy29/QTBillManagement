app.controller("HomeController", function ($scope, $http, blockUI) {
    $scope.lsitBlock = blockUI.instances.get('lsitBlock');
    $scope.showList = false;
    getList();

    setTimeout(function () {
        $('.mycounter').counterUp({
            delay: 10,
            time: 1000
        });
    }, 150);


    function getList() {
        $scope.lsitBlock.start();
        $http({
            url: "/Customer/Get",
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            if (data.length) {
                $scope.lsitBlock.stop();

                angular.forEach(data, function (aCustomer) {
                    aCustomer.LasyPayDate = ((aCustomer.BillDueInDay + aCustomer.BillGenerationDay) + "-Feb-2020");
                })

                $scope.entityList = data;
                $scope.entityListSearch = $scope.entityList;
                $scope.entityListActive = Enumerable.From($scope.entityList).Where("$.IsActive === true").ToArray();
                $scope.entityListInactive = Enumerable.From($scope.entityList).Where("$.IsActive === false").ToArray();
            }
            else {
                $scope.lsitBlock.stop();
                alertify.log('System could not retrive information, please refresh page', 'error', '10000');
            }

        }).error(function (data2) {
            $scope.lsitBlock.stop();
            alertify.log('Unknown server error', 'error', '10000');
        });
    };

    $scope.showCustomers = function (type) {
        $scope.listType = type;
        $scope.showList = true;

        if (type === 'Active ')
            $scope.entityListSearch = $scope.entityListActive;
        else if (type === 'Inactive ')
            $scope.entityListSearch = $scope.entityListInactive;
        else
            $scope.entityListSearch = $scope.entityList;
    }
});