$("document").ready(function () {

    $(".delete-card-button").each(function () {
        $(this).click(function (e) {
            e.preventDefault();
            this.innerHTML = "Confirm delete";
            $(this).removeClass("btn-danger");
            $(this).css("background-color", "#ff0000");
            $(this).css("color", "#ffffff");
            //$(".card-header").css("background-color", "#ff0000");
            $(this).unbind('click');
        })
    });
});