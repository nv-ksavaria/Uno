# Building and Debugging Uno.UI

## Building Uno.UI

Using Visual Studio 2017 (15.5 or later):
* Open the [Uno.UI.sln](/src/Uno.UI.sln)
* Select the Uno.UI project
* Build

Inside Visual Studio, the number of platforms is restricted to limit the compilation time.

## Microsoft Source Link support
Uno.UI supports [SourceLink](https://github.com/dotnet/sourcelink/) and it now possible to
step into Uno.UI without downloading the repository.

Make sure **Enable source link support** check box is checked in **Tools** / **Options**
/ **Debugging** / **General** properties page. 

## Debugging Uno.UI

To debug Uno.UI inside of an existing project, the simplest way (until Microsoft provides a better way to avoid overriding the global cache) is to :
* Install a published `Uno.UI` package in a project you want to debug, taking note of the version number.
* Rename [crosstargeting_override.props.sample](/src/crosstargeting_override.props.sample) to `crosstargeting_override.props`
* Uncomment the `UnoNugetOverrideVersion` node
* Change the version number to the package you installed at the first step
* Build your solution.

> Note: This overrides your local nuget cache, making the cache inconstent with the binaries you just built. 
To ensure that the file you have in your cache a correct, either clear the cache, or observe the properties of the `Uno.UI.dll` file, where the
product version should contain a git CommitID.

Once Uno.UI built, open the files you want to debug inside the solution running the application you need to debug, and set breakpoints there.

## Building Uno.UI for macOS using Visual Studio for Mac

Building Uno.UI for the macOS platform using vs4mac requires Visual Studio for mac 7.7 preview or later.

A few steps to be able to build:
- The `xamarinmac20` Target Framework must be the first in the `TargetFrameworks` list of the `Uno.UI`, `Uno`, `Uno.Foundation` and `Uno.Xaml` projects. VS4Mac only builds the first Target Framework.
- In both `Uno` and `Uno.UI` the ItemGroups containing references to `Xamarin.Android.Support.v4` and `Xamarin.Android.Support.v7.AppCompat` must be commented out.
Failing to remove those groups will make the nuget restore fail, because VS4Mac does not support conditional package references.
- In `Uno.UI`, comment the project reference to the `Uno.UI.BindingHelper` project, because VS4Mac does not support conditional project references.
- Disable both nuget restore features in Visual Studio for Mac configuration

To build and run:
- In a shell in the `src/Uno.UI` folder, run `msbuild /r`. This will make the nuget restore work properly.
- Once done, in VS4Mac, run the `SampleApp.macOS` project, which will build the dependencies and the app itself.
