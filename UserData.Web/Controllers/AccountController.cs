using System;
using System.Collections.Generic;
using System.Web.Http;
using UserData.SharedModels.DataModels;
using UserData.SharedModels.Responses;
using UserData.BusinessLogic.Enums.ServiceResults;
using UserData.BusinessLogic.Services;

namespace UserData.Web.Controllers
{
    /// <summary>
    /// CRUD web API controller for website Accounts
    /// </summary>
    public class AccountController : ApiController
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public AccountResponse Create(Account account)
        {

            var response = new AccountResponse();

            try
            {
                // Check if account exist
                var currentAccount = _accountService.GetAccount(account.Email);

                if (currentAccount != null)
                {
                    response.Account = null;
                    response.Status = AccountResult.Error.ToString();
                    response.Message = "Account already exists";
                }
                else
                {
                    var res = _accountService.CreateAccount(account);
                    if (res.Status == AccountResult.Success.ToString())
                    {
                        response.Account = res.Account;
                        response.Status = AccountResult.Success.ToString();
                    }
                }


            }
            catch (Exception ex)
            {
                // do nothing
                response.Account = response.Account;
                response.Status = AccountResult.Error.ToString();
                response.Message = string.Format("{0}: {1}", ex.Message, ex.StackTrace);
            }

            return response;
        }

        [HttpGet]
        public AccountResponse Get(string email)
        {

            try
            {
                var account = _accountService.GetAccount(email);
                if (account != null)
                {
                    return new AccountResponse
                    {
                        Account = account,
                        Status = AccountResult.Success.ToString()
                    };
                }

                // do nothing
                return new AccountResponse
                {
                    Status = AccountResult.Error.ToString()
                };

            }
            catch (Exception ex)
            {
                // do nothing
                return new AccountResponse
                {
                    Status = AccountResult.Error.ToString(),
                    Message = string.Format("{0}: {1}", ex.Message, ex.StackTrace)
                };
            }
        }

        [HttpGet]
        public AccountResponse GetById(int id)
        {

            try
            {
                var account = _accountService.GetAccount(id);
                return new AccountResponse
                {
                    Account = account,
                    Status = AccountResult.Success.ToString()
                };

            }
            catch (Exception)
            {
                return new AccountResponse
                {
                    Items = null,
                    Status = AccountResult.Error.ToString()
                };
            }
        }
        
        public string GetHello()
        {
            return "hello";
        }
        
        [HttpGet]
        public AccountResponse GetByAccountIds(string accountIds)
        {
            try
            {
                var list = new List<int>();
                foreach (var id in accountIds.Split(','))
                {
                    int value;
                    int.TryParse(id, out value);
                    if (value > 0) list.Add(value);
                }

                var accounts = _accountService.GetByAccountIds(list);
                return new AccountResponse
                {
                        Items = accounts,
                        Status = AccountResult.Success.ToString()
                    }; 

            }
            catch (Exception)
            {
                return new AccountResponse
                {
                    Items = null,
                    Status = AccountResult.Error.ToString()
                }; 
            }
        }
        

        [HttpGet]
        public AccountResponse GetAccountsByCountry(string countryCode)
        {
            var response = _accountService.GetAccountsByCountry(countryCode);
            try
            {
                return new AccountResponse
                {
                    Items = response.Items, TotalItems = response.Items.Count,
                    Status = AccountResult.Success.ToString()
                };

            }
            catch (Exception)
            {
                return new AccountResponse
                {
                    Items = null,
                    Status = AccountResult.Error.ToString()
                };
            }
        }

        [HttpGet]
        public AccountResponse PagedQuery(int pageNo, int itemsPerPage, string firstName, string lastName, string email, int accountId)
        {
            AccountResponse res;

            try
            {
                res = _accountService.PagedQuery(pageNo, itemsPerPage, firstName, lastName, email,  accountId);

                return res;
            }
            catch (Exception ex)
            {
                // do nothing
                return new AccountResponse()
                {
                    Status = AccountResult.Error.ToString(),
                    Message = string.Format("{0}. {1}", ex.Message, ex.StackTrace)
                };
            }
        }

        
        [HttpPost]
        public AccountResponse Update(Account account)
        {

            var response = new AccountResponse();

            try
            {
                var res = _accountService.UpdateAccount(account);
                if (res.Status == AccountResult.Success.ToString())
                {
                    response.Account = res.Account;
                    response.Status = AccountResult.Success.ToString();
                }
            }
            catch (Exception ex)
            {
                // do nothing
                response.Status = AccountResult.Error.ToString();
                response.Message = string.Format("{0}: {1}", ex.Message, ex.StackTrace);
            }

            return response;
        }

        // This method is updated to remove the account from The DB
        [HttpPost]
        public AccountResponse Delete(Account account)
        {

            var response = new AccountResponse();

            try
            {
                var res = _accountService.RemoveAccountFromDB(account.user_id);
                if (res == AccountResult.Success)
                {
                    response.Status = AccountResult.Success.ToString();
                }
            }
            catch (Exception ex)
            {
                // do nothing
                response.Status = AccountResult.Error.ToString();
                response.Message = string.Format("{0}:{1}", ex.Message, ex.StackTrace);
            }

            return response;
        }

      
        
    }
}