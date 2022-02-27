;(function($) {

    $(function() {
        var skin = $("#skin").val();
        var $content = $(".tm-view");
        var $overlay = $(".tm-click-overlay");

        $("#skin-chooser")
            .find("li.selected")
            .removeClass("selected")
            .end()
            .find("li > a.skin-" + skin)
            .parent()
            .addClass("selected");

        $("#header > .tm-secondary").on("click", function(e) {
            $content.toggleClass("tm-right-drawer-active");
            $overlay.css("left", "0");
        });

        $overlay.on("click", function() {
            $content.removeClass("tm-right-drawer-active");
            $overlay.css("left", "-100%");
        });
    });

})($telerik.$);