using System.Net;
using Grpc.Core;
using Polly;
using Polly.Retry;

namespace GrpcAggregator.Policies;

public class GrpcClientPolicy
{
    public AsyncRetryPolicy<HttpResponseMessage> ImmediateHttpRetry { get; }
    public AsyncRetryPolicy<HttpResponseMessage> LinearHttpRetry { get; }
    public AsyncRetryPolicy<HttpResponseMessage> ExponentialHttpRetry { get; }

    public GrpcClientPolicy()
    {
        ImmediateHttpRetry =
            Policy.HandleResult<HttpResponseMessage>(res => ExtractStatusCode(res) != StatusCode.OK)
                .RetryAsync();
        
        LinearHttpRetry =
            Policy.HandleResult<HttpResponseMessage>(res => ExtractStatusCode(res) != StatusCode.OK)
                .WaitAndRetryAsync(5, _ => TimeSpan.FromSeconds(3));
        
        ExponentialHttpRetry =
            Policy.HandleResult<HttpResponseMessage>(res => ExtractStatusCode(res) != StatusCode.OK)
                .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }
    
    private static StatusCode? ExtractStatusCode(HttpResponseMessage response)
    {
        var headers = response.Headers;

        if (!headers.Contains("grpc-status") && response.StatusCode == HttpStatusCode.OK)
            return StatusCode.OK;

        if (headers.Contains("grpc-status"))
            return (StatusCode)int.Parse(headers.GetValues("grpc-status").First());

        return null;
    }
}