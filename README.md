## Cake.Watch

Cake addin to watch files changed.

## Install

```
#addin "nuget:?package=Cake.Watch"
```

## Watch current path

```csharp
Task("watch-js")
    .Does(() => {
        Watch("*.js", (changed) => {
            Console.WriteLine(changed);
        });
    });
```


## Watch specific path

```csharp
Task("watch-path")
    .Does(() => {
        var settings = new WatchSettings {
            Recursive = true,
            Path = "./src",
            Pattern = "*.js"
        };
        Watch(settings, (changed) => {
            Console.WriteLine(changed);
        });
    });
```

## License

- MIT