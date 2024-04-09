# E-commerce Platform (Backend)

Welcome to our e-commerce platform repository! This project is dedicated to building a robust and scalable backend system for an online store using .NET Core and SQL Server.

## Features:

- **User Management**: The backend includes a comprehensive user management system where users can register, log in, and update their profiles. Passwords are securely encrypted before storage in the database.

- **Product Management**: Administrators have the ability to add, update, and delete products. Each product is associated with attributes such as name, description, price, and stock quantity.

- **Order Management**: The platform supports order processing, allowing users to add products to their cart, proceed to checkout, and place orders. Order details are stored in the database, including products, quantities, and total prices.

- **JWT Authentication**: JSON Web Token (JWT) authentication is implemented to secure user authentication. Upon successful login, users receive a JWT token that is used to authenticate subsequent requests.

- **Authorization with Policy Roles**: Role-based access control (RBAC) is implemented using policy-based authorization. Users are assigned roles (e.g., admin, customer) upon registration, and access to various functionalities is restricted based on these roles.

- **Database Integration**: The backend is integrated with a SQL Server database for storing user information, product details, and order data. Entity Framework Core is used for ORM (Object-Relational Mapping) to interact with the database.

## Technologies Used:

- Backend Framework: .NET Core 7 (Clean architecture, DDD, MediatR, CQRS)
- Database: SQL Server
- ORM: Entity Framework Core

## Getting Started:

To get started with the e-commerce platform backend, follow these steps:

1. Clone the repository to your local machine.
2. Set up a SQL Server database and configure the connection string in the project's appsettings.json file.
3. Run the database migrations to create the necessary tables in the database.
4. Build and run the .NET Core application locally.
