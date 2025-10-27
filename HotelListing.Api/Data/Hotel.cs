﻿namespace HotelListing.Api.Data
{
    public class Hotel
    {
        public int HotelId { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public double Rating { get; set; }
    }
}
