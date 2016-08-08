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
        	changes.ToList().ForEach(Console.WriteLine);
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
        	changes.ToList().ForEach(Console.WriteLine);
        });
    });
```

## Filter change period by Diff 

```csharp
Task("Last-Change-500ms")
    .Does(() => {
        Watch("*.js", (changes) => {
        	Diff(500, () => {
	        	changes.ToList().ForEach(Console.WriteLine);
        	});
        });
    });
```

## License

- MIT