using Matcher.Models;

namespace Matcher.Services;
public interface IVnPayService
{
    string CreatePaymentUrl(Subscription model, HttpContext context);
    Subscription PaymentExecute(IQueryCollection collections);
}