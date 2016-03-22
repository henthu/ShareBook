//bookController.js

(function () {
    angular.module("user-app")
    .controller("bookController",bookController);
    
    function bookController($http,$routeParams) {
        vm = this;
        
        vm.errorMessage = "";
        vm.bookId = $routeParams.id;
        vm.isBusy = true;
        vm.currentBook = {};
        
        $http.get("/api/books/" + vm.bookId)
        .then(function (Response) {
            //success
            alert(JSON.stringify(Response.data));
            vm.currentBook = Response.data;
            vm.currentBook.comments.forEach(function (comment) {
                comment.isRead = true;
                $("#comment-"+comment.id).remove();
            });
            
        },function (error) {
            //failure
            vm.errorMessage = "could not get Data : " +JSON.stringify(error);
        })
        .finally(function () {
            vm.isBusy = false;
           if (vm.currentBook != {}){
            $http.post("/api/books/"+vm.currentBook.id,vm.currentBook);
        }
        });
        
       
    }
})();