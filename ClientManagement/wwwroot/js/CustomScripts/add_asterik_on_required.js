$(function () {
    addAsterikToRequiredFields();
});
function addAsterikToRequiredFields() {
    $('input,select').each(function () {
        var req = $(this).attr('data-val-required');
        if (undefined != req && req != "" && $(this).attr('type') != "hidden") {
            var label = $(this).closest(".form-group").find("label");
            var text = label.text();
            if (text.length > 0) {
                label.append('<span style="color:red"> *</span>');
            }
        }
    });
}

