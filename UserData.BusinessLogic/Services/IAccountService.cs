using System.Collections.Generic;
using UserData.SharedModels.DataModels;
using UserData.SharedModels.Responses;
using UserData.BusinessLogic.Enums.ServiceResults;
namespace UserData.BusinessLogic.Services
{
    public interface IAccountService
    {
        AccountResponse CreateAccount(Account account);
        AccountResponse UpdateAccount(Account account);
        Account GetAccount(int id);
        Account GetAccount(string email);
        Account GetAccountByAccessToken(string accessToken);
        List<Account> GetByAccountIds(List<int> accountIds);
        AccountResult RemoveAccountFromDB(int accountId);
        AccountResult DeleteFacebook(string email);
        AccountResponse GetAccountsByCountry(string countryCode);
        AccountResponse PagedQuery(int pageNo, int itemsPerPage, string firstName, string lastName, string email, int accountId);
    }

}
