using DataAccess.Entities;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQLService.GraphQLQueries
{
    public class MutationResponse
    {
        public object Data { get; set; }
        public bool IsError { get; set; }
        public string Message { get; set; }
    }

    public class DeleteMutationResponse
    {
        public string Response { get; set; }
    }
    public class MutationResponseType : ObjectGraphType<MutationResponse>
    {
        public MutationResponseType()
        {
            Field(a => a.Data);
            Field(a => a.IsError);
            Field(a => a.Message);
        }
    }
    
}
