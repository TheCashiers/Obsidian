﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Application.ProcessManagement
{
    public class Result<TCommand>
    {
        public bool Succeed { get; set; }
        public string Message { get; set; }
    }
}