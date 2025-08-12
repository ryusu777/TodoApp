# TodoApp - Project Management System

A comprehensive project management application with integrated Git repository management, built using modern microservices architecture and clean domain-driven design patterns.

## 🚀 Features

- **Project Management**: Create and manage projects with hierarchical structures and phases
- **Assignment Tracking**: Task assignment with status tracking, deadlines, and review system
- **Gitea Integration**: Seamless integration with Gitea repositories for issue tracking and version control
- **Kanban Boards**: Visual project management with drag-and-drop functionality
- **User Authentication**: Secure authentication and authorization system
- **Real-time Updates**: Event-driven architecture for real-time collaboration
- **File Attachments**: Upload and manage project-related documents
- **Dashboard**: Comprehensive project overview and analytics

## 🏗️ Architecture

TodoApp follows a **microservices architecture** with **Domain-Driven Design (DDD)** principles:

### Services

- **Frontend** (`src/FrontEnd/`) - Nuxt 3 application with Vue.js
- **ProjectManagement** (`src/ProjectManagement/`) - Core project and assignment management
- **IntegrationContext** (`src/IntegrationContext/`) - External integrations (Gitea, etc.)
- **AuthContext** (`src/AuthContext/`) - Authentication and user management
- **ApiGateway** (`src/ApiGateway/`) - API routing and load balancing

### Key Patterns

- **CQRS** (Command Query Responsibility Segregation)
- **Event Sourcing** with MassTransit messaging
- **Clean Architecture** with separated concerns
- **Aggregate Pattern** for domain modeling
- **Outbox Pattern** for reliable messaging

## 🛠️ Technology Stack

### Frontend
- **Nuxt 3** - Vue.js framework with SSR/SPA capabilities
- **TypeScript** - Type-safe development
- **Tailwind CSS** - Utility-first CSS framework
- **Pinia** - State management
- **Nuxt UI** - Component library
- **Vitest** - Unit testing framework

### Backend
- **.NET 8** - Cross-platform framework
- **Entity Framework Core** - ORM for data access
- **MassTransit** - Distributed application framework
- **PostgreSQL** - Primary database
- **RabbitMQ** - Message broker

### DevOps
- **Docker** - Containerization
- **Docker Compose** - Local development orchestration

## 📋 Prerequisites

