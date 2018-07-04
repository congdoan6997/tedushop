(function (app) {
    app.controller('productEditController', productEditController);
    productEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams', 'commonService'];

    function productEditController($scope, apiService, notificationService, $state, $stateParams, commonService) {
        $scope.product = {}
        $scope.productCategories = [];
        $scope.getSeoTitle = getSeoTitle;
        $scope.updateProduct = updateProduct;
        function updateProduct() {
            apiService.put('/api/product/update', $scope.product, function (result) {
                notificationService.displaySuccess($scope.product.Name + ' đã cập nhật thành công');
                $state.go('products');
            }, function (error) {
                notificationService.displayError('Cập nhật thất bại')
            })
        }
        function getSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }
        function loadProductCategories() {
            apiService.get('/api/productcategory/getallparents', null, function (result) {
                $scope.productCategories = result.data;
            }, function (error) {
                console.log(error);
            })
        }
        function loadProductDetail() {
            apiService.get('/api/product/getbyid/' + $stateParams.id, null, function (result) {
                $scope.product = result.data;
            }, function (error) {
                console.log(error);
            })
        }
        loadProductDetail();
        loadProductCategories();
    }
})(angular.module('tedushop.products'))