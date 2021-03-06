﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;

using BetterCms.Core.Models;

namespace BetterCms.Core.Modules.Projections
{
    public interface IJavaScriptAccessor
    {
        string GetCustomJavaScript(HtmlHelper html);
    }

    public interface IStylesheetAccessor
    {
        string GetCustomStyles(HtmlHelper html);
    }

    public interface IHtmlAccessor
    {
        string GetRegionWrapperCssClass(HtmlHelper html);

        string GetHtml(HtmlHelper html);
    }

    public interface IContentAccessor : IHtmlAccessor, IStylesheetAccessor, IJavaScriptAccessor
    {        
    }

    [Serializable]
    public abstract class ContentAccessor<TContent> :  IContentAccessor 
        where TContent : IContent
    {
        protected TContent Content { get; private set; }

        protected IList<IPageContentOption> Options { get; private set; }
        
        protected ContentAccessor(TContent content, IList<IPageContentOption> options)
        {
            Content = content;
            Options = options;
        }

        public abstract string GetRegionWrapperCssClass(HtmlHelper html);

        public abstract string GetHtml(HtmlHelper html);

        public abstract string GetCustomStyles(HtmlHelper html);

        public abstract string GetCustomJavaScript(HtmlHelper html);
    }
}