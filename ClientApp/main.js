
// for at få en token skal man lave en POST request til https://localhost:5001/api/login/token Basic Auth Username: Admin, Password: pass
// Token er valid i 10 minutter
// Eksempel token
var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiY29uc3VtZXIiLCJleHAiOjE1NDA2NDkyMzEsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjUwMDEvIiwiYXVkIjoibXlzaXRlLmNvbSJ9.ijfJOo63fj-_8DoxSTSMSv-kc_usf-QiDlEewpGqMzg"

$.ajax({
    url: 'https://localhost:5001/api/values',
    beforeSend: function (xhr) {   //Include the bearer token in header
        xhr.setRequestHeader("Authorization", 'Bearer '+ token);
    },
    method: 'GET',
    success: function(data){
        // Fix - use bootstrap
        
       document.body.innerHTML = data.map(m => Object.values(m)).map(mm => mm.map(mmm => '<p style="margin:0; font-size: 18px;">' + mmm + '</p>').join("")).join("")
     }
  });
