using System;
using System.Collections.Generic;
using System.Linq;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.Cache;
using PrimeActs.Infrastructure.EntityFramework;

namespace PrimeActs.Data.Service 
{
       
    public class TabPanelService: Service<UserTabPanel>, ITabPanelService
    {
       // private readonly ICache _cache;
        private readonly IRepositoryAsync<UserTabPanel> _repository;
        private readonly object lockboject = new object();

        public TabPanelService(IRepositoryAsync<UserTabPanel> repository)
            : base(repository)
        {
            _repository = repository;
            //_cache = cache;
            //type = typeof (TabPanel).FullName;
        }


        public UserTabPanel TabPanelById(Guid UserId, string Name) // user Id and Panel Name
        {
            return
                _repository.Query(fil => fil.UserID == UserId && fil.Name == Name)                  
                    .Select()
                    .FirstOrDefault();
        }

        public List<UserTabPanel> GetAllTabPanels(Guid UserId)
        {
                          
            return
                _repository.Query(fil => fil.UserID == UserId)
                    .Select()
                    .ToList();
        }

        //public override void Insert(TabPanel TabPanel)
        //{
        //    _repository.Insert(TabPanel);
            
        //}


    }
}
