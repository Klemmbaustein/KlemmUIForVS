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
		internal static ContentTypeDefinition FooContentTypeDefinition;


		[Export]
		[FileExtension(".kui")]
		[ContentType("kui")]
		internal static FileExtensionToContentTypeDefinition FooFileExtensionDefinition;
	}
#pragma warning restore 649
}
