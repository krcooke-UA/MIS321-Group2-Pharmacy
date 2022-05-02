const adminUrl = "https://localhost:5001/api/User";
var userId = localStorage.getItem("TidePharmacy-User").replace(/^"(.+(?="$))"$/, '$1');
var myAdmin = {};

function handleOnLoad() {
    getUserInfo();
}
function getUserInfo() {
    const putAdminApiUrl = adminUrl + "/" + userId
    fetch(putAdminApiUrl).then(function(response) {
        return response.json();
    }).then(function(json) {
        myAdmin = json[0];
		populateForm(myAdmin);
    }).catch(function(error) {
        console.log(error);
    });
}
function populateForm(json) {
    document.getElementById("FirstName").value = myAdmin.fname;
    document.getElementById("LastName").value = myAdmin.lname;
    document.getElementById("Email").value = myAdmin.email;
}


function handleEditClick() {
    makeEditable();
    hideButtons();
    var buttonHtml = "<button class=\"btn btn-primary btn-lg\" onclick=\"handleEditSave("+userId+")\">Save</button>"
    buttonHtml += "<button class=\"btn btn-warning btn-lg btn-cancle\" onclick=\"handleCancelSave()\">Cancel</button>"
    document.getElementById("saveButton").innerHTML = buttonHtml;
    document.getElementById("saveButton").style.display="inline-block";
}
function handleEditSave(id) {
    putUser(id);
    makeReadOnly();
    showButtons();
}
function handleCancelSave() {
    populateForm();
    makeReadOnly();
    showButtons();
}


function makeReadOnly() {
    document.getElementById("FirstName").readOnly = true;
    document.getElementById("LastName").readOnly = true;
}
function makeEditable() {
    document.getElementById("FirstName").readOnly = false;
    document.getElementById("LastName").readOnly = false;
}

function hideButtons() {
    document.getElementById("editButton").style.display = "none";
    document.getElementById("backButton").style.display = "none";
}
function showButtons() {
    document.getElementById("editButton").style.display = "inline-block";
    document.getElementById("backButton").style.display = "inline-block";
    document.getElementById("saveButton").style.display = "none";
}