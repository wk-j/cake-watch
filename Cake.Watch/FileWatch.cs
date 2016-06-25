using System;

namespace Cake.Watch {
	public class FileWatch {
		public void Watch(string pattern, Action<string> changedPath) {
			var settings = new WatchSettings { Pattern = pattern, Path = "./", Recursive = true };
			Watch(settings, changedPath);
		}

		public void Watch(WatchSettings settings, Action<string> changedPath) {
			var watcher = new System.IO.FileSystemWatcher();
			watcher.Path = settings.Path;
			watcher.NotifyFilter =
				       System.IO.NotifyFilters.Size; // | System.IO.NotifyFilters.LastAccess | System.IO.NotifyFilters.LastWrite;
			watcher.Filter = settings.Pattern;
			watcher.EnableRaisingEvents = true;
			watcher.IncludeSubdirectories = settings.Recursive;

			var lastWatch = DateTime.MinValue;

			Action<System.IO.FileSystemEventArgs> doWatch = (e) => {
				var fullPath = e.FullPath;
				var lastWriteTime = System.IO.File.GetLastWriteTime(fullPath);
				if (lastWriteTime.Ticks != lastWatch.Ticks) {
					changedPath(fullPath);
				}
				lastWatch = lastWriteTime;
			};

			var locker = new Object();

			watcher.Changed += (s, e) => {
				lock (locker)
					doWatch(e);
			};

			watcher.Created += (s, e) => {
				lock (locker)
					doWatch(e);
			};

			while (System.Console.ReadLine() != "q") { }
		}
	}
}

