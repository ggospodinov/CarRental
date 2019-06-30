﻿using System;
using System.ComponentModel.DataAnnotations;

namespace CarRental.Web.InputModels.Orders
{
    public class OrderPreviewInputModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public decimal PricePerDay { get; set; }
        [Required]
        public DateTime RentStart { get; set; }
        [Required]
        public DateTime RentEnd { get; set; }
        [Required]
        public int Days { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string PickUpPlace { get; set; }
        [Required]
        public string ReturnPlace { get; set; }
    }
}
