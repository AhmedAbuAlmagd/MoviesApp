# MoviesApp 

A robust .NET Core backend API for the Movie Application, providing secure endpoints for movie management, user authentication, and review handling.

## 🛠️ Technology Stack

- .NET Core 7
- Entity Framework Core
- SQL Server
- JWT Authentication
- AutoMapper
- Swagger/OpenAPI

## 📁 Project Structure

```
MoviesApp/
├── MoviesApp.Application/      # Application Layer
│   ├── Controllers/           # API endpoints
│   │   ├── AuthController
│   │   ├── MovieController
│   │   ├── ReviewController
│   │   └── WatchlistController
│   ├── Program.cs
│   └── appsettings.json
│
├── MoviesApp.Core/            # Core Layer
│   ├── Interfaces/           # Service interfaces
│   ├── Services/             # Service implementations
│   ├── DTOs/                 # Data Transfer Objects
│   │   ├── AuthDTOs/
│   │   ├── MovieDTOs/
│   │   └── ReviewDTOs/
│   └── ├── Models/              # Database entities
    │   ├── User.cs
    │   ├── Movie.cs
    │   ├── Review.cs
    │   ├── Watchlist.cs
    │   └── Category.cs             # Models
│
└── MoviesApp.Data/           # Data Layer
    ├── Data/                 # Database context
    │   ├── MoviesContext.cs
    └── Migrations/          # Database migrations
    │   Repositories/         

```

## 🔌 API Endpoints

### Authentication
- `POST /api/auth/login` - User login
- `POST /api/auth/register` - User registration

### Movies
- `GET /api/movie/getall` - Get all movies (paginated)
- `GET /api/movie/getById/{id}` - Get movie details
- `POST /api/movie/add` - Add movie (Admin)
- `PUT /api/movie/edit` - Edit movie (Admin)
- `DELETE /api/movie/delete/{id}` - Delete movie (Admin)

### Reviews
- `GET /api/review/getall` - Get movie reviews
- `POST /api/review/add` - Add review
- `PUT /api/review/edit` - Edit review
- `DELETE /api/review/delete` - Delete review

### Watchlist
- `GET /api/Watchlist` - Get user's watchlist
- `POST /api/Watchlist/{movieId}` - Add to watchlist
- `DELETE /api/Watchlist/{movieId}` - Remove from watchlist

## 🚀 Getting Started

### Prerequisites
- .NET Core 7 SDK
- SQL Server
- Visual Studio 2022 or VS Code

### Installation

1. **Clone the Repository**
```bash
git clone [repository-url]
cd MoviesApp
```

2. **Restore Dependencies**
```bash
dotnet restore
```

3. **Update Database Connection**
- Open `appsettings.json`
- Update the connection string with your SQL Server details:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=MoviesDB;Trusted_Connection=True;"
  }
}
```

4. **Run Database Migrations**
```bash
dotnet ef database update
```

5. **Run the Application**
```bash
dotnet run
```

The API will be available at `https://localhost:7001` (or your configured port)

### Default Admin Account
The application comes with a pre-seeded admin account:
- **Email:** Admin@gmail.com
- **Password:** Admin@123

## 🔒 Security Features

- JWT-based authentication
- Role-based authorization (Admin/User)
- Password hashing
- Secure HTTP headers
- CORS configuration

## 📝 Database Schema

### Users
- Id (PK)
- Email
- Password (hashed)
- Role
- CreatedAt

### Movies
- Id (PK)
- Title
- Description
- ReleaseDate
- Poster
- Trailer
- Rating
- Categories (Many-to-Many)

### Reviews
- Id (PK)
- MovieId (FK)
- UserId (FK)
- Rating
- Comment
- CreatedAt

### Watchlist
- Id (PK)
- UserId (FK)
- MovieId (FK)
- AddedAt

## 🧪 Testing

The API can be tested using Swagger UI, available at:
```
https://localhost:7001/swagger
```

## 📚 Dependencies

- Microsoft.AspNetCore.Authentication.JwtBearer
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools
- AutoMapper
- Swashbuckle.AspNetCore
