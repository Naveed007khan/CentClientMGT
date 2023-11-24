function InitDatatables(tableId, ajaxUrl, columns, formId) {
    $('#' + tableId).dataTable({
        "serverSide": true,
        "proccessing": true,
        "searching": true,
        "lengthMenu": [[10, 25, 50, 200], [10, 25, 50, 200]],
        "dom": "<'datatable-header'B<'float-right'l>f>" +
            "<'datatable-scroll'tr>" +
            "<'datatable-footer'p>",
        "ajax": {
            url: ajaxUrl,
            type: 'POST',
            dataType: "json",

            "data": function (searchParams) {
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
                //if ($('#' + formId).length > 0) {
                //    $('#' + formId + ' :input, #' + formId + ' select').each(function (key, val) {
                //        if (val.value !== "") {
                //            searchParams[val.name] = $(val).val();
                //        }
                //    });
                //}
                if (searchParams.length === -1) {
                    searchParams["Pagination"] = false;
                }
                else {
                    searchParams["Pagination"] = true;
                }
                searchParams["CurrentPage"] = (searchParams.start / searchParams.length) + 1;
                searchParams["RecordsPerPage"] = searchParams.length;
                searchParams["CalculateTotal"] = true;
                searchParams["OrderByColumn"] = searchParams.columns[searchParams.order[0].column].data;
                searchParams["OrderDir"] = searchParams.order[0].dir;
                searchParams["Draw"] = searchParams.draw;
                return searchParams;
            },
            "dataSrc": function (json) {
                if (json.ProcessManuallyForDT === true) {
                    var lstObjects = [];
                    json.ResultList.forEach(function (item) {
                        var dynamicObject = {};
                        dynamicObject['EntityName'] = item.EntityName;
                        item.DynamicColumns.forEach(function (di) {
                            dynamicObject[di.Key] = di.Value;
                        });
                        lstObjects.push(dynamicObject);
                    });
                    return lstObjects;
                }
                else {
                    return json.ResultList;
                }

            }
        },
        "language": {
            "search": "",
            "searchPlaceholder": "Search..."
        },
        "columns": columns,
        "buttons": {
            dom: {
                button: {
                    tag: 'button',
                    className: 'btn btn-danger rounded-round m-1 bg-pink'
                }
            },
            //buttons: [{
            //    extend: 'excel',
            //    className: 'btn btn-sm btn-success',
            //    titleAttr: 'Excel export.',
            //    text: 'Excel',
            //    filename: 'excel-export',
            //    extension: '.xlsx'
            //}, {
            //    extend: 'copy',
            //    className: 'btn btn-sm btn-primary',
            //    titleAttr: 'Copy table data.',
            //    text: 'Copy'
            //}],
            "buttons": {
                dom: {
                    button: {
                        tag: 'button',
                        className: 'btn rounded-round m-1 bg-custom-dark'
                    }
                },
                'buttons': [
                    {
                        extend: 'copy',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'csv',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'excel',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'pdf',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'print',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    'colvis'
                ]

            },

        },
        "drawCallback": function (settings) {
            $('#' + tableId).unblock();
        }

    });
}