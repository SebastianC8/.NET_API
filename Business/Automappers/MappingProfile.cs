using AutoMapper;
using Repository.Data;
using Repository.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Automappers
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            // Mapping rules
            CreateMap<BeerInsertDTO, Beer>();

            // Different property names
            CreateMap<Beer, BeerDTO>().ForMember(
                dto => dto.Id,
                m => m.MapFrom(b => b.BeerID)
            );

            CreateMap<BeerUpdateDTO, Beer>();
        }
    }
}
