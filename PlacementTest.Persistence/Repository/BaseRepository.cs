using Microsoft.EntityFrameworkCore;
using PlacementTest.Application.Common.Services;
using PlacementTest.Application.Repository;
using PlacementTest.Domain.Common;
using PlacementTest.Persistence.Context;

namespace PlacementTest.Persistence.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext Context;
        private readonly ICurrentUserService _currentUserService;

        public BaseRepository(ApplicationDbContext context, ICurrentUserService currentUserService)
        {
            Context = context;
            _currentUserService = currentUserService;
        }

        public void Create(T entity)
        {
            entity.CreatedDate = DateTimeOffset.UtcNow;
            entity.UpdatedDate = DateTimeOffset.UtcNow;
            entity.CreatedBy = _currentUserService.UserId ?? Guid.Empty;
            entity.UpdatedBy = _currentUserService.UserId ?? Guid.Empty;
            Context.Add(entity);
        }

        public void Update(T entity)
        {
            entity.UpdatedDate = DateTimeOffset.UtcNow;
            entity.UpdatedBy = _currentUserService.UserId ?? Guid.Empty;
            Context.Update(entity);
        }

        public void Delete(T entity)
        {
            entity.DeletedDate = DateTimeOffset.UtcNow;
            entity.UpdatedBy = _currentUserService.UserId ?? Guid.Empty;
            entity.IsDeleted = true;
            Context.Update(entity);
        }

        public Task<T> Get(Guid id, CancellationToken cancellationToken)
        {
            return Context.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken)!;
        }

        public Task<List<T>> GetAll(CancellationToken cancellationToken)
        {
            return Context.Set<T>().ToListAsync(cancellationToken);
        }
    }
}
