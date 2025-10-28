# E-Leraning Application

A modern e-learning management system built using ASP.NET Core with Entity Framework Core, designed to help students and instructors connect, learn, and manage courses efficiently.

## 🚀 Features

### 👩‍🎓 Student Module
- Register and log in securely
- Browse and enroll in available courses
- View enrolled courses on the dashboard
- Update profile information (name, email, contact, education, etc.)

### 👨‍🏫 Instructor Module
- Instructor registration and login
- Create and manage courses
- Upload lessons with video/content links
- Track number of learners per course
- View most-loved or most-enrolled courses

### ⚙️ Admin Module (optional if added)
- Manage all users and courses
- View system metrics and reports

## 🏗️ Tech Stack
- Frontend: Razor Pages, Bootstrap 5, HTML5, CSS3
- Backend: ASP.NET Core 8.0
- Database: Microsoft SQL Server
- ORM: Entity Framework Core 9.0
- IDE: Visual Studio 2022
- Version Control: Git & GitHub

## 📂 Project Structure
E-Leraning-Application/
├── Controllers/
├── Models/
├── Pages/
│   ├── Student/
│   ├── Instructor/
│   ├── Shared/
├── Data/
├── wwwroot/
│   ├── css/
│   ├── js/
│   ├── images/
├── appsettings.json
└── Program.cs

##  Setup Instructions
1. Clone the repository
   git clone https://github.com/sanskruti2602/E-Leraning-Application.git

2. Open in Visual Studio 2022

3. Update Database Connection String in appsettings.json

4. Run Migrations
   dotnet ef database update

5. Build & Run
   Press Ctrl + F5 to launch the project

##  UI Overview
- Clean orange-white theme for instructors
- Soft background (#fff4e6) for better readability
- Responsive layout using Bootstrap
- Student & instructor dashboards with course cards


<img width="1920" height="1080" alt="Screenshot 2025-10-28 195841" src="https://github.com/user-attachments/assets/7a6a5597-a0fc-48cb-b38e-6f40be7a228e" />















