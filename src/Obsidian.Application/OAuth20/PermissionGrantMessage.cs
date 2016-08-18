﻿using System;
using Obsidian.Application.ProcessManagement;

namespace Obsidian.Application.OAuth20
{
    public class PermissionGrantMessage : Message<PermissionGrantResult>
    {
        public PermissionGrantMessage(Guid sagaId) : base(sagaId)
        {
        }

        public bool PermissionGranted { get; set; }
    }
}