﻿using AutoMapper;
using CarRental.DTOs.Cars;
using CarRental.DTOs.Orders;
using CarRental.Models;
using CarRental.Web.Areas.Administration.ViewModels.Cars;
using CarRental.Web.Areas.Administration.ViewModels.Locations;
using CarRental.Web.ViewModels.Orders;

namespace CarRental.Web.MappingConfiguration
{
    public class CarRentalConfiguration : Profile
    {
        public CarRentalConfiguration()
        {
            this.CreateMap<AddLocationViewModel, Location>();
            this.CreateMap<Order, OrderDto>();
            this.CreateMap<OrderDto, MyOrdersViewModel>()
               .ForMember(dest => dest.PickUpLocation, src => src.MapFrom(x => x.PickUpLocation.Name))
               .ForMember(dest => dest.ReturnLocation, src => src.MapFrom(x => x.ReturnLocation.Name))
               .ForMember(dest => dest.CarModel, src => src.MapFrom(x => x.Car.Model))
               .ReverseMap();
            this.CreateMap<OrderDto, OrderDetailsViewModel>()
              .ForMember(dest => dest.CarModel, src => src.MapFrom(x => x.Car.Model))
              .ForMember(dest => dest.CarGearType, src => src.MapFrom(x => x.Car.GearType))
              .ForMember(dest => dest.CarYear, src => src.MapFrom(x => x.Car.Year))
              .ForMember(dest => dest.Email, src => src.MapFrom(x => x.User.Email))
              .ForMember(dest => dest.Firstname, src => src.MapFrom(x => x.User.FirstName))
              .ForMember(dest => dest.Lastname, src => src.MapFrom(x => x.User.LastName))
              .ForMember(dest => dest.PickUpLocation, src => src.MapFrom(x => x.PickUpLocation.Name))
              .ForMember(dest => dest.ReturnLocation, src => src.MapFrom(x => x.ReturnLocation.Name))
               .ForMember(dest => dest.CarImage, src => src.MapFrom(x => x.Car.Image))
               .ForMember(dest => dest.CarDescription, src => src.MapFrom(x => x.Car.Description))
              .ReverseMap();
            this.CreateMap<AddCarViewModel, Car>();
            this.CreateMap<ListCarDto, Car>();
            this.CreateMap<CarDetailsDto, Car>().
                ReverseMap();
        }
    }
}
