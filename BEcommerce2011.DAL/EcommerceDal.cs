using BEcommerce2011.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace BEcommerce2011.DAL
{
    public class EcommerceDal
    {
        #region 私有变量
        // 数据上下文类
        private readonly EcommerceDbContext db;
        // 日志
        private readonly ILogger<EcommerceDal> logger;

        #endregion

        #region 构造函数
        public EcommerceDal(EcommerceDbContext _db, ILogger<EcommerceDal> logger)
        {
            this.db = _db;
            this.logger = logger;

        }
        #endregion

        #region 用户管理

        /// <summary>
        /// 解决列表
        /// </summary>
        /// <returns></returns>
        public List<Role> GetRoleList()
        {
            try
            {
                return db.Role.ToList();
            }
            catch (Exception ex)
            {
                // 写错误日志
                logger.LogError($"获取角色列表失败！{ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Register(UserInfo user)
        {
            try
            {
                db.UserInfo.Add(user);
                return db.SaveChanges();
            }
            catch (Exception ex)
            {
                // 写错误日志
                logger.LogError($"注册失败！{ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int UserInfoUpd(UserInfo user)
        {
            try
            {
                db.UserInfo.Update(user);
                return db.SaveChanges();
            }
            catch (Exception ex)
            {
                // 写错误日志
                logger.LogError($"修改用户信息失败！{ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int UserInfoDel(int id)
        {
            try
            {
                // 查询要删除的用户
                UserInfo user = db.UserInfo.Find(id);
                db.UserInfo.Remove(user);

                return db.SaveChanges();
            }
            catch (Exception ex)
            {
                // 写错误日志
                logger.LogError($"删除用户信息失败！{ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// 登录判定
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public UserInfo? Login(string? loginName,string? password)
        {
            try
            {
                // 写Nlog日志测试
                logger.LogError("Dal层测试Nlog写日志！");

                UserInfo user = db.UserInfo.FirstOrDefault(u=>u.LoginName ==  loginName && u.Password == password);
                return user;
            }
            catch (Exception ex)
            {
                // 写错误日志
                logger.LogError($"登录判定失败！{ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageCount"></param>
        /// <param name="loginName"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<UserInfo> GetUserList(int page,int size,out int totalCount,out int pageCount,string? loginName,int roleId)
        {
            try
            {
                // 所有列表（未删除的）
                var list = db.UserInfo.Include(s=>s.Role).Where(u=>u.IsDel==false).AsQueryable();

                // 查询条件
                if(!string.IsNullOrEmpty(loginName))
                {
                    list = list.Where(u => u.LoginName.Contains(loginName));
                }
                if (roleId > 0)
                {
                    list = list.Where(u=>u.RoleId == roleId);
                }
                // 总条数，总页数
                totalCount = list.Count();
                pageCount = (int)Math.Ceiling(totalCount * 1.0 / size);

                // 分页
                list = list.OrderBy(u => u.UserId).Skip((page - 1) * size).Take(size);

                return list.ToList();
            }
            catch (Exception ex)
            {
                // 写错误日志
                logger.LogError($"获取用户列表失败！{ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// 反填
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserInfo GetUserInfo(int id)
        {
            try
            {
                return db.UserInfo.Find(id);
            }
            catch (Exception ex)
            {
                // 写错误日志
                logger.LogError($"获取用户信息失败！{ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// 登录日志统计（按年月）
        /// </summary>
        /// <returns></returns>
        public object CountLogin()
        {
            try
            {
                // 按年月统计的登录信息
                var list = db.LoginLog.ToList().GroupBy(l => new { l.LoginTime.Year, l.LoginTime.Month });

                // 转换格式
                var countList = list.Select(l => new
                {
                    yearMonth = $"{l.Key.Year}-{l.Key.Month.ToString("00")}",
                    total = l.Count()
                }).ToList();

                return countList;
            }
            catch (Exception ex)
            {
                // 写错误日志
                logger.LogError($"获取用户登录统计信息失败！{ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// 用户登录统计（执行SQL语句）
        /// </summary>
        /// <returns></returns>
        public object CountLoginBySQL()
        {
            try
            {
                string sql = @" SELECT  CONVERT(VARCHAR(7),LoginTime,120) AS YearMonth,COUNT(*) AS Total
                                FROM dbo.LoginLog
                                GROUP BY CONVERT(VARCHAR(7),LoginTime,120)";
                var countList = db.LoginCount.FromSqlRaw<LoginCount>(sql).ToList();
                return countList;
            }
            catch (Exception ex)
            {
                // 写错误日志
                logger.LogError($"获取用户登录统计信息失败！{ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// 批量状态修改
        /// </summary>
        /// <param name="list"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public int BatchChangeState(List<UserInfo> list,bool state)
        {
            try
            {
                // 循环修改用户状态
                foreach (UserInfo item in list)
                {
                    item.State = state;
                    item.Role = null; // 修改导航为null，防止出现导航信息修改
                }

                db.UserInfo.UpdateRange(list);
                return db.SaveChanges();
            }
            catch (Exception ex)
            {
                // 写错误日志
                logger.LogError($"修改用户状态信息失败！{ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// 批量修改状态（根据Id列表查询）
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public int BatchChangeStateByIds(List<int> ids, bool state)
        {
            try
            {
                // 查询id对应的用户列表([1,2,3] => userId // where userId in (1,2,3) 
                var list = db.UserInfo.Where(u => ids.Contains(u.UserId));
                // 修改查询出的用户状态
                foreach (var item in list)
                {
                    item.State = state;
                }
                db.UserInfo.UpdateRange(list);
                return db.SaveChanges();
            }
            catch (Exception ex)
            {
                // 写错误日志
                logger.LogError($"修改用户状态信息失败！{ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// 批量逻辑删除
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int LogicDel(List<UserInfo> list)
        {
            try
            {
                // 修改用户状态为删除
                foreach (var item in list)
                {
                    item.IsDel = true;
                    item.Role = null;// 修改导航为null，防止出现导航信息修改
                }
                db.UserInfo.UpdateRange(list);
                return db.SaveChanges();
            }
            catch (Exception ex)
            {
                // 写错误日志
                logger.LogError($"逻辑删除用户状态信息失败！{ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// 真实批量删除
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int TrueDel(List<UserInfo> list)
        {
            try
            {
                // 修改导航为null，防止出现导航信息修改
                foreach (var item in list)
                {
                    item.Role = null;
                }
                db.UserInfo.RemoveRange(list);
                return db.SaveChanges();
            }
            catch (Exception ex)
            {
                // 写错误日志
                logger.LogError($"真实删除用户状态信息失败！{ex.Message}");
                throw;
            }
        }

        #endregion

        #region 商品管理

        #region 商品分类
        /// <summary>
        /// 获取子商品分类列表（转换为树型类型）
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        public List<MenuItem> GetGoodsTypeListByPId(int pId=0)
        {
            try
            {
                // 获取 父Id对应的子分类信息
                List<GoodsType> list = db.GoodsType.Where(gt => gt.PId == pId).ToList();

                // 转换数据类型为 菜单类型（Tree 树型，级联选择器）

                List<MenuItem> menuList = list.Select(gt => new MenuItem
                {
                    Value = gt.GTId.ToString(),
                     Label = gt.GTName,
                     Children = GetGoodsTypeListByPId(gt.GTId).Count==0?null: GetGoodsTypeListByPId(gt.GTId)// 递归
                }).ToList();

                // 返回树型列表
                return menuList;
            }
            catch (Exception ex)
            {
                logger.LogError($"获取商品分类列表出错！{ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// 获取子分类列表
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        public List<MenuItem> GetGoodsTypeSubList(int pId = 0)
        {
            try
            {
                // 获取 父Id对应的子分类信息
                List<GoodsType> list = db.GoodsType.Where(gt => gt.PId == pId).ToList();

                // 转换数据类型为 菜单类型（Tree 树型，级联选择器）

                List<MenuItem> menuList = list.Select(gt => new MenuItem
                {
                    Value = gt.GTId.ToString(),
                    Label = gt.GTName,
                    HasChildren = GetGoodsTypeListByPId(gt.GTId).Count>0
                }).ToList();

                // 返回树型列表
                return menuList;
            }
            catch (Exception ex)
            {
                logger.LogError($"获取商品分类列表出错！{ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// 反加Id列表对应的商品分类名称（用-分隔）
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        private string GetGTNameById(string ids) // ",1,3,9,"
        {
            try
            {
                // 转换字符串为字符数组
                string[] idArr = ids.Split(',',StringSplitOptions.RemoveEmptyEntries); // 拆分字符串为数组，把空值删除
                // 查询Id对应的商品分类列表
                List<GoodsType> gtList = db.GoodsType.Where(gt => idArr.Contains(gt.GTId.ToString())).ToList();
                // 获取分类名称
                List<string> nameList = gtList.Select(gt => gt.GTName).ToList();

                // 转换List为字符串
                string gtName = string.Join("-", nameList);

                return gtName;
            }
            catch (Exception ex)
            {
                logger.LogError($"获取商品分类名称出错！{ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// 添加商品分类
        /// </summary>
        /// <param name="gt"></param>
        /// <returns></returns>
        public int GoodsTypeAdd(GoodsType gt)
        {
            try
            {
                db.GoodsType.Add(gt);
                return db.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.LogError($"添加商品分类出错！{ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// 修改商品分类名称
        /// </summary>
        /// <param name="gtId"></param>
        /// <param name="gtName"></param>
        /// <returns></returns>
        public int GoodsTypeUpd(int gtId,string gtName)
        {
            try
            {
                // 要修改的商品分类
                var gt = db.GoodsType.Find(gtId);
                // 修改它的名称
                gt.GTName = gtName;
                db.GoodsType.Update(gt);
                return db.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.LogError($"修改商品分类出错！{ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// 删除商品分类
        /// </summary>
        /// <param name="gtId">分类Id</param>
        /// <returns></returns>
        public int GoodsTypeDel(int gtId)
        {
            try
            {
                // 要删除的商品分类
                var gt = db.GoodsType.Find(gtId);
                db.GoodsType.Remove(gt);
                return db.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.LogError($"删除商品分类出错！{ex.Message}");
                throw;
            }
        }
        #endregion

        #region 商品

        /// <summary>
        /// 品牌列表
        /// </summary>
        /// <returns></returns>
        public List<Brand> GetBrandList()
        {
            try
            {
                return db.Brand.ToList();
            }
            catch (Exception ex)
            {
                logger.LogError($"获取品牌列表出错！{ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// 商品属性分类列表
        /// </summary>
        /// <returns></returns>
        public List<GoodsPropType> GetGoodsPropTypeList()
        {
            try
            {
                return db.GoodsPropType.ToList();
            }
            catch (Exception ex)
            {
                logger.LogError($"获取商品属性分类列表出错！{ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// 反回商品属性分类对应的属性值
        /// </summary>
        /// <param name="gptId">属性分类Id</param>
        /// <returns></returns>
        public List<GoodsProp> GetGoodsPropList(int gptId)
        {
            try
            {
                return db.GoodsProp.Where(gp=>gp.GPTId == gptId).ToList();
            }
            catch (Exception ex)
            {
                logger.LogError($"获取商品属性列表出错！{ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// 商品列表（分页，4个查询条件）
        /// </summary>
        /// <param name="page">页序号</param>
        /// <param name="size">页大小（一页显示几要）</param>
        /// <param name="totalCount">总条数</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="gName">商品名称</param>
        /// <param name="bId">品牌id </param>
        /// <param name="gtId">分类Id</param>
        /// <param name="sDate">添加日期（开始）</param>
        /// <param name="eDate">添加日期（结束）</param>
        /// <returns></returns>
        public List<Goods> GetGoodsList(int page,int size,out int totalCount,out int pageCount,string? gName,int bId,int gtId,string? sDate,string? eDate)
        {
            try
            {
                // 获取所有未删除的商品信息（加入导航的信息）
                var list = db.Goods.Include(g => g.Brand).Include(g => g.GoodsPropType).Where(g => g.IsDel == false).AsQueryable();

                // 查询条件
                // 商品名称
                if (!string.IsNullOrEmpty(gName))
                {
                    list = list.Where(g=>g.GName.Contains(gName));
                }
                // 品牌
                if (bId > 0)
                {
                    list = list.Where(g => g.BId == bId);
                }
                // 分类Id(查询范围下的有信息，如：选择家用电器，要显示它下面所的子分类下的商品信息)
                if(gtId > 0)
                {
                    list = list.Where(list=>list.GTIdAll.Contains($",{gtId},"));
                }
                // 添加日期
                if (!string.IsNullOrEmpty(sDate)) // 开始日期（>= 查询条件）
                {
                    list = list.Where(g => g.AddTime >= DateTime.Parse(sDate));
                }

                if (!string.IsNullOrEmpty(eDate)) // 结束日期( < 查询条件 + 1 天）
                {
                    list = list.Where(g => g.AddTime < DateTime.Parse(eDate).AddDays(1));
                }

                // 总条数，总页数
                totalCount = list.Count();
                pageCount = (int)Math.Ceiling(totalCount * 1.0 / size);

                // 分页
                list = list.OrderBy(g => g.GId).Skip((page - 1) * size).Take(size);

                // 返回（要转换类型为List）
                // 设置商品分类名称
                foreach (var item in list.ToList())
                {
                    item.GTName = this.GetGTNameById(item.GTIdAll);
                }
                return list.ToList();

            }
            catch (Exception ex)
            {
                logger.LogError($"获取商品列表出错！{ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// 反填
        /// </summary>
        /// <param name="id">商品Id</param>
        /// <returns></returns>
        public Goods GetGoodsInfo(int id)
        {
            try
            {
                return db.Goods.Find(id);
            }
            catch (Exception ex)
            {
                logger.LogError($"获取商品信息出错！{ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="goods">商品信息</param>
        /// <returns></returns>
        public int GoodsAdd(Goods goods)
        {
            try
            {
                //// 添加时间
                //goods.AddTime = DateTime.Now;
                db.Goods.Add(goods);
                return db.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.LogError($"添加商品信息出错！{ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// 修改商品信息
        /// </summary>
        /// <param name="goods">商品信息</param>
        /// <returns></returns>
        public int GoodsUpd(Goods goods)
        {
            try
            {
                db.Goods.Update(goods);
                return db.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.LogError($"修改商品信息出错！{ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// 逻辑删除商品信息
        /// </summary>
        /// <param name="id">商品Id</param>
        /// <returns></returns>
        public int GoodsDel(int id)
        {
            try
            {
                // 要删除的商品信息
                var goods = db.Goods.Find(id);
                // 修改删除标志（逻辑删除）
                goods.IsDel = true;

                return db.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.LogError($"删除商品信息出错！{ex.Message}");
                throw;
            }
        }

        #endregion
        #endregion

    }
}
