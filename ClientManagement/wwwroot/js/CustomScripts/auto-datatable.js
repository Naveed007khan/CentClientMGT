var dataTable;
var actionIcons = {};
var showSelectedFilters = true;
actionIcons["View"] = "fas fa-folder-open";
actionIcons["Update"] = "icon-pencil";
actionIcons["Detail"] = "fas fa-folder-open"
actionIcons["Approve"] = "icon-file-text"
actionIcons["Delete"] = "icon-trash";
actionIcons["Test"] = "icon-plus-circle2";
actionIcons["Report"] = "icon-copy";
actionIcons["Invoice"] = "icon-file-text";
actionIcons["ResetPassword"] = "icon-key"; // this needs to change
actionIcons["Payment"] = "icon-pen"; // this needs to change
actionIcons["EntityDetail"] = "icon-copy"; // this needs to change
actionIcons["PaymentDetails"] = "icon-copy"; // this needs to change
actionIcons["Approve"] = "icon-checkmark4";

function CallBackFunctionality() {
}
CallBackFunctionality.prototype.GetFunctionality = function () {
    return "";
}

function InitializeDataTables(dtColumns, dataUrl = "") {
    var currentController = window.location.pathname.split('/')[1];
    var dataAjaxUrl = dataUrl;
    if (dataAjaxUrl === "") {
        dataAjaxUrl = "/" + currentController + "/Search";
    }
    //custom modification for cbo index in activity 
    var tableId = $("table.datatable-basic.dataTable").attr('id');
    var formId = $(".search-form").attr('id');
    var actionsList = [];
    //For Showing Loader
    $("#" + tableId).append("<button id='loader' style='display:none' type='button' class='btn bg-custom-dark btn-float rounded-round'><i class='icon-spinner4 spinner'></i></button>");

    $(document).off('click', '.clear-form-btn');
    $(document).on('click', '.clear-form-btn', function () {
        ClearDatatableSearch(dataAjaxUrl, tableId, formId, actionsList, dtColumns);
    });
    $(document).off('click', '.badge-datatable-clear');
    $(document).on('click', '.badge-datatable-clear', function () {
        ClearDatatableSearch(dataAjaxUrl, tableId, formId, actionsList, dtColumns);
    });
    $(document).off('click', '.badge-datatable-search');
    $(document).on('click', '.badge-datatable-search', function () {
        var name = $(this).attr('data-input-name');
        RemoveSearchFilterInput(name, dataAjaxUrl, tableId, formId, actionsList, dtColumns);
    });

    $(document).off('click', '.search-form-btn');
    $(document).on('click', '.search-form-btn', function () {
        SearchDataTable(dataAjaxUrl, tableId, formId, actionsList, dtColumns);
    });
    $(document).off('click', '#search-filters');
    $(document).on('click', '#search-filters', function () {
        $(".search-filter-container").show();
        $(".list-container").hide();
    });
    $(document).off('click', '#back-to-list');
    $(document).on('click', '#back-to-list', function () {
        $(".search-filter-container").hide();
        $(".list-container").show();
    });
    $(document).off('click', '.delete');
    $(document).on('click', '.delete', function (e) {
        e.preventDefault();
        DeleteDataItem($(this).attr('href'));

    });
    $(document).off('click', '.cancel');
    $(document).on('click', '.cancel', function (e) {
        e.preventDefault();
        DeleteDataItem($(this).attr('href'), "Yes, cancel it!", "No!", $(this).data("return-url"));

    });
    FilterDataTable(dataAjaxUrl, tableId, formId, actionsList, dtColumns);
}
function FilterDataTable(dataAjaxUrl, tableId, formId, actionsList, dtColumns) {

    var columnsIndexExcludingAction = [];
    for (var i = 0; i <= (dtColumns.length - 1); i++) {
        columnsIndexExcludingAction.push(i);
    }
    dataTable = $('#' + tableId).dataTable({
        "serverSide": true,
        "proccessing": true,
        "searching": true,
        "responsive": true,
        "ordering": false,
        "lengthMenu": [[10, 25, 50, 250], [10, 25, 50, 250]],
        "dom": "<'datatable-header'<'float-right'l>f>" +//<'datatable-header'B<'float-right'l>f> to add button
            "<'datatable-scroll'tr>" +
            "<'datatable-footer'p>",
        "ajax": {
            url: dataAjaxUrl,
            type: 'POST',
            dataType: "json",
            "data": function (searchParams) {
                $("#" + tableId + " td").removeAttr("colspan");
                if ($("#loader").length > 0) {
                    $('#' + tableId).block({
                        message: $("#loader"),
                        centerY: false,
                        centerX: false,
                        css: {
                            margin: 'auto',
                            border: 'none',
                            padding: '15px',
                            backgroundColor: 'transparent',
                            '-webkit-border-radius': '10px',
                            '-moz-border-radius': '10px',
                            color: '#fff'
                        }

                    });
                }
                if ($('#' + formId).length > 0) {
                    $('#' + formId + ' :input, #' + formId + ' select').each(function (key, val) {
                        if (val.value !== "") {
                            searchParams[val.name] = $(val).val();
                        }
                    });
                }
                if (searchParams.length === -1) {
                    searchParams["Pagination"] = false;
                }
                else {
                    searchParams["Pagination"] = true;
                }
                searchParams["CurrentPage"] = (searchParams.start / searchParams.length) + 1;
                searchParams["PerPage"] = searchParams.length;
                searchParams["CalculateTotal"] = true;
                //searchParams["OrderByColumn"] = searchParams.columns[searchParams.order[0].column].data;
                //searchParams["OrderDir"] = searchParams.order[0].dir;
                searchParams["Draw"] = searchParams.draw;
                return searchParams;
            },
            "dataSrc": function (json) {
                actionsList = json.ActionsList;
                showSelectedFilters = json.ShowSelectedFilters;
                SetSearchFilters();
                return json.Items;
            }
        },
        "language": {
            "search": "",
            "searchPlaceholder": "Search"
        },
        columns: dtColumns,
        "columnDefs": [
            {
                "targets": columnsIndexExcludingAction,
                "render": function (data, type, row, meta) {
                    if (dtColumns[meta.col].title !== 'Action') {
                        if (dtColumns[meta.col].format === 'html') {
                            return RenderHtml(data, dtColumns, meta);
                        }
                        else if (dtColumns[meta.col].format === 'numeric') {
                            return RenderNumericValue(data, dtColumns, meta);
                        }
                        else {
                            return data;
                        }
                    }
                    else {
                        return GetActionLinks(actionsList, data);
                    }
                }
            },
            //{
            //    "targets": -1,
            //    "createdCell": function (td, cellData, rowData, row, col) {
            //        if (dtColumns[col].title === "Action") {
            //            GetActionLinks(actionsList, cellData, td);
            //        }
            //    }
            //}
        ],

        "buttons": {
            dom: {
                button: {
                    tag: 'button',
                    className: 'btn rounded-round bg-custom-dark datatable-extension-button'
                }
            },
            'buttons': [
                {
                    extend: 'copy',
                    exportOptions: {
                        columns: ':visible',
                        page: 'current'
                    }
                },
                {
                    extend: 'csv',
                    exportOptions: {
                        columns: ':visible',
                        page: 'current'
                    }
                },
                {
                    extend: 'excel',
                    exportOptions: {
                        columns: ':visible',
                        page: 'current'
                    }
                },
                {
                    extend: 'pdf',
                    exportOptions: {
                        columns: ':visible',
                        page: 'current'
                    }
                },
                {
                    extend: 'print',
                    exportOptions: {
                        columns: ':visible',
                        page: 'current'
                    }
                },
                'colvis'
            ]

        },
        "drawCallback": function (settings) {
            $('#' + tableId).unblock();
            new CallBackFunctionality().GetFunctionality();
        }

    });
    SetSearchFilters();
}
function GetActionLinks(actionsList, cellData) {
    var actionHtml = "";
    if (actionsList.length > 3) {

        actionHtml = "<div class='list-icons'>";
        actionHtml += "<div class='dropdown show'>";
        actionHtml += "<div class='dropdown show'>";
        actionHtml += "<a href='#' class='list-icons-item' data-toggle='dropdown' aria-expanded='true'>";
        actionHtml += "<i class='icon-menu9'></i>";
        actionHtml += "</a>";
        actionHtml += "<div class='dropdown-menu dropdown-menu-right'>";

        $.each(actionsList, function (index, actionItem) {
            if (cellData.IsUserDefined === false || cellData.IsUserDefined === undefined)
                actionHtml += GetHref(actionItem, cellData);
        });
        actionHtml += "</div>";
    }
    else {
        $.each(actionsList, function (index, actionItem) {
            //to avoid the system defined record in Account table to be deleted.
            if (cellData.IsUserDefined === false || cellData.IsUserDefined === undefined)
                actionHtml += GetHref(actionItem, cellData);
        });
    }
    return actionHtml;
    //$(td).html(actionHtml);
}
function GetHref(actionItem, cellData) {
    var link = "javascript:void(0)";
    if (actionItem.Href !== '') {
        link = actionItem.Href;
        if (link.split('/').length > 3) {
            link = "/" + link.split('/')[1] + "/" + link.split('/')[2] + "/" + GetCellObjectValue(cellData, link.split('/')[3]);
        }
        if (link.split('/').length > 2) {
            linkAndQueryParams = actionItem.Href.split('?');
            if (linkAndQueryParams.length > 1) {
                var queryParams = linkAndQueryParams[1].split('&');
                link = actionItem.Href.split('?')[0] + "?";
                $.each(queryParams, function (index, value) {
                    link = link + $.trim(value.split('=')[0]) + "=" + GetCellObjectValue(cellData, $.trim(value.split('=')[1])) + "&";

                });
                if (queryParams.length > 1) {
                    link = link.slice(0, -1);
                }
            }
        }
    }
    var appendClass = "";
    var dataAttributes = "";
    if (actionItem.Action === "Delete") {
        appendClass = "delete";
    }
    //if (actionItem.Action === "Approve") {
    //    appendClass = "review-user";
    //}
    if (actionItem.Action === "Cancel") {
        appendClass = "cancel";
        dataAttributes = "data-return-url='" + actionItem.ReturnUrl + "'";
    }
    if (actionItem.Class !== null && actionItem.Class !== "") {
        appendClass = " " + actionItem.Class;
    }
    var customAttributes = "";
    if (actionItem.Attr !== null && actionItem.Attr.length > 0) {
        actionItem.Attr.forEach(function (v, i) {
            customAttributes += "attr-" + v.toLowerCase() + '="' + GetCellObjectValue(cellData, v) + '" ';
        });
    }
    return '<a href="' + link + '" ' + dataAttributes + ' class="datatable-action ' + appendClass + '" ' + customAttributes + '> <i class="' + actionIcons[actionItem.Title] + '"></i> ' + actionItem.LinkTitle + '</a > ';
}

