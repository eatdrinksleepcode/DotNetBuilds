#r @"FAKE/tools/FakeLib.dll"
open Fake
open Fake.AssemblyInfoFile

Target "SetAssemblyVersion" (fun _ ->
  CreateCSharpAssemblyInfo "ConsoleApp\Properties\AssemblyInfo.cs"
    [Attribute.Version "1.1"]
)

Target "Clean" (fun _ ->
  MSBuildRelease "" "Clean" ["DotNetBuilds.sln"]
    |> Log "AppBuild-Output: "
)

Target "Build" (fun _ ->
  MSBuildRelease "" "Build" ["DotNetBuilds.sln"]
    |> Log "AppBuild-Output: "
)

Target "Rebuild" DoNothing

"Build" ==> "Rebuild"
"Clean" ==> "Rebuild"
"SetAssemblyVersion" ==> "Build"
"Clean" ?=> "Build"

RunTargetOrDefault "Rebuild"