using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cake.Watch {
	[Obsolete("Use ChangeWatcher instead", true)]
    internal class FileWatch {
        public void Watch(string pattern, Action<IEnumerable<string>> changedPath) {
            var settings = new WatchSettings { Pattern = pattern, Path = "./", Recursive = true };
            Watch(settings, changedPath);
        }

        public void Watch(WatchSettings settings, Action<IEnumerable<string>> changedPath) {
            var watcher = new FileSystemWatcher {
                Path = settings.Path,
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.CreationTime,
                Filter = settings.Pattern,
                EnableRaisingEvents = true,
                IncludeSubdirectories = settings.Recursive
            };

            var locker = new object();
            var currentSource = new CancellationTokenSource();
            var changedFiles = new List<string>();
            Func<CancellationTokenSource, bool> isCanceled = c => c == null || c.IsCancellationRequested;
            Action<FileSystemEventArgs> doWatch = (e) => {
                if (!isCanceled(currentSource)) {
                    lock (locker) {
                        if (!isCanceled(currentSource)) {
                            currentSource?.Cancel();
                            currentSource = new CancellationTokenSource();
                        }

                        changedFiles.Add(e.FullPath);
                    }
                }

                // Forces multiple file changes to run the task only once when made in rapid succession.
                // This prevents the create -> rename VS does from triggering 2 calls while still appearing to be instant.
                Task.Delay(TimeSpan.FromSeconds(0.25), currentSource.Token)
                    .ContinueWith(t => {
                        List<string> localChanged;
                        lock (locker) {
                            localChanged = new List<string>(changedFiles.Distinct());
                            changedFiles.Clear();
                        }

                        changedPath(localChanged);
                    },
                    currentSource.Token);
            };

            watcher.Changed += (s, e) => doWatch(e);
            watcher.Created += (s, e) => doWatch(e);
            watcher.Renamed += (s, e) => doWatch(e);

            while (Console.ReadLine() != "q") { }
        }
    }
}

