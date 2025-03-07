import Swal from 'https://cdn.jsdelivr.net/npm/sweetalert2@11';

$(document).ready(function () {
    $('.js-delete').on('click', function () {
        var btn = $(this);

        Swal.fire({
            title: 'Are you sure that you need to delete this game?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Yes, delete it!',
            cancelButtonText: 'No, cancel!',
            reverseButtons: true
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: `/Games/Delete/${btn.data('id')}`,
                    method: 'DELETE',
                    success: function () {
                        Swal.fire(
                            'Deleted!',
                            'Game has been deleted.',
                            'success'
                        );

                        btn.parents('tr').fadeOut();
                    },
                    error: function () {
                        Swal.fire(
                            'Oooops...',
                            'Something went wrong.',
                            'error'
                        );
                    }
                });
            }
        });
    });
});
