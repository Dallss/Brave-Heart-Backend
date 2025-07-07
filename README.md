# BraveHeartBackend

BraveHeartBackend is a backend API built with ASP.NET Core, designed to power the BraveHeart application. It provides a robust, secure, and modular foundation for managing users, products, and related business logic.

## Overview

BraveHeartBackend serves as the core backend for a product and user management system. It is structured to support scalable, maintainable development and integrates with modern authentication, logging, and cloud storage solutions.

## Key Features

- **ASP.NET Core 9 Web API**: Modern, high-performance RESTful API framework.
- **Entity Framework Core with PostgreSQL**: Strongly-typed ORM for relational data, supporting migrations and seeding.
- **JWT Authentication & Role-based Authorization**: Secure endpoints with support for user roles and token-based authentication.
- **User Management**: Registration, login, Google Sign-In, and role assignment.
- **Product Management**: CRUD operations for products, product types, attributes, and attribute values.
- **Cloudinary Integration**: Secure image upload and signature generation for product images.
- **Serilog Logging**: Structured logging to both console and rolling log files.
- **Swagger/OpenAPI**: Built-in API documentation for easy exploration and testing.
- **Environment-based Configuration**: Supports different settings for development and production.

## Architecture

- **Modular Structure**: Separate folders for Controllers, Models, DTOs, Services, and Data access.
- **Database**: Uses PostgreSQL for persistent storage, with migrations for schema evolution.
- **Authentication**: Implements JWT for stateless authentication and supports role-based access control.
- **Cloud Storage**: Integrates with Cloudinary for image management, including secure upload signatures.
- **Logging**: Uses Serilog for detailed request and error logging, with daily log file rotation.
- **API Documentation**: Swagger UI is available for interactive API exploration.

## Entities

- **User**: Application users with support for roles and refresh tokens.
- **Product**: Items managed by the system, linked to product types and attributes.
- **ProductType**: Categories or types of products, each with its own set of attributes.
- **ProductAttribute**: Defines characteristics for product types (e.g., color, size).
- **ProductAttributeValue**: Stores values for attributes assigned to products.
- **CartItem**: Represents items in a user's shopping cart.
- **CheckoutDetails**: Stores checkout and shipping information for orders.

## Relationships

- **ProductType → Product**: One-to-many, with cascade delete (deleting a product type removes its products).
- **Product → ProductAttributeValue**: One-to-many, linking products to their attribute values.
- **User → CartItem**: One-to-many, each user can have multiple cart items.

## Logging & Monitoring

- All API activity and errors are logged using Serilog, with logs stored in the `Logs/` directory and output to the console.

## API Documentation

- The API is self-documented using Swagger/OpenAPI, making it easy to explore endpoints and data models.

---

BraveHeartBackend is designed for extensibility, security, and ease of integration with modern frontend applications and cloud services. 