//user-app.js

(function () {
    "use strict";
    
    angular.module("user-app",["MyDirectives","ngRoute"])
        .config(function ($routeProvider) {
            $routeProvider.
            when("/",{
                controller:"homeController",
                controllerAs:"vm",
                templateUrl:"/views/home.html"
            })
            .when("/:userName/books",{
                controller:"userBooksController",
                controllerAs:"vm",
                templateUrl:"/views/userBooks.html"
            })
            .when("/books/:id",{
                controller:"bookController",
                controllerAs:"vm",
                templateUrl:"/views/bookComments.html"
            }
            )
            .when("/messages/:userName",{
                controller:"conversationController",
                controllerAs:"vm",
                templateUrl:"/views/conversation.html"   
            }                
            )
            .when("/messages",{
                controller:"allMessagesController",
                controllerAs:"vm",
                templateUrl:"/views/allMessages.html"
            }
                
            );
            
            $routeProvider.otherwise({
                redirectTo:"/"
            });
        });
}
)();