namespace Tests.Chapter07.EfClasses
{
    public enum PTypes : byte { Cash = 1, Card = 2 }
    public abstract class Payment
    {
        public int PaymentId { get; set; }

        public PTypes PType { get; set; }

        public decimal Amount { get; set; }
    }
}
