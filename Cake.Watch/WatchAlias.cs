using System;
using System.Collections.Generic;
using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.Watch {
	/// <summary>
	/// Contains functionality for watching file changes.
	/// </summary>
	[CakeAliasCategory("Watch")]
	public static class WatchAlias {

		/// <summary>
		/// Watch files in current directory.
		/// </summary>
		/// <example>
		/// Watch("*.html", changes => {
		///     changes.ToList().ForEach(change => {
		///         Console.WriteLine(change.FullPath);        
		///     });
		/// });
		/// </example>
		/// <param name="context"></param>
		/// <param name="pattern"></param>
		/// <param name="fileChanged"></param>
		[CakeMethodAlias]
		public static void Watch(this ICakeContext context, string pattern, Action<IEnumerable<FileChange>> fileChanged) {
			new ChangeWatcher().Watch(pattern, fileChanged);

			while (Console.ReadLine() != "q") {}
		}


		/// <summary>
		/// Watch files that is specified by settings.
		/// </summary>
		/// <example>
		/// var settings = new WatchSettings { Recursive = true, Path = "src", Pattern = "*.html" };
		/// Watch(settings , (changes) => {
		///     var list = changes.ToList();
		///     list.ForEach(change => {
		///        Console.WriteLine(change.FullName);
		///     });
		/// })
		/// </example>
		/// <param name="context"></param>
		/// <param name="settings"></param>
		/// <param name="fileChanged"></param>
		[CakeMethodAlias]
		public static void Watch(this ICakeContext context, WatchSettings settings, Action<IEnumerable<FileChange>> fileChanged) {
			new ChangeWatcher().Watch(settings, fileChanged);

			while (Console.ReadLine() != "q") {}
		}
	}
}

