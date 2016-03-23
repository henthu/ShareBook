//bookController.js

(function() {
    angular.module("user-app")
        .controller("bookController", bookController);

    function bookController($http, $routeParams) {
        vm = this;

        vm.errorMessage = "";
        vm.bookId = $routeParams.id;
        vm.isBusy = true;
        vm.currentBook = {};
        vm.newComment = {};

        $http.get("/api/books/" + vm.bookId)
            .then(function(Response) {
                //success
                alert("first get" + JSON.stringify(Response.data));
                angular.copy(Response.data, vm.currentBook);
                vm.currentBook.comments.forEach(function(comment) {
                    comment.isRead = true;
                    $("#comment-" + comment.id).remove();
                });

            }, function(error) {
                //failure
                vm.errorMessage = "could not get Data : " + JSON.stringify(error);
            })
            .finally(function() {
                vm.isBusy = false;
                if (vm.currentBook != {}) {
                    alert("currentBookId : " + JSON.stringify(vm.currentBook));
                    $http.post("/api/books/" + vm.currentBook.id, vm.currentBook)
                        .then(function(Response) {
                            //success
                            alert(JSON.stringify(Response.data));
                        }, function(error) {
                            //failure
                            alert(JSON.stringify(error));
                        });
                }
                
                if($("#commentNotif").html() == ""){
                    $("#emptyComment").show();
                    $("#newComment").hide();
                }
            });

        vm.addComment = function() {
            vm.isBusy = true;

            $http.post("/api/books/" + vm.currentBook.id + "/comment", vm.newComment)
                .then(function(Response) {
                    //success
                    vm.currentBook.comments.splice(0, 0, Response.data);
                    vm.newComment = {};
                }, function(error) {
                    //failure
                    vm.errorMessage = "Failed to create a comment" + JSON.stringify(error);
                })
                .finally(function() {
                    vm.isBusy = false;
                });

        };
    }    
 })();