angular.module('AdminLayoutApp', [])
    .controller('paymentController', function ($scope, $http) {

        $scope.loadData = function () {
            //fireCustomLoading(true);        
            $http({
                method: "POST",
                url: "/Admin/AdminPayment/GetBankList",
                headers: { "Content-Type": "Application/json;charset=utf-8" },
                data: {}

            }).then(function (response) {
                //fireCustomLoading(false);

                $scope.bankList = response.data;
                $scope.originalBankData = angular.copy($scope.bankList);
            });
        };

        $scope.changeSwitch = function (item) {
            $scope.SelectedItem = angular.copy(item);

            angular.forEach($scope.bankList, function (data) {
                if ($scope.SelectedItem.MainPos === true) {
                    data.MainPos = false;
                }

                if ($scope.SelectedItem.OneShotBank === true) {
                    data.OneShotBank = false;
                }

                if ($scope.SelectedItem.MainPos && data.Id === $scope.SelectedItem.Id)
                    data.MainPos = true;

                if ($scope.SelectedItem.OneShotBank && data.Id === $scope.SelectedItem.Id)
                    data.OneShotBank = true;

            });
            if (item.Active && item.PosBankDetailId === 0) {
                iziToast.error({
                    title: 'Hatalı',
                    message: 'İşlem Sırasında Hata Oluştu',
                    position: 'topCenter'
                });
                item.Active = false;
                $scope.SelectedItem.Active = false;
                return;
            }
            //fireCustomLoading(true);

            $http({
                method: "POST",
                url: "/Admin/AdminPayment/UpdatePosBank",
                headers: { "Content-Type": "Application/json;charset=utf-8" },
                data: { selectedItem: $scope.SelectedItem }

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

        $(document).ready(function () {
            $scope.loadData();

        });



    });