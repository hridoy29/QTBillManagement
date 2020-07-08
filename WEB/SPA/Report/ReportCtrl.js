app.controller("ReportCtrl", function ($scope, $http, blockUI,$window) {
    
    $('#dtFromDate').datepicker({
        autoclose: true,
        clearBtn: true,
        daysOfWeekHighlighted: '06',
        disableTouchKeyboard: true,
        format: 'M dd, yyyy',
        todayHighlight: true,
        weekStart: 6
    });
    $('#dtToDate').datepicker({
        autoclose: true,
        clearBtn: true,
        daysOfWeekHighlighted: '06',
        disableTouchKeyboard: true,
        format: 'M dd, yyyy',
        todayHighlight: true,
        weekStart: 6
    });
    $scope.openCalendar = function (from) {
        switch (from) {
            case 'fromDate':
                $("#dtFromDate").focus();
                break;
            case 'toDate':
                $("#dtToDate").focus();
                break;
            default:
                break;
        }
    };

    $scope.post = function () {
        $window.open("/SPA/Report/RVbilling.aspx?&FromDate=" + $scope.entity.FromDate + "&ToDate=" + $scope.entity.ToDate,
                     "_blank", "width=1115,height=630,left=125,top=25");
    }
})