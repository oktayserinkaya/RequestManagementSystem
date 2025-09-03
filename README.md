# Request Management System (Talep Yönetim Sistemi)

### Contributors / Developers
- [Oktay SERİNKAYA](https://github.com/oktayserinkaya)
- [Hüseyin KAYAN](https://github.com/hsynkyn)

<br>

### Introduction
<p align="justify">
  The "Request Management System" application was developed to electronically manage requests from the supply warehouse by departments within a healthcare institution . Users log in to the application with their assigned username and password and perform transactions with the authorizations appropriate to their role within the application.
</p>
<p align="justify">
  For example, while ordinary users can only request materials on behalf of their assigned department when they log in to the system, a user with request evaluation authority can review requests submitted by departments and, taking into account the inventory status of the materials, approve the request to be fulfilled from the warehouse, decide whether a portion of the request can be fulfilled from the warehouse and the remainder through purchasing, or reject the request entirely. A user with a purchasing role processes purchase orders received from the request evaluation unit.
</p>
<p align="justify">
  Since the user in the Admin role has all the authorizations, he/she can monitor the transactions of all units or perform unit operations.
</p>
<p align="justify">
  Below are a few videos from the app, showing a user from each authorization group performing the actions. These will help you get a preview for the app.
</p><br>

https://github.com/user-attachments/assets/18cf5f04-2abb-4780-bcaf-2bbecd5c2f0e

https://github.com/user-attachments/assets/3fae4466-1d7e-4847-8f98-62547a35409f

https://github.com/user-attachments/assets/410aa0a2-8744-48f7-9241-ee6c14bb04c0

https://github.com/user-attachments/assets/2c8ebf28-456b-4efa-9011-b363d76e7ea0

https://github.com/user-attachments/assets/3c70a90f-c737-45aa-a780-7028feb12b2e

<br>

### Technical Details About the Project
- <p align="justify">The project was developed in n-tier architecture structure using ASP.Net Core MVC.</p>
- <p align="justify">PostgreSQL was used as the database in the project. However, MS SQL can also be used with simple code modifications.</p>
- <p align="justify">During developing the project, two databases were used, one of which is “RMSDb” (tables such as categories of materials in the warehouse, materials, stock status, requests made, request evaluations, etc.) and the other is “RMSIdentityDb” (tables related to user transactions).</p>
- <p align="justify">Asynchronous programming was used in this project where “Fluent Validation, Autofac, AutoMapper” technologies were used.</p>
- <p align="justify">Efforts have been made to develop in accordance with SOLID principles.</p>

<br>

### User Informations for the Project
|           | Admin Panel | Request Creation Role | Request Evaluation Role | Purchasing Role | Payment Role |
|-----------|-------------|-----------------------|-------------------------|-----------------|--------------|
| User Name | admin       | ahmetyilmaz           | elifkara                | mehmetdemir     | fatmaoz      |
| Password  | 123         | 123                   | 123                     | 123             | 123          |

<br>

### Project Structure
`/RequestManagementSystem`<br>
  - `BUSINESS`
  - `CORE`
  - `DATAACCESS`
  - `DTO`
  - `WEB`

<br>

### Migrations
`/DATAACCESS`<br>
  -`Add-Migration InitializeAppDb -Context AppDbContex -OutputDir "Migrations/AppDbMigrations"`
