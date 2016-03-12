//user-app.js

(function () {
    "use strict";
    
    angular.module("user-app",["ngRoute"])
        .config(function ($routeProvider) {
            $routeProvider.
            when("/users/register",{
                controller:"userController",
                controllerAs:"vm",
                templateUrl: "/views/register.html"
            });
            
            $routeProvider.otherwise({
                redirectTo:"/"
            });
        });
}
)();