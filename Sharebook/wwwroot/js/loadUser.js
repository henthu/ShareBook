//loadUser.js

(function () {

    $(document).on("ready",function () {
        $.ajax({
            url: "/api/users",
            type: "GET",
            success: function (data) {
                $("#userNav").html("<span class='text text-success'>"+data.firstName+"</span>");
                

            },
            error: function(error) {
                alert(JSON.stringify(error));
            }
            
        });
    });
})();