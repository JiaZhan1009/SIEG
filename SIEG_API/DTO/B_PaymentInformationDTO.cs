﻿namespace SIEG_API.DTO
{
    public class B_PaymentInformationDTO
    {
        public int? MemberId { get; set; }
        public string? CreditCard { get; set; }
        public string? CreditCardDate { get; set; }
        public int? CreditCardCCV { get; set; }
        public string? Name { get; set; }
        public string? BillingAddress { get; set; }
        public string? Phone { get; set; }

        public string? Shippingaddress { get; set; }
        public int? BankAccount { get; set; }
        public string? BankCode { get; set; }

        public string? Bankname { get; set; }
    }
}
