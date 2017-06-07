using System;
using System.Collections.Generic;
using System.Linq;
using UserData.SharedModels.DataModels;
using UserData.SharedModels.Responses;
using UserData.BusinessLogic.Enums.ServiceResults;
using UserData.BusinessLogic.UnitOfWork;
using UserData.Data.Repositories;

namespace UserData.BusinessLogic.Services
{


    public class AccountService : IAccountService
    {
        private readonly IUnitOfWorkProvider _unitOfWorkProvider;
        private readonly ILogger _logger;
        const string AccountSelectSql = "SELECT * from Account ";
     

        public AccountService(IUnitOfWorkProvider uowp, ILogger logger)
        {
            _unitOfWorkProvider = uowp;
            _logger = logger;
        }
        
        
        /// <summary>
        /// Calls repository to create an account.
        /// </summary>
        /// <returns></returns>
        public AccountResponse CreateAccount(Account account)
        {

            var response = new AccountResponse();

            using (var uow = _unitOfWorkProvider.GetUnitOfWork())
            {
                try
                {
                    
                    account.Created = DateTime.UtcNow;
                    account.Updated = account.Created;
                    var res = uow.Repository.Insert(account);
                    if (res > 0)
                    {
                        uow.Commit();
                        response.Account = account;
                        response.Status = AccountResult.Success.ToString();
                        return response;
                    }
                }
                catch (Exception ex)
                {
                    // do some logging
                    _logger.Log(ex);
                    response.Status = AccountResult.Error.ToString();
                    response.Message = string.Format("{0}: {1}", ex.Message, ex.StackTrace);

                    throw;
                }
            }
            response.Status = AccountResult.Error.ToString();
            return response;
        }





        
        /// <summary>
        /// Gets account by email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Account GetAccount(string email)
        {
            using (var repository = _unitOfWorkProvider.GetRepository())
            {
                try
                {
                    var res = GetAccount(repository, email);

                    return res;
                }
                catch (Exception ex)
                {
                    // do some logging
                    _logger.Log(ex);
                    throw;
                }
            }
        }


        public Account GetAccount(int id)
        {
            using (var repository = _unitOfWorkProvider.GetRepository())
            {
                try
                {
                    var res = GetAccount(repository, id);

                    return res;
                }
                catch (Exception ex)
                {
                    // do some logging
                    _logger.Log(ex);
                    throw;
                }
            }
        }


        public AccountResponse GetAccountsByCountry(string countryCode)
        {
            using (var repository = _unitOfWorkProvider.GetRepository())
            {
                try
                {
                    var res = GetAccountsByCountry(repository, countryCode);

                    return res;
                }
                catch (Exception ex)
                {
                    // do some logging
                    _logger.Log(ex);
                    throw;
                }
            }
        }
        

        public List<Account> GetByAccountIds(List<int> accountIds)
        {
            using (var repository = _unitOfWorkProvider.GetRepository())
            {
                try
                {
                    var res = GetByAccountIds(repository, accountIds);

                    return res;
                }
                catch (Exception ex)
                {
                    // do some logging
                    _logger.Log(ex);
                    throw;
                }
            }
        }


   


        public Account GetAccountByAccessToken(string accessToken)
        {
            using (var repository = _unitOfWorkProvider.GetRepository())
            {
                try
                {
                    var res = GetAccountByAccessToken(repository, accessToken);

                    return res;
                }
                catch (Exception ex)
                {
                    // do some logging
                    _logger.Log(ex);
                    throw;
                }
            }
        }
        

        protected Account GetAccount(IRepository repository, string email)
        {
            return repository.Fetch<Account>(AccountSelectSql + "WHERE Email = @0", email).SingleOrDefault();
        }


        protected Account GetAccount(IRepository repository, int id)
        {
            return repository.Fetch<Account>(AccountSelectSql + "WHERE user_id = @0", id).SingleOrDefault();
        }



        protected Account GetAccountByAccessToken(IRepository repository, string token)
        {
            return repository.Fetch<Account>(AccountSelectSql + "WHERE AccessToken = @0", token).SingleOrDefault();
        }


