function ProcessFileUpload(element) {

    var elementId = $(element).attr('id');
    var fileExtension = ['jpeg', 'jpg', 'png', 'gif', 'bmp', 'doc', 'docx', 'pdf', 'xls', 'xlsx'];
    if ($.inArray($(element).val().split('.').pop().toLowerCase(), fileExtension) === -1) {
        $(element).val("");
        ShowFileUploadErrorMessage(elementId, "Allowed formats are: " + fileExtension.join(', '));
    }
    else {
        $(element).closest("div.input-group-btn").find("button#UploadRemoveButton").removeClass('hideElement');
        //$('#UploadRemoveButton').removeClass('hideElement');
        ShowFileThumbnail(element);
        $(element).closest("div.input-group-btn").prev().find("input#InputDisplay").attr('placeholder', $(element).val());
        //$('#InputDisplay').attr('placeholder',$(element).val());
        RemoveFileUploadErrorMessage(elementId);
    }
}
function ShowFileThumbnail(input) {
    
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#file-thumbnail').attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}
//
//function for displaying error messages
function ShowFileUploadErrorMessage(id, error) {

    var errorSpan = $("span[data-valmsg-for = '" + id + "']");
    errorSpan.removeClass("field-validation-valid");
    errorSpan.addClass("field-validation-error");
    errorSpan.html('<span id="Image-error">' + error + '.</span>');
}

//
//function for removing error messages
function RemoveFileUploadErrorMessage(id) {

    var errorSpan = $("span[data-valmsg-for = '" + id + "']");
    errorSpan.addClass("field-validation-valid");
    errorSpan.removeClass("field-validation-error");
    errorSpan.html('');
}
//function for removing file
function RemoveFile(element) {
    $(element).closest("div.fileinput").find("input[type='file']").val("");
    $(element).closest("div.fileinput").prev().attr('src', '');
    var previous = $(element).closest("div.input-group-btn").prev().find("input.file-caption-name");
    $(element).closest("div.input-group-btn").prev().find("input.file-caption-name").attr('placeholder', 'Select file...');

    //$(element).closest("div.input-group-btn").prev().attr('placeholder', 'Select file...');
    $(element).addClass('hideElement');
}