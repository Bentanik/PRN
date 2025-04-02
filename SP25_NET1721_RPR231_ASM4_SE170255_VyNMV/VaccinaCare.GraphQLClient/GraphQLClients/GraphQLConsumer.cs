using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using VaccinaCare.GraphQLClient.Models;
using static VaccinaCare.GraphQLClient.Models.HealthGuide;

namespace VaccinaCare.GraphQLClient.GraphQLClients;

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
                query HealthGuide($id: String!) {
    schedule(id: $id) {
        id
        serviceId
        completedAt
        time
        scheduleTypeId
        expertNote
        referedLink
        customerNote
        expertResponse
        rescheduledTime
        rescheduledCount
        appointmentMode
        location
        priority
        reminderSent
        createdAt
        modifiedAt
        createdBy
        modifiedBy
        isActive
        scheduleType {
            id
            name
            startDate
            endDate
            buffer
            bufferUnit
            createdAt
            modifiedAt
            createdBy
            modifiedBy
            isActive
            isDeleted
        }
        service {
            id
            name
            description
            price
            currency
            commission
            commissionRate
            estimatedDuration
            durationUnit
            expertId
            categoryId
            image
            serviceCode
            avgRating
            createdAt
            modifiedAt
            createdBy
            modifiedBy
            isActive
            isDeleted
        }
    }
}
            ",
                Variables = new { id }
                //,OperationName = "CategoryBankAccounts"
            };
            #endregion

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
}
