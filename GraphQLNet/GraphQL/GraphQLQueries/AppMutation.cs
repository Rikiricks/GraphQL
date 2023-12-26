using DataAccess.Entities;
using GraphQL;
using GraphQL.Types;
using GraphQLService.GraphQLTypes;
using Repository;

namespace GraphQLService.GraphQLQueries
{
    public class AppMutation : ObjectGraphType
    {
        public AppMutation(ICustomerRepository repository)
        {
            FieldAsync<CustomerType>(
            "createCustomer",
            arguments: new QueryArguments(new QueryArgument<NonNullGraphType<CustomerInputType>> { Name = "customer" }),
            resolve: async context =>
            {
                var customer = context.GetArgument<Customer>("customer");
                return await repository.CreateCustomer(customer);
            });

            FieldAsync<CustomerType>(
    "updateCustomer",
    arguments: new QueryArguments(
        new QueryArgument<NonNullGraphType<CustomerInputType>> { Name = "customer" },
        new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "customerId" }),
    resolve: async context =>
    {
        var customer = context.GetArgument<Customer>("customer");
        var customerId = context.GetArgument<long>("customerId");
        var dbCustomer = await repository.GetById(customerId);
        if (dbCustomer == null)
        {
            context.Errors.Add(new ExecutionError("Couldn't find customer in db."));
            return null;
        }
        return await repository.UpdateCustomer(dbCustomer, customer);
    }
);

            FieldAsync<StringGraphType>(
    "deleteCustomer",
    arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "customerId" }),
    resolve: async context =>
    {
        var customerId = context.GetArgument<long>("customerId");
        var customer = await repository.GetById(customerId);
        if (customer == null)
        {
            context.Errors.Add(new ExecutionError("Couldn't find customer in db."));
            return null;
            //return new MutationResponse() { IsError = true};
        }

        await repository.DeleteCustomer(customer);
        var res = new MutationResponse()
        {
            Message = $"The customer with the id: {customerId} has been successfully deleted from db."
        };
        return res.Message;
    }
);

        }
    }
}
