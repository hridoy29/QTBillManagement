app.controller("CustomerProfileCtrl", function ($scope, $http, blockUI) {
    $scope.DefaultPerPage = 20;
    $scope.currentPage = 1;
    $scope.PerPage = $scope.DefaultPerPage;
    $scope.total_count = 0;
    $scope.entityList = [];
    $scope.entityListPaged = [];
    $scope.additionalFeeList = [];
    $scope.additionalFeeAmountList = [];
    $scope.entryBlock = blockUI.instances.get('entryBlock');
    $scope.lsitBlock = blockUI.instances.get('lsitBlock');
    clear();
    getList();
    getNotificationActive();
    getAdditionalFeeActive();
    getPackageType();

    function clear() {
        $scope.entity = { CustomerId: 0, IsActive: true };
        //$scope.additionalFeeAmountList = {};
        $("#txtFocus").focus();
    };

    function getNotificationActive() {
        $scope.notificationList = [{ Notification: 1, NotificationTitle: "Email" }, { Notification: 2, NotificationTitle: "SMS" }, { Notification: 3, NotificationTitle: "Both" }];
    };

    function getAdditionalFeeActive() {
        var where = "IsActive = 1";
        $http({
            url: '/AdditionalFee/GetDynamic?where=' + where + '&orderBy=AdditionalFeeName',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            if (data.length) {
                $scope.additionalFeeList = data;
            }
        })
    };

    $scope.addAdditionalFee = function (aDetail, index) {

        $scope.additionalFeeAmountList.push(aDetail);

    };

    function getPackageType() {
        $http({
            url: '/Package/Get',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            if (data.length) {
                $scope.packageTypeList = data;
            }
        });
    }

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
                    aCustomer.LasyPayDate = ((aCustomer.BillDueInDay + aCustomer.BillGenerationDay ) + "-Feb-2020");
                })

                $scope.entityList = data;
                $scope.total_count = data.length;

                var begin = ($scope.PerPage * ($scope.currentPage - 1));
                var end = begin + $scope.PerPage;
                $scope.entityListPaged = $scope.entityList.slice(begin, end);
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

    function submitRequest(trnType) {
        var params = JSON.stringify({ obj: $scope.entity, additionalFeeList: $scope.additionalFeeAmountList, transactionType: trnType });

        $http.post('/Customer/Post', params).success(function (data) {
            $scope.entryBlock.start();
            if (data != '') {
                if (data.indexOf('successfully') > -1) {
                    $scope.entryBlock.stop();
                    getList();
                    alertify.log(data, 'success', '5000');
                    $scope.resetForm();
                    
                }
                else {
                    $scope.entryBlock.stop();
                    alertify.log('System could not execute the operation. ' + data, 'error', '10000');
                }
            }
            else {
                $scope.entryBlock.stop();
                alertify.log('System could not execute the operation.', 'error', '10000');
            }
        }).error(function () {
            $scope.entryBlock.stop();
            alertify.log('Unknown server error', 'error', '10000');
        });
    };

    $scope.searchCustomer = function (isSearch) {
        var searchList = [];
        if (isSearch) {
            if (angular.isUndefined($scope.searchText) || $scope.searchText === null || $scope.searchText === '')
                return;

            searchList = Enumerable.From($scope.entityList).Where("$.CustomerId === " + $scope.searchText).ToArray();

            if (!searchList.length) {
                alertify.log('No data found', 'error', '5000');
                return;
            }
        }
        else {
            searchList = $scope.entityList;
            $scope.searchText = null;
        }

        $scope.currentPage = 1;
        $scope.PerPage = $scope.DefaultPerPage;
        $scope.total_count = searchList.length;

        var begin = ($scope.PerPage * ($scope.currentPage - 1));
        var end = begin + $scope.PerPage;
        $scope.entityListPaged = searchList.slice(begin, end);
    };

    $scope.GetPaged = function (curPage) {
        $scope.currentPage = curPage;
        $scope.PerPage = (angular.isUndefined($scope.PerPage) || $scope.PerPage == null) ? $scope.DefaultPerPage : $scope.PerPage;

        if ($scope.PerPage > 100) {
            $scope.PerPage = 100;
            alertify.log('Maximum record  per page is 100', 'error', '5000');
        }
        else if ($scope.PerPage < 1) {
            $scope.PerPage = 1;
            alertify.log('Minimum record  per page is 1', 'error', '5000');
        }

        var begin = ($scope.PerPage * (curPage - 1));
        var end = begin + $scope.PerPage;

        $scope.entityListPaged = $scope.entityList.slice(begin, end);
    }

    $scope.saveImage = function (mobileNumber) {
        $http({
            url: '/Customer/QrCode?MobileNumber=' + mobileNumber,
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.path = "";
            $scope.path = "../../Images/QrCode.jpg";
        })
    };

    $scope.post = function (trnType) {
        var where = "CustomerName = '" + $scope.entity.CustomerName + "'";
        if ($scope.entity.CustomerId > 0)
            where += " AND CustomerId <> " + $scope.entity.CustomerId;

        $http({
            url: '/Customer/GetDynamic?where=' + where + '&orderBy=CustomerName',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            if (data.length > 0) {
                alertify.log($scope.entity.CustomerName + ' already exists!', 'already', '5000');
                $('#txtFocus').focus();
            } else {
                if (trnType === 'save') {
                    trnType = $scope.entity.CustomerId === 0 ? "INSERT" : "UPDATE";
                    submitRequest(trnType);
                }

                else {
                    trnType = "DELETE";

                    alertify.set({
                        labels: {
                            ok: "Yes",
                            cancel: "No"
                        },
                        buttonReverse: true
                    });

                    alertify.confirm('Are you sure to delete?', function (e) {
                        if (e) {
                            submitRequest(trnType);
                        }
                    });
                }
            }
        });
    };

    $scope.rowClick = function (obj) {
        $scope.entity = obj;
        $('#txtFocus').focus();
    };

    $scope.resetForm = function () {
        clear();
        $scope.frm.$setUntouched();
        $scope.frm.$setPristine();
    };


})