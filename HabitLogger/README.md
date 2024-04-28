# Habit Logger
Console based CRUD application to track habits.
Developed using C# and SQLite.

# Given Requirements:
- [x] When the application starts, it should create a sqlite database, if one isn’t present.
- [x] It should also create a table in the database, where the habits will be saved.
- [x] You need to be able to insert, delete, update and view your logged habits. 
- [x] You should handle all possible errors so that the application never crashes 
- [x] The application should only be terminated when the user inserts 0. 
- [x] You can only interact with the database using raw SQL. You can’t use mappers such as Entity Framework

# Features

* SQLite database connection

	- The program uses a SQLite db connection to store and read information. 
	- If no database exists, or the correct table does not exist they will be created on program start.

* A console based UI where users can navigate by key presses

* CRUD DB functions
	- From the main menu users can Create, Read, Update or Delete entries for whatever habit they want.

# Challenges
	
- It was my first time using SQLite and with limited experience in C#. I had to learn it from the beginning in order to complete this project. 
- Getting the correct SQLite version installed using VS code was a challenge at first. After looking through the docs i found that i might've been trying to install the wrong version.
I then realised i had old versions of .NET on my machine which interacted badly with the package. Cleaning up in the old versions and clearing the build i was able to get it working.
- Trying to use the MVC (Model - View - Controller) pattern in a way that made sense. I am still not sure if i did it correctly, however i believe the responsibilites are placed and executed in the correct classes. 
	
# Lessons Learned
- I created a very rough UML diagram to visualize the MVC pattern. I think it was good way to start, I however did not always look at it when i found myself confused while programming. I should have updated it as i went along.
- I need to focus on one feature at a time, and implement it fully. I have to remember i can always refactor and extract some of the logic if necessary.

# Areas to Improve
- I could utilize code snippets better - Especially when writing a console app requires a lot of Console.??? that are repeated. 
- single responsibility. I did improve at this along the way, but I still have some work to do on it. I think I could have made my methods a little better so they only had a single use.