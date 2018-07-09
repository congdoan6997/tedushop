(function (app) {
    app.factory('authData', [function () {
        var authDataFactory = {};
        var authentication = {
            userName: "",
            IsAuthenticated: false
        };
        authDataFactory.authenticationData = authentication;
        return authDataFactory;
    }]);
})(angular.module('tedushop.common'));