namespace Patronage.DataAccess
{
    public class BaseResponseError
    {
        public string PropertyName { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }
    }
}