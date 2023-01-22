namespace SIEG_API.DTO
{
    public class J_AddSellrrOrder
    {
        public int bID { get; set; }
        public int sID { get; set; }
        public int pID { get; set; }
        public int pPrice { get; set; }
        public string pImg { get; set; }   
        public string pay { get; set; }
        public string receiver { get; set; }
        public string receivingPhone { get; set; }
        public string shippingAddress { get; set; }
    }
}
