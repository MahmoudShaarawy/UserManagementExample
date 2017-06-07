using System;
using System.Collections.Generic;
using System.Linq;
using UserData.SharedModels.DataModels;
using UserData.SharedModels.Responses;
using UserData.BusinessLogic.Enums.ServiceResults;
namespace UserData.BusinessLogic.Services
{
    public interface ICommentsService
    {
        CommentsResponse CreateComment(Comments comment);
        CommentsResponse UpdateComment(Comments comment);
        CommentsResponse GetComment(int id);
        CommentsResult DeleteCommentById(int comment_id);
        CommentsResponse PagedQuery(int pageNo, int itemsPerPage);

    }

}
