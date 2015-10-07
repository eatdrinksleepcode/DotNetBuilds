Task Default -Depends Rebuild

Task Rebuild -Depends Clean, Build

Task Clean {
	Exec { msbuild DotNetBuilds.sln /t:Clean }
}

Task SetAssemblyVersion {
	$assemblyInfo = (cat ConsoleApp/Properties/AssemblyInfo.cs) -replace 'AssemblyVersion\(""\)', 'AssemblyVersion("1.1")'
	$assemblyInfo > ConsoleApp/Properties/AssemblyInfo.cs
}

Task Build -Depends SetAssemblyVersion {
	Exec { msbuild DotNetBuilds.sln /t:Build }
	
	Exec { git checkout ConsoleApp/Properties/AssemblyInfo.cs }
}
