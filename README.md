# Web shop

This project is a demo Web shop application. The goal was to implement a system that manages users, carts, and products using the DummyJSON API. The project includes a backend (built with .NET) and an optional frontend (built with Angular and hosted on Netlify).

## 💡 Features

### 👤 Users
- User registration
- User login
- Fetch current authenticated user data

### 🛍️ Products
- Fetch all products from DummyJSON API
- Fetch single product details
- Add/remove products to/from favorites

### 🛒 Cart
- Add products to user’s cart
- Remove products from cart
- Fetch the current user’s cart

### 🌟 Bonus Features
- Clean Architecture applied in backend structure
- Product pagination and sorting
- Caching layer to minimize API calls and improve performance

---

## 🏗️ Backend Setup Instructions (.NET)

### Prerequisites
- .NET SDK
- Node.js and npm
- Angular CLI (npm install -g @angular/cli)
- Visual Studio 2022+ with ASP.NET and web development workload
- SQL Server
  
### Clone the Repository

git clone https://github.com/MartinaHuljaj/ShoppingCart.git

### Run the solution
- Open solution with Visual Studio
- In Package Manager Console select AbySalto.Mid.Infrastructure as the deafult project and run command Update-Database
- Run the project with http
## 🔗 Live Demo

While running the backend, you are able to access the frontend (Angular) deployed on Netlify: https://shop-mhuljaj.netlify.app/

