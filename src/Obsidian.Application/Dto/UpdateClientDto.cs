﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Application.Dto
{
    public class UpdateClientDto
    {
        public Guid ClientId { get; set; }
        public string DisplayName { get; set; }
        public string RedirectUri { get; set; }
    }
}
