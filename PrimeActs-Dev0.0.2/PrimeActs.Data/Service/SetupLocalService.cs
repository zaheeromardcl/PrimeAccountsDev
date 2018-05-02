#region

using System;
using System.Linq;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Infrastructure.BaseEntities;

#endregion

namespace PrimeActs.Data.Service
{
    public class SetupLocalService : Service<SetupLocal>, ISetupLocalService
    {
        private readonly IRepositoryAsync<SetupLocal> _repository;

        public SetupLocalService(IRepositoryAsync<SetupLocal> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public SetupLocal GetDisplayOption(string strSetupName)
        {
            return _repository.Query().Select().Where(x => x.SetupName == strSetupName).FirstOrDefault();
        }
        public bool? GetSetupBooleanByNameAndCompanyID(string SetupName, Guid CompanyID)
        {
            return _repository.Query(x => x.SetupName == SetupName && x.CompanyID == CompanyID).Select().First().SetupValueBit;
        }
        public bool? GetSetupBooleanByNameAndDivisionID(string SetupName, Guid DivisionID)
        {
            return _repository.Query(x => x.SetupName == SetupName && x.DivisionID == DivisionID).Select().First().SetupValueBit;
        }
        public bool? GetSetupBooleanByNameAndDepartmentID(string SetupName, Guid DepartmentID)
        {
            return _repository.Query(x => x.SetupName == SetupName && x.DepartmentID == DepartmentID).Select().First().SetupValueBit;
        }
        public decimal? GetSetupDecimalByNameAndCompanyID(string SetupName, Guid CompanyID)
        {
            return _repository.Query(x => x.SetupName == SetupName && x.CompanyID == CompanyID).Select().First().SetupValueNumeric;
        }
        public decimal? GetSetupDecimalByNameAndDivisionID(string SetupName, Guid DivisionID)
        {
            return _repository.Query(x => x.SetupName == SetupName && x.DivisionID == DivisionID).Select().First().SetupValueNumeric;
        }
        public decimal? GetSetupDecimalByNameAndDepartmentID(string SetupName, Guid DepartmentID)
        {
            return _repository.Query(x => x.SetupName == SetupName && x.DepartmentID == DepartmentID).Select().First().SetupValueNumeric;
        }
        public int? GetSetupIntByNameAndCompanyID(string SetupName, Guid CompanyID, bool doIncrement = false)
        {
            int retInt;
            var setup = _repository.Query(x => x.SetupName == SetupName && x.CompanyID == CompanyID).Select().First();
            if (setup == null || setup.SetupValueInt == null)
            {
                return null;
            }
            retInt = (int)setup.SetupValueInt;
            if (doIncrement)
            {
                lock (SetupName + CompanyID.ToString())
                {
                    setup.ObjectState = ObjectState.Modified;
                    setup.SetupValueInt = retInt + 1;
                    Update(setup);
                }
            }
            return retInt;
        }
        public int? GetSetupIntByNameAndDivisionID(string SetupName, Guid DivisionID, bool doIncrement = false)
        {
            int retInt;
            var setup = _repository.Query(x => x.SetupName == SetupName && x.DivisionID == DivisionID).Select().First();
            if (setup == null || setup.SetupValueInt == null)
            {
                return null;
            }
            retInt = (int)setup.SetupValueInt;
            if (doIncrement)
            {
                lock (SetupName + DivisionID.ToString())
                {
                    setup.ObjectState = ObjectState.Modified;
                    setup.SetupValueInt = retInt + 1;
                    Update(setup);
                }
            }
            return retInt;
        }
        public int? GetSetupIntByNameAndDepartmentID(string SetupName, Guid DepartmentID, bool doIncrement = false)
        {
            int retInt;
            var setup = _repository.Query(x => x.SetupName == SetupName && x.DepartmentID == DepartmentID).Select().First();
            if (setup == null || setup.SetupValueInt == null)
            {
                return null;
            }
            retInt = (int)setup.SetupValueInt;
            if (doIncrement)
            {
                lock (SetupName + DepartmentID.ToString())
                {
                    setup.ObjectState = ObjectState.Modified;
                    setup.SetupValueInt = retInt + 1;
                    Update(setup);
                }
            }
            return retInt;
        }
        public string GetSetupStringByNameAndCompanyID(string SetupName, Guid CompanyID)
        {
            return _repository.Query(x => x.SetupName == SetupName && x.CompanyID == CompanyID).Select().First().SetupValueNvarchar;
        }
        public string GetSetupStringByNameAndDivisionID(string SetupName, Guid DivisionID)
        {
            return _repository.Query(x => x.SetupName == SetupName && x.DivisionID == DivisionID).Select().First().SetupValueNvarchar;
        }
        public string GetSetupStringByNameAndDepartmentID(string SetupName, Guid DepartmentID)
        {
            return _repository.Query(x => x.SetupName == SetupName && x.DepartmentID == DepartmentID).Select().First().SetupValueNvarchar;
        }
        public Guid? GetSetupGuidByNameAndCompanyID(string SetupName, Guid CompanyID)
        {
            return _repository.Query(x => x.SetupName == SetupName && x.CompanyID == CompanyID).Select().First().SetupValueUniqueIdentifier;
        }
        public Guid? GetSetupGuidByNameAndDivisionID(string SetupName, Guid DivisionID)
        {
            return _repository.Query(x => x.SetupName == SetupName && x.DivisionID == DivisionID).Select().First().SetupValueUniqueIdentifier;
        }
        public Guid? GetSetupGuidByNameAndDepartmentID(string SetupName, Guid DepartmentID)
        {
            return _repository.Query(x => x.SetupName == SetupName && x.DepartmentID == DepartmentID).Select().First().SetupValueUniqueIdentifier;
        }
    }
}