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
![CQRS implementation](https://github.com/thushanmanujith/blob/CQRS_Implementation.png?raw=true)

### Entity structure
![CQRS implementation](https://github.com/thushanmanujith/blob/EntityStructure.png?raw=true)
