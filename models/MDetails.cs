using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM_1_21_;

namespace FindDetails.models
{
    [MapTable("details2")]
   public class MDetails
    {
        [MapPrimaryKey("id",Generator.Native)]
        public int Id { get; set; }
        [MapColumn("number")]
        public string Number { get; set; }
        [MapColumn("isshow")]
        public int IsShow { get; set; }
        [MapColumnType("TEXT")]
        [MapColumn("description")]
        public string Description { get; set; }
        [MapColumn("verdict")]
        public int Verdict { get; set; }
    }

   [MapTable("details")]
   public class MDetails2
   {
       [MapPrimaryKey("id", Generator.Native)]
       public Guid Id { get; set; } = Guid.NewGuid();
       [MapColumn("number")]
       public string Number { get; set; }
       [MapColumn("isshow")]
       public int IsShow { get; set; }
       [MapColumnType("TEXT")]
       [MapColumn("description")]
       public string Description { get; set; }
       [MapColumn("verdict")]
       public int Verdict { get; set; }
   }
}
