# MovieCollection

## Description
MovieCollection API is a C# based solution that implements a test solution for demonstrate some concepts.

## Installation
A .NET Core 6+ environment is required in order to work with this project. The solution supports Microsoft Visual Studio for Windows and Mac, as well as Visual Studio Code etc.
Database required a PostgreSQL database.

## Solution
This solution develop on Domain Driven Desgin and based on Hexagonal (Ports & Adapters) Architecture. Inside the core domin implementation based on CQRS pattern.

### Hexagonal Architecture:
The hexagonal architecture divides the system into loosely-coupled interchangeable components; such as application core, user interface, data repositories, test scripts and other system interfaces etc.

In this approach, the application core that contains the business domain information and it is surrounded by a layer that contains adapters handling bi-directional communication with other components in a generic way.

### Solution Architecture:
![Solution Architecture](https://github.com/thushanmanujith/MovieCollection/blob/main/blob/Architecture.jpg?raw=true)

### CQRS implementation base on Mediator pattern
![CQRS implementation](https://github.com/thushanmanujith/MovieCollection/blob/main/blob/CQRS_Implementation.png?raw=true)

### Entity structure
![CQRS implementation](https://github.com/thushanmanujith/MovieCollection/blob/main/blob/EntityStructure.png?raw=true)

### API Guide
API base on role based authentication and currently support two main roles:
* User
* Admin
  
Defult admin user account available with seed data.
- Username: test@mail.com
- passwoard: Test@123

Initial user registration and sign-in can proceed with below endpoints:
- POST /api/Auth/signup
- POST /api/Auth/login
- GET /api/Auth/user

Admin User can change user role:
- POST /api/Auth/access

User can create their own movie collection with endpoints:
- POST /api/Movie/collection/add
- POST /api/Movie/collection/add_movie/{movieId}
- DELETE /api/Movie/collection/remove_movie/{movieId}

User can their own collection and others by this endpoint:
- GET /api/Movie/collection/{userId}

User can search movies in a collection by:
- GET /api/Movie/collection/{collectionId}/search?searchText=""
  
Admin user can add movies to the system with endpoint:
- POST /api/Movie/add
