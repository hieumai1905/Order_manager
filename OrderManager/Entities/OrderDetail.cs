using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrderManager.Entities;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }
    [Display(Name = "Giá sản phẩm")] public double Price { get; set; }
    [Display(Name = "Số lượng sản phẩm")] public int Quantity { get; set; }
    [Display(Name = "Mã hoá đơn")] public int OrderId { get; set; }
    [Display(Name = "Mã sản phẩm")] public string ProductId { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}