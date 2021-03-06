﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Autofac;

using BetterCms.Core.Modules;
using BetterCms.Core.Modules.Projections;
using BetterCms.Module.MediaManager.Content.Resources;
using BetterCms.Module.MediaManager.Registration;
using BetterCms.Module.MediaManager.Services;

namespace BetterCms.Module.MediaManager
{
    /// <summary>
    /// Pages module descriptor.
    /// </summary>
    public class MediaManagerModuleDescriptor : ModuleDescriptor
    {
        /// <summary>
        /// The module name.
        /// </summary>
        internal const string ModuleName = "media";

        /// <summary>
        /// The media java script module descriptor.
        /// </summary>
        private readonly MediaManagerJavaScriptModuleDescriptor mediaJavaScriptModuleDescriptor;

        /// <summary>
        /// The media upload module descriptor.
        /// </summary>
        private readonly MediaUploadJavaScriptModuleDescriptor mediaUploadModuleDescriptor;

        /// <summary>
        /// The image editor module descriptor.
        /// </summary>
        private readonly ImageEditorJavaScriptModuleDescriptor imageEditorModuleDescriptor;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaManagerModuleDescriptor" /> class.
        /// </summary>
        public MediaManagerModuleDescriptor()
        {
            mediaJavaScriptModuleDescriptor = new MediaManagerJavaScriptModuleDescriptor(this);
            mediaUploadModuleDescriptor = new MediaUploadJavaScriptModuleDescriptor(this);
            imageEditorModuleDescriptor = new ImageEditorJavaScriptModuleDescriptor(this);
        }

        /// <summary>
        /// Gets the name of module.
        /// </summary>
        /// <value>
        /// The name of pages module.
        /// </value>
        public override string Name
        {
            get
            {
                return ModuleName;
            }
        }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The module description.
        /// </value>
        public override string Description
        {
            get
            {
                return "A media manager module for BetterCMS.";
            }
        }

        /// <summary>
        /// Registers module types.
        /// </summary>
        /// <param name="context">The area registration context.</param>
        /// <param name="containerBuilder">The container builder.</param>
        /// <param name="configuration">The configuration.</param>
        public override void RegisterModuleTypes(ModuleRegistrationContext context, ContainerBuilder containerBuilder, ICmsConfiguration configuration)
        {
            containerBuilder.RegisterType<DefaultMediaFileService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            containerBuilder.RegisterType<DefaultMediaImageService>().AsImplementedInterfaces().InstancePerLifetimeScope();
        }

        /// <summary>
        /// Registers the style sheet files.
        /// </summary>
        /// <param name="containerBuilder">The container builder.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>Enumerator of known module style sheet files.</returns>
        public override IEnumerable<string> RegisterStyleSheetFiles(ContainerBuilder containerBuilder, ICmsConfiguration configuration)
        {
            return new[]
                {
                    "/file/bcms-media/Content/Css/bcms.media.css",
                    "/file/bcms-media/Content/Css/jquery.Jcrop.css"
                };
        }

        /// <summary>
        /// Gets known client side modules in page module.
        /// </summary>
        /// <param name="containerBuilder">The container builder.</param>
        /// <param name="configuration">The CMS configuration.</param>
        /// <returns>List of known client side modules in page module.</returns>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed. Suppression is OK here.")]
        public override IEnumerable<JavaScriptModuleDescriptor> RegisterJavaScriptModules(ContainerBuilder containerBuilder, ICmsConfiguration configuration)
        {
            return new[]
                {
                    mediaJavaScriptModuleDescriptor,
                    mediaUploadModuleDescriptor,
                    imageEditorModuleDescriptor,
                    new JavaScriptModuleDescriptor(this, "html5Upload", "/file/bcms-media/scripts/html5Upload"),
                    new JavaScriptModuleDescriptor(this, "jquery.Jcrop", "/file/bcms-media/scripts/jquery.Jcrop"),
                    new JavaScriptModuleDescriptor(this, "bcms.contextMenu", "/file/bcms-media/scripts/bcms.contextMenu")
                };
        }

        /// <summary>
        /// Registers the site settings projections.
        /// </summary>
        /// <param name="containerBuilder">The container builder.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>List of page action projections.</returns>
        public override IEnumerable<IPageActionProjection> RegisterSiteSettingsProjections(ContainerBuilder containerBuilder, ICmsConfiguration configuration)
        {
            return new IPageActionProjection[]
                {
                    new LinkActionProjection(mediaJavaScriptModuleDescriptor, page => "loadSiteSettingsMediaManager")
                        {
                            Order = 2400,
                            Title = () => MediaGlobalization.SiteSettings_MediaManagerMenuItem,
                            CssClass = page => "bcms-sidebar-link"
                        }                                      
                };
        }
    }
}
