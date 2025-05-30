# BankBlazor - Web Banking Application

## Overview
BankBlazor is a modern web banking application built using Blazor WebAssembly. It provides a user-friendly interface for customers to manage their bank accounts, view transactions, and perform various banking operations.

## Features
- **Customer Profile**: View detailed customer information and account overview
- **Account Management**: Access multiple account details and balances
- **Transaction History**: View and sort transaction history with pagination
- **Banking Operations**: 
  - Make deposits
  - Process withdrawals
  - Transfer money between accounts

## Technical Implementation
- **Frontend**: Blazor WebAssembly
- **Backend**: ASP.NET Core Web API
- **Architecture**: Headless architecture with JSON communication
- **Models**:
  - Customer
  - Account
  - Transaction
  - Disposition

## Prerequisites
- .NET 8.0 SDK
- Visual Studio 2022
- WebAssembly workload

## Installation
1. Clone the repository:
```bash
git clone [repository-url]
```

2. Ensure WebAssembly workload is installed:
```bash
dotnet workload install wasm-tools
```

3. Navigate to the project directory:
```bash
cd BankBlazor2
```

4. Restore dependencies:
```bash
dotnet restore
```

## Running the Application
1. Build the solution:
```bash
dotnet build
```

2. Run the application:
```bash
dotnet run
```

3. Open your browser and navigate to:
```
https://localhost:7222
```

## Project Structure
- **BankBlazor.Client**: Contains the Blazor WebAssembly frontend
  - Components/
  - Models/
  - Pages/
  - Services/
- **BankBlazor.Server**: Contains the ASP.NET Core backend
  - Controllers/
  - Data/
  - Services/

## API Endpoints
All API endpoints are accessed through the `/api/Bank` route:
- GET `/api/Bank/customers/{id}` - Get customer details
- GET `/api/Bank/accounts/{id}` - Get account details
- GET `/api/Bank/transactions` - Get transaction history
- POST `/api/Bank/deposit` - Make a deposit
- POST `/api/Bank/withdraw` - Make a withdrawal
- POST `/api/Bank/transfer` - Transfer between accounts

## Development Workflow
- Feature branches are used for development
- Main branch contains stable releases
- Pull requests are required for code review
- Continuous integration ensures code quality

## Troubleshooting
If you encounter the browser-wasm runtime pack error, run the following command as administrator:
```powershell
dotnet workload install microsoft-net-sdk-blazorwebassembly-aot
```

## Contributors
[Jonathan Tjäder]
