# Weekend Project: Sales Order Management API with Blazor UI

## Project Overview

This project involves creating a robust C# REST API application using .NET Core, designed to manage the capture, update, deletion, and viewing of order information. The solution is structured according to Onion Architecture, ensuring a clear separation of concerns and promoting maintainability, scalability, and testability.

- **API Layer**: The REST API will utilize Dapper for efficient and lightweight CRUD operations, interacting with the database through a repository pattern.
  
- **Database Management**: A dedicated database project will handle migrations and schema management using Entity Framework Core, following a code-first approach to ensure the database structure evolves alongside the application.

- **Blazor UI**: The user interface will be built as a Blazor WebAssembly application, providing a responsive and dynamic platform for users to manage sales orders. The UI will integrate seamlessly with the API, allowing for data manipulation and retrieval.

- **Architectural Focus**: The project emphasizes adherence to SOLID principles and leverages Dependency Injection to maintain a loosely coupled architecture. The design is microservices-ready, allowing for easy separation of components into individual services if needed in the future.

This approach ensures that the system is not only functional but also adaptable to future requirements, with a clear emphasis on clean code and best practices.

## Features

1. **REST API for Order Management**
   - Built with .NET Core, following Onion Architecture to separate concerns into Core, Application, Infrastructure, and API layers.
   - Supports creating, updating, deleting, and retrieving orders using Dapper for efficient data access.
   - Receives a shipment request and converts it into the expected DBO model before storing it in the database.
   - Implements dependency injection for all services, repositories, and database contexts, promoting a loosely coupled architecture.
   - Adheres to SOLID principles, ensuring maintainable, scalable, and testable code.

2. **Blazor UI for Sales Order Management**
   - A Blazor WebAssembly application that serves as the client-side interface for managing sales orders.
   - Provides forms for capturing or updating sales orders, integrating seamlessly with the API endpoints.
   - Displays all order headers in a responsive grid, with options to view, edit, or delete associated order lines.
   - Supports inline or popup editing for order lines, enabling full CRUD functionality directly from the UI.
   - Implements authentication and role management:
     - **Admin Users**: Full CRUD access to order management features.
     - **Guest Users**: Read-only access, restricted to viewing order information.
   - Follows SOLID principles in UI component design, promoting reusability and clear separation of responsibilities.

3. **Database Management with Entity Framework Core**
   - A dedicated database project handles database schema management using EF Core with a code-first approach.
   - Migrations are managed to ensure smooth updates to the database structure while maintaining data integrity.
   - Supports creating, updating, and managing stored procedures and other database objects through EF Core migrations.

4. **Microservices-Ready Architecture**
   - Designed with a microservices approach, allowing easy separation of components into individual services if needed.
   - Each layer interacts through well-defined interfaces, ensuring that components remain independent and interchangeable.

5. **Dependency Injection and Extensibility**
   - Fully utilizes dependency injection to manage service lifetimes and dependencies, enhancing testability and flexibility.
   - Easily extendable due to the use of abstractions and loosely coupled design patterns.

6. **Adherence to Best Practices and SOLID Principles**
   - Implements SOLID principles across all layers, ensuring each class and method has a single responsibility, is easily extendable, and relies on abstractions rather than concrete implementations.
   - Designed for maintainability, scalability, and adaptability to changes in business requirements.

## Development Tasks

### 1. Project Setup

- Set up a new ASP.NET Core Web API project.
- Set up a Blazor WebAssembly project for the UI.
- Set up a new Database project to handle database management with Entity Framework Core.
- Follow the principles of Onion Architecture by structuring the solution into separate layers:
  - **Core Layer**: Contains business logic, domain models, and interfaces.
  - **Application Layer**: Contains service implementations, DTOs, and application-specific logic.
  - **Infrastructure Layer**: Handles data access with Dapper, manages EF Core migrations, and interacts with external services or databases.
  - **API Layer**: Contains controllers and manages dependency injection, serving as the entry point for the API.

#### Project Structure Overview

- **Core Layer**
  - `Entities`: Define the core business objects (e.g., `Order`, `OrderLine`).
  - `Interfaces`: Define contracts for services and repositories.

- **Application Layer**
  - `Services`: Implement business logic using the interfaces defined in the Core layer.
  - `DTOs`: Define data transfer objects used for communication between the API and the services.

- **Infrastructure Layer**
  - `Repositories`: Implement data access logic using Dapper.
  - `Data`: Set up Entity Framework Core DbContext for handling migrations.

