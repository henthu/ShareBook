//userController.js

(function () {
    "use strict";
    angular.module("user-app")
    .controller("userController",userController);
    
    function userController($http) {
        var vm = this;
        
        vm.errorMessage = "";
        vm.newUser = {};
        vm.isBusy = true;
        
        $http.get("/api/users")
        .then(function (response) {
            //Success
            angular.copy(response.Data,vm.users);
        },function (error) {
            //failure
            vm.errorMessage = "Failed to get Data" + error;
        })
        .finally(function () {
            vm.isBusy = false;
        });
        
        vm.addUser = function () {
            vm.isBusy = true;
            vm.errorMessage = "";
            $http.post("/api/users", vm.newUser)
            .then(function (response) {
                //success
                vm.newUser = response.Data;
                window.location.href = "/";
                
            },function (error) {
                //failure
                vm.errorMessage = "failed create user : "+ error;
            })
            .finally(function () {
                vm.isBusy = false;
            });
        }
    }
})();