Before you begin, ensure you have the following installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js 18+](https://nodejs.org/)
- [Docker](https://www.docker.com/get-started)
- [Docker Compose](https://docs.docker.com/compose/)
- [PostgreSQL](https://www.postgresql.org/) (or use Docker)

## 🚀 Quick Start

### 1. Clone the Repository

```bash
git clone https://github.com/ryusu777/TodoApp.git
cd TodoApp
```

### 2. Environment Setup

The frontend environment files are already configured:

- `.development.env` - Development environment (API_URL=http://localhost:8081/api)
- `.production.env` - Production environment (API_URL=http://host.docker.internal:8081/api)

You can modify these files as needed for your environment.

### 3. Backend Services Setup

Set up environment variables for backend services:

```bash
# Required environment variables
export ConnectionStrings__PostgreContext="Host=localhost;Database=TodoApp;Username=postgres;Password=yourpassword"
export ConnectionStrings__MassTransitDbContext="Host=localhost;Database=TodoAppMT;Username=postgres;Password=yourpassword"
export JwtOptions__SecretKey="your-super-secret-jwt-key-here"
export ClientUrl="http://localhost:8080"
```

### 4. Database Setup

Start PostgreSQL (using Docker):

```bash
docker run --name todoapp-postgres -e POSTGRES_PASSWORD=yourpassword -p 5432:5432 -d postgres:15
```

Run database migrations:

```bash
cd src/ProjectManagement
dotnet ef database update

cd ../IntegrationContext  
dotnet ef database update

cd ../AuthContext
dotnet ef database update
```

### 5. Run with Docker Compose

The easiest way to run the entire application:

```bash
cd src
docker compose up --build
```

This will start all services:
- Frontend: http://localhost:8080
- API Gateway: http://localhost:8081
- RabbitMQ Management: http://localhost:15672 (guest/guest)

**Note**: The Docker services require environment variables to be set. You'll need to configure the database connections and JWT secret as described in the backend setup section.

### 6. Manual Development Setup

For development with hot reload:

#### Start Backend Services

```bash
# Terminal 1 - ProjectManagement Service
cd src/ProjectManagement/ProjectManagement.Presentation
dotnet run

# Terminal 2 - IntegrationContext Service  
cd src/IntegrationContext/IntegrationContext.Presentation
dotnet run

# Terminal 3 - AuthContext Service
cd src/AuthContext/AuthContext.Presentation  
dotnet run

# Terminal 4 - API Gateway
cd src/ApiGateway
dotnet run

# Terminal 5 - RabbitMQ
docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:management
```

#### Start Frontend

```bash
# Terminal 6 - Frontend
cd src/FrontEnd
npm install
npm run dev
```

## 🧪 Testing

### Frontend Tests

```bash
cd src/FrontEnd
npm run test
```

**Note**: Frontend tests require the backend API to be running.

### Backend Tests

```bash
cd src/ProjectManagement
dotnet test
```

**Note**: Backend tests require a PostgreSQL database connection. The tests use integration testing patterns and need the full application context.

## 📚 API Documentation

The API Gateway routes requests to appropriate microservices:

- **Projects API**: `/api/projects/` - Project management endpoints
- **Assignments API**: `/api/assignments/` - Task management endpoints  
- **Auth API**: `/api/auth/` - Authentication endpoints
- **Integration API**: `/api/integration/` - Gitea integration endpoints

### Example API Calls

```bash
# Login
curl -X POST http://localhost:8081/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"username": "user", "password": "password"}'

# Get Projects
curl -X GET http://localhost:8081/api/projects \
  -H "Authorization: Bearer YOUR_JWT_TOKEN"

# Create Assignment
curl -X POST http://localhost:8081/api/assignments \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -d '{"title": "New Task", "description": "Task description", "projectId": "project-id"}'
```

## 🔧 Development Guidelines

### Code Style

- **C#**: Follow Microsoft's C# coding conventions
- **TypeScript/Vue**: Use ESLint and Prettier configurations
- **Naming**: Use descriptive names for variables, methods, and classes

### Domain-Driven Design

- Keep domain logic in domain services and aggregates
- Use value objects for complex types
- Implement repository pattern for data access
- Follow aggregate boundaries for consistency

### Event-Driven Architecture

- Publish domain events for cross-context communication
- Use event handlers for side effects
- Implement idempotent event processing

## 🐳 Deployment

### Docker Production Build

```bash
# Build all services
docker-compose -f docker-compose.prod.yml build

# Deploy
docker-compose -f docker-compose.prod.yml up -d
```

### Environment Variables for Production

```env
# Database
ConnectionStrings__PostgreContext=your-production-db-connection
ConnectionStrings__MassTransitDbContext=your-production-mt-db-connection

# Security  
JwtOptions__SecretKey=your-production-secret-key

# URLs
ClientUrl=https://your-frontend-domain.com
API_URL=https://your-api-domain.com
```

## 🤝 Contributing

We welcome contributions! Please follow these steps:

1. **Fork the repository**
2. **Create a feature branch**: `git checkout -b feature/amazing-feature`
3. **Follow the coding standards** and write tests
4. **Commit your changes**: `git commit -m 'Add amazing feature'`
5. **Push to the branch**: `git push origin feature/amazing-feature`
6. **Open a Pull Request**

### Development Setup for Contributors

1. Read the [Development Guidelines](#-development-guidelines)
2. Set up the development environment as described in [Quick Start](#-quick-start)
3. Run tests to ensure everything works
4. Make your changes following the established patterns
5. Add tests for new functionality
6. Update documentation if needed

## 📖 Project Structure

```
TodoApp/
├── src/
│   ├── FrontEnd/                 # Nuxt 3 frontend application
│   │   ├── pages/               # Vue.js pages and routing
│   │   ├── components/          # Reusable Vue components
│   │   ├── domain/             # Domain-specific UI logic
│   │   └── middleware/         # Nuxt middleware
│   ├── ProjectManagement/       # Project management microservice
│   │   ├── Domain/             # Domain models and business logic
│   │   ├── Application/        # Application services and commands
│   │   ├── Infrastructure/     # Data access and external services
│   │   └── Presentation/       # Web API controllers and endpoints
│   ├── IntegrationContext/      # External integrations microservice
│   ├── AuthContext/            # Authentication microservice
│   ├── ApiGateway/             # API gateway service
│   ├── Library/                # Shared libraries and utilities
│   └── MassTransitContracts/   # Shared message contracts
├── Diagrams/                   # Architecture and design diagrams
└── docker-compose.yml         # Docker orchestration
```

## 🐛 Troubleshooting

### Common Issues

1. **Port conflicts**: Ensure ports 8080, 8081, 5432, and 5672 are available
2. **Database connection errors**: Verify PostgreSQL is running and connection strings are correct
3. **JWT errors**: Ensure the JWT secret key is properly configured
4. **RabbitMQ connection issues**: Verify RabbitMQ is running and accessible
5. **Frontend build issues**: Try updating TypeScript dependencies: `npm update typescript vue-tsc`
6. **Test failures**: Tests require running services - use Docker Compose for a complete environment

### Logs

Check service logs using Docker:

```bash
# View all service logs
docker compose logs

# View specific service logs
docker compose logs front-end
docker compose logs project-management
```

## 📄 License

This project is open source and available under the [MIT License](LICENSE).

## 🙏 Acknowledgments

- Built with love using modern development practices
- Inspired by clean architecture and domain-driven design principles
- Thanks to the open-source community for the amazing tools and frameworks

---

For more detailed information, please refer to the individual service documentation in their respective directories.