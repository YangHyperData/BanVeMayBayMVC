using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebBanHangOnline.Models.EF
{
    [Table("tb_Type_ClassProduct")]
    public class Type_ClassProduct
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public string TypeTicket { get; set; }
        public string ClassTicket { get; set; }
        public int Quantity { get; set; }
        public virtual Product Product { get; set; }
    }
}