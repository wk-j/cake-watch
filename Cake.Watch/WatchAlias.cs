using System;
using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.Watch {
	public static class WatchAlias {

		[CakeMethodAlias]
		public static void Watch(this ICakeContext context, string filePath, Action<string> fileChanged) {
			new FileWatch().Watch(filePath, fileChanged);
		}
		
		[CakeMethodAlias]
		public static void Watch(this ICakeContext context, WatchSettings settings, Action<string> fileChanged) {
			new FileWatch().Watch(settings, fileChanged);
		}
	}
}

