//import { Date } from "core-js";

(function (app) {
    app.controller('productCategoryAddController', productCategoryAddController);

    productCategoryAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state','commonService'];

    function productCategoryAddController($scope, apiService, notificationService, $state,commonService) {
        $scope.productCategory = {
            CreatedDate: new Date(),
            Status: true
        };
        $scope.parentCategories = [];
        $scope.addProductCategory = addProductCategory;
        $scope.getSeoTitle = getSeoTitle;
        function getSeoTitle() {
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
        }
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
})(angular.module('tedushop.product_categories'));