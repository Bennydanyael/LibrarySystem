using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAppAPI.Requirments
{
    public class CustomerBlockedStatusRequirment : IAuthorizationRequirement
    {
        public bool IsBlocked { get; }
        public CustomerBlockedStatusRequirment(bool _isBlocked)
        {
            IsBlocked = _isBlocked;
        }
    }
}
