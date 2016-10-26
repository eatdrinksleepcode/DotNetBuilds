#r @"FAKE/tools/FakeLib.dll"
open Fake
open Fake.AssemblyInfoFile

let sln = ["DotNetBuilds.sln"]

Target "SetAssemblyVersion" (fun _ ->
  CreateCSharpAssemblyInfo "ConsoleApp\Properties\AssemblyInfo.cs"
    [Attribute.Version "1.1"]
)

Target "Clean" (fun _ ->
  MSBuildRelease "" "Clean" sln
    |> Log "AppBuild-Output: "
)

Target "Build" (fun _ ->
  MSBuildRelease "" "Build" sln
    |> Log "AppBuild-Output: "
)

Target "Rebuild" DoNothing

"Build" ==> "Rebuild"
"Clean" ==> "Rebuild"
"SetAssemblyVersion" ==> "Build"
"Clean" ?=> "Build"

RunTargetOrDefault "Rebuild"