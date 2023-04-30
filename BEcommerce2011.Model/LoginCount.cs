using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEcommerce2011.Model
{
    /// <summary>
    /// 登录统计
    /// </summary>
    [Table("LoginCount")]
    public class LoginCount
    {
        [Key]
        public int Total { get; set; }

        [Required]
        public string YearMonth { get; set; }
       
    }
}