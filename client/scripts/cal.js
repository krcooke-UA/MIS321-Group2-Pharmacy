const date = new Date();

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
      days += `<div class="today" id=${selectedDate} name=${selectedDate} onClick="selectDate(event)">${i}</div>`;
    } else {
      days += `<div id=${selectedDate} name=${selectedDate} onClick="selectDate(event)">${i}</div>`;
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
  showTimes(element.id);
}
function showTimes(selectedDate) {
  let html = `<div class="box">`
  html += `<div class="times">`
  html += `<div class="currentDate">`
  html += `<h1>Appointment Times<p>${selectedDate}`
  html += `</p></h1></div></div></div>`;
  document.getElementById("box").innerHTML = html;
}