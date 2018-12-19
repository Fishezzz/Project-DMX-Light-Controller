using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registry
{
    class Account
    {
        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        private string passWord;
        public string PassWord
        {
            get { return passWord; }
            set { passWord = value; }
        }
    }
}
