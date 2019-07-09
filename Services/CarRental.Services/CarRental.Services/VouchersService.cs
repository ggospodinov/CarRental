﻿using AutoMapper;
using CarRental.Data;
using CarRental.DTOs.Vouchers;
using CarRental.Models;
using CarRental.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRental.Services
{
    public class VouchersService : IVouchersService
    {
        private readonly CarRentalDbContext dbCotenxt;
        private readonly IUsersService usersService;
        private readonly IMapper mapper;

        public VouchersService(CarRentalDbContext dbCotenxt, IUsersService usersService, IMapper mapper)
        {
            this.dbCotenxt = dbCotenxt;
            this.usersService = usersService;
            this.mapper = mapper;
        }

        public bool CreateForUser(string username)
        {
            var userId = this.usersService.GetUserIdByEmail(username);
            if (userId is null)
            {
                return false;
            }
            var voucher = new Voucher
            {
                ApplicationUserId = userId,
                Status = Models.Enums.VoucherStatus.Active,
                VoucherCode = Guid.NewGuid().ToString(),
                Discount = this.GenerateDiscount()
            };
            this.dbCotenxt.Vouchers.Add(voucher);
            this.dbCotenxt.SaveChanges();
            return true;
        }

        public bool CreateForUserCustom(string username, int discount)
        {
            var userId = this.usersService.GetUserIdByEmail(username);
            if (userId is null)
            {
                return false;
            }
            var voucher = new Voucher
            {
                ApplicationUserId = userId,
                Status = Models.Enums.VoucherStatus.Active,
                VoucherCode = Guid.NewGuid().ToString(),
                Discount = discount
            };
            this.dbCotenxt.Vouchers.Add(voucher);
            this.dbCotenxt.SaveChanges();
            return true;
        }

        public bool DeleteVoucher(int id)
        {
            var voucher = this.dbCotenxt.
                             Vouchers.Find(id);

            if (voucher is null)
            {
                return false;
            }

            this.dbCotenxt.Vouchers.Remove(voucher);
            return true;
        }

        public ICollection<VoucherDto> GetAllActiveForUser(string username)
        {
            var userId = this.usersService.GetUserIdByEmail(username);

            var vouchers = this.dbCotenxt.Vouchers.Where(x => x.ApplicationUserId == userId && x.Status == Models.Enums.VoucherStatus.Active);

            return this.mapper.Map<List<VoucherDto>>(vouchers);
        }

        public ICollection<VoucherDto> GetAllForUser(string username)
        {
            var userId = this.usersService.GetUserIdByEmail(username);

            var vouchers = this.dbCotenxt.Vouchers.Where(x => x.ApplicationUserId == userId);

            return this.mapper.Map<List<VoucherDto>>(vouchers);
        }

        public ICollection<VoucherDto> GetAllVouchers()
        {
            var vouchers = this.dbCotenxt.
                             Vouchers.ToList();

            return this.mapper.Map<List<VoucherDto>>(vouchers);
        }

        public int GetDiscountForCode(string voucherCode)
        {
            if (String.IsNullOrEmpty(voucherCode) ||voucherCode == "none")
            {
                return 0;
            }

            var discount = this.dbCotenxt.Vouchers.
                 FirstOrDefault(x => x.VoucherCode == voucherCode).
                 Discount;

            return discount == null ? 0 : discount;
        }

        public bool UseVoucher(string voucherCode)
        {
            var voucher = this.dbCotenxt.Vouchers.
                Where(x => x.VoucherCode == voucherCode).
                FirstOrDefault();

            if (voucher is null)
            {
                return false;
            }

            voucher.Status = Models.Enums.VoucherStatus.Used;
            this.dbCotenxt.SaveChanges();
            return true;
        }

        private int GenerateDiscount()
        {
            var random = new Random();
            var value = random.Next(0, 5);
            return value;
        }
    }
}
