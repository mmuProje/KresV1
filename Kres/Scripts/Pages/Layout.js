angular.module('LayoutApp', [])
    .controller('LayoutController', function ($scope, $http) {

        $(document).ready(function () {

            $http({
                method: "POST",
                url: "/Home/GetLanguageList",
                headers: { "Content-Type": "Application/json;charset=utf-8" },
                data: {}
            }).then(function (response) {

                $scope.LanguageStr = response.data;

            });

        });


        // function fireSetLanguageId(id) {
        $scope.fireSetLanguageId = function (id) {
            $.ajax({
                type: "POST",
                url: "/Home/SetCurrentLanguage",
                data: "{id:" + id + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                success: function (data) {
                    if (data)
                    window.location.reload();


                }
            });
        }



    });