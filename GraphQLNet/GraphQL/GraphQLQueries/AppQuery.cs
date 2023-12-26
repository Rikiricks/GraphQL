using GraphQL;
using GraphQL.Types;
using GraphQL.Utilities.Federation;
using GraphQLService.GraphQLTypes;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQLService.GraphQLQueries
{
    public class AppQuery : ObjectGraphType
    {
        public AppQuery(ICustomerRepository repository)
        {
            FieldAsync<ListGraphType<CustomerType>>(
               "customers",
               resolve: async context => await repository.GetAll()
           );

            FieldAsync<CustomerType>(
            "customer",
            arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "customerId" }),
            resolve: async context =>
            {
                var id = context.GetArgument<long>("customerId");
                if(id > 0)
                {
                    return await repository.GetById(id);
                }
                else
                {
                    context.Errors.Add(new ExecutionError("Wrong value for id"));
                    return null;
                }
            }
        );
        }
    }
}
