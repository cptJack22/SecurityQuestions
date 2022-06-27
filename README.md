# Security Questions
Using Visual Studio and c#, build a console app that stores answers to security question for a specified person.

## Technologies used
- ASP.NET Core (.Net 6.0)
- EntityFramework
- [Sharprompt](https://github.com/shibayan/Sharprompt) - Interactive command-line based application framework for C#
- SQL Server - data storage
- Optional InMemory mode (for debugging - does not persist data)

## Data
This application utilizes a SQL database with an option to run on InMemory data. InMemory is not persistant, so saves won't save. Running the application should connect and setup to your database on the local sql server. If you need to connect to a different server, you will neeed to update the "ConnectionStrings:ESQConnectionString" setting in the appsettings.config JSON file within the project.

If you need to run in InMemory mode, update the "DataSource" setting in the appsettings.config JSON file to "InMemory". Any other value will default to SQL Server mode.

## Notes and choices
Repository Pattern is used to access and modify data.

Main program runs by selecting a UI flow appropriate to the  context of the user. Each flow returns the value indicating the next flow to run.

Sharprompt NuGet package by shibayan selected for Console prompt capabilities.

User's name is limited to 255 characters.

User's answers are limited to 500 characters.

Questions are limited to 500 characters.

Required question count stored in a constant for easy updating.

## UI Flow

The UI Flow is meant to be easily updated if there is ever a need for a UI flow change. A new flow can be coded and called from the other UI Flows after the new flow is added to dependency injection.

There are currently three UI Flows:

### Initial Flow

User is asked their name. Once entered, the application check for an existing username (capitalization agnostic).

If the user enters `exit` as a name, the application will close. Same for **Ctrl+c** at any time.

If a user is not found, a user record will be created for the new name.

If the user record has no stored questions with responses, the user will be taken to the Store FLow.

If the user record has stored questions with responses, the user will be asked if they wish to answer security questions.

If yes, they will be taken to the Answer Flow.

If no, they will be taken to the Store Flow.

### Store Flow

In the Store Flow, the user will be asked if they would like to store answers to security questions.

If no, the user will be taken back to the Initial Flow.

If yes, the user will be presented with a list of questions to answer. They must have three questions answered to continue.

If there were questions previously stored with this user, they will be replaced upon completion of the Store Flow.

Questions may be selected from the list one at a time by using the arrow keys to highlight the desired question to answer.

When a question is selected, the user will be asked the question and may enter their answer.

The answered question will be removed from the list. 

This will continue until three questions are answered. At this point the user will taken back to the Initial Flow

### Answer Flow

In the Answer Flow, the user will be presented with their security questions to answer.

Once one question is answered correctly, the user will get a "congratulations" message.

If a question is not answered correctly the user will be ~~cast into the Gorge of Eternal Peril~~ asked the next security question.

If no questions are answered correctly, the user is notified they have not answered any security questions correctly and they are taken to the Initial Flow.
