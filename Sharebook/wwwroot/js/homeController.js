//homeController.js

(function () {
    "use strict";
    angular.module("user-app")
    .controller("homeController",homeController);
    
    function homeController($http) {
        var vm = this;
        
        vm.errorMessage = "";
        vm.isBusy = true;
        vm.myProfile = {};
        vm.newBook = {};
        
        $http.get("/api/books")
        .then(function (response) {
            //Success
            angular.copy(response.Data,vm.myProfile);
        },function (error) {
            //failure
            vm.errorMessage = "Failed to get Data" + error;
        })
        .finally(function () {
            vm.isBusy = false;
        });
        
        vm.addBook = function () {
            vm.isBusy = true;
            vm.errorMessage = "";
            $http.post("/api/books",vm.newBook)
            .then(function (response) {
                //success
                vm.myProfile.books.push(response.data);
                vm.newBook = {};
            },function (error) {
                //failure
                vm.errorMessage = "Failed to create a new book";
            })
            .finally(function () {
                vm.isBusy = false;
            });
            
        };
    }
})();