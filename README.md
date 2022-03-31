# DaprActors
Dapr actor service and client example

## Usage
Open terminal, switch path to `MyActorService`, then enter the commands below:
```
dapr run --app-id myapp --app-port 5000 --dapr-http-port 3500 -- dotnet run --no-launch-profile
```

Open another terminal, switch path to `DaprBlazorServer`, then enter the commands below:
```
dapr run --app-id myapp2 --app-port 5010 --dapr-http-port 3510 -- dotnet run
```
Click any button and see what messages you get in command propmpt:
![image](https://user-images.githubusercontent.com/55481017/160965561-b3ebcdde-93f7-41ae-bda7-d849e32bcf9b.png)
![image](https://user-images.githubusercontent.com/55481017/160965698-11ecb05f-ba50-481f-9178-2fb77ae3e472.png)
![image](https://user-images.githubusercontent.com/55481017/160965710-0e0fec77-7639-4e40-8db2-0c99214ff33c.png)


Below are some commands for test:
```
curl -X POST http://localhost:3500/v1.0/actors/MyActor/myactor2/method/SetDataAsync -d "{\"propertyA\":\"ValueA\",\"propertyB\":\"ValueB\"}"

curl -X GET http://localhost:5000/dapr/config


curl -X GET http://localhost:3500/v1.0/actors/MyActor/myactor2/state/

curl -X POST http://localhost:3500/v1.0/actors/MyActor/myactor2/state '[{"operation": "delete", "request": {"key": "key2"}}]'

curl -X POST http://localhost:3500/v1.0/actors/MyActor/myactor2/timers/tinytimer \
  -H "Content-Type: application/json" \
  -d '{
        "dueTime": "0h0m0s0ms",
        "period": "0h0m10s0ms"
      }'
	  
	  
curl -X DELETE http://localhost:3500/v1.0/actors/MyActor/myactor2/timers/tinytimer


curl -X GET http://localhost:5000/dapr/config -H "Content-Type: application/json"

```
