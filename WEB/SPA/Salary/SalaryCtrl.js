app.controller("SalaryCtrl", function ($scope, $http, blockUI, $window) {

    $('#dtDate').datepicker({
        autoclose: true,
        clearBtn: true,
        daysOfWeekHighlighted: '05',
        disableTouchKeyboard: true,
        format: 'M dd, yyyy',
        todayHighlight: true,
        weekStart: 6
    });
    $('#dtDate').keypress(function (e) {
        e.preventDefault();
    });
    getList();
    function getList() {
        $scope.entityList = [{ EmployeeId: 1, EmployeeName: "Rana", EmployeeAddress: "Khilgoan", EmployeeMobileNo: "01670892099", EmployeeEmail: "rana@gmail.com", EmployeeNID: "1992024997456332", EmployeeDesignation: "Manager", EmployeeSalary: "25000", IsActive: 1 }
            , { EmployeeId: 2, EmployeeName: "Arif", EmployeeAddress: "Uttara", EmployeeMobileNo: "01670892565", EmployeeEmail: "arif@gmail.com", EmployeeNID: "19920249974554545", EmployeeDesignation: "Line Man", EmployeeSalary: "10000", IsActive: 1 }];
        $scope.total_count = $scope.entityList.length;
        var begin = ($scope.PerPage * ($scope.currentPage - 1));
        var end = begin + $scope.PerPage;
        $scope.entityListPaged = $scope.entityList.slice(begin, end)
    };
});