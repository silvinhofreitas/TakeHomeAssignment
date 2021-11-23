# TAKE HOME ASSIGNMENT

This project is part of [Take Home Assignment](https://github.com/OriginFinancial/origin-backend-take-home-assignment) for Origin Financial.
It was developed in **.net core 5.0**.

## RUNNING THE PROJECT

To properly run the project, you need to have installed *ASP.NET Core Runtime*, that can be found in [this link](https://dotnet.microsoft.com/download/dotnet/5.0).
After installed, follow these steps in your terminal to run the project:

1) Navigate to *TakeHomeAssignment* folder;
2) Run ``dotnet build`` to make sure you have the last compilation working;
3) Run ``dotnet run`` to spin up the server.

When the server is running, you can make the calls to the API using your preferred service, or even use Swagger UI:

### Swagger UI

You can run the API directly in your browser using Swagger UI:

1) Open your browser;
2) Navigate to [https://localhost:5001/swagger/index.html](https://localhost:5001/swagger/index.html)
3) Expand the *RISK CONTROLLER POST EXAMPLE* and click in *Try It Out*;
4) Provide the data in the *Request body JSON*;
5) Click in *Execute*

The result should be displayed after the execution.
    
## RUNNING THE TESTS

To properly run the test project, you need to have installed *ASP.NET Core Runtime*, that can be found in [this link](https://dotnet.microsoft.com/download/dotnet/5.0).
After installed, follow these steps in your terminal to run the project:

1) Navigate to *TakeHomeAssignment.Test* folder;
2) Run ``dotnet test`` to run all the tests inside the Test Project.

## OVERALL COMMENTS

### Insurance Lines
For the **Insurance Lines** it was created a Constant File with the initial lines (Auto, Disability, Home and Life).
That way, if a new Insurance Line is created, we need to add it to this file. That way, we can assure that we don't have typos when dealing with the strings.

For each new **Insurance Line**, the business rule inside **RiskCalculator** and the tests needs to be updated with this new line rules.

### Validations
The only files with validation are the DTO's **UserInputDTO**, **HouseDTO** and **VehicleDTO**, giving the data inside these files being the only ones that the user can input.

### Final Score
The **FinalScore Field (Economic, Regular, Responsible)** is automatic generated at the *GET* moment, according to **Score Field**. There is also a **Ineligible Field** that determines if that line is ineligible.
Even being ineligible, the score is calculated, but it's returned **Ineligible** as the **FinalScore** for the user.
The **FinalScore** is mapped in the following way:

```c#
Score <= 0 = Economic
Score >0 && Score <3 = Regular
Score >= 3 = Responsible
```

### DTO's
See bellow the DTO's example for input and output data:

- Input:

```json
{
  "age": 35,
  "dependents": 2,
  "house": {"ownership_status": "owned"},
  "income": 0,
  "marital_status": "married",
  "risk_questions": [0, 1, 0],
  "vehicle": {"year": 2018}
}
```

- Output:

```json
{
  "auto": "regular",
  "disability": "ineligible",
  "home": "economic",
  "life": "regular"
}
```

### QUESTIONS

Any doubts or further questions can be addressed to me (silvinhofreitas@live.com).
I'll be more than happy to help with anything that may came up!