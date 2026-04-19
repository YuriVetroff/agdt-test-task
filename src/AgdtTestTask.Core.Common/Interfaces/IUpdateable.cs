namespace AgdtTestTask.Core.Common.Interfaces
{
    public interface IUpdateable
        : ICreateable
    {
        public DateTime? UpdatedAt { get; set; }
    }
}
