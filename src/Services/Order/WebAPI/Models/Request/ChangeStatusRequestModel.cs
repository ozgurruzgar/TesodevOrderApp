namespace WebAPI.Models.Request
{
    public class ChangeStatusRequestModel
    {
        public Guid Id { get; set;}
        public string Status { get; set; }
    }
}
