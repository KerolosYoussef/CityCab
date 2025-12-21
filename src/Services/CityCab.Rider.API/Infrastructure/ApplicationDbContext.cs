namespace CityCab.Rider.API.Infrastructure
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Models.Rider> Riders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            ApplyDeletedAtGlobalFilter(modelBuilder);
        }

        private static void ApplyDeletedAtGlobalFilter(ModelBuilder modelBuilder)
        {
            Expression<Func<BaseTimeStampedModel, bool>> filterExpr = bm => bm.DeletedAt == null;
            var entityTypes = modelBuilder.Model.GetEntityTypes().Where(x => x.ClrType.IsAssignableTo(typeof(BaseTimeStampedModel)));
            foreach (var mutableEntityType in entityTypes)
            {
                // modify expression to handle correct child type
                var parameter = Expression.Parameter(mutableEntityType.ClrType);
                var body = ReplacingExpressionVisitor.Replace(filterExpr.Parameters[0], parameter, filterExpr.Body);
                var lambdaExpression = Expression.Lambda(body, parameter);

                // set filter
                mutableEntityType.SetQueryFilter(lambdaExpression);
            }

        }
    }
}
