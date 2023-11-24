# Orleans.StreamProcessing
This project is a .NET application that utilizes Orleans framework for stream processing. It provides a scalable and distributed architecture for processing real-time data streams.

## Running the StreamProcessing.Sample
1. Go to src\Services\StreamProcessing and build solution.
2. Go to StreamProcessing output folder and run following command:
```dotnetcli
dotnet run StreamProcessing.dll --InstanceId 1
```
3. Go to StreamProcessing.Sample output folder and run following command:
```dotnetcli
dotnet run StreamProcessing.Sample.dll --HostGetWayId 1
```
---
**NOTE**

Run StreamProcessing.dll with different `InstanceId` in different terminals to have multiple silos.

---