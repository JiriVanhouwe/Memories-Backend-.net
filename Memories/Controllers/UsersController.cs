using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Memories.DTOs;
using Memories.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Memories.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/friends")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] //je moet aangemeld zijn om de endpoints te gebruiken
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public User _loggedInUser;


        public UsersController(IUserRepository context, IMemoryRepository memoRepo, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = context;

            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;

            var id = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            User user = _userRepository.GetByEmail(id);
            _loggedInUser = user;
        }


        //GET api/friends
        /// <summary>
        /// Get a user and his friends.
        /// </summary>
        /// <returns>The user his friends. </returns>
        [HttpGet]
        public ActionResult<UserDTOWithFriends> GetUserAndFriends()
        {
            User user = _loggedInUser;

            if (user == null)
                return NotFound();

           List<User> friends = new List<User>();

            if (user.FriendsWith != null)
            {
                user.FriendsWith.ForEach(friend => friends.Add(_userRepository.GetById(friend.FriendWithId)));
            }              

            UserDTOWithFriends userWithFriends = new UserDTOWithFriends(user.FirstName, user.LastName, user.Email, friends);

            return userWithFriends;
        }

        //POST api/friends
        /// <summary>
        /// If email not known: invite and return true. If email known: return false.
        /// </summary>
        /// <param name="invite">The email of someone.</param>
        /// <returns>True if the email is send, false if the user already exists. </returns>
        [HttpGet("{invite}")]
        public ActionResult<string> InviteFriend(string invite)
        {
            User user = _userRepository.GetByEmail(invite);

            if (user != null)
                return "Dit e-mailadres is reeds geregistreerd.";

            if (SendEmail(invite))
                return "De uitnodiging werd verzonden.";
                 else   
            return "Controleer of je een bestaand e-mailadres opgaf.";
        }

        //DELETE api/friends
        /// <summary>
        /// Delete a friend of the user's friendslist.
        /// </summary>
        /// <param name="email">This user will be deleted of the friendslist.</param>
        /// <returns>If it worked.</returns>
        [HttpDelete]
        public ActionResult<string> DeleteFriend(string email)
        {
            User user = _userRepository.GetByEmail(email);

            if (user == null)
                return "Er ging iets mis";

            _loggedInUser.RemoveFriend(user);
            _userRepository.SaveChanges();
            return email + " werd uit je vriendelijst verwijderd.";
        }

        //PUT api/friends
        /// <summary>
        /// Add a friend.
        /// </summary>
        /// <param name="email">Email of the friend to add.</param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult<string> AddFriend(string email)
        {
            User user = _userRepository.GetByEmail(email);

            if (user == null)
                return email + " kennen we nog niet. Nodig hem of haar gerust uit!";

            if (_loggedInUser.AreFriends(user))
                return "Jullie zijn al vrienden.";

            _loggedInUser.AddFriend(user);
            _userRepository.SaveChanges();
            return email + " werd toegevoegd aan jouw vriendenlijst.";
        }


        private bool SendEmail(string email)
        {
            if (IsValidEmail(email))
            {
                MailAddress to = new MailAddress(email);
                MailAddress from = new MailAddress("memories.invitation@gmail.com");

                MailMessage message = new MailMessage(from, to);
                message.Subject = "Uitnodiging Memories";
                message.Body = String.Format("Beste, \n\n{0} nodigt je uit om fijne herinneringen te delen via de Memories applicatie. Neem gerust een kijkje op memories.be.\n\nTot binnenkort,\nHet Memories-team\n", _loggedInUser.FirstName + " " + _loggedInUser.LastName); //TODO aanpassen zodat de ingelogde persoon zijn naam erbij staat

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("memories.invitation@gmail.com", "M3m0r13s1@")
                };

                try
                {
                    smtp.Send(message);
                }
                catch (SmtpException ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                return true;
            }
            else
            {
                Console.WriteLine("Emailadres van een foutief formaat.");
                return false;
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }


    }
}