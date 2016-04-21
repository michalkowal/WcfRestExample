"use strict";

(function () {

    /**
     * Employee entity
     * @typedef {Object} Employee
     * @property {number} EmployeID - Employee Identifier
     * @property {string} Name - Employee's name
     * @property {string} Address - Employee's address
     * @property {string} Email - Employee's email
     * @property {string} PhoneNumber - Employee's phone number
     */

    /**
     * Filling employee form with json employee data
     * @param {Employee} data - employee data to display
     */
    function fillEmployeeForm(data) {
        if (data) {
            $("#employeeId").val(data.EmployeeID);
            $("#name").val(data.Name);
            $("#address").val(data.Address);
            $("#email").val(data.Email);
            $("#phone").val(data.PhoneNumber);
        }

        $("#employee-form").submit(function (event) {

            /* stop form from submitting normally */
            event.preventDefault();

            var employee = {};
            employee.EmployeeID = $("#employeeId").val();
            employee.Name = $("#name").val();
            employee.Address = $("#address").val();
            employee.Email = $("#email").val();
            employee.PhoneNumber = $("#phone").val();

            saveEmployee(employee);
        });

        window.updateMdl(function () {
            $(".mdl-textfield").each(function () {
                this.MaterialTextfield.checkValidity();
                this.MaterialTextfield.checkDirty();
            });
        });
    }

    /**
     * Sending to server updated by user employee data
     * @param {Employee} employee - employee data to save
     */
    function saveEmployee(employee) {
        var baseUrl = window.getServiceUrl();
        var method = ((employee.EmployeeID && employee.EmployeeID > 0 && employee.EmployeeID !== "0") ? "PUT" : "POST");
        var url = baseUrl + "/employee/" + ((employee.EmployeeID && employee.EmployeeID > 0 && employee.EmployeeID !== "0") ? employee.EmployeeID : "");
        $.ajax({
            method: method,
            data: JSON.stringify(employee),
            url: url,
            contentType: "application/json",
            dataType: "text",
            success: function (data) {
                window.location.hash = "/employee";
            },
            error: function (jqXHR, textStatus, errorThrown) {
                var message = "ajax error(" + jqXHR.status + " " + errorThrown + ")";
                message += "; url: " + url;
                message += "; method: " + method;
                message += "; data: " + JSON.stringify(employee);
                if (jqXHR.responseText) {
                    message += ": " + jqXHR.responseText;
                }

                console.log(jqXHR);
                window.trace("error", "Employee", "saveEmployee(employee)", message);
            }
        });
    }

    /**
     * Employee Edit Module cnstructor
     * @constructor
     */
    function EmployeeEditModule() {
        this.route = "/employee/%u";
        this.routeContainer = $("#page-content");
        this.routeAjaxResponse = "employees/edit.html";
    }
    /**
     * Executing when route is changed to /employee/{id}
     * @param {object} container - jquery DOM container
     * @param {object} properties  - jquery hash router properties for current route
     * @param {object} html - loaded html file
     */
    EmployeeEditModule.prototype.routeAction = function (container, properties, html) {
        var me = this;

        $("#page-content").html(html);

        var baseUrl = window.getServiceUrl();
        var method = "GET";
        var url = baseUrl + "/employee/" + properties.args[0];
        $.ajax({
            method: method,
            url: url,
            dataType: "json",
            success: function (data) {
                fillEmployeeForm(data);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                var message = "ajax error (" + jqXHR.status + " " + errorThrown + ")";
                message += "; url: " + url;
                message += "; method: " + method;
                if (jqXHR.responseText) {
                    message += ": " + jqXHR.responseText;
                }

                console.log(jqXHR);
                window.trace("error", "Employee", "EmployeeEditModule.routeAction(container, properties, html)", message);
            }
        });

        window.updateMdl();
    }

    /**
     * Employee Add Module constrcutor
     * @constructor
     */
    function EmployeeAddModule() {
        this.route = "/employee/new";
        this.routeContainer = $("#page-content");
        this.routeAjaxResponse = "employees/edit.html";
    }
    /**
     * Executing when route is changed to /employee/new
     * @param {object} container - jquery DOM container
     * @param {object} properties  - jquery hash router properties for current route
     * @param {object} html - loaded html file
     */
    EmployeeAddModule.prototype.routeAction = function (container, properties, html) {
        var me = this;

        $("#page-content").html(html);

        $("#employeeId-field").hide();

        fillEmployeeForm({
            EmployeeID: 0
        });

        window.updateMdl();
    }

    window.siteModules.push(new EmployeeEditModule());
    window.siteModules.push(new EmployeeAddModule());

})();
