@echo Off
set config=%1
if "%config%" == "" (
   set config=Release
)
 
set nuget=
if "%nuget%" == "" (
	set nuget=tools\nuget
)

%nuget% restore NETXUSASharp.sln

%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild NETXUSASharp.sln /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=diag /nr:false

mkdir Build
mkdir Build\lib
mkdir Build\lib\net40

%nuget% pack "src\NETXUSASharp\NETXUSASharp.nuspec" -NoPackageAnalysis -Verbosity detailed -OutputDirectory Build -Properties Configuration="%config%"