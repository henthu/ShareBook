//bookController.js

(function() {
    angular.module("user-app")
        .controller("conversationController", conversationController);

    function conversationController($http, $routeParams) {
        vm = this;

        vm.errorMessage = "";
        vm.reciever = $routeParams.userName;
        vm.conversation =[];
        vm.newMessage = {};
        vm.newMessage.recieverUserName = $routeParams.userName;
        vm.isBusy = true;
        

        $http.get("/api/messages/" + vm.reciever)
            .then(function(Response) {
                //success
                angular.copy(Response.data, vm.conversation);
            }, function(error) {
                //failure
                vm.errorMessage = "could not get Data : " + JSON.stringify(error);
            })
            .finally(function() {
                //update count unread
                $http.get("/api/messages/unread")
                .then(function (Response) {
                    //success
                    // TODO
                },function (error) {
                    //failure
                    vm.errorMessage = "could not get Data : " + JSON.stringify(error);
                })

                vm.isBusy = false;
            });

        vm.sendMessage = function() {
            vm.isBusy = true;
            vm.errorMessage = "";
            $http.post("/api/messages/" + vm.reciever, vm.newMessage)
                .then(function(Response) {
                    //success
                    vm.conversation.push(Response.data);
                }, function(error) {
                    //failure
                    vm.errorMessage = "Failed to create a comment" + JSON.stringify(error);
                })
                .finally(function() {
                    vm.isBusy = false;
                    vm.newMessage.content ="";
                });

        };
    }    
 })();