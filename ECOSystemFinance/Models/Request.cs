using System;
using System.Collections.Generic;
using System.Text;

namespace ECOSystemFinance.Models
{
    internal class Request
    {
        public Request(string ClientId, string DeductionAccount, string secondAccount, string thirdAccount, int ProgramId, int itemId) { 
            this.ClientId = ClientId;
            this.DeductionAccount = DeductionAccount;
            this.secondAccount = secondAccount;
            this.thirdAccount = thirdAccount;
            this.ProgramId = ProgramId;
            StatusId = 1;
            UserTypeId = 1;
            this.itemId = itemId;
        }

        
        public string ClientId { get; set; }
        public string DeductionAccount { get; set; }
        public int StatusId { get; set; }

        public int UserTypeId { get; set; }
        public int ProgramId { get; set; }
        public string secondAccount { get; set; }
        public string thirdAccount { get; set; }
        public int itemId { get; set; }
    }
}
