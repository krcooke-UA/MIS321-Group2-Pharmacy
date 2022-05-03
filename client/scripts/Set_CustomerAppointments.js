const date = new Date();
const availabilityUrl = "https://localhost:5001/api/Availability";
const appointmentUrl = "https://localhost:5001/api/Appointment";
var userId = localStorage.getItem("TidePharmacy-User").replace(/^"(.+(?="$))"$/, '$1');
var availabilityList = [];
var availability = {};
var viewingDate;

const renderCalendar = () => {
  var trueMonth = date.getMonth() + 1;
  var currentDay = new Date().getDate();
  var currentMonth = new Date().getMonth();
  var currentYear = new Date().getYear();

  date.setDate(1);

  const monthDays = document.querySelector(".days");

  const lastDay = new Date(
    date.getFullYear(),
    date.getMonth() + 1,
    0
  ).getDate();

  const prevLastDay = new Date(
    date.getFullYear(),
    date.getMonth(),
    0
  ).getDate();

  const firstDayIndex = date.getDay();

  const lastDayIndex = new Date(
    date.getFullYear(),
    date.getMonth() + 1,
    0
  ).getDay();

  const nextDays = 7 - lastDayIndex - 1;

  const months = [
    "January",
    "February",
    "March",
    "April",
    "May",
    "June",
    "July",
    "August",
    "September",
    "October",
    "November",
    "December",
  ];

  document.querySelector(".date h1").innerHTML = months[date.getMonth()];

  document.querySelector(".date p").innerHTML = new Date().toDateString();

  let days = "";

  for (let x = firstDayIndex; x > 0; x--) {
    days += `<div class="prev-month">${prevLastDay - x + 1}</div>`;
  }

  for (let i = 1; i <= lastDay; i++) {
    mm = date.getMonth() + 1;
    dd = i
    yyyy = date.getFullYear();
    if (dd < 10) dd = '0' + dd;
    if (mm < 10) mm = '0' + mm;
    var selectedDate = yyyy + "-" + mm + "-" + dd
    if (
      (date.getMonth() < currentMonth && date.getYear() == currentYear) ||
      (i < currentDay && date.getMonth() === currentMonth)
    ) {
      days += `<div class="prev-date">${i}</div>`;
    } else if (
      i === currentDay &&
      date.getMonth() === currentMonth
    ) {
      days += `<div class="today" id=${selectedDate} name=${selectedDate} onClick="selectDate(event)" method="get">${i}</div>`;
    } else {
      days += `<div id=${selectedDate} name=${selectedDate} onClick="selectDate(event)" method="get">${i}</div>`;
    }
  }

  for (let j = 1; j <= nextDays; j++) {
    days += `<div class="next-month">${j}</div>`;
  }
  monthDays.innerHTML = days;
};

document.querySelector(".prev").addEventListener("click", () => {
  date.setMonth(date.getMonth() - 1);
  renderCalendar();
});

document.querySelector(".next").addEventListener("click", () => {
  date.setMonth(date.getMonth() + 1);
  renderCalendar();
});

function OnLoad() {
  date.setMonth(date.getMonth());
  renderCalendar();
}


function selectDate(e) {
  var element = e.target || e.srcElement;
  console.log(element.id);
  viewingDate = element.id;
  GetDateAvailability(element.id);
}

function GetDateAvailability(selectedDate) {
  const selectedDateApiUrl = availabilityUrl + "/GetAvailability/" + selectedDate;
  console.log(selectedDateApiUrl);
  var requestOptions = {
    method: 'GET',
    redirect: 'follow'
  };
  fetch(selectedDateApiUrl, requestOptions).then(function(response) {
    return response.json();
  }).then(function(json) {
    showTimes(json, selectedDate);
  }).catch(function(error) {
      console.log(error);
  });
}

function showTimes(json, selectedDate) {
  var today = new Date();
    mm = today.getMonth() + 1;
    dd = today.getDate();
    yyyy = today.getFullYear();
    if (dd < 10) dd = '0' + dd;
    if (mm < 10) mm = '0' + mm;
  var date = yyyy + "-" + mm + "-" + dd
  var time = today.getHours() + ":" + (today.getMinutes()+30) + ":" + today.getSeconds();

  availabilityList = json;
  var allTimeslots = [];
  availabilityList.forEach((availability) => {
    var alreadyExists = false;
    var inThePast = false;
    if(date == availability.date) {
      if(time > availability.time) {
        inThePast = true;
      }
    }
    allTimeslots.forEach((timeslot) => {
      if(availability.time == timeslot.time) {
        alreadyExists = true;
      }
    });
    if(!alreadyExists && !inThePast) {
      allTimeslots.push(availability);
    }
  });
  let html = `<div class="box">`;
  html = `<div class="times">`
  html += `<div class="currentDate">`
  html += `<h1>Appointment Times<p>${selectedDate}`
  html += `</p></h1></div></div>`;
  html += `<div class="availabilities">`;
  allTimeslots.forEach((timeslot) => {
    var obj = {
      "user_id":timeslot.user_Id,
      "availability_id":timeslot.id,
      "timeslot_id":timeslot.timeslot_Id,
      "time":timeslot.time,
      "date":timeslot.date
    }
    html += `<div id=${obj.user_id}_${obj.availability_id}_${obj.timeslot_id}_${obj.date}_${obj.time} class="card col-md-2" onClick="makeAppointment(event)">${timeslot.timeslot_Text}`;
    html += `</div><br>`;
  });
  html += `</div>`;
  document.getElementById("box").innerHTML = html;
}

function makeAppointment(e) {
  const selectedAppointmentApiUrl = appointmentUrl;
  var element = e.target || e.srcElement;
  console.log(element.id);

  const myArray = element.id.split("_");
  let admin_id = myArray[0];
  let availability_id = myArray[1];
  let timeslot_id = myArray[2];
  let date = myArray[3];
  let time = myArray[4];

  const sendAppointment = {
    User_Id: userId,
    Date: date,
    Time: time,
    Availability_Id: availability_id,
    Timeslot_Id: timeslot_id
  }
  fetch(selectedAppointmentApiUrl, {
      method: "POST",
      headers: {
          "Accept": 'application/json',
          "Content-Type": 'application/json',
      },
      body: JSON.stringify(sendAppointment)
  }).then((response)=>{
      alert("Appointment booked for: " + date + " at: " + time);
      console.log(sendAppointment);
      console.log(date);
      GetDateAvailability(date);
      window.location.href = "Get_CustomerAppointments.html";
  });
}