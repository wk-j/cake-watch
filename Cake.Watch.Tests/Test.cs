using NUnit.Framework;
using System;
namespace Cake.Watch.Tests {

	[TestFixture()]
	public class Test {

		[Test()]
		public void TestCase() {

			var fileWatch = new FileWatch();
			var settings = new WatchSettings {
				Pattern = "*.cs",
				Path = "../../",
				Recursive = false
			};

			fileWatch.Watch(settings, (changed) => {
				Console.WriteLine(changed);
			});
		}
	}
}