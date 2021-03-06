﻿using System;

using BetterCms.Module.Root.Mvc.Grids;

namespace BetterCms.Module.Pages.ViewModels.SiteSettings
{
    public class SiteSettingPageViewModel : IEditableGridItem
    {
        /// <summary>
        /// Gets or sets the page id.
        /// </summary>
        /// <value>
        /// The page id.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the entity version.
        /// </summary>
        /// <value>
        /// The entity version.
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
        /// Gets or sets the date the page is created on.
        /// </summary>
        /// <value>
        /// The date the page is created on.
        /// </value>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the date the page is modified on.
        /// </summary>
        /// <value>
        /// The date the page is modified on.
        /// </value>
        public DateTime ModifiedOn { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether page has SEO.
        /// </summary>
        /// <value>
        ///   <c>true</c> if page has SEO; otherwise, <c>false</c>.
        /// </value>
        public bool HasSEO { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether page is published.
        /// </summary>
        /// <value>
        /// <c>true</c> if page is published; otherwise, <c>false</c>.
        /// </value>
        public bool IsPublished { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("Id: {0}, Version: {1}, Title: {2}", Id, Version, Title);
        }
    }
}