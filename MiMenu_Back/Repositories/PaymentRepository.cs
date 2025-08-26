using Microsoft.EntityFrameworkCore.ChangeTracking;
using MiMenu_Back.Data;
using MiMenu_Back.Data.Models;
using MiMenu_Back.Repositories.Interfaces;

namespace MiMenu_Back.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDB _appDB;
        public PaymentRepository(AppDB appDB)
        {
            _appDB = appDB;
        }
        public async Task<EntityEntry<PaymentModel>> Add(PaymentModel payment)
        {
            var pay = _appDB.Payments.Add(payment);
            await _appDB.SaveChangesAsync();
            return pay;
        }
    }
}
