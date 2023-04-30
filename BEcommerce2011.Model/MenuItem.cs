using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEcommerce2011.Model
{
    /// <summary>
    /// Tree 树型（级联选择器）信息类
    /// </summary>
    public class MenuItem
    {
        public string? Value { get; set; }
        public string? Label { get; set; }
        public bool HasChildren { get; set; }
        public List<MenuItem>? Children { get; set; }
    }
}
