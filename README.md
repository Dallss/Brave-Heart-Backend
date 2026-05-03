# BraveHeartBackend

BraveHeartBackend is an ASP.NET Core Web API that handles authentication, product management, and related business workflows.

## Overview

The API is built around a flexible product system with dynamic attributes and role-based user access. It integrates external services for media handling and uses structured logging for observability.

## Key Features

- JWT authentication with role support (Admin, BusinessOwner, Customer)
- Google Sign-In integration
- Product system with dynamic attributes (types, attributes, values)
- Cloudinary integration for secure image uploads
- PostgreSQL with Entity Framework Core (migrations and seeding)
- Serilog logging (console + rolling files)
- Swagger for API exploration and testing

## Architecture

- Layered structure: Controllers, DTOs, Services, Data
- Stateless authentication using JWT and refresh tokens
- Externalized media handling via Cloudinary
- Environment-based configuration

## Core Models

- **User**: Authentication, roles, refresh tokens
- **Product**: Linked to product types and attribute values
- **ProductType**: Defines available attributes
- **ProductAttribute / ProductAttributeValue**: Dynamic product properties
- **CartItem**: User cart state
- **CheckoutDetails**: Shipping and order information

## Relationships

- ProductType → Products (cascade delete)
- Product → ProductAttributeValues
- User → CartItems

## Logging

API requests and errors are logged using Serilog with daily file rotation.
