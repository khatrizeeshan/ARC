using ARC.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ARC.Infrastructure
{
    public class AuthorizationRequestDocumentService : IDocumentService<AuthorizationRequest>
    {
        private IHttpClientFactory Factory { get; set; }
        private IConfiguration Configuration { get; set; }
        private ILogger<AuthorizationRequestDocumentService> Logger { get; set; }

        public AuthorizationRequestDocumentService(IHttpClientFactory factory, IConfiguration configuration, ILogger<AuthorizationRequestDocumentService> logger)
        {
            Factory = factory;
            Configuration = configuration;
            Logger = logger;
        }
            
        public async Task<AuthorizationRequest> CreateAsync(AuthorizationRequest document)
        {
            var endpoint = "/public/v1/documents";
            var request = new PandaDocCreateDocument
            {
                name = $"{document.Engagement.Client.Code} - {document.Engagement.Code} - Authorization Request",
                template_uuid = Configuration["PandaDoc:AuthorizationTemplate"]
            };

            request.AddRecipient(document.ContactEmail, document.ContactName);
            request.AddToken("YearEndDate", document.Engagement.ClientYearEndDate.ToString());

            var result = await PostAsync<PandaDocCreateDocument, PandaDocDocumentResult>(request, endpoint);

            document.DocumentId = result.id;
            document.DocumentStatus = result.status;

            return document;
        }

        public async Task<AuthorizationRequest> SendAsync(AuthorizationRequest document)
        {
            var endpoint = $"/public/v1/documents/{document.DocumentId}/send";

            var request = new PandaDocSendDocument
            {
                subject = $"Authoriation Request - {document.Engagement.Name}",
                message = $"Kindly approve the request."
            };

            var result = await PostAsync<PandaDocSendDocument, PandaDocDocumentResult>(request, endpoint);

            document.DocumentStatus = result.status;

            return document;
        }

        public Task<AuthorizationRequest> GetAsync(string documentId)
        {
            throw new NotImplementedException();
        }

        public Task<AuthorizationRequest> ResendAsync(AuthorizationRequest document)
        {
            throw new NotImplementedException();
        }

        private async Task<TResponse> PostAsync<TRequest, TResponse>(TRequest request, string endpoint)
        {
            var client = Factory.CreateClient("pandadoc");
            var options = new JsonSerializerOptions();

            var content = new StringContent(
                JsonSerializer.Serialize(request, options),
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync(endpoint, content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Logger.LogError(error);
                throw new Exception($"{typeof(TRequest).Name} request has failed.");
            }

            using var stream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<TResponse>(stream);

            return result;
        }
    }
}
