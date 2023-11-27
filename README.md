# Orleans.StreamProcessing
This project is a .NET application that utilizes Orleans framework for stream processing. It provides a scalable and distributed architecture for processing real-time data streams.

## Running the StreamProcessing.Sample
1. Go to src\Services\StreamProcessing and build solution.
2. Go to StreamProcessing output folder and run following command:
```dotnetcli
dotnet StreamProcessing.dll --InstanceId 1
```
3. Go to StreamProcessing.Sample output folder and run following command:
```dotnetcli
dotnet StreamProcessing.Sample.dll --HostGatewayInstanceId 1
```
---
**Note**

Run StreamProcessing.dll with different `InstanceId` in different terminals to have multiple silos.

---
## Running the Workflow.Api.Sample
1. Go to src\Services\StreamProcessing and build solution.
2. Run StreamProcessing project.
3. Run StreamProcessing.Api project;
4. Go to src\Services\Workflow and build solution.
5. Run Workflow.Api project.
6. Run Workflow.Api.Sample project.