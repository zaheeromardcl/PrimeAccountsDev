#region

using System;
using System.Collections.Generic;
using PrimeActs.Domain;

#endregion

namespace PrimeActs.Data.Service
{
    public interface IProduceGroupService : IService<ProduceGroup>
    {
        ProduceGroup ProduceGroupByName(string ProduceGroupName);
        ProduceGroup ProduceGroupIncludeProduceByName(string ProduceGroupName, Guid optDepartmentID);
        ProduceGroup ProduceGroupIncludeProduceByID(Guid ProduceGroupID, Guid optDepartmentID);
        ProduceGroup ProduceGroupById(Guid Id);
        List<ProduceGroup> GetAllProduceGroups();
        List<ProduceGroup> ProduceGroupIncludeProduceByNameRange(string produceGroupStartName, string produceGroupEndName,Guid optDepartmentID = default(Guid));
        List<ProduceGroup> ProduceGroupIncludeProduceByDepartment(Guid optDepartmentID = default(Guid));
        ProduceGroup ProduceGroupIncludeProduce(Guid optDepartmentID = default(Guid));
        void RefreshCache();
    }
}