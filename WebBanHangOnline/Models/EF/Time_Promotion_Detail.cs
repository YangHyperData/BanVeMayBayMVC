using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebBanHangOnline.Models.EF
{
    [Table("tb_Time_Promotion_Detail")]
    public class Time_Promotion_Detail
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Time_Promotion")]
        public int TimePromotionId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Time_Promotion Time_Promotion { get; set; }
        public virtual Product Product { get; set; }
    }
}
