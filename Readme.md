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

## Solution
### Frontend
The frontend was designed to keep the UI very simple and it fulfills all the functionalities. It currently has following pages:-

- **Category Navigation Page** - It shows all the categories and on clicking on particular category you can go to products list in it's categories
- **Product List Page** - It's a page where you can see all the products in the category, you can add to cart and also view product details.
- **Product Details Page** - It's a page where all the details of single product is shown with related products.
- **Cart Page** - It's a page where you can see all the products and edit quantities if needed

### Backend
Their are routes created to perform cart actions, categories and products view. These routes can be viewed at /swagger/index.html of backend endpoint