- **API Layer**
  - `Controllers`: Handle HTTP requests and responses using services from the Application layer.

### 2. Dependency Injection and Microservices Alignment

- **Dependency Injection (DI)**:
  - Use DI throughout the application to inject services, repositories, and other dependencies, promoting loose coupling and adherence to SOLID principles.
  - Register all services, repositories, and DbContext within the `Startup` or `Program.cs` file using `IServiceCollection`.
  
- **Microservices Architecture Alignment**:
  - Design the application components with a microservices mindset, even if they are part of a single solution initially.
  - Ensure each layer is independent and interacts through well-defined interfaces, enabling easier separation into microservices if needed in the future.

### 3. SOLID Principles

- **Single Responsibility Principle (SRP)**:
  - Ensure each class has one reason to change by keeping responsibilities focused. For example, services handle business logic, while repositories focus on data access.
  
- **Open/Closed Principle (OCP)**:
  - Classes should be open for extension but closed for modification. Use interfaces and abstractions to allow extending functionality without altering existing code.

- **Liskov Substitution Principle (LSP)**:
  - Ensure that derived classes can be substituted for their base classes without altering the correct functioning of the program.

- **Interface Segregation Principle (ISP)**:
  - Define specific interfaces for distinct functionalities rather than using a large general-purpose interface. For example, separate interfaces for read and write operations.

- **Dependency Inversion Principle (DIP)**:
  - High-level modules should not depend on low-level modules; both should depend on abstractions. Use dependency injection to manage dependencies effectively.

### 4. Database Setup with Dapper and Entity Framework Core

- **Dapper for CRUD Operations**:
  - Use Dapper to perform CRUD operations with SQL queries directly within the repository classes.
  
- **Entity Framework Core for Migrations**:
  - Set up a separate database project using Entity Framework Core to handle schema management and migrations.
  - Use a code-first approach to define your database models and ensure database consistency through migrations.
  - Use EF Core CLI commands (`Add-Migration`, `Update-Database`) to manage changes.
  - Create a `DbContext` in the database project to define table structures and handle migration scripts.

### 5. API Endpoints

- **Order Endpoints:**
  - `POST /orders`: Create a new order.
  - `GET /orders`: Retrieve all orders.
  - `GET /orders/{id}`: Retrieve a specific order.
  - `PUT /orders/{id}`: Update an existing order.
  - `DELETE /orders/{id}`: Delete an order.

### 6. Data Mapping and Repository Pattern

- Implement logic to map the Shipment Order Request to the expected DBO model before storing it using Dapper.
- Use Dapper to handle CRUD operations by executing SQL queries directly from the repository classes.

### 7. Blazor UI Implementation

- **Order Management Page:**
  - Create a page to capture or update sales orders with data binding to API endpoints.
  - Use forms for order input fields: order reference, currency, order date, category code, and ship date.
  - Display order lines with SKU, product description, and quantity, with options to add, edit, or delete lines.

- **Order Header and Order Line Grids:**
  - Create grids to list order headers and corresponding order lines.
  - Implement search functionality by Order Header, Order Type, and date range.
  - Add editing capabilities inline, in popups, or navigate to a detailed order page.

### 8. User Authentication and Security

- Implement a login screen in Blazor with basic authentication.
- Define roles for Admin and Guest users with appropriate access controls.
  - **Admin User:** Full access to CRUD functions.
  - **Guest User:** Read-only access.

## Expected Models

### DBO Model

The Database Object (DBO) model represents how the order information is stored within the database after processing by the API. This model is used by the Dapper repositories for CRUD operations.

```json
{
  "order": {
    "orderRef": "TestSO123",
    "orderDate": "2023-01-23T00:00:00",
    "currency": "EUR",
    "shipDate": "2023-01-23T00:00:00",
    "categoryCode": "B2C",
    "lines": [
      {
        "sku": "TestSKU1",
        "qty": 1,
        "desc": "Test SKU 1"
      },
      {
        "sku": "TestSKU2",
        "qty": 4,
        "desc": "Test SKU 2"
      }
    ]
  }
}
```

- Fields Explained:
  - `orderRef`: Unique identifier for the order.
  - `orderDate`: Date the order was placed.
  - `currency`: Currency type used for the order.
  - `shipDate`: Date the order is expected to ship.
  - `categoryCode`: Type of order, e.g., B2C (Business to Consumer) or B2B (Business to Business).
  - `lines`: Array of order line items, each containing:
    - `sku`: Stock Keeping Unit code.
    - `qty`: Quantity of the item ordered.
    - `desc`: Description of the item.

