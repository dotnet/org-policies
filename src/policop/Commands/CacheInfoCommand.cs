﻿using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Csv;

using Mono.Options;

namespace Microsoft.DotnetOrg.PolicyCop.Commands
{
    internal sealed class CacheInfoCommand : ToolCommand
    {
        public override string Name => "cache-info";

        public override string Description => "Displays information about the cached data";

        public override void AddOptions(OptionSet options)
        {
        }

        public override Task ExecuteAsync()
        {
            var orgCaches = CacheManager.GetOrgCaches().ToArray();
            var document = new CsvDocument("org", "date", "path");

            using (var writer = document.Append())
            {
                foreach (var orgCache in orgCaches)
                {
                    writer.Write(Path.GetFileNameWithoutExtension(orgCache.Name));
                    writer.Write(orgCache.LastWriteTime.ToShortDateString());
                    writer.Write(orgCache.FullName);
                    writer.WriteLine();
                }
            }

            document.PrintToConsole();

            return Task.CompletedTask;
        }
    }
}
