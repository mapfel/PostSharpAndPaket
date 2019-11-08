# Details about the issue with PostSharp and Paket

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
