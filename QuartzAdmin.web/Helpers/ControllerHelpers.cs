using System;
using System.Collections.Generic;
using System.Linq;
using System.Web; 
using System.Web.Mvc;
using QuartzAdmin.web.Models;

namespace QuartzAdmin.web.Helpers
{
    public static class ControllerHelpers
    {
        public static void AddRuleViolations(this ModelStateDictionary modelState, IEnumerable<RuleViolation> ruleViolations)
        {
            foreach (RuleViolation ruleViolation in ruleViolations)
            {
                modelState.AddModelError(ruleViolation.PropertyName ?? Guid.NewGuid().ToString(), ruleViolation.ErrorMessage);
            }
        }
    }
}
