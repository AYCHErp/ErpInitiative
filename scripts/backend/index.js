$("#EnabledCheckbox").prop("checked", $("#EnabledCheckbox").attr("data-checked") === "true");

window.validator.initialize($(".initiative.nepal.configuration"));

$("#SaveButton").off("click").on("click", function () {
    function request(model) {
        const url = "/dashboard/in";
        const data = JSON.stringify(model);

        return window.getAjaxRequest(url, "PUT", data);
    };

    if (!window.validator.validate($(".initiative.nepal.configuration"))) {
        return;
    };

    function getModel() {
        return window.serializeForm($(".initiative.nepal.configuration.segment"));
    };

    const model = getModel();
    const ajax = request(model);

    ajax.success(function () {
        window.displaySuccess();
    });

    ajax.fail(function (xhr) {
        window.logAjaxErrorMessage(xhr);
    });
});
