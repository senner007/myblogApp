
- mkdir MyblogApp
- dotnet new webapi
- dotnet add package Microsoft.EntityFrameworkCore.Design --version 2.2.0-preview3-35497
- dotnet add package Pomelo.EntityFrameworkCore.MySql --version 2.1.2
- dotnet restore
- dotnet ef dbcontext scaffold "server=localhost;database=myblog;user=root;pwd=123456;" "Pomelo.EntityFrameworkCore.MySql" -o \Models -f
- fix naming to conventional ( classes to uppercase )
- using MyblogApp.Models in Controller