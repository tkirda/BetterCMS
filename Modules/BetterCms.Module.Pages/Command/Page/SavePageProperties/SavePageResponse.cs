﻿using System;
using System.Globalization;

using BetterCms.Core.Models;
using BetterCms.Module.Pages.Models;

namespace BetterCms.Module.Pages.Command.Page.SavePageProperties
{
    public class SavePageResponse
    {
        /// <summary>
        /// Gets or sets the page id.
        /// </summary>
        /// <value>
        /// The page id.
        /// </value>
        public Guid PageId { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public int Version { get; set; }

        /// <summary>
        /// Gets or sets the page title.
        /// </summary>
        /// <value>
        /// The page title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the page URL.
        /// </summary>
        /// <value>
        /// The page URL.
        /// </value>
        public string PageUrl { get; set; }

        /// <summary>
        /// Gets or sets the date the page is created on.
        /// </summary>
        /// <value>
        /// The date the page is created on.
        /// </value>
        public string CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the date the page is modified on.
        /// </summary>
        /// <value>
        /// The date the page is modified on.
        /// </value>
        public string ModifiedOn { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether page is published.
        /// </summary>
        /// <value>
        /// <c>true</c> if page is published; otherwise, <c>false</c>.
        /// </value>
        public bool IsPublished { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether page has SEO.
        /// </summary>
        /// <value>
        ///   <c>true</c> if page has SEO; otherwise, <c>false</c>.
        /// </value>
        public bool HasSEO { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SavePageResponse" /> class.
        /// </summary>
        /// <param name="page">The page.</param>
        public SavePageResponse(PageProperties page)
        {
            PageId = page.Id;
            Title = page.Title;
            CreatedOn = page.CreatedOn.ToString(CultureInfo.CurrentCulture);
            ModifiedOn = page.ModifiedOn.ToString(CultureInfo.CurrentCulture);
            IsPublished = page.IsPublished;
            HasSEO = ((IPage)page).HasSEO;
            PageUrl = page.PageUrl;
        }
    }
}