        protected Account GetAccountByFacebookUserId(IRepository repository, string userId)
        {
            return repository.Fetch<Account>(AccountSelectSql + "WHERE facebookId = @0", userId).SingleOrDefault();
        }


        protected List<Account> GetByAccountIds(IRepository repository, IEnumerable<int> ids)
        {
            var res = repository.Fetch<Account>(AccountSelectSql + "WHERE user_id in (@0)", ids);
            return res;
        }


        protected AccountResponse GetAccountsByCountry(IRepository repository, string countryCode)
        {
            var res = repository.Fetch<Account>("WHERE Country = @0", countryCode);
            return new AccountResponse()
            {
                Items = res
            };
        }





        /// <summary>
        /// Calls repository to update an account.
        /// </summary>
        /// <returns></returns>
        public AccountResponse UpdateAccount(Account account)
        {
            var accountResponse = new AccountResponse();

            using (var uow = _unitOfWorkProvider.GetUnitOfWork())
            {
                // get account from repo
                var accountFromDb = GetAccount(uow.Repository, account.user_id);

                // WB: 22/4/14
                // PetaPoco Update fails with this simplier way due to Created & Updated date being 01/01/0001 00:00:00 
                // due to bad model binding coming from the website to this API proxy
                account.Updated = DateTime.UtcNow;
                account.Created = accountFromDb.Created;

                try
                {
                    var res = uow.Repository.Update(account);

                    if (res > 0)
                    {
                        uow.Commit();
                        accountResponse.Account = account;
                        accountResponse.Status = AccountResult.Success.ToString();
                        return accountResponse;
                    }
                }
                catch (Exception ex)
                {
                    // do some logging
                    _logger.Log(ex);
                    throw;
                }
            }

            accountResponse.Status = AccountResult.Error.ToString();
            return accountResponse;
        }

        




        /// <summary>
        /// Flags account as deleted.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public AccountResult RemoveAccountFromDB(int accountId)
        {
            using (var uow = _unitOfWorkProvider.GetUnitOfWork())
            {
                try
                {
                  //  var account = GetAccount(uow.Repository, accountId);
                    

                   // account.Deleted = true;

                    //var res = uow.Repository.Update(account);
                    var res = uow.Repository.Delete<Account>(accountId);
                    if (res > 0)
                    {
                        // mark media as deleted
                      //  uow.Repository.ExecuteScalar<Media>("update Media set Deleted = 1 where AccountId = @0", account.Id);
                        
                      uow.Commit();
                        return AccountResult.Success;
                    }
                }
                catch (Exception ex)
                {
                    // do some logging
                    _logger.Log(ex);
                    throw;
                }
            }
            return AccountResult.Error;
        }
        

        public AccountResult DeleteFacebook(string email)
        {
            using (var uow = _unitOfWorkProvider.GetUnitOfWork())
            {
                try
                {
                    var account = GetAccount(uow.Repository, email);

                    account.FacebookId = null;
                    account.AccessToken = null;

                    var res = uow.Repository.Update(account);

                    if (res > 0)
                    {
                        uow.Commit();
                        return AccountResult.Success;
                    }
                }
                catch (Exception ex)
                {
                    // do some logging
                    _logger.Log(ex);
                    throw;
                }
            }
            return AccountResult.Error;
        }


        /// <summary>
        /// </summary>
        /// <returns></returns>
        public AccountResponse PagedQuery(int pageNo, int itemsPerPage, string firstName, string lastName, string email, int accountId)
        {
            var response = new AccountResponse();

            var sql =
                "SELECT * From Account"
                + " WHERE "
                + " (@0 IS NULL OR FirstName = @0)"
                + " AND (@1 IS NULL OR LastName = @1)"
                + " AND (@2 IS NULL OR Email = @2)"
                + " AND (@3 = 0 OR user_id = @3)";
          

            using (var repository = _unitOfWorkProvider.GetRepository())
            {
                var res = repository.PagedQuery<Account>(pageNo, itemsPerPage, sql, firstName, lastName, email, accountId);

                response.Items = res.Items;
                response.TotalItems = (int)res.TotalItems;
                response.Status = AccountResult.Success.ToString();

                return response;
            }
        }


    }
}