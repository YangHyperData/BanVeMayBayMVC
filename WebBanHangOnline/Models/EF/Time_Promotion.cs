using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebBanHangOnline.Models.EF
{
    [Table("tb_Time_Promotion")]
    public class Time_Promotion : CommonAbstract
    {
        public Time_Promotion()
        {
            this.Time_Promotion_Details = new HashSet<Time_Promotion_Detail>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal PercentValue { get; set; }
        public bool IsActive { get; set; }
        public bool IsBan { get; set; }
        public virtual ICollection<Time_Promotion_Detail> Time_Promotion_Details { get; set; }
    }
}