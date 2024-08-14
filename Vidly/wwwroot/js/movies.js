$(document).ready(function () {
    // Initialize DataTable
    var table = $("#movies").DataTable({
        ajax: {
            url: "/api/movies",
            dataSrc: "data"
        },
        columns: [
            {
                data: "name",
                render: function(data, type, movies) {
                    return "<a href='/movies/edit/" + movies.id + "'>" + movies.name + "</a>";
                }
            },
            {
                data: "genre",
                render: function(data, type, movies) {
                    return data ? data.name : "No genre";
                }
            },
            {
                data: "id",
                render: function(data) {
                    return "<button class='btn btn-danger btn-sm js-delete' data-movie-id=" + data + ">Delete</button>";
                }
            }
        ]
    });

    // Handle delete button click
    $('#movies').on('click', '.js-delete', function () {
        var button = $(this);
        bootbox.confirm({
            message: "Are you sure you want to delete this movie?",
            buttons: {
                confirm: {
                    label: 'Yes',
                    className: 'btn-danger'
                },
                cancel: {
                    label: 'No',
                    className: 'btn-default'
                }
            },
            callback: function (result) {
                if (result) {
                    $.ajax({
                        url: '/api/movies/' + button.attr('data-movie-id'),
                        method: 'DELETE',
                        success: function () {
                            table.row(button.parents('tr')).remove().draw();
                        },
                        error: function () {
                            bootbox.alert('An error occurred while trying to delete the customer.');
                        }
                    });
                }
            }
        });
    });
});
