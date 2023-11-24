function UniqueCheckAjax(codeId, url, validationSpan) {
    try {
        $(document).ready(function () {
            $(codeId).on('keyup', function () {
                var Val = $(this).val();
                var data = {
                    code: Val,
                };
                $.ajax({
                    url: url,
                    type: "post",
                    dataType: "html",
                    data: data,
                    cache: false,
                    success: function (response) {
                        if (response == "true") {
                            $(validationSpan).html("Code is not unique");
                        }
                        else {
                            $(validationSpan).html("");
                        }
                    },
                    failure: function (response) {
                        console.log(response.responseText);
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        console.log(textStatus, errorThrown);
                    }
                })
            });
        });
    }
    catch (err) {
        console.log(err);
    }
}