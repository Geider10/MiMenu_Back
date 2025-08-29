using MercadoPago.Resource.Payment;
using Microsoft.IdentityModel.Tokens;
using MiMenu_Back.Data.Enums;
using MiMenu_Back.Data.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace MiMenu_Back.Utils
{
    public class Util
    {
        public string HashText(string text)
        {
            return BCrypt.Net.BCrypt.HashPassword(text);
        }
        public bool VerifyHashText(string text, string hashText)
        {
            return BCrypt.Net.BCrypt.Verify(text, hashText);
        }
        public string GenerateJWT(string id, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, id),
                new Claim(ClaimTypes.Role, role)
            };

            DotNetEnv.Env.Load();
            var secretKey = System.Environment.GetEnvironmentVariable("SecretKey");
            var encodingSecreKet = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(encodingSecreKet, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        #region Date Formatting
        public DateOnly? VerifyFormatDateOnly(string? date)
        {
            var dateFormat = new Regex(@"^(?:3[01]|[12][0-9]|0?[1-9])([\-/.])(0?[1-9]|1[0-2])\1\d{4}$");//dd-mm-yyy||dd/mm/yyyy
            if (!dateFormat.IsMatch(date)) return null;
            return DateOnly.Parse(date);
        }
        public DateOnly FormatDateOnly(string date)
        {
            return DateOnly.Parse(date);
        }
        public string FormatDateOnly(DateOnly date)
        {
            return date.ToString("dd-MM-yyyy");
        }
        public string? FormatDateOnly(DateOnly? date)
        {
            if (date == null) return null;
            return date.Value.ToString("dd-MM-yyyy");
        }
        public string FormatDateTime(DateTime date)
        {
            return date.ToString("dd-MM-yyyy HH:mm:ss");
        }
        public TimeOnly FormatTimeOnly(string time)
        {
            return TimeOnly.Parse(time);
        }
        public DateOnly CreateDateCurrent()
        {
            DateTime date = DateTime.Now;
            return DateOnly.FromDateTime(date);
        }
        public int CompareDates(DateOnly createDate, DateOnly dueDate)
        {
            return dueDate.CompareTo(createDate);
        }
        #endregion

        #region Enum Formatting
        public string FormatPaymentStatus(StatusPaymentEnum status)
        {
            if (status == StatusPaymentEnum.Pending) return "Pending";
            if (status == StatusPaymentEnum.Approved) return "Approved";
            return "Rejected";
        }
        public TypeOrderEnum FormatTypeOrder(string type)
        {
            if(type.ToLower() == "takeaway") return TypeOrderEnum.TakeAway;
            if(type.ToLower() == "dinein") return TypeOrderEnum.DineIn;
            return TypeOrderEnum.Delivery;
        }
        public StatusOrderEnum NextStatusOrder(StatusOrderEnum status)
        {
            if (status == StatusOrderEnum.Pending) return StatusOrderEnum.InPreparation;
            if (status == StatusOrderEnum.InPreparation) return StatusOrderEnum.Ready;
            if (status == StatusOrderEnum.Ready) return StatusOrderEnum.Delivered;
            return status;
        }
        #endregion
    }
}
