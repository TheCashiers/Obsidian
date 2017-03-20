# Obsidian Canary branch
[![Build Status](https://travis-ci.org/ZA-PT/Obsidian.svg?branch=canary)](https://travis-ci.org/ZA-PT/Obsidian)

# Contributing Guide
1. Make sure these following tools are installed:
  - .NET Core 1.0
2. Run the following commands.

    ```bash
        cd ./src/Obsidian/
        dotnet restore
        dotnet --verbose build
        npm update
    ```
## Naming convention

### For C# code

References
- [Capitalization Conventions](https://msdn.microsoft.com/en-us/library/ms229043.aspx)
- [C# Coding Conventions (C# Programming Guide)](https://msdn.microsoft.com/en-us/library/ff926074.aspx)

``` C# 
using System;
using System.Threading.Tasks;

//For namespaces, use Pascal casing.
namespace NamingConversions
{
    // For classes, enums, use Pascal casing.
    public class SampleClass
    {
        //For constanrs, use Pascal casing.
        public const int Count = 0;

        //For static menbers, use Pascal casing.
        //For public fields, use Pascak casing.
        public static string Name = "Obsidian";
        
        //For private fieldss, use camel case with an unserscore.
        private string _name;
        
        //For properties, use Pascal casing.
        public string StatusData { get; set; }
        
        //For methods, use Pascal casing.
        //For parameters, use camel casing.
        public void MyMethod(int myParameter)
        {
        }

        //For asynchronous methods, use Pascal casing with subfix "Async".
        public async Task ProcessAsync()
        {
        }

        //For events, use Pascal casing.
        public event EventHandler MyEvent;
    }

    //For interfaces, use Pascal casing with prefix "I".
    public interface ISample
    {
    }
}
```
### For Typescript