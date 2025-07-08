# Simple E Commerce Application

This readme file covers tech stack, requirements, setup instructions and brief explanation of the project. Hope it helps. 

## Tech Stack

- **Frontend:** Angular 20 + Angular Material Design
- **Backend:** C#(.Net Core)
- **Database:** MySQL 

## Requirements
- **Node.js** (v20 or above recommended)
- **Angular CLI** (v20 or above)
- **.NET SDK** (v8.0 or above)
- **MySQL** (v8.0)

## Setup Instructions
1. **Clone the repository**  
   ```bash
   git clone https://github.com/jatin-devhub/E-Commerce.git
   cd E-Commerce
  2. **Configure the database**
 - Create a database name ecom and update the connection string in /backend/EcomBackend/appsettings.json.
  3. **Backend Setup**
   ```bash
  cd backend
  cd EcomBackend
dotnet restore
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
```

This should start a server on http://localhost:5241. If it's different you need to update baseUrl in /ecom-frontend/src/app/core/services/backend.service.ts

  4.  **Frontend Setup**
```Bash
cd ../ecom-frontend
npm install
ng serve
```
This should start server on http://localhost:4200 where you can test this application. 