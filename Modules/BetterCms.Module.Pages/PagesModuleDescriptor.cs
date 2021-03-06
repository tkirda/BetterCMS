﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Autofac;

using BetterCms.Core.Models;
using BetterCms.Core.Modules;
using BetterCms.Core.Modules.Projections;
using BetterCms.Module.Pages.Content.Resources;
using BetterCms.Module.Pages.Models;
using BetterCms.Module.Pages.Projections;
using BetterCms.Module.Pages.Registration;
using BetterCms.Module.Pages.Services;

namespace BetterCms.Module.Pages
{
    /// <summary>
    /// Pages module descriptor.
    /// </summary>
    public class PagesModuleDescriptor : ModuleDescriptor
    {
        internal const string ModuleName = "pages";

        /// <summary>
        /// bcms.pages.js java script module descriptor.
        /// </summary>
        private readonly PagesJavaScriptModuleDescriptor pagesJavaScriptModuleDescriptor;

        /// <summary>
        /// bcms.pages.properties.js java script module descriptor.
        /// </summary>
        private readonly PagePropertiesJavaScriptModuleDescriptor pagePropertiesJavaScriptModuleDescriptor;

        /// <summary>
        /// bcms.pages.seo.js java script module descriptor.
        /// </summary>
        private readonly SeoJavaScriptModuleDescriptor seoJavaScriptModuleDescriptor;

        /// <summary>
        /// bcms.pages.tags.js java script module descriptor.
        /// </summary>
        private readonly TagsJavaScriptModuleDescriptor tagsJavaScriptModuleDescriptor;

        /// <summary>
        /// bcms.pages.content.js java script module descriptor.
        /// </summary>
        private readonly PagesContentJavaScriptModuleDescriptor pagesContentJavaScriptModuleDescriptor;
        
        /// <summary>
        /// bcms.pages.redirects.js java script module descriptor.
        /// </summary>
        private readonly RedirectsJavaScriptModuleDescriptor redirectsJavaScriptModuleDescriptor;

        /// <summary>
        /// bcms.pages.widgets.js java script module descriptor.
        /// </summary>
        private readonly WidgetsJavaScriptModuleDescriptor widgetsJavaScriptModuleDescriptor;

        private readonly TemplatesJavaScriptModuleDescriptor templatesJavaScriptModuleDescriptor;

        /// <summary>
        /// Initializes a new instance of the <see cref="PagesModuleDescriptor" /> class.
        /// </summary>
        public PagesModuleDescriptor()
        {
            pagesJavaScriptModuleDescriptor = new PagesJavaScriptModuleDescriptor(this);
            pagePropertiesJavaScriptModuleDescriptor = new PagePropertiesJavaScriptModuleDescriptor(this);
            seoJavaScriptModuleDescriptor = new SeoJavaScriptModuleDescriptor(this);
            pagesContentJavaScriptModuleDescriptor = new PagesContentJavaScriptModuleDescriptor(this);
            widgetsJavaScriptModuleDescriptor = new WidgetsJavaScriptModuleDescriptor(this);
            tagsJavaScriptModuleDescriptor = new TagsJavaScriptModuleDescriptor(this);
            redirectsJavaScriptModuleDescriptor = new RedirectsJavaScriptModuleDescriptor(this);
            templatesJavaScriptModuleDescriptor = new TemplatesJavaScriptModuleDescriptor(this);
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
                return "Pages module for BetterCMS.";
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
            RegisterContentRendererType<HtmlContentAccessor, HtmlContent>(containerBuilder);
            RegisterContentRendererType<HtmlContentWidgetAccessor, HtmlContentWidget>(containerBuilder);
            RegisterContentRendererType<ServerControlWidgetAccessor, ServerControlWidget>(containerBuilder);            

            containerBuilder.RegisterType<DefaultPageService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            containerBuilder.RegisterType<DefaultRedirectService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            containerBuilder.RegisterType<DefaultCategoryService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            containerBuilder.RegisterType<DefaultWidgetsService>().AsImplementedInterfaces().InstancePerLifetimeScope();
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
                    "/file/bcms-pages/Content/Css/bcms.page.css"
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
            return new JavaScriptModuleDescriptor[]
                {
                    pagesJavaScriptModuleDescriptor,
                    pagePropertiesJavaScriptModuleDescriptor,
                    pagesContentJavaScriptModuleDescriptor,
                    redirectsJavaScriptModuleDescriptor,
                    seoJavaScriptModuleDescriptor,
                    tagsJavaScriptModuleDescriptor,
                    widgetsJavaScriptModuleDescriptor,
                    templatesJavaScriptModuleDescriptor
                };
        }

