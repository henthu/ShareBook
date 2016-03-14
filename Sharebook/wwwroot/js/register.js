function getCities(countryCode) {
    $.ajax({
        url: '/api/cities',
        data: { CountryCode: countryCode },
        dataType: "json",
        type: "POST",
        error: function () {
            alert("Something went wrong !");
        },
        success: function (data) {
            var items = "";
            
            $.each(data.data.cities.value, function (i, item) {
                items += "<option value=\"" + item.name + "\">" + item.name + "</option>";
            });

            $("#City").html(items);
            ;
        }
    });
}


$(document).ready(function () {
    $("#Country").change(function () {
        var value = $("#Country").val();
        getCities(value);
    });
});