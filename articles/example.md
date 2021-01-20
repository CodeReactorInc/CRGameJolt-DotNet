# Examples

Very easy to use, you only need initialize only one class and she gonna take control of the rest

## Using Global Data Storage without logging

```csharp
GameJolt gameJolt = new GameJolt(gameId, apiToken);
gameJolt.GlobalDataStorage["key"] = new DataStorageValue("Value");
```

## Giving trophies to a user

```csharp
GameJolt gameJolt = new GameJolt(gameId, apiToken);
gameJolt.Login(username, userToken);
gameJolt.UserLogged.Trophies[userId].Add();
```

## Adding a score in GameJolt Game API

```csharp
GameJolt gameJolt = new GameJolt(gameId, apiToken);
gameJolt.Login(username, userToken);
gameJolt.Tables.Primary.Add("20 Points", 20, null, gameJolt.UserLogged);
```

## Creating a session

```csharp
GameJolt gameJolt = new GameJolt(gameId, apiToken);
gameJolt.Login(username, userToken);
gameJolt.UserLogged.Session.Open();
Thread.Sleep(SessionManager.Timeout);
gameJolt.UserLogged.Session.Ping();
```