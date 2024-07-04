namespace NanoNexus.ViewModels
{ public class OrderVm
    {
        public List<CheckOutVm>? CheckOutVms = new List<CheckOutVm>();
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ZipCode { get; set; }
        public string? Note { get; set; }


        public MemberLoginVm? Member { get; set; }
    }
}
