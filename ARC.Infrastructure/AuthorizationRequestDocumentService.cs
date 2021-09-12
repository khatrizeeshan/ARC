using ARC.Domain;
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

        public AuthorizationRequestDocumentService(IHttpClientFactory factory)
        {
            Factory = factory;
        }
            
        public async Task<AuthorizationRequest> CreateAsync(AuthorizationRequest document)
        {
            var client = Factory.CreateClient("pandadoc");
            var options = new JsonSerializerOptions();
            var request = new PandaDocCreateDocument();
            var content = new StringContent(
                JsonSerializer.Serialize(request, options),
                Encoding.UTF8,
                "application/json");

            
            using var response = await client.PostAsync("/public/v1/documents", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Create document request failed.");
            }

            using var stream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<PandaDocCreateDocumentResult>(stream);

            document.DocumentId = result.id;
            document.DocumentStatus = result.status;

            return document;
        }

        public Task<AuthorizationRequest> GetAsync(string documentId)
        {
            throw new NotImplementedException();
        }

        public Task ResendAsync(string documentId)
        {
            throw new NotImplementedException();
        }

        public Task SendAsync(string documentId)
        {
            throw new NotImplementedException();
        }
    }
}
