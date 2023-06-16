# powerplant-coding-challenge

## Requirement:
.net framework sdk 6.0

## How to build:
Go to the project folder and run the commands below:

    dotnet restore

    dotnet build

    dotnet run

### Using docker instead:
To build the image use the command below:

    docker build -f .\src\Dockerfile -t powercalc .

To run the container, type the command below:

    docker container run -d --name powercalccontainer -p 8888:80 powercalc

## How to call the API
Use the address to call the api via POST, with the JSON payload in the body:

http://localhost:8888/productionplan



