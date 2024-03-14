
# Full Stack Event Invitation Web App Using .NET Core Web API & Angular 

This project is used to send invites for an event to another user.

A general user can access the portal, but to access its functionality a used must be logged in.

There are two types of users seeded in the database, one is a user and second is admin, but as per now there's no particular access for admin, we can manage this further as per future requirements.

A user can create it's event & send invites to other users (those who are present in databse) on the basis of their names.

Events & Users must be listed in the database, 

# Tech Stack used
-- Dotnet Core Web API for backend
-- Angular 17 for frontend
-- Bootstrap for designing
-- SSMS ORM as a relational database

# Steps to access this project

1. Clone the git repository
2. Setting up your sql server connection string   (if you clone this project, there's no need to setup connection string as it is already designed for global use)
3. Run the migrations in backend and start the web API
4. Open the frontend project and run command `ng serve`





