![Alt text](./estipro-infrastructure.drawio.svg)

# EstiPro Backend API
[![main](https://github.com/EstimatePro/estipro-api/actions/workflows/main.yml/badge.svg)](https://github.com/ViacheslavMelnichenko/estipro-api/actions/workflows/main.yml)

![.NET Core](https://img.shields.io/badge/.NET_Core-9.0-blue)
![License](https://img.shields.io/badge/License-CC%20BY%204.0-lightgrey)

## Overview
The **EstiPro API** is the backend component of the EstiPro project, an ASP.NET Core-based API for Planning EstiPror, designed to streamline task estimation for IT teams.

Planning EstiPror is a collaborative approach to estimate project complexity and task effort, encouraging team discussion and consensus. This API serves as the core engine for the EstiPro application, providing endpoints to manage sessions, handle user estimates, and synchronize responses.

## Features

Here's the Features section based on your API endpoints:

markdown
Copy code
## Features
- **Room Management**:
   - **Create Room**: Allows authenticated users to create a new planning room for estimation sessions.
   - **Get Room**: Retrieve details of a specific room by ID.
   - **Update Room**: Update room details to accommodate session needs.
   - **Delete Room**: Remove a room and its associated data from the system.

- **Attendance Management**:
   - **Join Room**: Enables users to join an existing planning room for participation.
   - **Leave Room**: Allows users to exit a planning room when they are done participating.

- **Ticket Management**:
   - **Add Ticket**: Add a new ticket (or task) to the room for estimation. Tickets represent individual tasks to be estimated.

- **Voting Management**:
   - **Add Vote**: Allows users to submit their vote on a ticket, contributing to the team’s estimation process.

## Getting Started

### Prerequisites
- [.NET Core SDK 9.0](https://dotnet.microsoft.com/download/dotnet/9.0)
- A SQL database (PostgreSQL) for persistent storage

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/EstimatePro/estipro-api.git
   cd estipro-api/src/EstiPro.Api
2. Install dependencies:
   ```bash
   dotnet restore
3. Set up database and environment variables as required (see Configuration below).
4. Install dependencies:
   ```bash
   dotnet run
   
### Configuration
Configure database connection strings and any required environment variables in the appsettings.json or through environment variables.

### API Documentation
API documentation is available via Swagger. After starting the application, navigate to:
   ```bash
   https://localhost:5001/swagger