function GetCellObjectValue(cellData, Prop) {
    if (Prop.indexOf(".") !== -1) {
        return cellData["" + Prop.split('.')[0]]["" + Prop.split('.')[1]];
    }
    else {
        return cellData['' + Prop];
    }
}
function RenderHtml(data, dtColumns, meta) {
    var classValue = dtColumns[meta.col].cssClasses;
    if (dtColumns[meta.col].formatValue === "checkbox") {
        return '<input type="checkbox" class="checkbox-items ' + classValue + '" value="' + data + '" />';
    }
    else if (dtColumns[meta.col].formatValue === "textbox") {
        return '<input type="text" class="text-box-items ' + classValue + '" value="' + data + '" />';
    }
    else if (dtColumns[meta.col].formatValue === "number-textbox") {
        return '<input type="number" class="number-text-box-items ' + classValue + '" value="' + data + '" />';
    }
    else if (dtColumns[meta.col].formatValue === "badge") {
        return '<span class="badge ' + data + '">' + data + '</span>';
    }
    else if (dtColumns[meta.col].formatValue === "hidden") {
        return '<div>' + data + '</div><input type="hidden" class="hidden ' + classValue + '" value="' + data + '">';
    }
}
function RenderNumericValue(data, dtColumns, meta) {
    if (dtColumns[meta.col].formatValue !== null && dtColumns[meta.col].formatValue !== "") {
        return data.toFixed(2);
    }
    return data.toFixed(dtColumns[meta.col].formatValue);
}
function SetSearchFilters() {
    var containerHtml = "";
    if (showSelectedFilters) {
        $('.search-filter-container input, .search-filter-container select').each(
            function (index) {
                var input = $(this);
                if (input.attr('type') != "hidden" && input.val() != "") {
                    var value = input.val();
                    var inputName = input.attr('name');
                    if ($(input).data('select2')) {
                        value = $(input).select2('data')[0].text
                    }
                    if (containerHtml == "") {
                        containerHtml = "<span class='mr-1'>Filters: </span>";
                    }
                    containerHtml += "<span class='badge badge-datatable-search mb-1' data-input-name='" + inputName + "'>" + $(input).parent().find("label").html() + " : " + value + "</span>";
                }
            }
        );
        if (containerHtml != "") {
            containerHtml += "<span class='datatable-clear-container'><span class='badge badge-datatable-clear mb-1'>Clear</span></span>";
            containerHtml = "<div class='row col-12'>" + containerHtml + "</div>";
        }
        $(".selected-filters-container").html(containerHtml);
    }
    else {
        $(".selected-filters-container").html("");
    }
}
function ClearDatatableSearch(dataAjaxUrl, tableId, formId, actionsList, dtColumns) {
    $('#' + formId).trigger("reset");
    $('select[class*="select2"]').each(function (i, element) {
        $(element).val('').trigger('change');
    });
    SearchDataTable(dataAjaxUrl, tableId, formId, actionsList, dtColumns);
}
function RemoveSearchFilterInput(name, dataAjaxUrl, tableId, formId, actionsList, dtColumns) {
    var input = $("#" + formId + " input[name=" + name + "]");
    if ($(input).data('select2')) {
        $(input).val('').trigger('change');
    }
    else {
        $(input).val("");
    }
    SearchDataTable(dataAjaxUrl, tableId, formId, actionsList, dtColumns);
}
function SearchDataTable(dataAjaxUrl, tableId, formId, actionsList, dtColumns) {
    $(".list-container").show();
    $(".search-filter-container").hide();
    $("#" + tableId).dataTable().fnDestroy();
    FilterDataTable(dataAjaxUrl, tableId, formId, actionsList, dtColumns);
}

function DeleteDataItem(url, confirmBtnText = "", cancelBtnText = "", deleteReturnUrl = "") {
    if (confirmBtnText === "") {
        confirmBtnText = "Yes, delete it!";
    }
    if (cancelBtnText === "") {
        cancelBtnText = "No, cancel!";
    }
    var deleteUrl = url;
    swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        type: 'error',
        //icon: 'warning',
        showCancelButton: true,
        confirmButtonText: confirmBtnText,
        cancelButtonText: cancelBtnText,
        confirmButtonClass: 'btn btn-success',
        cancelButtonClass: 'btn btn-danger',
        buttonsStyling: false
    }).then(function (result) {
        if (result.value) {
            DeleteCardItem(deleteUrl).then(function (ajaxResult) {
                if (ajaxResult) {
                    if (deleteReturnUrl === "" || deleteReturnUrl === null || deleteReturnUrl === undefined) {
                        location.reload();
                    }
                    else {
                        window.location.href = deleteReturnUrl;
                    }
                }
            });

        }
        else if (result.dismiss === swal.DismissReason.cancel) {
        }
    });
}
function DeleteCardItem(url) {
    return $.ajax({
        url: url,
        type: 'POST',
        success: function (res) {
        }
    });
}