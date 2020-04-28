using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Memories.DTOs;
using Memories.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Memories.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/friends")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMemoryRepository _memoryRepository;

        public UsersController(IUserRepository context, IMemoryRepository memoRepo)
        {
            _userRepository = context;
            _memoryRepository = memoRepo;
        }

        //GET api/friends/id
        /// <summary>
        /// Get a user and friends and memories with a given id.
        /// </summary>
        /// <param name="id">The email of a user.</param>
        /// <returns>The user + memories + friends. </returns>
        [HttpGet("{id}")]
        public ActionResult<UserDTOWithFriends> GetUserAndFriends(int id)
        {
            User user = _userRepository.GetById(id);

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

        //POST api/friends/
        /// <summary>
        /// If email not known: invite and return true. If email known: return false.
        /// </summary>
        /// <param name="email">The email of someone.</param>
        /// <returns>True if the email is send, false if the user already exists. </returns>
        [HttpGet]
        public ActionResult<string> InviteUser(string email)
        {
            User user = _userRepository.GetByEmail(email);

            if (user != null)
                return "Dit e-mailadres is reeds geregistreerd.";

            if (SendEmail(email))
                return "De uitnodiging werd verzonden.";
                 else   
            return "Controleer of je een bestaand e-mailadres opgaf.";
        }

        private bool SendEmail(string email)
        {
            if (IsValidEmail(email))
            {
                MailAddress to = new MailAddress(email);
                MailAddress from = new MailAddress("hetitlab@gmail.com");

                MailMessage message = new MailMessage(from, to);
                message.Subject = "Beste";
                message.Body = "We nodigen je graag uit om de Memories-applicatie te gebruiken! Groeten, Jiri"; //TODO aanpassen zodat de ingelogde persoon zijn naam erbij staat

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("hetitlab@gmail.com", "admin2020")
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

        /*
        //POST api/friends
        /// <summary>
        /// Add a new user.
        /// </summary>
        /// <param name="user">The new user.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<User> CreateUser(UserDTO user)
        {
            User userToCreate = new User() {FirstName = user.FirstName, LastName = user.LastName, Email = user.Email };
            _userRepository.Add(userToCreate);
            _userRepository.SaveChanges();

            return CreatedAtAction(nameof(GetUserAndFriends), new { id = userToCreate.UserId }, userToCreate);
        }*/


    }
}