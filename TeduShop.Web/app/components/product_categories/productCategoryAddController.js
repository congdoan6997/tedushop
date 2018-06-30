//import { Date } from "core-js";

(function (app) {
    app.controller('productCategoryAddController', productCategoryAddController);

    productCategoryAddController.$inject = ['$scope', 'apiService', 'notificationService','$state'];

    function productCategoryAddController($scope, apiService, notificationService,$state) {
        $scope.productCategory = {
            CreatedDate: new Date(),
            Status:true
        }
        $scope.parentCategories = [];
        $scope.addProductCategory = addProductCategory;
        function addProductCategory() {
            apiService.post('/api/productCategory/create', $scope.productCategory, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã thêm mới!');
                $state.go('product_categories');
            }, function (error) {
                notificationService.displayError('Tạo mới không thành công!');
            });
        }
        function loadParentCategory() {
            apiService.get('/api/productCategory/getallparents', null, function (result) {
                $scope.parentCategories = result.data;
            }, function (error) {
                console.log(error);
            });
        }
        loadParentCategory();
    }
})(angular.module('tedushop.product_categories'))