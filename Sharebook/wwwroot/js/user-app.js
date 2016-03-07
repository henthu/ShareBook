//user-app.js

(function () {
    "use strict";
    
    var userApp = angular.module("userApp",["ngRoute"]);
    
    userApp.config(["$routeProvider"],function ($routeProvider) {
        $routeProvider.
        when("/users/register",{
            templateUrl: "register.html"
        })
    }
    );
    
})