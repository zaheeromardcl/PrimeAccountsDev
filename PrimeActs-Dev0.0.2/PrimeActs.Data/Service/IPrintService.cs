using PrimeActs.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.Data.Service
{
    [ServiceContract]
    public interface IPrintService
    {
        [OperationContract]
        void PrintDocument(string[] printerNames, PrintDocumentModel document);
    }

    public interface IPrintServiceChannel : IPrintService, IClientChannel
    {
    }
}
