# VsProjectSetupPlugin

This plugin is intended to help ensure that Visual Studio projects follow some standards.

All projects should:

* Have `Warnings as Errors` enabled
* Have the StyleCop.MsBuild nuget installed
* Have the StyleCop warnings as errors setting enabled

This plugin is intended to work with:

* Visual Studio 2017
* Old style .vsproj files
* New style .vsproj files