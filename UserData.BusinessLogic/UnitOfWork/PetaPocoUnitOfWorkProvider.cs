using PetaPoco;
using UserData.Data.Repositories;

namespace UserData.BusinessLogic.UnitOfWork
{
    

    public interface IUnitOfWorkProvider
    {
        IUnitOfWork GetUnitOfWork();

        IRepository GetRepository();
    }

    public class PetaPocoUnitOfWorkProvider : IUnitOfWorkProvider
    {
        public IUnitOfWork GetUnitOfWork()
        {
            return new PetaPocoUnitOfWork();
        }


        public IRepository GetRepository()
        {
            return new PetaPocoRepository();
        }
    }
}