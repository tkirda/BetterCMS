﻿using System.Web.Mvc;

using BetterCms.Module.MediaManager.Command.Files.GetFiles;
using BetterCms.Module.MediaManager.Command.MediaManager.DeleteMedia;
using BetterCms.Module.MediaManager.Content.Resources;
using BetterCms.Module.MediaManager.ViewModels.MediaManager;

using BetterCms.Module.Root.Models;
using BetterCms.Module.Root.Mvc;

namespace BetterCms.Module.MediaManager.Controllers
{
    /// <summary>
    /// Handles site settings logic for Media module Files tab.
    /// </summary>
    public class FilesController : CmsControllerBase
    {
        /// <summary>
        /// Gets the files list.
        /// </summary>
        /// <returns>List of files</returns>
        public ActionResult GetFilesList(MediaManagerViewModel options)
        {
            var success = true;
            if (options == null)
            {
                options = new MediaManagerViewModel();
            }

            var model = GetCommand<GetFilesCommand>().ExecuteCommand(options);
            if (model == null)
            {
                success = false;
            }
            return Json(new WireJson { Success = success, Data = model });
        }

        /// <summary>
        /// Deletes file.
        /// </summary>
        /// <param name="id">The file id.</param>
        /// <param name="version">The file entity version.</param>
        /// <returns>
        /// Json with result status.
        /// </returns>
        [HttpPost]
        public ActionResult FileDelete(string id, string version)
        {
            var request = new DeleteMediaCommandRequest
            {
                Id = id.ToGuidOrDefault(),
                Version = version.ToIntOrDefault()
            };
            if (GetCommand<DeleteMediaCommand>().ExecuteCommand(request))
            {
                Messages.AddSuccess(MediaGlobalization.DeleteFile_DeletedSuccessfully_Message);
                return Json(new WireJson
                {
                    Success = true
                });
            }

            return Json(new WireJson { Success = false });
        }
    }
}
