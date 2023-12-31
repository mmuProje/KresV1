
angular.module('AdminLayoutApp', [])
    .controller('studentListController', function ($scope, $http) {

        $scope.studentSearchCriteria =
        {
            T9Text: '',
            StudentStatu: 1,
            StartDate: '',
            EndDate: ''
        };

        //$scope.showPdf = function (item) {
        //    //fireCustomLoading(true);
        //    $http({
        //        method: "POST",
        //        url: "/Admin/AdminPayment/SavePdf",
        //        headers: { "Content-Type": "Application/json;charset=utf-8" },
        //        data: { eItem: item }//, 
        //    }).then(function (response) {
        //        $scope.frameUrl = response.data;

        //        $('#mPdfShow').appendTo("body").modal('show');

        //        //fireCustomLoading(false);

        //    });
        //}

        $scope.tableLinkedShowDetail = function (row) {
            $scope.selectedPayment = row;
            $('#mPaymentDetail').appendTo("body").modal('show');
        };

        $scope.studentSearch = function (studentSearchCriteria) {
            ////fireCustomLoading(true);
            studentSearchCriteria.StartDate = $('#iPaymentStartDate').val();
            studentSearchCriteria.EndDate = $('#iPaymentEndDate').val();

            ////fireCustomLoading(true);
            $http({
                method: "POST",
                url: "/Admin/Student/GetListStudent",
                headers: { "Content-Type": "Application/json;charset=utf-8" },
                data: { studentSearchCriteria: studentSearchCriteria } //, 

            }).then(function (response) {

                $scope.studentlist = response.data;
                //fireCustomLoading(false);

            });
        };

        $scope.ConvertDate = function (input) {
            if (input)
                return $filter('date')(new Date(input), 'dd/MM/yyyy');
            else
                return "-";
        };

        //$scope.askForDelete = function (row, type, status) {
        //    if (!type) {
        //        $.confirm({
        //            title: ('Scripts.Common.Warning').toLanguage(),
        //            content: ("Admin.Scripts.PaymentList.ContentCancel").toLanguage(),
        //            buttons:
        //            {
        //                Ok: {
        //                    text: ("Scripts.Common.Yes").toLanguage(),
        //                    btnClass: 'btn-blue',
        //                    action: function () { $scope.askForDelete(row, true, status); }

        //                },
        //                Cancel: {
        //                    text: ("Scripts.Common.No").toLanguage(),
        //                    btnClass: 'btn-red any-other-class',
        //                    action: function () {
        //                        iziToast.error({
        //                            message: ('Scripts.Common.Warning.Delete.Cancel').toLanguage(),
        //                            position: 'topCenter'
        //                        });

        //                    }
        //                }

        //            }
        //        });
        //    }
        //    else {

        //        $scope.updatepaymentStatus(row.Id, status);
        //    }

        //};

        $scope.updatepaymentStatus = function (id, status) {
            //fireCustomLoading(true);
            $http({
                method: "POST",
                url: "/Admin/AdminPayment/UpdatePaymentStatus",
                headers: { "Content-Type": "Application/json;charset=utf-8" },
                data: { id: id, status: status }

            }).then(function (response) {
                //fireCustomLoading(false);
                iziToast.show({
                    message: response.data.Message,
                    position: 'topCenter',
                    color: response.data.Color,
                    icon: response.data.Icon
                });
                $scope.paymentSearch($scope.studentSearchCriteria);
            });
        };

        $scope.keypressEventPaymentSearch = function (e, studentSearchCriteria) {
            var key;
            if (window.event)
                key = window.event.keyCode; //IE
            else
                key = (e.which) ? e.which : event.keyCode; //Firefox
            if (key === 46)
                e.returnValue = false;
            if (key === 13) {
                $scope.paymentSearch(studentSearchCriteria);
            }
        }

        function setDefaultDate() {
            var today = new Date();
            var dStart = '01/01/' + today.getFullYear();
            $('#iPaymentStartDate').datetimepicker({
                format: "DD/MM/YYYY",
                defaultDate: dStart,
                locale: "tr"
            }).val(dStart);

            $('#iPaymentEndDate').datetimepicker({
                format: 'DD/MM/YYYY',
                locale: 'tr'
            }).val(today.getDate() + '/' + (today.getMonth() + 1) + '/' + today.getFullYear());
        }

        $scope.closeModal = function () {
            $('#mPaymentDetail').modal('hide');
        }

        $scope.clear = function () {
            $scope.studentSearchCriteria = {};
            $scope.studentSearchCriteria =
            {
                T9Text: '',
                PaymentStatu: 1,
                StartDate: '',
                EndDate: ''
            };
            setDefaultDate();
        }

        $(document).ready(function () {
            // setDefaultDate();
        });
    })



