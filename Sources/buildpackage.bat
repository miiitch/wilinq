msbuild wilinq.sln /p:Configuration="Release" /p:Platform=AnyCPU /Rebuild

mkdir ..\GeneratedPackages

nuget pack WiLinq.nuspec -version 0.1.0.1 -symbols -Verbosity detailed -OutputDirectory ..\GeneratedPackages