(function (app) {
    app.controller('productAddController', productAddController);

    productAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService'];

    function productAddController($scope, apiService, notificationService, $state, commonService) {
        $scope.product = {
            CreatedDate: new Date(),
            Status: true
        };
        //$scope.parentCategories = [];
        $scope.addProduct = addProduct;
        $scope.getSeoTitle = getSeoTitle;
        $scope.productCategories = [];
        $scope.ckeditorOptions = {
            language: 'vi',
            height: '200px'
        };
        $scope.chooseImage = chooseImage;
        function chooseImage() {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.product.Image = fileUrl;
            }
            finder.popup();
        }
        function getSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }
        function addProduct() {
            apiService.post('/api/product/create', $scope.product, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã thêm mới!');
                $state.go('products');
            }, function (error) {
                notificationService.displayError('Tạo mới không thành công!');
            });
        }
        function loadproductCategories() {
            apiService.get('/api/productcategory/getallparents', null, function (result) {
                $scope.productCategories = result.data;
            }, function (error) {
                console.log(error);
            });
        }
        loadproductCategories();
    }
})(angular.module('tedushop.products'));