        public override IEnumerable<IPageActionProjection> RegisterSidebarMainProjections(ContainerBuilder containerBuilder, ICmsConfiguration configuration)
        {
            return new IPageActionProjection[]
                {
                    new DropDownListProjection(pagesJavaScriptModuleDescriptor, page => "changePublishStatus")
                        {
                            Items = new Func<IPage, DropDownListProjectionItem>[]
                                {
                                     page => new DropDownListProjectionItem
                                         {
                                             Order = 1,
                                             Text = () => page.IsPublished 
                                                                ? PagesGlobalization.Sidebar_PageStatusPublished 
                                                                : PagesGlobalization.Sidebar_PageStatusPublish,
                                             Value = page.IsPublished 
                                                                ? "published"
                                                                : "publish",
                                             IsSelected = page.IsPublished                                             
                                         }, 

                                     page => new DropDownListProjectionItem
                                         {
                                             Order = 2,
                                             Text = () => page.IsPublished 
                                                                ? PagesGlobalization.Sidebar_PageStatusUnpublish
                                                                : PagesGlobalization.Sidebar_PageStatusUnpublished,
                                                                    
                                             Value = page.IsPublished
                                                                ? "unpublish"
                                                                : "unpublished",
                                             IsSelected = !page.IsPublished
                                         }
                                },
                                Order = 10,
                                CssClass = page => !page.IsPublished ? "bcms-sidemenu-select" : "bcms-sidemenu-select bcms-select-published"
                        }, 
                    
                    new ButtonActionProjection(pagePropertiesJavaScriptModuleDescriptor, page => "editPageProperties")
                            {
                                Order = 20,
                                Title = () => PagesGlobalization.Sidebar_EditPagePropertiesButtonTitle,
                                CssClass = page => "bcms-sidemenu-btn"
                            },

                    new ButtonActionProjection(seoJavaScriptModuleDescriptor, page => "openEditSeoDialog")
                            {
                                Order = 30,
                                Title = () => PagesGlobalization.Sidebar_EditSeoButtonTitle,
                                CssClass = page => page.HasSEO ? "bcms-sidemenu-btn bcms-btn-ok" : "bcms-sidemenu-btn bcms-btn-warn"
                            },

                    new SeparatorProjection(40){CssClass = page => "bcms-sidebar-separator"}, 

                    new InheriteProjection(
                        "div",
                        new IPageActionProjection[]
                            {
                                new ButtonActionProjection(pagesJavaScriptModuleDescriptor, page => "addNewPage")
                                {
                                    Order = 10,
                                    Title = () => PagesGlobalization.Sidebar_AddNewPageButtonTitle,
                                    CssClass = page => "bcms-sidemenu-btn bcms-btn-add"
                                },
                                new ButtonActionProjection(pagesJavaScriptModuleDescriptor, page => "clonePage")
                                {
                                    Order = 20,
                                    Title = () => PagesGlobalization.Siderbar_ClonePageButtonTitle,
                                    CssClass = page => "bcms-sidemenu-btn bcms-btn-clone"
                                }
                             })
                        {
                            Order = 50,
                            CssClass = page => "bcms-buttons-block"
                        },                          

                    new ButtonActionProjection(pagesJavaScriptModuleDescriptor, page => "deleteCurrentPage")
                        {
                            Order = 900,
                            Title = () => PagesGlobalization.Sidebar_DeletePageButtonTitle,
                            CssClass = page => "bcms-sidemenu-btn bcms-btn-delete"
                        }
                };
        }

