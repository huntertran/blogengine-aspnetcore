DOTNET CORE DEMONSTRATION
---

**Build status**

![dotnet core build status](https://github.com/huntertran/nashtech-netcoretraining/workflows/dotnet%20core/badge.svg)

# 1. Introduction

> Historically, this project was used for demonstrating my knowledge for dotnet core and achieve the dotnet core certificate by NashTech Vietnam back in November 2018.
> 
> The technologies used back then was:
> * .net core 2.1
> * Vuejs 3
> * Vuetifyjs 1.3.0

This is a simple project for demonstrating the use of .net core 3.1 for web application with a separated front-end.

The main function is a dead simple Content Management System for blog posts

Currently, the project is using these technologies:
* .net core 3.1
* Vuejs 4
* Vuetifyjs 2

# 2. Demo (deprecated)

The blog engine is hosted in:

1. NashTech's Azure: [http://sd2941.azurewebsites.net/](http://sd2941.azurewebsites.net/)
2. Personal Azure: [https://hunter-blogengine.azurewebsites.net/](https://hunter-blogengine.azurewebsites.net/)

Admin site
1. NashTech's Azure: [http://sd2941.azurewebsites.net/admin](http://sd2941.azurewebsites.net/admin)
2. Personal Azure: [https://hunter-blogengine.azurewebsites.net/admin](https://hunter-blogengine.azurewebsites.net/admin)

Login credentials
* Username: admin@blogengine.com
* Password: 8PNddq5zFB6_zjQ

# 3. Files and folders structure

The solution is divided into folders

- `course1`, `material`: legacy training project at Nashtech
- `blogengine-client`: client project that consume the api
- `BlogEngine.*`: separated folders for projects, as below

# 4. BlogEngine.*

BlogEngine separated into back-end and front-end.

## 4.1. Back-end

`BlogEngine` was build on ASP.NET Core 3.1, target .NET Standard 3.1.

BlogEngine was separated to many small projects
* `BlogEngine.Repository`: handle data connection between Database and Service layer. The database migration and seed are also placed here.
    * `Data` > `blogengine.sql`: Script to create database
* `BlogEngine.Services`: handle business logics (but not all logics are implemented here due to short development time).
* `BlogEngine.Models`: contains application's models mapped from database
* `BlogEngine.Core`: contains convenient codes to use in the main back-end project. As solution grow, this project can be serve for a more clear purpose. For now, it can be combined with main project
* `BlogEngine.Api`: main back-end project. Contains controllers to handle user's requests.

**Authorization (deprecated)**

> DEPRECATED: The Authorization libraries in .net core 3.1 has breaking changes, make the below solution not working anymore.

At the beginning of the development phase, I would like to support both authorization schemes (Cookie based for normal Asp.net Web Razor and Token based for Single Page Application).

I actually achieved this goal, you can check the `IdentityBuilderExtension` class in `BlogEngine.Core`, method `AddIdentityWithCookieAndJwt`.

> To use this double authorization schemes, on each controller, you have to set what authorization scheme it support. Multiple schemes on a single controller is supported.

Later on, as application grow, maintaining both schemes seem to take so much effort, so I decided to use token-based only, resulted in the client site and admin-site has to use an SPA framework (Vue to be exact)

## 4.2. Front-end

The front-end client application was located in `blogengine-client`

**Libraries used**

* `npm`: Package manager
* `Vue`: front-end data binding
* `Vuetifyjs`: layout
* `Vuex`: state manager
* `vue-router`: application routing
* `Axios`: handle ajax request
* `tinymce`: WYSIWYG editor for blog post

# 5. Unit test

BlogEngine have unit test, yet simple but demonstrate how a unit test would be written

The unit tests is located in `BlogEngine.Services.Tests`, using `xUnit`

# 6. How to build 

To build BlogEngine, after cloning to your local folder, run the followings commands

**Using Visual Studio Code**

Open folder BlogEngine in VSCode, you should see the following

![first open](https://2.pik.vn/2018f6959117-d102-4a11-b79b-c9a7eafbee2b.png)

Open VSCode terminal and `cd` to `BlogEngine.Api\blogengine-client`

Restore npm packages

```bash
npm install
```

## 6.1. Run the project

You can either using Visual Studio or VSCode to run the project

**VS Code**

Click on Debug section and start debug

![debug vscode](https://2.pik.vn/2018fd8f5b91-5a2f-4f73-8068-693cc89984ff.png)

**Visual Studio**

Open the solution -> debug as normal asp.net application

# 7. Continue on development of Front-end

When debugging with VSCode or Visual Studio, the front-end components does not auto reload.

To address this problem, you can start the back-end server, then serve the front-end application as an separated project, pointed to the back-end.

Edit the main.js Axios' base URL setup:

File `main.js`

```javascript
Vue.prototype.$http.defaults.baseURL = 'https://localhost:44394/api';
```

Open VSCode terminal in `blogengine-client` folder and type

```bash
npm run serve
```

To build the client-app

```bash
npm run build
```