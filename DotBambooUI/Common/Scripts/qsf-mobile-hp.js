; (function($) {
    var actualTarget;

    window.mobileNavigationClicking = function(sender, args) {
        var cancelEvent = args.get_item().get_level() === 0;
        var domEvent = args.get_domEvent();
        var eventMapTarget = domEvent.target;
        var actualItem;

        args.set_cancel(cancelEvent);

        if ($(actualTarget).is("span") && $(eventMapTarget).is("a")) {
            domEvent.preventDefault();
            actualItem = sender._extractItemFromDomElement(actualTarget);
            actualItem.collapse();
        }
    };

    window.mobileNavigationExpanded = function(sender, args) {
        var item = args.get_item();

        $.each(sender.get_expandedItems(), function(_, expandedItem) {
            if (expandedItem.get_level() > 0 && expandedItem !== item) {
                expandedItem.collapse();
                return false;
            }
        });
    };

    $(function() {
        var $content = $(".tm-view");
        var $overlay = $(".tm-click-overlay");

        $("#header > .tm-primary").on("click", function(e) {
            $content.addClass("tm-left-drawer-active");
            $overlay.css("left", "0");
        });

        $overlay.on("click", function() {
            $content.removeClass("tm-left-drawer-active");
            $overlay.css("left", "-100%");
        });

        $("#Navigation").on("touchend", ".rpLink", function(e) {
            actualTarget = $telerik.getTouches(e)[0].target;
        });

    });

})($telerik.$);