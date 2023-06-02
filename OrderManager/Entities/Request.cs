using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;

namespace OrderManager.Entities;

public partial class Request
{
    public int RequestId { get; set; }
    [Display(Name = "Ảnh")] public string Image { get; set; } = null!;
    [Display(Name = "Chi tiết yêu cầu")] public string Detail { get; set; } = null!;
    [Display(Name = "Trạng thái yêu cầu")] public int Status { get; set; }
    [Display(Name = "Ngày yêu cầu")] public DateTime RequestAt { get; set; }
    [Display(Name = "Mã khách hàng")] public string CustomerId { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;
}