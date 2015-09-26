using System.Collections.Generic;

namespace SYDQ.Infrastructure.Domain
{
    public abstract class EntityBase
    {
        private List<BusinessRule> _brokenRules = new List<BusinessRule>();

        protected abstract void Validate();

        public IEnumerable<BusinessRule> GetBrokenRules()
        {
            _brokenRules.Clear();
            Validate();
            return _brokenRules;
        }

        protected void AddBrokenRule(BusinessRule businessRule)
        {
            _brokenRules.Add(businessRule);
        }
    }

}
