# Details about the issue with PostSharp and Paket

## Table of Contents

- [Prerequisites](#Prerequisites)  
  initial steps to prepare a demo project for this issue

- [Problem](#Problem)  
  describes the issue in detail

- [Conclusion](#Conclusion)  
  summary of the issue and conjecture of the root cause

- [Workarounds and tracking the solving efforts](#Workarounds-and-tracking-the-solving-efforts)  
  audits the solutions hints and efforts to solve the issue

## Prerequisites

1. Create a new project with an empty class named `ClassUsingPostSharp`

1. Add via **NuGet** the package `PostSharp.Patterns.Common`

1. Add a new integer property named `Counter` and put the **PostSharp** `RangeAttribute` on it

    ```cs
    [Range(1, 7)]
    public int Counter { get; set; }
    ```

1. Add via **NuGet** the package `xUnit`

1. Add a unit-test for the functionality of **PostSharp**  
  by verifying whether an exception is thrown when the `Counter` property is set to a value outside the given range

1. Run the unit-test to check that everything is green

1. Add **Paket** in Magic Mode

1. Run `paket convert-from-nuget`

1. Run the unit-test to check that everything is still green

    - also after closing and re-opening **Visual Studio** this is still the case
    - also after _Clean Solution_, _Rebuild Solution_

1. put all files under source control  

    - use a reasonable `.gitignore` for **Visual Studio** projects
    - that was done by each step in this repo here)

## Problem

1. Verify with `git status` that nothing was forgotten

1. Run `git clean -dxf` to cleanup all intermediate outputs somewhere in the developing tree

1. Run the unit-test again and now it is red!

1. Add via **NuGet** the package `PostSharp.Patterns.Common`

1. Run the unit-test again and now it is back green

1. With `git status` you see that only the file `src/PostSharpAndPaket/PostSharpAndPaket.csproj` was modified

1. With `git reset --hard` we take back the changes to ensure a clean workspace (from git perspective)

1. Run the unit-test again and this time it is still green

1. Again we get back the issue by running `git clean -dxf`

## Conclusion

- This means, that something under the hood in the `obj`, `bin`, .. folder is necessary for **PostSharp**.
- The same issue a co-worker and the CI server will have.

> Who can help?

## Workarounds and tracking the solving efforts

### Easier access to build and run

#### Build via CLI

- open a **Developer Command Prompt for Visual Studio**

- navigate to project root folder

- run build with msbuild

  ```prompt
  .\src\PostSharpAndPaket\PostSharpAndPaket.sln
  ```

#### xUnit Console Runner

- with commit `56444b` ("add xUnit console runner") the **xUnit** Console Runner was included

- so from root we can now run the unit tests via

  ```prompt
  .\packages\xunit.runner.console\tools\net472\xunit.console.exe .\src\PostSharpAndPaket\bin\Debug\PostSharpAndPaket.dll
  ```

#### Paket commands

- (re-) install packages after changing `paket.dependencies` file

  ```prompt
  .paket\paket.exe install
  ```

- restoring `package` folder after cleanup DevTree or switching branches

  ```prompt
  .paket\paket.exe restore
  ```

### Hint from Daniel Balas ([comment: 103888639_58799413](https://stackoverflow.com/questions/58799413/using-postsharp-and-paket-together-ignores-under-some-circumstances-the-weaving#comment103888639_58799413))

- **Paket** throws the warning `Could not detect any platforms from 'unzipper' in '...\packages\PostSharp\build\unzipper\SharpCompress.dll', please tell the package authors`  

- to check which versions of **PostSharp** have this issue the branch `dev/play-with-postsharp-versions` was created (also pushed to remote here)

- indeed the version `5.0.55` doesn't throw this warning

- all later versions (the 6er line) have that issue
