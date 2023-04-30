using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEcommerce2011.Model
{
    /// <summary>
    /// 商品属性分类信息
    /// </summary>
    [Table("GoodsPropType")]
    public class GoodsPropType
    {
        [Key]
        public int GPTId { get; set; }

        [Required]
        public string GPTName { get; set; }
       
    }
}