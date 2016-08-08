using System;
using System.Collections.Generic;
using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.Watch {
	public static class WatchAlias {

		static DateTime _lastUpdate = DateTime.Now;

		[CakeMethodAlias]
		public static void Watch(this ICakeContext context, string filePath, Action<IEnumerable<string>> fileChanged) {
			new FileWatch().Watch(filePath, fileChanged);
		}
		
		[CakeMethodAlias]
		public static void Watch(this ICakeContext context, WatchSettings settings, Action<IEnumerable<string>> fileChanged) {
			new FileWatch().Watch(settings, fileChanged);
		}

		[CakeMethodAlias]
		public static void Diff(this ICakeContext context, int diff, Action change) {
			var ms = (DateTime.Now - _lastUpdate).TotalMilliseconds;
			if (ms > diff) {
				_lastUpdate = DateTime.Now;
				change();
			}
		}
	}
}

