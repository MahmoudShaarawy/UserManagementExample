using System;
using PetaPoco;
using UserData.Data.Repositories;

namespace UserData.BusinessLogic.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        //Database Db { get; }
        IRepository Repository { get; }
    }


    public class PetaPocoUnitOfWork : IUnitOfWork 
    {
        private readonly Transaction _petaTransaction;
        private readonly PetaPocoRepository _repository;

        public PetaPocoUnitOfWork()
        {
            _repository = new PetaPocoRepository();
            _petaTransaction = new Transaction(_repository.Db);
        }

        public void Dispose()
        {
            _petaTransaction.Dispose();
        }

        public IRepository Repository
        {
            get { return _repository; }
        }

        public void Commit()
        {
            _petaTransaction.Complete();
        }
    }
}