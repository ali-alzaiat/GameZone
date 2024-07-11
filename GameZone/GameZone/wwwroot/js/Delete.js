$(document).ready(() => {
    $('#delete').on('click', () => {
        var btn = $(this);

        $.ajax({
            url: `/Games/Delete/${btn.data('id')}`,
            method: "DELETE",
            success: () => {
                alert('success');
            },
            error: () => {
                alert('error');
            }
        })
    })
})
