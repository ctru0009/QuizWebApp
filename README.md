# QuizApp

QuizApp is a full-stack application built to create, manage, and participate in quizzes. It uses React for the frontend, .NET as the backend API server, and PostgreSQL for data storage.

## Features

- **User Authentication**: Secure login and registration using JWT token.
- **Quiz Creation**: Admin users can create, edit, and delete quizzes.
- **Quiz Participation**: Users can take quizzes and view their scores.
- **Review Answers**: Users can review their answers for each quiz.

## Tech Stack

- **Frontend**: React
- **Backend**: .NET Core
- **Database**: PostgreSQL

## Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download)
- [Node.js and npm](https://nodejs.org/en/download/) (for React)
- [PostgreSQL](https://www.postgresql.org/download/)

## Getting Started

### Clone this git repo to your local machine
```bash
git clone https://github.com/ctru009/QuizApp.git
cd QuizApp
```

### Create the env
Add this to appsettings.json
```
DB_CONNECTION_STRING=Host=localhost;Port=5432;Database=quiz_db;Username=postgres;Password=password
```

### Create the initial database schema
Run this in the terminal.
```
dotnet ef database update
```

### Install dependencies in the frontend
Move to the front end folder
```
npm run dev
```
### Run 
Run the app in the Visual Studio.
