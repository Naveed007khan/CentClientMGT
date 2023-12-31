﻿$(function () {
    $("#sidebar-nav-div a").each(function (i, v) {
        if (window.location.pathname === $(v).attr("href")) {
            console.log($(v).attr("href"));
            $(v).addClass("active"); $(v).parent().parent().show();
            var $this = $(v),
                $bc = $('<div class="breadcrumb-item"></div>');

            $this.parents('li').each(function (n, li) {
                var $a = $(li).children('a').clone();
                $a.addClass("breadcrumb-item").removeClass("nav-link active")
                $a.children("i").addClass("mr-2");
                $bc.prepend($a);
                switch (n) {
                    case 0:
                        $("#current-page-sub-dir").html($a.text()); break;
                    case 1:
                        $("#current-page-main-dir").html($a.text()); break;
                }
            });
            $('#breadcrumb-right').html($bc.prepend('<a href="/Dashboard/Index" class="breadcrumb-item"><i class="icon-home2 mr-2"></i> Home </a>'));
            return false;
        }
        else
            $(v).removeClass("active");
    })

    $("body").on("click", ".attachment-hyperlink", function (e) {
        e.preventDefault();
        var url = $(this).attr('href');
        window.open(url, '_blank');
    })
})

