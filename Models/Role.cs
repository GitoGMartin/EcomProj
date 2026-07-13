using MiNET.Utils;

namespace EcomProj.Models
{
    public class Role
    {
        private Guid roleID { get; set; }
        private string roleName { get; set; }
        private string description { get; set; }
        private DateTime createdDate { get; set; }

        public Role(Guid id,string Name,string descr,DateTime date)
        {
            
        }

    }
}
