using NUnit.Framework;
using System;
using System.Linq;

namespace Cake.Watch.Tests {

	[TestFixture()]
	public class Test {

		[Test()]
		public void TestCase() {

			var fileWatch = new ChangeWatcher();
			var settings = new WatchSettings {
				Pattern = "*.cs",
				Path = "../../",
				Recursive = false
			};

			fileWatch.Watch(settings, (changed) => {
				changed.ToList().ForEach(x => Console.WriteLine(x.FullPath));
			});
		}
	}
}