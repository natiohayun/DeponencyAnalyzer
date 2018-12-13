using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator
{
    public abstract class User
    {
        IChatMediator chatMediator;
        public History history;

        private string name;

        public User(IChatMediator chatMediator, string name)
        {
            this.chatMediator = chatMediator;
            this.name = name;
            history = new History();
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public IChatMediator ChatMediator
        {
            get
            {
                return chatMediator;
            }
        }

        public abstract void SendMessage(string message);
        public abstract void ReceiveMessage(string message);
    }
}
