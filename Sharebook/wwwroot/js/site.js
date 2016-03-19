//site.js
(function () {
    
    $(document).on("click", ".editLink", function() {
            var myBookId = $(this).data('id');
            var myTitle = $(this).data('title');
            var myAuthor = $(this).data('author');
            var myGenre = $(this).data('genre');
            
            $("#titleEdit").val(myTitle);
            $("#authorEdit").val(myAuthor);
            $("#idEdit").val(myBookId);
            $("#genreEdit").val(myGenre);
        });
        
    $(document).on("submit","#editBookForm",function() {
       
       var editedBook = {id: $("#idEdit").val(),
                         name: $("#titleEdit").val(),
                         author: $("#authorEdit").val(),
                        genre:$("#genreEdit").val()};
       $.ajax({
           url:"/api/books/"+$("#idEdit").val(),
           type : "POST",
           data : editedBook,
           success: function (data) {
               window.location.href = "/";
           },
           error: function (error) {
               alert("Error on updating book: "+ error)
           }
       });
       
    });
}
)();
 