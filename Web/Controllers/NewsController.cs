using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceLayer.Interfaces;
using DataLayer.Context;
using System.Data.Entity.Validation;
using DomainClass.Models;
using ServiceLayer.Models;
using System.IO;

namespace Web.Controllers
{
    [OverrideAuthorization]
    public partial class NewsController : Controller
    {
        IUnitOfWork _uow;//db
        INewsService _news;
        IPersonelService _personel;
        IApplicationUserManager _user;
        ICommentService _comments;
        public NewsController(IUnitOfWork uow, INewsService news, IPersonelService personel, ICommentService comments,
         IApplicationUserManager user)
        {
            _uow = uow;
            _user = user;
            _personel = personel;
            _news = news;
            _comments = comments;

        }
        // GET: Admin/News
        public virtual ActionResult Index()
        {
            return View(_news.GetAll().Where(ll => ll.IsDeleted == false && ll.ParentId != null).Select(
                q => new NewsViewModel
                {
                    IsAuthenticated = q.IsAuthenticated,
                    MasterPicPathThumb = q.MasterPicPathThumb,
                    NewsBody = q.NewsBody,
                    NewsId = q.NewsId,
                    Dislike = q.Dislike,
                    HeadTitle = q.HeadTitle,
                    Like = q.Like,
                    PublishDate = q.ModifiedDate,
                    Visit = q.Visit,
                    NewsTitle = q.NewsTitle
                }
                ));
        }
        public virtual ActionResult NewsDetail(int id)
        {
            var q = _news.Find(id);
            NewsViewModel newsVM = new NewsViewModel
            {
                IsAuthenticated = q.IsAuthenticated,
                MasterPicPathThumb = q.MasterPicPathThumb,
                NewsBody = q.NewsBody,
                NewsId = q.NewsId,
                Dislike = q.Dislike,
                HeadTitle = q.HeadTitle,
                Like = q.Like,
                PublishDate = q.ModifiedDate,
                Visit = q.Visit,
                CommentsCounts = 100,
                NewsTitle = q.NewsTitle
                //ParentId = (int)q.ParentId
            };
            NewsDetailViewModel newsDetailViewModel = new NewsDetailViewModel();
            newsDetailViewModel.NewsViewModel = newsVM;
            var comments = _comments.GetAll().Where(ll => ll.NewsID == newsVM.NewsId);
            newsDetailViewModel.CommentViewModel = comments;
            //foreach (var item in comments)
            //{
            //    CommentViewModel commentVM = new CommentViewModel
            //    {
            //        Confirm = item.Confirm,
            //      //  Date = item.Date,
            //        Dislike = item.Dislike,
            //        Email = item.Email,
            //        ID = item.ID,
            //        IP = item.IP,
            //        IsDeleted = item.IsDeleted,
            //        Like = item.Like,
            //        Name = item.Name,
            //        NewsID = item.NewsID,
            //        ParentID = item.ParentID,
            //        Read = item.Read,
            //        Text = item.Text,
            //        Title = item.Title
            //    };
            //    newsDetailViewModel.CommentViewModel.(commentVM);
            //}

            return View(newsDetailViewModel);
        }
        [HttpPost]
        public virtual JsonResult InsertComment(Comments t)
        {
            int resCode = 0;
            t.Confirm = true;
            t.Date = DateTime.Now.Date;
            t.Dislike = 0;
            t.Like = 0;
            //t.ParentID = 0;
            t.Read = false;
            t.IP = Request.UserHostAddress;
            t.NewsID = t.NewsID;
            _comments.Add(t);
            try
            {
                _uow.SaveAllChanges();
                resCode = 1;
                return Json(new { ret = "true", codeid = resCode, message = "نظر با موفقیت ثبت شد و پس از تایید توسط مدیران منتشر خواهد شد" });
                //ViewBag.Style = "color:green;";
                //ViewBag.Message = "نظر با موفقیت ثبت شد و پس از تایید توسط مدیران منتشر خواهد شد";
            }
            catch
            {
                return Json(new { ret = "false", codeid = resCode, message = "خطا امکان ثبت نظر وجود ندارد" });

                //ViewBag.Style = "color:red;";
                //ViewBag.Message = "نظر ثبت نشد";

            }
        }
        //    var q = _news.Find(t.NewsID);
        //    NewsViewModel newsVM = new NewsViewModel
        //    {
        //        IsAuthenticated = q.IsAuthenticated,
        //        MasterPicPathThumb = q.MasterPicPathThumb,
        //        NewsBody = q.NewsBody,
        //        NewsId = q.NewsId,
        //        Dislike = q.Dislike,
        //        HeadTitle = q.HeadTitle,
        //        Like = q.Like,
        //        PublishDate = q.ModifiedDate,
        //        Visit = q.Visit,
        //        CommentsCounts = 100,
        //        NewsTitle = q.NewsTitle
        //        //ParentId = (int)q.ParentId
        //    };
        //    NewsDetailViewModel newsDetailViewModel = new NewsDetailViewModel();
        //    newsDetailViewModel.NewsViewModel = newsVM;
        //    newsDetailViewModel.CommentViewModel = _comments.GetAll();
        //    //return RedirectToAction("NewsDetail", new { id = t.NewsID });
        //    return View("NewsDetail", newsDetailViewModel);
        //}
        //catch
        //{

        //    return Content("Error");
        //}

        [ChildActionOnly]
        //  [HttpPost]
        public virtual ActionResult FillComments(int newsId = 9)
        {
            var Comments = _comments.GetAll().Where(ll => ll.NewsID == newsId);
            return PartialView("_CommentPartial", Comments);
        }

        [HttpPost]
        public virtual JsonResult NewsLike(int id)
        {
            var news = _news.Find(id);
            news.Like++;
            try
            {
                _uow.SaveAllChanges();
                return Json(new { ret = "true", message = "ممنون", like = news.Like });
            }
            catch (Exception)
            {

                news.Like--;
                return Json(new { ret = "false", message = "سپاس", like = news.Like });
            }

        }
        [HttpPost]
        public virtual JsonResult NewsDisLike(int id)
        {
            var news = _news.Find(id);
            news.Like--;
            try
            {
                _uow.SaveAllChanges();
                return Json(new { ret = "true", message = "ممنون", like = news.Like });
            }
            catch (Exception)
            {

                news.Like++;
                return Json(new { ret = "false", message = "سپاس", like = news.Like });
            }

        }
        private void PopulateNewsGroups()
        {
            ViewBag.ParentId = new SelectList(_news.GetAll().Where(ll => ll.IsDeleted == false && ll.ParentId == null), "NewsId", "NewsTitle");
        }


    }
}