using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebReportMessageService
{
    public class IncidentReport
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public bool AssistanceNeeded { get; set; }
        public DateTime FixationAt { get; set; }
        public AttackVector AttackVector { get; set; }
        public TypeOfAttack TypeOfAttack { get; set; }
    }

    public enum AttackVector
    {
        INT,
        EXT
    }

    public enum TypeOfAttack
    {
        TRAFFICHIJACK,
        MALWARE,
        SOCIALENGINEERING,
        DDOSATTACKS,
        ATMATTACKS,
        VULNERABILITIES,
        BRUTEFORCES,
        SPAMS,
        CONTROLCENTERS,
        SIM,
        PHISHINGATTACKS,
        PROHIBITEDCONTENTS,
        MALICIOUSRESOURCES,
        CHANGECONTENTS,
        SCANPORTS,
        OTHER
    }
}
