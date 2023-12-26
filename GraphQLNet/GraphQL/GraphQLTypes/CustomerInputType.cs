using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQLService.GraphQLTypes
{
    public class CustomerInputType : InputObjectGraphType
    {
        public CustomerInputType()
        {
            Name = "customerInput";
            Field<NonNullGraphType<StringGraphType>>("firstName");
            Field<NonNullGraphType<StringGraphType>>("lastName");
            Field<NonNullGraphType<StringGraphType>>("mobileNo");
            Field<NonNullGraphType<StringGraphType>>("email");
            Field<NonNullGraphType<DateTimeGraphType>>("addedDate");
            Field<NonNullGraphType<DateTimeGraphType>>("modifiedDate");
        }
    }
}
