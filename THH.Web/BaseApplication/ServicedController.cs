using THH.Web.Controllers;

namespace THH.Web.BaseApplication
{
    public class ServicedController<TService> : BaseController where TService : new()
    {
        public ServicedController()
        {
        }
        private TService _service;
        /// <summary>
        /// 用于业务端方法调用
        /// </summary>
        public TService Service
        {
            get
            {
                if (_service == null)
                {
                    _service = new TService();
                }
                return _service;
            }
            set
            {
                _service = value;
            }
        }
    }
}