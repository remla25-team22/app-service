# app-service

This repository contains the **backend API service** for the REMLA project (Team 22).  
It acts as a gateway between the frontend and the `model-service`, routing user input to the model and returning predictions.

---

##  Project Structure

```
app-service/
├── app-service/                  # ASP.NET Core Web API source
│   └── Controllers/              # Controllers for routing requests
├── model-service.Connector/     # Shared code for communicating with the model-service
├── app-service.sln              # .NET solution file
├── VERSION.txt                  # Semantic version for automated releases
├── Dockerfile                   # Docker container definition
├── .github/workflows/           # CI/CD configuration
│   └── github-actions.yml       # Workflow for test and release
├── .gitignore
└── README.md
```

---

##  API Overview

The backend exposes REST endpoints to:
- Receive user review text
- Forward it to the `model-service`
- Return the model's sentiment prediction to the frontend

---

##  Integration

- Communicates with the `model-service` over HTTP
- `MODEL_SERVICE_URL` is passed as an environment variable for deployment flexibility
- Includes a shared connector in `model-service.Connector` for request formatting

# Setup
## Running the app
1. Open the app-service.sln in a compatible IDE. For this project, Visual Studio was used (*Not Visual Studio Code*)
2. Add the `Remla25-team22` GitHub NuGet Package Source (https://nuget.pkg.github.com/remla25-team22/index.json)
3. Run app-service

## Publish the app to a docker registry 
To publish the service to a docker registry, simply run `dotnet publish --os linux --arch x64 /t:PublishContainer` to push to your local container registry. For further information, visit [Containerize a .NET app with dotnet publish](https://learn.microsoft.com/en-us/dotnet/core/containers/sdk-publish).