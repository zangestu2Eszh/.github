namespace IraqWebsite.AuthManager.Response
{
    public class ManagerResponse
    {
        public string? Message { get; set; }
        public bool IsSuccess { get; set; }
        public List<string>? Errors { get; set; }
        public List<object>? ListMessage { get; set; }

        public static ManagerResponse Response(string Message, bool IsSuccess, List<string> Errors, List<object> ListMessage)
        {
            try
            {
                return new ManagerResponse
                {
                    IsSuccess = IsSuccess,
                    Errors = Errors,
                    Message = Message,
                    ListMessage = ListMessage
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
