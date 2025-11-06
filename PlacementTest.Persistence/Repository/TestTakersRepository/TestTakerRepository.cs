using Microsoft.EntityFrameworkCore;
using PlacementTest.Application.Repository.TestTakersRepository;
using PlacementTest.Domain.Entities;
using PlacementTest.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacementTest.Persistence.Repository.TestTakersRepository
{
    public class TestTakerRepository : BaseRepository<TestTakers>, ITestTakerRepository
    {
        public TestTakerRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<TestTakers> GetByID(string BannerID, CancellationToken cancellationToken)
        {
            var testTaker = await Context.TestTakers.FirstOrDefaultAsync(x => x.BannerID == BannerID, cancellationToken);
            if (testTaker == null)
            {
                throw new InvalidOperationException($"TestTaker with BannerID '{BannerID}' not found.");
            }
            return testTaker;
        }
    }
}
