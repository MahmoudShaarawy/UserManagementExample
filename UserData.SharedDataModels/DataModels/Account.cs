using System;
using PetaPoco;

namespace UserData.SharedModels.DataModels
{
    [PrimaryKey("user_id", autoIncrement = true), TableName("Account")]
    public class Account
    {
        public int user_id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CultureCode { get; set; }
        public string Password { get; set; } // plain text

        public string Avatar { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public bool Deleted { get; set; }
        public bool NewsLetter { get; set; }

        public bool OptInA { get; set; }
        public bool OptInB { get; set; }

        public string FacebookId { get; set; }
        public bool AdditionalTerms1 { get; set; }
        public bool AdditionalTerms2 { get; set; }
        public bool AdditionalTerms3 { get; set; }

        public string AccessToken { get; set; }
        public string PasswordToken { get; set; }
    }
}