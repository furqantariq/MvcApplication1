using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcApplication1.Models
{
    public interface IUserRepository
    {
        Account isUserExist(Account user);
        Account Add(Account user);
        Account ChangeUser(Account user);
        bool check(String username);
    }
}
