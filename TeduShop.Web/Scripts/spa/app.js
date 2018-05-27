/// <reference path="../plugins/angular/angular.js" />

var myApp = angular.module("myModule", []);

myApp.controller('schoolController', schoolController);
myApp.service('Validitor', Validitor);

schoolController.$inject = ['$scope', 'Validitor'];

function schoolController($scope, Validitor) {
    $scope.num = 1;
    $scope.checkNumber = function () {
        $scope.message = Validitor.checkNumber($scope.num);
    }
   
}
function Validitor($window) {
    return {
        checkNumber: checkNumber
    }

    function checkNumber(input) {
        if (input % 2 == 0) {
            return "This is even";
        } else {
            return "This is odd";
        }
    };
};