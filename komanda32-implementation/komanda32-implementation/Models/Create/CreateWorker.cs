namespace komanda32_implementation.Models.Create
{
    public class CreateWorker
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public decimal? Employment { get; set; }
        public string BankAccount { get; set; }
        public WorkerType? Type { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
