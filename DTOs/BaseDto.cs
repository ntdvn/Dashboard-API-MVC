namespace DashboardMVC.DTOs
{
    public class BaseDto
    {
        public bool Status { get; set; } = false;
        public object Data { get; set; }
        public string[] Message { get; set; } = new string[] { };
    }
}