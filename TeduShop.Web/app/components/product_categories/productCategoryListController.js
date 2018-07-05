/// <reference path="../../../assets/admin/libs/angular/angular.js" />

(function (app) {
    app.controller('productCategoryListController', productCategoryListController);
    productCategoryListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];
    function productCategoryListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.productCategories = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.keyword = '';

        $scope.isAll = false;

        $scope.deleteMulti = function () {
            var listId = [];
            $.each($scope.selected, function (i, item) {
                listId.push(item.ID);
            });

            var config = {
                params: {
                    listId: JSON.stringify(listId)
                }
            };
            apiService.del('/api/productcategory/deletemulti', config, function (result) {
                notificationService.displaySuccess("Xoá thành công!");
                search();
            }, function (error) {
                notificationService.displayError("Xóa không thành công!");
            });
        }
        $scope.selectAll = function () {
            if ($scope.isAll === false) {
                angular.forEach($scope.productCategories, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.productCategories, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }
        $scope.$watch('productCategories', function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);
        $scope.search = function () {
            getProductCategories();
        }
        $scope.deleteProductCategory = function (id) {
            $ngBootbox.confirm('Bạn muốn xóa không?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                };
                apiService.del('/api/productcategory/delete', config, function (result) {
                    notificationService.displaySuccess('Xóa thành công');
                    search();
                }, function (error) {
                    notificationService.displayError('Lỗi khi xóa!');
                });
            }, function () {
                notificationService.displayError('Xóa không thành công!');
            });
        }
        $scope.getProductCategories = function (page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 20
                }
            };
            apiService.get('/api/productcategory/getall', config, function (result) {
                if (result.data.TotalCount === 0) {
                    notificationService.displayWarning('Không có bản ghi nào được tìm thấy.');
                }
                //else {
                //    notificationService.displaySuccess('Đã tìm thấy ' + result.data.TotalCount + ' bản ghi.');
                //}
                $scope.productCategories = result.data.Items;
                $scope.page = result.data.Page;
                $scope.totalCount = result.data.TotalCount;
                $scope.pagesCount = result.data.TotalPages;
            }, function () {
                console.log('Get product categories failed!');
            });
        }

        $scope.getProductCategories();
    }
})(angular.module('tedushop.product_categories'));