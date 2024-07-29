$(document).ready(function () {

    $('#deleteModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget);
        var itemId = button.data('id');
        var itemName = button.data('name');
        var actionUrl = button.data('url');

        var modal = $(this);
        modal.find('#modalItemId').val(itemId);
        modal.find('#modalItemName').text(itemName);
        modal.find('#deleteForm').attr('action', actionUrl);
    });


    if ($('#successModal').length) {
        $('#successModal').modal('show');
    }

    if ($('#errorModal').length) {
        $('#errorModal').modal('show');
    }
});