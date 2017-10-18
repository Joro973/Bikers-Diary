using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bytes2you.Validation;

namespace BikersDiary.ForumSystem.Data.SaveContext
{
    public class SaveContext : ISaveContext
    {
        private readonly MsSqlDbContext context;

        public SaveContext(MsSqlDbContext context)
        {
            Guard.WhenArgument(context, "context").IsNull().Throw();

            this.context = context;
        }

        public void Commit()
        {
            this.context.SaveChanges();
        }
    }
}
