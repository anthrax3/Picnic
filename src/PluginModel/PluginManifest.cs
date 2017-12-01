using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Picnic.PluginModel
{
    public static class PluginManifest
    {
        /// <summary>
        /// Gets a list of plugins
        /// </summary>
        public static IList<IPluginDescriptor> GetPlugins()
        {
            var allAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            var allPluginTypes = allAssemblies.SelectMany(x => x.GetTypes().Where(y => y.GetInterfaces().Contains(typeof(IPluginDescriptor))));

            return allPluginTypes.Select(x => Activator.CreateInstance(x) as IPluginDescriptor).ToList();
        }
    }
}