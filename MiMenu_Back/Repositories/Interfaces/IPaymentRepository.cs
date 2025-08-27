using Microsoft.EntityFrameworkCore.ChangeTracking;
using MiMenu_Back.Data.Models;
using System.Drawing;

namespace MiMenu_Back.Repositories.Interfaces
{
    public interface IPaymentRepository
    {
        Task Add(PaymentModel payment);
        Task<PaymentModel?> GetByIdPublic(string id);
        Task Update(PaymentModel payment);
        Task<PaymentModel?> GetById(string id);
    }
}
