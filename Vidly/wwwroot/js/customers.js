$(document).ready(function () {
    // Initialize DataTable
    var table = $("#customers").DataTable({
        ajax: {
            url: "/api/customers",
            dataSrc: "data"
        },
        columns: [
            {
                data: "name",
                render: function(data, type, customer) {
                    return "<a href='/customers/edit/" + customer.id + "'>" + customer.name + "</a>";
                }
            },
            {
                data: "membershipType",
                render: function(data, type, customer) {
                    return data ? data.name : "No Membership";
                }
            },
            {
                data: "id",
                render: function(data) {
                    return "<button class='btn btn-danger btn-sm js-delete' data-customer-id=" + data + ">Delete</button>";
                }
            }
        ]
    });

    // Handle delete button click
    $('#customers').on('click', '.js-delete', function () {
        var button = $(this);
        bootbox.confirm({
            message: "Are you sure you want to delete this customer?",
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
                        url: '/api/customers/' + button.attr('data-customer-id'),
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
