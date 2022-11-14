# bacit-dotnet (based on https://github.com/espenlimi/-bacit_dotnet)
##Student group project in subject IS-202 - UiA 2022

### How to use
Prerequisites:
To make this work, you need to have Docker installed and running on your system.

Via commandline with docker (Recommended):
Note: On Unix and Unix-like systems (Mac and Linux) you might need to run the commands with sudo to make them work.

### 1. Build then start the docker container with the web application:
Type the following in the terminal:

```docker image build -t webapp .```

```docker container run --rm -it -d --name webapp --publish 80:80 webapp```

or

(Windows)
Run build.exe file to run image build in docker.
(Mac)
Run build.sh file to run image build in docker.

### 2. Start a mariadb container using the localdirectory "database" to store the data:

| Bash (Mac and Linux)  | Powershell (Windows) |
| ------------- | ------------- |
| ```docker run --rm --name mariadb -p 3308:3306/tcp -v "$(pwd)/database":/var/lib/mysql -e MYSQL_ROOT_PASSWORD=12345 -d mariadb:10.5.11```  | ```docker run --rm --name mariadb -p 3308:3306/tcp -v "%cd%\database":/var/lib/mysql -e MYSQL_ROOT_PASSWORD=12345 -d mariadb:10.5.11```  |

or

(Windows)

Run startDb.exe to automatic start mariaDb container in docker.

(Mac)

Run startDb.sh to automatic start mariaDb container in docker.

### 3. Run the reloadTables script to create the database and table for this project.
reloadTables.exe - Execute database and tables on Windows
reloadTables.sh - Execute database and tables on Mac
reloadTables.sql - SQL file

### 4. Test out the code at http://localhost:80/

Ps:
To enter the database and create the database and table for this skeleton, run:
docker exec -it mariadb mysql -p12345
in the terminal.
