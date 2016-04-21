"use strict";

(function () {

    /**
     * Config Module constructor
     * @constructor
     */
    function ConfigModule() {
        this.title = "Configuration";
        this.route = "/config";
        this.routeContainer = $("#page-content");
        this.routeAjaxResponse = "config/index.html";
    }
    /**
     * Executing when route is changed to /config
     * @param {object} container - jquery DOM container
     * @param {object} properties  - jquery hash router properties for current route
     * @param {object} html - loaded html file
     */
    ConfigModule.prototype.routeAction = function (container, properties, html) {
        $("#page-content").html(html);

        $("#server").val(localStorage.getItem("wcfAddress"));
        $("#tracing").attr('checked', localStorage.getItem("tracing") === "true");

        $("#config-form").submit(function (event) {

            /* stop form from submitting normally */
            event.preventDefault();

            localStorage.setItem("wcfAddress", $("#server").val());
            localStorage.setItem("tracing", $("#tracing").is(':checked').toString());

            var snackbarContainer = $("#success-snackbar")[0];
            if (snackbarContainer) {
                var data = { message: "Configuration saved!" };
                snackbarContainer.MaterialSnackbar.showSnackbar(data);
            }
        });

        window.updateMdl();
    }

    window.siteModules.push(new ConfigModule());

})();
