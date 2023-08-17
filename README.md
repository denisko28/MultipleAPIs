# MultipleAPIs

The backend part of the **Barbershop CRM**. The app was created with **C# .NET Core Framework**.
* For the microservices version, view the "Microservice-Architecture" branch: https://github.com/denisko28/MultipleAPIs/tree/Microservice-Architecture

## The diagram of the project
<img width="1038" alt="Снимок экрана 2023-08-17 в 16 22 11" src="https://github.com/denisko28/MultipleAPIs/assets/39884531/0697d748-0dc0-4a33-890a-363d7b4a2856">

## Tech. stack used in the project
**REST API, XUnit, Jwt Tokens, Microservices, RabbitMQ, MassTransit, gRPC, Ocelot, Entity Framework, Fluent Validation, Automapper, Serilog, 
Elastic Search, Kibana, Polly, SQL Server, MongoDB, Redis, Docker, K8S.**

## The history of the project
The application was created for educational purposes, as a University project.
As a result, the main goal was to learn new technologies and principles.

### Initial version
At the very beginning, there was a task to create an app, which will consist of three different projects, built using different architectures 
(**N-Tier** and **Clean Arch**).
Also, there was a requirement to use **MongoDB** as a primary database inside one of the projects, and for the rest **SQL Server** had to be used.
In addition, both projects that use **SQL Server** are required to have different data access mechanisms, one should use Entity Framework Core and another ADO.NET.

In the final result, all the necessary entities, repositories, and services were successfully implemented.

At this point, each of the projects was represented with a separate **REST API**.

The initial version is stored in the "master" branch: https://github.com/denisko28/MultipleAPIs/tree/master

### Microservices version
After completing the initial version of the app, there was a task to separate the projects completely. This was necessary due to prior 
interdependencies that existed between them.

All these actions were done to fit the **microservice architecture**. 

To perform data exchange among projects(microservices) the
message broker (**RabbitMQ** + **MassTransit**) and **gRPC** were used.

The microservices version is stored in the "Microservice-Architecture" branch: https://github.com/denisko28/MultipleAPIs/tree/Microservice-Architecture
