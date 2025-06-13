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
