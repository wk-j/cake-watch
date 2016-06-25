#addin "nuget:?package=Cake.Watch"

var target = Argument("target", "default");

Task("default")
    .Does(() => {
            Console.WriteLine("use './build.sh --target watch-path'");
    });

Task("watch-path")
    .Does(() => {
        var settings = new WatchSettings {
            Recursive = true,
            Path = "./src",
            Pattern = "*.*"
        };
        Watch(settings, (changed) => {
            Console.WriteLine(changed);
        });
    });

Task("watch-js")
    .Does(() => {
        Watch("*.js", (changed) => {
            Console.WriteLine(changed);
        });
    });

RunTarget(target);