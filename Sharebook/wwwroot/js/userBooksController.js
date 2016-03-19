//userBooksController.js

(function () {
    angular.module("user-app")
    .controller("userBooksController",userBooksController);
    
    function userBooksController($http,$routeParams) {
        vm = this;
        
        vm.errorMessage = "";
        vm.isBusy = true;
        vm.userBooks = {};
        vm.bookComments = [];
        vm.newComment={};
        vm.selectedBook = "";
        
        $http.get("/api/users/"+$routeParams.userName)
        .then(function (Response) {
            //success
            vm.userBooks = Response.data;
        },function (error) {
            //failure
            vm.errorMessage = "could not get Data : " +JSON.stringify(error);
        })
        .finally(function () {
            vm.isBusy = false;
        });
        
        vm.showComments = function (book) {
            vm.errorMessage = "";
            vm.isBusy  = true;
            vm.bookComments = [];
            
            $http.get("/api/books/"+book.id+"/comments")
            .then(function (Response) {
                //success
                vm.bookComments = Response.data;
            },function (error) {
                //failure
                vm.errorMessage = "Could not get Comments : "+JSON.stringify(error);
            })
            .finally(function () {
                vm.isBusy = false;
            });
        };
        
        vm.addComment = function () {
            vm.isBusy=true;
            
            $http.post("/api/books/"+vm.selectedBook+"/comment",vm.newComment)
            .then(function (Response) {
                //success
                vm.bookComments.splice(0,0,Response.data);
            },function (error) {
                //failure
                vm.errorMessage = "Failed to create a comment"+JSON.stringify(error);
            })
            .finally(function () {
                vm.isBusy = false;
            });
        }
    }
})();