using System;

namespace CodeFun.LogConfig
{
    public class NLogItemEntity
    {
        public  DateTime? CreationDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public dynamic Config { get; set; }
    }
}