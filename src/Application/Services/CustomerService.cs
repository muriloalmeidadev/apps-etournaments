using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Fwks.Core.Abstractions.Contexts;
using Fwks.Core.Abstractions.Services.Infra;
using Fwks.Core.Domain;
using Fwks.Core.Extensions;
using Caesareum.ETournamentsApp.Core.Abstractions.Repositories;
using Caesareum.ETournamentsApp.Core.Abstractions.Services;
using Caesareum.ETournamentsApp.Core.Entities;
using Caesareum.ETournamentsApp.Core.Enums;
using Caesareum.ETournamentsApp.Core.Requests;
using Caesareum.ETournamentsApp.Core.Responses;
using Microsoft.Extensions.Logging;

namespace Caesareum.ETournamentsApp.Application.Services;

internal sealed class CustomerService : ICustomerService
{
    private readonly ILogger<CustomerService> _logger;
    private readonly INotificationContext _notifications;
    private readonly ITransactionService _transaction;
    private readonly ICustomerRepository<CustomerEntity, int> _postgresRepository;
    private readonly ICustomerRepository<CustomerDocument, Guid> _mongoRepository;

    public CustomerService(
        ILogger<CustomerService> logger,
        INotificationContext notifications,
        ITransactionService transaction,
        ICustomerRepository<CustomerEntity, int> postgresRepository,
        ICustomerRepository<CustomerDocument, Guid> mongoRepository)
    {
        _logger = logger;
        _notifications = notifications;
        _postgresRepository = postgresRepository;
        _mongoRepository = mongoRepository;
        _transaction = transaction;
    }

    public async Task<CustomerCreatedResponse> AddAsync(AddCustomerRequest request)
    {
        var guid = Guid.NewGuid();

        await Task.WhenAll(
            AddToMongoAsync(request, guid), 
            AddToPostgresAsync(request, guid));

        return new(guid);

        async Task AddToMongoAsync(AddCustomerRequest request, Guid guid)
        {
            try
            {
                await _mongoRepository.AddAsync(new()
                {
                    Id = guid,
                    Name = request.Name,
                    Email = request.Email,
                    DateOfBirth = request.DateOfBirth,
                    PhoneNumber = request.PhoneNumber,
                });

                _logger.TraceCorrelatedInfo($"Added customer to mongo database at {DateTime.UtcNow:HH:mm:ss}");
            }
            catch (Exception ex)
            {
                _notifications.Add(ex);
            }
        }

        async Task AddToPostgresAsync(AddCustomerRequest request, Guid guid)
        {
            try
            {
                _ = DateOnly.TryParse(request.DateOfBirth, out var dateOfBirth);

                await _postgresRepository.AddAsync(new()
                {
                    Guid = guid,
                    Name = request.Name,
                    Email = request.Email,
                    DateOfBirth = dateOfBirth,
                    PhoneNumber = request.PhoneNumber,
                });

                if (await _transaction.CommitAsync())
                    _logger.TraceCorrelatedInfo($"Added customer to postgres database at {DateTime.UtcNow:HH:mm:ss}");
            }
            catch (Exception ex)
            {
                _notifications.Add(ex);
            }
        }
    }

    public async Task<Page<CustomerResponse>> FindByFilterAsync(GetCustomerByNameRequest request)
    {
        return request.DbType switch
        {
            DbType.Postgres => await FetchFromPostgres(),
            _ => await FetchFromMongoDb(),
        };

        async Task<Page<CustomerResponse>> FetchFromPostgres()
        {
            var customers = await _postgresRepository.FindPageByAsync(request.CurrentPage, request.PageSize, Predicate());

            _logger.TraceCorrelatedInfo("Finding customers by filter.", new
            {
                Filter = request,
                ResultCount = customers.Items.Count
            });

            if (!customers.Items.Any())
                _notifications.Add("404", "No Customers were found.");

            return customers.Transform(x =>
                new CustomerResponse(x.Guid, x.Name, x.DateOfBirth.ToString(), x.Email, x.PhoneNumber));

            Expression<Func<CustomerEntity, bool>> Predicate()
            {
                return request.Name.IsEmpty()
                    ? default
                    : x => x.Name.ToLower().Contains(request.Name.ToLower());
            }
        }

        async Task<Page<CustomerResponse>> FetchFromMongoDb()
        {
            var customers = await _mongoRepository.FindPageByAsync(request.CurrentPage, request.PageSize, Predicate());

            _logger.TraceCorrelatedInfo("Finding customers by filter.", new
            {
                Filter = request,
                ResultCount = customers.Items.Count
            });

            if (!customers.Items.Any())
                _notifications.Add("404", "No Customers were found.");

            return customers.Transform(x =>
                new CustomerResponse(x.Id, x.Name, x.DateOfBirth, x.Email, x.PhoneNumber));

            Expression<Func<CustomerDocument, bool>> Predicate()
            {
                return request.Name.IsEmpty()
                    ? default
                    : x => x.Name.ToLower().Contains(request.Name.ToLower());
            }
        }
    }
}
