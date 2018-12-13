using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator
{
    class Program
    {
        static void Main(string[] args)
        {
            IChatMediator chatMediator = new ChatMediator();

            //3 users are online;
            User ryan = new BasicUser(chatMediator, "Ryan");
            User michael = new PremiumUser(chatMediator, "Michael");
            User james = new PremiumUser(chatMediator, "James");
            chatMediator.AddUser(ryan);
            chatMediator.AddUser(michael);
            chatMediator.AddUser(james);


            //dana added to the chat room
            User dana = new BasicUser(chatMediator, "Dana");
            chatMediator.AddUser(dana);

            dana.SendMessage("Dana is online.");
            //Basic User: Ryan receive message: Dana is online.
            //Premium User: Michael receive message: Dana is online.
            //Premium User: James receive message: Dana is online.
        }
    }
}
