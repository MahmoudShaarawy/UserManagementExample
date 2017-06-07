using System;
using PetaPoco;
using System.Collections.Generic;

namespace UserData.SharedModels.DataModels
{
    [PrimaryKey("comment_id", autoIncrement = true),TableName("comments")]
    public class Comments
    {
        public int comment_id { get; set; }
        public int comments_UserId { get; set; }
        public string CommentText { get; set; }
        public DateTime comments_created { get; set; }
        public int comments_Node_Id { get; set; }
        public bool comments_is_published { get; set; }
        public bool comment_reported { get; set; }
    }
    
}
