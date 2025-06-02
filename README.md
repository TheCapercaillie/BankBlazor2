# BankBlazor

## Overview  
**BankBlazor** is a web-based banking application built using **Blazor WebAssembly**. It offers an interface for customers to manage their accounts, perform transactions, and view account activity.

## Features
Customer Profile: View customer information
Account Management: See account balances and IDs
Transaction History: Browse, filter, and sort past transactions
Banking Operations: Deposit money, Withdraw funds, Transfer between accounts  

## Used in development  
- Blazor WebAssembly  
- ASP.NET Core Web API  
- .NET 8.0 SDK  
- Visual Studio 2022  
- SQL Server Management Studio  
- Swagger

## Process  
1. Cloned the repository  
2. Set up the client/server structure  
3. Built backend logic for API endpoints and DB access  
4. Created pages/components in the frontend  
5. Connected everything with HttpClient and routing
6. Fixed bugs and made UI improvements

## Project Structure  
**Directory structure:**

**BankBlazor.Client/**
_Imports.razor
App.razor
BankBlazor.Client.csproj
Program.cs
Layout/
   MainLayout.razor
   MainLayout.razor.css
   NavMenu.razor
   NavMenu.razor.css
Pages/
   AllCustomers.razor
      AllCustomers.razor.css
   CustomerProfile.razor
      CustomerProfile.razor.css
   Home.razor
   TransactionHistory.razor
   TransactionHistory.razor.css
Properties/
   launchSettings.json
wwwroot/
   index.html
      css/
         app.css
         bootstrap/
      images/
      
**BankBlazor.Server/**
appsettings.Development.json
appsettings.json
BankBlazor.Server.csproj
BankBlazor.Server.http
Program.cs
WeatherForecast.cs
APIController/
   BankController.cs
Controllers/
   WeatherForecastController.cs
Data/
   BankBlazorContext.cs
Migrations/
   20250601164432_InitialCreate.cs
   20250601164432_InitialCreate.Designer.cs
   BankContextModelSnapshot.cs
Models/
   Account.cs
   Customer.cs
   Transaction.cs
Properties/
   launchSettings.json
   
**BankBlazor.Shared/**
BankBlazor.Shared.csproj
Models/
   DTOs/
      AccountDto.cs
      CustomerDto.cs
      TransactionDto.cs

## API Endpoints  
All API calls go through /api/Bank.  
- GET /api/Bank/customers/{id}
- GET /api/Bank/customers
- GET /api/Bank/accounts/{id}/transactions
- POST /api/Bank/accounts/{id}/deposit
- POST /api/Bank/accounts/{id}/withdraw
- POST /api/Bank/transfer

Tested mostly through Swagger.

## Development Workflow  
Worked locally using branches for features.  
Used a development branch for ongoing work, and merged into the master branch when stable.
Although I had some trouble using that workflow and ended up working on the wrong branch all the time so in the latter half I only worked in the master branch for self conveniens and speed.

## Trouble while developing  
I ran into a tough nut to crack **browser-wasm runtime pack error** during development.  
In the end it was an error caused by one class interfering with API routing because of name interfering.  
This caused delays since I couldn’t properly run or test the app for a while.
Had hostly lots of trouble developing this app running into error after error slowing down the development.
In the end I can proudly say there are no bugs or errors on my end.

## Developer  
Jonathan Tjäder
