
angular.module('AdminLayoutApp', [])
    .controller('studentAddController', function ($scope, $http) {

        $scope.selectedStudent = {};

        //$scope.newCustomer = function () {

        //    $scope.selectedStudent.Gender = $("#txtGender option:selected").text();

        //    // fireCustomLoading(true);
        //    $http({
        //        method: "POST",
        //        url: "/Admin/Student/AddStudent",
        //        headers: { "Content-Type": "Application/json;charset=utf-8" },
        //        data: { student: $scope.selectedStudent }
        //    }).then(function (response) {
        //        iziToast.show({
        //            message: response.data.Message,
        //            position: 'topCenter',
        //            color: response.data.Color,
        //            icon: response.data.Icon
        //        });
        //        //fireCustomLoading(false);

        //    });
        //};

        $scope.setFileImage = function (element) {
            $scope.currentFile = element.files[0];
            var reader = new FileReader();
            reader.onload = function (event) {
                $scope.image_sourceIcon = event.target.result;
                $scope.$apply()
            }
            reader.readAsDataURL(element.files[0]);
        };
        $scope.setFile = function (element) {
            $scope.currentFile = element.files[0];
            var reader = new FileReader();
            //$scope.image_path = element.value;
            reader.onload = function (event) {
                $scope.file_source = event.target.result;
                $scope.$apply()
            }
            reader.readAsDataURL(element.files[0]);
        };


        $scope.newCustomer = function () {
            $scope.selectedStudent.Gender = $("#txtGender option:selected").text();
            $scope.selectedStudent.Age = $("#date").val();
            var data = new FormData();
            data.append("Name", $scope.selectedStudent.Name);
            data.append("GuardianName", $scope.selectedStudent.GuardianName);
            data.append("Tel1", $scope.selectedStudent.Tel1);
            data.append("Mail", $scope.selectedStudent.Mail);
            data.append("Age", $scope.selectedStudent.Age);
            data.append("Gender", $scope.selectedStudent.Gender);
            data.append("Address", $scope.selectedStudent.Address);
            data.append("City", $scope.selectedStudent.City);
            data.append("Town", $scope.selectedStudent.Town);
            data.append("fileSelectedOne", $("#file-selector")[0].files[0]);

            $.ajax({
                url: "/Admin/Student/AddStudent",
                type: 'POST',
                dataType: 'json',
                data: data,
                contentType: false,
                processData: false
            }).then(function successCallback(response) {
                //fireCustomLoading(false);
                iziToast.show({
                    message: response.Message,
                    position: 'topCenter',
                    color: response.Color,
                    icon: response.Icon
                });

            });


        };

        $(document).ready(function () {

        });
    })



