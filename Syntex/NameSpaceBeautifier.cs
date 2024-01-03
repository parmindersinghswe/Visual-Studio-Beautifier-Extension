using System.Linq;
using System.Collections.Generic;


namespace Simply_Beautify_CSharp.Syntex
{
	internal class NameSpaceBeautifier
	{
		private static Lazy<NameSpaceBeautifier> _Instance => new Lazy<NameSpaceBeautifier>(() => new NameSpaceBeautifier());
		public static NameSpaceBeautifier Instance { get { return _Instance.Value; } }
		
		public string Sort(string codeText)
		{
			List<string> codeLines = codeText.ToString().Split('\n').ToList();
			List<string> namespaceLines = new List<string>();
			foreach(var line in codeLines)
			{
				string trimmedLine = line.Trim();
				if(!trimmedLine.StartsWith("using") && !string.IsNullOrWhiteSpace(trimmedLine))
				{
					break;
				}
				namespaceLines.Add(line);
			}
			int nameSpacesLinesCount = namespaceLines.Count;
			namespaceLines = namespaceLines.SelectMany(x => x.Split(';').Where(y=> !string.IsNullOrWhiteSpace(y) && y.Trim().StartsWith("using")).Select(y => $"{y};")).ToList();
			codeLines.RemoveRange(0, namespaceLines.Count);
			namespaceLines.OrderByDescending(x => x.Length).ToList().ForEach(x =>
			{
				codeLines.Insert(0, x);
			});
			return string.Join("\n", codeLines);
		}
	}
}
