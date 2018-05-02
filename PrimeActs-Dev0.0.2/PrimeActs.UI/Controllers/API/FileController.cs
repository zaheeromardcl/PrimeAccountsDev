#region

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Json;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Orchestras;
using Microsoft.AspNet.Identity.Owin;

using System.Threading.Tasks;
using PrimeActs.Data.Service;
using PrimeActs.Domain.ViewModels;
using File = PrimeActs.Domain.File;

#endregion

namespace PrimeActs.UI.Controllers.API
{
    //==================
    public class ByteArrayConverter : JsonConverter
    {

        public override bool CanRead
        {
            get { return false; }
        }

        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(
            JsonWriter writer,
            object value,
            JsonSerializer serializer)
        {
            var base64String = Convert.ToBase64String((byte[])value);

            serializer.Serialize(writer, base64String);
        }

        public override bool CanConvert(Type t)
        {
            return typeof(byte[]).IsAssignableFrom(t);
        }
    }

    //=============================


    public class FileController : ApiController
    {
        private string _serverCode = "L";//Need to change with actual at runtime.
        private readonly IConsignmentOrchestra _consignmentOrchestra;
        private readonly IUnitOfWorkAsync _unitofWork;
        private readonly IFileOrchestra _fileOrchestra;
        private readonly ISetupLocalService _setupService;

        private readonly string _mainUploadFolder;
        //private PrimeActsUserManager _userManager;
        //public PrimeActsUserManager UserManager
        //{
        //    get { return _userManager ?? Request.GetOwinContext().GetUserManager<PrimeActsUserManager>(); }
        //    private set { _userManager = value; }
        //}


        public FileController(IConsignmentOrchestra consignmentOrchestra, IUnitOfWorkAsync unitofWork, IFileOrchestra fileOrchestra, ISetupLocalService setupLocalService)
        {
            _consignmentOrchestra = consignmentOrchestra;
            _unitofWork = unitofWork;
            _fileOrchestra = fileOrchestra;
            _setupService = setupLocalService;

            _mainUploadFolder = System.Web.Hosting.HostingEnvironment.MapPath(@"\") + _setupService.GetDisplayOption("UploadFolderPath").SetupValueNvarchar; //ConfigurationManager.AppSettings["UploadDirectoryPath"];
        }


        public Guid StoreFile(HttpPostedFile file)
        {
            var data = new byte[file.ContentLength];
            file.InputStream.Read(data, 0, file.ContentLength);
            var f = new File();
            f.FileContent = data;
            var pos = file.FileName.LastIndexOf("\\");
            var length = file.FileName.Length;
            var fName = file.FileName.Substring(pos + 1, length - pos - 1);
            f.FileName = fName;
            f.FileID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]);
          

            _consignmentOrchestra.InsertFile(f);

            return f.FileID;
        }

