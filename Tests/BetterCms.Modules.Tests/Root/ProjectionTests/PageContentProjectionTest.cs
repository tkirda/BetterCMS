﻿using System.Linq;

using BetterCms.Core.Models;
using BetterCms.Module.Pages.Models;
using BetterCms.Module.Pages.Projections;
using BetterCms.Module.Root.Projections;

using NUnit.Framework;

namespace BetterCms.Test.Module.Root.ProjectionTests
{
    [TestFixture]
    public class PageContentProjectionTest : SerializationTestBase
    {
        [Test]
        public void Should_By_Xml_And_Binary_Serializable()
        {
            var pageContent = TestDataProvider.CreateNewPageContent();
            pageContent.Content = TestDataProvider.CreateNewHtmlContent();
            pageContent.PageContentOptions = new[]
                                                 {
                                                     TestDataProvider.CreateNewPageContentOption(pageContent),
                                                     TestDataProvider.CreateNewPageContentOption(pageContent),
                                                     TestDataProvider.CreateNewPageContentOption(pageContent)
                                                 };

            PageContentProjection original = new PageContentProjection(
                pageContent, new HtmlContentAccessor((HtmlContent)pageContent.Content, pageContent.PageContentOptions.Cast<IPageContentOption>().ToList()));

            RunSerializationAndDeserialization(original,
                projection =>
                    {
                        Assert.AreEqual(original.ContentId, projection.ContentId);
                        Assert.AreEqual(original.Order, projection.Order);
                        Assert.AreEqual(original.RegionId, projection.RegionId);
                    });
        }       
    }
}
