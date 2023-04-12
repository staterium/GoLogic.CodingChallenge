# GoLogic Coding Challenge
 A coding challenge completed for GoLogic.

## Description

### Problem
A vending machine needs an API to enable it to transact with a user: deposit funds, make selections, receive change.

### Solution
This is a back-end solution focusing only on the API that the vending machine would need. The tech stack is .NET 6. It consists of the following components:

#### 1. Core Project
This project contains all the core functionality shared between other projects: Entities, Interfaces and Exceptions.

The entities are User, Product and Purchase. There are interfaces for a repository for each entity, as well as one for a Purchase Service that performs a purchase.

#### 2. Infrastructure Project
This project proves implementations of the interfaces defined in Core, using MongoDB as a data store. It relies only on the Core project. I use the repository pattern for easy substitution during testing.

#### 3. WebAPI Project
This project provides the API endpoints called by the vending machine. It relies only on the Infrastructure project, and thus can be easily replaced with another implementation using a different database.

This project also contains a .http file for easy on-the-go testing against running dev instances.

#### 4. Unit Test Project
This project tests the core project and all the related business rules.

#### 5. Functional Test Project
This project performs E2E functional tests against the API. It uses in-memory implementations for the Repositories and starts the WebAPI project with those dependencies. It then makes and asserts actual API calls using an HTTPClient.

### 6. Docker
The WebAPI runs in a docker container. I use docker compose to also start a container for MongoDB. Container-to-container communication is facilitated via container name. 

### 7. Github actions
Github actions build, test and deploy the code to Azure. A new build happens on every push.

### 8 Azure Container Apps
Azure Container Apps make it easy to deploy docker containers in a single environment. Within the same environment, container-to-container communication also happens via container name, so once the production environment is set up there is no config difference between Dev and Prod.

## Technology Choices

- I chose to use docker containers to make everything easy to deploy and scale in the cloud.
- I chose MongoDB for rapid prototyping and to cater for a data schema that his minimal relationships.
- I chose .NET 6 as it is the tech stack I am most comfortable with.
- I chose Azure to deploy to as I am most familiar with it.
- I chose to separate the unit and functional tests because they inherently operate much differently. While unit tests are fast to write and run independently or in a batch, the functional tests require more set up, and often perform many steps against the API before testing can begin.
- I chose to ensure both testing project run without external dependencies, and as such are called from the Github deploy action as a quality gate, before any actual deployments take place.
- The overall architecture is inspired by Steve Smith's [Clean Architecture](https://github.com/ardalis/CleanArchitecture).
- The API endpoint module was originally written by [Tim Deschryver](https://timdeschryver.dev/blog/maybe-its-time-to-rethink-our-project-structure-with-dot-net-6).

## Trade-offs and areas for improvement

- There is an opportunity to improve the architecture using event messaging. All the user actions (deposit, purchase, cashout), can be improved by firing events. This will enable much easier future development due to the decoupling of procedure calls.
- The system implements no security measures whatsoever. To enable scaling in the cloud I "log" a user in using only a name, and that name is then the user's identity. There is no validation that a user name is unique.
- There is no logging apart from the system logging that Azure performs on container apps. This can definitely be improved.
- If I had more time I would have probably developed a v2 of the app, using a more microservices-based approach and vertical organisation of related concepts.

![Proposed v2 Design](https://i.imgur.com/M0q4ngq.png)

The published address for the API is at https://webapi.politetree-28b92419.australiaeast.azurecontainerapps.io/.
