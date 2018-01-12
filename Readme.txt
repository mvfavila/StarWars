Note from the author:
	My previous objective was to document the application using Sandcastle (https://github.com/EWSoftware/SHFB)
	but so I can deliver quicker, I have decided to create this read me file.

############# R2-D2 Travel Log Documentation #############

Summary

	1 - Application
	2 - Commands
	3 - Computing  
	4 - Technology & Solution Architecture	
	5 - Tests
	6 - Dependency Injection
	7 - Author

1 - Application
	R2-D2 Travel Log was built as a solution to a code challenge for a job opportunity with Kneat Software.
	Using the API SWAPI (https://swapi.co/) and given a trip distance, the solution will compute the number of 
	stops for resupply each of the existing Star Ships should make while travelling to a Star.
	The application was built as a C# Console Application according to the SOLID principles and aspects of
	Domain Driven Design, also Unit tests were added to support the code correctness.

2 - Commands
	 ----------------------------------------------------------------------------
	|  Command  |             Description             | Case sensitive |  Domain |
	 ----------------------------------------------------------------------------
	|           |                                     |                |         |
	|   Exit    | Stops the application execution.    |      No        |   N/A   |
	|           |                                     |                |         |
	 ----------------------------------------------------------------------------
	|           |                                     |                |         |
	|  Distance | Distance the Star Ships will travel.|      N/A       | decimal |
	|           |                                     |                |         |
	 ----------------------------------------------------------------------------
	 
3 - Computing 
	The number of resupply stops planned for each Star Ship is given by the formula:
	
		Stops = D / (M * C)
		
	Where D = Trip total distance (informed by the user),
		  M (or MGLT) = Maximum speed travelled by a Star Ships in Mega Lights in 1(one) hour,
		  C = Consumables used by a Star Ship per hour.
    
4 - Technology & Solution Architecture
	This app was built with .Net 4.6.2, Visual Studio Community and a set of free productivity tools.	
	The solution was organized in 4 layers and 5 projects (+ tests projects).
	
	Layers: 
		Presentation, Application, Domain, Infra
	
	Projects:
	
		- Presentation
			Represents the User Interface which in this case is a Console. Responsible for the interaction with
			with the user.
			Project: KS.StarWars.Presentation.UI.MainConsoleApp
		
		- Application
			Represents the application orchestrator. Responsible  for organizing the steps required to execute
			a function e.g. Find the number of resupply stops for the Star Ships.
			Project: KS.StarWars.Application
					 
		- Domain
			Represents the application Domain, or the entities and their behaviour e.g. StarShip, StarlogPage.
			Responsible  for the application business rules.
			Project: KS.StarWars.Domain
					 KS.StarWars.Domain.Tests
					 
		- Infra.Data
			Represents the application repositories. Responsible  for retrieving the information from external
			sources e.g. SWAPI.
			Project: KS.StarWars.Data
					 KS.StarWars.Data
					 
		- Infra.CrossCutting.IoC
			Responsible  for the Dependency Injection.
			Project: KS.StarWars.CrossCutting.IoC
			
5 - Tests
	Tests were build using the following frameworks:
	
	xUnit - A free, open source, community-focused unit testing tool for the .NET Framework. (https://xunit.github.io/)
	Bogus - A simple and sane fake data generator for C#, F#, and VB.NET. (https://github.com/bchavez/Bogus)
	Moq - Mocking framework for .NET. (https://github.com/moq/moq)
	AutoMoq - Auto mocking provider for Moq. (https://github.com/darrencauthon/AutoMoq)
	
	The use of data generators allowed to easily test up to one hundred thousand entries per Unit Test.
	
6 - Dependency Injection
	SimpleInjector is the Dependency Injection library used to build this application.
	Some characteristics:
		Simple to use
		Well documented
		Amongst the fastest DI Containers available (http://www.palmmedia.de/blog/2011/8/30/ioc-container-benchmark-performance-comparison)

7 - Author
	Name: Marcos Vinicius Fontes de Avila
	Last contribution: 12/01/2018
	E-mail: mvfavila@gmail.com
