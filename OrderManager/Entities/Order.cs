using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrderManager.Entities;

public partial class Order
{
    public int OrderId { get; set; }
    [Display(Name = "Ngày đặt")]
    public DateTime OrderAt { get; set; }
    [Display(Name = "Tổng thể tích (m3)")]
    [Range(0, 10000000000)]
    public double Volume { get; set; }
    [Display(Name = "Tổng khối lượng (kg)")]
    [Range(0, 10000000000)]
    public double Mass { get; set; }
    [Range(0, 200)]
    [Display(Name = "Thời gian chuẩn bị hàng (ngày)")]
    public int TimeOrder { get; set; }
    [Display(Name = "Thời gian giao nội địa (ngày)")]
    [Range(0, 200)]
    public int ShipIn { get; set; }
    [Display(Name = "Tổng tiền hàng (tệ)")]
    [Range(0, 10000000000)]
    public double TotalPrice { get; set; }
    [Display(Name = "Giá nhập (tệ)")]
    [Range(0, 10000000000)]
    public double PriceIn { get; set; }

    [Display(Name = "Cách đóng hàng")] public string TypeWrap { get; set; }
    [Display(Name = "Giá bán (tệ)")]
    [Range(0, 10000000000)]
    public double PriceOut { get; set; }
    [Display(Name = "Ship nội địa (tệ)")]
    [Range(0, 10000000000)]
    public double ShipPrice { get; set; }
    [Display(Name = "Mã khách hàng")]
    public string CustomerId { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
