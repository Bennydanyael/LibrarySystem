using LibraryAppAPI.Helpers;
using LibraryAppAPI.Requirments;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAppAPI.Handler
{
    public class CustomerBlockedStatusHandler : AuthorizationHandler<CustomerBlockedStatusRequirment>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomerBlockedStatusRequirment requirement)
        {
            var _claim = context.User.FindFirst(c => c.Type == "IsBlocked" && c.Issuer == TokenHelper.Issuer);
            if (!context.User.HasClaim(c => c.Type == "IsBlocked" && c.Issuer == TokenHelper.Issuer))
                return Task.CompletedTask;
            string _value = context.User.FindFirst(c => c.Type == "IsBlocked" && c.Issuer == TokenHelper.Issuer).Value;
            var _blockedStatus = Convert.ToBoolean(_value);
            if (_blockedStatus == requirement.IsBlocked)
                context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
