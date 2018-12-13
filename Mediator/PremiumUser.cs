using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator
{
    public class PremiumUser : User
    {
        public PremiumUser(IChatMediator chatMediator, string name)
            : base(chatMediator, name)
        {
        }

        public override void SendMessage(string message)
        {
            this.ChatMediator.SendMessageToAllUsers(message, this);
        }

        public override void ReceiveMessage(string message)
        {
            Console.WriteLine("Premium User: " + this.Name + " receive message: " + message);
        }
    }

}
