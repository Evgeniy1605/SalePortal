namespace SalePortal.Models
{
    public class PasswordRecoveryModel
    {
        public int UserId { get; set; }
        public int Code { get; set; }
        public int InputCode { get; set; }
        public string NewPassword { get; set; }
    }
}
