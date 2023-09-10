using AutoMapper;
using Backend.Models.DTO;

namespace Backend.Models.Profiles
{
    public class MascotaProfile : Profile
    {
        public MascotaProfile()
        {
            CreateMap<Mascota, MascotaDTO>();
            CreateMap<MascotaDTO, Mascota>();
        }
    }
}
