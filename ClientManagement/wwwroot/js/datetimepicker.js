var dateFormat = "dddd, DD MMMM, YYYY hh:mm A";
function InitializeDateTimePicker(id) {
    $('#' + id).datetimepicker({
        format: dateFormat,
        useCurrent: false,
        allowInputToggle: true,
    });
}
function InitializeDependentDateTimePicker(sourceId, dependentId) {
    $('#' + sourceId).datetimepicker({
        format: dateFormat,
        useCurrent: false,
        allowInputToggle: true,
    });
    $('#' + dependentId).datetimepicker({
        format: dateFormat,
        useCurrent: false,
        allowInputToggle: true,
    });
    $('#' + sourceId).on("change.datetimepicker", function (e) {
        $('#' + dependentId).datetimepicker('minDate', e.date);
    });
    $('#' + dependentId).on("change.datetimepicker", function (e) {
        $('#' + sourceId).datetimepicker('maxDate', e.date);
    });
}