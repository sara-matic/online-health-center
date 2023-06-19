# Online Health Center

Online Health Center is a microservice application designed to enhance the healthcare experience for both patients and healthcare professionals.  
<br>

## Technologies Used
- .NET 6
- Angular
- Docker

<br>

## Getting Started
To get started with the Online Health Center, follow the instructions given bellow. 

1. Run docker compose command in the solution directory: <br>
    ```shell
    docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml up -d --build
    ```

2. Go to <strong> Tools > NuGet Package Manager > Package Manager Console </strong> to open the Package Manager Console. <br>
Ensure that the default project selected in the Package Manager Console is the IdentityServer microservice project.

3. Run the migration update command in the Package Manager Console: <br>
    ```shell 
    Update-Database
    ``` 

4. Open a terminal and navigate to the <strong>\WebApps\OnlineHealthCenterSPA\ </strong> directory.

5. Run the following commands: <br>
    ``` shell
    npm install
    ng serve
    ```

<br>

## Microservices

#### IdentityServer: 
The IdentityServer microservice handles user authentication and authorization. It provides secure access control.

#### Impressions: 
The Impressions microservice allows patients to share their experiences and provide feedback by leaving impressions about doctors, helping to foster transparency.

#### Reports: 
The Reports microservice enables doctors to create and manage medical reports for patients. It ensures accurate documentation of patient diagnoses, prescription, and medical histories. Patients can access their own reports, while doctors can review reports they have written.

#### EmployeeInformation: 
The EmployeeInformation microservice manages information about healthcare staff members. It provides details such as names, specializations, titles, and biographies.

#### Appointments: 
The Appointments microservice enables patients to request appointments with preferred doctors, and doctors and nurses to review and manage appointment requests.

#### Discounts: 
The Discounts microservice manages discounts for patients.

<br>

## User Roles
The Online Health Center supports three types of users:

#### Patients
Main capabilities: appointment requests, viewing scheduled appointments, accessing medical reports, leaving feedback

#### Doctors
Main capabilities: ability to create and review patient medical reports

#### Nurses
Main capabilities: register new staff members, approve appointment requests, add employee information

<br>

## Authors
- Sara Matić, 1031/2022
- Ana Pantić 1021/2022
- Petar Milikić 1032/2021

<br>
