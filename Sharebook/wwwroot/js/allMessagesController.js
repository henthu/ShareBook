//allMessagesController.js


(function() {
    "use strict";
    
    angular.module("user-app")
        .controller("allMessagesController", allMessagesController);

    function allMessagesController($http) {
        
        var vm = this;

        vm.errorMessage = "";
        vm.conversations = [];
        vm.isBusy = true;



        $http.get("/api/messages/All")
            .then(function(Response) {
                //success
                alert(JSON.stringify(Response));
                angular.copy(Response.data, vm.conversations);

            }, function(error) {
                //failure
                vm.errorMessage = "could not get Data : " + JSON.stringify(error);
            })
            .finally(function() {
                vm.isBusy = false;
            });


    }
})();