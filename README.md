# Updog.in

Live: https://updog.in

Updog is a reddit-esque content aggregation website. Users can create accounts to share posts, and create comments. Posts can be upvoted and downvoted and users can earn karma on their posts.

This project was built as a learning experience to see what it would take to build a reddit clone. The front end is built using Vue.js and TypeScript. The back end is powered by ASP.NET Core, and a PostgreSQL database. Architecture wise the project adheres to clean architecture although I've never actually read the book so it probably isn't 100% up to par ¯\\_(ツ)_/¯

![ScreenShot](https://raw.githubusercontent.com/EddieAbbondanzio/Updog.in/master/screenshots/1.png)
![ScreenShot](https://raw.githubusercontent.com/EddieAbbondanzio/Updog.in/master/screenshots/2.png)
![ScreenShot](https://raw.githubusercontent.com/EddieAbbondanzio/Updog.in/master/screenshots/3.png)
![ScreenShot](https://raw.githubusercontent.com/EddieAbbondanzio/Updog.in/master/screenshots/4.png)

## Features

-   Create text, or link posts.
-   Edit or delete posts.
-   Comment on posts, or reply to other comments
-   Edit or delete comments
-   Karma system
-   User system with hashed + salted passwords
-   Profile pages for users

## Set Up

You'll need a PostgreSQL database. You'll also need to pre-populate the tables using the table defintion files included in the `Updog.Persistance` layer. Perhaps in the future the project will support a db initialization step.

The project is divided into two parts: the API, and the client. Client source code can be found under `Updog.Client`. All other folders hold the source code for the backend (API).

## API

To start up the API open up the project in VS Code. You'll need to define an appsettings.json file in `Updog.Api`. Config file is as follows:

```json

{
    "Database": {
        "Host": "",
        "Port": 5432,
        "User": "",
        "Password": "",
        "Database": ""
    },
    "AuthenticationToken": {
        "Secret": "",
        "Expires": 7776000,
        "Issuer": "updog.in"
    },
    "Admin": {
        "Username": "",
        "Password": ""
    }
}

- Database section holds database connection information such as IP address, user, password, and database name.
- AuthenticationToken section is everything related to the JWTs issued by the API.
- Admin section handles the admin account. Password is auto updated upon API start up, and if no admin user exists, one is created.
```

Once an `appsettings.json` file is defined the project can be ran using:

```
dotnet run -p Updog.Api/Updog.Api.csproj
```

### Client

Frontend source code is stored under `Updog.Client` and is a Vue.js project written in TypeScript. You'll need to update the backend URL in the .env file to point to your instance of the API. Project can be started via the following command:

```
serve
```

## Future Work

Backend could use a switch to pub sub set up.
