using System;
namespace Cake.Watch {
	public class WatchSettings {
		public string Pattern { set; get; } = "*.*";
		public string Path { set; get; } = "./";
		public bool Recursive { set; get; } 
	}
}

