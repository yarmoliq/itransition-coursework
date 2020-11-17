
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using System.Text.Json.Serialization;

using coursework_itransition.Models;
using coursework_itransition.Data;
using Microsoft.EntityFrameworkCore;


namespace coursework_itransition.Hubs
{
    public class CommentsHub : Hub
    {
        private readonly ILogger<CommentsHub> _logger;
        private ApplicationDbContext _context;

        public CommentsHub(ILogger<CommentsHub> looger, ApplicationDbContext context)
        {
            _logger = looger;
            _context = context;
        }

        public async Task<string> GetComments(string compID)
        {
            var comp = await this._context.Compositions
                                    .Include(c => c.Comments)
                                    .FirstOrDefaultAsync(c => c.ID == compID);

            if(comp == null)
                return null;

            return JsonSerializer.Serialize(comp.Comments.ToArray());
        }

        public string GetUserName(string userID)
        {
            var user = this._context.Users.Find(userID);
            if(user == null)
                return null;

            return user.Name;
        }

        public async Task CreateComment(string compID, string contents)
        {
            Comment newComment = new Comment();
            newComment.CompositionID = compID;
            newComment.Contents = contents;
            newComment.CreationDT = newComment.LastEditDT = System.DateTime.UtcNow;
            newComment.AuthorID = coursework_itransition.Utils.GetUserID(this.Context.User);

            this._context.Comments.Add(newComment);
            this._context.SaveChanges();

            await this.Clients.All.SendAsync("AddComment", JsonSerializer.Serialize(newComment));
        }
    }
}