using Microsoft.Practices.ServiceLocation;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using StructureMap.Configuration.DSL;
using UserData.BusinessLogic.Services;
using UserData.BusinessLogic.UnitOfWork;

namespace UserData.BusinessLogic.StructureMap
{
    public class StructureMapDependencyResolver 
    {
        public static void ConfigureDependencies()
        {
            ObjectFactory.Initialize(x => { x.AddRegistry<ControllerRegistry>(); });
        }

        #region Nested type: ControllerRegistry

        public class ControllerRegistry : Registry
        {
            public ControllerRegistry()
            {

                Scan(x =>
                {
                    x.AssembliesFromApplicationBaseDirectory();
                    x.WithDefaultConventions();
                });

                For<IAccountService>().Use<AccountService>();
                For<ICommentsService>().Use<CommentsService>();
                For<IUnitOfWork>().Use<PetaPocoUnitOfWork>();
                For<IUnitOfWorkProvider>().Use<PetaPocoUnitOfWorkProvider>();
                

            }
        }

        #endregion
    }
}