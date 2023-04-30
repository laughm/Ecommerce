using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEcommerce2011.Model
{
    /// <summary>
    /// 商品信息
    /// </summary>
    [Table("Goods")]
    public class Goods
    {
        /// <summary>
        /// 商品Id，主键，自增
        /// </summary>
        [Key]      
        public int GId { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string? GName { get; set; }

        /// <summary>
        /// 品牌Id
        /// </summary>
        public int BId { get; set; }

        /// <summary>
        /// 货号（商品编号）
        /// </summary>
        public string? GCode { get; set; }

        /// <summary>
        /// 建议价格
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal SuggestPrice { get; set; }

        /// <summary>
        /// 市场价格
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        public string? JLDW { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Weight { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 分类Id
        /// </summary>
        public int GTId { get; set; }

        /// <summary>
        /// 分类Id信息（从根到子所有的id，用逗号分隔）
        /// </summary>
        public string? GTIdAll { get; set; }

        /// <summary>
        /// 分类名称（只是显示，不存储在数据库中）
        /// </summary>
        [NotMapped]
        public string? GTName { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string? Pic { get; set; }

        /// <summary>
        /// 状态（true:上架，false：下架）
        /// </summary>
        public bool State { get; set; }

        /// <summary>
        /// 商品描述（富文本编辑器）
        /// </summary>
        public string? Desc { get; set;}

        /// <summary>
        /// 商品属性分类Id
        /// </summary>
        public int GPTId { get; set; }

        /// <summary>
        /// 商品属性（用,分隔，如：128G,256G,512G,1T)
        /// </summary>
        public string? GPContent { get; set; }

        /// <summary>
        /// 删除标志（True:已删除，false：正常）
        /// </summary>
        public bool IsDel { get; set; }

        // 导航

        /// <summary>
        /// 品牌（导航必须可以为空，不然添加和修改要失败！设置类型后面加?）
        /// </summary>
        [ForeignKey("BId")]
        public Brand? Brand { get; set; }


        /// <summary>
        /// 商品属性分类（导航必须可以为空，不然添加和修改要失败！设置类型后面加?）
        /// </summary>
        [ForeignKey("GPTId")]
        public GoodsPropType? GoodsPropType { get; set; }
    }
}