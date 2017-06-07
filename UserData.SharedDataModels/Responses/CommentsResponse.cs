using System.Collections.Generic;
using UserData.SharedModels.DataModels;


namespace UserData.SharedModels.Responses
{
    public class CommentsResponse
    {
        public Comments comment { get; set; }
        public List<Comments> Items { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }

        // Paging
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
