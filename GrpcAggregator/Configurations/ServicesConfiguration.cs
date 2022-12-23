using System.Net;
using GrpcAggregator.Policies;
using GrpcAggregator.Protos;
using Microsoft.Extensions.Options;
using Polly;

namespace GrpcAggregator.Configurations;

public static class ServicesConfiguration
{
    public static IServiceCollection AddGrpcServices(this IServiceCollection services)
    {
        services.AddGrpcClient<PossibleTime.PossibleTimeClient>((serv, options) =>
        {
            var possibleTimeApi = serv.GetRequiredService<IOptions<UrlsConfig>>().Value.GrpcPossibleTime;
            options.Address = new Uri(possibleTimeApi);
        }).AddPolicyHandler(RetryFunc);

        services.AddGrpcClient<Customers.CustomersClient>((serv, options) =>
        {
            var customersApi = serv.GetRequiredService<IOptions<UrlsConfig>>().Value.GrpcCustomers;
            options.Address = new Uri(customersApi);
        }).AddPolicyHandler(RetryFunc);

        services.AddGrpcClient<Barbers.BarbersClient>((serv, options) =>
        {
            var barbersApi = serv.GetRequiredService<IOptions<UrlsConfig>>().Value.GrpcBarbers;
            options.Address = new Uri(barbersApi);
        }).AddPolicyHandler(RetryFunc);

        services.AddGrpcClient<Appointments.AppointmentsClient>((serv, options) =>
        {
            var appointmentsApi = serv.GetRequiredService<IOptions<UrlsConfig>>().Value.GrpcAppointments;
            options.Address = new Uri(appointmentsApi);
        }).AddPolicyHandler(RetryFunc);

        services.AddGrpcClient<User.UserClient>((serv, options) =>
        {
            var orderingApi = serv.GetRequiredService<IOptions<UrlsConfig>>().Value.GrpcUsers;
            options.Address = new Uri(orderingApi);
        }).AddPolicyHandler(RetryFunc);

        return services;
    }

    private static readonly Func<HttpRequestMessage, IAsyncPolicy<HttpResponseMessage>> RetryFunc = (request) =>
        request.Method == HttpMethod.Get
            ? new GrpcClientPolicy().ImmediateHttpRetry
            : new GrpcClientPolicy().LinearHttpRetry;
}