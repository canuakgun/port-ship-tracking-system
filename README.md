# Port Ship Tracking System

A full-stack web application for tracking ships, ports, cargoes, crew members, and port visits. Built as a summer internship project at Bimar (Arkas Holding).

## Overview

This system allows port authorities to manage ship arrivals and departures, track cargo shipments, assign crew members to ships, and maintain a record of port visits — all through a RESTful API with a React-based admin interface.

## Tech stack

**Backend**
- ASP.NET Core Web API (.NET)
- Entity Framework Core
- SQL Server
- Swagger / OpenAPI
- xUnit + Moq (unit testing)

**Frontend**
- React (Vite)
- React Router
- Axios

## Architecture

The backend follows a layered architecture:

PortShipTrackingSystem.Api             → Controllers, Swagger, app configuration
PortShipTrackingSystem.Core            → Entities, DTOs, interfaces, custom exceptions
PortShipTrackingSystem.Infrastructure  → EF Core DbContext, repository implementations
PortShipTrackingSystem.Services        → Business logic and validation rules
PortShipTrackingSystem.Tests           → Unit tests (xUnit + Moq)

Dependency direction: Api → Services/Infrastructure → Core. Core has no dependencies on other layers, keeping business rules and data contracts independent of implementation details.

## Features

- Full CRUD for Ships, Ports, Ship Visits, Cargoes, Crew Members, and Ship-Crew Assignments
- Business rule validation, including:
  - Unique IMO numbers for ships
  - Arrival date must precede departure date for ship visits
  - Cargo weight must be greater than zero
  - No duplicate ship-crew-date assignment combinations
  - Unique crew member email addresses
- Enriched read responses (e.g. ship visits return the ship and port names, not just their IDs)
- Searchable dropdown selectors for choosing ships, ports, and crew members by ID — filters as you type, designed to stay usable with large datasets
- Swagger UI for interactive API exploration and testing

## Getting started

### Prerequisites
- .NET SDK 8+
- Node.js (LTS)
- SQL Server (Express or higher)

### Backend

cd PortShipTrackingSystem.Api
dotnet restore
dotnet ef database update --project ../PortShipTrackingSystem.Infrastructure
dotnet run

The API will be available at http://localhost:5054, with Swagger UI at http://localhost:5054/swagger.

Update the connection string in appsettings.json if your SQL Server instance name differs from localhost\SQLEXPRESS.

### Frontend

cd port-ship-tracking-frontend
npm install
npm run dev

The app will be available at http://localhost:5173.

Note: the backend must be running for the frontend to fetch data.

## Running tests

cd PortShipTrackingSystem.Tests
dotnet test

## Screenshots

(add screenshots here, e.g. the Ships management page)

## Author

Can Ulaş Akgün — github.com/canuakgun
