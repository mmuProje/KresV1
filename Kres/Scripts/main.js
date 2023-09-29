function fireSetLanguageId(id) {
    $.ajax({
        type: "POST",
        url: "/Home/SetCurrentLanguage",
        data: "{id:" + id + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (data) {
            //if (data)
            //window.location.reload();


        }
    });
}