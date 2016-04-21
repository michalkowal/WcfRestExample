"use strict";

(function ($) {

    var NAV_LINK_TEMPLATE = "<a class=\"mdl-navigation__link\" href=\"#{0}\">{1}</a>";

    /**
     * Global array with site JS modules
     * @type {Array}
     */
    window.siteModules = [];

    /**
     * Global method to update Material Design Lite controls
     * after jquery DOM actions
     */
    window.updateMdl = function (callback) {
        setTimeout(function () {
            componentHandler.upgradeAllRegistered();
            if (callback) {
                callback();
            }
        }, 100);
    };
    /**
     * Global method returnig wcf address from browser localStorage
     * @return {string} stored wcf address
     */
    window.getServiceUrl = function () {
        return "http://" + localStorage.getItem("wcfAddress");
    };

    /**
      * Global tracing method.
      * Method sending log to the logger service.
      * @param {string} logLevel - 'trace', 'debug', 'info', 'warn', 'error', 'fatal'
      * @param {string} moduleName - Module who send trace message
      * @param {string} fnName - Sending function
      * @param {string} message - Log message
      */
    window.trace = function (logLevel, moduleName, fnName, message) {
        if (localStorage.getItem("tracing") === "true") {
            var baseUrl = window.getServiceUrl();
            var data = {
                Module: moduleName,
                Function: fnName,
                Message: message
            };
            $.ajax({
                method:  "POST",
                data: JSON.stringify(data),
                url: baseUrl + "/logger/" + (logLevel || "error"),
                contentType: "application/json",
                dataType: "text",
                error: function (jqXHR) {
                    console.log(jqXHR)
                }
            });
        }
    }

    function setActionRoute(module) {
        return function (container, properties, html) {
            module.routeAction(container, properties, html);
        };
    };

    $(document).ready(function () {
        var router = $.hashRouter();

        var initRoute = null;
        for (var i = 0; i < window.siteModules.length; i++) {
            var mod = window.siteModules[i];

            if (mod.route) {
                router.addRoute(mod.route,
                    $.hashRouter.controller()
                        .setEvent({
                            action: setActionRoute(mod)
                        })
                        .setResponse({
                            container: mod.routeContainer,
                            url: mod.routeAjaxResponse,
                        }, "AjaxResponse")
                );

                if (mod.title) {
                    var navLink = NAV_LINK_TEMPLATE.replace("{0}", mod.route).replace("{1}", mod.title);
                    $(".mdl-navigation").append(navLink);
                }

                if (!initRoute)
                    initRoute = mod.route;
            }
        }

        router.getReady();

        // Startup
        if (!window.location.hash && initRoute)
            window.location.hash = initRoute;

        if (!localStorage.getItem("wcfAddress"))
            localStorage.setItem("wcfAddress", "localhost:61256");
        if (!localStorage.getItem("tracing"))
            localStorage.setItem("tracing", "true");

        window.updateMdl();

    }).ajaxStart(function () {
        $("#loading").show();
    }).ajaxComplete(function () {
        $("#loading").hide();
    });

    window.onerror = function (errorMsg, url, lineNumber, column, errorObj) {
        window.trace("error", url, "line: " + lineNumber + "; column: " + column, errorMsg);
    };

})(jQuery);