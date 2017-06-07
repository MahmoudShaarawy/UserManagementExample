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
    /// CRUD web API controller for website RecipeReviews
    /// </summary>
    public class CommentsController : ApiController
    {
        private readonly ICommentsService _commentsService;
        public CommentsController(ICommentsService commentsService)
        {
            _commentsService = commentsService;
        }


        [HttpPost]
        public CommentsResponse Create(Comments comment)
        {
            var response = new CommentsResponse();
            try
            {
                    var res = _commentsService.CreateComment(comment);
                    if (res.Status == CommentsResult.Success.ToString())
                    {
                        response.comment = res.comment;
                        response.Status = CommentsResult.Success.ToString();
                    }
               


            }
            catch (Exception ex)
            {
                // do nothing
                response.comment = response.comment;
                response.Status = CommentsResult.Error.ToString();
                response.Message = string.Format("{0}: {1}", ex.Message, ex.StackTrace);
            }

            return response;
        }



        [HttpPost]
        public CommentsResponse Update(Comments comment)
        {
            var response = new CommentsResponse();
            try
            {
                var res = _commentsService.UpdateComment(comment);
                if (res.Status == CommentsResult.Success.ToString())
                {
                    response.comment = res.comment;
                    response.Status = CommentsResult.Success.ToString();
                }



            }
            catch (Exception ex)
            {
                // do nothing
                response.comment = response.comment;
                response.Status = CommentsResult.Error.ToString();
                response.Message = string.Format("{0}: {1}", ex.Message, ex.StackTrace);
            }

            return response;
        }
        [HttpGet]
        public CommentsResponse GetCommentById(int id)
        {
            try
            {
                var comment = _commentsService.GetComment(id);
                return new CommentsResponse
                {
                    comment = comment.comment,
                    Status = CommentsResult.Success.ToString()
                };

            }
            catch (Exception)
            {
                return new CommentsResponse
                {
                    Items = null,
                    Status = CommentsResult.Error.ToString()
                };
            }
        }

        public string GetHello()
        {
            return "hello";
        }

     

        [HttpPost]
        public string DeleteCommentById(int comment_id)
        {

            try
            {
                var comments = _commentsService.DeleteCommentById(comment_id);
                if (comments != null)
                {
                    return comments.ToString();
                }

                // do nothing
                return CommentsResult.Error.ToString();
               

            }
            catch (Exception ex)
            {
                // do nothing
                return CommentsResult.Error.ToString();
            }
        }


        [HttpGet]
        public CommentsResponse PagedQuery(int pageNo, int itemsPerPage)
        {
            CommentsResponse res;
            
            try
            {
                res = _commentsService.PagedQuery(pageNo, itemsPerPage);

                return res;
            }
            catch (Exception ex)
            {
                // do nothing
                return new CommentsResponse()
                {
                    Status = CommentsResult.Error.ToString(),
                    Message = string.Format("{0}. {1}", ex.Message, ex.StackTrace)
                };
            }
        }
        

        
    }
}