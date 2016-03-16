//user-app.js

(function () {
    "use strict";
    
    angular.module("user-app",["ngRoute"])
        .config(function ($routeProvider) {
            $routeProvider.
            when("/",{
                controller:"homeController",
                controllerAs:"vm",
                templateUrl:"/views/home.html"
            });
            
            $routeProvider.otherwise({
                redirectTo:"/"
            });
        });
}
)();