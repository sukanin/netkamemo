;(function () {
    var screenWidth = screen.width,
        screenHeight = screen.height,
        cookieValue = screenWidth + "x" + screenHeight,
        cookieName = "Telerik.Web.UI.DeviceInfoCookie",
        tenYears = 3650,
        MAX_PIXELS = 1367,
        xhr;
    
    function setDeviceInfoCookie(name, value, exdays) {
        var exdate = new Date();
        exdate.setDate(exdate.getDate() + exdays);

        var cookieValue = escape(value) + "; path=/" + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
        document.cookie = name + "=" + cookieValue;
    }

    function isDeviceInfoCookieExists(cookieName) {
        var cookieValue = document.cookie,
            startIndex = cookieValue.indexOf(" " + cookieName + "="),
            isExists = false;

        if (startIndex == -1) {
            startIndex = cookieValue.indexOf(cookieName + "=");
        }

        if (startIndex != -1) {
            isExists = true;
        }

        return isExists;
    }

    if (screenWidth < MAX_PIXELS && screenHeight < MAX_PIXELS) {
        if (!isDeviceInfoCookieExists(cookieName)) {
            xhr = new XMLHttpRequest();

            xhr.open("GET", deviceInfoRecorderURL + "?width=" + screenWidth + "&height=" + screenHeight, true); //deviceInfoRecorderURL is declared with a start script in the master page

            xhr.onreadystatechange = function () {
                if (xhr.readyState == 4 && xhr.status == 200) {
                    setDeviceInfoCookie(cookieName, cookieValue, tenYears);
                }
            };

            xhr.send();
        }
    }

})();