using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrderManager.Entities;

public partial class WorkShop
{
    public int WorkShopId { get; set; }

    [Display(Name ="Tên xưởng")]
    public string NameWorkShop { get; set; } = null!;

    [Display(Name ="Địa chỉ xưởng")]
    public string Address { get; set; } = null!;
    [Display(Name = "Mã Wechat")]
    public string Wechat { get; set; } = null!;
    [Display(Name = "Link shop")]
    public string LinkShop { get; set; } = null!;
    [Display(Name = "Số điện thoại")]
    public string Phone { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
