using Microsoft.EntityFrameworkCore.ChangeTracking;
using MiMenu_Back.Data.Models;

namespace MiMenu_Back.Repositories.Interfaces
{
    public interface IPaymentRepository
    {
        Task<EntityEntry<PaymentModel>> Add (PaymentModel payment);
    }
}
