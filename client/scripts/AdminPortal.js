const adminUrl = "https://localhost:5001/api/User";
var userId = localStorage.getItem("TidePharmacy-User").replace(/^"(.+(?="$))"$/, '$1');
var myAdmin = {};

function handleOnLoad() {
    getUserInfo();
}
function getUserInfo() {
    const getAdminApiUrl = adminUrl + "/" + userId
    fetch(getAdminApiUrl).then(function(response) {
        return response.json();
    }).then(function(json) {
        console.log(json);
        myAdmin = json[0];
        console.log(myAdmin);
		populateForm(myAdmin);
    }).catch(function(error) {
        console.log(error);
    });
}

function putUser() {
    const putAdminApiUrl = adminUrl + "/" + userId
    const sendAdmin = {
        id: parseInt(userId),
        fname: document.getElementById("FirstName").value,
        lname: document.getElementById("LastName").value,
        email: myAdmin.email
    }
    fetch(putAdminApiUrl, {
        method: "PUT",
        headers: {
            "Accept": 'application/json',
            "Content-Type": 'application/json',
        },
        body: JSON.stringify(sendAdmin)
    })
    .then((response)=>{
        myAdmin = sendAdmin;
        populateForm(myAdmin);
    });
}

function populateForm(myAdmin) {
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