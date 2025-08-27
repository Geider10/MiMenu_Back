using Microsoft.EntityFrameworkCore;
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
        public async Task Add(PaymentModel payment)
        {
            _appDB.Payments.Add(payment);
            await _appDB.SaveChangesAsync();
        }
        public async Task<PaymentModel?> GetByIdPublic(string idPublic)
        {
            return await _appDB.Payments.FirstOrDefaultAsync(p => p.IdPublic == idPublic);
        }
        public async Task Update(PaymentModel payment)
        {
            _appDB.Payments.Update(payment);
            await _appDB.SaveChangesAsync();
        }
    }
}
