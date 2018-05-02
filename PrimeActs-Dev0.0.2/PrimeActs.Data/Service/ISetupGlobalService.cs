#region

using System;
using System.Collections.Generic;
using PrimeActs.Domain;

#endregion

namespace PrimeActs.Data.Service
{
    public interface ISetupGlobalService : IService<SetupGlobal>
    {
        List<SetupGlobal> GetAllSetupValuesBySetupName(string SetupName);
        bool? GetSetupBooleanByNameAndCompanyID(string SetupName, Guid CompanyID);
        bool? GetSetupBooleanByNameAndDivisionID(string SetupName, Guid DivisionID);
        bool? GetSetupBooleanByNameAndDepartmentID(string SetupName, Guid DepartmentID);
        decimal? GetSetupDecimalByNameAndCompanyID(string SetupName, Guid CompanyID);
        decimal? GetSetupDecimalByNameAndDivisionID(string SetupName, Guid DivisionID);
        decimal? GetSetupDecimalByNameAndDepartmentID(string SetupName, Guid DepartmentID);
        int? GetSetupIntByNameAndCompanyID(string SetupName, Guid CompanyID, bool doIncrement = false);
        int? GetSetupIntByNameAndDivisionID(string SetupName, Guid DivisionID, bool doIncrement = false);
        int? GetSetupIntByNameAndDepartmentID(string SetupName, Guid DepartmentID, bool doIncrement = false);
        string GetSetupStringByNameAndCompanyID(string SetupName, Guid CompanyID);
        string GetSetupStringByNameAndDivisionID(string SetupName, Guid DivisionID);
        string GetSetupStringByNameAndDepartmentID(string SetupName, Guid DepartmentID);
        Guid? GetSetupGuidByNameAndCompanyID(string SetupName, Guid CompanyID);
        Guid? GetSetupGuidByNameAndDivisionID(string SetupName, Guid DivisionID);
        Guid? GetSetupGuidByNameAndDepartmentID(string SetupName, Guid DepartmentID);
    }
}