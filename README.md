
# Orchot Chinuch Project

A secure and efficient file upload platform built for educational institutions, enabling seamless integration with Azure cloud storage.

---

## 🧭 Overview

**Orchot Chinuch** is a full-stack web application designed for a national educational network. It enables administrators and authorized users to upload and manage files directly to Azure cloud storage. The platform is built with a modular and scalable architecture, combining Angular on the frontend and ASP.NET Web API on the backend.

---

## ✨ Key Features

### 📤 File Upload System
- Upload multiple files to Azure Blob Storage securely
- Real-time upload progress tracking
- Organized and centralized file management

### 🖥️ Angular Client
- Built with Angular CLI 15+
- Responsive and user-friendly UI using PrimeNG
- Upload interface with validations and feedback

### 🧩 Web API Backend
- Developed in ASP.NET Web API
- Layered architecture: Presentation, Business Logic, and Data Access layers
- Secure integration with Azure SDK for file operations

### 🏗️ Layered Architecture
- Clean separation of concerns
- High maintainability and modularity
- Scalable and testable components

### ☁️ Azure Integration
- Secure connection to Azure Blob Storage
- Utilizes SAS tokens or service credentials
- Optimized for performance and scalability

---

## 🛠️ Tech Stack

- Frontend: Angular 15, TypeScript, PrimeNG, HTML5, CSS
- Backend: ASP.NET Web API (.NET Core)
- Cloud: Microsoft Azure (Blob Storage)
- Dev Tools: Visual Studio Code, Swagger

---

## 🚀 Getting Started

### Prerequisites

- Angular CLI (npm install -g @angular/cli)
- .NET SDK (v6.0+)
- Azure Storage account

### Clone the Repository
 ```
git clone https://github.com/YOUR_USERNAME/OrchotChinuchProject.git  
cd OrchotChinuchProject
 ```
---

### Run the Angular Client
 ```
cd client  
npm install  
ng serve
 ```
Visit: http://localhost:4200

---

### Run the Web API Server
 ```
cd server  
dotnet build  
dotnet run
 ```
---

## ✅ Development Notes

- Code generation:
``` 
  ng generate component component-name
  ```

- Build project:
```
  ng build
  ```

- Run unit tests:
 ```
  ng test
   ```

- Run e2e tests:
 ```
  ng e2e
   ```

## 🔒 Security

- Authentication via Azure AD (planned)  
- Upload operations protected by access tokens  
- CORS configured for controlled access

---

## 🧠 Credits

Built with ❤️ for the Orchot Chinuch organization.

---

## 📘 License

This project is proprietary and licensed for internal organizational use only.

