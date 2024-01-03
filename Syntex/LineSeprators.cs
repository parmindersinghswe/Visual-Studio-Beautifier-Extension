using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Simply_Beautify_CSharp.Syntex
{
	internal class LineSeprators
	{
		private static Lazy<LineSeprators> _Instance => new Lazy<LineSeprators>(() => new LineSeprators());
		public static LineSeprators Instance { get { return _Instance.Value; } }

        public string RemoveUnwantedSemicolons(string codeText)
		{
			var codeBuilder = new StringBuilder(codeText);                           
			RemoveUnwantedSemicolons(ref codeBuilder);
			return codeBuilder.ToString();
		}
		public void RemoveUnwantedSemicolons(ref StringBuilder codeBuilder)
		{
			List<List<int>> indexesList = new List<List<int>>();
			List<int> indexes = new List<int>();
			bool doubleQuotes = false;
			bool multilineComments = false;
			bool singleLineComments = false;
			int lastSemicolonIndex = -1;
			for (int i = 0; i < codeBuilder.Length; i++)
			{
				if (singleLineComments)
				{
					if (codeBuilder[i] == '\n')
					{
						singleLineComments = false;
					}
					continue;
				}
				if (multilineComments)
				{
					if (codeBuilder[i] == '*' && (i + 1) < codeBuilder.Length && codeBuilder[i + 1] == '/')
					{
						multilineComments = false;
					}
					continue;
				}
				if (codeBuilder[i] == '"' && !singleLineComments && !multilineComments)
				{
					doubleQuotes = !doubleQuotes;
					continue;
				}
				else if (!doubleQuotes && codeBuilder[i] == '/' && (i + 1) < codeBuilder.Length)
				{
					if (codeBuilder[i + 1] == '*')
					{
						multilineComments = true;
					}
					else if (codeBuilder[i + 1] == '/')
					{
						singleLineComments = true;
					}
				}
				if (!doubleQuotes)
				{

					var currentChar = codeBuilder[i];
					if (currentChar == '\r' || currentChar == '\n' || currentChar == '\t' || currentChar == '\0')
					{
						continue;
					}
					if (currentChar == ' ')
					{
						if(indexes.Count > 0 && (lastSemicolonIndex + 1) == i)
						{
							indexes.Add(i);
						}
						continue; 
					}
					
					if (currentChar == ';')
					{
						indexes.Add(i);
						lastSemicolonIndex = i;
					}
					else
					{
						if (indexes.Count() > 1)
						{
							indexesList.Add(indexes.Skip(1).ToList());
							indexes = new List<int> ();
						}
						else
						{
							indexes.Clear();
						}
					}
				}
			}
			if(indexes.Count > 0)
			{
				indexesList.Add(indexes);
			}
			List<int> removeableIndexes = indexesList.SelectMany(indexes => indexes.Select(x=> x)).Distinct().OrderByDescending(y=> y).ToList();
			foreach(var indxs in removeableIndexes)
			{
				codeBuilder.Remove(indxs, 1);
			}
		}
	}
}
