$("#Select-Subject").on('change', function (e) {
    $.ajax({
        type: "GET",
        url: '/Ajax/GetIdCourseBySubject',
        data: {
            IdSubject: $("#Select-Subject option:selected").val()
        },
        dataType: "json",
        success: function (msg) {
            $(".Course").each(function () {
                $.grep(msg, function (n) {
                    if ($(this).val == n.Id)
                        $(this).hide(true);
                });
            });
        }
    })
});
