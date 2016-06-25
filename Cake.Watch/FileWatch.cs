using System;

namespace Cake.Watch {
	public class FileWatch {
		public void Watch(string filePath, Action<string> changedPath) {
			var settings = new WatchSettings { FilePattern = filePath, Path = "./", Recursive = false };
			Watch(settings, changedPath);
		}

		public void Watch(WatchSettings settings, Action<string> changedPath) {
			var watcher = new System.IO.FileSystemWatcher();
			watcher.Path = settings.Path;
			watcher.NotifyFilter = 
				System.IO.NotifyFilters.Size | System.IO.NotifyFilters.LastAccess | System.IO.NotifyFilters.LastWrite;
			watcher.Filter = settings.FilePattern;
			watcher.EnableRaisingEvents = true;
			watcher.IncludeSubdirectories = settings.Recursive;

			var lastRead = DateTime.MinValue;
			var locker = new Object();

			Action<System.IO.FileSystemEventArgs> doWatch = (e) => {
				var fullPath = e.FullPath;
				var lastWriteTime = System.IO.File.GetLastWriteTime(fullPath);
				if (lastWriteTime.Ticks != lastRead.Ticks && !fullPath.Contains("build.js")) {
					changedPath(fullPath);
				}
			};

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

