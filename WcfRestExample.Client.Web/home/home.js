'use strict';

(function () {

    /**
     * Home module constructor
     * @constructor
     */
    function HomeModule() {
        this.title = "Home";
        this.route = "/home";
        this.routeContainer = $("#page-content");
        this.routeAjaxResponse = "home/index.html";
    }
    /**
     * Executing when route is changed to /home
     * @param {object} container - jquery DOM container
     * @param {object} properties  - jquery hash router properties for current route
     * @param {object} html - loaded html file
     */
    HomeModule.prototype.routeAction = function (container, properties, html) {
        $("#page-content").html(html);
    }

    window.siteModules.push(new HomeModule());

})();