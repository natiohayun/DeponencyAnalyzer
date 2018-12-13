using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ChatMediator : IChatMediator
{
    private List<User> _users;
    public History _history;
    public ChatMediator()
    {
        _users = new List<User>();
        _history = new History();
    }

    public void AddUser(User user)
    {
        _users.Add(user);
    }

    public void SendMessageToAllUsers(string message, User user)
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
