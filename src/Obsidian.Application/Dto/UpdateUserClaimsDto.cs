using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Obsidian.Application.Dto
{
    public class UpdateUserClaimsDto
    {
        public IList<ClaimDto> Claims { get; set; }
    }
    public class ClaimDto
    {
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}