        public void StoreConsignmentFile(HttpPostedFile file, Guid fID, string id)
        {
            var data = new byte[file.ContentLength];
            file.InputStream.Read(data, 0, file.ContentLength);
            var f = new ConsignmentFile();
            f.ConsignmentFileID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]);
            f.FileID = fID;
            f.ConsignmentID = Guid.Parse(id);

            //The others must exists in the table so this should 
            //be passed back to the orchestra somehow
            _consignmentOrchestra.InsertConsignmentFile(f);
        }

        public class FilesToBeAttached
        {
            public string MainID { get; set; }
            public int itemType { get; set; }
            public IEnumerable<string> fileNames { get; set; }
            public IEnumerable<string> fileNamesDeleted { get; set; }
            public string uploadFolder { get; set; }
        }



        // delete the files from the temporary folder and the folder as well
        [System.Web.Http.HttpPost]
        public void DeleteFilesFromServer(FilesToBeAttached dto)
        {
            if (dto.fileNames != null && dto.fileNames.Any())
            {
                //foreach (string fileName in dto.fileNames)
                //{
                var folderPath = _mainUploadFolder + "\\" + dto.uploadFolder;

                System.IO.DirectoryInfo di = new DirectoryInfo(folderPath);
                if (di != null)
                {
                    foreach (FileInfo file in di.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in di.GetDirectories())
                    {
                        dir.Delete(true);
                    }
                    di.Delete();
                }
                //}
            }
        }

        // attach the posted files
        [System.Web.Http.HttpPost]
        public void AttachFilesFromServer(FilesToBeAttached dto)
        {
            PopulateUser();

            if (dto.fileNames != null)
            {
                foreach (string fileID in dto.fileNames.Union(dto.fileNamesDeleted))
                {
                    if (fileID != null)
                    {
                        var fileFolderPath = _mainUploadFolder + "\\" + dto.uploadFolder + "\\" + fileID;
                        var di = new DirectoryInfo(fileFolderPath);
                        var fileInfo = di.GetFiles().FirstOrDefault();
                        var fileIDGuid = new Guid(fileID);
                        if (fileInfo != null)
                        {
                            byte[] fileContent;
                            using (var stream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read))
                            {
                                using (var reader = new BinaryReader(stream))
                                {
                                    fileContent = reader.ReadBytes((int) stream.Length);
                                }
                            }

                            _fileOrchestra.InsertFileFromServer(fileInfo.Name, fileContent, fileIDGuid,
                                fileInfo.Extension);
                        }

                        //attach files to the corresponding item based on the defined type and ID
                        var itemID = Guid.Parse(dto.MainID);

                        if (dto.fileNames.Contains(fileID))
                            _fileOrchestra.AttachFileToItem(fileIDGuid, itemID, dto.itemType);
                    }
                }

                _unitofWork.SaveChanges();
            }
        }

        // attach the posted files
        [System.Web.Http.HttpPost]
        public string AttachFiles(string id, int itemType)
        {
            PopulateUser();

            string fileID = string.Empty;

            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];

                    var fileFolderGuid = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]);
                    var fileIDGuid = _fileOrchestra.InsertFile(postedFile);

                    //attach files to the corresponding item based on the defined type and ID
                    var itemID = Guid.Parse(id);
                    _fileOrchestra.AttachFileToItem(fileIDGuid, itemID, itemType);

                    fileID = fileFolderGuid.ToString();
                }

                //send back file id to  

                _unitofWork.SaveChanges();
            }

            return fileID;
        }

        protected HttpResponseMessage JsonApiRespond(dynamic input)
        {
            string jsonResult = JsonConvert.SerializeObject(input);
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(jsonResult, Encoding.UTF8, "application/json");
            return response;
        }


        public static Dictionary<string, object> parse(byte[] json)
        {
            var reader = new StreamReader(new MemoryStream(json), Encoding.Default);

            var values = new JsonSerializer().Deserialize<Dictionary<string, object>>(new JsonTextReader(reader));

            return values;
        }


        [System.Web.Http.HttpPost]
        public JsonResult UploadFile(string id)
        {
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;

            if (httpRequest.Files.Count > 0)
            {
                var i = 0;
                var docfiles = new List<string>();
                byte[] data = null;
                var client = new WebClient();

                foreach (string file in httpRequest.Files)
                {
                    i = i + 1;

                    var postedFile = httpRequest.Files[file];

                    var fID = StoreFile(postedFile);

                    var ms = new MemoryStream();

                    //postedFile.InputStream.CopyTo(ms);

                    data = client.DownloadData(postedFile.FileName).ToArray();

                    // If you need it...
                    //data = ms.ToArray();
                    //test
                    //docfiles[i] = postedFile.ToString();
                    //Each time a file is stored a row needs to be added in the 
                    //Consignment File table
                    //StoreConsignmentFile(postedFile, fID, id);

                    dynamic js = new JsonObject();
                    js.FileName = postedFile.FileName;

                    var serialized = JsonConvert.SerializeObject(data, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Objects,
                        Formatting = Formatting.Indented,
                        Converters = new[] { new ByteArrayConverter() }
                    });

                    js.Content = serialized;

                    try
                    {
                        //var json = new JavaScriptSerializer().Serialize(data);

                        //json.ToString();
                        var serializer = new JavaScriptSerializer();


                        //List<fileU> myDeserializedObjList =
                        //    (List<fileU>)Newtonsoft.Json.JsonConvert.DeserializeObject(js, typeof(List<fileU>));

                        //fileU myDeserializedObj = serializer.Deserialize<fileU>(js); 

                        //var item =  JsonConvert.SerializeObject(js);
                        //var standard = new JsonSerializerSettings();


                        return null; // Json(Foo, JsonRequestBehavior.AllowGet); 

                        //return Request.CreateResponse(HttpStatusCode.OK, item); 
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }

                //response.Content = new ObjectContent<T>(T, myFormatter, “application/some-format”);

                //var test = JsonApiRespond(docfiles);

                //result = Request.CreateResponse(HttpStatusCode.Created, "application/json");

                //var resp = new HttpResponseMessage { 
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            return null;
        }

        public class fileU
        {
            public string FileName { get; set; }
            public string Content { get; set; }
        }

        public void PopulateUser()
        {
            var applicationUser = User.Identity.GetApplicationUser();
            _fileOrchestra.Initialize(applicationUser);
        }
    }
}