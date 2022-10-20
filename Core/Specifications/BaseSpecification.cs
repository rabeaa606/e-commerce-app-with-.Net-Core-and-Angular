namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification()
        {
        }

        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; } //where

        public List<Expression<Func<T, object>>> Includes { get; } =
        new List<Expression<Func<T, object>>>(); //Include
        public Expression<Func<T, object>> OrderBy { get; private set; }//where
        public Expression<Func<T, object>> OrderByDescending { get; private set; }//where
        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPagingEnabled { get; private set; }

        protected void AddInclude(Expression<Func<T, object>> IncludeExpression)
        {
            Includes.Add(IncludeExpression);
        }
        protected void AddOrderBy(Expression<Func<T, object>> OrderByExpression)
        {
            OrderBy = OrderByExpression;
        }
        protected void AddOrderByDescending(Expression<Func<T, object>> OrderByDescendingExpression)
        {
            OrderByDescending = OrderByDescendingExpression;
        }
        protected void ApplyPaging(int skip,
        int take)
        {
            Take = take;
            Skip = skip;
            IsPagingEnabled = true;
        }
    }
}