using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeBase.Models
{

    public class DateModel : IComparable<DateModel>
    {
        [Key]
        public int Id { get; set; }
        public int ThemeId { get; set; }
        public DateTime Date { get; set; }

        public int CompareTo([AllowNull] DateModel other)
        {
            return DateTime.Compare(this.Date, other.Date);
        }



        //public static implicit operator DateTime(DateModel dm)
        //{
        //    return dm.Date;
        //}
    }
}
