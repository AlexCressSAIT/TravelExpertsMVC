using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TravelExpertsData
{
    [Index(nameof(FeeId), Name = "Agency Fee Code")]
    [Index(nameof(BookingId), Name = "BookingId")]
    [Index(nameof(BookingId), Name = "BookingsBookingDetails")]
    [Index(nameof(ClassId), Name = "ClassesBookingDetails")]
    [Index(nameof(RegionId), Name = "Dest ID")]
    [Index(nameof(RegionId), Name = "DestinationsBookingDetails")]
    [Index(nameof(FeeId), Name = "FeesBookingDetails")]
    [Index(nameof(ProductSupplierId), Name = "ProductSupplierId")]
    [Index(nameof(ProductSupplierId), Name = "Products_SuppliersBookingDetails")]
    public partial class BookingDetail
    {
        [Key]
        public int BookingDetailId { get; set; }
        public double? ItineraryNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TripStart { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TripEnd { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        [StringLength(255)]
        public string Destination { get; set; }
        [Column(TypeName = "money")]
        public decimal? BasePrice { get; set; }
        [Column(TypeName = "money")]
        public decimal? AgencyCommission { get; set; }
        public int? BookingId { get; set; }
        [StringLength(5)]
        public string RegionId { get; set; }
        [StringLength(5)]
        public string ClassId { get; set; }
        [StringLength(10)]
        public string FeeId { get; set; }
        public int? ProductSupplierId { get; set; }

        [ForeignKey(nameof(BookingId))]
        [InverseProperty("BookingDetails")]
        public virtual Booking Booking { get; set; }
        [ForeignKey(nameof(ClassId))]
        [InverseProperty("BookingDetails")]
        public virtual Class Class { get; set; }
        [ForeignKey(nameof(FeeId))]
        [InverseProperty("BookingDetails")]
        public virtual Fee Fee { get; set; }
        [ForeignKey(nameof(ProductSupplierId))]
        [InverseProperty(nameof(ProductsSupplier.BookingDetails))]
        public virtual ProductsSupplier ProductSupplier { get; set; }
        [ForeignKey(nameof(RegionId))]
        [InverseProperty("BookingDetails")]
        public virtual Region Region { get; set; }
    }
}
