using DataAccess.Entities;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQLService.GraphQLTypes
{
    public class CustomerType : ObjectGraphType<Customer>
    {
        public CustomerType() 
        {
            Field(x => x.Id, type: typeof(IdGraphType)).Description("Id property from the customer object.");
            Field(x => x.FirstName).Description("FirstName property from the customer object.");
            Field(x => x.LastName).Description("LastName property from the customer object.");
            Field(x => x.Email).Description("Email property from the customer object.");
            Field(x => x.MobileNo).Description("MobileNo property from the customer object.");
            Field(x => x.AddedDate).Description("AddedDate property from the customer object.");
            Field(x => x.ModifiedDate).Description("ModifiedDate property from the customer object.");
        }
    }
}
