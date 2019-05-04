namespace JW.Core.Captch
{
    public interface IVerifyCode
    {
        byte[] GetCaptch(string sessionKey);
    }
}
