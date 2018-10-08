using DriverExpansesTracker.Repository.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DriverExpansesTracker.Repository.Entities
{
    public class ExpiredToken: Entity
    {
        private ExpiredToken()
        {

        }
        public ExpiredToken(string value)
        {
            Value = value;
        }
        public string Value { get; private set; }
    }
}
