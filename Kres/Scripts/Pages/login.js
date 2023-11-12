angular.module('LoginApp', [])
    .controller('LoginController', function ($scope, $http) {

        $scope.keyPressedBasket = function (e) {
            var key, brwsr, keyCtrl = false, keyShift = false, keyAlt = false, keyMeta = false;

            if (window.event) { // Internet Explorer
                if (window.event.shiftKey) keyShift = true;
                if (window.event.ctrlKey) keyCtrl = true;
                if (window.event.metaKey) keyMeta = true;
                if (window.event.altKey) keyAlt = true;
                key = window.event.keyCode;
                brwsr = true;
            }
            else if (e) { // Firefox
                if (e.shiftKey) keyShift = true;
                if (e.ctrlKey) keyCtrl = true;
                if (e.metaKey) keyMeta = true;
                if (e.altKey) keyAlt = true;
                key = e.which;
                brwsr = false;
            }
            else { // Other Browsers
                if (event.shiftKey) keyShift = true;
                if (event.ctrlKey) keyCtrl = true;
                if (event.metaKey) keyMeta = true;
                if (event.altKey) keyAlt = true;
                key = event.keyCode;
                brwsr = false;
            }

            function fireReturnFalse(e) {
                if (e.preventDefault) {
                    e.preventDefault();
                    e.stopPropagation();
                }
                else
                    e.returnValue = false;

                if (brwsr === true) {
                    window.event.returnValue = false;
                    event.keyCode = 0;
                }
                else {
                    e.cancelbubble = true;
                    e.returnvalue = false;
                    e.keycode = false;
                }

                return false;
            }

            function fireReturnTrue() {
                return true;
            }

            if (keyShift || keyCtrl || keyAlt || keyMeta || (key >= 112 && key <= 123)) {
                fireReturnFalse(e);
            }
            else if (key === 13) {
                $scope.updateDiscountData();
                fireReturnFalse(e);
            }
            else if (key === 8 || key === 9 || key === 37 || key === 39) {
                fireReturnTrue();
            }
            else if (key < 48 || key > 57) {
                fireReturnFalse(e);
            }
            else {
                fireReturnTrue();
            }
        };

        $scope.login = function () {

            var Code = $('#usercode').val();
            var Password = $('#password').val();
            var loginType = '1';

            $http({
                method: "POST",
                url: "/Login/sLogin",
                headers: { "Content-Type": "Application/json;charset=utf-8" },
                data: { Code: Code, Password: Password, loginType: loginType }
            }).then(function (response) {

                if (response.data.Statu == "error") {
                    iziToast.show({
                        title: response.data.Statu,
                        message: response.data.Message,
                        position: 'topCenter',
                        color: 'red',
                        icon: response.data.Icon
                    });
                }
                else {
                    //iziToast.show({
                    //    title: response.data.Statu,
                    //    message: response.data.Message,
                    //    position: 'topCenter',
                    //    color: 'green',
                    //    icon: response.data.Icon
                    //});

                    window.location.href = "/Home/Index";
                }

            });
        }

        $(document).ready(function () {


        });

    });