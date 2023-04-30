using BEcommerce2011.DAL;
using BEcommerce2011.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace BEcommerce2011.WebAPI.Controllers
{
    /// <summary>
    /// 电商操作
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EcommerceController : ControllerBase
    {
        // Dal
        private EcommerceDal dal;
        // 日志
        private readonly ILogger<EcommerceController> logger;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dal"></param>
        public EcommerceController(EcommerceDal dal, ILogger<EcommerceController> logger)
        {
            this.dal = dal;
            this.logger = logger;
        }

        #region 用户管理
        /// <summary>
        /// 角色列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetRoleList()
        {
            var list = dal.GetRoleList();
            return Ok(list);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Register(UserInfo user)
        {
            int result = dal.Register(user);
            return Ok(result);
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult UserInfoUpd(UserInfo user)
        {
            int result = dal.UserInfoUpd(user);
            return Ok(result);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult UserInfoDel(int id)
        {
            var result = dal.UserInfoDel(id);
            return Ok(result);
        }

        /// <summary>
        /// 登录判定
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login(string? loginName, string? password)
        {
            // 读Session中内容
            string userName = HttpContext.Session.GetString("loginName") ?? "";

            // 写Session
            HttpContext.Session.SetString("loginName", loginName);

            // 写cookie 值
            HttpContext.Response.Cookies.Append("b_loginName", loginName);
            HttpContext.Response.Cookies.Append("b_password", password);

            // 读Cookies值
            string lName = HttpContext.Request.Cookies["b_loginName"] ?? "";

            var user = dal.Login(loginName, password);
            return Ok(user);
        }

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="loginName"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetUserList(int page=1, int size=2, string? loginName="", int roleId=0)
        {
            int totalCount;
            int pageCount;

            var list = dal.GetUserList(page, size, out totalCount, out pageCount, loginName,roleId);
            return Ok(new
            {
                totalCount,
                pageCount,
                list
            });
        }

        /// <summary>
        /// 反填
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetUserInfo(int id)
        {
            var user = dal.GetUserInfo(id);
            return Ok(user);
        }

        /// <summary>
        /// 生成图片校验码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult CreateCheckCode()
        {
            string checkCode = ValidateCodeHelper.CreateRandomCode(4);

            // 写校验码到Session中
            HttpContext.Session.SetString("checkCode", checkCode);

            byte[] buffer = ValidateCodeHelper.CreateValidateGraphic(checkCode);

            return File(buffer, "image/jpg");

        }


        /// <summary>
        /// 判定校验码是否正确
        /// </summary>
        /// <param name="checkCode"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult CheckCheckCode(string checkCode)
        {
            string serverCheckCode = HttpContext.Session.GetString("checkCode") ?? "";
            if(checkCode.ToLower() != serverCheckCode.ToLower())
            {
                return Ok(false);
            }
            return Ok(true);
        }


        /// <summary>
        /// 登录统计
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult CountLogin()
        {
            var list = dal.CountLogin();
            return Ok(list);
        }

        /// <summary>
        /// 登录统计（使用SQL语句）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult CountLoginBySQL()
        {
            var list = dal.CountLoginBySQL();
            return Ok(list);
        }

        /// <summary>
        /// 批量修改用户状态
        /// </summary>
        /// <param name="list"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult BatchChangeState(List<UserInfo> list, bool state)
        {
            var result = dal.BatchChangeState(list, state);
            return Ok(result);
        }

        /// <summary>
        /// 批量修改用户状态（按Ids查询）
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult BatchChangeStateByIds(List<int> ids, bool state)
        {
            var result = dal.BatchChangeStateByIds(ids, state);
            return Ok(result);
        }
        /// <summary>
        /// 批量逻辑删除
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult LogicDel(List<UserInfo> list)
        {
            var result = dal.LogicDel(list);
            return Ok(result);
        }

        /// <summary>
        /// 批量真实删除
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [HttpPost]
        public  IActionResult TrueDel(List<UserInfo> list)
        {
            var result = dal.TrueDel(list);
            return Ok(result);
        }
        #endregion

        #region 商品管理

        #region 商品分类
        /// <summary>
        /// 获取子商品分类列表（转换为树型类型）
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetGoodsTypeListByPId(int pId)
        {
            var list = dal.GetGoodsTypeListByPId(pId);
            return Ok(list);
        }

        /// <summary>
        /// 获取子分类列表
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetGoodsTypeSubList(int pId = 0)
        {
            var list = dal.GetGoodsTypeSubList(pId);
            return Ok(list);
        }

        /// <summary>
        /// 添加商品分类
        /// </summary>
        /// <param name="gt"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GoodsTypeAdd(GoodsType gt)
        {
            var result = dal.GoodsTypeAdd(gt);
            return Ok(result);
        }

        /// <summary>
        /// 修改商品分类名称
        /// </summary>
        /// <param name="gtId"></param>
        /// <param name="gtName"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult GoodsTypeUpd(int gtId, string gtName)
        {
            var result = dal.GoodsTypeUpd(gtId, gtName);
            return Ok(result);
        }

        /// <summary>
        /// 删除商品分类
        /// </summary>
        /// <param name="gtId"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult GoodsTypeDel(int gtId)
        {
            var result = dal.GoodsTypeDel(gtId);
            return Ok(result);
        }

        #endregion

        #region 商品

        /// <summary>
        /// 品牌列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetBrandList()
        {
            var list = dal.GetBrandList();
            return Ok(list);
        }

        /// <summary>
        /// 商品属性分类列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetGoodsPropTypeList()
        {
            var list = dal.GetGoodsPropTypeList();
            return Ok(list);
        }

        /// <summary>
        /// 反加属性分类对应的属性列表
        /// </summary>
        /// <param name="gptId"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetGoodsPropList(int gptId)
        {
            var list = dal.GetGoodsPropList(gptId);
            return Ok(list);
        }

        /// <summary>
        /// 商品列表（分页，4个查询条件）
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="gName"></param>
        /// <param name="bId"></param>
        /// <param name="gtId"></param>
        /// <param name="sDate"></param>
        /// <param name="eDate"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetGoodsList(int page=1, int size=2, string? gName="", int bId=0, int gtId=0, string? sDate="", string? eDate="")
        {
            // 总条数，总页面
            int totalCount;
            int pageCount;

            var list = dal.GetGoodsList(page, size, out totalCount, out pageCount, gName, bId, gtId, sDate, eDate);

            return Ok(new
            {
                totalCount,
                pageCount,
                list
            });

        }

        /// <summary>
        /// 反填
        /// </summary>
        /// <param name="id">商品Id</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetGoodsInfo(int id)
        {
            var goods = dal.GetGoodsInfo(id);
            return Ok(goods);
        }

        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="goods">商品信息</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GoodsAdd(Goods goods)
        {
            // 添加时间（赋值）
            goods.AddTime = DateTime.Now;
            var result = dal.GoodsAdd(goods);
            return Ok(result);
        }

        /// <summary>
        /// 修改商品信息
        /// </summary>
        /// <param name="goods">商品信息</param>
        /// <returns></returns>
        [HttpPut]
       public IActionResult GoodsUpd(Goods goods)
        {
            var result = dal.GoodsUpd(goods);
            return Ok(result);
        }

        /// <summary>
        /// 删除商品信息
        /// </summary>
        /// <param name="id">商品Id</param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult GoodsDel(int id)
        {
            var reuslt = dal.GoodsDel(id);
            return Ok(reuslt);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UploadFile(IFormFile file)
        {
            try
            {
                // 上传到服务器上的路径和文件 -- 
                // D:\xxxx\BEcommerce2011.WebAPI\wwwroot\File\小米电视.jpg
                string saveFileName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/file", file.FileName);
               
                // 保存上传的文件
                using (FileStream fs = new FileStream(saveFileName,FileMode.Create))
                {
                    // 写数据流
                    file.CopyTo(fs);
                    fs.Flush();
                }

                return Ok(file.FileName);
            }
            catch (Exception ex)
            {
                // 错误日志
                logger.Log(LogLevel.Error,"上传文件出错！"+ex.Message);
                throw;
            }
        }


        #endregion

        #endregion
    }

    #region 生成校验码类
    public static class ValidateCodeHelper
    {


        public static string CreateRandomCode(int codeCount)
        {
            string allChar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,W,X,Y,Z";
            string[] allCharArray = allChar.Split(',');
            string randomCode = "";
            int temp = -1;
            Random rand = new Random();
            for (int i = 0; i < codeCount; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
                }
                int t = rand.Next(35);
                if (temp == t)
                {
                    return CreateRandomCode(codeCount);
                }
                temp = t;
                randomCode += allCharArray[t];
            }
            return randomCode;
        }


        /// <summary>
        /// 创建验证码的图片
        /// </summary>
        public static byte[] CreateValidateGraphic(string validateCode)
        {
            Bitmap image = new Bitmap((int)Math.Ceiling(validateCode.Length * 16.0), 27);
            Graphics g = Graphics.FromImage(image);
            try
            {
                //生成随机生成器
                Random random = new Random();
                //清空图片背景色
                g.Clear(Color.White);
                //画图片的干扰线
                for (int i = 0; i < 25; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }
                Font font = new Font("Arial", 13, (FontStyle.Bold | FontStyle.Italic));
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height),
                 Color.Blue, Color.DarkRed, 1.2f, true);
                g.DrawString(validateCode, font, brush, 3, 2);
                //画图片的前景干扰点
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }
                //画图片的边框线
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
                //保存图片数据
                MemoryStream stream = new MemoryStream();
                image.Save(stream, ImageFormat.Jpeg);
                //输出图片流
                return stream.ToArray();
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }
    }
    #endregion
}
