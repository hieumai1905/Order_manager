using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrderManager.Entities;

public partial class Customer
{
    [Display(Name = "Mã khách hàng")]
    public string CustomerId { get; set; } = null!;
    [Display(Name = "Tên khách hàng")]
    [RegularExpression(@"^[a-zA-Z\sàáạảãâầấậẩẫăằắặẳẵèéẹẻẽêềếệểễìíịỉĩòóọỏõôồốộổỗơờớợởỡùúụủũưừứựửữỳýỵỷỹ]+$", ErrorMessage = "Tên không hợp lệ!")]
    public string Name { get; set; } = null!;
    [Display(Name = "Số điện thoại")]
    public string Phone { get; set; } = null!;
    [Display(Name = "Thông tin chi tiết")]
    public string Detail { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
