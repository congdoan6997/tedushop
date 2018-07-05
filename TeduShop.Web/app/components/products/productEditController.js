(function (app) {
    app.controller('productEditController', productEditController);
    productEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams', 'commonService'];

    function productEditController($scope, apiService, notificationService, $state, $stateParams, commonService) {
        $scope.product = {}
        $scope.productCategories = [];
        $scope.moreImages = [];

        $scope.chooseMoreImages = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.moreImages.push(fileUrl);
                })
            }
            finder.popup();
        }
        $scope.chooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.product.Image = fileUrl;
                })
            }
            finder.popup();
        }
        $scope.updateProduct = function () {
            $scope.product.MoreImages = JSON.stringify($scope.moreImages);
            apiService.put('/api/product/update', $scope.product, function (result) {
                notificationService.displaySuccess($scope.product.Name + ' đã cập nhật thành công');
                $state.go('products');
            }, function (error) {
                notificationService.displayError('Cập nhật thất bại')
            })
        }
        $scope.getSeoTitle = function () {
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
                $scope.moreImages = JSON.parse($scope.product.MoreImages);
            }, function (error) {
                console.log(error);
            })
        }
        loadProductDetail();
        loadProductCategories();
    }
})(angular.module('tedushop.products'))