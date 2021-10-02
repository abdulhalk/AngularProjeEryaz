var app = angular.module("Homeapp", []);

app.controller("RegisterController", function ($scope, $http,$location) {
    $scope.btntext = "Register";

    // Add User

    $scope.savedata = function () {

        $scope.btntext = "Please Wait..";
        

        $http({

            method: 'POST',
            dataType: 'json',
            url: '/Login/Add_User',
            headers: { "Content-Type": "application/json" },

            data: $scope.user

        }).success(function (d) {

            $scope.btntext = "Register";

            $scope.user = null;

           /* alert(d);*/
            window.location.href = '/Login/Index2';
            

        }).error(function () {

            alert('Failed');

        });
    };
});


app.controller("LoginController", function ($scope, $http) {
    $scope.btntext = "Login";
    $scope.login = function () {
        $scope.btntext = "please wait...";
        $http({
            method: "POST",
            url: "/Login/userlogin",
            data: $scope.user
        }).success(function (d) {
            $scope.btntext = "Login";
            if (d == 1) {
                window.location.href = '/Product/HomePage';
            }
            else {
                alert(d);
            }
            $scope.user = null;

        }).error(function () {
            alert("failed");
        })

    }
})

