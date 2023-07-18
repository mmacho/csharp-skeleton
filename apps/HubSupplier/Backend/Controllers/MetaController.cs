using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;

namespace Aseme.Apps.HubSupplier.Backend.Controllers
{
    public class MetaController : ControllerBase
    {
        [HttpGet("/info")]
        public ActionResult<string> Info()
        {
            //var assembly = typeof(Startup).Assembly;
            Assembly assembly = Assembly.GetEntryAssembly();
            var lastUpdate = System.IO.File.GetLastWriteTime(assembly.Location);
            var version = FileVersionInfo.GetVersionInfo(assembly.Location).ProductVersion;

            return Ok($"Version: {version}, Last Updated: {lastUpdate}");
        }
    }
}