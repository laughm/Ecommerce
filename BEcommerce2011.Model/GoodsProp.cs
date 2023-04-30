using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEcommerce2011.Model
{
    /// <summary>
    /// 商品属性信息
    /// </summary>
    [Table("GoodsProp")]
    public class GoodsProp
    {
        [Key]
        public int GPId { get; set; }

        [Required]
        public string? GPName { get; set; }

        /// <summary>
        /// 对应的分类Id
        /// </summary>
        public int GPTId { get; set; }

    }
}