using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebSiteBanHang.Models
{
    [MetadataTypeAttribute(typeof(ThanhVienMetaData))]
    public partial class ThanhVien
    {
        internal sealed class ThanhVienMetaData
        {
            public int MaThanhVien { get; set; }
            [DisplayName("Tài khoản")]
            //[Range(5,10,ErrorMessage ="Đơn vị {0} phải từ {1} đến {2}")]
            [Required(ErrorMessage ="{0} không được bỏ trống!")]
            public string TaiKhoan { get; set; }
            [DisplayName("Mật khẩu")]
            [Required(ErrorMessage = "{0} không được bỏ trống!")]
            public string MatKhau { get; set; }
            [DisplayName("Họ tên")]
            [Required(ErrorMessage = "{0} không được bỏ trống!")]
            public string HoTen { get; set; }
            [DisplayName("Email")]
            [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
            ErrorMessage = "Email không hợp lệ")]
            [Required(ErrorMessage = "{0} không được bỏ trống!")]
            public string Email { get; set; }
            [DisplayName("Địa chỉ")]
            [Required(ErrorMessage = "{0} không được bỏ trống!")]
            public string DiaChi { get; set; }
            [DisplayName("Số điện thoại")]
            [Required(ErrorMessage = "{0} không được bỏ trống!")]
            public string SoDienThoai { get; set; }
            [DisplayName("Câu hỏi bí mật")]
            public string CauHoi { get; set; }
            [DisplayName("Câu trả lời")]
            [Required(ErrorMessage = "{0} không được bỏ trống!")]
            public string CauTraLoi { get; set; }
            [DisplayName("Mã loại thành viên")]
            [Required(ErrorMessage = "{0} không được bỏ trống!")]
            public Nullable<int> MaLoaiTV { get; set; }
        }
    }
}