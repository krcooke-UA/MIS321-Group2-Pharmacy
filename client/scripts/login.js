const baseUrl = "https://localhost:5001/api/Auth/Login";
var myUser = {};

function handleLogin() {
    console.log("Made it to post");
    const postUserApiUrl = baseUrl;
    const sendUser = {
        Email: document.getElementById("email").value,
        Password: document.getElementById("password").value
    }
    console.log(sendUser);
    console.log(JSON.stringify(sendUser))
    fetch(postUserApiUrl, {
        method: "POST",
        headers: {
            "Accept": 'application/json',
            "Content-Type": 'application/json',
        },
        body: JSON.stringify(sendUser)
    })
    .then((response)=>{
        myUser = sendUser;
        return response.json();
    }).then((json) => {
        console.log(json);
        localStorage.setItem("TidePharmacy-User", JSON.stringify(json.id));
        localStorage.setItem("TidePharmacy-Token", JSON.stringify(json.authToken));
        localStorage.setItem("TidePharmacy-Type", JSON.stringify(json.type))
        window.location.href = "home.html";
    }).catch(error => {
        console.log(error);
    });
}

// gets string
// localStorage.getItem("TidePharmacy-User")

// routing
// window.location.href = "index.html";