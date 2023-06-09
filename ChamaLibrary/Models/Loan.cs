﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamaLibrary.Models
{
    public class Loan
    {
        //LoanTypeId,PrincipleAmount,InterestRate,InterestAmount,RepayPeriod,RepayPeriodType
        //PeriodicRepayAmount,PeriodicPenaltyAmount,TotalAmount
        public int Id { get; set; }
        public int LoanTypeId { get; set; }
        public int PaymentId { get; set; }
        public decimal PrincipleAmount { get; set; }
        public decimal InterestRate { get; set; }
        public decimal InterestAmount { get; set; }
        public int RepayPeriod { get; set; }
        public string RepayPeriodType { get; set; }
        public decimal PeriodicRepayAmount { get; set; }
        public decimal PeriodicPenaltyAmount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
