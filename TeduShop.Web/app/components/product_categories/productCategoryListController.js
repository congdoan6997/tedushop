﻿/// <reference path="../../../assets/admin/libs/angular/angular.js" />

(function (app) {
    app.controller('productCategoryListController', productCategoryListController);
    productCategoryListController.$inject = ['$scope', 'apiService','notificationService'];
    function productCategoryListController($scope, apiService, notificationService) {
        $scope.productCategories = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.keyword = '';
        $scope.search = search;
        function search() {
            getProductCategories();
        }
        $scope.getProductCategories = getProductCategories;
        function getProductCategories(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword:$scope.keyword,
                    page : page,
                    pageSize : 20,

                }
            };
            apiService.get('/api/productcategory/getall', config, function (result) {
                $scope.productCategories = result.data.Items;
                $scope.page = result.data.Page;
                $scope.totalCount = result.data.TotalCount;
                $scope.pagesCount = result.data.TotalPages;
                if ($scope.totalCount == 0) {
                    notificationService.displayWarning("Không tìm thấy bản ghi nào!");                   
                }

            }, function () {
                console.log('Get product categories failed!')
            });
        };

        $scope.getProductCategories();

    }
})(angular.module('tedushop.product_categories'))