var userId = localStorage.getItem("TidePharmacy-User").replace(/^"(.+(?="$))"$/, '$1');
const getCustomerAppointmentsUrl = "https://localhost:5001/api/Appointment/GetCustomerAppointments/" + userId;

var editor = new $.fn.dataTable.Editor( {
    ajax:  {url:getCustomerAppointmentsUrl,dataSrc:""},
    table: '#myTable',
    fields: [
        { label: 'Appointment Date',  name: 'date'  },
        { label: 'Appointment Time',  name: 'time'  }
    ]
} );

$(document).ready(function() {
    $('#myTable').DataTable( {
        ajax:  {url:getCustomerAppointmentsUrl,dataSrc:""},
        dom: 'Bfrtip',
        columns: [
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