$("#Select-Subject").on('change', function (e) {
    $.ajax({
        type: "GET",
        url: '/Ajax/GetIdCourseBySubject',
        data: {
            IdSubject: $("#Select-Subject option:selected").val()
        },
        dataType: "json",
        success: function (msg) {
            if (msg == 0)
                $(".Course").each(function () {
                    $(this).show();
                });
            else
                $(".Course").each(function () {
                    var course = $(this);
                    var courseElement = $(this)[0].children[0];
                    course.hide();
                    $.grep(msg, function (n) {
                        if (courseElement.value == n.IdSubject)
                            course.show();
                    });
                });
        }
    })
});
