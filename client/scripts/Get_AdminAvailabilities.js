var editor = new $.fn.dataTable.Editor( {
    ajax:  {url:"https://localhost:5001/api/Availability/GetAll",dataSrc:""},
    table: '#myTable',
    fields: [
        { label: 'Admin Name', name: 'fullName' },
        { label: 'Date',  name: 'date'  },
        { label: 'Time',  name: 'time'  }
    ]
} );

$(document).ready(function() {
    $('#myTable').DataTable( {
        ajax:  {url:"https://localhost:5001/api/Availability/GetAll",dataSrc:""},
        dom: 'Bfrtip',
        columns: [
            { data: "fullName" },
            { data: "date" },
            { data: "time" }
        ],
        select: true,
        buttons: [
            { extend: 'edit',   editor: editor },
            { extend: 'remove', editor: editor }
        ]
    } );
} );