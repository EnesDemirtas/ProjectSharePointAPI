using PSP.Application.Enums;

namespace PSP.Application.Models
{

    public class Error
    {
        public ErrorCode Code { get; set; }
        public string Message { get; set; }
    }
}