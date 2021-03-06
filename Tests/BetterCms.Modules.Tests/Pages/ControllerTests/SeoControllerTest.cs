﻿using System;
using System.Web.Mvc;

using BetterCms.Core.Mvc.Commands;
using BetterCms.Module.Pages.Command.Page.GetPageSeo;
using BetterCms.Module.Pages.Command.Page.SavePageSeo;
using BetterCms.Module.Pages.Controllers;
using BetterCms.Module.Pages.Services;
using BetterCms.Module.Pages.ViewModels.Seo;
using BetterCms.Module.Root.Models;

using Moq;

using NUnit.Framework;

namespace BetterCms.Test.Module.Pages.ControllerTests
{
    /// <summary>
    /// SEO controller tests.
    /// </summary>
    [TestFixture]
    public class SeoControllerTest : ControllerTestBase
    {        
        [Test]
        public void Should_Get_EditSeo_ViewResult_With_EditSeoViewModel_Successfully()
        {            
            Mock<GetPageSeoCommand> getPageSeoCommandMock = new Mock<GetPageSeoCommand>();            
            getPageSeoCommandMock.Setup(f => f.Execute(It.IsAny<Guid>())).Returns(new EditSeoViewModel());                        

            SeoController seoController = new SeoController();
            seoController.CommandResolver = GetMockedCommandResolver(mock =>
                {
                    mock.Setup(resolver => resolver.ResolveCommand<GetPageSeoCommand>(It.IsAny<ICommandContext>())).Returns(() => getPageSeoCommandMock.Object);
                });

            ActionResult result = seoController.EditSeo(Guid.NewGuid().ToString());
            
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
            ViewResult viewResult = (ViewResult)result;
            Assert.IsNotNull(viewResult.Model);            
            Assert.IsInstanceOf<EditSeoViewModel>(viewResult.Model);
            getPageSeoCommandMock.Verify(f => f.Execute(It.IsAny<Guid>()), Times.Once());
        }

        [Test]
        public void Should_Try_To_Save_EditSeoViewModel_And_Return_Error_Flag()
        {
            SeoController seoController = new SeoController();
            seoController.ModelState.AddModelError("PageTitle", "Page title required.");

            ActionResult result = seoController.EditSeo(new EditSeoViewModel());            

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<JsonResult>(result);

            JsonResult viewResult = (JsonResult)result;
            Assert.IsNotNull(viewResult.Data);

            Assert.IsInstanceOf<WireJson>(viewResult.Data);
            WireJson wireJson = (WireJson)viewResult.Data;
            Assert.IsFalse(wireJson.Success);
        }

        [Test]
        public void Should_Save_EditSeoViewModel_And_Return_Success_Flag()
        {
            Mock<IRedirectService> redirectService = new Mock<IRedirectService>();
            Mock<IPageService> pageService = new Mock<IPageService>();
            Mock<SavePageSeoCommand> savePageSeoCommandMock = new Mock<SavePageSeoCommand>(redirectService.Object, pageService.Object);

            savePageSeoCommandMock.Setup(f => f.Execute(It.IsAny<EditSeoViewModel>())).Returns(true).Verifiable();

            SeoController seoController = new SeoController();
            seoController.CommandResolver = GetMockedCommandResolver(mock =>
                {
                    mock.Setup(resolver => resolver.ResolveCommand<SavePageSeoCommand>(It.IsAny<ICommandContext>())).Returns(() => savePageSeoCommandMock.Object);
                });

            ActionResult result = seoController.EditSeo(new EditSeoViewModel());

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<JsonResult>(result);

            JsonResult viewResult = (JsonResult)result;
            Assert.IsNotNull(viewResult.Data);

            Assert.IsInstanceOf<WireJson>(viewResult.Data);
            WireJson wireJson = (WireJson)viewResult.Data;
            Assert.IsTrue(wireJson.Success);

            savePageSeoCommandMock.Verify(f => f.Execute(It.IsAny<EditSeoViewModel>()), Times.Once());
        }
    }
}
