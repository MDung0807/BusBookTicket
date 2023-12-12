namespace BusBookTicket.Auth.DTOs.Responses
{
    public class AccResponse
    {
        public int Id { get; set; }
        public string username { get; set; }
        public string roleName { get; set; }
        public int status { get; set; }

        public AccResponse(int userID, string username, string role)
        {
            this.Id = userID;
            this.username = username;
            this.roleName = role;
        }

        public AccResponse() { }
    }
}
