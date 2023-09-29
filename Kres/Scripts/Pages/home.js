angular.module('LayoutApp', [])
    .controller('LayoutController', function ($scope, $http) {

        $(document).ready(function () {
            $http({
                method: "POST",
                url: "/Home/GetLanguageList",
                headers: { "Content-Type": "Application/json;charset=utf-8" },
                data: {}
            }).then(function (response) {

                $scope.Language = response.data;

            });
        });



    });