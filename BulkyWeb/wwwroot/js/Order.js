var dataTable;

$(document).ready(function () {

    var url = window.location.search;
    if (url.includes("inprocess")) {
        loadDataTable("inprocess");
    }
    else {
        if (url.includes("pending")) {
            loadDataTable("pending");
        }
        else {
            if (url.includes("completed")) {
                loadDataTable("completed");
            }
            else {
                if (url.includes("approved")) {
                    loadDataTable("approved");
                }
                else {
                    loadDataTable("all");
                }
            }
        }

    }

   
});

function loadDataTable(status) {
    console.log("I got here")
    //dataTable = $('#tblData').DataTable({
    //    data: [
    //        { title: 'Sample Book', isbn: '12345', listPrice: 20, author: 'John Doe', category: { name: 'Fiction' } }
    //    ],
    //    "columns": [
    //        { data: 'title', "width": "20%" },
    //        { data: 'isbn', "width": "15%" },
    //        { data: 'listPrice', "width": "10%" },
    //        { data: 'author', "width": "20%" },
    //        { data: 'category.name', "width": "15%" }
    //    ]
    //});
    
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/Order/GetAll?status=' + status },

        "columns": [
            { data: 'id', "width": "5%" },
            { data: 'name', "width": "15%" },
            { data: 'phoneNumber', "width": "20%" },
            { data: 'applicationuUser.email', "width": "15%" },
            { data: 'orderTotal', "width": "10%" },
            { data: 'orderStatus', "width": "10%" },
       
            {
                data: 'id',
                "render": function (data)
                {
                     
                    return `<div class="w-75 btn-group" role="group">
                    <a href="/admin/order/details?orderId=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i></a>
                    
                    </div>`
                },
                "width" : "25%"
            }
        ]
   }); 
}





