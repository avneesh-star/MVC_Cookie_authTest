using System;
using System.Collections.Generic;

namespace authTest.Data.Entities
{
    public partial class TblRole
    {
        public TblRole()
        {
            TblUserMasters = new HashSet<TblUserMaster>();
        }

        public int RoleId { get; set; }
        public string? RoleName { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<TblUserMaster> TblUserMasters { get; set; }
    }
}
