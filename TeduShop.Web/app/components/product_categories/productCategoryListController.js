/// <reference path="../../../assets/admin/libs/angular/angular.js" />

(function (app) {
    app.controller('productCategoryListController', productCategoryListController);
    productCategoryListController.$inject = ['$scope','apiService'];
    function productCategoryListController($scope,apiService) {
        $scope.productCategories = [];

        $scope.getProductCategories = getProductCategories;
        function getProductCategories() {
            apiService.get('/api/productcategory/getall', null, function (result) {
                $scope.productCategories = result.data;
            }, function () {
                console.log('Get product categories failed!')
            });
        };

        $scope.getProductCategories();

    }
})(angular.module('tedushop.product_categories'))