using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEcommerce2011.Model
{
    [Table("UserInfo")]
    public class UserInfo
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string? LoginName { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        public string? EMail { get; set; }

        [Required]
        public string? Phone { get; set; }

        /// <summary>
        /// 状态（true：启用，false：禁用）
        /// </summary>
        public bool State { get; set; }

        /// <summary>
        /// 逻辑删除标志（True：已删除，false：正常）
        /// </summary>
        public bool IsDel { get; set; }

        [Required]
        public int RoleId { get; set; }

        /// <summary>
        /// 用户角色（导航必须是可以为null的，也就类型要加个?）
        /// </summary>
        [Required]
        [ForeignKey("RoleId")]
        public Role? Role { get; set; }

    }
}