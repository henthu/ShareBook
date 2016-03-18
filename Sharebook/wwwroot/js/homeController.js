//homeController.js

(function() {
    "use strict";
    angular.module("user-app")
        .controller("homeController", homeController);

    function homeController($http) {
        var vm = this;

        vm.errorMessage = "";
        vm.isBusy = true;
        vm.books = [];
        vm.newBook = {};
        vm.editedBook = {};
        

        

        $http.get("/api/books")
            .then(function(response) {
                //Success
                angular.copy(response.data, vm.books);
            }, function(error) {
                //failure
                vm.errorMessage = "Failed to get Data" + error;
            })
            .finally(function() {
                vm.isBusy = false;
            });
        vm.books.forEach(function(book) {
            book.editMode = false;
        });

        vm.addBook = function() {
            vm.isBusy = true;
            vm.errorMessage = "";
            
            $http.post("/api/books", vm.newBook)
                .then(function(response) {
                    //success
                    vm.books.push(response.data);
                    vm.newBook = {};
                }, function(error) {
                    //failure
                    vm.errorMessage = "Failed to create a new book";
                })
                .finally(function() {
                    vm.isBusy = false;
                });

        };

        vm.remove = function(book) {
            vm.isBusy = true;
            vm.errorMessage = "";
            $http.delete("/api/books/" + book.id)
                .then(
                function(Response) {
                    //success
                    if (Response.data.success == "true") {
                        var index = vm.books.indexOf(book);
                        vm.books.splice(index, 1);
                    } else {
                        vm.errorMessage = Response.data.errorMessage;
                    }
                }, function(error) {
                    //failure
                    vm.errorMessage = Response.data.errorMessage;
                }
                ).finally(function() {
                    vm.isBusy = false;
                })
        }
        
        
       
    }
})();