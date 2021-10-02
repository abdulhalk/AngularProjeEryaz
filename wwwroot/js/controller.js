var app = angular.module("Homeapp", ['ngAnimate', 'ngSanitize', 'ui.bootstrap', 'angularUtils.directives.dirPagination']);

app.controller("SaveController", function ($scope, $http) {
    $scope.btntext = "Save";

    // Add Product

    $scope.savedata = function () {

        $scope.btntext = "Please Wait..";
        
        $http({

            method: 'POST',
            dataType: 'json',
            url: '/Product/Add_Product',
            headers: { "Content-Type": "application/json" },

            data: $scope.product

        }).success(function (d) {

            $scope.btntext = "Save";

            $scope.product = null;

            alert(d);

        }).error(function () {

            alert('Failed');

        });
    };
});

app.controller("DisplayController", function ($scope, $http, $uibModal) {


    $scope.updatemodal = function (product) {
        var modalInstance = $uibModal.open({
            animation: true,
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/html/myModalContent.html',
            controller: function ($scope, $uibModalInstance) {
                $scope.product = product;
                $scope.btntext = "Update Product";
                $scope.savedata = function () {
                    $http({

                        method: 'POST',
                        dataType: 'json',
                        url: '/Product/Update_Product',
                        headers: { "Content-Type": "application/json" },

                        data: $scope.product

                    }).then(function (d) {
                        $scope.product = {};
                        $uibModalInstance.close();
                    }, function () {
                        alert('Failed');
                    });
                };
                $scope.ok = function () {
                    //{...}
                    alert("You clicked the ok button.");
                    $uibModalInstance.close();
                };

                $scope.cancel = function () {
                    //{...}

                    $uibModalInstance.close();
                };
            },
            size: 'lg',
            resolve: {
                data: function () {
                    return $scope.data;
                }
            }
        });

        modalInstance.result.then(function () {
            $scope.getAllProduct();
        });
    };


    $scope.newproduct = function () {
        var modalInstance = $uibModal.open({
            animation: true,
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/html/myModalContent.html',
            controller: function ($scope, $uibModalInstance) {
                $scope.product = {};
                $scope.btntext = "Add new Product";
                $scope.savedata = function () {
                    $http({

                        method: 'POST',
                        dataType: 'json',
                        url: '/Product/Add_Product',
                        headers: { "Content-Type": "application/json" },

                        data: $scope.product

                    }).then(function (d) {
                        $scope.product = {};
                        $uibModalInstance.close();
                    },function () {
                        alert('Failed');
                    });
                };

                $scope.ok = function () {
                    //{...}
                    alert("You clicked the ok button.");
                    $uibModalInstance.close();
                };

                $scope.cancel = function () {
                    //{...}
                    alert("You clicked the cancel button.");
                    $uibModalInstance.dismiss('cancel');
                };
            },
            size: 'lg',
            resolve: {
                data: function () {
                    return $scope.data;
                }
            }
        });

        modalInstance.result.then(function () {
            $scope.getAllProduct();
        });
    };


    $scope.btntext = "update"
    $scope.getProduct = []
    $http.get("/Product/Get_Product").then(function (d) {
        $scope.getProduct = d.data;
        console.log($scope.getProduct)
    }, function (error) {
        alert("Failed");

    });


    $scope.getAllProduct = function () {
        $http.get("/Product/Get_Product").then(function (d) {
            $scope.getProduct = d.data;
            console.log($scope.getProduct)
        }, function (error) {
            

        });
    };



    $scope.deleterecord = function (id) {

        $http.get("/Product/delete_record?id=" + id).then(function (d) {

            alert(d.data);

            $http.get("/Product/Get_Product").then(function (d) {
                $scope.getProduct = d.data;
            }, function (error) {
                alert("Failed");

            });
        }, function (error) {

            alert('Failed');

        });

    }; 
    
});