

function Delete(url) {
    Swal.fire({
        title: 'Emin misiniz?',
        text: "Soruyu silmek üzeresiniz",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Evet, sil',
        cancelButtonText: 'İptal!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    Swal.fire(
                        (data.message))
                        .then(okay => {
                            if (okay) {
                                location.reload();
                            }
                        });                    
                }
            })
        }
    })
}