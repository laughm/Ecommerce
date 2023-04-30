using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEcommerce2011.Model
{
    /// <summary>
    /// 品牌信息
    /// </summary>
    [Table("Brand")]
    public class Brand
    {
        [Key]
        public int BId { get; set; }

        [Required]
        public string BName { get; set; }
       
    }
}