$(document).ready(function () {
    $('.project').click(function () {
        $.ajax({
            type: 'GET',
            dataType: 'json',
            data: $(this).serialize(),
            url: 'Project/GetProjects',
            success: function (result) {
                for (var i = 0; i < 3; i++) {
                    $('#project-result').append('<p>' + result[i].name + '</p>');
                }

            }
        })
    })

});