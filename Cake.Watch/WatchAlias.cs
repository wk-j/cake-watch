using System;
using System.Collections.Generic;
using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.Watch {
	public static class WatchAlias {

		[CakeMethodAlias]
		public static void Watch(this ICakeContext context, string filePath, Action<IEnumerable<string>> fileChanged) {
			new FileWatch().Watch(filePath, fileChanged);
		}
		
		[CakeMethodAlias]
		public static void Watch(this ICakeContext context, WatchSettings settings, Action<IEnumerable<string>> fileChanged) {
			new FileWatch().Watch(settings, fileChanged);
		}
	}
}

