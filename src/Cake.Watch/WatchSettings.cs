using System;
namespace Cake.Watch {
	/// <summary>
	/// Specifies a set of values that are used to watch file changes.
	/// </summary>
	public class WatchSettings {
        /// <summary>
        /// Gets or sets string to match against the names of files in path.
        /// </summary>
		public string Pattern { set; get; } = "*.*";
        /// <summary>
        /// Gets or sets root directory to watch file changes.
        /// </summary>
		public string Path { set; get; } = "./";
        /// <summary>
        /// Gets or sets whether the watcher should watch all subdirectories or only the current directory.
        /// </summary>
		public bool Recursive { set; get; } 
	}
}

