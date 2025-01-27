# learning-together

## Setup
- following are the setup instructions for the project

### Frontend
- position in the /frontend directory through the console
	- run `npm install` in the conosle
	- run `npm run dev` in the console
	- the page should be available on port 3000  
		
### Backend
- if you are working on **Windows**:
	- use [this](https://learn.microsoft.com/en-us/dotnet/core/install/windows?tabs=net60) link for .NET installation
	- use [this](https://learn.microsoft.com/en-us/sql/database-engine/install-windows/install-sql-server?view=sql-server-ver16) link for SQL server installation
- on **Visual Studio Code**:
	- run `dotnet tool install --global dotnet-ef` in the console (this step
		does not have to be repeated if it was already executed)
	- open the project folder in the repository
	- run `dotnet ef database update` in the console
	- run `dotnet run` in the console
	- if something is not working, first check if you are positioned in the
		right directory
- on **Visual Studio**:
	- open the project
	- run `update-database` in Package Manager Console
	- start the project
- if you are working on **Linux**:
	- `sudo <package-manager> install dotnet6`
	- `sudo <package-manager> install dotnet-sdk-6.0`
	- to install SQL server follow
		[this](https://learn.microsoft.com/en-us/sql/linux/sql-server-linux-overview?view=sql-server-ver16)
 		link
		- if you are running one of the unsupported distro versions use
			[this](https://learn.microsoft.com/en-us/sql/linux/quickstart-install-connect-docker?view=sql-server-ver16&pivots=cs1-bash)
			instead
		- after you set up the docker container run `sudo docker ps -a` and
			check the 'PORTS' column - it should say 0.0.0.0:1433->1433/tcp
	- change the connection string in the appsettings.json file to this:
	  *Server=localhost,1433;Database=LearningTogether;User Id=<yourUserId>;Password=<yourPassword>;Trusted_Connection=False;MultipleActiveResultSets=true*
	- follow the steps for Visual Studio Code setup
	- if you are having issues send me a message (Mihael)
	
## Running the app
- Activating the API
	- open learning-together/backend/LearningTogether in console
	- start the SQL server
	- run `dotnet ef database update` in the console (after you've done it once you dont have to do this step anymore until some new beckend changes are pulled)
	- run `dotnet run` in the console (leave it running)

- position in the learning-together/frontend directory through the console
	- run `npm run dev` in the console
	- the page should be available on port 3000 
	
- if you are having issues send me a message (Lea)

