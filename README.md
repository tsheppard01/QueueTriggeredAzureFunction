# QueueTriggeredAzureFunction

Project showing implementation of a queue triggered Azure Function.  Source code assocciated with medium blog: link.

Function is triggered by an azure storage queue, contents of queue are uploaded to and azure storage container as a txt file.


## Build
```
dotnet clean
dotnet build FunctionsProj
dotnet publish FunctionsProj/FunctionsProj.csproj -c Release -o ./published/FunctionsProj
```

## Run
```
docker-compose up -d --build
```

This runs an Azurite container and the Azure Function in a container.  Use Azure Storage Explorer to interact with the Azurite container.  Adding a message to the queue `srcqueue` will trigger the function.  Data is output to the `destcontainer` container.
 
