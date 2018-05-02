using System;
using System.Web;
using PrimeActs.Data.Service;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;

namespace PrimeActs.Orchestras
{
    public class FileOrchestra : IFileOrchestra
    {
        private readonly IFileService _fileService;
        private readonly IPurchaseInvoiceFileService _purchaseInvoiceFileService;
        private readonly IConsignmentFileService _consignmentFileService;
        private readonly string _serverCode;
        private ApplicationUser _principal;

        enum FileItemType
        {
            PurchaseInvoice = 1,
            Consignment = 2
            //BankStatement = 3
        }

        public FileOrchestra(ISetupLocalService setupLocalService, IFileService fileService, IConsignmentFileService consignmentFileService, IPurchaseInvoiceFileService purchaseInvoiceFileService)
        {
            _fileService = fileService;
            _consignmentFileService = consignmentFileService;
            var setting = setupLocalService.Find("ServerCode");
            _serverCode = setting != null ? setting.SetupValueNvarchar : "L";
            _purchaseInvoiceFileService = purchaseInvoiceFileService;
        }

        public FileModel GetFile(Guid fileID)
        {
            var file = _fileService.FileById(fileID);

            var fileModel = new FileModel()
            {
                FileName = file.FileName,
                Content = file.FileContent,
                ContentType = file.ContentType
            };

            return fileModel;
        }

        public Guid InsertFileFromServer(string fileName, byte[] fileContent, Guid fileID, string extension)
        {
            var f = new File();
            f.FileContent = fileContent;
            f.FileName = fileName;
            f.FileID = fileID;
           f.ContentType = extension.ToLower();

            _fileService.Insert(f);

            return f.FileID;
        }

        public Guid InsertFile(HttpPostedFile file)
        {
            var data = new byte[file.ContentLength];
            file.InputStream.Read(data, 0, file.ContentLength);
            var f = new File();
            f.FileContent = data;
            var pos = file.FileName.LastIndexOf("\\");
            var length = file.FileName.Length;
            var fName = file.FileName.Substring(pos + 1, length - pos - 1);
            f.FileName = fName;
            //f.FileID = 
           
          

            _fileService.Insert(f);

            return f.FileID;
        }

        public void AttachFileToItem(Guid fileID, Guid itemID, int itemType)
        {
            var newGuid = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]);

            switch ((FileItemType)itemType)
            {
                case FileItemType.PurchaseInvoice: _purchaseInvoiceFileService.Insert(new PurchaseInvoiceFile() { PurchaseInvoiceFileID = newGuid, PurchaseInvoiceID = itemID, FileID = fileID, CreatedBy = _principal.Id, CreatedDate = DateTime.Now });
                    break;
                case FileItemType.Consignment: _consignmentFileService.Insert(new ConsignmentFile() { ConsignmentFileID = newGuid, ConsignmentID = itemID, FileID = fileID, CreatedBy = _principal.Id, CreatedDate = DateTime.Now});
                    break;
            }
        }

        public void Initialize(ApplicationUser principal)
        {
            _principal = principal;
        }
    }
}
