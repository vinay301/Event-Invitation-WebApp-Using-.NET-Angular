namespace EventInvitationWebApp.Models.DTO
{
    public class ResponseDto
    {
        //To store result, whether it is a single object or a list
        public object? Result { get; set; }
        //Denote whether the request was succesfull or not
        public bool IsSuccess { get; set; } = true;
        //For Error/Success Message
        public string Message { get; set; } = "";
    }
}
