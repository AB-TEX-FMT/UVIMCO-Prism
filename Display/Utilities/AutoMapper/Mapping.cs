using AutoMapper;
using System;

namespace Display.Utilities.AutoMapper
{
    public static class Mapping
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg => 
                {
                    // This line ensures that internal properties are also mapped over.
                    //cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                    cfg.AddMaps(new[] {
                        "Display",
                        "DataRepository"
                });
        });
            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<Source, Destination>();
            // Additional mappings here...
            //CreateMap<DataModel.Shared.ApplicationUser, DataRepository.Models.ApplicationUser>().ReverseMap();
            //CreateMap<DataModel.Shared.ApplicationRole, DataRepository.Models.ApplicationRole>().ReverseMap();
            //CreateMap<DataModel.Shared.ApplicationUserClaim, DataRepository.Models.ApplicationUserClaim>().ReverseMap();
            //CreateMap<DataModel.Shared.ApplicationRoleClaim, DataRepository.Models.ApplicationRoleClaim>().ReverseMap();
            //CreateMap<DataModel.Shared.ApplicationUserRole, DataRepository.Models.ApplicationUserRole>().ReverseMap();
            //CreateMap<DataModel.Shared.ApplicationClaim, System.Security.Claims.Claim>()
            //    .ConstructUsing(source => new System.Security.Claims.Claim(source.ClaimType, source.ClaimValue))
            //    .ReverseMap()
            //    .ForMember(dest => dest.ClaimType, opt => opt.MapFrom(src => src.Type))
            //    .ForMember(dest => dest.ClaimValue, opt => opt.MapFrom(src => src.Value));
            CreateMap<DataRepository.NPocoRepository.MapObjects.ReportGroupMapping, DataModel.Shared.ReportGroup>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.RG_ID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.RG_Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.RG_Description))
                .ForMember(dest => dest.VisibilityLevel, opt => opt.MapFrom(src => src.RG_VisibilityLevel))
                .ForMember(dest => dest.ReportDefs, opt => opt.MapFrom(src => src.ReportDefMappings))
                .ReverseMap();
            CreateMap<DataRepository.NPocoRepository.MapObjects.ReportDefMapping, DataModel.Shared.ReportDef>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.R_ID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.R_Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.R_Description))
                .ForMember(dest => dest.VisibilityLevel, opt => opt.MapFrom(src => src.R_VisibilityLevel))
                .ReverseMap();

        }
    }
}
