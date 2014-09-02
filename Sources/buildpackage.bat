rmdir ..\GeneratedPackages
rmdir /s /q ..\BuildOutput
mkdir ..\BuildOutput
mkdir ..\GeneratedPackages
set VERSION=0.2.0.2

msbuild wilinq.sln /p:Configuration="Release" /p:Platform="Any CPU"  /p:GenerateProjectSpecificOutputFolder=true /p:Outdir="..\..\BuildOutput" /t:build


.nuget\nuget.exe pack WiLinq.nuspec -version %VERSION% -symbols -Verbosity detailed -OutputDirectory ..\GeneratedPackages