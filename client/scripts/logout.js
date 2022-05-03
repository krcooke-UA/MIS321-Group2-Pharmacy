const logoutUrl = "https://localhost:5001/api/Auth/Logout";
var myToken = {};

function handleLogout() {
    window.localStorage.clear();
    window.location.href = "index.html";
}