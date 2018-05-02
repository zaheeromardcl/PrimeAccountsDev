using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using PrimeActs.Data.Service;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Orchestras;

namespace PrimeActs.UI.Controllers
{
    public class FileController : Controller
    {
        private readonly ISetupLocalService _setupService;
        private string _serverCode = "L";//Need to change with actual at runtime.
        private readonly IFileOrchestra _fileOrchestra;

        private readonly string _mainUploadFolder;

        public FileController(ISetupLocalService setupLocalService, IFileOrchestra fileOrchestra)
        {
            _setupService = setupLocalService;
            _fileOrchestra = fileOrchestra;
            _mainUploadFolder = System.Web.Hosting.HostingEnvironment.MapPath(@"\") + _setupService.GetDisplayOption("UploadFolderPath").SetupValueNvarchar; //Server.MapPath(@"\")
        }

        // GET: File
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Show(Guid fileID, Guid itemID)
        {
            var fileModel = new FileModel();

            if (System.IO.Directory.Exists(_mainUploadFolder + "\\" + itemID))
            {
                var fileFolderPath = _mainUploadFolder + "\\" + itemID + "\\" + fileID;
                var di = new DirectoryInfo(fileFolderPath);

                var fileInfo = di.GetFiles().FirstOrDefault();
                var fileIDGuid = fileID;
                if (fileInfo != null)
                {
                    byte[] fileContent;
                    using (var stream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = new BinaryReader(stream))
                        {
                            fileModel.Content = reader.ReadBytes((int)stream.Length);
                            fileModel.ContentType = fileInfo.Extension.ToLower();
                        }
                    }
                }
            }
            else
            {
                fileModel = _fileOrchestra.GetFile(fileID);
            }


            return File(fileModel.Content, fileModel.ContentType);
        }

        public ActionResult SaveUploadedFile()
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            string fileFolderName = "";
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    var uploadFolder = Request.Headers["uploadFolder"];
                    //Save file content goes here
                    fName = file.FileName;
                    if (file != null && file.ContentLength > 0)
                    {
                        var uploadDirectory = string.Format("{0}\\{1}", _mainUploadFolder, uploadFolder);

                        //var fileName1 = Path.GetFileName(file.FileName);

                        bool isExists = System.IO.Directory.Exists(uploadDirectory);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(uploadDirectory);

                        fileFolderName = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]).ToString();
                        var fileFolderPath = uploadDirectory + "/" + fileFolderName;
                        System.IO.Directory.CreateDirectory(fileFolderPath);

                        var path = string.Format("{0}\\{1}", fileFolderPath, file.FileName);
                        file.SaveAs(path);
                    }
                }

            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
            }

            if (isSavedSuccessfully)
            {
                return Json(new { Message = fName, FileID = fileFolderName });
            }
            else
            {
                return Json(new { Message = "Error in saving file" });
            }
        }

    }
}