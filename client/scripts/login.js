const baseUrl = "https://localhost:5001/api/Auth/Login";
var myUser = {};

function handleLogin() {
    console.log("Made it to post");
    const postUserApiUrl = baseUrl;
    console.log(postUserApiUrl);
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
    });
}