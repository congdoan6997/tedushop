(function (app) {
    app.controller('rootController', ['$scope', 'loginService', '$state', 'authData', 'authenticationService',
        function ($scope, loginService, $state, authData, authenticationService) {
            $scope.logOut = function () {
                loginService.logOut();
                $state.go('login');
            };
            $scope.authentication = authData.authenticationData;

            //authenticationService.validateRequest();
    }]);


})(angular.module('tedushop'))