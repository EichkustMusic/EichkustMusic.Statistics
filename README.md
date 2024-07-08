# EichkustMusic.Statistics
ASP.Net core statistics microservice


## API versions
* 1.0 - actual

## Configure and run application
1. Create _appsettings.json_ in _./EichkustMusic.Statistics.API_
```
{
 "ConnectionStrings": {
    "StatisticsDb": (postgresql connection string)
  }
}
```
2. ```dotnet run --project EickustMusic.Statistics.API```

## All endpoints are mapped to the Postman collection
https://elements.getpostman.com/redirect?entityId=25734689-ff7ed1dc-358c-4412-82c4-a308e006e459&entityType=collection