using AutoMapper;
using NanoCell.Application.CRMTuyenDoc.Commands;
using NanoCell.Domain.Entities;
using System;
using System.Linq;
using System.Reflection;

namespace NanoCell.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        { 
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
            // var lst = Assembly.GetExecutingAssembly();
            // var configuration = new MapperConfiguration(cfg => cfg.AddMaps(Assembly.GetExecutingAssembly()));
            // var mapper = new Mapper(configuration);
            //CreateMap<CRMDMTuyenDoc, CreateCRMTuyenDocCommand>();
            //CreateMap<CreateCRMTuyenDocCommand, CRMDMTuyenDoc>();
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i => 
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod("Mapping") 
                    ?? type.GetInterface("IMapFrom`1").GetMethod("Mapping");
                
                methodInfo?.Invoke(instance, new object[] { this });
            }

            types = assembly.GetExportedTypes()
            .Where(t => t.GetCustomAttribute(typeof(AutoMapper.AutoMapAttribute)) != null)
            .ToList();
            foreach (var type in types)
            {
                var a = type.GetCustomAttribute(typeof(AutoMapper.AutoMapAttribute)) as AutoMapAttribute;
                CreateMap(type, a.SourceType);
                CreateMap(a.SourceType, type);
                //CreateMap(type, type.GetCustomAttribute(typeof(AutoMapper.AutoMapAttribute)).
                //var instance = Activator.CreateInstance(type);

                //var methodInfo = type.GetMethod("Mapping")
                //    ?? type.GetInterface("IMapFrom`1").GetMethod("Mapping");

                //methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}