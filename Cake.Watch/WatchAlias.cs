using System;
using System.Collections.Generic;
using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.Watch {
	public static class WatchAlias {

		[CakeMethodAlias]
		public static void Watch(this ICakeContext context, string pattern, Action<IEnumerable<FileChange>> fileChanged) {
			new ChangeWatcher().Watch(pattern, fileChanged);

			while (Console.ReadLine() != "q") {}
		}
		
		[CakeMethodAlias]
		public static void Watch(this ICakeContext context, WatchSettings settings, Action<IEnumerable<FileChange>> fileChanged) {
			new ChangeWatcher().Watch(settings, fileChanged);

			while (Console.ReadLine() != "q") {}
		}
	}
}

