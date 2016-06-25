using System;
namespace Cake.Watch {
	public class WatchSettings {
		public string FilePattern { set; get; } = "*.*";
		public string Path { set; get; } = "./";
		public bool Recursive { set; get; } 
	}
}

