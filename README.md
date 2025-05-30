# BankBlazor - Web Banking Application

## Overview
BankBlazor is a web banking application built using Blazor WebAssembly. It provides a slick interface for customers to manage their bank accounts, view transactions, and more.

## Features
- **Customer Profile**: View customer information and account overview
- **Account Management**: Access account details and balances
- **Transaction History**: View and sort transaction history
- **Banking Operations**: 
  - Make deposits
  - Process withdrawals
  - Transfer money between accounts

## Used in development
- Blazor WebAssembly
- ASP.NET Core Web API
- .NET 8.0 SDK
- Visual Studio 2022
- SQL Server Management Studio

## Process
1. Clone the repository

2. Set up the project structure (Client/Server architecture)

3. Backend Development

4. Frontend Development

5. Bug Fixes & Improvements

## Project Structure
- **BankBlazor.Client**: Contains the Blazor WebAssembly frontend
  - Layout/
  - Models/
  - Pages/
- **BankBlazor.Server**: Contains the ASP.NET Core backend
  - APIController/
  - Data/
  - Connected Services/
  - Models/

## API Endpoints
All API endpoints are accessed through the `/api/Bank` route:
- GET `/api/Bank/customers/{id}` - Get customer details
- GET `/api/Bank/accounts/{id}` - Get account details
- GET `/api/Bank/transactions` - Get transaction history
- POST `/api/Bank/deposit` - Make a deposit
- POST `/api/Bank/withdraw` - Make a withdrawal
- POST `/api/Bank/transfer` - Transfer between accounts

## Development Workflow
I used feature branches, a development branch and a main branch
  

## Trouble while developing
I encountered a browser-wasm runtime pack error, that I couldnt fix in time that caused big trouble making me unable to check if the code was right. It was not a code related error.


## Developer
**Jonathan Tjäder**
