using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEcommerce2011.Model
{
    /// <summary>
    /// 商品分类信息
    /// </summary>
    [Table("GoodsType")]
    public class GoodsType
    {
        [Key]
        public int GTId { get; set; }

        [Required]
        public string? GTName { get; set; }

        [Required]
        public int PId { get; set; }

    }
}