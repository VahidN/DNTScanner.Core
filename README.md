DNTScanner.Core
=======

DNTScanner.Core is a .NET 4x and .NET Core 2x+ wrapper for the `Windows` Image Acquisition library.

Install via NuGet
-----------------
To install DNTScanner.Core, run the following command in the Package Manager Console:

```
PM> Install-Package DNTScanner.Core
```

You can also view the [package page](http://www.nuget.org/packages/DNTScanner.Core/) on NuGet.


Usage
-----
- [How to use it with a console application](/DNTScanner.Core.Tests/DNTScanner.ConsoleTestApp/Program.cs)
- [How to use it with a web application](/DNTScanner.Core.Tests/DNTScanner.WebTestApps/DNTScanner.ASPNETCoreApp/Controllers/ScannerController.cs)
  - Restore the dependencies of DNTScanner.WebTestApps by running _0-restore.bat file.
  - Then run the web application by using the _1-dotnet_run.bat file.
  - Now repeat these steps for the [DNTScanner.WindowsService](/DNTScanner.Core.Tests/DNTScanner.WebTestApps/DNTScanner.WindowsService/Program.cs) application:
     - Restore its dependencies by running the _0-restore.bat file.
     - Then run the WindowsService application by using the _1-dotnet_run.bat file.
	 - Finally connect your scanner device to the system and start testing it.

![test-scanner](/DNTScanner.Core.Tests/DNTScanner.WebTestApps/DNTScanner.ASPNETCoreApp/wwwroot/uploads/test-scanner.gif)