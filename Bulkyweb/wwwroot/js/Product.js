var datatable;
$(document).ready(function () {

    loadDataTabel();
});
function loadDataTabel() {
    datatable = $('#tblData').DataTable({
        "ajax": { url: '/admin/product/getall' },

        "columns": [
            { data: 'titel' ,"width":"25%" },
            { data: 'auther', "width": "15%" },
            { data: 'isbn', "width": "15%" },
            { data: 'price', "width": "10%" },
            { data: 'category.name', "width": "10%" },
            {
                data: 'id',
                width: "25%",
                render: function (data) {
                    return `
                        <div  class="W-75 btn-group" role="group" >
                            <a href="/admin/product/upsert?id=${data}" class="btn btn-primary mx-1">
                            <i class="bi bi-pencil-square"></i> Edit </a>
                            <a  onClick=Delete('/admin/product/Delete/${data}') class="btn btn-danger mx-1">
                            <i class="bi bi-person-x"></i> Delete </a>
                        </div>
                        `
                }
            }
         
        ]
    });
}
function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        $.ajax({
            url: url,
            type: 'Delete',
            success: function (data) {
                datatable.ajax.reload();
                toastr.success(data.message)
            }

        })
    })

}
