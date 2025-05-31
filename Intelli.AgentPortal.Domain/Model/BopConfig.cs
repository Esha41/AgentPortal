using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class BopConfig
    {
        public int Id { get; set; }
        public string Setting { get; set; }
        public string EnumValue { get; set; }
        public string Value { get; set; }
    }
}
