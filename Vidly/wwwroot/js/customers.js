$(document).ready(function () {
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
                            button.parents('tr').remove();
                        }
                    });
                }
            }

        });
    });

});