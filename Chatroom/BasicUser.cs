using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatroom
{
    public class BasicUser : User
    {

        private History hist;
    
        public BasicUser(string name, Chatroom chatroom)
            : base(chatroom)
        {
            this.name = name;
            chatroom._users.Add(this);
        }


        public override void ReceiveMessage(string message)
        {
            Chatroom chatroom = new Chatroom();
            Console.WriteLine(name + " received message: " + message);
        }



        public override void SendMessage(string message, List<User> users)
        {
            Console.WriteLine("This user is a basic user and cant send message");
        }
    }
}
