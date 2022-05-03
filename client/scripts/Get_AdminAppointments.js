var userId = localStorage.getItem("TidePharmacy-User").replace(/^"(.+(?="$))"$/, '$1');
const getAdminAppointmentsUrl = "https://localhost:5001/api/Appointment/" + userId;

var editor = new $.fn.dataTable.Editor( {
    ajax:  {url:getAdminAppointmentsUrl,dataSrc:""},
    table: '#myTable',
    fields: [
        { label: 'Customer Name', name: 'fullName' },
        { label: 'Date',  name: 'date'  },
        { label: 'Time',  name: 'time'  }
    ]
} );

$(document).ready(function() {
    $('#myTable').DataTable( {
        ajax:  {url:getAdminAppointmentsUrl,dataSrc:""},
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