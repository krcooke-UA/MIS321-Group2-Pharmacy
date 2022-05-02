const date = new Date();
const timeslotUrl = "https://localhost:5001/api/Timeslot";
var userId = localStorage.getItem("TidePharmacy-User").replace(/^"(.+(?="$))"$/, '$1');
var timeslotList = [];
var timeslot = {};
var AvaDate = "";
var endTime = "";
var startTime = "";
var myDate;

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
      days += `<div class="today">${i}</div>`;
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
  console.log("Selected date: " + element.id);
  myDate = element.id;
  GetDateTimeslots(element.id);
  document.getElementById("box").innerHTML = "";
}
function GetDateTimeslots(selectedDate) {
  const selectedTimeslotApiUrl = timeslotUrl + "/" + selectedDate;
  console.log(selectedTimeslotApiUrl);
  var requestOptions = {
    method: 'GET',
    redirect: 'follow'
  };
  fetch(selectedTimeslotApiUrl, requestOptions).then(function(response) {
    return response.json();
  }).then(function(json) {
    timeslotList = json;
    showTimes(timeslotList, selectedDate);
  }).catch(function(error) {
      console.log(error);
  });
}
function showTimes(timeslotList, AvaDate) {
  let html = `<div class="box">`;
  html = `<div class="times">`
  html += `<div class="currentDate">`
  html += `<h1>Manage Availability<p>${AvaDate}`
  html += `</p></h1></div></div>`;
  html += `<div class="availabilities">`;
  html += `<select id="iFunction" name="nFunction" onchange="changeddl(this)">`;
  html += `<option id="start" value="-1" selected=""></option>`;
  timeslotList.forEach((timeslot) => {
    var obj = {
      Id: timeslot.id,
      Datetime: timeslot.datetime,
      Date: timeslot.date,
      Time: timeslot.time,
      Text: timeslot.text
    }
    html += `<option id=${obj.Id} value=${obj.Time}>${obj.Time}</option>`;
  });
  html += `</select>`;
  document.getElementById("box").innerHTML = html;
}
function changeddl(obj) {
  startTime = obj.options[obj.selectedIndex].id;
  // console.log(startTime);
  let html = `<div class="box">`;
  html = `<div class="times">`
  html += `<div class="currentDate">`
  html += `<h1>Manage Availability<p>`
  html += `</p></h1></div></div>`;
  html += `<div class="availabilities">`;
  html += `<select id="iFunction" name="nFunction" onchange="changeddl(this)">`;
  html += `<option id="end" value="-1" selected="">${obj.options[obj.selectedIndex].value}</option>`;
  timeslotList.forEach((timeslot) => {
    var obj = {
      Id: timeslot.id,
      Datetime: timeslot.datetime,
      Date: timeslot.date,
      Time: timeslot.time,
      Text: timeslot.text
    }
    html += `<option id=${obj.Id} value=${obj.Time}>${obj.Time}</option>`;
  });
  html += `</select>`;
  var endTimes = [];
  var maxTime = obj.options[obj.selectedIndex].id;
  timeslotList.forEach((timeslot) => {
    if(timeslot.id > maxTime) {
      endTimes.push(timeslot);
    }
  });
  html += `<select id="iOperation" name="nOperation" onchange="showOptions(this)">`;
  html += `<option value="-1" selected=""></option>`;
  endTimes.forEach((timeslot) => {
      obj = {
        Id: timeslot.id,
        Datetime: timeslot.datetime,
        Date: timeslot.date,
        Time: timeslot.time,
        Text: timeslot.text
      }
    endTimes.push(timeslot);
    html += `<option id=${obj.Id} value=${obj.Text}>${obj.Time}</option>`;
  });
  html += `</select>`;
  html += `<br><br>`;
  html += `<button id ="Submit" class ="btn btn-primary" onclick = "handleSubmit()">SUBMIT</button></div>`;
  document.getElementById("box").innerHTML = html;
}
function showOptions(obj) {
  endTime = obj.options[obj.selectedIndex].id;
  // console.log(startTime + " - " + endTime);
}
function handleSubmit() {
  console.log(myDate + ": " + startTime + " - " + endTime);

  // const sendAvailability = {
  //   User_Id: userId,
  //   Date: date,
  //   Time: time,
  //   Availability_Id: availability_id,
  //   Timeslot_Id: timeslot_id
  // }
  // fetch(selectedAppointmentApiUrl, {
  //   method: "POST",
  //   headers: {
  //       "Accept": 'application/json',
  //       "Content-Type": 'application/json',
  //   },
  //   body: JSON.stringify(sendAvailability)
  // }).then((response)=>{
  //     alert(date + " availability scheduled for: " + startTime + " - " + endTime);
  //     console.log(sendAvailability);
  //     console.log(date);
  //     GetDateAvailability(date);
  // });
}