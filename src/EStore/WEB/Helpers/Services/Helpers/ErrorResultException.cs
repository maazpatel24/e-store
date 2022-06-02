using DAL.Models.Api;

namespace WEB.Helpers.Services.Helpers
{
    public class ErrorResultException : Exception
    {
        public ErrorResult Error { get; set; }
        public ErrorResultException(ErrorResult error)
        {
            Error = error;
        }
    }
}
