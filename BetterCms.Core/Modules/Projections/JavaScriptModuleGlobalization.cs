﻿using System;
using System.Web.Mvc;

using Common.Logging;

namespace BetterCms.Core.Modules.Projections
{
    /// <summary>
    /// Java script module resource initialization renderer.
    /// </summary>
    public class JavaScriptModuleGlobalization : IActionProjection
    {
        /// <summary>
        /// Current class logger.
        /// </summary>
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Resource name.
        /// </summary>
        private readonly string name;

        /// <summary>
        /// Function to retrieve resource in current culture.
        /// </summary>
        private readonly Func<string> resource;

        /// <summary>
        /// Resources module.
        /// </summary>
        private readonly JavaScriptModuleDescriptor javaScriptModule;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="JavaScriptModuleGlobalization" /> class.
        /// </summary>
        /// <param name="javaScriptModule">The java script module.</param>
        /// <param name="name">The name.</param>
        /// <param name="resource">A function to retrieve resource in current culture.</param>
        public JavaScriptModuleGlobalization(JavaScriptModuleDescriptor javaScriptModule, string name, Func<string> resource)
        {
            this.javaScriptModule = javaScriptModule;
            this.name = name;
            this.resource = resource;
        }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        public int Order { get; set; }

        /// <summary>
        /// Renders java script module resource initialization. 
        /// </summary>
        /// <param name="html">Html helper.</param>
        public void Render(HtmlHelper html)
        {
            try
            {
                var resourceObject = resource();
                if (resourceObject != null)
                {
                    string globalization = string.Format("{0}.globalization.{1} = '{2}';", javaScriptModule.FriendlyName, name, html.Encode(resourceObject));
                    html.ViewContext.Writer.WriteLine(globalization);
                }
                else
                {
                    Log.WarnFormat("Resource object not found to globalize {0}.{1} from resource {2}.", javaScriptModule, name, resource);
                }
            }
            catch (Exception ex)
            {
                Log.WarnFormat("Failed to render globalization for {0}.{1} from resource {2}.", ex, javaScriptModule, name, javaScriptModule);
            }
        }        
    }
}
