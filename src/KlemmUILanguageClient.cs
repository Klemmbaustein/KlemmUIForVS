using Microsoft.VisualStudio.LanguageServer.Client;
using Microsoft.VisualStudio.Utilities;
using StreamJsonRpc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Threading;
using Newtonsoft.Json.Linq;
using Task = System.Threading.Tasks.Task;
using Microsoft.VisualStudio.LanguageServer.Protocol;
using System.ComponentModel.Composition;

namespace KlemmUI.LanguageServer
{
	[ContentType("kui")]
	[Export(typeof(ILanguageClient))]
	[RunOnContext(RunningContext.RunOnHost)]
	public class KlemmUILanguageClient : ILanguageClient, ILanguageClientCustomMessage2
	{
		public KlemmUILanguageClient()
		{
			Instance = this;
		}

		internal static KlemmUILanguageClient Instance
		{
			get;
			set;
		}

		internal JsonRpc Rpc
		{
			get;
			set;
		}

		public event AsyncEventHandler<EventArgs> StartAsync;
		public event AsyncEventHandler<EventArgs> StopAsync;

		public string Name => "KlemmUI LSP";

		public IEnumerable<string> ConfigurationSections
		{
			get
			{
				yield return "kui";
			}
		}

		public object InitializationOptions => null;

		public IEnumerable<string> FilesToWatch => null;

		public object MiddleLayer
		{
			get;
			set;
		}

		public object CustomMessageTarget => null;

		public bool ShowNotificationOnInitializeFailed => true;

		public async Task<Connection> ActivateAsync(CancellationToken token)
		{
			await Task.Yield();

			string executable = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Resources", "KlemmUILanguageServer.exe");
			
			Process process = new Process
			{
				StartInfo = new ProcessStartInfo
				{
					FileName = executable,
					WorkingDirectory = Path.GetDirectoryName(executable),
					RedirectStandardInput = true,
					RedirectStandardOutput = true,
					UseShellExecute = false,
					CreateNoWindow = true,
				}
			};

			if (process.Start())
			{
				return new Connection(process.StandardOutput.BaseStream, process.StandardInput.BaseStream);
			}
			return null;
		}

		public async Task OnLoadedAsync()
		{
			if (StartAsync != null)
			{
				await StartAsync.InvokeAsync(this, EventArgs.Empty);
			}
		}

		public async Task StopServerAsync()
		{
			if (StopAsync != null)
			{
				await StopAsync.InvokeAsync(this, EventArgs.Empty);
			}
		}

		public Task OnServerInitializedAsync()
		{
			return Task.CompletedTask;
		}

		public Task AttachForCustomMessageAsync(JsonRpc rpc)
		{
			this.Rpc = rpc;

			return Task.CompletedTask;
		}

		public Task<InitializationFailureContext> OnServerInitializeFailedAsync(ILanguageClientInitializationInfo initializationState)
		{
			string message = "KlemmUI language server initialization failed.";
			string exception = initializationState.InitializationException?.ToString() ?? string.Empty;
			message = $"{message}\n {exception}";

			var failureContext = new InitializationFailureContext()
			{
				FailureMessage = message,
			};

			return Task.FromResult(failureContext);
		}
	}
}
