using System;
using System.Linq;
using UserData.SharedModels.DataModels;
using UserData.SharedModels.Responses;
using UserData.BusinessLogic.Enums.ServiceResults;
using UserData.BusinessLogic.UnitOfWork;
using UserData.Data.Repositories;

namespace UserData.BusinessLogic.Services
{
   
    public class CommentsService : ICommentsService
    {

        private readonly IUnitOfWorkProvider _unitOfWorkProvider;
        private readonly ILogger _logger;
        const string CommentsSelectSql = "SELECT * from comments ";


        public CommentsService(IUnitOfWorkProvider uowp, ILogger logger)
        {
            _unitOfWorkProvider = uowp;
            _logger = logger;
        }

        public CommentsResponse UpdateComment(Comments comment)
        {
            var replyResponse = new CommentsResponse();

            using (var uow = _unitOfWorkProvider.GetUnitOfWork())
            {
                // get account from repo
                var replyFromDb = GetComment(uow.Repository, comment.comment_id);
                replyFromDb.comment.comments_is_published = comment.comments_is_published;
                replyFromDb.comment.comment_reported = comment.comment_reported;

                try
                {
                    var res = uow.Repository.Update(replyFromDb.comment);

                    if (res > 0)
                    {
                        uow.Commit();
                        replyResponse.comment = replyFromDb.comment;
                        replyResponse.Status = CommentsResult.Success.ToString();
                        return replyResponse;
                    }
                }
                catch (Exception ex)
                {
                    // do some logging
                    _logger.Log(ex);
                    throw;
                }
            }

            replyResponse.Status = CommentsResult.Error.ToString();
            return replyResponse;
        }
        public CommentsResponse CreateComment(Comments comment)
        {

            var response = new CommentsResponse();

            using (var uow = _unitOfWorkProvider.GetUnitOfWork())
            {
                try
                {

                        comment.comments_created = DateTime.Now;
                    
                    //  recipeReview.Updated = recipeReview.Created;
                    var res = uow.Repository.Insert(comment);
                    if (res > 0)
                    {
                        uow.Commit();
                        response.comment = comment;
                        response.Status = CommentsResult.Success.ToString();
                        return response;
                    }
                }
                catch (Exception ex)
                {
                    // do some logging
                    _logger.Log(ex);
                    response.Status = CommentsResult.Error.ToString();
                    response.Message = string.Format("{0}: {1}", ex.Message, ex.StackTrace);

                    throw;
                }
            }
            response.Status = CommentsResult.Error.ToString();
            return response;
        }
        
        public CommentsResponse GetComment(int id)
        {
            using (var repository = _unitOfWorkProvider.GetRepository())
            {
                try
                {
                    var res = GetComment(repository, id);

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


        public CommentsResponse PagedQuery(int pageNo, int itemsPerPage)
        {
            var response = new CommentsResponse();
            var sql = CommentsSelectSql + " Where Node_id=@0 ORDER BY comments_created DESC";
            using (var repository = _unitOfWorkProvider.GetRepository())
            {
                var res = repository.PagedQuery<Comments>(pageNo, itemsPerPage, sql);
                response.Items = res.Items;
                response.TotalItems = (int)res.TotalItems;
                response.Status = CommentsResult.Success.ToString();
                return response;
            }
        }
        
        protected CommentsResponse GetCommentByUserIdAndNodeId(IRepository repository, int Node_id,int UserId)
        {
            var res = repository.Fetch<Comments>(CommentsSelectSql + "WHERE Node_Id = @0 AND comments_UserId=@1", Node_id,UserId);
            return new CommentsResponse()
            {
                Items = res
            };
        }
  
        protected CommentsResponse GetComment(IRepository repository, int id)
        {
            var res = repository.Fetch<Comments>(CommentsSelectSql+"WHERE comment_id = @0", id).SingleOrDefault();
            return new CommentsResponse()
            {
                comment = res
            };
        }


        public CommentsResult DeleteCommentById(int comment_id)
        {
            using (var uow = _unitOfWorkProvider.GetUnitOfWork())
            {
                try
                {
                    var res = uow.Repository.ExecuteScalar<Comments>("DELETE from comments WHERE comment_id = @0", comment_id);
                    uow.Commit();
                    return CommentsResult.Success;

                }
                catch (Exception ex)
                {
                    // do some logging
                    _logger.Log(ex);
                    throw;

                }
            }
            return CommentsResult.Error;
        }




    


        

      
    }
}