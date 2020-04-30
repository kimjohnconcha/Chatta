using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Chatta.Services
{
    public interface IUserService
    {
        bool TestBool(string n);
    }

    public class UserService : IUserService
    {
        public bool TestBool(string n)
        {
            if (n == "mik")
            {
                return true;
            }
            else
                return false;
        }
    }
}

