﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Forums.Models;
using Microsoft.EntityFrameworkCore;
using Forums.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace Forums.Controllers
{

    public class PostsController : Controller
    {
        private ForumsDbContext db = new ForumsDbContext();
        public IActionResult Index(int id)
        {
            var model = new PostsViewModel();
            model.topic = db.Topics.FirstOrDefault(topic => topic.TopicId == id);
            model.Posts = db.Posts.Include(posts => posts.Topic).Where(topics => topics.TopicId == id).ToList();
            return View(model);
        }

        [Authorize]
        public IActionResult Create()
        {
            ViewBag.TopicId = new SelectList(db.Topics, "TopicId", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Post post)
        {
            db.Posts.Add(post);
            db.SaveChanges();
            return RedirectToAction("Create");
        }
    }
}
