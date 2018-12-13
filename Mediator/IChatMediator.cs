using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator
{
    public interface IChatMediator
    {
        void AddUser(User user);
        void SendMessageToAllUsers(string message, User currentUsr);
    }
}

