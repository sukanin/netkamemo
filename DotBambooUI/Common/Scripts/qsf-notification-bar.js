;(function () {

    /* handy functions to read/write a cookie*/
    function readCookie(name) {
        var nameEQ = name + "=",
            ca = document.cookie.split(';');

        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) == ' ') c = c.substring(1, c.length);
            if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
        }
        return null;
    }

    function createCookie(name, value, days) {
        if (days) {
            var date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            var expires = "; expires=" + date.toGMTString();
        }
        else 
            var expires = "";
        document.cookie = name + "=" + value + expires + "; path=/";
    }

    function camelize( input ) {
        return input.replace(/-+(.)?/g, function(match, chr) {
            return chr ? chr.toUpperCase() : '';
        });
    }

    function pascalCase( input ) {
        return camelize( input.charAt(0).toUpperCase() + input.substring(1) );
    }

    Sys.Application.add_load(function() {

        var $ = $telerik.$,
            $body = $("body"),
            $notifications = $(".notification-bar");

        $notifications.each(function() {

            var $notification = $(this);
            var cookieName = "Qsf" + pascalCase(this.id) + "Cookie";

            if (!readCookie(cookieName)) {

                $notification.show(0, function() {
                    $body.addClass( "notification-bar-active" );
                });

                $notification.find(".close").on("click", function (e) {

                    // set a cookie, so next time ninja will not show up
                    createCookie(cookieName, "notificationIsHidden", 30);

                    $notification.fadeOut(function() {

                        $body.removeClass( "notification-bar-active" );

                    });

                    return false;
                });
            }
        });

    });

}());