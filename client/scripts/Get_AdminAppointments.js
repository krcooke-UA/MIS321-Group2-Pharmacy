var userId = localStorage.getItem("TidePharmacy-User").replace(/^"(.+(?="$))"$/, '$1');
const getAdminAppointmentsUrl = "https://localhost:5001/api/Appointment/" + userId;

var editor = new $.fn.dataTable.Editor( {
    ajax:  {url:getAdminAppointmentsUrl,dataSrc:""},
    table: '#myTable',
    fields: [
        { label: 'Customer Name', name: 'fullName' },
        { label: 'Date',  name: 'date'  },
        { label: 'Time',  name: 'time'  },
        { label: 'Appointment Type', name: 'type_Text'}
    ]
} );

$(document).ready(function() {
    var currentTime = getTime();
    var currentDate = getDate();
    
    $('#myTable').DataTable( {
        ajax:  {url:getAdminAppointmentsUrl,dataSrc:""},
        dom: 'Bfrtip',
        columns: [
            { data: "fullName", render: function(data) {
                return `<span style="display: flex; flex-flow: row nowrap; justify-content: center;">${data}</span>`;
            } },
            { data: "date", render: function(data) {
                return `<span style="display: flex; flex-flow: row nowrap; justify-content: center;">${data}</span>`;
            } },
            { data: "time", render: function(data) {
                return `<span style="display: flex; flex-flow: row nowrap; justify-content: center;">${data}</span>`;
            } },
            { data: "type_Text", render: function(data) {
                return `<span style="display: flex; flex-flow: row nowrap; justify-content: center;">${data}</span>`;
            } }
        ],
        rowCallback: function(row, data, index){
            var date = data.date;
            var time = timeConversion(data.time);

            if(date == currentDate) {
                $(row).find('td:eq(0)').css('background-color', '#e3faff');
                $(row).find('td:eq(1)').css('background-color', '#e3faff');
                $(row).find('td:eq(2)').css('background-color', '#e3faff');
                $(row).find('td:eq(3)').css('background-color', '#e3faff');
            }
            if ( (date < currentDate) || (date <= currentDate && time < currentTime) ) {
                $(row).find('td:eq(0)').css('background-color', '#e3e3e3');
                $(row).find('td:eq(1)').css('background-color', '#e3e3e3');
                $(row).find('td:eq(2)').css('background-color', '#e3e3e3');
                $(row).find('td:eq(3)').css('background-color', '#e3e3e3');
                $(row).find('td:eq(0)').css('opacity', '0.7');
                $(row).find('td:eq(1)').css('opacity', '0.7');
                $(row).find('td:eq(2)').css('opacity', '0.7');
                $(row).find('td:eq(3)').css('opacity', '0.7');
            }
        },
        select: true
    } );
} );

function checkTime(i) {
    if (i < 10) {
      i = "0" + i;
    }
    return i;
  }
function getTime() {
    var currentTime = new Date();
    var h = currentTime.getHours();
    var m = currentTime.getMinutes();
    var s = currentTime.getSeconds();
    // add a zero in front of numbers<10
    m = checkTime(m);
    s = checkTime(s);
    time = h + ":" + m + ":" + s;
    return time;
}
function getDate() {
    var currentYear = new Date();
    var dd = currentYear.getDate();
    var mm = currentYear.getMonth()+1; 
    var yyyy = currentYear.getFullYear();
    if(dd<10) 
    {
        dd='0'+dd;
    } 
    if(mm<10) 
    {
        mm='0'+mm;
    } 
    currentYear = yyyy+'-'+mm+'-'+dd;
    return currentYear;
}
function timeConversion(s) {
    var time = s.toLowerCase().split(':');
    var hours = parseInt(time[0]);
    var _ampm = time[2];
    if (_ampm.indexOf('am') != -1 && hours == 12) {
        time[0] = '00';
    }
    if (_ampm.indexOf('pm')  != -1 && hours < 12) {
        time[0] = hours + 12;
    }
    return time.join(':').replace(/(am|pm)/, '');
}