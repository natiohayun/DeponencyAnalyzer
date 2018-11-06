using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatroom
{
    public abstract class User
    {
        public string name;

        public History history;

        public Chatroom chatroom;

        public User(Chatroom chatroom)
        {
            history = new History();
            this.chatroom = chatroom;
        }
        public abstract void ReceiveMessage(string message);

        public abstract void SendMessage(string message, List<User> users);
    }
}
