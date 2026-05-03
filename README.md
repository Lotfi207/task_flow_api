# task_flow_api

Description:

API TaskFlow, une API REST développée avec ASP.NET Core Web API et Entity Framework Core.

Elle permet aux utilisateurs authentifiés de gérer :

- des projets,
- des tâches liées aux projets en ajoutant aussi des commentaires .

Authentification
//
//
//
Projects:

base route: /api/projects

 * Endpoints:
	
	* get all projects: GET /api/projects
	=> Retourne uniquement les projets de l’utilisateur connecté

	* get project by id: GET /api/projects/{id}
	=>Retourne un projet s’il appartient à l’utilisateur connecté

	* create project: POST /api/projects
	   
	   Body example:
	   ```json
	   {
	       "name": "Project Name",
	       "description": "Project Description"
	   }
	   ```
	   * update project: PUT /api/projects/{id}
	   
	   Body example:
	   ```json
	   {
	       "name": "Updated Project Name",
	       "description": "Updated Project Description"
	   }
	   ``` 
	   => Met à jour un projet s’il appartient à l’utilisateur connecté

	   * delete project: DELETE /api/projects/{id}
		=> Supprime un projet s’il appartient à l’utilisateur connecté


	Tasks:
		base route: /api/tasks

		* Endpoints:
	
	* get all tasks: GET /api/tasks
	=> Retourne uniquement les tâches de l’utilisateur connecté

	* get task by id: GET /api/tasks/{id}
	=>Retourne une tâche s’il appartient à l’utilisateur connecté
	
	* create task: POST /api/tasks
	
	   Body example:
	   ```json
	   {
		   "title": "Task Title",
		   "description": "Task Description",
		   "projectId": 1
	   }
	   ```
	   * update task: PUT /api/tasks/{id}
	
	   Body example:
	   ```json
	   {
		   "title": "Updated Task Title",
		   "description": "Updated Task Description",
		   "projectId": 1
	   }
	   ``` 
	   => Met à jour une tâche s’il appartient à l’utilisateur connecté
	   * delete task: DELETE /api/tasks/{id}
		=> Supprime une tâche s’il appartient à l’utilisateur connecté

		Security Rules:

		- Les utilisateurs doivent être authentifiés pour accéder à l'API.
		- Les utilisateurs ne peuvent accéder qu'aux projets et tâches qui leur appartiennent.
		- Les utilisateurs ne peuvent pas accéder aux projets et tâches des autres utilisateurs.
		- Les utilisateurs doivent fournir un token d'authentification valide pour chaque requête.
		- Les utilisateurs doivent avoir les permissions appropriées pour effectuer certaines actions.
		

		Tech Stack:
		
		- ASP.NET Core Web API
		- Entity Framework Core
		- SQL Server
		- Swagger / OpenAPI
		- JWT Authentication
		

		Status Enum:

		AFaire
        EnCours
        Termine

		How to run:
        1. Clone the repository
		2. Open the solution in Visual Studio
		3. Update the connection string in appsettings.Development.json to point to your SQL Server instance
		4. Run the application: dotnet restore
                                dotnet ef database update
                                dotnet run 


