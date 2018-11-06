using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatroom
{
    public class PremiumUser : User
    {

        public PremiumUser(string name, Chatroom chatroom)
            : base(chatroom)
        {
            this.name = name;
            chatroom._users.Add(this);
       

        }

        public override void ReceiveMessage(string message)
        {
            Console.WriteLine(name + " received message: " + message);
        }

        public override void SendMessage(string message, List<User> users)
        {
            chatroom.BrodcastMessage(this, message);
        }
    }
}
