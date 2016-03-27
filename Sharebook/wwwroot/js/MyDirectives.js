//MyDirectives.js
(function() {
    "use strict";
    angular.module("MyDirectives", [])
        .directive("messageDirective", messageDirective);

    function messageDirective() {

        return {
            link: function(scope) {
                if (scope.$last) {

                    var scrHeight = $("#Message-body")[0].scrollHeight * 4;
                    $("#Message-body").scrollTop(scrHeight);

                }
            }
        };

    }
})();