Task Default -Depends Rebuild

Task Rebuild -Depends Clean, Build

Task Clean {
	Exec { msbuild DotNetBuilds.sln /t:Clean }
}

Task SetAssemblyVersion {
	(cat ConsoleApp/Properties/AssemblyInfo.cs.template) -replace '@@AssemblyVersion@@', '1.1' `
		> ConsoleApp/Properties/AssemblyInfo.cs
}

Task Build -Depends SetAssemblyVersion {
	Exec { msbuild DotNetBuilds.sln /t:Build }
}
