(function (app) {
    app.controller('productCategoryEditController', productCategoryEditController);

    productCategoryEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams', 'commonService'];

    function productCategoryEditController($scope, apiService, notificationService, $state, $stateParams, commonService) {
        $scope.productCategory = {}
        $scope.parentCategories = [];
        $scope.updateProductCategory = updateProductCategory;
        $scope.getSeoTitle = getSeoTitle;
        function getSeoTitle() {
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
        }
        function loadProductCategoryDetail() {
            apiService.get('/api/productCategory/getbyid/' + $stateParams.id, null, function (result) {
                $scope.productCategory = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }
        function updateProductCategory() {
            apiService.put('/api/productCategory/update', $scope.productCategory, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được cập nhật!');
                $state.go('product_categories');
            }, function (error) {
                notificationService.displayError('Cập nhật không thành công!');
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
        loadProductCategoryDetail();
    }
})(angular.module('tedushop.product_categories'));