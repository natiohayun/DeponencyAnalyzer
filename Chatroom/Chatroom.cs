using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatroom
{
    public class Chatroom
    {
        public List<User> _users;

        public History _history;
        public Chatroom()
        {
            _users = new List<User>();
            _history = new History();
        }


        public void BrodcastMessage(User user, string message)
        {
            _history.Save(message);
            _users.ForEach(w =>
            {
                if (w != user)
                {
                    w.ReceiveMessage(message);
                }
                else w.history.Save(message);
            });


        }
    }
}
