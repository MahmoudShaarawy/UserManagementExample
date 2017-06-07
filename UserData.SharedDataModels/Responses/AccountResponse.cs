using System.Collections.Generic;
using UserData.SharedModels.DataModels;

namespace UserData.SharedModels.Responses
{
    public class AccountResponse
    {
        public Account Account { get; set; }
        public List<Account> Items { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public bool Deleted { get; set; }

        // Paging
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}