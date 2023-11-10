angular.module('AdminLayoutApp', [])
    .controller('paymentDetailController', function ($scope, $http) {
        $scope.posBankDetailItem;

        $scope.getURLParameter = function () {
            var sPageURL = window.location.href;
            var indexOfLastSlash = sPageURL.lastIndexOf("/");

            if (indexOfLastSlash > 0 && sPageURL.length - 1 !== indexOfLastSlash)
                return sPageURL.substring(indexOfLastSlash + 1);
            else
                return 0;
        };

        $scope.loadPosOfBankData = function () {
            //fireCustomLoading(true);
            $http({
                method: "POST",
                url: "/Admin/AdminPayment/GetPosOfBankList",
                headers: { "Content-Type": "Application/json;charset=utf-8" },
                data: { posBankId: $scope.selectedPosBankId }

            }).then(function (response) {
                //fireCustomLoading(false);

                $scope.posOfBankList = response.data;

                $scope.originalPosOfBankData = angular.copy($scope.posOfBankList);
            });
        };

        $scope.checkVisible = function (menu) {

            if ($scope.posBankDetailItem === undefined)
                return false;

            if ($scope.posBankDetailItem.MenuStr.indexOf('<' + menu + '>') !== -1)
                return true;
            else
                return false;

        };

        $scope.loadPosBankDetail = function () {
            //fireCustomLoading(true);
            $http({
                method: "POST",
                url: "/Admin/AdminPayment/GetPosBankDetail",
                headers: { "Content-Type": "Application/json;charset=utf-8" },
                data: { posBankId: $scope.selectedPosBankId }

            }).then(function (response) {
                //fireCustomLoading(false);

                $scope.posBankDetailItem = response.data;
                $scope.originalPosBankDetail = angular.copy($scope.posBankDetailItem);
            });
        };

        $scope._3dOptions = [
            {
                _3dSecureSelectionInt: 0,
                Text: "3D Sanal Pos"
            },
            {
                _3dSecureSelectionInt: 1,
                Text: "3D'siz Sanal Pos"
            },
            {
                _3dSecureSelectionInt: 2,
                Text: "Kullanıcıya Sor"
            },
        ];

        $scope.changeSwitch = function (posItem) {
            //fireCustomLoading(true);
            $http({
                method: "POST",
                url: "/Admin/AdminPayment/UpdatePosOfBank",
                headers: { "Content-Type": "Application/json;charset=utf-8" },
                data: { posOfBankItem: posItem }

            }).then(function (response) {
                //fireCustomLoading(false);
                iziToast.show({
                    message: response.data.Message,
                    position: 'topCenter',
                    color: response.data.Color,
                    icon: response.data.Icon
                });
            });
        };

        $scope.loadPosInstallment = function () {
            //fireCustomLoading(true);
            $http({
                method: "POST",
                url: "/Admin/AdminPayment/GetPosInstallment",
                headers: { "Content-Type": "Application/json;charset=utf-8" },
                data: { posBankId: $scope.selectedPosBankId }

            }).then(function (response) {
                //fireCustomLoading(false);
                $scope.posInstallment = response.data;
            });
        };

        $scope.saveDetailItem = function () {
            //fireCustomLoading(true);
            $http({
                method: "POST",
                url: "/Admin/AdminPayment/SaveDetailItem",
                headers: { "Content-Type": "Application/json;charset=utf-8" },
                data: { selectedItem: $scope.posBankDetailItem, installmentList: $scope.posInstallment }

            }).then(function (response) {
                //fireCustomLoading(false);

                $scope.posBankDetailItem.Id = response.data.ResultId;
                $scope.loadPosInstallment();
                iziToast.show({
                    message: response.data.Message,
                    position: 'topCenter',
                    color: response.data.Color,
                    icon: response.data.Icon
                });
            });
        };

        $(document).ready(function () {
            $scope.selectedPosBankId = $scope.getURLParameter();
            $scope.loadPosOfBankData();
            $scope.loadPosBankDetail();
            $scope.loadPosInstallment();
            firePriceFormat();
            firePriceFormatNumberOnly();
        });

        $scope.$on('ngRepeatPosInstallmentFinished', function (ngRepeatPosInstallmentFinishedEvent) {
            firePriceFormat();
            firePriceFormatNumberOnly();
        });
        
    }).directive('onFinishRender', function ($timeout) {
        return {
            restrict: 'A',
            link: function (scope, element, attr) {
                if (scope.$last === true) {
                    $timeout(function () {
                        scope.$emit(attr.onFinishRender);
                    });
                }
            }
        };
    });
