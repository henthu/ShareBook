//loadUser.js

(function () {

    $(document).on("ready",function () {
        var commentCount = 0;
        var commentNotif = "";
        
        $.ajax({
            url: "/api/users",
            type: "GET",
            success: function (data) {
                $("#userNav").html("<span class='text text-success'>"+data.firstName+"</span>");
                data.books.forEach(function(book) {
                    book.comments.forEach(function(comment){
                        if(comment.isRead == false){
                            commentCount ++;
                            commentNotif += "<li id='comment-"+comment.id+"' class='small text text-default'><a href='#/books/"+book.id+"'> new comment on "+book.name+" from "+comment.userName+"</a></li>";
                        }
                    });
                });
                if(commentCount > 0){
                    $("#newComment").show();
                    $("#commentNotif").html(commentNotif);
                    $("#emptyComment").hide();
                }else{
                    $("#emptyComment").show();
                    $("#newComment").hide();
                }
            },
            error: function(error) {
                alert(JSON.stringify(error));
            }
            
        });
    });
})();