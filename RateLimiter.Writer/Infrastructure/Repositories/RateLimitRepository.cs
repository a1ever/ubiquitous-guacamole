using MongoDB.Driver;
using RateLimiter.Writer.DomainService.Models;
using RateLimiter.Writer.DomainService.Repositories;

namespace RateLimiter.Writer.Infrastructure.Repositories;

public class RateLimitRepository : IRateLimitRepository
    {
        private readonly IMongoCollection<RateLimit> _rateLimitsCollection;

        public RateLimitRepository(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoDb");
            var mongoClient = new MongoClient(connectionString);

            var database = mongoClient.GetDatabase("RateLimiterDb");

            _rateLimitsCollection = database.GetCollection<RateLimit>("rate_limits");

            var indexKeys = Builders<RateLimit>.IndexKeys.Ascending(r => r.Route);
            var indexOptions = new CreateIndexOptions { Unique = true };
            var indexModel = new CreateIndexModel<RateLimit>(indexKeys, indexOptions);
            _rateLimitsCollection.Indexes.CreateOne(indexModel);
        }

        public async Task<RateLimit> CreateAsync(RateLimit rateLimit)
        {
            await _rateLimitsCollection.InsertOneAsync(rateLimit);
            return rateLimit;
        }

        public async Task<RateLimit> GetByRouteAsync(string route)
        {
            var filter = Builders<RateLimit>.Filter.Eq(r => r.Route, route);
            return await _rateLimitsCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsync(RateLimit rateLimit)
        {
            var filter = Builders<RateLimit>.Filter.Eq(r => r.Route, rateLimit.Route);
            var update = Builders<RateLimit>.Update.Set(r => r.RequestsPerMinute, rateLimit.RequestsPerMinute);
            var result = await _rateLimitsCollection.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(string route)
        {
            var filter = Builders<RateLimit>.Filter.Eq(r => r.Route, route);
            var result = await _rateLimitsCollection.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }
    }