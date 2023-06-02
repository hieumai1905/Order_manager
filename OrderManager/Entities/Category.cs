using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrderManager.Entities;

public partial class Category
{
    public int CategoryId { get; set; }

    [Display(Name = "Tên danh mục")]
    public string NameCategory { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}