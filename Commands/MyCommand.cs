using System.Linq;
using Simply_Beautify_CSharp.Syntex;

namespace Simply_Beautify_CSharp
{
	[Command(PackageIds.MyCommand)]
	internal sealed class MyCommand : BaseCommand<MyCommand>
	{
        public MyCommand() { }
		protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
		{
			var activeDocument = await VS.Documents.GetActiveDocumentViewAsync();
			var selection = activeDocument?.TextView.Selection.SelectedSpans.FirstOrDefault();      
			if (selection != null)
			{
				var textBuffer = activeDocument.TextBuffer;
				if (textBuffer != null && activeDocument.Document != null && !string.IsNullOrEmpty(activeDocument.Document.FilePath) && activeDocument.Document.FilePath.EndsWith(".cs"))
				{
					textBuffer.Replace(new Microsoft.VisualStudio.Text.Span(0, textBuffer.CurrentSnapshot.Length), GetBeautifulCode(activeDocument.TextBuffer.CurrentSnapshot.GetText().ToString()));
				}
			}
			//await VS.MessageBox.ShowWarningAsync("Simply_Beautify_CSharp", $"Current Document: {activeDocument.Document.FilePath}");
		}
		/// <summary>
		/// To Beautify And Replace The CSharp Code
		/// </summary>
		/// <param name="codeText">Text Of Current Document</param>
		/// <returns></returns>
		private string GetBeautifulCode(string codeText)
		{
			codeText = LineSeprators.Instance.RemoveUnwantedSemicolons(codeText);
			codeText = NameSpaceBeautifier.Instance.Sort(codeText);
			return codeText;
		}
	}
}
