rmdir ..\GeneratedPackages
rmdir /s /q ..\BuildOutput
mkdir ..\BuildOutput
mkdir ..\GeneratedPackages


msbuild wilinq.sln /p:Configuration="Release" /p:Platform="Any CPU"  /p:GenerateProjectSpecificOutputFolder=true /p:Outdir="..\..\BuildOutput" /t:build


.nuget\nuget.exe pack WiLinq.nuspec -version 0.1.1.0 -symbols -Verbosity detailed -OutputDirectory ..\GeneratedPackages