using System.Collections.Generic;
using System.Linq;

namespace THH.Web.Models
{
    public class MenuTree
    {
        private int _Id;
        private string _RightNO;
        private List<string> _RightNOs;
        private string _Name;
        private string _Url;
        private List<MenuTree> _Items;

        public int Id { get { return _Id; } }
        public string RightNO { get { return _RightNO; } }
        public List<string> RightNOs { get { return _RightNOs; } set { _RightNOs = value; } }
        public string Name { get { return _Name; } }
        public string Url { get { return _Url; } }
        public MenuTree()
        {
            _Items = new List<MenuTree>();
            _RightNOs = new List<string>();
        }
        public MenuTree(int id) : this() { _Id = id; }
        public MenuTree(int id, string rightNo, string name, string information, string url)
            : this()
        {
            _Id = id;
            _RightNO = rightNo;
            _Name = name;
            _Url = url;
        }
        /// <summary>
        /// 添加一项子菜单
        /// </summary>
        public void AddItem(MenuTree item)
        {
            _Items.Add(item);
        }
        /// <summary>
        /// 添加多项子菜单
        /// </summary>
        public void AddItem(List<MenuTree> items)
        {
            foreach (var item in items)
            {
                _Items.Add(item);
            }
        }
        /// <summary>
        /// 设置子菜单值
        /// </summary>
        public void SetItem(List<MenuTree> items)
        {
            _Items = items;
        }

        /// <summary>
        /// 子菜单
        /// </summary>
        public IEnumerable<MenuTree> Items()
        {
            foreach (var item in _Items)
            {
                yield return item;
            }
        }
        /// <summary>
        /// 子菜单数目
        /// </summary>
        public int Count() { return _Items.Count(); }
    }
}