# task_flow_api

link to git repo:
https://github.com/Lotfi207/task_flow_api.git

Description:

API TaskFlow, une API REST développée avec ASP.NET Core Web API et Entity Framework Core.

Elle permet aux utilisateurs authentifiés de gérer :

- des projets,
- des tâches liées aux projets en ajoutant aussi des commentaires .

Authentification: 

-les routes d'authentification sont definies dans le controller de user, pour raison de simplification d'architecture et respect 
de l'enonce du projet.

-L'API utilise JWT (JSON Web Tokens) pour l'authentification. Les utilisateurs doivent s'inscrire et se connecter
pour obtenir un token d'authentification, qui doit être inclus dans les en-têtes des requêtes pour accéder aux 
ressources protégées de l'API.
base route : /api/users/

* Endpoints:
	
	* register: POST /api/users/register
	
	   Body example:
	   ```json
	   {
		   "username": "user1",
		   "email": "user1@example.com",
		   "password": "Password123!",
		   "Role": "User"/"Admin"
	   }
	   ```
	* login: POST /api/users/login
	   
	   Body example:
	   ```json
	   {
		   "email": "user1@example.com",
		   "password": "Password123!"
	   }
	   ```
	   => Retourne un token JWT si les informations d'identification sont correctes

	* get all users: GET /api/users protected by admin role
	   login par un utilisateur admin pour obtenir un token JWT, 
	   puis inclure ce token dans les en-têtes de la requête GET /api/users pour accéder à la liste des utilisateurs.
	   
	   Body example:  
	      None 
		  => Retourne une liste de tous les utilisateurs (accessible uniquement par les administrateurs)

	* get user by id: GET /api/users/{id} protected by admin role
	   login par un utilisateur admin pour obtenir un token JWT, 
	   puis inclure ce token dans les en-têtes de la requête GET /api/users/{id} pour accéder aux détails d'un utilisateur spécifique.

	   Body example:
		  None 
		  => Retourne les détails d'un utilisateur spécifique (accessible uniquement par les administrateurs)

	* update user: PUT /api/users/{id} protected by admin role
	   login par un utilisateur admin pour obtenir un token JWT, 
	   puis inclure ce token dans les en-têtes de la requête PUT /api/users/{id} pour mettre à jour les informations d'un utilisateur spécifique.
	   Body example:
	   ```json
	   {
		   "username": "updatedUser1",
		   "email": "updatedUser1@example.com",
		   "password": "UpdatedPassword123!",
		   "Role": "User"/"Admin"
	   }
	   ```
	   => Met à jour les informations d'un utilisateur spécifique (accessible uniquement par les administrateurs)

	* delete user: DELETE /api/users/{id} protected by admin role
	   login par un utilisateur admin pour obtenir un token JWT, 
	   puis inclure ce token dans les en-têtes de la requête DELETE /api/users/{id} pour supprimer un utilisateur spécifique.
	   Body example:
		  None
		  => Supprime un utilisateur spécifique (accessible uniquement par les administrateurs)

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


Associations:pseudo git <=> nom et prenom :
- sfdhafsa <=> HAFSA SIF-EDDINE
- Lotfi207 <=> IKRAM LOTFI