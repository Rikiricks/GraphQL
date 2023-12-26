using GraphQL;
using GraphQL.Client.Abstractions;
using GraphQL.Types;
using GraphQLDemo.Model;
using GraphQLService.GraphQLQueries;

namespace GraphQLDemo.GraphQlClient
{
    public class CustomerConsumerService
    {
        private readonly IGraphQLClient _client;
        public CustomerConsumerService(IGraphQLClient client)
        {
            _client = client;
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            var query = new GraphQLRequest
            {
                Query = @"
                query customersQuery{
                  customers {
                    id
                    firstName
                    lastName
                    email
                  }
                }"
            };
            var response = await _client.SendQueryAsync<ResponseCustomerCollectionType>(query);
            return response.Data.Customers;
        }

        public async Task<Customer> GetCustomer(long id)
        {
            var query = new GraphQLRequest
            {
                Query = @"
                query customerQuery($customerId: ID!) {
                   customer(customerId: $customerId) {
                    id
                    firstName
                    lastName
                    email
                  }
                }",
                Variables = new { customerId = id }
            };
            var response = await _client.SendQueryAsync<ResponseCustomerType>(query);
            return response.Data.Customer;
        }

        public async Task<Customer> CreateCustomer(CustomerInput customerToCreate)
        {
            var query = new GraphQLRequest
            {
                Query = @"
                mutation($customer: customerInput!){
                  createCustomer(customer: $customer){
                     id
                    firstName
                    lastName
                    email
                  }
                }",
                Variables = new { customer = customerToCreate }
            };
            var response = await _client.SendMutationAsync<ResponseCustomerType>(query);
            return response.Data.Customer;
        }

        public async Task<Customer> UpdateCustomer(long id, CustomerInput customerToUpdate)
        {
            var query = new GraphQLRequest {
                Query = @"
                        mutation($customer: customerInput!, $customerId: ID!){
                            updateCustomer(customer:$customer, customerId: $customerId){
                                id
                                firstName
                                lastName
                                email
                            }        
                            }",
                Variables = new { customer = customerToUpdate, customerId = id }
            };
            var response = await _client.SendMutationAsync<ResponseCustomerType>(query);
            return response.Data.Customer;
        }

        public async Task<string> DeleteCustomer(long id)
        {
            var query = new GraphQLRequest
            {
                Query = @"
               mutation($customerId: ID!){
                  deleteCustomer(customerId: $customerId)
                }",
                Variables = new { customerId = id }
            };
            var response = await _client.SendMutationAsync<object>(query);

            if (response.Errors != null)
            {
                return response.Errors[0].Message;
            }
            else
            {
                return response.Data.ToString();
            }
            
        }
    }
}
