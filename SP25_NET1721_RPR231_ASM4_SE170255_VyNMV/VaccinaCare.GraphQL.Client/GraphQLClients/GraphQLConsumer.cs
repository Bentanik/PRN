using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using VaccinaCare.GraphQL.Client.Models;
using static VaccinaCare.GraphQL.Client.Models.HealthGuide;

namespace VaccinaCare.GraphQL.Client.GraphQLClients;

public class GraphQLConsumer
{
    private static string APIEndPoint = "https://localhost:5050/graphql/";

    private readonly GraphQLHttpClient _graphqlClient = new GraphQLHttpClient(APIEndPoint, new NewtonsoftJsonSerializer());

    public async Task<List<HealthGuide>> GetHealthGuides()
    {
        try
        {
            #region GraphQL Request

            var graphQLRequest = new GraphQLRequest
            {
                Query = @"
                  query HealthGuids {
                    healthGuids {
                        id
                        title
                        content
                        healthGuideCategorieId
                        author
                        createdAt
                        updatedAt
                        isActive
                        views
                        imageUrl
                    }
                }
                "
                //,OperationName = "CategoryBankAccounts"
            };
            #endregion

            //// var response = await _graphqlClient.SendQueryAsync<dynamic>(graphQLRequest);
            var response = await _graphqlClient.SendQueryAsync<HealthGuidesGraphQLResponse>(graphQLRequest);
            var result = response?.Data?.HealthGuids;

            return result;
        }
        catch (Exception ex)
        {
            return new List<HealthGuide>();
        }
    }

    public async Task<HealthGuide> GetDetailsHealthGuide(string id)
    {
        try
        {
            #region GraphQL Request

            var graphQLRequest = new GraphQLRequest
            {
                Query = @"
                query HealthGuid($id: String!) {
                    healthGuid(id: $id) {
                        id
                        title
                        content
                        healthGuideCategorieId
                        author
                        createdAt
                        updatedAt
                        isActive
                        views
                        imageUrl
                    }
                }",
                Variables = new { id }
            };
            #endregion

            //// var response = await _graphqlClient.SendQueryAsync<dynamic>(graphQLRequest);
            var response = await _graphqlClient.SendQueryAsync<HealthGuidesGraphQLResponse>(graphQLRequest);
            var result = response?.Data?.HealthGuid;

            return result;
        }
        catch (Exception ex)
        {
            return new HealthGuide();
        }
    }

    public async Task<int> CreteHealthGuide(HealthGuide healthGuide)
    {
        var query = $@"
        mutation AddHealthGuide {{
            addHealthGuide(
                healthGuid: {{
                    title: ""{healthGuide.Title}""
                    content: ""{healthGuide.Content}""
                    healthGuideCategorieId: {healthGuide.HealthGuideCategorieId}
                    imageUrl: ""{healthGuide.ImageUrl}""
                    author: ""{healthGuide.Author}""
                    isActive: {healthGuide.IsActive.ToString().ToLower()}
                    views: {healthGuide.Views}
                }}
            )
        }}";

        var graphQLRequest = new GraphQLRequest
        {
            Query = query
        };
        var response = await _graphqlClient.SendQueryAsync<int>(graphQLRequest);
        var result = response?.Data;
        return result.Value;
    }

    public async Task<bool> UpdateHealthGuide(HealthGuide healthGuide)
    {
        var query = $@"
            mutation UpdateHealthGuide {{
                updateHealthGuide(
                    healthGuid: {{
                        id: {healthGuide.Id}
                        title: ""{healthGuide.Title}""
                        content: ""{healthGuide.Content}""
                        healthGuideCategorieId: {healthGuide.HealthGuideCategorieId}
                        imageUrl: ""{healthGuide.ImageUrl}""
                        author: ""{healthGuide.Author}""
                        isActive: {healthGuide.IsActive.ToString().ToLower()}
                        views: {healthGuide.Views}
                    }}
                )
            }}";

        var graphQLRequest = new GraphQLRequest
        {
            Query = query
        };

        var response = await _graphqlClient.SendMutationAsync<GraphQLResponse<HealthGuide>>(graphQLRequest);
        return response.Data != null;
    }

    public async Task<bool> DeleteHealthGuide(int id)
    {
        var graphQLRequest = new GraphQLRequest
        {
            Query = @"
            mutation DeleteHealthGuide($id: Int!) {
                deleteHealthGuide(id: $id)
            }",
            Variables = new { id }
        };

        var response = await _graphqlClient.SendMutationAsync<DeleteResponse>(graphQLRequest);
        return response?.Data?.DeleteHealthGuide ?? false;
    }

}
