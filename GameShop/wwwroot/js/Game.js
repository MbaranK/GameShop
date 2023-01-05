var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Game/GetAll"
        },

        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "category.name", "width": "15%" },
            { "data": "studio.name", "width": "15%" },
            { "data": "price", "width": "15%" },
            { "data": "stock", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="w-75 btn-group" role="group">
                            <a href="/Admin/Game/Upsert?id=${data}" 
                    class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i>Edit</a>
                            <a onClick=Delete('/Admin/Game/Delete/${data}')
                        class="btn btn-danger mx-2"><i class="bi bi-trash3"></i>Delete</a>
                    </div>
                         `
                },
                "width": "15%"
            }
        ]
    });
}