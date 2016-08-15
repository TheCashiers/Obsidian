using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Obsidian.Domain.Test
{
    public class UserTest
    {
        [Fact]
        public void CreationTest()
        {
            var id = Guid.NewGuid();
            var userName = Guid.NewGuid().ToString();
            var user = User.Create(id, userName);
            Assert.Equal(id, user.Id);
            Assert.Equal(userName, user.UserName);
        }

        [Fact]
        public void VaildatePassword_Success_When_PasswordCorrect()
        {
            var user = new User();
            const string password = "test";
            user.SetPassword(password);
            Assert.True(user.VaildatePassword(password));
        }

        [Fact]
        public void VaildatePassword_Fail_When_PasswordWrong()
        {
            var user = new User();
            user.SetPassword("test");
            Assert.False(user.VaildatePassword("wrong"));
        }
    }
}
