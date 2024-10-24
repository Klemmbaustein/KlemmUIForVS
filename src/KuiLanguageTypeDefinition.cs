using Microsoft.VisualStudio.LanguageServer.Client;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace KlemmUI.LanguageServer
{
#pragma warning disable 649
	public class KuiLanguageTypeDefinition
	{
		[Export]
		[Name("kui")]
		[BaseDefinition(CodeRemoteContentDefinition.CodeRemoteContentTypeName)]
		internal static ContentTypeDefinition KuiContentTypeDefinition;


		[Export]
		[FileExtension(".kui")]
		[ContentType("kui")]
		internal static FileExtensionToContentTypeDefinition KuiFileExtensionDefinition;
	}
#pragma warning restore 649
}
