using System;
using System.Linq;

namespace Cake.Watch.Console {
	class MainClass {
		public static void Main(string[] args) {
			var fileWatch = new ChangeWatcher();
			var settings = new WatchSettings {
				Pattern = "*.cs",
				Path = "/Users/wk/Source/github/cake-addin/Cake.Watch",
				Recursive = true
			};

			fileWatch.Watch(settings, (changed) => {
				foreach (var change in changed){
					System.Console.WriteLine(change.Status);
					System.Console.WriteLine(change.Name);
					System.Console.WriteLine(change.FullPath);
				}
			});

			while (System.Console.ReadLine() != "q") {}

		}
	}
}
