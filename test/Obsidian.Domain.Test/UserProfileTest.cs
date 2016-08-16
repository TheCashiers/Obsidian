using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Obsidian.Domain.Test
{
    public class UserProfileTest
    {
        [Theory]
        [InlineData("xxx")]
        [InlineData("xxx@")]
        [InlineData("xxx@xxx")]
        [InlineData("@xxx.com")]
        [InlineData("xxx.ccc")]
        public void SetEmail_Fail_When_EmailIncorrect(string value)
        {
            var profile = new UserProfile();
            Assert.Throws<ArgumentException>("value", () => profile.EmailAddress = value);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void SetEmail_Fail_When_EmailNull(string value)
        {
            var profile = new UserProfile();
            Assert.Throws<ArgumentNullException>("value", () => profile.EmailAddress = value);
        }

        [Theory]
        [InlineData("xxx@yyy.com")]
        [InlineData("xxx@101.com")]
        [InlineData("xxx@xxx.yyy.edu.cn")]
        public void SetEmail_Success_When_EmailCorrect(string value)
        {
            var profile = new UserProfile();
            profile.EmailAddress = value;
            Assert.Equal(value, profile.EmailAddress);
        }
    }
}
