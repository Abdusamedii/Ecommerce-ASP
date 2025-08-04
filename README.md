
# E-commerce REST API

A full-featured e-commerce REST API built with .NET 9.0 and Entity Framework Core. This project showcases modern backend development practices with a complete e-commerce solution including user management, product catalog, shopping cart, order processing, and payment handling.

## What's Included

### Authentication & User Management
- JWT token-based authentication
- Role-based authorization (Customer/Admin roles)
- User registration and login
- Secure password handling with custom token provider

### E-commerce Core Features
- Complete shopping cart system with item management
- Product catalog with categories and subcategories
- Order management with payment processing
- Real-time inventory tracking
- Multiple delivery address support
- Payment integration (Cash/Card options)

### Technical Architecture
- Clean architecture with proper separation of concerns
- Service layer pattern for business logic
- DTO pattern for API contracts
- Dependency injection throughout
- Entity Framework Core with SQL Server
- Database transactions for data integrity

### Security & Performance
- Background job processing with Hangfire
- Input validation with custom attributes
- Soft delete functionality for data preservation
- Database indexing for performance
- Async/await patterns throughout

### Development & Deployment
- Docker containerization
- Environment configuration with dotenv
- Database migrations for version control
- Comprehensive Swagger API documentation

## Tech Stack

- **.NET 9.0** - Latest framework
- **Entity Framework Core** - ORM for database operations
- **SQL Server** - Database
- **JWT Bearer Authentication** - Token-based auth
- **Hangfire** - Background job processing
- **Swagger/OpenAPI** - API documentation
- **Mapster** - Object mapping
- **Docker** - Containerization

## Getting Started

### Prerequisites
- .NET 9.0 SDK
- SQL Server (Local or Azure)
- Docker (optional)

### Setup

1. **Clone the repository**
```bash
git clone <repository-url>
cd Ecomm
```

2. **Environment configuration**
Create a `.env` file in the root directory:
```env
JWT_SECRET_KEY=your-super-secret-jwt-key-here
```

3. **Database setup**
Update `appsettings.json` with your connection string:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=EcommDB;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}
```

4. **Run migrations**
```bash
dotnet ef database update
```

5. **Start the application**
```bash
dotnet run
```

The API will be available at:
- API: https://localhost:5263
- Swagger UI: https://localhost:5263/swagger

## Project Structure

```
Ecomm/
├── Controllers/          # API Controllers
├── Services/            # Business Logic Layer
├── Models/              # Entity Models & DTOs
│   ├── DTO/            # Data Transfer Objects
│   ├── enums/          # Enumerations
│   └── Validation/     # Custom Validation Attributes
├── Data/               # Database Context
├── Authentication/      # JWT Token Provider
├── Exceptions/         # Custom Exception Handling
├── Migrations/         # Entity Framework Migrations
└── Properties/         # Application Properties
```

## Key Components

### Models
- **User** - User management with roles
- **Product** - Product catalog with images
- **Cart/CartItem** - Shopping cart functionality
- **Order/OrderItem** - Order processing
- **Payment** - Payment processing
- **Category/SubCategory** - Product categorization
- **Address** - Delivery address management

### Services
- **AuthService** - Authentication logic
- **UserService** - User management
- **ProductService** - Product operations
- **CartService** - Cart management
- **OrderService** - Order processing with transactions
- **CategoryService** - Category management
- **AddressService** - Address operations

## Security Features

- JWT Authentication with secure token validation
- Role-based Authorization for different user types
- Input Validation with custom attributes
- Database Transactions for data integrity
- Soft Delete for data preservation
- Environment Variable configuration for secrets

## Performance Optimizations

- Entity Framework Core with optimized queries
- Background Job Processing with Hangfire
- Database Indexing for better performance
- Async/Await patterns throughout
- Connection Pooling for database efficiency

## Database Schema

The application uses Entity Framework Core with SQL Server, featuring:
- User Management with roles and authentication
- Product Catalog with categories and images
- Shopping Cart with item management
- Order Processing with payment integration
- Address Management for delivery

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## License

This project is licensed under the MIT License.

## Author

**Abdusamed Beqiri** - Full Stack Developer

---

 **Star this repository if you find it helpful!** 



Future additions:
 Comprehensive unit testing with xUnit
 API rate limiting for security
 Structured logging with Serilog
 Redis caching for performance
 File upload for product images
 Email notifications for orders
 Search & filtering capabilities
 Health checks for monitoring

Tech stack: .NET 9.0, EF Core, JWT, Hangfire, Docker, Redis, xUnit

Ready for production deployment!
