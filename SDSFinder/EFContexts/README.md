# SDSFinder Code First
These commands should be run under the following directory: `.\repos\SDSFinder\SDSFinder`

## Add Migration Deployment:
```
$env:ASPNETCORE_ENVIRONMENT='Local'
dotnet ef database update --context SDSFinderContext
dotnet ef migrations add [Description] --context SDSFinderContext
dotnet ef database update --context SDSFinderContext
```

## Merge Code Changes:
```
$env:ASPNETCORE_ENVIRONMENT='Local'
dotnet ef database drop --context SDSFinderContext
dotnet ef database update --context SDSFinderContext
dotnet ef migrations add [Description] --context SDSFinderContext
dotnet ef database update --context SDSFinderContext
```

## Replace Deployment
```
$env:ASPNETCORE_ENVIRONMENT='Local'
dotnet ef database update 0 --context SDSFinderContext
dotnet ef migrations remove --context SDSFinderContext
dotnet ef migrations add [Description] --context SDSFinderContext
dotnet ef database update --context SDSFinderContext
```