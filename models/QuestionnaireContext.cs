
using Microsoft.EntityFrameworkCore;


namespace questionnaireBackend
{
    public class QuestionnaireContext : DbContext
    {
        
        public QuestionnaireContext(DbContextOptions<QuestionnaireContext> options)
            : base(options)
        {
        }

        public DbSet<Questionnaire> Questionnaire { get; set; }
        
    }
}