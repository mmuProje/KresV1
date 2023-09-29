LayoutApp.controller('LayoutController', function ($scope, $http, B2BServices) {
    //$scope.askAvailable = function (id, type) {
    //    $http({
    //        method: "POST",
    //        url: "/Search/GetAvailable",
    //        headers: { "Content-Type": "Application/json;charset=utf-8" },
    //        data: { id: id }

    //    }).then(function (response) {
    //        if (response.data !== "[]" && response.data !== "") {
    //            var qnt = response.data;
    //            if (qnt !== "")
    //                $scope.askForAdd(id, qnt, type);
    //            else
    //                $scope.addBasket(id, type);
    //        } else {
    //            $scope.addBasket(id, type);
    //        }
    //    });
    //};
    //$scope.askForAdd = function (id, qnt, type) {
    //    $.confirm({
    //        title: ('Scripts.Pages.Search.AskForAdd.Tittle').toLanguage(),
    //        content: ("Scripts.Pages.Search.AskForAdd.Content").toLanguage().replace("{0}", qnt),
    //        buttons:
    //        {
    //            Ok: {
    //                text: ("Scripts.Pages.Search.AskForAdd.Button.Ok.Text").toLanguage(),
    //                btnClass: 'btn-success',
    //                keys: ['enter'],
    //                action: function () { $scope.addBasket(id, type); }

    //            },
    //            Cancel: {
    //                text: ("Scripts.Pages.Search.AskForAdd.Button.Cancel.Text").toLanguage(),
    //                btnClass: 'btn-danger',
    //                keys: ['esc'],
    //                action: function () {
    //                    if (type === 0)
    //                        $('#qty' + id).val('');
    //                    if (type === 1)
    //                        $('#qtyP' + id).val('');
    //                    if (type === 2)
    //                        $('#qtyD' + id).val('');
    //                    if (type === 3)
    //                        $('#qtyM').val('');
    //                    if (type === 7)
    //                        $('#qtyAl').val('');

    //                    iziToast.error({
    //                        message: ('Scripts.Pages.Search.AskForAdd.Button.Cancel.Message').toLanguage(),
    //                        position: 'topCenter'
    //                    });

    //                }
    //            }

    //        }
    //    });
    //};
    //$scope.addBasket = function (id, type) {
    //    var qtyStr;
    //    if (type === 0) {
    //        qtyStr = $('#qty' + id).val();
    //        if (qtyStr === '')
    //            qtyStr = $('#qty' + id).attr('placeholder');
    //    }
    //    else if (type === 1) {
    //        qtyStr = $('#qtyP' + id).val();
    //        if (qtyStr === '')
    //            qtyStr = $('#qtyP' + id).attr('placeholder');
    //    }
    //    else if (type === 2) {
    //        qtyStr = $('#qtyD' + id).val();
    //        if (qtyStr === '')
    //            qtyStr = $('#qtyD' + id).attr('placeholder');
    //    }
    //    else if (type === 3) {
    //        qtyStr = $('#qtyIk' + id).val();
    //        if (qtyStr === '')
    //            qtyStr = $('#qtyIk' + id).attr('placeholder');
    //    }
    //    else if (type === 4) {
    //        qtyStr = $('#qtyIo' + id).val();
    //        if (qtyStr === '')
    //            qtyStr = $('#qtyIo' + id).attr('placeholder');
    //    }
    //    else if (type === 5) {
    //        qtyStr = $('#qtyComp_' + id).val();
    //        if (qtyStr === '')
    //            qtyStr = $('#qtyComp_' + id).attr('placeholder');
    //    }
    //    else if (type === 6) {
    //        qtyStr = $('#qtyIyy' + id).val();
    //        if (qtyStr === '')
    //            qtyStr = $('#qtyIyy' + id).attr('placeholder');
    //    }
    //    else if (type === 7) {
    //        qtyStr = $('#qtyAl' + id).val();
    //        if (qtyStr === '')
    //            qtyStr = $('#qtyAl' + id).attr('placeholder');
    //    }
    //    else {
    //        qtyStr = $('#qtyM').val();
    //        if (qtyStr === '')
    //            qtyStr = $('#qtyM' + id).attr('placeholder');
    //    }
    //    if ($.isNumeric(qtyStr)) {
    //        $http({
    //            method: "POST",
    //            url: "/Search/AddBasket",
    //            headers: { "Content-Type": "Application/json;charset=utf-8" },
    //            data: { id: id, qty: qtyStr }

    //        }).then(function (response) {
    //            var retVal = jQuery.parseJSON(response.data);
    //            if (retVal.statu === "success") {
    //                iziToast.success({
    //                    message: retVal.message,
    //                    position: 'topCenter'
    //                });

    //               // B2BServices.getBasketCount();

    //                if (retVal.cmpAvailableQuantity > 0)
    //                    $scope.showCampaignMessage(id, retVal.cmpAvailableQuantity);
    //            }
    //            else {
    //                iziToast.error({
    //                    //title: 'Hata',
    //                    message: retVal.message,
    //                    position: 'topCenter'
    //                });

    //            }
    //            $('#qty' + id).val('');
    //            $('#qtyP' + id).val('');
    //            $('#qtyD' + id).val('');
    //            $('#qtyAl' + id).val('');
    //        });
    //    }
    //};

    $scope.fireSetLanguageId = function (id) {
        $http({
            method: "POST",
            url: "/Home/SetCurrentLanguage",
            headers: { "Content-Type": "Application/json;charset=utf-8" },
            data: { id: id }
        }).then(function (response) {
            if (response.data)
                window.location.reload();
        });
    }

    $(document).ready(function () {

      //  /*alert($scope.text);*/
      //  $scope.controlRightPanel = false;
      //  $scope.showLeftPanel(-1);
      //  $scope.showRightPanel(-1);
      //  // Fade Menu
      //  $('#fadeLeft').on('click', function () {
      //      $('#leftContainer').removeClass('show');
      //      $('#fadeLeft').fadeOut(500);
      //  });
      //  // Fade Menu
      //  $('#fadeRight').on('click', function () {
      //      $('#rightContainer').removeClass('show-right');
      //      $('#rightContainer').removeClass('menu-active');
      //      $('#fadeRight').fadeOut(500);
      //  });
      //  //Right Menu Close Button
      //  $('#RightMenuClose').on('click', function () {
      //      $('#rightContainer').removeClass('show-right');
      //      $('#rightContainer').removeClass('menu-active');
      //      $('#fadeRight').fadeOut(500);
      //  });

      ////  B2BServices.getBasketCount();


    });



    //$scope.changControl = function (item) {
    //    if (item.Quantity < item.MinOrder || item.Quantity === "Nan" || item.Quantity === undefined) {
    //        item.Quantity = item.MinOrder;
    //        iziToast.error({
    //            message: ('Script.AngularServices.ChangControl.Message').toLanguage(),
    //            position: 'topCenter'
    //        });
    //    }
    //    else {

    //        var qty = parseFloat(item.Quantity) - item.MinOrder;
    //        var k = qty / item.RisingQuantity;
    //        if (qty % item.RisingQuantity == 0)
    //            qty = parseInt(k) * item.RisingQuantity;
    //        else
    //            qty = (parseInt(Math.floor(k)) + 1) * item.RisingQuantity;

    //        item.Quantity = qty + item.MinOrder;


    //        item.SendDisabled = true;

    //    }
    //}

    //$scope.showLeftPanel = function (type) {
    //    $http({
    //        method: "POST",
    //        url: "/Home/GetProductOfDayList",
    //        headers: { "Content-Type": "Application/json;charset=utf-8" },
    //        data: { type: (type === -1 ? 0 : type) }
    //    }).then(function (response) {
    //        $scope.productOfDayList = response.data;
    //        if ($scope.productOfDayList.length === 0)
    //            $('.menu-button_container-left').addClass("hidden");
    //        else
    //            $('.menu-button_container-left').removeClass("hidden");

    //        if (type !== -1) {
    //            $('#leftContainer').addClass('show');
    //            $('#fadeLeft').fadeIn(400);
    //        }

    //    });

    //}



    //$scope.showRightPanel = function (type) {
    //    $http({
    //        method: "POST",
    //        url: "/Home/GetNotificationsList",
    //        headers: { "Content-Type": "Application/json;charset=utf-8" },
    //        data: { type: (type === -1 ? 0 : type) }
    //    }).then(function (response) {
    //        $scope.notificationList = response.data;
    //        if ($scope.notificationList.length === 0)
    //            $('.menu-button_container-right').addClass("hidden");
    //        else
    //            $('.menu-button_container-right').removeClass("hidden");

    //        if (type !== -1) {
    //            //$('#rightContainer').addClass('show-right');
    //            //$('#rightContainer').addClass('menu-active');
    //            //$('#fadeRight').fadeIn(400);
    //            $scope.controlRightPanel = true;
    //        }
    //    });

    //}

    //$scope.closeNoti = function (item) {

    //    $http({
    //        method: "POST",
    //        url: "/Home/CloseNotification",
    //        headers: { "Content-Type": "Application/json;charset=utf-8" },
    //        data: { item: item }
    //    }).then(function (response) {
    //        $scope.showRightPanel(1);
    //    });

    //};

    //$scope.sendOrder = function (item) {
    //    var message = '';

    //    $scope.selectedProductOfDay = angular.copy(item);

    //    if (item.TotalQuantity !== 0 && (item.SaledQuantity + item.Quantity) > item.TotalQuantity) {

    //        message = ("Script.AngularServices.SendOrder.Message").toLanguage().replace("{0}", (item.TotalQuantity - item.SaledQuantity));
    //        $scope.selectedProductOfDay.Quantity = ($scope.selectedProductOfDay.TotalQuantity - $scope.selectedProductOfDay.SaledQuantity);
    //    } else
    //        message = ('Script.AngularServices.SendOrder.Message.Else').toLanguage().replace("{0}", $scope.selectedProductOfDay.Product.Code).replace("{1}", $scope.selectedProductOfDay.Quantity);


    //    $.confirm({
    //        title: ('Script.AngularServices.SendOrder.Confirm.Tittle').toLanguage(),
    //        content: message,
    //        buttons: {
    //            formSubmit: {
    //                text: ('Script.AngularServices.SendOrder.Confirm.Text').toLanguage(),
    //                btnClass: 'btn-blue',
    //                action: function () {
    //                    fireCustomLoading(true);
    //                    $http({
    //                        method: "POST",
    //                        url: "/Home/SendProductOfDayOrder",
    //                        headers: { "Content-Type": "Application/json;charset=utf-8" },
    //                        data: { productOfDay: $scope.selectedProductOfDay }

    //                    }).then(function (response) {
    //                        fireCustomLoading(false);
    //                        $('#leftContainer').removeClass('show');
    //                        $('#fadeLeft').fadeOut(500);
    //                        $scope.showLeftPanel(1);
    //                        iziToast.show({
    //                            message: response.data.Message,
    //                            position: 'topCenter',
    //                            color: response.data.Color,
    //                            icon: response.data.Icon
    //                        });
    //                    });
    //                }

    //            },
    //            Vazgeç: function () {
    //                //close
    //            }
    //        },
    //        scrollToPreviousElement: false,
    //        scrollToPreviousElementAnimate: false,
    //        onOpen: function () {

    //        }
    //    });


    //    //confirm

    //}

    //$scope.$on('ngRepeatProductOfDayFinished', function (ngRepeatProductOfDayFinishedEvent) {
    //    firePriceFormatNumberOnly();
    //});


    //$scope.$on('ngRepeatProductOfDayFinished2', function (ngRepeatProductOfDayFinished2Event) {
    //    firePriceFormatNumberOnly();
    //    if ($scope.controlRightPanel) {
    //        $('#rightContainer').addClass('show-right');
    //        $('#fadeRight').fadeIn(400);
    //        $('#rightContainer').addClass('menu-active');
    //    }
    //});


}).directive('onFinishRender', function ($timeout) {
    return {
        restrict: 'A',
        link: function (scope, element, attr) {
            if (scope.$last === true) {//ng repeat dönerken son kayıtmı diye bakıyorum
                $timeout(function () {
                    scope.$emit(attr.onFinishRender);
                });
            }
        }
    };
});