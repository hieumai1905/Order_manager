using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrderManager.Entities;

public partial class Product
{
    [Display(Name = "Mã sản phẩm")]
    public string ProductId { get; set; } = null!;

    [Display(Name = "Tên sản phẩm")]
    public string Name { get; set; } = null!;
    [Display(Name = "Giá sản phẩm")]
    public double Price { get; set; }
    [Display(Name = "Trạng thái")]
    public int Status { get; set; }
    [Display(Name = "Ảnh sản phẩm")]
    public string Photo { get; set; } = null!;
    [Display(Name = "Mô tả sản phẩm")]
    public string Description { get; set; } = null!;
    [Display(Name = "Thể tích thùng (m3)")]
    public double Volume { get; set; }
    [Display(Name = "Cân nặng thùng (kg)")]
    public double Mass { get; set; }
    [Display(Name = "Tên xưởng")]
    public int WorkShopId { get; set; }
    [Display(Name = "Danh mục")]
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual WorkShop WorkShop { get; set; } = null!;
}
