#addin "nuget:?package=Cake.SquareLogo"

var target = Argument("target", "default");
var npi = EnvironmentVariable("npi");

Task("Create-Logo").Does(() => {
    var settings = new LogoSettings { Background = "Black" };
    CreateLogo("W", "Assets/logo.png", settings);
});

Task("Wk").Does(() => {
    var settings = new LogoSettings { Background = "Black" };
    CreateLogo("wk", "Assets/wk.png", settings);
});

Task("Publish-Nuget")
    .IsDependentOn("Create-Nuget-Package")
    .Description("Push nuget")
    .Does(() => {
        var nupkg = new DirectoryInfo("./nuget").GetFiles("*.nupkg").LastOrDefault();
        var package = nupkg.FullName;
        NuGetPush(package, new NuGetPushSettings {
            Source = "https://www.nuget.org/api/v2/package",
            ApiKey = npi
        });
    });

Task("Build-Release")
    .Description("Build")
    .Does(() => {
        DotNetBuild("./Cake.Watch.sln", settings =>
            settings.SetConfiguration("Release")
            //.SetVerbosity(Core.Diagnostics.Verbosity.Minimal)
            .WithTarget("Build")
            .WithProperty("TreatWarningsAsErrors","true"));
    });

// https://github.com/cake-addin/cake-square-logo/raw/master/Screen/alfresco.png

Task("Create-Nuget-Package")
    .Description("Create pack")
    .IsDependentOn("Build-Release")
    .Does(() => {
        CleanDirectory("./nuget");
        var dll = "./Cake.Watch/bin/Release/Cake.Watch.dll";
        var full = new System.IO.FileInfo(dll).FullName;
        var version = ParseAssemblyInfo("./Cake.Watch/Properties/AssemblyInfo.cs").AssemblyVersion;
        var settings   = new NuGetPackSettings {
                                        //ToolPath                = "./tools/nuget.exe",
                                        Id                      = "Cake.Watch",
                                        Version                 = version,
                                        Title                   = "Cake.Watch",
                                        Authors                 = new[] {"wk"},
                                        Owners                  = new[] {"wk"},
                                        Description             = "Cake.Watch",
                                        //NoDefaultExcludes       = true,
                                        Summary                 = "Watch file change",
                                        ProjectUrl              = new Uri("https://github.com/cake-addin/cake-watch"),
                                        IconUrl                 = new Uri("https://raw.githubusercontent.com/cake-addin/cake-watch/master/Assets/logo.png"),
                                        LicenseUrl              = new Uri("https://github.com/cake-addin/cake-watch"),
                                        Copyright               = "MIT",
                                        ReleaseNotes            = new [] { "New version"},
                                        Tags                    = new [] {"Cake", "Watch" },
                                        RequireLicenseAcceptance= false,
                                        Symbols                 = false,
                                        NoPackageAnalysis       = true,
                                        Files                   = new [] {
                                                                             new NuSpecContent { Source = full, Target = "bin/net45" }
                                                                          },
                                        BasePath                = "./",
                                        OutputDirectory         = "./nuget"
                                    };
        NuGetPack(settings);
    });

RunTarget(target);