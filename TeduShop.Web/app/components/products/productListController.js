﻿(function (app) {
    app.controller('productListController', productListController);
    productListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];
    function productListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.products = [];
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
            apiService.del('/api/product/deletemulti', config, function (result) {
                notificationService.displaySuccess("Xoá thành công!");
                search();
            }, function (error) {
                notificationService.displayError("Xóa không thành công!");
            });
        }
        $scope.selectAll = function () {
            if ($scope.isAll === false) {
                angular.forEach($scope.products, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.products, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }
        $scope.$watch('products', function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);
        $scope.search = function () {
            getProducts();
        }
        $scope.deleteProduct = function (id) {
            $ngBootbox.confirm('Bạn muốn xóa không?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                };
                apiService.del('/api/product/delete', config, function (result) {
                    notificationService.displaySuccess('Xóa thành công');
                    search();
                }, function (error) {
                    notificationService.displayError('Lỗi khi xóa!');
                });
            }, function () {
                notificationService.displayError('Xóa không thành công!');
            });
        }
        $scope.getProducts = function (page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 20
                }
            };
            apiService.get('/api/product/getall', config, function (result) {
                if (result.data.TotalCount === 0) {
                    notificationService.displayWarning('Không có bản ghi nào được tìm thấy.');
                }
                //else {
                //    notificationService.displaySuccess('Đã tìm thấy ' + result.data.TotalCount + ' bản ghi.');
                //}
                $scope.products = result.data.Items;
                $scope.page = result.data.Page;
                $scope.totalCount = result.data.TotalCount;
                $scope.pagesCount = result.data.TotalPages;
            }, function () {
                console.log('Get products failed!');
            });
        }

        $scope.getProducts();
    }
})(angular.module('tedushop.products'));