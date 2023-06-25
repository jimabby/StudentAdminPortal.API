using Microsoft.EntityFrameworkCore;
using StudentAdminPortal.API.DataModel;

namespace StudentAdminPortal.API.Repositories
{
    public class SQLStudentRepositories:IStudentRepository
    {
        private readonly StudentAdminContext context;
        public SQLStudentRepositories(StudentAdminContext context) 
        {
            this.context = context;
        }
        public async Task<List<Student>> GetStudentsAsync()
        {
            return await context.Student.Include(nameof(Gender)).Include(nameof(Address)).ToListAsync();
        }
        public async Task<Student> GetStudentAsync(Guid studentId)
        {
            return await context.Student
                .Include(nameof(Gender)).Include(nameof(Address))
                .FirstOrDefaultAsync(x => x.id == studentId);
        }
    }
}
