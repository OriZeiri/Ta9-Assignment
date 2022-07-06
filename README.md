# Ta9-Assignment - Organization Managment 

this is an excrices given by Ta9

Create a microservice that manage graph items as nodes and edges (objects and relationships) and allows query requests
Notice, you can select any data/data base.

I Chose to represent an organization that has Departments & Employees. 
You can make requests to manage them using the API.
Add, Create, Update & Delete both Department & Employees,
Also assign an employee to a department, using EmployeeController. Access to all the Employees who work at a specified department using DepartmentController.

# Tech Stack
-   Application Type: .NET Core WebAPI
    
-   Web framework:  [ASP.NET core](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-6.0)
    
-   Neo4j Database Connector:  [Neo4jClient](https://github.com/DotNet4Neo4j/Neo4jClient)  for Cypher  [Docs](https://neo4j.com/developer/dotnet/)
    
-   Database: Online Neo4j-Server 4.x with multi-database [Browser](https://browser.neo4j.io/) 

# Endpoints
 In addition to the basic action of the models, there are more methods who make the relations between the nodes.

    
    // Create a relation in DB, now {depId} has employee {empId} 
        curl http://BASE_URL/Employees/{empId}/assignemployee/{depId}
    
    // JSON object for all the Employees who works for a Department 
        curl http://BASE_URL/Department/{id}/employees

 

# Run

## With Docker:

 1. Clone the project
 2. Run the command

		
		docker-compose -f ./docker-compose.yml -f ./docker-compose.debug.yml up -d
	Automatically  create a Docker Image & container that runs up.
		
3. Go to http://localhost:8153

## With dotnet CLI:

1. Clone the project

2. Run the command   `dotnet run` on terminal

3. Go to  [http://localhost:8153](http://localhost:8153/)

Happy Hacking!

# Configuration options

NOTE: These are preferably configured via `./Properties/launchSettings.json`.
|Environment variable name  | Default value (or N/A) |
|--|--|
| SERVER_DB_URI | neo4j+s://c979ddd2.databases.neo4j.io |
| USERNAME | neo4j |
| PASSWORD | kdVgJkfCly0xr82fIwtH1-OC59skFNwFzMmKzKEnSig |

