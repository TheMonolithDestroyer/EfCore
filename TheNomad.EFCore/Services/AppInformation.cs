using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Versioning;
using System;

namespace TheNomad.EFCore.Api.Services
{
    public class AppInformation
    {
        public AppInformation()
        {

        }

        public IEnumerable<Tuple<string, string>> GetAssembliesInfo()
        {
            var efCore = typeof(DbContext).GetTypeInfo().Assembly.GetName();
            yield return new Tuple<string, string>(efCore.Name, efCore.Version.ToString());
            var aspNetCore = typeof(WebHostBuilder).GetTypeInfo().Assembly.GetName();
            yield return new Tuple<string, string>(aspNetCore.Name, aspNetCore.Version.ToString());
            var netCore = typeof(Program).GetTypeInfo().Assembly;
            yield return new Tuple<string, string>("Targeted .NET Core", netCore.GetCustomAttribute<TargetFrameworkAttribute>().FrameworkName);
        }
    }
}