        public override IEnumerable<IPageActionProjection> RegisterSidebarSideProjections(ContainerBuilder containerBuilder, ICmsConfiguration configuration)
        {
            return new IPageActionProjection[]
                {
                      new HtmlElementProjection("div")
                          {
                              Id = page => "bcms-sidemenu-pubstatus",
                              Tooltip = page => page.IsPublished 
                                                    ? PagesGlobalization.Sidebar_PageStatusPublishedTooltip
                                                    : PagesGlobalization.Sidebar_PageStatusUnpublishedTooltip,
                              Order = 10,
                              CssClass = page => page.IsPublished 
                                                    ? "bcms-sidemenu-pubstatus"
                                                    : "bcms-sidemenu-pubstatus bcms-pubstatus-warn"
                          }, 

                      new HtmlElementProjection("div")
                          {
                              Id = page => "bcms-sidemenu-seostatus",
                              Tooltip = page => page.HasSEO 
                                                    ? PagesGlobalization.Sidebar_PageStatusSeoOkTooltip
                                                    : PagesGlobalization.Sidebar_PageStatusNoSeoTooltip,
                              Order = 20,
                              CssClass = page => page.HasSEO 
                                                    ? "bcms-sidemenu-seostatus"
                                                    : "bcms-sidemenu-seostatus bcms-seostatus-warn"
                          }
                };
        }

        public override IEnumerable<IPageActionProjection> RegisterSiteSettingsProjections(ContainerBuilder containerBuilder, ICmsConfiguration configuration)
        {
            return new IPageActionProjection[]
                {
                    new LinkActionProjection(pagesJavaScriptModuleDescriptor, page => "loadSiteSettingsPageList")
                        {
                            Order = 1000,
                            Title = () => PagesGlobalization.SiteSettings_PagesMenuItem,
                            CssClass = page => "bcms-sidebar-link"
                        },
                    
                    new SeparatorProjection(1500), 

                    new LinkActionProjection(tagsJavaScriptModuleDescriptor, page => "loadSiteSettingsCategoryList")
                        {
                            Order = 2000,
                            Title = () => PagesGlobalization.SiteSettings_CategoriesMenuItem,
                            CssClass = page => "bcms-sidebar-link"
                        },
                   new LinkActionProjection(tagsJavaScriptModuleDescriptor, page => "loadSiteSettingsTagList")
                        {
                            Order = 2100,
                            Title = () => PagesGlobalization.SiteSettings_TagsMenuItem,
                            CssClass = page => "bcms-sidebar-link"
                        },

                    new SeparatorProjection(2500), 

                    new LinkActionProjection(widgetsJavaScriptModuleDescriptor, page => "loadSiteSettingsWidgetList")
                        {
                            Order = 3000,
                            Title = () => PagesGlobalization.SiteSettings_WidgetsMenuItem,
                            CssClass = page => "bcms-sidebar-link"
                        },
                        
                     new LinkActionProjection(templatesJavaScriptModuleDescriptor, page => "loadSiteSettingsTemplateList")
                        {
                            Order = 3100,
                            Title = () => PagesGlobalization.SiteSettings_TemplatesMenuItem,
                            CssClass = page => "bcms-sidebar-link"
                        },

                    new SeparatorProjection(3500), 

                    new LinkActionProjection(this.redirectsJavaScriptModuleDescriptor, page => "loadSiteSettingsRedirectList")
                        {
                            Order = 4000,
                            Title = () => PagesGlobalization.SiteSettings_Redirects,
                            CssClass = page => "bcms-sidebar-link"
                        }
                };
        }
    }
}
