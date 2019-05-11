using System.ComponentModel.DataAnnotations.Schema;

namespace THH.Model.ReportModel.DBModel
{
    [Table("ReportLog")]
    public class ReportLog : BaseModel
    {
        /// <summary>
        /// 日志类型
        /// </summary>
        public string LogType { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        public int ReportId { get; set; }
        /// <summary>
        /// 报表名称
        /// </summary>
        public string ReportName { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
    }
}
