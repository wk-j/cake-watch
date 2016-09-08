## Cake.Watch

Cake addin to watch files changed.

## Install

```
#addin "nuget:?package=Cake.Watch"
```

## Watch current path

```csharp
Task("Watch-Only-Js")
    .Does(() => {
        Watch("*.js", (changes) => {
        	changes.ToList().ForEach(change => {
                Console.WriteLine(change.Status);
                Console.WriteLine(change.Name);
                Console.WriteLine(change.FullPath);
            });
        });
    });
```

## Watch specific path

```csharp
Task("Watch-With-Settings")
    .Does(() => {
        var settings = new WatchSettings {
            Recursive = true,
            Path = "./src",
            Pattern = "*.js"
        };
        Watch(settings, (changes) => {
            changes.ToList().ForEach(change => {
                Console.WriteLine(change.Status);
                Console.WriteLine(change.Name);
                Console.WriteLine(change.FullPath);
            });
        });
    });
```