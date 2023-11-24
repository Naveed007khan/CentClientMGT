
function SetErrorProperties(element, validationMessage, status = true) {
    if (status) {
        $(element).removeClass("input-validation-error").addClass("valid");
        $(element).siblings(".validation-invalid-label").addClass("field-validation-valid").removeClass("field-validation-error").html("");
    }
    else {
        $(element).addClass("input-validation-error").removeClass("valid").attr("aria-invalid",true);
        $(element).siblings(".validation-invalid-label").removeClass("field-validation-valid").addClass("field-validation-error").html(validationMessage);
    }
}