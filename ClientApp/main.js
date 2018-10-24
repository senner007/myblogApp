
// for at få en token skal man lave en POST request til https://localhost:5001/api/login/token Basic Auth Username: Admin, Password: pass
// Token er valid i 10 minutter
// Eksempel token
var token = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQWRtaW4iLCJleHAiOjE1NDA0MDg1MjYsImlzcyI6Im15c2l0ZS5jb20iLCJhdWQiOiJteXNpdGUuY29tIn0.wVdmqmpy0vvAsyfJ57fTWaJj0hVZU5rAi5C7zW-g0v8"

$.ajax({
    url: 'https://localhost:5001/api/values',
    beforeSend: function (xhr) {   //Include the bearer token in header
        xhr.setRequestHeader("Authorization", 'Bearer '+ token);
    },
    method: 'GET',
    success: function(data){
       document.body.innerHTML = data.map(m => Object.values(m)).map(mm => mm.map(mmm => '<p style="margin:0; font-size: 18px;">' + mmm + '</p>').join("")).join("")
     }
  });
