﻿using System;
using System.Text;

using BetterCms.Module.Root.ViewModels.Cms;

namespace BetterCms.Module.Root.Mvc.Helpers
{
    /// <summary>
    /// Helper class for rendering layout section (region)
    /// </summary>
    public class LayoutRegionWrapper : IDisposable
    {
        private readonly StringBuilder sb;
        private readonly PageRegionViewModel region;
        private readonly bool allowContentManagement;

        /// <summary>
        /// Initializes a new instance of the <see cref="LayoutRegionWrapper" /> class.
        /// </summary>
        /// <param name="sb">The string builder.</param>
        /// <param name="region">The region.</param>
        /// <param name="allowContentManagement">if set to <c>true</c> allows content management.</param>
        public LayoutRegionWrapper(StringBuilder sb, PageRegionViewModel region, bool allowContentManagement)
        {
            this.sb = sb;
            this.region = region;
            this.allowContentManagement = allowContentManagement;

            if (allowContentManagement)
            {
                RenderOpeningTags();
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (allowContentManagement)
            {
                RenderClosingTags();
            }
        }

        /// <summary>
        /// Renders the opening tags.
        /// </summary>
        private void RenderOpeningTags()
        {
            sb.AppendFormat(@"<div class=""bcms-region"" data-id=""{0}"">", region.RegionId);
            sb.AppendLine();
        }

        /// <summary>
        /// Renders the closing tags.
        /// </summary>
        private void RenderClosingTags()
        {
            sb.AppendLine(@"</div>");
        }
    }
}