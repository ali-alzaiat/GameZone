# GameZone

## Overview
This project is a simple games library built using the .NET MVC framework. It displays a list of games on the home page, and clicking on a game redirects to a detailed description page. The application includes all CRUD operations (Create, Read, Update, Delete) for managing the games, as well as validation for forms on both the client and server sides. Additionally, the project includes a feature to preview the selected photo before submitting the form.

## Deployment
[demo link](http://gameapp.runasp.net/)

## Features
* Display a list of games on the home page.

* Redirect to a game description page upon clicking a game.

* Implement all CRUD operations for managing games:

  * Create: Add a new game to the library.
  
  * Read: View game details.
  
  * Update: Edit game information.
  
  * Delete: Remove a game from the library.
  
* Client-side and server-side validation for forms.

* Photo preview feature before form submission.

## Prerequisites
.NET SDK

Visual Studio

MySQL

## Installation
1. Clone the repository:
```
git clone https://github.com/yourusername/games-library.git
```
2. Navigate to the project directory:

```
cd GameZone
```
3. Open the solution file in Visual Studio:
```
GameZone.sln
```
4. Restore the NuGet packages:
```
dotnet restore
```
5. Update the connection string in appsettings.json:
```
"ConnectionStrings": {
  "DefaultConnection": "Type the connection string for your database here."
}
```
6. Apply migrations to create the database:
```
dotnet ef database update
```
7. Run the application:
```
dotnet run
```
## Usage
### Home Page
The home page displays a list of games.
Click on any game to view its detailed description.
### CRUD Operations
1. Create:

* Navigate to the "Add New Game" page.
* Fill out the form with game details.
* Preview the selected photo before submission.
* Submit the form to add the game to the library.

2. Read:

* Click on a game from the home page to view its details.

3. Update:

* Navigate to the games page.
* Click on "Edit" to update the game information.
* Submit the form to save changes.

4. Delete:

* Navigate to the games page.
* Click on "Delete" to remove the game from the library.
## Validation

* The form includes both client-side and server-side validation to ensure data integrity.
* Client-side validation provides immediate feedback to users.
* Server-side validation ensures data integrity on the server.

## Screenshots
Home Page

Game Details Page

Add New Game Page
