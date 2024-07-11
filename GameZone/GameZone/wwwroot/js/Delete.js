$(document).ready(() => {
    $('.delete').on('click',function() {
        var btn = $(this);
        const swalWithBootstrapButtons = Swal.mixin({
            customClass: {
                confirmButton: "btn btn-danger",
                cancelButton: "btn btn-success"
            },
            buttonsStyling: false
        });
        swalWithBootstrapButtons.fire({
            title: "Are you sure?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: "Yes, delete it!",
            cancelButtonText: "No, cancel!",
            reverseButtons: true
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: `/Games/Delete/${btn.data('id')}`,
                    method: "DELETE",
                    success: () => {
                        swalWithBootstrapButtons.fire({
                            title: "Deleted!",
                            text: "Game has been deleted.",
                            icon: "success"
                        });
                        btn.parents('#game').fadeOut();
                    },
                    error: () => {
                        swalWithBootstrapButtons.fire({
                            title: "Ooooooops ..!",
                            text: "Something went wrong.",
                            icon: "error"
                        });
                    }
                })

                
            }
        });

        
    })
})
