using System;
using System.Collections.Generic;
using System.Text;
using static SanTsgProject.Application.Models.PriceSearchResponse;

namespace SanTsgProject.Application.Models
{
    public class Hotels
    {
        public string Id { get; set; }
        public string HotelName { get; set; }
        public double Stars { get; set; }

        public double Rating { get; set; }

        public double Price { get; set; }
        public string OfferId { get; set; }
        public Hotels(string hotelName, string id, double stars, double rating, double price, string offerId)
        {
            HotelName = hotelName;
            Id = id;
            Stars = stars;
            Rating = rating;
            Price = price;
            OfferId = offerId;
        }
    }
}
