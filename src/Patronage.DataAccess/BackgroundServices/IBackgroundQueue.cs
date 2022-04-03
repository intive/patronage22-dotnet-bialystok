namespace Patronage.DataAccess.BackgroundServices
{
    public interface IBackgroundQueue<T>
    {
        void Enqueue(T item);
        T? Dequeue();
    }
}
