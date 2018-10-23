// $.ajax({
//     url: 'https://localhost:5001/api/values',
//     type: 'GET',
//     beforeSend: function (xhr) {
//         xhr.setRequestHeader('Authorization', 'Bearer eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQWRtaW4iLCJleHAiOjE1NDAzMTQ2MDYsImlzcyI6Im15c2l0ZS5jb20iLCJhdWQiOiJteXNpdGUuY29tIn0.RkmgmobdAv7pR4PQxX0D4Efsm4wU9WO68nLTx0Di2zU');
//     },
//     success: function (data) { console.log(data) },
//     error: function (data) { console.log(data) },
// });


// Token er valid i 10 minutter
var token = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQWRtaW4iLCJleHAiOjE1NDAzMTgxNDUsImlzcyI6Im15c2l0ZS5jb20iLCJhdWQiOiJteXNpdGUuY29tIn0.SQzl--NeCymM1Yna_S156WPoXxbjqmQtYacx52MuG4w"

$.ajax({
    url: 'https://localhost:5001/api/values',
    headers: {
        'Authorization': `Bearer ${token}`
    },
    beforeSend: function (xhr) {   //Include the bearer token in header
        xhr.setRequestHeader("Authorization", 'Bearer '+ token);
    },
    method: 'GET',
    success: function(data){
      console.log(data);
    }

  });
