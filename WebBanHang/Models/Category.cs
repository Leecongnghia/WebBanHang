using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebBanHang.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Chưa nhập tên chủng loại"), StringLength(50)]
        public String Name { get; set; }
        [Range(1, 100, ErrorMessage = "Thứ tự từ 1 đến 100")]
        public int DisplayOrder { get; set; }
    }
}
