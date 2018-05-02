using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;

namespace PrimeActs.Orchestras
{
    public interface IFileOrchestra
    {
        Guid InsertFile(HttpPostedFile postedFile);
        void AttachFileToItem(Guid fileID, Guid itemID, int itemType);
        void Initialize(ApplicationUser principal);
        Guid InsertFileFromServer(string fileName, byte[] fileContent, Guid fileID, string extension);
        FileModel GetFile(Guid fileID);
    }
}
