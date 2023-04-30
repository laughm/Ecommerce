using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEcommerce2011.Model
{
    /// <summary>
    /// 登录日志
    /// </summary>
    [Table("LoginLog")]
    public class LoginLog
    {
        [Key]
        public int LogId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime LoginTime { get; set; }
       
    }
}