using AutoMapper;
using Centangle.ClientManager.Entity;
using ClientManagement.Helpers.ViewModels;
using ClientManagement.Helpers.ViewModels.SharedVM;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClientManagement.Helpers.Models.Shared.Mapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            IgnoreGlobalProperties();
            CreateMap(typeof(ClientBreifVM<>), typeof(Client<>))
              .ForMember("ClientId", opt => opt.MapFrom("Id"))
              .ReverseMap();
            CreateMap(typeof(Module<>), typeof(ModuleBreifVM<>)).ReverseMap();
            CreateMap(typeof(Tenant<>), typeof(TenantBreifVM<>)).ReverseMap();

            CreateMap(typeof(Client<>), typeof(ClientVM<>)).ReverseMap();

            CreateMap(typeof(Branch<>), typeof(BranchVM<>));
            CreateMap(typeof(BranchVM<>), typeof(Branch<>)).ForMember("TenantId", to => to.MapFrom("Tenant.Id"));
            //.ForMember("TenantId", to => to.MapFrom("Tenant.Id"))
            //.ReverseMap();

            CreateMap(typeof(Tenant<>), typeof(TenantVM<>));
            CreateMap(typeof(TenantVM<>), typeof(Tenant<>))
            .ForMember("ClientId", opt => opt.MapFrom("Client.Id"));
            //.ReverseMap();

            CreateMap(typeof(Module<>), typeof(ModuleVM<>));
            CreateMap(typeof(ModuleVM<>), typeof(Module<>))
            .ForMember("ClientId", opt => opt.MapFrom("Client.Id"));
            //.ReverseMap();

            CreateMap(typeof(ModulePermission<>), typeof(ModulePermissionVM<>));
            CreateMap(typeof(ModulePermissionVM<>), typeof(ModulePermission<>))
            .ForMember("ClientId", opt => opt.MapFrom("Client.Id"))
            .ForMember("ModuleId", opt => opt.MapFrom("Module.Id"));
            //.ReverseMap();
        }

        private void IgnoreGlobalProperties()
        {
            var properties = typeof(BaseVM).GetProperties();
            foreach (var property in properties.Select(x => x.Name).ToList())
            {
                if (property != "Id")
                    AddGlobalIgnore(property);
            }
        }
    }
}
