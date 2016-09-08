using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Timers;

namespace Cake.Watch {

	// Stolen from https://github.com/fsharp/FAKE/blob/b7630d6e9bdb7caf7640ac38ca4166d9d7717d59/src/app/FakeLib/ChangeWatcher.fs

	public enum FileStatus { Deleted, Created, Changed }

	public class FileChange {
		public string FullPath { set; get; }
		public string Name { set; get; }
		public FileStatus Status { set; get; }
	}

	public class ChangeWatcher : IDisposable {

		private bool _runningHandler = false;
		private readonly Timer _timer = new Timer(50);
		private readonly List<FileChange> _unNotifiedChanges = new List<FileChange>();
		private FileSystemWatcher _watcher;

		private void AcumChanges(FileChange fileChange) {
			if (!_runningHandler) {
				lock(_unNotifiedChanges) {
					_unNotifiedChanges.Add(fileChange);
					_timer.Start();
				}
			}
		}

		private void HandleWatcherEvent(FileStatus status, FileSystemEventArgs e, Action<FileChange> onChange) {
			onChange(new FileChange { FullPath = e.FullPath, Name = e.Name, Status = status });
		}

		public void Start(WatchSettings options) {
			var full = new DirectoryInfo(options.Path).FullName;
			_watcher = new FileSystemWatcher(full, options.Pattern);
			_watcher.EnableRaisingEvents = true;
			_watcher.IncludeSubdirectories = options.Recursive;

			_watcher.Changed += (sender, e) => {
				HandleWatcherEvent(FileStatus.Changed, e, AcumChanges);
			};
			_watcher.Created+= (sender, e) => { 
				HandleWatcherEvent(FileStatus.Created, e, AcumChanges);
			};
			_watcher.Deleted += (sender, e) => {
				HandleWatcherEvent(FileStatus.Deleted, e, AcumChanges);
			};
			_watcher.Renamed += (sender, e) => {
				AcumChanges(new FileChange { FullPath = e.FullPath, Name = e.Name, Status = FileStatus.Deleted });
				AcumChanges(new FileChange { FullPath = e.FullPath, Name = e.Name, Status = FileStatus.Created });
			};

		}

		public void Watch(WatchSettings options, Action<IEnumerable<FileChange>> onChange) {
			_timer.AutoReset = false;
			_timer.Elapsed += (sender, e) => { 
				lock(_unNotifiedChanges) {
					if (_unNotifiedChanges.Any()) {
						var changes = _unNotifiedChanges
							.GroupBy(x => x.FullPath)
							.Select(x => x.OrderBy(k => k.Status).FirstOrDefault()).ToList();

						_unNotifiedChanges.Clear();

						try {
							_runningHandler = true;
							onChange(changes);
						} finally {
							_runningHandler = false;
						}
					}
				}
			};
			Start(options);
		}

		public void Watch(string pattern, Action<IEnumerable<FileChange>> onChange) {
			Watch(new WatchSettings { Path = "./", Pattern = pattern, Recursive = false }, onChange);
		}

		public void Dispose() {
			_watcher.EnableRaisingEvents = false;
			_watcher.Dispose();
			_timer.Dispose();
		}
	}
}
