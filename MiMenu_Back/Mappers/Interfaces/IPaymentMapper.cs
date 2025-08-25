using MercadoPago.Client.Preference;
using MiMenu_Back.Data.DTOs.Order;
using MiMenu_Back.Data.DTOs.User;
using MiMenu_Back.Data.Models;

namespace MiMenu_Back.Mappers.Interfaces
{
    public interface IPaymentMapper
    {
        List<PreferenceItemRequest> ListDtoToListItem(List<CartItemGetDto> listDto);
        PreferencePayerRequest UserToPayer(UserGetDto userDto);
    }
}
