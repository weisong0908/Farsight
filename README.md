[![Staging](https://github.com/weisong0908/Farsight/actions/workflows/staging.yml/badge.svg)](https://github.com/weisong0908/Farsight/actions/workflows/staging.yml)

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
## Local/Development
Navigate to each of the directories to start the projects:

| Name | Directory | Command to use | URL |
| --- | --- | --- | --- |
| web-app | frontend/web-app | npm run serve | http://localhost:8080 |  
| Farsight.Backend | backend/Farsight.Backend | dotnet run  | https://localhost:5001 |
| Farsight.IdentityService | backend/Farsight.IdentityService | dotnet run  | https://localhost:5101 |
| Farsight.CommonService | backend/Farsight.CommonService | dotnet run  | https://localhost:5201 |

## Staging
The projects are hosted behind an NGINX proxy server on a DigitalOcean droplet. A Github Action workflow is set up to automatically build, package, and deploy when new commit is pushed to **staging** branch.

| Name | URL | Remarks |
| --- | --- | --- |
| web-app | https://farsight-stg.tengweisong.com | The default entry point |
| Farsight.Backend | https://farsight-stg-backend.tengweisong.com | [Documentation](https://farsight-stg-backend.tengweisong.com/swagger) |
| Farsight.IdentityService | https://farsight-stg-identity-service.tengweisong.com | [Discovery](https://farsight-stg-identity-service.tengweisong.com/.well-known/openid-configuration) |
| Farsight.CommonService | https://farsight-stg-common-service.tengweisong.com | [Documentation](https://farsight-stg-common-service.tengweisong.com/swagger) |
