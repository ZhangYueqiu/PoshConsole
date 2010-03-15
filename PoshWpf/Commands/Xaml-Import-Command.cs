﻿using System;
using System.Management.Automation;
using System.Text;
using System.Xml;
using System.Linq;

namespace PoshWpf.Commands
{
#if CLR4
	[Cmdlet(VerbsData.Import, "Xaml", DefaultParameterSetName = ParamSetPath)]
   public class XamlImportCommand : HuddledContentProviderBaseCommand
   {
      protected override void ProcessRecord()
      {
			// pre-call base to get ProviderPaths populated
			base.ProcessRecord();

      	foreach (var path in ProviderPaths)
      	{
      		using (var reader = TryGetReader(path))
      		{
      			var builder = new StringBuilder();
					// read everything into a list of ... stuff
					try
					{
						var lines = reader.Read(0);
						foreach (var line in lines)
						{
							builder.Append(line.ToString());
						}
					} 
					catch(Exception ex)
					{
						WriteError(new ErrorRecord(ex, "CantReadContent", ErrorCategory.ReadError, path));
					}

					// Any errors here will just propagate out and crash.
					// I'm ok with that.
					WriteObject(System.Xaml.XamlServices.Parse(builder.ToString()));
      		}
      	}
      }
   }
#endif
}

