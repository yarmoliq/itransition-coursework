using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

using coursework_itransition.Models;
using coursework_itransition.Data;


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

            var user = this._context.Users.Find(coursework_itransition.Utils.GetUserID(this.Context.User));
            if(user == null)
                return;
            newComment.AuthorID = user.Id;

            this._context.Comments.Add(newComment);
            this._context.SaveChanges();

            await this.Clients.All.SendAsync("AddComment", JsonSerializer.Serialize(newComment));
        }
    }
}