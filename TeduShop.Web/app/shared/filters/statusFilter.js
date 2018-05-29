﻿/// <reference path="../../../assets/admin/libs/angular/angular.js" />


(function (app) {
    app.filter('statusFilter', function () {
        return function (input) {
            if (input) {
                return 'Kích hoạt';
            }
            return 'Khóa';
        }
    })
})(angular.module('tedushop.common'));