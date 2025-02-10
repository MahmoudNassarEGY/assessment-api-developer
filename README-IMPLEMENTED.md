# Assessment API Developer

## Overview
This project is a **Customer Management API** built using **ASP.NET Web API**. It provides CRUD operations for managing customer data, with an initial in-memory data store but designed to support a real database in the future.

---
## Features
- RESTful API with **CRUD operations** for customer management.
- **In-memory data storage** with an easy migration path to a database.
- **Swagger UI** for easy API testing.
- **Unit tests** for service layer validation using Moq.

---
## Technologies Used
- **ASP.NET Web API**
- **Entity Framework (for potential DB support)**
- **Moq & MSTest** (for unit testing)
- **Swashbuckle** (for API documentation)

---
## Setup Instructions

### **1. Clone the Repository**
```powershell
 git clone https://github.com/MahmoudNassarEGY/assessment-api-developer.git
 cd assessment-api-developer
```

### **2. Restore NuGet Packages**
```powershell
dotnet restore
```

### **3. Run the API**
```powershell
dotnet run
```

### **4. Access Swagger UI**
Swagger is enabled for testing API endpoints.
````

---
## API Endpoints

### **Customer Endpoints**
| Method | Endpoint                  | Description                  |
|--------|---------------------------|------------------------------|
| **POST**   | `/api/customers`        | Create a new customer        |
| **GET**    | `/api/customers`        | Retrieve all customers       |
| **GET**    | `/api/customers/{id}`   | Retrieve a customer by ID    |
| **PUT**    | `/api/customers/{id}`   | Update an existing customer  |
| **DELETE** | `/api/customers/{id}`   | Delete a customer            |

---
## Running Unit Tests

Unit tests ensure the integrity of the business logic.

### **1. Navigate to the Test Project**
```powershell
cd assessment-api-developer.Tests
```

### **2. Run the Tests**
```powershell
dotnet test
```

---
## Design Decisions

### **In-Memory Data Store with Future DB Support**
- The project **currently uses an in-memory list** for storing customer data.
- It is **designed to allow switching to a database** (SQL Server, PostgreSQL, etc.) by simply replacing the repository implementation.
- This approach keeps the API functional without requiring a database setup but ensures an easy migration path when needed.


If there were more time for the assessment or in a real-world example, I would definitely work on adding regex-based validators to all customer fields.


