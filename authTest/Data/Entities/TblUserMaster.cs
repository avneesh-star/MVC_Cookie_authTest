using System;
using System.Collections.Generic;

namespace authTest.Data.Entities
{
    public partial class TblUserMaster
    {
        public TblUserMaster()
        {
            InverseParent = new HashSet<TblUserMaster>();
        }

        public long Id { get; set; }
        public string Email { get; set; } = null!;
        public string? Name { get; set; }
        public string? Password { get; set; }
        public int? RoleId { get; set; }
        public long? ParentId { get; set; }
        public bool? IsActive { get; set; }

        public virtual TblUserMaster? Parent { get; set; }
        public virtual TblRole? Role { get; set; }
        public virtual ICollection<TblUserMaster> InverseParent { get; set; }
    }
}
