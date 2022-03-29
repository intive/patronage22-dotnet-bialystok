namespace Patronage.Common
{
    public interface IEntity
    {
        public string Alias { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}