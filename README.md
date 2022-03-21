# Health_Fitness_Journal_Portal

This repo contains a sample MVC based UI for login, logout, grant management.The system allows publishing and subscribing to Health Journals in a secure way.

# Requirement
1.	For publishers
      Web portal to upload and manage list of health and fitness Journals.
      Supported format for health Journal - Pdf
2.	For public users
      Web portal to find and subscribe to health Journal.
      Once subscribed the users are able to browse and read health Journals online.
      
## 🧐 Features

Just login into application with provided credentials you will see the required screen.

- **Role based login**

- **Microservice architechture**

- **Identity server used as service**

- **Upload file in secured manner and stored in DB**

## 🛠️ Installation Steps

1. Clone the repository

```bash
git clone https://github.com/swapniladmulwar/Health_Fitness_Journal_Portal.git
```

2. Run SQL script which is mentioned into Database folder.

```bash
cd /Database
```

3. Switch to folder Code folder to Run Identity server

```bash
cd /Code/HealthJournals
```

4. Run the Identity server app. This run as background application on port 5001.

```bash
run HealthJournalsIdentityserver.exe
url : https://localhost:5001
```

5. Switch to folder Code folder to Run MVC application
```bash
cd /Code/HealthJournals
```

6. Update your connection string in appsetting.json

```bash
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*",
  "Settings": {
    "DBConnectionString": "Data Source=dell-xps; initial catalog=Journal; user id=test; password=test@123#"
  }
}
```

7. Run the MV app. This run as application on port 5002 and open the browser with login page.

```bash
run HealthJournalsIdentityserver.exe
url : https://localhost:5002
```

8. If you are admin you can see your uploaded journals and if you are user then you will see the subscribed journal.

```bash
Test users : 
  1. Admin
      Username = "AliceSmith@email.com", Password = "alice"
  2. User
      Username = "BobSmith@email.com", Password = "bob"
```

🌟 You are all set!
