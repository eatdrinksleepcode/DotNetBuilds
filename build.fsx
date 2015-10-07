#r @"FAKE/tools/FakeLib.dll"
open Fake

Target "SetAssemblyVersion" (fun _ ->
  ReplaceInFiles [("AssemblyVersion(\"\")", "AssemblyVersion(\"1.1\")")] ["ConsoleApp/Properties/AssemblyInfo.cs"]
)

Target "Clean" (fun _ ->
  MSBuildRelease "" "Clean" ["DotNetBuilds.sln"]
    |> Log "AppBuild-Output: "
)

Target "Build" (fun _ ->
  MSBuildRelease "" "Build" ["DotNetBuilds.sln"]
    |> Log "AppBuild-Output: "

  let result =
    ExecProcess (fun info -> 
      info.FileName <- "git"
      info.Arguments <- "checkout ConsoleApp\Properties\AssemblyInfo.cs"
    ) (System.TimeSpan.FromSeconds 15.)

  if result <> 0 then failwith "git checkout failed"
)

Target "Rebuild" DoNothing

"Build" ==> "Rebuild"
"Clean" ==> "Rebuild"
"SetAssemblyVersion" ==> "Build"
"Clean" ?=> "Build"

RunTargetOrDefault "Rebuild"