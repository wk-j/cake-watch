
#r "../Cake.Watch/bin/Debug/Cake.Watch.dll"

using Cake.Watch;

Task("Watch").Does(() => {

    Watch("*.js", (changes) => {
        var list = changes.ToList();
        list.ForEach(x => {
            Console.WriteLine("{2} {0} {1}", x.Status, x.Name, DateTime.Now);
        });
    });
});

var target = Argument("target", "Watch");
RunTarget(target);