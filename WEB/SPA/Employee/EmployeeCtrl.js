app.controller("EmployeeCtrl", function ($scope, $http, blockUI) {
    $scope.DefaultPerPage = 20;
    $scope.currentPage = 1;
    $scope.PerPage = $scope.DefaultPerPage;
    $scope.total_count = 0;
    $scope.entityList = [];
    $scope.entityListPaged = [];
    $scope.entryBlock = blockUI.instances.get('entryBlock');
    $scope.lsitBlock = blockUI.instances.get('lsitBlock');
    clear();
    getList();

    function clear() {
        $scope.entity = { EmployeeId: 0, IsActive: true };
        $("#txtFocus").focus();
    };
 


    function getList() {
        $scope.entityList = [{ EmployeeId: 1, EmployeeName: "Rana", EmployeeAddress: "Khilgoan", EmployeeMobileNo: "01670892099", EmployeeEmail: "rana@gmail.com", EmployeeNID: "1992024997456332", EmployeeDesignation: "Manager", EmployeeSalary: "25000", IsActive: 1 }
                             ,{ EmployeeId: 2, EmployeeName: "Arif", EmployeeAddress: "Uttara", EmployeeMobileNo: "01670892565", EmployeeEmail: "arif@gmail.com", EmployeeNID: "19920249974554545", EmployeeDesignation: "Line Man", EmployeeSalary: "10000", IsActive: 1 }];
                $scope.total_count = $scope.entityList.length;
                var begin = ($scope.PerPage * ($scope.currentPage - 1));
                var end = begin + $scope.PerPage;
                $scope.entityListPaged = $scope.entityList.slice(begin, end)          
    };

    function submitRequest(trnType) {
        var params = JSON.stringify({ obj: $scope.entity, transactionType: trnType });

        $http.post('/Employee/Post', params).success(function (data) {
            $scope.entryBlock.start();
            if (data != '') {
                if (data.indexOf('successfully') > -1) {
                    $scope.entryBlock.stop();
                    $scope.resetForm();
                    getList();
                    alertify.log(data, 'success', '5000');
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

    $scope.post = function (trnType) {
        var where = "EmployeeName = '" + $scope.entity.EmployeeName + "'";
        if ($scope.entity.EmployeeId > 0)
            where += " AND EmployeeId <> " + $scope.entity.EmployeeId;

        $http({
            url: '/Employee/GetDynamic?where=' + where + '&orderBy=EmployeeName',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            if (data.length > 0) {
                alertify.log($scope.entity.EmployeeName + ' already exists!', 'already', '5000');
                $('#txtFocus').focus();
            } else {
                if (trnType === 'save') {
                    trnType = $scope.entity.EmployeeId === 0 ? "INSERT" : "UPDATE";
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