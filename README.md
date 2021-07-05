# Introduction 
**Farsight** is an investment portfolio tracker that:
* manages holdings in each portfolio,
* displays portfolio performance over time, and
* shows component allocation for each portfolio. 

# Getting Started
This project consists of several frontend and backend components
| Name | Group | Type | Framework | Description |
| --- | --- | --- | --- | --- |
| web-app | Frontend | Single page application | Vue.js 2.x | The web application or UI |  
| Farsight.Backend | Backend | Web API  | .NET 5 | Manages the endpoints for project backend |
| Farsight.IdentityService | Backend | Web API  | .NET 5 | Provides the security tokens and account management |
| Farsight.CommonService | Backend | Web API  | .NET 5 | Provides common endpoints available for both Backend and IdentityService |

The project also requires PostgreSQL databases:
| Database Name | Description |
| --- | --- |
| farsight_identity_service | Database for Farsight.IdentityService |
| farsight_backend | Database for Farsight.Backend |

The API endpoints can be found in the [Postman collection](https://www.getpostman.com/collections/651e605bf9feb84a0af2).

# Build and Run
Navigate to each of the directories to start the app:

| Name | Directory | Command to use | URL |
| --- | --- | --- | --- | --- |
| web-app | web-app | npm run serve | http://localhost:8080 |  
| Farsight.Backend | backend/Farsight.Backend | dotnet run  | https://localhost:5001 |
| Farsight.IdentityService | backend/Farsight.IdentityService | dotnet run  | https://localhost:5101 |
| Farsight.CommonService | backend/Farsight.CommonService | dotnet run  | https://localhost:5201 |