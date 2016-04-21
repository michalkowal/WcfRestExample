"use strict";

(function () {

    var ROW_TEMPLATE =
        "<tr>" +
        "  <td>{0}</td>" +
        "  <td class=\"mdl-data-table__cell--non-numeric\">{1}</td>" +
        "  <td class=\"mdl-data-table__cell--non-numeric\">{2}</td>" +
        "  <td class=\"mdl-data-table__cell--non-numeric\">{3}</td>" +
        "  <td class=\"mdl-data-table__cell--non-numeric\">{4}</td>" +
        "  <td class=\"mdl-data-table__cell--non-numeric\">" +
        "    <button onclick=\"window.location.hash = '{5}'\" class=\"mdl-button mdl-js-button mdl-button--fab mdl-button--mini-fab mdl-js-ripple-effect mdl-button--colored\">" +
        "      <i class=\"material-icons\">edit</i>" +
        "    </button>" +
        "  </td>" +
        "  <td class=\"mdl-data-table__cell--non-numeric\">" +
        "    <button data-employeeid=\"{6}\" class=\"delete mdl-button mdl-js-button mdl-button--fab mdl-button--mini-fab mdl-js-ripple-effect mdl-button--colored\">" +
        "      <i class=\"material-icons\">delete</i>" +
        "    </button>" +
        "  </td>" +
        "</tr>";

    /**
     * Employees List Module constructor
     * @constructor
     */
    function EmployeesListModule() {
        this.title = "Employees";
        this.route = "/employee";
        this.routeContainer = $("#page-content");
        this.routeAjaxResponse = "employees/index.html";
    }
    /**
     * Executing when route is changed to /employee
     * @param {object} container - jquery DOM container
     * @param {object} properties  - jquery hash router properties for current route
     * @param {object} html - loaded html file
     */
    EmployeesListModule.prototype.routeAction = function (container, properties, html) {
        var me = this;

        $("#page-content").html(html);

        var baseUrl = window.getServiceUrl();
        var method = "GET";
        var url = baseUrl + "/employee/";
        $.ajax({
            method: method,
            url: url,
            dataType: "json",
            success: function (data) {
                me.displayList(data);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                var message = "ajax error (" + jqXHR.status + " " + errorThrown + ")";
                message += "; url: " + url;
                message += "; method: " + method;
                if (jqXHR.responseText) {
                    message += ": " + jqXHR.responseText;
                }

                console.log(jqXHR);
                window.trace("error", "Employee", "EmployeesListModule.routeAction(container, properties, html)", message);
            }
        });
    }
    /**
     * Displaying employees list by inserting new records into table
     * @param {Employee[]} data - Lsit of employees
     */
    EmployeesListModule.prototype.displayList = function (data) {
        var me = this;

        if (data) {
            $("#employees-list > tbody:last-child").html("");
            for (var i = 0; i < data.length; i++) {
                var dataRow = data[i];
                var newRow = ROW_TEMPLATE
                    .replace("{0}", dataRow.EmployeeID)
                    .replace("{1}", dataRow.Name)
                    .replace("{2}", dataRow.Address)
                    .replace("{3}", dataRow.Email ? dataRow.Email : "")
                    .replace("{4}", dataRow.PhoneNumber ? dataRow.PhoneNumber : "")
                    .replace("{5}", "/employee/" + dataRow.EmployeeID)
                    .replace("{6}", dataRow.EmployeeID);

                $("#employees-list > tbody:last-child").append(newRow);
            }

            $(".delete").click(function () {
                var emplId = $(this).data("employeeid");
                
                me.deleteEmployee(emplId);
            });

            window.updateMdl();
        }
    };
    /**
     * Sending request to delete employee by EmployeeID
     * @param {number} employeeId - Identifier of employee
     */
    EmployeesListModule.prototype.deleteEmployee = function (employeeId) {
        $("#dialog-employeeId").text(employeeId);

        var dialog = $("#confirm-dialog")[0];
        if (dialog) {
            if (!dialog.showModal) {
                dialogPolyfill.registerDialog(dialog);
            }

            
            $("#confirm-dialog").find(".agree").click(function () {
                $("#confirm-dialog").find(".agree").unbind("click");

                var baseUrl = window.getServiceUrl();
                var method = "DELETE";
                var url = baseUrl + "/employee/" + employeeId;
                $.ajax({
                    method: method,
                    url: url,
                    dataType: "text",
                    success: function (data) {
                        dialog.close();
                        window.location.hash = "/";
                        window.location.hash = "/employee";
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        dialog.close();

                        var message = "ajax error (" + jqXHR.status + " " + errorThrown + ")";
                        message += "; url: " + url;
                        message += "; method: " + method;
                        if (jqXHR.responseText) {
                            message += ": " + jqXHR.responseText;
                        }

                        console.log(jqXHR);
                        window.trace("error", "Employee", "EmployeesListModule.deleteEmployee(employeeId)", message);
                    }
                });
            });
            $("#confirm-dialog").find(".disagree").click(function () {
                $("#confirm-dialog").find(".disagree").unbind("click");
                dialog.close();
            });

            dialog.showModal();
        }
    };

    window.siteModules.push(new EmployeesListModule());

})();