### Shipment Order Request

The Shipment Order Request represents the incoming data format from the client, typically via the Blazor UI, before it is processed and converted to the DBO model. This model ensures that the API properly maps client input to the internal representation.

```json
{
  "SalesOrderRequest": {
    "SalesOrder": {
      "salesOrderRef": "TestSO123",
      "orderDate": "2023-01-23T00:00:00",
      "currency": "EUR",
      "shipDate": "2023-01-23T00:00:00",
      "categoryCode": "B2B",
      "orderLines": [
        {
          "skuCode": "TestSKU1",
          "quantity": 1,
          "description": "Test SKU 1"
        },
        {
          "skuCode": "TestSKU2",
          "quantity": 4,
          "description": "Test SKU 2"
        }
      ]
    }
  }
}
```

- Fields Explained:
  - `salesOrderRef`: Unique reference number for the sales order.
  - `orderDate`: The date when the order was placed.
  - `currency`: Currency code representing the type of currency used in the order.
  - `shipDate`: The date when the order is scheduled for shipping.
  - `categoryCode`: Specifies the category of the order (e.g., B2B or B2C).
  - `orderLines`: Array of line items with:
    - `skuCode`: Unique identifier for the stock-keeping unit.
    - `quantity`: The number of items ordered.
    - `description`: A brief description of the product.

### Mapping Considerations

- The API will transform the SalesOrderRequest into the DBO Model for internal processing and storage.
- Validation and mapping logic should be implemented in the service layer to ensure that all incoming requests conform to expected formats and business rules.
- By separating the request and storage models, we maintain a clean boundary between client-facing data structures and internal database representations.

## Additional Notes

- **Adhere to RESTful API Design Principles**:
  - Ensure that API endpoints are well-defined, use appropriate HTTP methods (GET, POST, PUT, DELETE), and return standardized response codes and messages.
  - Implement input validation and error handling to maintain data integrity and provide clear feedback to clients.

- **Data Access and Efficiency**:
  - Use Dapper for lightweight and efficient data access, leveraging direct SQL queries for optimal performance.
  - Manage database schema changes and migrations using Entity Framework Core to maintain control over database structure and ensure consistency across environments.

- **Onion Architecture and SOLID Principles**:
  - Ensure each layer of the application adheres to the principles of Onion Architecture, maintaining separation of concerns and a clean flow of data and dependencies.
  - Apply SOLID principles to design maintainable, extensible, and testable code, focusing on single responsibility and dependency inversion throughout the system.

- **Dependency Injection and Extensibility**:
  - Utilize Dependency Injection to promote loose coupling and enhance testability. Ensure all dependencies are injected through constructors and registered in the application’s service container.

- **Microservices Readiness**:
  - Structure the application components to facilitate future transitions into a microservices architecture, with clear boundaries between services and well-defined interfaces.

- **Unit and Integration Testing**:
  - Implement unit tests for critical components in the Core and Application layers to ensure business logic is correct and maintainable.
  - Include integration tests for API endpoints and database interactions to validate the complete functionality of data flows.

- **Blazor UI Best Practices**:
  - Design Blazor components to be reusable, maintainable, and responsive, ensuring a seamless user experience across different devices.
  - Implement state management effectively to handle data consistency and reduce unnecessary re-rendering of components.

- **Security Considerations**:
  - Implement proper authentication and authorization mechanisms to protect sensitive data and ensure that users can only access resources they are permitted to.
  - Use secure coding practices to prevent common vulnerabilities such as SQL injection, XSS, and CSRF attacks.

- **Responsive Design and Usability**:
  - Ensure the Blazor UI is designed with a responsive layout that adapts well to various screen sizes and resolutions.
  - Focus on creating an intuitive and user-friendly interface that enhances the overall user experience.

- **Documentation and Code Maintainability**:
  - Maintain thorough documentation of code, APIs, and business logic to facilitate onboarding and maintenance.
  - Use meaningful naming conventions, clear comments, and consistent formatting throughout the codebase.

- **Scalability and Performance Optimization**:
  - Design with scalability in mind, ensuring that the architecture can handle increasing loads as the application grows.
  - Optimize queries and use appropriate indexing strategies in the database to improve performance.
